using Token.EventBus;
using Token.EventBus.EventBus;
using Token.Module;
using Token.Module.Attributes;

namespace NetCore.Event;

[DependOn(typeof(TokenEventBusModule))]
public class NetCoreEventModule : TokenModule
{
    public override async Task OnApplicationShutdownAsync(IApplicationBuilder app)
    {

        
        var push = app.ApplicationServices.GetRequiredService<ILocalEventBus>();
        
        await push.PublishAsync(new Test()
        {
            Name = "asd"
        });
        
        
    }
}