using System.Collections.Generic;
using System.Runtime.Serialization;
using Alpha.Shared.Exceptions;
using Xunit;

namespace Alpha.Shared.Tests.Exceptions;

public class DomainExceptionTests
{
    [Fact]
    public void Constructor_WithMessageAndErrorCodeAndErrorData_SetsPropertiesCorrectly()
    {
        // Arrange
        var message = "Test error message";
        var errorCode = 123;
        var errorData = new Dictionary<string, object>
            {
                { "key1", "value1" },
                { "key2", 42 }
            };

        // Act
        var exception = new TestDomainException(message, errorCode, errorData);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal(errorCode, exception.ErrorCode);
        Assert.Same(errorData, exception.ErrorData);
    }

    // Helper class to test the protected constructors
    private class TestDomainException : DomainException
    {
        public TestDomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public TestDomainException(string message, int errorCode, IDictionary<string, object> errorData)
            : base(message, errorCode, errorData)
        {
        }
    }
}
