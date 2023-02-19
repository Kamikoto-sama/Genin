namespace Common;

public static class EnumerableExtensions
{
    public static string ToStringJoin<T>(this IEnumerable<T> enumerable, string separator = ", ") => string.Join(separator, enumerable);
}