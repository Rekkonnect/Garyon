namespace Garyon.Objects
{
    /// <summary>Represents a time object that has a minute component.</summary>
    public interface IHasMinute : ITimeObject
    {
        /// <summary>Gets or sets the minute component of the time object.</summary>
        int Minute { get; set; }
    }
}
