using Token.Module.Extensions;

namespace Test.Token.Module;

public class ModuleTest
{
    [Fact]
    public async Task StartModule()
    {
        var services = new ServiceCollection();
        await services.AddModuleApplication<TestTokenModule>();
        
        
    }
}