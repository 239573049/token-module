using Microsoft.Extensions.DependencyInjection;
using Token.EventBus.EventBus;
using Token.EventBus.Handlers;
using Token.Module.Extensions;

namespace Test.Token.EventBus;

public class EventBusTest
{
    [Fact]
    public async Task EventBus()
    {
        
        var services = new ServiceCollection();
        await services.AddModuleApplication<TestTokenEventBusModule>();

        var eventBus = services.GetService<ILocalEventBus>();
        eventBus?.PublishAsync("data");

    }
}