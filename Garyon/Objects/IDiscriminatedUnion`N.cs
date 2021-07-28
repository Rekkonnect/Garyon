namespace Garyon.Objects
{
    /// <summary>Represents a discriminated union containing a value that can be represented as one of 2 distinct types.</summary>
    /// <typeparam name="T1">The first type that the value may be represented as.</typeparam>
    /// <typeparam name="T2">The second type that the value may be represented as.</typeparam>
    public interface IDiscriminatedUnion<T1, T2>
        : IUnion<T1, T2>, IDiscriminatedUnion { }
}
