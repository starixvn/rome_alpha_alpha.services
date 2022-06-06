using Alpha.Customers.Criteria;
using Xunit;

namespace Alpha.Customers.Tests.Criteria;

public class GetCustomerStatusCriteriaTests
{
    [Fact]
    public void GetSettingKey_Always_ReturnsValidSettingKey()
    {
        // Arrange
        var custIds = "1,2,3,4,5,6";
        var criteria = new GetCustomerStatusCriteria(custIds);

        // Act & Assert
        Assert.Equal("Age_Chatbot_CustomerStatusSel", criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("     ", false)]
    [InlineData("1,2,3,4,5,6", true)]
    public void IsValid_CustList_ReturnsExpectedResult(string custIds, bool expected)
    {
        // Arrange
        var criteria = new GetCustomerStatusCriteria(custIds);

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }
}
