using Token.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NetCore.Application;

public class NetCoreApplicationModule:TokenModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        
    }

    public override void OnApplicationShutdown(IApplicationBuilder app)
    {
        
    }
}