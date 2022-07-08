using System;

namespace Token.Module.Attributes;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RunOrderAttribute :Attribute
{
    public int Order;

    public RunOrderAttribute(int order)
    {
        Order = order;
    }
}