using System;
using rr_events.Services.Interfaces;

namespace rr_events.Models
{
    /// <summary>
    /// Represents a general event, either online or in-person.
    /// </summary>
    public class Event : IDescribable
    {
        /// <summary>
        /// Unique identifier for the event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the event.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Optional description of the event.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Start time of the event (UTC).
        /// </summary>
        public DateTime StartTimeUtc { get; set; }

        /// <summary>
        /// End time of the event (UTC).
        /// </summary>
        public DateTime EndTimeUtc { get; set; }

        /// <summary>
        /// Physical or virtual location name.
        /// </summary>
        public required string Location { get; set; }

        /// <summary>
        /// Whether the event is private or publicly visible.
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Optional link to purchase or register for the event.
        /// </summary>
        public string? TicketLink { get; set; }

        /// <summary>
        /// Returns a human-readable summary of the event.
        /// </summary>
        public virtual string Describe()
        {
            return $"{Title} @ {Location} ({StartTimeUtc:yyyy-MM-dd HH:mm} - {EndTimeUtc:HH:mm} UTC)";
        }
    }
}
