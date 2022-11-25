using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using Token.Events;
using Token.Handlers;

namespace Token.Manager;

public class EventManager<TEntity> : IDisposable where TEntity : class
{
    private bool _disposable = false;
    private readonly ConcurrentQueue<TEntity> _queue;
    private readonly IServiceProvider _serviceProvider;

    public readonly TriggerEvent.EventExceptionHandler<TEntity>? EventExceptionHandler;

    public EventManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        _queue = new ConcurrentQueue<TEntity>();
    }

    private void Start()
    {
        _ = Task.Factory.StartNew(() =>
        {
            if (!_disposable && _queue.TryDequeue(out var result))
            {
                Dequeue(result);
            }
        });
    }

    private void Dequeue(TEntity entity)
    {
        _ = Task.Run(async () =>
        {
            var loadEventHandler = _serviceProvider.GetService<ILoadEventHandler<TEntity>>();
            try
            {
                await loadEventHandler.HandleEventAsync(entity);
            }
            catch (Exception e)
            {
                var result = EventExceptionHandler?.Invoke(entity, e);
                if (result == false)
                {
                    throw;
                }
            }
        });
    }

    /// <summary>
    /// 添加入队
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public void Enqueue(TEntity entity)
    {
        _queue.Enqueue(entity);

        // 当数据入队成功启动消费
        Start();
    }

    public void Dispose()
    {
        _disposable = true;
    }
}