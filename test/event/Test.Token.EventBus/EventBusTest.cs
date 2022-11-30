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


        await loadEventBus.PushAsync("1");
        await loadEventBus.PushAsync("2");
        await loadEventBus.PushAsync("3");
        await loadEventBus.PushAsync("4");
        await loadEventBus.PushAsync("5");

        keyEventBus.Subscription("demo", (x) =>
        {

        });

        await keyEventBus.PushAsync("demo", "1");

        await Task.Delay(100000);
    }
}