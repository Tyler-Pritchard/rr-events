using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace rr_events.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context, IWebHostEnvironment env, bool forceSeed = false)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var logger = loggerFactory.CreateLogger("DbInitializer");

            try
            {
                if (!forceSeed && env.IsProduction())
                {
                    logger.LogInformation("📦 Production environment: skipping seed and migration.");
                    return;
                }

                logger.LogInformation("🔧 Applying migrations...");
                context.Database.Migrate(); // ensures the schema is created

                var anyEvents = false;

                try
                {
                    anyEvents = context.Events.Any(); // throws if Events table doesn't exist
                }
                catch (Exception ex)
                {
                    logger.LogWarning("⚠️ Couldn't check if events exist: " + ex.Message);
                }

                if (anyEvents)
                {
                    logger.LogInformation("📂 Events already exist — skipping seed.");
                    return;
                }

                logger.LogInformation("🌱 Seeding events...");
                context.Events.AddRange(SeedData.Events);
                context.SaveChanges();
                logger.LogInformation("✅ Seed completed. Total events added: " + SeedData.Events.Count);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "🔥 Error during seeding.");
                throw;
            }
        }
    }
}
