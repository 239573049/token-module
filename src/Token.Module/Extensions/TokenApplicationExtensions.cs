using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Token.Module.Extensions;


public static class TokenApplicationExtensions
{
    public static IWebHostEnvironment? GetEnvironment(
        this IApplicationBuilder app)
    {
        return app.ApplicationServices.GetService<IWebHostEnvironment>();
    }

    public static IConfiguration? GetConfiguration(this IApplicationBuilder app) => app.ApplicationServices.GetService<IConfiguration>();

    public static ILoggerFactory? GetLoggerFactory(IApplicationBuilder app) => app.ApplicationServices.GetService<ILoggerFactory>();
}