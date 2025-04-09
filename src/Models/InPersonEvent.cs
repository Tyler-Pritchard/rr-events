namespace rr_events.Models
{
    /// <summary>
    /// Represents an event held at a physical location.
    /// </summary>
    public class InPersonEvent : Event
    {
        /// <summary>
        /// Optional room number or specific sub-location.
        /// </summary>
        public string? RoomNumber { get; set; }

        /// <summary>
        /// Returns a summary with location-specific context.
        /// </summary>
        public override string Describe()
        {
            var roomInfo = string.IsNullOrEmpty(RoomNumber) ? "" : $" Room: {RoomNumber}.";
            return $"{Title} at {Location} from {StartTimeUtc} to {EndTimeUtc}.{roomInfo}";
        }
    }
}
