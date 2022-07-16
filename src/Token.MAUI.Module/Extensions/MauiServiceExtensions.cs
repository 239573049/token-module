using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Token.MAUI.Module.Extensions;

public static class MauiServiceExtensions
{
    public static TService GetService<TService>(this IServiceCollection services) =>
        services.BuildServiceProvider().GetService<TService>();
    
    public static IConfiguration GetConfiguration(this IServiceCollection services) =>
        services.BuildServiceProvider().GetService<IConfiguration>();

    public static ILoggerFactory GetLoggerFactory(IServiceCollection services) =>
        services.BuildServiceProvider().GetService<ILoggerFactory>();
}