using rr_events.Data;
using rr_events.DTOs;
using rr_events.Models;
using Microsoft.EntityFrameworkCore;

namespace rr_events.Controllers
{
    /// <summary>
    /// Handles write operations related to event creation and modification.
    /// </summary>
    public interface IEventCommandService
    {
        Task<EventResponse> CreateEventAsync(CreateEventRequest request);
        Task<EventResponse?> UpdateEventAsync(UpdateEventRequest request);
        Task<bool> DeleteEventAsync(int id);
    }

    /// <summary>
    /// Provides command logic for creating and modifying events.
    /// </summary>
    public class EventCommandService : IEventCommandService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EventCommandService> _logger;

        public EventCommandService(AppDbContext context, ILogger<EventCommandService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<EventResponse> CreateEventAsync(CreateEventRequest request)
        {
            var newEvent = new Event
            {
                Title = request.Title,
                Description = request.Description,
                StartTimeUtc = request.StartTimeUtc,
                EndTimeUtc = request.EndTimeUtc,
                Location = request.Location,
                IsPrivate = request.IsPrivate,
                TicketLink = request.TicketLink
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            _logger.LogInformation("New event created with ID {Id}", newEvent.Id);

            return new EventResponse
            {
                Id = newEvent.Id,
                Title = newEvent.Title,
                Description = newEvent.Description,
                StartTimeUtc = newEvent.StartTimeUtc,
                EndTimeUtc = newEvent.EndTimeUtc,
                Location = newEvent.Location,
                IsPrivate = newEvent.IsPrivate,
                TicketLink = newEvent.TicketLink
            };
        }

        public async Task<EventResponse?> UpdateEventAsync(UpdateEventRequest request)
        {
            var existing = await _context.Events.FindAsync(request.Id);

            if (existing == null)
            {
                _logger.LogWarning("Attempted to update non-existent event with ID {Id}", request.Id);
                return null;
            }

            existing.Title = request.Title;
            existing.Description = request.Description;
            existing.StartTimeUtc = request.StartTimeUtc;
            existing.EndTimeUtc = request.EndTimeUtc;
            existing.Location = request.Location;
            existing.IsPrivate = request.IsPrivate;
            existing.TicketLink = request.TicketLink;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Event with ID {Id} updated", request.Id);

            return new EventResponse
            {
                Id = existing.Id,
                Title = existing.Title,
                Description = existing.Description,
                StartTimeUtc = existing.StartTimeUtc,
                EndTimeUtc = existing.EndTimeUtc,
                Location = existing.Location,
                IsPrivate = existing.IsPrivate,
                TicketLink = existing.TicketLink
            };
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var existing = await _context.Events.FindAsync(id);

            if (existing == null)
            {
                _logger.LogWarning("Attempted to delete non-existent event with ID {Id}", id);
                return false;
            }

            _context.Events.Remove(existing);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Event with ID {Id} deleted", id);
            return true;
        }
    }
}
