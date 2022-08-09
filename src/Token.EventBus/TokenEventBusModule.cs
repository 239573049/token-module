using Microsoft.Extensions.DependencyInjection;
using Token.EventBus.Extensions;
using Token.Module;

namespace Token.EventBus;

public class TokenEventBusModule : TokenModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTokenEventBus();
    }

}