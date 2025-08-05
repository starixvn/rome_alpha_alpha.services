using System.Collections.Generic;
using Alpha.Customers.Helpers;
using Alpha.Shared.Enumerations;
using Xunit;

namespace Alpha.Customers.Tests.Helpers;

public class TransferConditionHelperTests
{
    [Fact]
    public void GetAutoTransferScheduleFromTransferOption_WhenUserIsSuperAdmin_ReturnsAllDays()
    {
        // Arrange
        var transferOption = "any_value";
        var roleId = UserLevel.Super.Value;
        var expected = new List<int> { 2, 3, 4, 5, 6, 7, 1 };

        // Act
        var result = TransferConditionHelper.GetAutoTransferScheduleFromTransferOption(transferOption, roleId);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GetAutoTransferScheduleFromTransferOption_WhenTransferOptionIsNullOrEmpty_ReturnsEmptyList(string transferOption)
    {
        // Arrange
        var roleId = 0; // Any role other than Super

        // Act
        var result = TransferConditionHelper.GetAutoTransferScheduleFromTransferOption(transferOption, roleId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetAutoTransferScheduleFromTransferOption_WhenTransferOptionIsZeroLength_ReturnsEmptyList()
    {
        // Arrange
        var transferOption = "";
        var roleId = 0; // Any role other than Super

        // Act
        var result = TransferConditionHelper.GetAutoTransferScheduleFromTransferOption(transferOption, roleId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetAutoTransferScheduleFromTransferOption_WhenDailyOption_ReturnsAllDays()
    {
        // Arrange
        // 8th character (index 7) is '0' which means daily
        var transferOption = "12345600";
        var roleId = 0; // Any role other than Super
        var expected = new List<int> { 2, 3, 4, 5, 6, 7, 1 };

        // Act
        var result = TransferConditionHelper.GetAutoTransferScheduleFromTransferOption(transferOption, roleId);

        // Assert
        Assert.Equal(expected, result);
    }
}
