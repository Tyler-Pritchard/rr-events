using Microsoft.EntityFrameworkCore;
using rr_events.Data;
using rr_events.DTOs;
using rr_events.Models;

namespace rr_events.Services
{
    /// <summary>
    /// Handles read operations related to events.
    /// </summary>
    public interface IEventQueryService
    {
        Task<List<EventResponse>> GetAllEventsAsync();
        Task<List<EventResponse>> GetUpcomingEventsAsync();
        Task<EventResponse?> GetEventBySlugAsync(string slug);
    }

    /// <summary>
    /// Provides query logic for fetching events from the database.
    /// </summary>
    public class EventQueryService : IEventQueryService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EventQueryService> _logger;

        public EventQueryService(AppDbContext context, ILogger<EventQueryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<EventResponse>> GetAllEventsAsync()
        {
            var events = await _context.Events.ToListAsync();
            return events.Select(MapToResponse).ToList();
        }

        public async Task<List<EventResponse>> GetUpcomingEventsAsync()
        {
            var now = DateTime.UtcNow;
            var upcoming = await _context.Events
                .Where(e => e.StartTimeUtc > now)
                .OrderBy(e => e.StartTimeUtc)
                .ToListAsync();

            return upcoming.Select(MapToResponse).ToList();
        }
        
        public async Task<EventResponse?> GetEventBySlugAsync(string slug)
        {
            var ev = await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Slug == slug);

            return ev == null ? null : MapToResponse(ev);
        }

        private static EventResponse MapToResponse(Event e)
        {
            return new EventResponse
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartTimeUtc = e.StartTimeUtc,
                EndTimeUtc = e.EndTimeUtc,
                Location = e.Location,
                Slug = e.Slug,
                IsPrivate = e.IsPrivate,
                TicketLink = e.TicketLink
            };
        }
    }
}
