using Microsoft.Extensions.DependencyInjection;

namespace Token.EventBus;

public static class TokenEventBusExtension
{
    public static void AddEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IDistributedEventBus, DistributedEventBus>();
    }
}