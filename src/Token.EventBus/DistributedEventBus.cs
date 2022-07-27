namespace Token.EventBus;

public class DistributedEventBus : IDistributedEventBus, IDisposable
{
    private readonly Dictionary<string, Action<object>> _dictionary = new();

    public async Task Subscribe<TEvent>(string name, Action<object> action) where TEvent : class
    {
        var data = _dictionary.FirstOrDefault(x => x.Key == name);

        if (string.IsNullOrEmpty(data.Key))
        {
            _dictionary.Add(name,action);
        }
        else
        {
            throw new Exception($"{name} already existed");
        }

        await Task.CompletedTask;
    }

    public async Task Subscribe(string name, Action<object> action)
    {
        var data = _dictionary.FirstOrDefault(x => x.Key == name);

        if (string.IsNullOrEmpty(data.Key))
        {
            _dictionary.Add(name, action);
        }
        else
        {
            throw new Exception($"{name} already existed");
        }

        await Task.CompletedTask;
    }

    public Task PublishAsync<TEvent>(string name, TEvent eventData) where TEvent : class
    {
        var data = _dictionary.FirstOrDefault(x => x.Key == name);

        data.Value.Invoke(eventData);

        return Task.CompletedTask;
    }


    public void Dispose()
    {
        _dictionary.Clear();
    }
}