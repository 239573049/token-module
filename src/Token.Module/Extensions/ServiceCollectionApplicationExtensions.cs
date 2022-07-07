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
    /// 初始化Service
    /// </summary>
    /// <param name="services"></param>
    /// <param name="isAutoInject">是否自动依赖注入</param>
    /// <typeparam name="TModule"></typeparam>
    public static async Task AddTagApplication<TModule>(this IServiceCollection services,bool isAutoInject=true) where TModule : ITokenModule
    {
        var types = new List<ITokenModule>();
        var type = typeof(TModule);
        await GetModuleTypeAsync(type, types);

        foreach (var t in types.Distinct())
        {
            await t.ConfigureServicesAsync(services);
        }
        
        services.AddSingleton(types);
        if (isAutoInject)
        {
            services.AddAutoInject(types);
        }
    }

    private static async Task GetModuleTypeAsync(Type type,List<ITokenModule> types)
    {
        var iTokenModule = typeof(ITokenModule);
        if (!iTokenModule.IsAssignableFrom(type))
        {
            return;
        }
        // 通过放射创建一个对象并且回调方法
        ITokenModule typeInstance = type.Assembly.CreateInstance(type.FullName, true) as ITokenModule;

        if (typeInstance != null) types.Add(typeInstance);

        // 获取DependOn特性注入的模块
        var attributes = type.GetCustomAttributes().OfType<DependOnAttribute>()
            .SelectMany(x => x.Type).Where(x=>iTokenModule.IsAssignableFrom(x));
        

        foreach (var t in attributes)
        {
            ITokenModule module = t.Assembly.CreateInstance(t?.FullName, true) as ITokenModule;
            if(module==null)
                continue;

            types.Add(module);
            // 可能存在循环依赖的问题
            await GetModuleTypeAsync(t, types);
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