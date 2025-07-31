using Alpha.Reports.Criteria.WinLoss;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.WinLoss;

public class DailyEnteredSportCriteriaTests
{
    [Fact]
    public void Constructor_SetsIncludeTestDataProperty()
    {
        // Arrange
        var expectedIncludeTestData = true;

        // Act
        var criteria = new DailyEnteredSportCriteria(expectedIncludeTestData);

        // Assert
        Assert.Equal(expectedIncludeTestData, criteria.IncludeTestData);
    }

    [Fact]
    public void GetSettingKey_ReturnsCorrectKey()
    {
        // Arrange
        var criteria = new DailyEnteredSportCriteria(false);
        var expectedKey = "Age_Rpt_MatchSelResultEntered";

        // Act
        var result = criteria.GetSettingKey();

        // Assert
        Assert.Equal(expectedKey, result);
    }

    [Fact]
    public void IsValid_ReturnsTrue()
    {
        // Arrange
        var criteria = new DailyEnteredSportCriteria(false);

        // Act
        var result = criteria.IsValid();

        // Assert
        Assert.True(result);
    }
}
