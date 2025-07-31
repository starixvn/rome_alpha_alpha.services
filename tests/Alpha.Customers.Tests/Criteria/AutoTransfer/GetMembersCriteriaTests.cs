using Alpha.Customers.Criteria.AutoTransfer;
using Xunit;

namespace Alpha.Customers.Tests.Criteria.AutoTransfer;

public class GetMembersCriteriaTests
{
    [Fact]
    public void GetSettingKey_ReturnsCorrectFormat()
    {
        // Arrange
        var criteria = new GetMembersCriteria
        {
            SiteDBGroupID = "5"
        };

        // Act
        var result = criteria.GetSettingKey();

        // Assert
        Assert.Equal("SiteDB5_Age_Transfer_SelList_Level2", result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(10, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void IsValid_ValidatesCustId(int custId, bool expected)
    {
        // Arrange
        var criteria = new GetMembersCriteria
        {
            CustId = custId
        };

        // Act
        var result = criteria.IsValid();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CustId_CanBeSetAndRetrieved()
    {
        // Arrange
        var criteria = new GetMembersCriteria();

        // Act
        criteria.CustId = 42;

        // Assert
        Assert.Equal(42, criteria.CustId);
    }
}
