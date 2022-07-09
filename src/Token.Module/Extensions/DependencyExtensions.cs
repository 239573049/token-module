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

        // 过滤程序集
        var types = assemblies
            .Where(type => typeof(ISingletonDependency).IsAssignableFrom(type) ||
                           typeof(IScopedDependency).IsAssignableFrom(type) ||
                           typeof(ITransientDependency).IsAssignableFrom(type));
        // 注入
        foreach (var t in types)
        {
            var interfaces = t.GetInterfaces().Where(x => x.Name.EndsWith(t.Name))?.FirstOrDefault();

            if (interfaces != null)
            {
                if (t.IsAssignableFrom<ITransientDependency>())
                {
                    services.AddTransient(interfaces, t);
                }
                else if (t.IsAssignableFrom<IScopedDependency>())
                {
                    services.AddScoped(interfaces, t);
                }
                else if (t.IsAssignableFrom<ISingletonDependency>())
                {
                    services.AddSingleton(interfaces, t);
                }
            }
            else
            {
                if (t.IsAssignableFrom<ITransientDependency>())
                {
                    services.AddTransient(t);
                }
                else if (t.IsAssignableFrom<IScopedDependency>())
                {
                    services.AddScoped(t);
                }
                else if (t.IsAssignableFrom<ISingletonDependency>())
                {
                    services.AddSingleton(t);
                }
            }
        }

        types = null;
        assemblies = null;
    }
}