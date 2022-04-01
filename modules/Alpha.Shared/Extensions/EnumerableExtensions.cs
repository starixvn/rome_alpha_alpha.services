namespace Alpha.Shared.Extensions;

public static class EnumerableExtensions
{
    public static string JoinWithComma<T>(this IEnumerable<T> source, string separator = ",")
        => source == null ? string.Empty : string.Join(separator, source);
}
