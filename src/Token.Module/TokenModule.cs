using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Token;

public abstract class TokenModule : ITokenModule
{
    private IServiceCollection _serviceCollection;

    public virtual Task ConfigureServicesAsync(IServiceCollection services)
    {
        _serviceCollection = services ?? throw new ArgumentNullException(nameof(services));
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

    protected void Configure<TOptions>(Action<TOptions> configureOptions) where TOptions : class =>
        _serviceCollection.Configure(configureOptions);

    protected void Configure<TOptions>(string name, Action<TOptions> configureOptions) where TOptions : class =>
        _serviceCollection.Configure(name, configureOptions);
}