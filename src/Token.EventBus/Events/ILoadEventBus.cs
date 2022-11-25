namespace Token.Events;

public interface ILoadEventBus<in TEntity> where TEntity : class
{
    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task PushAsync(TEntity entity);
}