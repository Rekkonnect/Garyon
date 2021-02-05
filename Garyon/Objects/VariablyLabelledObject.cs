namespace Garyon.Objects
{
    /// <summary>Represents a object that is labelled with a name that may variably change.</summary>
    /// <typeparam name="T">The type of the object value.</typeparam>
    public class VariablyLabelledObject<T> : LabelledObject<T>
    {
        private string label;

        /// <inheritdoc/>
        public sealed override string Label => label;
        /// <summary>Sets the label of this object to a value.</summary>
        public string VariableLabel { set => label = value; }

        /// <summary>Initializes a new instance of the <seealso cref="LabelledObject{T}"/> class.</summary>
        /// <param name="label">The label of the object.</param>
        /// <param name="value">The value of the object.</param>
        public VariablyLabelledObject(string label, T value = default)
            : base(value)
        {
            VariableLabel = label;
        }
    }
}
