namespace rr_events.Models
{
    /// <summary>
    /// Interface for any entity that can provide a human-readable summary.
    /// </summary>
    public interface IDescribable
    {
        /// <summary>
        /// Returns a string describing the object.
        /// </summary>
        string Describe();
    }
}
