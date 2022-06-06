using System.Collections.Generic;
using Alpha.Shared.Extensions;
using Xunit;

namespace Alpha.Shared.Tests.Extensions;

public class EnumerableExtensionsTests
{
    [Fact]
    public void JoinWithComma_NullList_ReturnEmpty()
    {
        // Arrange
        List<string> list = null;

        // Act
        var actual = list.JoinWithComma();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public void JoinWithComma_NotNullList_ReturnCorrectValue()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3, 4, 5 };

        // Act
        var actual = list.JoinWithComma();

        // Assert
        Assert.Equal("1,2,3,4,5", actual);
    }
}