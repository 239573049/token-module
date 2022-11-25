namespace Token.Events;

public static class TriggerEvent
{
    /// <summary>
    /// 本地事件异常事件
    /// </summary>
    /// <returns>false抛出异常|true 不抛出异常</returns>
    /// <typeparam name="TEntity"></typeparam>
    public delegate bool EventExceptionHandler<in TEntity>(TEntity entity,Exception exception)  where TEntity : class;
}