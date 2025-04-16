using rr_events.Models;
using System.Text.Json;

namespace rr_events.Data
{
    public static class SeedData
    {
        public static List<Event> Events => new List<Event>
        {
            new Event
            {
                Id = 4,
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
            },
            new Event
            {
                Id = 3,
                Title = "Halloween Fundraiser Tribute Show",
                StartTimeUtc = new DateTime(2024, 11, 2, 2, 30, 0, DateTimeKind.Utc),
                EndTimeUtc = new DateTime(2024, 11, 2, 5, 30, 0, DateTimeKind.Utc),   
                Location = "Real Art Tacoma, 5412 South Tacoma Way, Tacoma, WA",
                Venue = "Real Art Tacoma",
                TourName = "Benefit Shows & Guest Features",
                Description = "A Halloween tribute night featuring performances by Green Day (When September Ends), Neutral Milk Hotel (Koa Koala ft. Rob Rich), The Pogues (Alexander EP), and The Folk-Pop-Punk-Jukebox (Bailey Ukulele). Proceeds benefitted the Palestine Children's Relief Fund.",
                TicketsSoldOut = true,
                TicketLink = null,
                EnhancedExperienceSoldOut = true,
                EnhancedExperienceLink = null,
                SupportingActsSerialized = JsonSerializer.Serialize(new List<string>
                {
                    "Green Day (When September Ends)",
                    "The Pogues (Alexander EP)",
                    "The Folk-Pop-Punk-Jukebox (Bailey Ukulele)"
                }),
                EventImageUrl = "https://imgur.com/ACLrDCj",
                IsPrivate = false,
                FanClubPresale = null,
                Slug = "2024-11-01-tacoma-wa"
            },
            new Event
            {
                Id = 1,
                Title = "The Polyclinic Morgue Presents",
                StartTimeUtc = new DateTime(2024, 3, 24, 22, 0, 0, DateTimeKind.Utc),
                EndTimeUtc = new DateTime(2024, 3, 24, 1, 0, 0, DateTimeKind.Utc).AddDays(1),
                Location = "Tacoma, WA (DM for address)",
                Venue = "Private Space – The Polyclinic Morgue",
                TourName = "DIY & House Shows",
                Description = "A sober-space Sunday matinee hosted by The Polyclinic Morgue featuring Lace Bass & Suitcase, Pale Blue Dot, Spange Ranger, Free Range Lunatic, and Rob Rich. Donation-based entry ($5–10 NOTAFLOF).",
                TicketsSoldOut = true,
                TicketLink = null,
                EnhancedExperienceSoldOut = true,
                EnhancedExperienceLink = null,
                SupportingActsSerialized = JsonSerializer.Serialize(new List<string>
                {
                    "Lace Bass & Suitcase",
                    "Pale Blue Dot",
                    "Spange Ranger",
                    "Free Range Lunatic"
                }),
                EventImageUrl = "https://imgur.com/aF6m5K3",
                IsPrivate = true,
                FanClubPresale = null,
                Slug = "2024-03-24-tacoma-wa"
            }

        };
    }
}
