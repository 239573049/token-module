using Microsoft.Extensions.DependencyInjection;
using Token.EventBus.Handlers;
using Token.Module.Dependencys;

namespace Token.EventBus.EventBus;

public class LocalEventBus : ILocalEventBus, ISingletonDependency
{
    private readonly EventTypes _eventTypes;
    private readonly IServiceProvider _serviceProvider;

    public LocalEventBus(EventTypes eventTypes, IServiceProvider serviceProvider)
    {
        _eventTypes = eventTypes;
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<TEvent>(TEvent eventData, bool waitEvents = true) where TEvent : class
    {
        var type = _eventTypes.GetTypes(x => typeof(IEventHandler).IsAssignableFrom(x)).FirstOrDefault();

        if (type != null)
        {
            var handler = _serviceProvider.GetService(type);

            if (handler is ILocalEventHandler<TEvent> eventHandler)
            {
                if (waitEvents)
                {
                    await eventHandler.HandleEventAsync(eventData);
                }
                else
                {
                    _ = eventHandler.HandleEventAsync(eventData);
                }
            }
        }
    }
}