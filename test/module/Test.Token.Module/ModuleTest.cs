using Token.Extensions;

namespace Test.Token.Module;

public class ModuleTest
{
    [Fact]
    public async Task StartModule()
    {
        var services = new ServiceCollection();
        await services.AddModuleApplicationAsync<TestTokenModule>();
        
    }
}