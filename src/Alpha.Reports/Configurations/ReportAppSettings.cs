namespace Alpha.Reports.Configurations;

public sealed class ReportAppSettings
{    
    public byte SettingStatus { get; init; }

    public int BalanceUpDayAndMoveBetsDayCacheInSeconds { get; init; }

    public int OfferedProductsByCustIdCacheInSeconds { get; init; }

    public int CustomerLimitDateCacheInSeconds { get; init; }

    public MinimizeUM[] MinimizeUM { get; init; }
}
