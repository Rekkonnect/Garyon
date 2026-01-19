using System;
using System.Collections;
using System.Collections.Generic;

namespace Garyon.Objects.Enumerators;

/// <summary>
/// Represents an immutable collection of enumerables, which is flattened,
/// through enumeration, into a single <seealso cref="IEnumerable{T}"/>.
/// </summary>
/// <typeparam name="T">
/// The type of the elements that are held
/// </typeparam>
public class FlattenedEnumerables3D<T>(IEnumerable<IEnumerable<IEnumerable<T>>> enumerables)
    : IEnumerable<T>
{
    private readonly IEnumerable<IEnumerable<IEnumerable<T>>> _enumerables = enumerables;

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator() => new Enumerator(this);
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Only intended for benchmarking purposes.
    /// </summary>
    /// <returns>
    /// The backed enumerator for the 3D enumeration process.
    /// </returns>
    public IEnumerator<T> GetBackedEnumerator() => new BackedEnumerator(this);

    /// <summary>
    /// Represents an enumerator for the
    /// <seealso cref="FlattenedEnumerables3D{T}"/> class.
    /// </summary>
    private class Enumerator : IEnumerator<T>
    {
        private readonly IEnumerator<IEnumerable<IEnumerable<T>>> _outerEnumerator0;
        private IEnumerator<IEnumerable<T>>? _outerEnumerator1;
        private IEnumerator<T>? _innerEnumerator;

        private T? _current;

        /// <summary>
        /// Gets the currently enumerated element in the flattened enumerable.
        /// </summary>
        public T Current => _current!;
        object? IEnumerator.Current => Current;

        /// <summary>
        /// Initializes a new instance of the <seealso cref="BackedEnumerator"/>
        /// class out of a <seealso cref="FlattenedEnumerables3D{T}"/> instance.
        /// </summary>
        /// <param name="instance">
        /// The <seealso cref="FlattenedEnumerables3D{T}"/> whose enumerables
        /// will be flattened.
        /// </param>
        public Enumerator(FlattenedEnumerables3D<T> instance)
        {
            _outerEnumerator0 = instance._enumerables.GetEnumerator();
        }

        /// <summary>
        /// Advances to the next element in the flattened enumerable.
        /// Enumerables that contain no elements are automatically skipped.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the enumerator advanced to the next
        /// element, otherwise <see langword="false"/> if there were no
        /// remaining elements to enumerate.
        /// </returns>
        public bool MoveNext()
        {
            while (_innerEnumerator?.MoveNext() != true)
            {
                while (_outerEnumerator1?.MoveNext() != true)
                {
                    if (!_outerEnumerator0.MoveNext())
                        return false;

                    _outerEnumerator1 = _outerEnumerator0.Current.GetEnumerator();
                }

                _innerEnumerator = _outerEnumerator1.Current.GetEnumerator();
            }

            _current = _innerEnumerator.Current;
            return true;
        }
        /// <inheritdoc/>
        public void Reset()
        {
            _outerEnumerator0.Reset();
            _outerEnumerator1 = null;
            _innerEnumerator = null;
        }

        /// <summary>
        /// Disposes the currently available enumerators backing the enumeration
        /// process.
        /// </summary>
        /// <remarks>
        /// The enumerators that are wrapped should not be reused outside the
        /// flattening context in any case, otherwise problems might occur. The
        /// enumerators that are generated for this purpose should be
        /// exclusively available to the flattener, and nowhere else.
        /// </remarks>
        public void Dispose()
        {
            _outerEnumerator0.Dispose();
            _outerEnumerator1?.Dispose();
            _innerEnumerator?.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    private class BackedEnumerator : IEnumerator<T>
    {
        private readonly IEnumerator<IEnumerable<IEnumerable<T>>> _outerEnumerator;
        private FlattenedEnumerables2D<T>.Enumerator? _innerFlattenedEnumerator;

        private T? _current;

        /// <summary>
        /// Gets the currently enumerated element in the flattened enumerable.
        /// </summary>
        public T Current => _current!;
        object? IEnumerator.Current => Current;

        /// <summary>
        /// Initializes a new instance of the <seealso cref="BackedEnumerator"/>
        /// class out of a <seealso cref="FlattenedEnumerables3D{T}"/> instance.
        /// </summary>
        /// <param name="instance">
        /// The <seealso cref="FlattenedEnumerables3D{T}"/> whose enumerables
        /// will be flattened.
        /// </param>
        public BackedEnumerator(FlattenedEnumerables3D<T> instance)
        {
            _outerEnumerator = instance._enumerables.GetEnumerator();
        }

        /// <summary>
        /// Advances to the next element in the flattened enumerable.
        /// Enumerables that contain no elements are automatically skipped.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the enumerator advanced to the next
        /// element, otherwise <see langword="false"/> if there were no
        /// remaining elements to enumerate.
        /// </returns>
        public bool MoveNext()
        {
            while (_innerFlattenedEnumerator?.MoveNext() != true)
            {
                if (!_outerEnumerator.MoveNext())
                    return false;

                _innerFlattenedEnumerator = new(_outerEnumerator.Current.GetEnumerator());
            }

            _current = _innerFlattenedEnumerator.Current;
            return true;
        }
        /// <inheritdoc/>
        public void Reset()
        {
            _outerEnumerator.Reset();
            _innerFlattenedEnumerator = null;
        }

        /// <summary>
        /// Disposes the currently available enumerators backing the enumeration
        /// process.
        /// </summary>
        /// <remarks>
        /// The enumerators that are wrapped should not be reused outside the
        /// flattening context in any case, otherwise problems might occur. The
        /// enumerators that are generated for this purpose should be
        /// exclusively available to the flattener, and nowhere else.
        /// </remarks>
        public void Dispose()
        {
            _outerEnumerator.Dispose();
            _innerFlattenedEnumerator?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
