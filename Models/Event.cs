using System;

namespace rr_events.Models
{
    public class Event
    {
        public int Id { get; set; }                     // Primary key
        public string Title { get; set; }               // Event title (required)
        public string? Description { get; set; }        // Optional description
        public DateTime StartTimeUtc { get; set; }      // When the event starts
        public DateTime EndTimeUtc { get; set; }        // When it ends
        public string Location { get; set; }            // Location (city, venue, etc.)
        public bool IsPrivate { get; set; }             // True if this is a private event
        public string? TicketLink { get; set; }         // Optional link to buy tickets
    }
}
