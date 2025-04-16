using Microsoft.EntityFrameworkCore;
using rr_events.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace rr_events.Data
{
    public static class DbInitializer
    {

        public static void Seed(AppDbContext context, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                context.Database.Migrate();

                if (context.Events.Any())
                    return;

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
                    FanClubPresale = null,
                    Slug = "2025-04-25-downtown-olympia-wa"
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
                    FanClubPresale = null,
                    Slug = "2023-06-15-tacoma-wa"
                };

                context.Events.AddRange(upcomingEvent, pastEvent);
                context.SaveChanges();
            }
        }
    }
}
