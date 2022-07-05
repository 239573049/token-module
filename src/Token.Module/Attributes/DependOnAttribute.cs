using System;

namespace Token.Module.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependOnAttribute:Attribute 
{
    public Type[] Type { get; }

    public DependOnAttribute(params Type[] type)
    {
        Type = type;
    }
}