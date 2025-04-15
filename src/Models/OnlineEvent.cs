namespace rr_events.Models
{
    /// <summary>
    /// Represents an event held on an online platform such as Zoom or Teams.
    /// </summary>
    public class OnlineEvent : Event
    {
        /// <summary>
        /// The name of the online platform (e.g., Zoom, YouTube).
        /// </summary>
        public required string Platform { get; set; }

        /// <summary>
        /// Optional direct link to join the event.
        /// </summary>
        public string? MeetingLink { get; set; }

        /// <summary>
        /// Returns a summary with online-specific context.
        /// </summary>
        public override string Describe()
        {
            return $"{Title} (Online via {Platform}) from {StartTimeUtc} to {EndTimeUtc}";
        }
    }
}
