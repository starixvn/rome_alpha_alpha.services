using Alpha.Customers.Criteria.AutoTransfer;
using Xunit;

namespace Alpha.Customers.Tests.Criteria.AutoTransfer;

public class GetMastersCriteriaTests
{
    [Fact]
    public void GetSettingKey_ReturnsCorrectKey()
    {
        // Arrange
        var criteria = new GetMastersCriteria { SiteDBGroupID = "5" };

        // Act
        var result = criteria.GetSettingKey();

        // Assert
        Assert.Equal("SiteDB5_Age_Transfer_SelList_Level4", result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(100, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void IsValid_ReturnsExpectedResult(int sagId, bool expected)
    {
        // Arrange
        var criteria = new GetMastersCriteria { SAgId = sagId };

        // Act
        var result = criteria.IsValid();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void DefaultProperties_HaveCorrectDefaultValues()
    {
        // Arrange
        var criteria = new GetMastersCriteria();

        // Act & Assert
        Assert.Equal(0, criteria.SAgId);
    }
}
