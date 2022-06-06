using System;
using Alpha.Shared.Extensions;
using Xunit;

namespace Alpha.Shared.Tests.Extensions;

public class DateTimeExtensionsTests
{
    [Fact]
    public void FirstDayOfMonth_Always_ReturnCorrectValue()
    {
        // Arrange
        var date = new DateTime(2022, 6, 10);

        // Act
        var actual = date.FirstDayOfMonth();

        // Assert
        Assert.True(
            1 == actual.Day
            && date.Month == actual.Month
            && date.Year == actual.Year);
    }
}