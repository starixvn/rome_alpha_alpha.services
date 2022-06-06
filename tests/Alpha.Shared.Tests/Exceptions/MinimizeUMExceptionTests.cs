using Alpha.Shared.Exceptions;
using Xunit;

namespace Alpha.Shared.Tests.Exceptions;

public class MinimizeUMExceptionTests
{
    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange
        var startTime = "3:00";
        var endTime = "5:00";

        // Act
        var exception = new MinimizeUMException(startTime, endTime);

        // Assert
        Assert.True(
            1001 == exception.ErrorCode
            && $"Sorry, the system is under maintenance from {startTime} to {endTime} GMT+8. Please come back later" == exception.Message
            && startTime == exception.ErrorData[nameof(startTime)].ToString()
            && endTime == exception.ErrorData[nameof(endTime)].ToString());
    }
}