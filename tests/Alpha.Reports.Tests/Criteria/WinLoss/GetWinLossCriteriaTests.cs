using System;
using Alpha.Reports.Criteria.WinLoss;
using Alpha.Shared.Enumerations;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.WinLoss;

public class GetWinLossCriteriaTests
{
    [Theory]
    [InlineData(4, "Age_Rpt_WinLoss_Master")]
    [InlineData(3, "Age_Rpt_WinLoss_Agent")]
    [InlineData(2, "Age_Rpt_WinLoss_Member")]
    [InlineData(1, "Age_Rpt_WinLoss_Member")]
    public void GetSettingKey_Always_ReturnsValidSettingKey(byte custLevel, string expected)
    {
        // Arrange
        var today = DateTime.Today;

        // Act
        var criteria = new GetWinLossCriteria(
            1176070,
            today,
            today,
            "1,2,3,4,5,6",
            today,
            today,
            Enumeration.FromValue<UserLevel>(custLevel),
            null);

        // Assert
        Assert.Equal(expected, criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expected)
    {
        // Arrange
        var today = DateTime.Today;

        // Act
        var criteria = new GetWinLossCriteria(
            custId,
            today,
            today,
            "1,2,3,4,5,6",
            today,
            today,
            UserLevel.Master,
            null);

        // Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Fact]
    public void IsValid_FromLessThanTo_ReturnsTrue()
    {
        // Arrange
        var today = DateTime.Today;

        // Act
        var criteria = new GetWinLossCriteria(
            1176070,
            today.AddDays(-1),
            today,
            "1,2,3,4,5,6",
            today,
            today,
            UserLevel.Master,
            null);

        // Assert
        Assert.True(criteria.IsValid());
    }

    [Fact]
    public void IsValid_FromEqualTo_ReturnsTrue()
    {
        // Arrange
        var today = DateTime.Today;

        // Act
        var criteria = new GetWinLossCriteria(
            1176070,
            today,
            today,
            "1,2,3,4,5,6",
            today,
            today,
            UserLevel.Master,
            null);

        // Assert
        Assert.True(criteria.IsValid());
    }

    [Fact]
    public void IsValid_FromGreaterTo_ReturnsFalse()
    {
        // Arrange
        var today = DateTime.Today;

        // Act
        var criteria = new GetWinLossCriteria(
            1176070,
            today.AddDays(1),
            today,
            "1,2,3,4,5,6",
            today,
            today,
            UserLevel.Master,
            null);

        // Assert
        Assert.False(criteria.IsValid());
    }

    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange
        var today = DateTime.Today;

        // Act
        var criteria = new GetWinLossCriteria(
            1176070,
            today,
            today,
            "1,2,3,4,5,6",
            today,
            today,
            UserLevel.Master,
            null);

        // Assert
        Assert.True("1,2,3,4,5,6" == criteria.ProductIds
            && today == criteria.MoveBetsDay
            && today == criteria.BalanceUpDay);
    }
}
