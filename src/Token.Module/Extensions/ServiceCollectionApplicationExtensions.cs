using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using System.Collections.Generic;
using Token.Module.Attributes;

namespace Token.Module.Extensions;

public static class ServiceCollectionApplicationExtensions
{
    /// <summary>
    /// 初始化Service
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TTag"></typeparam>
    public static async Task AddTagApplication<TTag>(this IServiceCollection services) where TTag : ITokenModule
    {
        var types = new List<ITokenModule>();
        var type = typeof(TTag);
        var attributes = type.GetCustomAttributes().OfType<Token.Module.Attributes.DependOnAttribute>()
                             .SelectMany(x => x.Type);

        var tag = type.Assembly.CreateInstance(type.FullName, true) as ITokenModule;
        types.Add(tag);
        await tag.ConfigureServicesAsync(services);
        foreach (var t in attributes)
        {
            if (t.Assembly.CreateInstance(t.FullName, true) is not ITokenModule ts)
                continue;

            types.Add(ts);
            await ts.ConfigureServicesAsync(services);
        }

        services.AddSingleton(types);
    }

    /// <summary>
    /// 初始化Application
    /// </summary>
    /// <param name="app"></param>
    public static async Task InitializeApplication(this IApplicationBuilder app)
    {
        var tag = app.ApplicationServices.GetService<List<ITokenModule>>();

        async void Action(ITokenModule x) => await x.OnApplicationShutdownAsync(app);

        tag.ForEach(Action);
    }
}