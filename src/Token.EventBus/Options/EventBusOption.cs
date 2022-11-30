namespace Token.Options;

public class EventBusOption
{
    /// <summary>
    /// 设置EventManager的默认管道容量
    /// </summary>
    public int Capacity { get; set; } = 100000;
}