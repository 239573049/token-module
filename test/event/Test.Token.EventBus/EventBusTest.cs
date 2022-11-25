using Microsoft.Extensions.DependencyInjection;
using Token.Events;
using Token.Extensions;
using Token.Handlers;

namespace Test.Token.EventBus;

public class EventBusTest
{
    [Fact]
    public async Task EventBus()
    {
        var services = new ServiceCollection();
        services.AddEventBus();
        services.AddTransient(typeof(ILoadEventHandler<string>),typeof(DemoLoadEventHandler));

        var serviceProvider = services.BuildServiceProvider();

        var loadEventBus = serviceProvider.GetRequiredService<ILoadEventBus<string>>();
        var keyEventBus = serviceProvider.GetRequiredService<IKeyLoadEventBus>();

        keyEventBus.Subscription("demo", (x) =>
        {
            
        });

        await keyEventBus.PushAsync("demo", "测试");
        
        await loadEventBus.PushAsync("测试事件总线处理");

        await Task.Delay(1000);
    }
}