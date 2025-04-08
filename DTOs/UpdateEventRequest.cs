using System.ComponentModel.DataAnnotations;

namespace rr_events.DTOs
{
    /// <summary>
    /// Represents the data required to update an existing event.
    /// </summary>
    public class UpdateEventRequest
    {
        [Required]
        public int Id { get; set; } // ID of the event to update

        [Required]
        [StringLength(100, ErrorMessage = "Title length can't exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartTimeUtc { get; set; }

        [Required]
        public DateTime EndTimeUtc { get; set; }

        [Required]
        public string Location { get; set; }

        public bool IsPrivate { get; set; }

        [Url]
        public string? TicketLink { get; set; }
    }
}
