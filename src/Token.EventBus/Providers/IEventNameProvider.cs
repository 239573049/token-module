namespace Token.EventBus.Providers;

public interface IEventNameProvider
{
    string GetName(Type eventType);
}