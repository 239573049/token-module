using System.Reflection;
using Token.MAUI.Module.Attributes;

namespace Token.MAUI.Module.Extensions;

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
        where TModule : IMauiModule
    {
        var types = new List<Tuple<IMauiModule, int>>();
        var type = typeof(TModule);
        await GetModuleTypeAsync(type, types);

        var modules = types.OrderBy(x => x.Item2).Select(x => x.Item1).Distinct();

        var tokenModules = modules as IMauiModule[] ?? modules.ToArray();
        if (isAutoInject)
        {
            services.AddAutoInject(tokenModules);
        }


        foreach (var t in tokenModules)
        {
            await t.ConfigureServicesAsync(services);
        }

        services.AddSingleton(types);
    }

    /// <summary>
    /// 初始化Application
    /// </summary>
    /// <param name="app"></param>
    public static void InitializeApplication(this MauiApp app)
    {
        
        var types = app.Services.GetService<List<Tuple<IMauiModule, int>>>();

        var modules = types?.OrderBy(x => x.Item2).Select(x => x.Item1);

        if (modules == null)
            return;

        async void Action(IMauiModule x) => await x.OnApplicationShutdownAsync(app);

        foreach (var module in modules)
        {
            Action(module);
        }
    }

    private static async Task GetModuleTypeAsync(Type type, ICollection<Tuple<IMauiModule, int>> types)
    {
        if (!type.IsAssignableFrom<IMauiModule>())
        {
            return;
        }

        // 通过反射创建一个对象并且回调方法
        IMauiModule typeInstance = type.Assembly.CreateInstance(type.FullName, true) as IMauiModule;

        if (typeInstance != null) types.Add(new Tuple<IMauiModule, int>(typeInstance, GetRunOrder(type)));

        // 获取DependOn特性注入的模块
        var attributes = type.GetCustomAttributes().OfType<DependsOnAttribute>()
            .SelectMany(x => x.Type).Where(x=>x.IsAssignableFrom<DependsOnAttribute>());
        

        foreach (var t in attributes)
        {
            var module = t.Assembly.CreateInstance(t?.FullName, true) as IMauiModule;
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