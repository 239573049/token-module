using Token.Module.Dependencys;

namespace NetCore.Event;

public class DemoService : ISingletonDependency
{
    public async Task Get()
    {
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine("asda");
            await Task.Delay(1);
        }
    }
}