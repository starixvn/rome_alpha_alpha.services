using Alpha.Customers.Criteria;
using Xunit;

namespace Alpha.Customers.Tests.Criteria;

public class GetSiteDBConfigCriteriaTests
{
    [Fact]
    public void Constructor_ShouldSetCustId()
    {
        // Arrange
        const int expectedCustId = 123;

        // Act
        var criteria = new GetSiteDBConfigCriteria(expectedCustId);

        // Assert
        Assert.Equal(expectedCustId, criteria.CustId);
    }

    [Fact]
    public void GetSettingKey_ShouldReturnCorrectKey()
    {
        // Arrange
        var criteria = new GetSiteDBConfigCriteria(1);

        // Act
        var result = criteria.GetSettingKey();

        // Assert
        Assert.Equal("Acc_SiteDBConfigSel", result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(100, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void IsValid_ShouldReturnExpectedResult(int custId, bool expected)
    {
        // Arrange
        var criteria = new GetSiteDBConfigCriteria(custId);

        // Act
        var result = criteria.IsValid();

        // Assert
        Assert.Equal(expected, result);
    }
}