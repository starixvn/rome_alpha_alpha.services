using Alpha.Shared.Exceptions;
using Alpha.Shared.Models;
using Xunit;

namespace Alpha.Shared.Tests.Models;

public class ApiErrorTests
{
    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange
        var startTime = "3:00";
        var endTime = "5:00";
        var exception = new MinimizeUMException(startTime, endTime);

        // Act
        var apiError = new ApiError(exception);

        // Assert
        Assert.True(
            exception.ErrorCode == apiError.Code
            && exception.Message == apiError.Message
            && exception.ErrorData == apiError.Data);
    }
}