using System.Collections.Concurrent;
using System.Reflection;
using System.Xml;

namespace Token.Events;

public class KeyLoadEventBus : IKeyLoadEventBus
{
    private readonly ConcurrentDictionary<string, List<Action<object>>> _concurrentDictionary;

    public KeyLoadEventBus()
    {
        _concurrentDictionary = new ConcurrentDictionary<string, List<Action<object>>>();
    }

    /// <inheritdoc />
    public async Task PushAsync(string name, object entity)
    {
        _concurrentDictionary.TryGetValue(name, out var value);

        value?.ForEach(x =>
        {
            x.Invoke(entity);
        });

        await Task.CompletedTask;
    }

    /// <summary>
    /// 订阅事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="func"></param>
    public void Subscription(string name, Action<object> func)
    {
        if (_concurrentDictionary.Any(x => x.Key == name ))
        {
            _concurrentDictionary.TryGetValue(name, out var list);
            list?.Add(func);
        }
        else
        {
            _concurrentDictionary.TryAdd(name, new List<Action<object>>()
            {
                func
            });
        }
    }

    /// <inheritdoc />
    public void Remove(string name)
    {
        _concurrentDictionary.Remove(name,out _);
    }
}