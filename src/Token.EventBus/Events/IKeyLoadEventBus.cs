namespace Token.Events;

public interface IKeyLoadEventBus
{
    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task PushAsync(string name,object entity);

    /// <summary>
    /// 订阅事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="func"></param>
    void Subscription(string name,Action<object> func);

    /// <summary>
    /// 删除事件
    /// </summary>
    /// <param name="name"></param>
    void Remove(string name);
}