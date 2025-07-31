using Alpha.Reports.Configurations;
using Xunit;

namespace Alpha.Reports.Tests.Configurations;

public class ReportAppSettingsTests
{
    [Fact]
    public void SettingStatus_SetAndGet_ReturnsExpectedValue()
    {
        // Arrange
        byte expectedStatus = 1;
        var settings = new ReportAppSettings { SettingStatus = expectedStatus };

        // Act & Assert
        Assert.Equal(expectedStatus, settings.SettingStatus);
    }

    [Fact]
    public void BalanceUpDayAndMoveBetsDayCacheInSeconds_SetAndGet_ReturnsExpectedValue()
    {
        // Arrange
        int expectedValue = 300;
        var settings = new ReportAppSettings { BalanceUpDayAndMoveBetsDayCacheInSeconds = expectedValue };

        // Act & Assert
        Assert.Equal(expectedValue, settings.BalanceUpDayAndMoveBetsDayCacheInSeconds);
    }

    [Fact]
    public void OfferedProductsByCustIdCacheInSeconds_SetAndGet_ReturnsExpectedValue()
    {
        // Arrange
        int expectedValue = 600;
        var settings = new ReportAppSettings { OfferedProductsByCustIdCacheInSeconds = expectedValue };

        // Act & Assert
        Assert.Equal(expectedValue, settings.OfferedProductsByCustIdCacheInSeconds);
    }

    [Fact]
    public void CustomerLimitDateCacheInSeconds_SetAndGet_ReturnsExpectedValue()
    {
        // Arrange
        int expectedValue = 900;
        var settings = new ReportAppSettings { CustomerLimitDateCacheInSeconds = expectedValue };

        // Act & Assert
        Assert.Equal(expectedValue, settings.CustomerLimitDateCacheInSeconds);
    }

    [Fact]
    public void LocalTest_SetAndGet_ReturnsExpectedValue()
    {
        // Arrange
        bool expectedValue = true;
        var settings = new ReportAppSettings { LocalTest = expectedValue };

        // Act & Assert
        Assert.Equal(expectedValue, settings.LocalTest);
    }

    [Fact]
    public void SiteLabelBelonging777Group_SetAndGet_ReturnsExpectedValue()
    {
        // Arrange
        string expectedValue = "site1,site2,site3";
        var settings = new ReportAppSettings { SiteLabelBelonging777Group = expectedValue };

        // Act & Assert
        Assert.Equal(expectedValue, settings.SiteLabelBelonging777Group);
    }

    [Theory]
    [InlineData("site1", "site1,site2,site3", true)]
    [InlineData("SITE1", "site1,site2,site3", true)] // Case insensitive
    [InlineData("site2", "site1,site2,site3", true)]
    [InlineData("site4", "site1,site2,site3", false)]
    [InlineData("site1", "", false)]
    [InlineData("site1", null, false)]
    [InlineData("site1", " site1 , site2 ", true)] // With spaces
    public void Belonging777Group_WithVariousInputs_ReturnsExpectedResult(string siteLabel, string configuredSites, bool expected)
    {
        // Arrange
        var settings = new ReportAppSettings { SiteLabelBelonging777Group = configuredSites };

        // Act
        var result = settings.Belonging777Group(siteLabel);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CashServiceOfferedSites_SetAndGet_ReturnsExpectedValue()
    {
        // Arrange
        string expectedValue = "site1,site2,site3";
        var settings = new ReportAppSettings { CashServiceOfferedSites = expectedValue };

        // Act & Assert
        Assert.Equal(expectedValue, settings.CashServiceOfferedSites);
    }
}
