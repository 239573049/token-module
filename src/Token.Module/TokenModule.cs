using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;

namespace Token.Module;

public abstract class TokenModule : ITokenModule
{
    private IServiceCollection _serviceCollection;

    private IServiceCollection ServiceCollection
    {
        get => this._serviceCollection != null
                   ? this._serviceCollection
                   : throw new Exception("在启动模块的时候ServiceCollection为空");
        set => this._serviceCollection = value;
    }

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

    protected void Configure<TOptions>(Action<TOptions> configureOptions) where TOptions : class =>
        ServiceCollection.Configure(configureOptions);

    protected void Configure<TOptions>(string name, Action<TOptions> configureOptions) where TOptions : class =>
        ServiceCollection.Configure(name, configureOptions);
}