using System.ComponentModel.DataAnnotations;

namespace rr_events.DTOs
{
    /// <summary>
    /// Represents the payload required to create a new event.
    /// </summary>
    public class CreateEventRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required]
        public DateTime StartTimeUtc { get; set; }

        [Required]
        public DateTime EndTimeUtc { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; } = string.Empty;

        public bool IsPrivate { get; set; } = false;

        [Url(ErrorMessage = "Ticket link must be a valid URL.")]
        public string? TicketLink { get; set; }

        public string? EventImageUrl { get; set; }

    }
}
