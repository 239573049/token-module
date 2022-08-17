using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Token.Module.Attributes;

namespace Token.Module.Extensions;

public static class ServiceCollectionApplicationExtensions
{
    /// <summary>
    /// 默认运行顺序
    /// </summary>
    private static int _defaultOrder = 1;

    public static void SetDefaultOrder(int order)
    {
        _defaultOrder = order;
    }

    /// <summary>
    /// 初始化Service
    /// </summary>
    /// <param name="services"></param>
    /// <param name="isAutoInject">是否自动依赖注入</param>
    /// <typeparam name="TModule"></typeparam>
    public static async Task AddModuleApplication<TModule>(this IServiceCollection services, bool isAutoInject = true)
        where TModule : ITokenModule
    {
        var types = new List<Tuple<ITokenModule, int>>();
        var type = typeof(TModule);
        await GetModuleTypeAsync(type, types);

        var modules = types.OrderBy(x => x.Item2).Select(x => x.Item1).Distinct();

        var tokenModules = modules as ITokenModule[] ?? modules.ToArray();
        if (isAutoInject)
        {
            // 启动主动注入
            services.AddAutoInject(tokenModules);
        }
        
        services.AddSingleton(types);
        
        foreach (var t in tokenModules)
        {
            await t.ConfigureServicesAsync(services);
        }

    }

    /// <summary>
    /// 初始化Application
    /// </summary>
    /// <param name="app"></param>
    public static void InitializeApplication(this IApplicationBuilder app)
    {
        var types = app.ApplicationServices.GetService<List<Tuple<ITokenModule, int>>>();

        var modules = types?.OrderBy(x => x.Item2).Select(x => x.Item1);

        if (modules == null)
            return;

        async void Action(ITokenModule x) => await x.OnApplicationShutdownAsync(app);

        foreach (var module in modules)
        {
            Action(module);
        }
    }

    private static async Task GetModuleTypeAsync(Type type, ICollection<Tuple<ITokenModule, int>> types)
    {
        if (!type.IsAssignableFrom<ITokenModule>())
        {
            return;
        }

        // 通过放射创建一个对象并且回调方法
        ITokenModule typeInstance = type.Assembly.CreateInstance(type.FullName, true) as ITokenModule;

        if (typeInstance != null) types.Add(new Tuple<ITokenModule, int>(typeInstance, GetRunOrder(type)));

        // 获取DependOn特性注入的模块
        var attributes = type.GetCustomAttributes().OfType<DependOnAttribute>()
            .SelectMany(x => x.Type).Where(x=>x.IsAssignableFrom<ITokenModule>());
        

        foreach (var t in attributes)
        {
            ITokenModule? module = t.Assembly.CreateInstance(t?.FullName, true) as ITokenModule;
            if (module == null)
                continue;

            // 可能存在循环依赖的问题
            await GetModuleTypeAsync(t, types);
        }
    }

    private static int GetRunOrder(Type type)
    {
        var runOrder = type.GetCustomAttribute<RunOrderAttribute>();

        return runOrder?.Order ?? _defaultOrder++;
    }
}