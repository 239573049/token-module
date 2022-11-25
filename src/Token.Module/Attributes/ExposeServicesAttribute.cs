using System;

namespace Token.Attributes;

/// <summary>
/// 指定注入实现
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ExposeServicesAttribute : Attribute
{
    public readonly Type? Type;

    public ExposeServicesAttribute(Type? type)
    {
        Type = type;
    }
}