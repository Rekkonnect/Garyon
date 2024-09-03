using System;
using System.Runtime.CompilerServices;

namespace Garyon.Functions.Arrays;

/// <summary>Provides functions useful for array identification, specifically regarding the element type.</summary>
public static class ArrayIdentification
{
    // TODO: Convert some of those to extensions
    /// <summary>Determines whether an array type contains elements of the provided type. It only checks for multidimensional arrays (of the form [(,)*]). Jagged arrays are not taken into consideration in this implementation.</summary>
    /// <typeparam name="TArray">The type of the array.</typeparam>
    /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
    /// <returns>Whether the given array type stores elements of the <typeparamref name="TElement"/> type.</returns>
    public static bool IsArrayOfType<TArray, TElement>()
    {
        return IsArrayOfType<TElement>(typeof(TArray));
    }
    /// <summary>Determines whether an array type contains elements of the provided type. It only checks for multidimensional arrays (of the form [(,)*]). Jagged arrays are not taken into consideration in this implementation.</summary>
    /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
    /// <param name="arrayType">The type of the array.</param>
    /// <returns>Whether the given array type stores elements of the <typeparamref name="TElement"/> type.</returns>
    public static bool IsArrayOfType<TElement>(Type arrayType)
    {
        if (!arrayType.IsArray)
            return false;

        return arrayType.GetElementType() == typeof(TElement);
    }
    /// <summary>Determines whether an array type contains elements of the provided type. It also checks for jagged arrays up to a maximum jagging level.</summary>
    /// <typeparam name="TArray">The type of the array.</typeparam>
    /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
    /// <param name="maxJaggingLevel">The maximum jagging level of the array (1 means up to [], 2 means up to [][], etc.).</param>
    /// <returns>Whether the given array type stores elements of the <typeparamref name="TElement"/> type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsArrayOfType<TArray, TElement>(int maxJaggingLevel)
    {
        return IsArrayOfType<TElement>(typeof(TArray), maxJaggingLevel);
    }
    /// <summary>Determines whether an array type contains elements of the provided type. It also checks for jagged arrays up to a maximum jagging level.</summary>
    /// <typeparam name="TElement">The type of the element the array stores.</typeparam>
    /// <param name="arrayType">The type of the array.</param>
    /// <param name="maxJaggingLevel">The maximum jagging level of the array (1 means up to [], 2 means up to [][], etc.).</param>
    /// <returns>Whether the given array type stores elements of the <typeparamref name="TElement"/> type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsArrayOfType<TElement>(Type arrayType, int maxJaggingLevel)
    {
        var currentType = arrayType;
        for (int i = 0; i < maxJaggingLevel; i++)
        {
            if (!currentType.IsArray)
                return false;

            currentType = currentType.GetElementType();
            if (currentType == typeof(TElement))
                return true;
        }

        return false;
    }
}
