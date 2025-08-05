using System;
using Alpha.Reports.Criteria.WinLoss;
using Alpha.Shared.Enumerations;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.WinLoss;

public class GetMemberWinLossCriteriaTests
{
    [Fact]
    public void Constructor_ShouldSetAllProperties()
    {
        // Arrange
        int custId = 123;
        DateTime fromDate = new DateTime(2023, 1, 1);
        DateTime toDate = new DateTime(2023, 1, 31);
        string productIds = "1,2,3";
        DateTime moveBetsDay = new DateTime(2023, 1, 15);
        DateTime balanceUpDay = new DateTime(2023, 1, 20);
        UserLevel custLevel = UserLevel.Agent;

        // Act
        var criteria = new GetMemberWinLossCriteria(
            custId,
            fromDate,
            toDate,
            productIds,
            moveBetsDay,
            balanceUpDay,
            custLevel);

        // Assert
        Assert.Equal(custId, criteria.CustId);
        Assert.Equal(fromDate, criteria.FromDate);
        Assert.Equal(toDate, criteria.ToDate);
        Assert.Equal(productIds, criteria.ProductIds);
        Assert.Equal(moveBetsDay, criteria.MoveBetsDay);
        Assert.Equal(balanceUpDay, criteria.BalanceUpDay);
        Assert.Equal(custLevel, criteria.CustLevel);
    }

    [Fact]
    public void GetSettingKey_ShouldReturnCorrectKey()
    {
        // Arrange
        var criteria = new GetMemberWinLossCriteria(
            1,
            DateTime.Now,
            DateTime.Now,
            "1",
            DateTime.Now,
            DateTime.Now,
            UserLevel.Member);

        // Act
        string result = criteria.GetSettingKey();

        // Assert
        Assert.Equal("Age_Rpt_WinLoss_Member", result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void IsValid_WithDifferentCustIds_ReturnsExpectedResult(int custId, bool expected)
    {
        // Arrange
        DateTime date = DateTime.Now;
        var criteria = new GetMemberWinLossCriteria(
            custId,
            date,
            date,
            "1",
            date,
            date,
            UserLevel.Member);

        // Act
        bool result = criteria.IsValid();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2023-01-01", "2023-01-31", true)]
    [InlineData("2023-01-01", "2023-01-01", true)]
    [InlineData("2023-01-31", "2023-01-01", false)]
    public void IsValid_WithDifferentDates_ReturnsExpectedResult(string fromDateStr, string toDateStr, bool expected)
    {
        // Arrange
        DateTime fromDate = DateTime.Parse(fromDateStr);
        DateTime toDate = DateTime.Parse(toDateStr);
        var criteria = new GetMemberWinLossCriteria(
            1,
            fromDate,
            toDate,
            "1",
            DateTime.Now,
            DateTime.Now,
            UserLevel.Member);

        // Act
        bool result = criteria.IsValid();

        // Assert
        Assert.Equal(expected, result);
    }
}
