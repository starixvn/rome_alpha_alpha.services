namespace Alpha.Shared.Extensions;

public static class DateTimeExtensions
{
    public static DateTime FirstDayOfMonth(this DateTime dateTime)
        => new DateTime(dateTime.Year, dateTime.Month, 1);
}
