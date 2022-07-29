namespace Token.EventBus;

public interface IKeyLocalEventBus<TEvent>
{
    Task Subscribe(string name, Action<TEvent> action);

    Task PublishAsync(string name, TEvent eventData) ;
}