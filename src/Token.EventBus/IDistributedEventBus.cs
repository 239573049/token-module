namespace Token.EventBus;

public interface IDistributedEventBus
{
    IDisposable Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class;

    IDisposable Subscribe(Action<object> handler);
    
    Task PublishAsync<TEvent>(TEvent eventData) where TEvent : class;
}