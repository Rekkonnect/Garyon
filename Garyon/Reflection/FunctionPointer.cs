namespace Garyon.Objects.FunctionPointers;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
// TODO: Autogenerate those
public unsafe readonly struct FunctionPointerAction
{
    public delegate*<void> Pointer { get; init; }

    public FunctionPointerAction(delegate*<void> pointer)
    {
        Pointer = pointer;
    }

    public void Invoke() => Pointer();
}
public unsafe readonly struct FunctionPointerAction<T1>
{
    public delegate*<T1, void> Pointer { get; init; }

    public FunctionPointerAction(delegate*<T1, void> pointer)
    {
        Pointer = pointer;
    }

    public void Invoke(T1 arg1) => Pointer(arg1);
}

public unsafe readonly struct FunctionPointer<TReturn>
{
    public delegate*<TReturn> Pointer { get; init; }

    public FunctionPointer(delegate*<TReturn> pointer)
    {
        Pointer = pointer;
    }

    public TReturn Invoke() => Pointer();
}
public unsafe readonly struct FunctionPointer<T1, TReturn>
{
    public delegate*<T1, TReturn> Pointer { get; init; }

    public FunctionPointer(delegate*<T1, TReturn> pointer)
    {
        Pointer = pointer;
    }

    public TReturn Invoke(T1 arg1) => Pointer(arg1);
}

#if SUPPORTS_UNMANAGED_FUNPTRS
public unsafe readonly struct UnmanagedFunctionPointerAction
{
    public delegate* unmanaged<void> Pointer { get; init; }

    public UnmanagedFunctionPointerAction(delegate* unmanaged<void> pointer)
    {
        Pointer = pointer;
    }

    public void Invoke() => Pointer();
}
#endif
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
