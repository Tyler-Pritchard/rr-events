using Microsoft.EntityFrameworkCore;
using rr_events.Models;
using System.Text.Json;

namespace rr_events.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Convert SupportingActs list to JSON for simple persistence
            var seattleActs = JsonSerializer.Serialize(new List<string> { "Local Opener" });
            var portlandActs = JsonSerializer.Serialize(new List<string> { "Special Guest" });

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Title = "Rob Rich: Seattle Show",
                    Description = "Opening night of the West Coast tour",
                    StartTimeUtc = DateTime.UtcNow.AddDays(-7),
                    EndTimeUtc = DateTime.UtcNow.AddDays(-7).AddHours(2),
                    Location = "Seattle, WA",
                    Venue = "Neptune Theater",
                    TourName = "Dark Roads Tour",
                    TicketsSoldOut = true,
                    TicketLink = "https://example.com/seattle-tickets",
                    EnhancedExperienceSoldOut = false,
                    EnhancedExperienceLink = null,
                    SupportingActsSerialized = seattleActs,
                    IsPrivate = false,
                    EventImageUrl = "https://example.com/seattle.png"
                },
                new Event
                {
                    Id = 2,
                    Title = "Rob Rich: Portland Show",
                    Description = "Next stop on the tour",
                    StartTimeUtc = DateTime.UtcNow.AddDays(5),
                    EndTimeUtc = DateTime.UtcNow.AddDays(5).AddHours(2),
                    Location = "Portland, OR",
                    Venue = "Crystal Ballroom",
                    TourName = "Dark Roads Tour",
                    TicketsSoldOut = false,
                    TicketLink = "https://example.com/portland-tickets",
                    EnhancedExperienceSoldOut = false,
                    EnhancedExperienceLink = "https://example.com/portland-vip",
                    SupportingActsSerialized = portlandActs,
                    IsPrivate = false,
                    EventImageUrl = "https://example.com/portland.png"
                }
            );

            modelBuilder.Entity<Event>(entity =>
            {
                entity.OwnsOne(e => e.FanClubPresale);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
