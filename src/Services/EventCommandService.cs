using rr_events.Data;
using rr_events.DTOs;
using rr_events.Models;
using Microsoft.EntityFrameworkCore;

namespace rr_events.Services
{
    /// <summary>
    /// Contract for modifying event data (create, update, delete).
    /// </summary>
    public interface IEventCommandService
    {
        /// <summary>
        /// Creates a new event in the system.
        /// </summary>
        /// <param name="request">The event creation data.</param>
        Task<EventResponse> CreateEventAsync(CreateEventRequest request);

        /// <summary>
        /// Updates an existing event.
        /// </summary>
        /// <param name="id">The ID of the event to update.</param>
        /// <param name="request">The new data to apply to the event.</param>
        Task<EventResponse?> UpdateEventAsync(int id, UpdateEventRequest request);

        /// <summary>
        /// Deletes an event by ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        Task<bool> DeleteEventAsync(int id);
    }

    /// <summary>
    /// Handles event write operations including creation, updates, and deletion.
    /// </summary>
    public class EventCommandService : IEventCommandService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EventCommandService> _logger;

        /// <summary>
        /// Constructs the service with required dependencies.
        /// </summary>
        public EventCommandService(AppDbContext context, ILogger<EventCommandService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<EventResponse> CreateEventAsync(CreateEventRequest request)
        {
            var slug = $"{request.StartTimeUtc:yyyy-MM-dd}-{request.Location}".ToLower().Replace(" ", "-");

            var newEvent = new Event
            {
                Title = request.Title,
                Description = request.Description,
                StartTimeUtc = request.StartTimeUtc,
                EndTimeUtc = request.EndTimeUtc,
                Location = request.Location,
                Slug = slug,
                IsPrivate = request.IsPrivate,
                TicketLink = request.TicketLink
            };

            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created new event with ID {Id}", newEvent.Id);

            return new EventResponse
            {
                Id = newEvent.Id,
                Title = newEvent.Title,
                Description = newEvent.Description,
                StartTimeUtc = newEvent.StartTimeUtc,
                EndTimeUtc = newEvent.EndTimeUtc,
                Location = newEvent.Location,
                IsPrivate = newEvent.IsPrivate,
                TicketLink = newEvent.TicketLink,
                Slug = newEvent.Slug
            };
        }

        /// <inheritdoc />
        public async Task<EventResponse?> UpdateEventAsync(int id, UpdateEventRequest request)
        {
            var existingEvent = await _context.Events.FindAsync(id);
            if (existingEvent == null)
            {
                _logger.LogWarning("Attempted to update non-existent event with ID {Id}", id);
                return null;
            }

            existingEvent.Title = request.Title;
            existingEvent.Description = request.Description;
            existingEvent.StartTimeUtc = request.StartTimeUtc;
            existingEvent.EndTimeUtc = request.EndTimeUtc;
            existingEvent.Location = request.Location;
            existingEvent.IsPrivate = request.IsPrivate;
            existingEvent.TicketLink = request.TicketLink;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated event with ID {Id}", id);

            return new EventResponse
            {
                Id = existingEvent.Id,
                Title = existingEvent.Title,
                Description = existingEvent.Description,
                StartTimeUtc = existingEvent.StartTimeUtc,
                EndTimeUtc = existingEvent.EndTimeUtc,
                Location = existingEvent.Location,
                IsPrivate = existingEvent.IsPrivate,
                TicketLink = existingEvent.TicketLink,
                Slug = existingEvent.Slug 
            };
        }

        /// <inheritdoc />
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

            _logger.LogInformation("Deleted event with ID {Id}", id);
            return true;
        }
    }
}
