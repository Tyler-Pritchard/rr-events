using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using rr_events.Models;

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
                context.Database.Migrate();

                int created = 0;
                int updated = 0;

                foreach (var seedEvent in SeedData.Events)
                {
                    var existing = context.Events
                        .FirstOrDefault(e => e.Slug == seedEvent.Slug);

                    if (existing == null)
                    {
                        context.Events.Add(seedEvent);
                        created++;
                    }
                    else
                    {
                        context.Entry(existing).CurrentValues.SetValues(seedEvent);
                        updated++;
                    }
                }

                context.SaveChanges();

                logger.LogInformation($"✅ Seed completed. Created: {created}, Updated: {updated}");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "🔥 Error during seeding.");
                throw;
            }
        }
    }
}
