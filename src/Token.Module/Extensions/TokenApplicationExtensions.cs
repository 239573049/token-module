using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Token.Module.Extensions;

public static class TokenApplicationExtensions
{
    public static TService? GetService<TService>(this IApplicationBuilder app) =>
        app.ApplicationServices.GetService<TService>();

    public static IConfiguration? GetConfiguration(this IApplicationBuilder app) =>
        app.ApplicationServices.GetService<IConfiguration>();

    public static ILoggerFactory? GetLoggerFactory(this IApplicationBuilder app) =>
        app.ApplicationServices.GetService<ILoggerFactory>();
}