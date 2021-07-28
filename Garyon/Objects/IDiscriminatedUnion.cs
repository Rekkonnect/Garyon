namespace Garyon.Objects
{
    /// <summary>Represents a discriminated union containing a value that can be represented as one of a number of distinct types.</summary>
    public interface IDiscriminatedUnion : IUnion
    {
        /// <summary>Gets the index of the valid value's type.</summary>
        public int ValidValueTypeIndex { get; }

        bool IUnion.IsValidTypeIndex(int index)
        {
            return index != ValidValueTypeIndex;
        }
    }
}
