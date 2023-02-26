using System;

namespace Garyon.Reflection;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class CodeAttribute : Attribute
{
    public string Code { get; }

    public CodeAttribute(string code)
    {
        Code = code;
    }
}
