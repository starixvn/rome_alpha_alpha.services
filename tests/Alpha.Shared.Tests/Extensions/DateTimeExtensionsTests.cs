using System;
using Xunit;
using Alpha.Shared.Extensions;

namespace Alpha.Shared.Tests.Extensions;

public class DateTimeExtensionsTests
{
    [Fact]
    public void FirstDayOfMonth_ReturnsFirstDayOfMonth()
    {
        // Arrange
        var date = new DateTime(2023, 4, 15);
        var expected = new DateTime(2023, 4, 1);

        // Act
        var result = date.FirstDayOfMonth();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2023, 1, 31, "31/01/2023 12:00 AM")]
    [InlineData(2023, 6, 15, "15/06/2023 12:00 AM")]
    [InlineData(2023, 12, 25, "25/12/2023 12:00 AM")]
    public void ToLongDateTimeMinute_ReturnsFormattedString_WithoutTime(int year, int month, int day, string expected)
    {
        // Arrange
        var date = new DateTime(year, month, day);

        // Act
        var result = date.ToLongDateTimeMinute();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2023, 1, 31, 14, 30, "31/01/2023 02:30 PM")]
    [InlineData(2023, 6, 15, 9, 45, "15/06/2023 09:45 AM")]
    [InlineData(2023, 12, 25, 23, 59, "25/12/2023 11:59 PM")]
    public void ToLongDateTimeMinute_ReturnsFormattedString_WithTime(int year, int month, int day, int hour, int minute, string expected)
    {
        // Arrange
        var date = new DateTime(year, month, day, hour, minute, 0);

        // Act
        var result = date.ToLongDateTimeMinute();

        // Assert
        Assert.Equal(expected, result);
    }
}