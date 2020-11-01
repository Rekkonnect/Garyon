namespace Garyon.Objects
{
    /// <summary>Represents a time object that has a second component.</summary>
    public interface IHasSecond : ITimeObject
    {
        /// <summary>Gets or sets the second component of the time object.</summary>
        int Second { get; set; }
    }
}
