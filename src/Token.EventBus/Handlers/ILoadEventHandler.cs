namespace Token.Handlers;

public interface ILoadEventHandler<in TEvent> where TEvent : class
{
    Task HandleEventAsync(TEvent eventData);
}