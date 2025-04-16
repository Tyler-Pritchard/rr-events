using rr_events.Data;
using rr_events.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// ------------------------
// 🔧 Load .env for Development
// ------------------------
if (builder.Environment.IsDevelopment())
{
    var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    if (File.Exists(envPath))
    {
        Env.Load(envPath);
        Console.WriteLine($"✅ Loaded .env file from: {envPath}");
    }
    else
    {
        Console.WriteLine($"⚠️ .env file not found at: {envPath}");
    }
}

// ------------------------
// 🔧 Configuration Binding
// ------------------------
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// ------------------------
// 🔧 Resolve Connection String
// ------------------------
string? connectionString;

if (builder.Environment.IsDevelopment())
{
    connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__DEFAULTCONNECTION");
    Console.WriteLine($"✅ Dev connection string loaded from .env: {connectionString}");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    Console.WriteLine("✅ Prod connection string loaded from Railway environment.");
}

if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("❌ Failed to resolve ConnectionStrings:DefaultConnection.");
    throw new InvalidOperationException("Missing required database connection string.");
}

// ------------------------
// 🔧 Register Services
// ------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://www.robrich.band", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddScoped<IEventQueryService, EventQueryService>();
builder.Services.AddScoped<IEventCommandService, EventCommandService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// ------------------------
// 🚀 App Middleware Pipeline
// ------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        var env = app.Services.GetRequiredService<IWebHostEnvironment>();
        if (env.IsDevelopment())
        {
            DbInitializer.Seed(dbContext, env);
        }
        logger.LogInformation("✅ Database seeded successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ Failed to seed the database.");
    }
}

app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

try
{
    app.Run();
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();

    Console.WriteLine("🔥 Unhandled exception during startup.");
    Console.WriteLine($"🔥 Message: {ex.Message}");
    Console.WriteLine($"🔥 Stack Trace: {ex.StackTrace}");
    
    logger.LogCritical(ex, "🔥 Fatal exception during app.Run().");
    throw;
}

// Required for integration testing
public partial class Program { }
