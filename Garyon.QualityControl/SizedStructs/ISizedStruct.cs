using System;

namespace Garyon.QualityControl.SizedStructs
{
    /// <summary>Represents a sized struct.</summary>
    public interface ISizedStruct { }

    /// <summary>Represents a sized struct.</summary>
    /// <typeparam name="T">The type of the struct, provided to <seealso cref="IEquatable{T}"/> for the Equals function.</typeparam>
    public interface ISizedStruct<T> : ISizedStruct, IEquatable<T> { }
}
