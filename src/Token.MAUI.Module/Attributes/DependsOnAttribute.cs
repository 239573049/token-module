namespace Token.MAUI.Module.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependsOnAttribute : Attribute
{
    public Type[] Type { get; }

    public DependsOnAttribute(params Type[] type)
    {
        Type = type;
    }
}