using Token.Handlers;

namespace Test.Token.EventBus;

public class DemoLoadEventHandler : ILoadEventHandler<string>
{
    public Task HandleEventAsync(string eventData)
    {
        Console.WriteLine(eventData);

        return Task.CompletedTask;
    }
}