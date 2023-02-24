namespace Common;

public static class StringUtils
{
    public static bool IsSignificant(this string value) => !string.IsNullOrWhiteSpace(value);

    public static string ToStringJoin<T>(this IEnumerable<T> enumerable, string separator = ", ") => string.Join(separator, enumerable);
}