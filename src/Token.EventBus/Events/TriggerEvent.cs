namespace Token.Events;

public static class TriggerEvent
{
    /// <summary>
    /// 本地事件异常事件
    /// </summary>
    /// <returns>false抛出异常|true 不抛出异常</returns>
    /// <typeparam name="TEntity">异常的数据</typeparam>
    /// <typeparam name="type">异常的类型</typeparam>
    public delegate bool EventExceptionHandler<in TEntity>(TEntity entity,Type type,Exception exception)  where TEntity : class;
}