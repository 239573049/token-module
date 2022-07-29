using Microsoft.Extensions.DependencyInjection;
using Token.EventBus.EventBus;
using Token.EventBus.Handlers;
using Token.EventBus.KeyEventBus;
using Token.Module;

namespace Token.EventBus;

public class TokenEventBusModule : TokenModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(typeof(IKeyLocalEventBus<>), typeof(KeyLocalEventBus<>));
        
        ConfigureEventBus(services);
    }

    private void ConfigureEventBus(IServiceCollection services)
    {
        var service = services.BuildServiceProvider().GetService<List<Tuple<ITokenModule, int>>>();

        var eventTypes = new EventTypes();
        
        var types = service?.Where(x => x.Item1.GetType() != typeof(TokenEventBusModule))
            .Select(x => x.Item1.GetType().Assembly).Distinct()
            .SelectMany(x => x.GetTypes()).Where(x => typeof(IEventHandler).IsAssignableFrom(x)).ToList();

        eventTypes.AddEventHandlerRange(types);

        services.AddSingleton(eventTypes);
    }

}