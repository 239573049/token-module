﻿using System.Linq;
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
    /// 初始化Service
    /// </summary>
    /// <param name="services"></param>
    /// <param name="isAutoInject">是否自动依赖注入</param>
    /// <typeparam name="TModule"></typeparam>
    public static async Task AddTagApplication<TModule>(this IServiceCollection services,bool isAutoInject=true) where TModule : ITokenModule
    {
        var types = new List<ITokenModule>();
        var type = typeof(TModule);
        var attributes = type.GetCustomAttributes().OfType<DependOnAttribute>()
                             .SelectMany(x => x.Type);

        var module = type.Assembly.CreateInstance(type.FullName, true) as ITokenModule;
        types.Add(module);
        await module.ConfigureServicesAsync(services);
        foreach (var t in attributes)
        {
            if (t.Assembly.CreateInstance(t.FullName, true) is not ITokenModule ts)
                continue;

            types.Add(ts);
            await ts.ConfigureServicesAsync(services);
        }
        services.AddSingleton(types);
        if (isAutoInject)
        {
            services.AddAutoInject(types);
        }
    }

    /// <summary>
    /// 初始化Application
    /// </summary>
    /// <param name="app"></param>
    public static  void InitializeApplication(this IApplicationBuilder app)
    {
        var tag = app.ApplicationServices.GetService<List<ITokenModule>>();

        async void Action(ITokenModule x) => await x.OnApplicationShutdownAsync(app);

        tag.ForEach(Action);
    }
}