using Alpha.Reports.Criteria;
using Xunit;

namespace Alpha.Reports.Tests.Criteria;

public class GetBalanceUpDayAndMoveBetsDayCriteriaTests
{
    [Fact]
    public void GetSettingKey_Always_ReturnsValidSettingKey()
    {
        // Arrange
        var criteria = new GetBalanceUpDayAndMoveBetsDayCriteria();

        // Act & Assert
        Assert.Equal("Age_SelBalanceUpDay", criteria.GetSettingKey());
    }

    [Fact]
    public void IsValid_Always_ReturnsTrue()
    {
        // Arrange
        var criteria = new GetBalanceUpDayAndMoveBetsDayCriteria();

        // Act & Assert
        Assert.True(criteria.IsValid());
    }
}