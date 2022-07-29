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

## 使用Key事件总线

```csharp
// 获取事件总线的接口
var distributedEventBus = services.GetService<IDistributedEventBus<string>>();
// 定义字符串类型的事件总线处理方法
distributedEventBus?.Subscribe("key",(data) =>
{
    Console.WriteLine(data);
});
// 提交字符串事件总线任务
distributedEventBus?.PublishAsync("key","测试内容");

```

## 使用类型事件总线

```csharp

// 定义一个Eto
public class TestEto
{
    public string? Name { get; set; }
}

.....

// 定义事件总线处理
public class EventTestEvent : ILocalEventHandler<Test>, ITransientDependency
{
    public Task HandleEventAsync(Test eventData)
    {
        // 当有事件触发将进入这里
        Console.WriteLine(eventData.Name);
        return Task.CompletedTask;
    }
}

·······

        // 在DI容器中获取到ILocalEventBus
        var push = app.ApplicationServices.GetRequiredService<ILocalEventBus>();
        // 发布一个事件
        await push.PublishAsync(new Test()
        {
            Name = "asd"
        });

```