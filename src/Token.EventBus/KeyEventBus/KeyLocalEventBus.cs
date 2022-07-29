namespace Token.EventBus.KeyEventBus;

public class KeyLocalEventBus<TEvent> : IKeyLocalEventBus<TEvent>
{
    private readonly Dictionary<string, Action<TEvent>> _dictionary = new();

    public async Task Subscribe(string name, Action<TEvent> action)
    {
        var data = _dictionary.FirstOrDefault(x => x.Key == name);

        if (string.IsNullOrEmpty(data.Key))
        {
            _dictionary.Add(name, action);
        }
        else
        {
            _dictionary.Remove(name);
            _dictionary.Add(name, action);
        }

        await Task.CompletedTask;
    }

    public Task PublishAsync(string name, TEvent eventData)
    {
        var data = _dictionary.FirstOrDefault(x => x.Key == name);

        data.Value?.Invoke(eventData);

        return Task.CompletedTask;
    }
}