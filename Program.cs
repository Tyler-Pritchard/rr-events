using rr_events.Data;
using rr_events.Controllers;
using rr_events.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ------------------------
// 🔧 Service Configuration
// ------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEventQueryService, EventQueryService>();
builder.Services.AddScoped<IEventCommandService, EventCommandService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger (with XML comments)
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// ------------------------
// ⚙️ App Pipeline Configuration
// ------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
