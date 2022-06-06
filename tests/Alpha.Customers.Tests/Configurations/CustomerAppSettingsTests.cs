using Alpha.Customers.Configurations;
using Xunit;

namespace Alpha.Customers.Tests.Configurations;

public class CustomerAppSettingsTests
{
    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange
        var expected = 120;

        // Act
        var actual = new CustomerAppSettings
        {
            CustomerStatusCacheInSeconds = expected
        };

        // Assert
        Assert.Equal(expected, actual.CustomerStatusCacheInSeconds);
    }
}
