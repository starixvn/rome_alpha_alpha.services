using Alpha.Shared.Extensions;

namespace Alpha.Shared.Utils;

public static class SystemTime
{
    public static DateTime Today => DateTime.Today;

    public static DateTime Yesterday => Today.AddDays(-1);
}
