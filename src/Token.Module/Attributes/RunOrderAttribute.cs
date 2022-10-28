using System;

namespace Token.Module.Attributes;

/// <summary>
/// 标记模块执行顺序 小=》大
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RunOrderAttribute : Attribute
{
    public readonly int Order;

    public RunOrderAttribute(int order)
    {
        Order = order;
    }
}