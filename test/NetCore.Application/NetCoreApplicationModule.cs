using Token.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NetCore.Application.Contracts;
using NetCore.Domain;
using Token.Module.Attributes;

namespace NetCore.Application;

[DependOn(
    typeof(NetCoreApplicationContractsModule),
    typeof(NetCoreDomainModule)
    )]
public class NetCoreApplicationModule:TokenModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        
    }

    public override void OnApplicationShutdown(IApplicationBuilder app)
    {
    }

}