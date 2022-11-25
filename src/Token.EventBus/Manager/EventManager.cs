using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;
using Microsoft.Extensions.Options;
using Token.Handlers;
using Token.Options;

namespace Token.Manager;

public class EventManager<TEntity> : IDisposable where TEntity : class
{
    private bool _disposable = false;

    private readonly Channel<TEntity> _queue;
    private readonly IServiceProvider _serviceProvider;

    public EventManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        var eventBusOption = serviceProvider.GetService<IOptions<EventBusOption>>()?.Value ?? new EventBusOption();

        _queue = Channel.CreateBounded<TEntity>(eventBusOption.Capacity);
        Start();
    }

    private void Start()
    {
        _ = Task.Factory.StartNew(async () =>
        {
            while (!_disposable)
            {
                var result = await _queue.Reader.ReadAsync();

                Dequeue(result);
            }

        });
    }

    private void Dequeue(TEntity entity)
    {
        _ = Task.Run(async () =>
        {
            var loadEventHandler = _serviceProvider.GetService<ILoadEventHandler<TEntity>>();

            await loadEventHandler.HandleEventAsync(entity);
        });
    }

    /// <summary>
    /// 添加入队
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task EnqueueAsync(TEntity entity)
    {
        await _queue.Writer.WriteAsync(entity);
    }

    public void Dispose()
    {
        _disposable = true;
    }
}