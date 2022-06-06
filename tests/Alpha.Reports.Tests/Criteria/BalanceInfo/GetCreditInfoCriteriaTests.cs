using Alpha.Reports.Criteria.BalanceInfo;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.BalanceInfo;

public class GetCreditInfoCriteriaTests
{
    [Fact]
    public void GetSettingKey_Always_ReturnsValidSettingKey()
    {
        // Arrange
        var criteria = new GetCreditInfoCriteria(1176070);

        // Act & Assert
        Assert.Equal("Age_Customer_CreditInfo_Sel", criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expected)
    {
        // Arrange
        var criteria = new GetCreditInfoCriteria(custId);

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }
}
