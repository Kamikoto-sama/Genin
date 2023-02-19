namespace Common;

public static class StringUtils
{
    public static bool IsSignificant(this string value) => !string.IsNullOrWhiteSpace(value);
}