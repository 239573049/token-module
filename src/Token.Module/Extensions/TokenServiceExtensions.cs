using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Token.Extensions;

public static class TokenServiceExtensions
{
    public static TService? GetService<TService>(this IServiceCollection services) =>
        services.BuildServiceProvider().GetService<TService>();
    
    public static IConfiguration? GetConfiguration(this IServiceCollection services) =>
        services.BuildServiceProvider().GetService<IConfiguration>();

}