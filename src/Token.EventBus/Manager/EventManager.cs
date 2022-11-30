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