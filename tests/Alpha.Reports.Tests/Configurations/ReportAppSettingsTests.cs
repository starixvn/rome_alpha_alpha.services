using Alpha.Reports.Configurations;
using Xunit;

namespace Alpha.Reports.Tests.Configurations;

public class ReportAppSettingsTests
{
    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange & Act
        var actual = new ReportAppSettings
        {
            SettingStatus = 1,
            BalanceUpDayAndMoveBetsDayCacheInSeconds = 120,
            OfferedProductsByCustIdCacheInSeconds = 300
        };

        // Assert
        Assert.True(1 == actual.SettingStatus
            && 120 == actual.BalanceUpDayAndMoveBetsDayCacheInSeconds
            && 300 == actual.OfferedProductsByCustIdCacheInSeconds);
    }
}