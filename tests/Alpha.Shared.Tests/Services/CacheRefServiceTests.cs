using Alpha.Shared.Services;
using Fanex.RemotingRef;
using NSubstitute;
using Xunit;

namespace Alpha.Shared.Tests.Services;

public class CacheRefServiceTests
{
    private readonly IRemotingRefService _remotingRefService;
    private readonly CacheRefService _service;

    public CacheRefServiceTests()
    {
        _remotingRefService = Substitute.For<IRemotingRefService>();
        _service = new CacheRefService(_remotingRefService);
    }

    [Fact]
    public void GetProductName_WhenBelonging777GroupAndSabaProductNameExists_ReturnsSabaProductName()
    {
        // Arrange
        const int productId = 123;
        const string languageCode = "en-US";
        const string sabaProductName = "Saba Product";

        _remotingRefService.GetResourceDataByRefIdAndResourceId("Product_Saba", "123", languageCode, "123")
            .Returns(sabaProductName);

        // Act
        var result = _service.GetProductName(productId, languageCode, true);

        // Assert
        Assert.Equal(sabaProductName, result);
    }

    [Fact]
    public void GetProductName_WhenBelonging777GroupAndSabaProductNameDoesNotExist_ReturnsRegularProductName()
    {
        // Arrange
        const int productId = 123;
        const string languageCode = "en-US";
        const string regularProductName = "Regular Product";

        _remotingRefService.GetResourceDataByRefIdAndResourceId("Product_Saba", "123", languageCode, "123")
            .Returns(string.Empty);
        _remotingRefService.GetResourceDataByRefIdAndResourceId("Product", "123", languageCode, "123")
            .Returns(regularProductName);

        // Act
        var result = _service.GetProductName(productId, languageCode, true);

        // Assert
        Assert.Equal(regularProductName, result);
    }

    [Fact]
    public void GetProductName_WhenNotBelonging777Group_ReturnsRegularProductName()
    {
        // Arrange
        const int productId = 123;
        const string languageCode = "en-US";
        const string regularProductName = "Regular Product";

        _remotingRefService.GetResourceDataByRefIdAndResourceId("Product", "123", languageCode, "123")
            .Returns(regularProductName);

        // Act
        var result = _service.GetProductName(productId, languageCode, false);

        // Assert
        Assert.Equal(regularProductName, result);
    }

    [Fact]
    public void GetResourceDataByRefIdAndResourceId_CallsRemotingRefService()
    {
        // Arrange
        const string refId = "Product";
        const string resourceId = "123";
        const string languageCode = "en-US";
        const string expectedResult = "Resource Data";

        _remotingRefService.GetResourceDataByRefIdAndResourceId(refId, resourceId, languageCode, resourceId)
            .Returns(expectedResult);

        // Act
        var result = _service.GetResourceDataByRefIdAndResourceId(refId, resourceId, languageCode);

        // Assert
        Assert.Equal(expectedResult, result);
        _remotingRefService.Received(1).GetResourceDataByRefIdAndResourceId(refId, resourceId, languageCode, resourceId);
    }

    [Fact]
    public void GetTeamName_CallsRemotingRefService()
    {
        // Arrange
        const int teamId = 456;
        const string languageCode = "en-US";
        const string expectedResult = "Team Name";

        _remotingRefService.GetTeamName("456", languageCode)
            .Returns(expectedResult);

        // Act
        var result = _service.GetTeamName(teamId, languageCode);

        // Assert
        Assert.Equal(expectedResult, result);
        _remotingRefService.Received(1).GetTeamName("456", languageCode);
    }

    [Fact]
    public void GetLeagueName_CallsRemotingRefService()
    {
        // Arrange
        const int leagueId = 789;
        const string languageCode = "en-US";
        const string expectedResult = "League Name";

        _remotingRefService.GetLeagueName("789", languageCode)
            .Returns(expectedResult);

        // Act
        var result = _service.GetLeagueName(leagueId, languageCode);

        // Assert
        Assert.Equal(expectedResult, result);
        _remotingRefService.Received(1).GetLeagueName("789", languageCode);
    }

    [Fact]
    public void GetBetTypeName_CallsRemotingRefService()
    {
        // Arrange
        const int betTypeId = 101;
        const string languageCode = "en-US";
        const string expectedResult = "Bet Type Name";

        _remotingRefService.GetBetTypeName("101", languageCode)
            .Returns(expectedResult);

        // Act
        var result = _service.GetBetTypeName(betTypeId, languageCode);

        // Assert
        Assert.Equal(expectedResult, result);
        _remotingRefService.Received(1).GetBetTypeName("101", languageCode);
    }
}
