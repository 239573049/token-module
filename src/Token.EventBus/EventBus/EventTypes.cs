namespace Token.EventBus.EventBus;

public class EventTypes
{
    private readonly List<Type> EventHandlerType = new();

    public void AddEventHandlerRange(IEnumerable<Type> types)
    {
        EventHandlerType?.AddRange(types);
    }

    public IEnumerable<Type> GetTypes()
    {
        return EventHandlerType;
    }

    public List<Type> GetTypes(Func<Type, bool> expression)
    {
        return EventHandlerType.Where(expression).ToList();
    }
}