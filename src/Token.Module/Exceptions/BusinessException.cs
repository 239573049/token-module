using System;

namespace Token.Module.Exceptions;

public class BusinessException : Exception
{
    /// <summary>
    /// 异常状态
    /// </summary>
    public int Code { get; }

    public BusinessException(string message, int code = 400) : base(message)
    {
        Code = code;
    }

    public BusinessException(string message, Exception innerException, int code = 400) : base(message, innerException)
    {
        Code = code;
    }
}