using Alpha.Shared.Services;
using Fanex.RemotingRef;
using NSubstitute;
using Xunit;

namespace Alpha.Shared.Tests.Services;

public class CacheRefServiceTests
{
    private readonly IRemotingRefService remotingRefService;
    private readonly ICacheRefService cacheRefService;

    public CacheRefServiceTests()
    {
        remotingRefService = Substitute.For<IRemotingRefService>();
        cacheRefService = new CacheRefService(remotingRefService);
    }
    
    [Fact]
    public void GetProductName_Always_ReturnCorrectProductName()
    {
        // Arrange
        var productId = 1;
        var language = "en-US";
        var productName = "Sportsbook";
        remotingRefService.GetResourceDataByRefIdAndResourceId(
            "Product",
            productId.ToString(),
            language,
            productId.ToString()).Returns(productName);

        // Act
        var actual = cacheRefService.GetProductName(productId, language);

        // Assert
        Assert.Equal(productName, actual);
    }
}