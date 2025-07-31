using Alpha.Customers.Models;
using Xunit;

namespace Alpha.Customers.Tests.Models;

public class ResultTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var result = new Result(42, "Test message");

        // Assert
        Assert.Equal(42, result.Code);
        Assert.Equal("Test message", result.Message);
    }

    [Fact]
    public void IsOk_ReturnsTrueWhenCodeIsZero()
    {
        // Arrange
        var result = new Result(0);

        // Act & Assert
        Assert.True(result.IsOk);
        Assert.False(result.IsNotOk);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    public void IsOk_ReturnsFalseWhenCodeIsNotZero(int code)
    {
        // Arrange
        var result = new Result(code);

        // Act & Assert
        Assert.False(result.IsOk);
        Assert.True(result.IsNotOk);
    }

    [Fact]
    public void CreateSuccessfulResult_ReturnsResultWithCodeZero()
    {
        // Arrange & Act
        var result = Result.CreateSuccessfulResult("Success");

        // Assert
        Assert.Equal(0, result.Code);
        Assert.Equal("Success", result.Message);
        Assert.True(result.IsOk);
        Assert.False(result.IsNotOk);
    }

    [Fact]
    public void CreateSuccessfulResult_WithoutMessage_ReturnsResultWithEmptyMessage()
    {
        // Arrange & Act
        var result = Result.CreateSuccessfulResult();

        // Assert
        Assert.Equal(0, result.Code);
        Assert.Equal("", result.Message);
        Assert.True(result.IsOk);
    }

    [Fact]
    public void CreateErrorResult_ReturnsResultWithCodeOne()
    {
        // Arrange & Act
        var result = Result.CreateErrorResult("Error");

        // Assert
        Assert.Equal(1, result.Code);
        Assert.Equal("Error", result.Message);
        Assert.False(result.IsOk);
        Assert.True(result.IsNotOk);
    }

    [Fact]
    public void CreateErrorResult_WithoutMessage_ReturnsResultWithEmptyMessage()
    {
        // Arrange & Act
        var result = Result.CreateErrorResult();

        // Assert
        Assert.Equal(1, result.Code);
        Assert.Equal("", result.Message);
        Assert.False(result.IsOk);
        Assert.True(result.IsNotOk);
    }
}
