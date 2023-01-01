namespace Garyon.Objects;

/// <summary>Represents a object that is labelled with a name.</summary>
/// <typeparam name="T">The type of the object value.</typeparam>
public abstract class LabelledObject<T>
{
    /// <summary>The object value.</summary>
    public T ObjectValue { get; set; }
    /// <summary>The label of the object.</summary>
    public abstract string Label { get; }

    /// <summary>Initializes a new instance of the <seealso cref="LabelledObject{T}"/> class.</summary>
    /// <param name="value">The value of the object.</param>
    protected LabelledObject(T value = default) => ObjectValue = value;

    /// <summary>Returns the string representation of this labelled object including its label.</summary>
    /// <returns>The string representation of the labelled object in the form <c>$"{Label}: {ObjectValue}"</c>.</returns>
    public sealed override string ToString() => $"{Label}: {ObjectValue}";
}
