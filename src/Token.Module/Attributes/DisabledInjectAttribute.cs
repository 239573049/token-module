using System;

namespace Token.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DisabledInjectAttribute : Attribute
{
    public readonly bool Disabled;

    public DisabledInjectAttribute(bool disabled = true)
    {
        Disabled = disabled;
    }
}