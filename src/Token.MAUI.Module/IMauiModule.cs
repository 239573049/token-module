namespace Token.MAUI.Module;

public interface IMauiModule
{
    Task ConfigureServicesAsync(IServiceCollection services);

    void ConfigureServices(IServiceCollection services);

    Task OnApplicationShutdownAsync(MauiApp app);

    void OnApplicationShutdown(MauiApp app);
}