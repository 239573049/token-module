using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Token.MAUI.Module.Extensions;

public static class MauiApplicationExtensions
{
    public static TService GetService<TService>(this MauiApp app) =>
        app.Services.GetService<TService>();

    public static IConfiguration GetConfiguration(this MauiApp app) =>
        app.Services.GetService<IConfiguration>();

    public static ILoggerFactory GetLoggerFactory(this MauiApp app) =>
        app.Services.GetService<ILoggerFactory>();
}