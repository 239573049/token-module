using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Tag;

public abstract class TokenTag:ITokenTag
{
    public virtual Task ConfigureServicesAsync(IServiceCollection services)
    {
        ConfigureServices(services);
        return Task.CompletedTask;
    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
    }

    public virtual Task OnApplicationShutdownAsync(IApplicationBuilder app)
    {
        OnApplicationShutdown(app);
        return Task.CompletedTask;
    }

    public virtual void OnApplicationShutdown(IApplicationBuilder app)
    {
    }
}