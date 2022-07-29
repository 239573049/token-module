namespace Token.EventBus.EventBus;

public interface ILocalEventBus
{
    /// <summary>
    /// 触发一个事件。
    /// </summary>
    /// <param name="eventData">事件的相关数据</param>
    /// <param name="waitEvents">是否等待事件完成</param>
    /// <typeparam name="TEvent">事件类型</typeparam>
    /// <returns></returns>
    Task PublishAsync<TEvent>(TEvent eventData, bool waitEvents = true) where TEvent : class;

}