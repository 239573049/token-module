using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Token.Module.Dependencys;

namespace Token.Module.Extensions;

public static class DependencyExtensions
{
    public static void AddAutoInject(this IServiceCollection services, List<ITokenModule> tokenModules)
    {
        var assemblies = tokenModules.Select(x => x.GetType().Assembly).Distinct()
            .SelectMany(x => x.GetTypes());

        var types = assemblies
            .Where(type => typeof(ISingletonDependency).IsAssignableFrom(type) ||
                           typeof(IScopedDependency).IsAssignableFrom(type) ||
                           typeof(ITransientDependency).IsAssignableFrom(type));

        foreach (var t in types)
        {
            var interfaces = t.GetInterfaces().Where(x => x.Name.EndsWith(t.Name))?.FirstOrDefault();

            if (interfaces != null)
            {
                if (typeof(ITransientDependency).IsAssignableFrom(t))
                {
                    services.AddTransient(interfaces, t);
                }
                else if (typeof(IScopedDependency).IsAssignableFrom(t))
                {
                    services.AddScoped(interfaces, t);
                }
                else if (typeof(ISingletonDependency).IsAssignableFrom(t))
                {
                    services.AddSingleton(interfaces, t);
                }
            }
            else
            {
                if (typeof(ITransientDependency).IsAssignableFrom(t))
                {
                    services.AddTransient(t);
                }
                else if (typeof(IScopedDependency).IsAssignableFrom(t))
                {
                    services.AddScoped(t);
                }
                else if (typeof(ISingletonDependency).IsAssignableFrom(t))
                {
                    services.AddSingleton(t);
                }
            }
        }

        types = null;
        assemblies = null;
    }
}