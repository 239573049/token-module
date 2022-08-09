using Token.EventBus.Handlers;
using Token.Module.Dependencys;

namespace Test.Token.EventBus;

public class DemoEventHandler : ILocalEventHandler<string>, ITransientDependency
{
    public Task HandleEventAsync(string eventData)
    {
        Console.WriteLine(eventData);

        return Task.CompletedTask;
    }
}