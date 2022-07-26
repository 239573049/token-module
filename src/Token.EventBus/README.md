# TokenModule

[![NuGet](https://img.shields.io/nuget/dt/Token.EventBus.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/Token.EventBus)
[![NuGet](https://img.shields.io/nuget/v/Token.EventBus.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/Token.EventBus/)

## 介绍

简化本地事件总线方法

## 使用教程

注入事件总线

```csharp
services.AddEventBus();
```

使用事件总线

```csharp
// 获取事件总线的接口
var distributedEventBus = services.GetService<IDistributedEventBus>();
// 定义字符串类型的事件总线处理方法
distributedEventBus?.Subscribe((string data) =>
{
    Console.WriteLine(data);
});
string data = "测试";
// 提交字符串事件总线任务
distributedEventBus?.PublishAsync(data);

```