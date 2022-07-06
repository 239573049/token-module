# TokenModule

[![NuGet](https://img.shields.io/nuget/dt/Token.Module.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/Token.Module/)
[![NuGet](https://img.shields.io/nuget/v/Token.Module.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/Token.Module/)

## 介绍
模块注入模块引用
设计灵感来源于ABPModule，简化了很多没有必要的依赖，可自动扩展工具

## 使用教程
```csharp

using Token.Module.Extensions;
using NetCoreTest;

var builder = WebApplication.CreateBuilder(args);
// 执行这一步的时候就会先执行NetCoreTestModule里面的 ConfigureService方法，
// 执行顺序ConfigureServicesAsync =》ConfigureService方法
// 如果NetCoreTestModule中使用了[DependOn(typeof(其他的Module))] 这样就可以按照传入顺序一并执行
builder.Services.AddTagApplication<NetCoreTestModule>();
var app = builder.Build();

// 执行这一步的时候就会先执行NetCoreTestModule里面的 OnApplicationShutdown，
// OnApplicationShutdownAsync =》OnApplicationShutdown方法
app.InitializeApplication();

app.Run();
```
## 测试
在test文件中存在简单的使用示例
