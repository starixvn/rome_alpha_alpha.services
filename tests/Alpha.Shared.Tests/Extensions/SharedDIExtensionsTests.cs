using Microsoft.Extensions.DependencyInjection;
using Alpha.Shared.Services;
using Xunit;

namespace Alpha.Shared.Extensions.Tests;

public class SharedDIExtensionsTests
{
    [Fact]
    public void AddSharedServices_RegistersCacheRefServiceAsSingleton()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddSharedServices();

        // Assert
        var serviceDescriptor = Assert.Single(services);
        Assert.Equal(typeof(ICacheRefService), serviceDescriptor.ServiceType);
        Assert.Equal(typeof(CacheRefService), serviceDescriptor.ImplementationType);
        Assert.Equal(ServiceLifetime.Singleton, serviceDescriptor.Lifetime);
    }

    [Fact]
    public void AddSharedServices_ReturnsSameServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddSharedServices();

        // Assert
        Assert.Same(services, result);
    }
}
