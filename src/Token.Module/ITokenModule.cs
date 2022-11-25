using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Token;

public interface ITokenModule
{
    Task ConfigureServicesAsync(IServiceCollection services);

    void ConfigureServices(IServiceCollection services);

    Task OnApplicationShutdownAsync(IApplicationBuilder app);

    void OnApplicationShutdown(IApplicationBuilder app);
}