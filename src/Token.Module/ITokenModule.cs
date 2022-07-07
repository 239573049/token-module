using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Token.Module;

public interface ITokenModule
{
    Task ConfigureServicesAsync(IServiceCollection services);

    void ConfigureServices(IServiceCollection services);
    
    Task OnApplicationShutdownAsync(IApplicationBuilder app);

    void OnApplicationShutdown(IApplicationBuilder app);
}