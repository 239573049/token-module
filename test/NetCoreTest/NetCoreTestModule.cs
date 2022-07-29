using Token.EventBus;
using Token.Module;
using Token.Module.Attributes;
using Token.Module.Extensions;

namespace NetCoreTest;

[RunOrder(1)]
public class NetCoreTestModule : TokenModule
{

    public override async Task ConfigureServicesAsync(IServiceCollection services)
    {
        
    }
}