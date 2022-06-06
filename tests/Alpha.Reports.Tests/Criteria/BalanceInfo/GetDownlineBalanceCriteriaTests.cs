using Alpha.Reports.Criteria.BalanceInfo;
using Alpha.Shared.Enumerations;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.BalanceInfo;

public class GetDownlineBalanceCriteriaTests
{
    [Theory]
    [InlineData(3, "Age_BalanceMasterSel")]
    [InlineData(2, "Age_BalanceAgentSel")]
    [InlineData(1, "Age_BalanceMemberSel")]
    [InlineData(4, "Age_BalanceMemberSel")]
    public void GetSettingKey_Always_ReturnsValidSettingKey(byte custLevel, string expected)
    {
        // Arrange & Act
        var criteria = new GetDownlineBalanceCriteria(
            1176070,
            Enumeration.FromValue<UserLevel>(custLevel));

        // Assert
        Assert.Equal(expected, criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expected)
    {
        // Arrange & Act
        var criteria = new GetDownlineBalanceCriteria(custId, UserLevel.Super);

        // Assert
        Assert.Equal(expected, criteria.IsValid());
    }
}
