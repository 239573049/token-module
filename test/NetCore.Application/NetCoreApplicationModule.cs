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
        ConfigCors(services);
    }

    private void ConfigCors(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(Constant.Cors, corsBuilder =>
            {
                corsBuilder.SetIsOriginAllowed((string _) => true).AllowAnyMethod().AllowAnyHeader()
                    .AllowCredentials();
            });
        });
    }
    
    public override void OnApplicationShutdown(IApplicationBuilder app)
    {
        UseCors(app);
    }

    private void UseCors(IApplicationBuilder app)
    {
        app.UseCors(Constant.Cors);
    }
}