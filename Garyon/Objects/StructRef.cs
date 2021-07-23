namespace Garyon.Objects
{
    /// <summary>Provides the ability to store a reference to an unmanaged struct without using pointers.</summary>
    /// <typeparam name="T">The type of the unmanaged struct whose reference to store.</typeparam>
    public unsafe class StructRef<T>
        where T : unmanaged
    {
        private T* reference;

        /// <summary>Initializes a new instance of the <see cref="StructRef{T}"/> class out of a reference to a struct instance.</summary>
        /// <param name="value">The struct instance whose reference to store initially.</param>
        public StructRef(ref T value)
        {
            SetReference(ref value);
        }

        /// <summary>Sets the struct reference to a reference of another struct instance.</summary>
        /// <param name="value">The struct instance whose reference to store.</param>
        /// <remarks>
        /// WARNING: The reference to the struct instance is not dynamically updated. If the GC moves the reference, a risk for memory corruption is induced.<br/>
        /// One way to move the reference is if the unmanaged struct instance is a class instance field, which class instance is moved by the GC.
        /// </remarks>
        public void SetReference(ref T value)
        {
            fixed (T* r = &value)
                reference = r;
        }
        /// <summary>Gets the stored struct instance reference.</summary>
        /// <returns>The stored reference to the struct instance.</returns>
        /// <remarks>It is recommended to refer to the remarks of <seealso cref="SetReference(ref T)"/>.</remarks>
        public ref T GetReference() => ref *reference;
    }

    /// <summary>Provides extensions related to <seealso cref="StructRef{T}"/>.</summary>
    public static class StructRefHelperExtensions
    {
        /// <summary>Creates a new instance of the <seealso cref="StructRef{T}"/> class out of the desired struct instance reference.</summary>
        /// <typeparam name="T">The type of the struct instance whose reference is obtained.</typeparam>
        /// <param name="instance">The reference to the struct instance.</param>
        /// <returns>A <seealso cref="StructRef{T}"/> instance holding a reference to the passed struct instance reference.</returns>
        public static StructRef<T> CreateReference<T>(this ref T instance)
            where T : unmanaged
        {
            return new StructRef<T>(ref instance);
        }
    }
}
