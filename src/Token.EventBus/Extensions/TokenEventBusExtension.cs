using Microsoft.Extensions.DependencyInjection;
using Token.Events;
using Token.Manager;

namespace Token.Extensions;

public static class TokenEventBusExtension
{
    public static void AddEventBus(this IServiceCollection services)
    {
        services.AddSingleton(typeof(EventManager<>));

        services.AddSingleton(typeof(ILoadEventBus<>), typeof(LoadEventBus<>));

        services.AddSingleton<IKeyLoadEventBus, KeyLoadEventBus>();
    }
}