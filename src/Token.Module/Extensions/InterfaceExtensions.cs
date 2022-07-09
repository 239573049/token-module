using System;

namespace Token.Module.Extensions;

public static class InterfaceExtensions
{
    public static bool IsAssignableFrom<T>(this Type t) =>
        typeof(T).IsAssignableFrom(t);
}