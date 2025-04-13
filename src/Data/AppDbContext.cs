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

            modelBuilder.Entity<Event>(entity =>
            {
                entity.OwnsOne(e => e.FanClubPresale);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
