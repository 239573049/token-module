namespace Token.EventBus;

public interface IDistributedEventBus
{
    Task Subscribe<TEvent>(string name, Action<object> action) where TEvent : class;

    Task Subscribe(string name,Action<object> action);

    Task PublishAsync<TEvent>(string name, TEvent eventData) where TEvent : class;
}