namespace Garyon.Objects
{
    /// <summary>Represents a function that performs an action on an enumerated element, also provided its index in the enumeration order.</summary>
    /// <typeparam name="T">The type of the element.</typeparam>
    /// <param name="index">The index of the element, provided the enumeration order.</param>
    /// <param name="element">The element.</param>
    public delegate void IndexedEnumeratedElementAction<T>(int index, T element);
}
