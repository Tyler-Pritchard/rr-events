using Microsoft.EntityFrameworkCore;
using rr_events.Models;
using System.Text.Json;

namespace rr_events.Data
{
    /// <summary>
    /// Static class for initializing the database with sample event data.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Seeds the database with upcoming and past events if none exist.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public static void Seed(AppDbContext context)
        {
            context.Database.Migrate();

            if (context.Events.Any())
            {
                return; // DB has already been seeded
            }

            var upcomingEvent = new Event
            {
                Id = 1,
                Title = "Olympia Arts Walk & Procession of the Species",
                StartTimeUtc = new DateTime(2025, 4, 25, 19, 0, 0, DateTimeKind.Utc),
                EndTimeUtc = new DateTime(2025, 4, 26, 23, 0, 0, DateTimeKind.Utc),
                Location = "Downtown Olympia, WA",
                Venue = "Capitol Way & 5th Ave",
                TourName = "Spring Street Series",
                Description = "A beloved Olympia tradition, the Arts Walk transforms downtown into pop-up galleries and performance areas featuring local artists, musicians, and performers. The Saturday highlight is the Procession of the Species — a colorful, Earth Day-inspired parade of costumes, dance, and music.",
                TicketsSoldOut = true,
                TicketLink = null,
                EnhancedExperienceSoldOut = true,
                EnhancedExperienceLink = null,
                SupportingActsSerialized = JsonSerializer.Serialize(new List<string>()),
                EventImageUrl = null,
                IsPrivate = false,
                FanClubPresale = null
            };

            var pastEvent = new Event
            {
                Id = 2,
                Title = "Tacoma Street Music Fest 2023",
                StartTimeUtc = new DateTime(2023, 6, 15, 19, 0, 0, DateTimeKind.Utc),
                EndTimeUtc = new DateTime(2023, 6, 15, 22, 0, 0, DateTimeKind.Utc),
                Location = "Downtown Tacoma, WA",
                Venue = "6th Ave District",
                TourName = "Northwest Noise",
                Description = "A grassroots performance night in the arts district featuring folk-punk, jazz, and metal. Street corners, small cafes, and alleys all became part of the venue.",
                TicketsSoldOut = false,
                TicketLink = null,
                EnhancedExperienceSoldOut = false,
                EnhancedExperienceLink = null,
                SupportingActsSerialized = JsonSerializer.Serialize(new List<string> { "Profane Sass", "The Buskers" }),
                EventImageUrl = null,
                IsPrivate = false,
                FanClubPresale = null
            };

            context.Events.AddRange(upcomingEvent, pastEvent);
            context.SaveChanges();
        }
    }
}
