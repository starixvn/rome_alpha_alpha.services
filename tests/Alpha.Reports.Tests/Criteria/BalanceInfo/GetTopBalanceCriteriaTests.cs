using System;
using Alpha.Reports.Criteria.BalanceInfo;
using Alpha.Shared.Enumerations;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.BalanceInfo;

public class GetTopBalanceCriteriaTests
{
    [Theory]
    [InlineData(4, "Age_CustomerSelBalanceList_Master")]
    [InlineData(3, "Age_CustomerSelBalanceList_Agent")]
    [InlineData(2, "Age_CustomerSelBalanceList_Member")]
    [InlineData(1, "Age_CustomerSelBalanceList_Member")]
    public void GetSettingKey_Always_ReturnsValidSettingKey(byte custLevel, string expected)
    {
        // Arrange & Act
        var criteria = new GetTopBalanceCriteria(
            1176070,
            Enumeration.FromValue<UserLevel>(custLevel),
            50);

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
        var criteria = new GetTopBalanceCriteria(custId, UserLevel.Super, 50);

        // Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange & Act
        var criteria = new GetTopBalanceCriteria(1176070, UserLevel.Super, 10);

        // Assert
        Assert.True(10 == criteria.PageSize);
    }
}
