namespace rr_events.DTOs
{
    /// <summary>
    /// DTO representing event data returned to clients.
    /// </summary>
    public class EventResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime StartTimeUtc { get; set; }

        public DateTime EndTimeUtc { get; set; }

        public string Location { get; set; } = string.Empty;

        public bool IsPrivate { get; set; }

        public string? TicketLink { get; set; }
    }
}
