using Token.EventBus.Handlers;
using Token.Module.Dependencys;

namespace NetCore.Event;

public class EventTestEvent : ILocalEventHandler<Test>, ITransientDependency
{
    public Task HandleEventAsync(Test eventData)
    {
        Console.WriteLine(eventData.Name);
        return Task.CompletedTask;
    }
}