using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Token.Attributes;
using Token.Dependency;

namespace Token.Extensions;

public static class DependencyExtensions
{
    public static void AddAutoInject(this IServiceCollection services, IEnumerable<ITokenModule> tokenModules)
    {
        // 加载所有需要注入的程序集（只有引用的模块）
        var assemblies = tokenModules.Select(x => x.GetType().Assembly).Distinct()
            .SelectMany(x => x.GetTypes());

        // 过滤程序集
        var types = assemblies
            .Where(type => type.IsAssignableModule());

        // 根据继承的接口注入相对应的生命周期
        foreach (var t in types)
        {
            var interfaces = t.GetDependencyType();

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

    /// <summary>
    /// 获取注入方法相对应的需要注入的标记
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static Type? GetDependencyType(this Type type)
    {
        var exposeServices = type.GetCustomAttribute<ExposeServicesAttribute>();
        if (exposeServices == null)
        {
            return type.GetInterfaces().Where(x => x.Name.EndsWith(type.Name))?.FirstOrDefault();
        }

        return type.GetInterfaces().Where(x => x == exposeServices.Type)?.FirstOrDefault();
    }

    /// <summary>
    /// 是否支持注入
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsAssignableModule(this Type type)
    {
        var disabledInjectAttribute = type.GetCustomAttribute<DisabledInjectAttribute>();

        if (disabledInjectAttribute?.Disabled == true)
        {
            return false;
        }

        if (type.Attributes.HasFlag(TypeAttributes.Abstract) || type.Attributes.HasFlag(TypeAttributes.Interface))
        {
            return false;
        }

        if (typeof(ISingletonDependency).IsAssignableFrom(type) ||
                           typeof(IScopedDependency).IsAssignableFrom(type) ||
                           typeof(ITransientDependency).IsAssignableFrom(type))
        {
            return true;
        }

        return false;
    }
}