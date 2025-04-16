using rr_events.Data;
using rr_events.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DotNetEnv;

// ------------------------
// 🔧 Create App Builder
// ------------------------
var builder = WebApplication.CreateBuilder(args);

// ------------------------
// 📦 Load .env in Development
// ------------------------
if (builder.Environment.IsDevelopment())
{
    var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
    if (File.Exists(envPath))
    {
        Env.Load(envPath);
        Console.WriteLine($"✅ .env file loaded from: {envPath}");
    }
    else
    {
        Console.WriteLine($"⚠️ .env file not found at: {envPath}");
    }
}

// ------------------------
// 🔧 Bind Config from .env if present
// ------------------------
var rawConn = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__DEFAULTCONNECTION");
if (!string.IsNullOrWhiteSpace(rawConn))
{
    builder.Configuration["ConnectionStrings:DefaultConnection"] = rawConn;
}

// ------------------------
// 🔧 Add JSON + Environment Config
// ------------------------
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var effectiveConn = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"🔍 Effective DB Connection: {(string.IsNullOrEmpty(effectiveConn) ? "❌ None found!" : "✅ Loaded")}");

// ------------------------
// 🧱 Register Services
// ------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(effectiveConn));

builder.Services.AddScoped<IEventQueryService, EventQueryService>();
builder.Services.AddScoped<IEventCommandService, EventCommandService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://www.robrich.band")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// ------------------------
// 🚀 Build App and Configure Pipeline
// ------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        DbInitializer.Seed(dbContext);
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
    logger.LogCritical(ex, "🔥 Unhandled exception on startup.");
    throw;
}

// Enable integration testing
public partial class Program { }
