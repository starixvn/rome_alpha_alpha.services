using Alpha.Reports.Criteria.Outstanding;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.Outstanding;

public class GetCustomerOutstandingCriteriaTests
{
    [Fact]
    public void GetSettingKey_Always_ReturnsValidSettingKey()
    {
        // Arrange
        var criteria = new GetCustomerOutstandingCriteria(1176070);

        // Act & Assert
        Assert.Equal("Age_CustomerSelOutstanding", criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expected)
    {
        // Arrange
        var criteria = new GetCustomerOutstandingCriteria(custId);

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }
}
