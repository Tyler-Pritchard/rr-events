using rr_events.Data;
using rr_events.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ------------------------
// 🔧 Load .env for local development only
// ------------------------
if (builder.Environment.IsDevelopment())
{
    DotNetEnv.Env.Load(".env");
    Console.WriteLine("✅ Loaded .env file for development");
}

// ------------------------
// 🔧 Resolve connection string
// ------------------------
var resolvedConn = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__DEFAULTCONNECTION");
if (!string.IsNullOrWhiteSpace(resolvedConn))
{
    builder.Configuration["ConnectionStrings:DefaultConnection"] = resolvedConn;
    Console.WriteLine("✅ Connection string loaded from environment.");
}
else
{
    Console.WriteLine("❌ CONNECTIONSTRINGS__DEFAULTCONNECTION not found.");
}

// ------------------------
// 🔧 Configuration layering
// ------------------------
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// ------------------------
// 🔧 Dependency injection
// ------------------------
var resolvedConnectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
if (string.IsNullOrWhiteSpace(resolvedConnectionString))
{
    Console.WriteLine("❌ No connection string found. Failing fast.");
    throw new InvalidOperationException("Missing ConnectionStrings:DefaultConnection");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(resolvedConnectionString));

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
// 🚀 Pipeline
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
        DbInitializer.Seed(dbContext, env);

        logger.LogInformation("✅ Database seeded.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ Failed to seed database.");
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
    logger.LogCritical(ex, "🔥 Fatal startup error.");
    throw;
}

public partial class Program { }
