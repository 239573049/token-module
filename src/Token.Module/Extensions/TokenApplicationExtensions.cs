using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Token.Extensions;

public static class TokenApplicationExtensions
{
    public static TService? GetService<TService>(this IApplicationBuilder app) =>
        app.ApplicationServices.GetService<TService>();

    public static IConfiguration? GetConfiguration(this IApplicationBuilder app) =>
        app.ApplicationServices.GetService<IConfiguration>();

}