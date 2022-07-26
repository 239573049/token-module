using Token.EventBus;
using Token.Module;
using Token.Module.Attributes;
using Token.Module.Extensions;

namespace NetCoreTest;

[RunOrder(1)]
public class NetCoreTestModule :TokenModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddEventBus();
        
        var distributedEventBus = services.GetService<IDistributedEventBus>();
        distributedEventBus?.Subscribe((string data) =>
        {
            Console.WriteLine(data);
        });

        string data = "测试";
        distributedEventBus?.PublishAsync(data);
    }

}