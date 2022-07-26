namespace Token.EventBus;

public class DistributedEventBus : IDistributedEventBus, IDisposable
{
    private readonly List<object> _distributed = new();
    public IDisposable Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class
    {
        _distributed.Add(handler);
        return Task.CompletedTask;
    }

    public IDisposable Subscribe(Action<object> handler)
    {
        _distributed.Add(handler);
        return Task.CompletedTask;
    }

    public Task PublishAsync<TEvent>(TEvent eventData) where TEvent : class
    {
        var data = _distributed.FirstOrDefault(x => x.GetType() == typeof(Action<TEvent>));

        var action = data as Action<TEvent>;

        action?.Invoke(eventData);

        return Task.CompletedTask;
    }


    public void Dispose()
    {
        _distributed.Clear();
    }
}