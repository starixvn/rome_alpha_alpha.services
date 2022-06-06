using System;
using Alpha.Reports.Criteria.BalanceInfo;
using Alpha.Shared.Enumerations;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.BalanceInfo;

public class GetCustomerBalanceCriteriaTests
{
    [Theory]
    [InlineData(4, "Age_CustomerSelBalanceInfo_SAg")]
    [InlineData(3, "Age_CustomerSelBalanceInfo_MAg")]
    [InlineData(2, "Age_CustomerSelBalanceInfo_Ag")]
    [InlineData(1, "Age_CustomerSelBalanceInfo_Ag")]
    public void GetSettingKey_Always_ReturnsValidSettingKey(byte custLevel, string expected)
    {
        // Arrange & Act
        var criteria = new GetCustomerBalanceCriteria(
            1176070,
            Enumeration.FromValue<UserLevel>(custLevel),
            DateTime.Today);

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
        var criteria = new GetCustomerBalanceCriteria(
            custId,
            UserLevel.Super,
            DateTime.Today);

        // Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange
        var currentDate = DateTime.Today;

        // Act
        var criteria = new GetCustomerBalanceCriteria(
            1176070,
            UserLevel.Agent,
            currentDate);

        // Assert
        Assert.True(1176070 == criteria.CustId
            && currentDate == criteria.MoveBetsDay);
    }
}
