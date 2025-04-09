using Microsoft.AspNetCore.Mvc;
using rr_events.Services;
using rr_events.DTOs;
using Microsoft.Extensions.Logging;

namespace rr_events.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventQueryService _queryService;
        private readonly IEventCommandService _commandService;
        private readonly ILogger<EventsController> _logger;

        public EventsController(
            IEventQueryService queryService,
            IEventCommandService commandService,
            ILogger<EventsController> logger)
        {
            _queryService = queryService;
            _commandService = commandService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                var events = await _queryService.GetAllEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all events.");
                return StatusCode(500, new { message = "An error occurred while retrieving events." });
            }
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingEvents()
        {
            try
            {
                var upcoming = await _queryService.GetUpcomingEventsAsync();
                return Ok(upcoming);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving upcoming events.");
                return StatusCode(500, new { message = "An error occurred while retrieving upcoming events." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest newEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid event data.",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var createdEvent = await _commandService.CreateEventAsync(newEvent);
                return CreatedAtAction(nameof(GetAllEvents), new { id = createdEvent.Id }, createdEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating event.");
                return StatusCode(500, new { message = "An error occurred while saving the event." });
            }
        }

        /// <summary>
        /// Updates an existing event by ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventRequest updatedEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data.", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            try
            {
                var result = await _commandService.UpdateEventAsync(id, updatedEvent);
                if (result == null)
                {
                    return NotFound(new { message = $"Event with ID {id} not found." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating event with ID {id}.");
                return StatusCode(500, new { message = "An error occurred while updating the event." });
            }
        }

        /// <summary>
        /// Deletes an event by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                var success = await _commandService.DeleteEventAsync(id);
                if (!success)
                {
                    return NotFound(new { message = $"Event with ID {id} not found." });
                }

                return Ok(new { message = $"Event with ID {id} deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting event with ID {id}.");
                return StatusCode(500, new { message = "An error occurred while deleting the event." });
            }
        }
    }
}
