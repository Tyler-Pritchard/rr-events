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
                        .Include(e => e.FanClubPresale)
                        .FirstOrDefault(e => e.Slug == seedEvent.Slug);

                    if (existing == null)
                    {
                        var newEvent = new Event
                        {
                            Title = seedEvent.Title,
                            StartTimeUtc = seedEvent.StartTimeUtc,
                            EndTimeUtc = seedEvent.EndTimeUtc,
                            Location = seedEvent.Location,
                            Venue = seedEvent.Venue,
                            TourName = seedEvent.TourName,
                            Description = seedEvent.Description,
                            TicketsSoldOut = seedEvent.TicketsSoldOut,
                            TicketLink = seedEvent.TicketLink,
                            EnhancedExperienceSoldOut = seedEvent.EnhancedExperienceSoldOut,
                            EnhancedExperienceLink = seedEvent.EnhancedExperienceLink,
                            SupportingActsSerialized = seedEvent.SupportingActsSerialized,
                            EventImageUrl = seedEvent.EventImageUrl,
                            IsPrivate = seedEvent.IsPrivate,
                            Slug = seedEvent.Slug,
                            FanClubPresale = seedEvent.FanClubPresale is not null
                                ? new PresaleDetails
                                {
                                    AccessCode = seedEvent.FanClubPresale.AccessCode,
                                    StartUtc = seedEvent.FanClubPresale.StartUtc,
                                    EndUtc = seedEvent.FanClubPresale.EndUtc
                                }
                                : null
                        };

                        context.Events.Add(newEvent);
                        created++;
                    }
                    else
                    {
                        // Manually copy primitive and owned type fields (without reusing tracked instances)
                        existing.Title = seedEvent.Title;
                        existing.StartTimeUtc = seedEvent.StartTimeUtc;
                        existing.EndTimeUtc = seedEvent.EndTimeUtc;
                        existing.Location = seedEvent.Location;
                        existing.Venue = seedEvent.Venue;
                        existing.TourName = seedEvent.TourName;
                        existing.Description = seedEvent.Description;
                        existing.TicketsSoldOut = seedEvent.TicketsSoldOut;
                        existing.TicketLink = seedEvent.TicketLink;
                        existing.EnhancedExperienceSoldOut = seedEvent.EnhancedExperienceSoldOut;
                        existing.EnhancedExperienceLink = seedEvent.EnhancedExperienceLink;
                        existing.SupportingActsSerialized = seedEvent.SupportingActsSerialized;
                        existing.EventImageUrl = seedEvent.EventImageUrl;
                        existing.IsPrivate = seedEvent.IsPrivate;
                        existing.Slug = seedEvent.Slug;

                        if (seedEvent.FanClubPresale != null)
                        {
                            existing.FanClubPresale = new PresaleDetails

                            {
                                AccessCode = seedEvent.FanClubPresale.AccessCode,
                                StartUtc = seedEvent.FanClubPresale.StartUtc,
                                EndUtc = seedEvent.FanClubPresale.EndUtc
                            };
                        }
                        else
                        {
                            existing.FanClubPresale = null;
                        }

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
