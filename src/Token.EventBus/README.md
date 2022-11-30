# TokenModule

[![NuGet](https://img.shields.io/nuget/dt/Token.EventBus.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/Token.EventBus)
[![NuGet](https://img.shields.io/nuget/v/Token.EventBus.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/Token.EventBus/)

## 介绍

简化本地事件总线方法
LoadEventBus实现采用Channel实现本地mq

## 使用教程

注入事件总线

```csharp
services.AddEventBus();
```

## 使用Key事件总线

```csharp
// 获取事件总线的接口
var keyLoadEventBus = services.GetService<IKeyLoadEventBus>();
// 定义字符串类型的事件总线处理方法
keyLoadEventBus?.Subscribe("key",(data) =>
{
    var result = data as string;
    Console.WriteLine(result);
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
public class EventTestEvent : ILoadEventHandler<Test>, ITransientDependency
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
        var push = app.ApplicationServices.GetRequiredService<ILoadEventBus<Test>>();
        // 发布一个事件
        await push.PushAsync(new Test()
        {
            Name = "asd"
        });

```



## 本地事件总线实现原理

## 第一步注入本地事件总线服务

```c#
services.AddEventBus();
```

注入这个服务的时候

内部方法其实只是将本地事件需要的方法注入到容器中共Service去依赖注入

## 第二步订阅事件

我们实现存在了一个事件处理器比如下代码;这个就是我们的本地事件处理器了，其实就是注入绑定 `ILoadEventHandler<string>` 提供了`HandleEventAsync()`方法回调 `DemoLoadEventHandler`是实现接口的实现类
实现了`HandleEventAsync`方法在内部就可以写具体业务了

```c#
using Token.Handlers;

namespace Test.Token.EventBus;

public class DemoLoadEventHandler : ILoadEventHandler<string>
{
    public Task HandleEventAsync(string eventData)
    {
        Console.WriteLine(eventData);

        return Task.CompletedTask;
    }
}
```

然后在讲当前服务注入到容器中 

```c#
services.AddTransient(typeof(ILoadEventHandler<string>),typeof(DemoLoadEventHandler));
```

这样就完成了事件订阅

## 第三步发布事件

```c#
var loadEventBus = serviceProvider.GetRequiredService<ILoadEventBus<string>>();
await loadEventBus.PushAsync("发布事件");
```

这样就完成了事件的发布

在`loadEventBus.PushAsync("发布事件")`内部其实是将数据添加到 `EventManager`中，`EventManager`内部实现是由`Channel`实现的一个当前线程的本地MQ 下面代码是EventManager 实现

```c#
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Threading.Channels;
using Microsoft.Extensions.Options;
using Token.Events;
using Token.Handlers;
using Token.Options;

namespace Token.Manager;

public class EventManager<TEntity> : IDisposable where TEntity : class
{
    private bool _disposable;
    private readonly CancellationToken _cancellation;
    private readonly Channel<TEntity> _queue;
    private readonly IServiceProvider _serviceProvider;
    public readonly TriggerEvent.EventExceptionHandler<TEntity>? EventExceptionHandler;

    public EventManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _cancellation = CancellationToken.None;
        var eventBus = serviceProvider.GetService<IOptions<EventBusOption>>()?.Value ?? new EventBusOption();
        _queue = Channel.CreateBounded<TEntity>(eventBus.Capacity);
    }

    private void Start()
    {
        _ = Task.Factory.StartNew(async () =>
        {
            while (!_disposable)
            {
                var result = await _queue.Reader.ReadAsync(_cancellation);
                Dequeue(result);
            }
        }, _cancellation);
    }

    private void Dequeue(TEntity entity)
    {
        _ = Task.Run(async () =>
        {
            var loadEventHandler = _serviceProvider.GetServices<ILoadEventHandler<TEntity>>();
            foreach (var handler in loadEventHandler)
            {
                try
                {
                    await handler.HandleEventAsync(entity);
                }
                catch (Exception e)
                {
                    var result = EventExceptionHandler?.Invoke(entity,handler.GetType(), e);
                    if (result == false)
                    {
                        throw;
                    }
                }
            }
        }, _cancellation);
    }

    /// <summary>
    /// 添加入队
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task EnqueueAsync(TEntity entity)
    {
        await _queue.Writer.WriteAsync(entity, _cancellation);

        // 当数据入队成功启动消费
        Start();
    }

    public void Dispose()
    {
        _disposable = true;
        _cancellation.ThrowIfCancellationRequested();
    }
}
```

当`loadEventBus.PushAsync("发布事件")`以后数据将添加到`_queue`队列中，然后在Start方法里面存在读取`_queue`队列的数据，然后在通过依赖注入，将数据传递到订阅者。这整个流程就是mq的生产 =》 消费 
