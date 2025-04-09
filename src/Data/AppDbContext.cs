using Microsoft.EntityFrameworkCore;
using rr_events.Models;

namespace rr_events.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tell EF Core to create a table for Events in the database
        public DbSet<Event> Events { get; set; }
    }
}
