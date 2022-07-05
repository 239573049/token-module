using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Token.Module;

public interface ITokenModule
{
    Task ConfigureServicesAsync(IServiceCollection context);

    void ConfigureServices(IServiceCollection context);
    
    Task OnApplicationShutdownAsync(IApplicationBuilder context);

    void OnApplicationShutdown(IApplicationBuilder context);
}