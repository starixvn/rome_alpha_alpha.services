namespace Alpha.Reports.Configurations;

public sealed class ReportAppSettings
{
    public byte SettingStatus { get; init; }

    public int BalanceUpDayAndMoveBetsDayCacheInSeconds { get; init; }

    public int OfferedProductsByCustIdCacheInSeconds { get; init; }

    public int CustomerLimitDateCacheInSeconds { get; init; }

    public MinimizeUM[] MinimizeUM { get; init; }

    public bool LocalTest { get; init; }

    public string SiteLabelBelonging777Group { get; init; }

    public bool Belonging777Group(string siteLabel)
        => !string.IsNullOrWhiteSpace(SiteLabelBelonging777Group)
           && SiteLabelBelonging777Group
               .Split(',')
               .Select(site => site.Trim())
               .Any(site => site.Equals(siteLabel, System.StringComparison.CurrentCultureIgnoreCase));

    public string CashServiceOfferedSites { get; init; }
}
