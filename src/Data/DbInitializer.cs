using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace rr_events.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context, IWebHostEnvironment env)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var logger = loggerFactory.CreateLogger("DbInitializer");

            try
            {
                if (env.IsProduction())
                {
                    logger.LogInformation("📦 Production environment: skipping seed and migration.");
                    return;
                }

                logger.LogInformation("🔧 Applying migrations...");
                context.Database.Migrate();
                logger.LogInformation("✅ Migrations complete.");

                if (context.Events.Any())
                {
                    logger.LogInformation("📂 Events already exist — skipping seed.");
                    return;
                }

                logger.LogInformation("🌱 Seeding development sample events...");
                context.Events.AddRange(SeedData.Events);
                context.SaveChanges();
                logger.LogInformation("✅ Seed completed.");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "🔥 Error during seed.");
                throw;
            }
        }
    }
}
