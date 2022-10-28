using System;

namespace Token.Module.Attributes;

/// <summary>
/// 模块依赖
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependOnAttribute : Attribute
{
    public Type[] Type { get; }

    public DependOnAttribute(params Type[] type)
    {
        Type = type;
    }
}