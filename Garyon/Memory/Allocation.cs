using Garyon.Functions;
using System;
using System.Runtime.InteropServices;

namespace Garyon.Memory;

/// <summary>Provides functions for memory allocation on the unmanaged heap.</summary>
public static unsafe class Allocation
{
    /// <summary>Allocates a block of bytes from the unmanaged heap.</summary>
    /// <param name="bytes">The number of bytes to allocate.</param>
    /// <returns>A <see langword="void"/>* pointing to the start of the allocated block.</returns>
    public static void* Allocate(int bytes) => (void*)Marshal.AllocHGlobal(bytes);
    /// <summary>Allocates a block of elements of a specified type <typeparamref name="T"/> from the unmanaged heap.</summary>
    /// <typeparam name="T">The type of elements the allocated block will store.</typeparam>
    /// <param name="elements">The number of elements the allocated block will store.</param>
    /// <returns>A <typeparamref name="T"/>* pointing to the start of the allocated block. The block's size is equal to the number of elements multiplied by <see langword="sizeof"/>(<typeparamref name="T"/>).</returns>
    public static T* Allocate<T>(int elements)
        where T : unmanaged
    {
        return (T*)Marshal.AllocHGlobal(elements * sizeof(T));
    }

    /// <summary>Allocates a block of bytes from the unmanaged heap, and zeroes it.</summary>
    /// <param name="bytes">The number of bytes to allocate.</param>
    /// <returns>A <see langword="void"/>* pointing to the start of the allocated block.</returns>
    public static void* AllocateClear(int bytes)
    {
        void* pointer = Allocate(bytes);
        PointerFunctions.Clear(pointer, bytes);
        return pointer;
    }
    /// <summary>Allocates a block of elements of a specified type <typeparamref name="T"/> from the unmanaged heap, and zeroes it.</summary>
    /// <typeparam name="T">The type of elements the allocated block will store.</typeparam>
    /// <param name="elements">The number of elements the allocated block will store.</param>
    /// <returns>A <typeparamref name="T"/>* pointing to the start of the allocated block. The block's size is equal to the number of elements multiplied by <see langword="sizeof"/>(<typeparamref name="T"/>).</returns>
    public static T* AllocateClear<T>(int elements)
        where T : unmanaged
    {
        return (T*)AllocateClear(elements * sizeof(T));
    }

    /// <summary>Reallocates a block of bytes from the unmanaged heap.</summary>
    /// <param name="pointer">The pointer to the original block that will be reallocated.</param>
    /// <param name="bytes">The number of bytes of the newly allocated block.</param>
    /// <returns>A <see langword="void"/>* pointing to the start of the reallocated block.</returns>
    public static void* Reallocate(void* pointer, int bytes)
    {
        return (void*)Marshal.ReAllocHGlobal((IntPtr)pointer, (IntPtr)bytes);
    }
    /// <summary>Reallocates a block of elements from the unmanaged heap.</summary>
    /// <param name="pointer">The pointer to the original block that will be reallocated.</param>
    /// <param name="elements">The number of elements of the newly allocated block.</param>
    /// <returns>A <typeparamref name="T"/>* pointing to the start of the reallocated block. The block's size is equal to the number of elements multiplied by <see langword="sizeof"/>(<typeparamref name="T"/>).</returns>
    public static T* Reallocate<T>(T* pointer, int elements)
        where T : unmanaged
    {
        return (T*)Reallocate((void*)pointer, elements * sizeof(T));
    }

    /// <summary>Frees a previously allocated block of bytes from the unmanaged heap.</summary>
    /// <param name="pointer">The <see langword="void"/>* pointing at the block that was allocated.</param>
    public static void Free(void* pointer) => Marshal.FreeHGlobal((IntPtr)pointer);
}
