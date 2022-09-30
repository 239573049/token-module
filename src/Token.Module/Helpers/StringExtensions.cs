namespace Token.Module.Helpers;

/// <summary>
/// 字符串扩展方法
/// </summary>
public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? value) =>
        string.IsNullOrEmpty(value);

    public static bool IsNullOrWhiteSpace(this string? value) =>
        string.IsNullOrWhiteSpace(value);
}