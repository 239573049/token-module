using Microsoft.Extensions.DependencyInjection;
using Token.EventBus.EventBus;
using Token.EventBus.Handlers;
using Token.EventBus.KeyEventBus;
using Token.Module;

namespace Token.EventBus.Extensions;

public static class TokenEventBusExtension
{
    public static void AddTokenEventBus(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IKeyLocalEventBus<>), typeof(KeyLocalEventBus<>));
        
        var service = services.BuildServiceProvider().GetService<List<Tuple<ITokenModule, int>>>();

        var eventTypes = new EventTypes();
        
        var types = service?.Where(x => x.Item1.GetType() != typeof(TokenEventBusModule))
            .Select(x => x.Item1.GetType().Assembly).Distinct()
            .SelectMany(x => x.GetTypes()).Where(x => typeof(IEventHandler).IsAssignableFrom(x)).ToList();

        eventTypes.AddEventHandlerRange(types);

        services.AddSingleton(eventTypes);
        
    }
}