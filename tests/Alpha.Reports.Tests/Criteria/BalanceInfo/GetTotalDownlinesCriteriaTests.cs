using Alpha.Reports.Criteria.BalanceInfo;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.BalanceInfo;

public class GetTotalDownlinesCriteriaTests
{
    [Fact]
    public void GetSettingKey_Always_ReturnsValidSettingKey()
    {
        // Arrange & Act
        var criteria = new GetTotalDownlinesCriteria(1176070, 4);

        // Assert
        Assert.Equal("Acc_CustomerSelTotalDownline", criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expected)
    {
        // Arrange & Act
        var criteria = new GetTotalDownlinesCriteria(custId, 4);

        // Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange & Act
        var criteria = new GetTotalDownlinesCriteria(1176070, 3);

        // Assert
        Assert.True(3 == criteria.RoleId);
    }
}
