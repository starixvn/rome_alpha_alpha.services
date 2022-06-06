using System.Diagnostics.CodeAnalysis;

namespace Alpha.Shared.Extensions;

[ExcludeFromCodeCoverage]
public static class DateTimeExtensions
{
    public static DateTime FirstDayOfMonth(this DateTime dateTime)
        => new(dateTime.Year, dateTime.Month, 1);
}
