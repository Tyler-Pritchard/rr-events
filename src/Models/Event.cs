using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using rr_events.Models.Interfaces;

namespace rr_events.Models
{
    /// <summary>
    /// Represents a Rob Rich event, either upcoming or past.
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
        /// City and country or general location string.
        /// </summary>
        public required string Location { get; set; }

        /// <summary>
        /// Name of the venue where the event is held.
        /// </summary>
        public string? Venue { get; set; }

        /// <summary>
        /// Name of the tour this event is part of.
        /// </summary>
        public string? TourName { get; set; }

        /// <summary>
        /// Whether all tickets are sold out.
        /// </summary>
        public bool TicketsSoldOut { get; set; }

        /// <summary>
        /// Link to purchase or register for tickets.
        /// </summary>
        public string? TicketLink { get; set; }

        /// <summary>
        /// Whether enhanced experience packages are sold out.
        /// </summary>
        public bool EnhancedExperienceSoldOut { get; set; }

        /// <summary>
        /// Optional link to the enhanced experience page.
        /// </summary>
        public string? EnhancedExperienceLink { get; set; }

        /// <summary>
        /// Optional list of supporting acts (e.g., openers).
        /// </summary>
        [NotMapped]
        public List<string>? SupportingActs
        {
            get => string.IsNullOrEmpty(SupportingActsSerialized)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(SupportingActsSerialized);
            set => SupportingActsSerialized = JsonSerializer.Serialize(value);
        }

        /// <summary>
        /// Serialized version of SupportingActs for EF Core.
        /// </summary>
        public string? SupportingActsSerialized { get; set; }

        /// <summary>
        /// Optional presale details, such as dates or access codes.
        /// </summary>
        public PresaleDetails? FanClubPresale { get; set; }

        /// <summary>
        /// Optional image URL for the event.
        /// </summary>
        public string? EventImageUrl { get; set; }

        /// <summary>
        /// Whether the event is private or publicly visible.
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Track event as upcoming event.
        /// </summary>
        [NotMapped]
        public bool IsUpcoming => EndTimeUtc > DateTime.UtcNow;

        /// <summary>
        /// Track event as past event.
        /// </summary>
        [NotMapped]
        public bool IsPast => EndTimeUtc <= DateTime.UtcNow;


        /// <summary>
        /// Returns a human-readable summary of the event.
        /// </summary>
        public virtual string Describe()
        {
            return $"{Title} @ {Location} ({StartTimeUtc:yyyy-MM-dd HH:mm} - {EndTimeUtc:HH:mm} UTC)";
        }
    }
}
