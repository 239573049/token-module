using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Tag;

public interface ITokenTag
{
    Task ConfigureServicesAsync(IServiceCollection context);

    void ConfigureServices(IServiceCollection context);
    
    Task OnApplicationShutdownAsync(IApplicationBuilder context);

    void OnApplicationShutdown(IApplicationBuilder context);
}