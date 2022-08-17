using System;

namespace Token.Module.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ExposeServicesAttribute : Attribute
{
    public readonly Type? Type;

    public ExposeServicesAttribute(Type? type)
    {
        Type = type;
    }
}