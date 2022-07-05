using Token.Module;
using NetCore.Application;
using Token.Module.Attributes;

namespace NetCoreTest;

[DependOn(typeof(NetCoreApplicationModule))]
public class NetCoreTestModule :TokenModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        
    }

    public override void OnApplicationShutdown(IApplicationBuilder app)
    {
    }
}