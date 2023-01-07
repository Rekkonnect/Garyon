namespace Garyon.Objects.FunctionPointers;

// TODO: Autogenerate those
public unsafe readonly record struct FunctionPointerAction(delegate*<void> Pointer)
{
    public void Invoke() => Pointer();
}
public unsafe readonly record struct FunctionPointerAction<T1>(delegate*<T1, void> Pointer)
{
    public void Invoke(T1 arg1) => Pointer(arg1);
}

public unsafe readonly record struct FunctionPointer<TReturn>(delegate*<TReturn> Pointer)
{
    public TReturn Invoke() => Pointer();
}
public unsafe readonly record struct FunctionPointer<T1, TReturn>(delegate*<T1, TReturn> Pointer)
{
    public TReturn Invoke(T1 arg1) => Pointer(arg1);
}

public unsafe readonly record struct UnmanagedFunctionPointerAction(delegate* unmanaged<void> Pointer)
{
    public void Invoke() => Pointer();
}
