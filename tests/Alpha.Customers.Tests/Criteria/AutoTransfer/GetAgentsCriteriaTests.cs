using Alpha.Customers.Criteria.AutoTransfer;
using Xunit;

namespace Alpha.Customers.Tests.Criteria.AutoTransfer;

public class GetAgentsCriteriaTests
{
    [Fact]
    public void GetSettingKey_ReturnsCorrectKey()
    {
        // Arrange
        var criteria = new GetAgentsCriteria { SiteDBGroupID = "123" };

        // Act
        var result = criteria.GetSettingKey();

        // Assert
        Assert.Equal("SiteDB123_Age_Transfer_SelList_Level3", result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(100, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void IsValid_ChecksMasterId(int masterId, bool expectedResult)
    {
        // Arrange
        var criteria = new GetAgentsCriteria { MasterId = masterId };

        // Act
        var result = criteria.IsValid();

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void MasterId_CanBeSetAndRetrieved()
    {
        // Arrange
        var criteria = new GetAgentsCriteria();

        // Act
        criteria.MasterId = 42;

        // Assert
        Assert.Equal(42, criteria.MasterId);
    }
}
