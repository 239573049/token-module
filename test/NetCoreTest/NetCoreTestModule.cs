using Token.Module;
using NetCore.Application;
using NetCore.HttpApi;
using Token.Module.Attributes;

namespace NetCoreTest;

[DependOn(
    typeof(NetCoreHttpApiModule),
    typeof(NetCoreApplicationModule))]
public class NetCoreTestModule :TokenModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
    }

    public override void OnApplicationShutdown(IApplicationBuilder app)
    {
    }
}