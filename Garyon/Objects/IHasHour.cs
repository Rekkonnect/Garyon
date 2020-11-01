namespace Garyon.Objects
{
    /// <summary>Represents a time object that has an hour component.</summary>
    public interface IHasHour : ITimeObject
    {
        /// <summary>Gets or sets the hour component of the time object.</summary>
        int Hour { get; set; }
    }
}
