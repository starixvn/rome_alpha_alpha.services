namespace Alpha.Shared.Extensions;

public static class DateTimeExtensions
{
    private const string LongDateTimeMinute = "dd/MM/yyyy hh:mm tt";

    public static DateTime FirstDayOfMonth(this DateTime dateTime)
        => new(dateTime.Year, dateTime.Month, 1);

    public static string ToLongDateTimeMinute(this DateTime dateTime)
        => dateTime.ToString(LongDateTimeMinute);
}
