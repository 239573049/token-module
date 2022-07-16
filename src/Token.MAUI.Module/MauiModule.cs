namespace Token.MAUI.Module;

public abstract class MauiModule : IMauiModule
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

    public virtual Task OnApplicationShutdownAsync(MauiApp app)
    {
        OnApplicationShutdown(app);
        return Task.CompletedTask;
    }

    public virtual void OnApplicationShutdown(MauiApp app)
    {
    }

    protected void Configure<TOptions>(Action<TOptions> configureOptions) where TOptions : class =>
        _serviceCollection.Configure(configureOptions);

    protected void Configure<TOptions>(string name, Action<TOptions> configureOptions) where TOptions : class =>
        _serviceCollection.Configure(name, configureOptions);
}