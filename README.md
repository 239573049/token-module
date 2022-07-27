# TokenModule
1. WebApi 的Module

[![NuGet](https://img.shields.io/nuget/dt/Token.Module.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/Token.Module/)
[![NuGet](https://img.shields.io/nuget/v/Token.Module.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/Token.Module/)

2. MAUI 的Module

[![NuGet](https://img.shields.io/nuget/dt/Token.MAUI.Module.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/Token.MAUI.Module/)
[![NuGet](https://img.shields.io/nuget/v/Token.MAUI.Module.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/Token.MAUI.Module/)
## 其他模块

[事件总线](./src/Token.EventBus/README.md)

## 介绍
模块注入模块引用
设计灵感来源于ABPModule，简化了很多没有必要的依赖，可自动扩展工具
/test/的Demo项目是一个完整的项目示例，有兴趣的可以查看阅读

## 使用教程
```csharp

using Token.Module.Extensions;
using NetCoreTest;

var builder = WebApplication.CreateBuilder(args);
// 执行这一步的时候就会先执行NetCoreTestModule里面的 ConfigureService方法，
// 执行顺序ConfigureServicesAsync =》ConfigureService方法
// 如果NetCoreTestModule中使用了[DependOn(typeof(其他的Module))] 这样就可以按照传入顺序一并执行
// 默认自动依赖注入继承指定依赖生命周期的接口
builder.Services.AddModuleApplication<NetCoreTestModule>();

// 这样将不会自动依赖注入
builder.Services.AddModuleApplication<NetCoreTestModule>(false);

var app = builder.Build();

// 执行这一步的时候就会先执行NetCoreTestModule里面的 OnApplicationShutdown，
// OnApplicationShutdownAsync =》OnApplicationShutdown方法
app.InitializeApplication();

app.Run();
```

## 自动依赖注入

// 如果没有取消自动注入的话，您只需要在实现类继承相应的接口即可
// 接口的名字和实现类型的名字必须基本一致，接口多加I其他的名字必须一致

IScopedDependency => services.AddScoped();

ISingletonDependency => service.AddSingleton();

ITransientDependency => service.AddTransient();

### 示例
```csharp
// 接口
public interface IDemoService
{
    Task<string> GetAsync();

    Task UpdateAsync(Guid id,string data);
}

// 实现类
public class DemoService : IDemoService, ISingletonDependency
{
    public Task<string> GetAsync()
    {

        return Task.FromResult("ok");
    }

    public async Task UpdateAsync(Guid id, string data)
    {
        await Task.CompletedTask;
    }
}
// 只需要按照这个示例就可以完成注入了，必须是未忽略自动注入才会注入
// 建议按照abp官方的DDD实践去项目划分
```
## 测试
在test文件中存在简单的使用示例
