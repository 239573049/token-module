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
        
        services.AddEventBus();
        var distributedEventBus = services.GetService<IDistributedEventBus>();
        await distributedEventBus?.Subscribe<string>("张飞", (x) =>
        {
            Console.WriteLine(x);
        })!;

        var data = "dasd";
        distributedEventBus?.PublishAsync<string>("张飞",data);
    }
}