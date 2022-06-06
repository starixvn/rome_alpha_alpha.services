using Alpha.Shared.Enumerations;
using Xunit;

namespace Alpha.Shared.Tests.Enumerations;

public class UserLevelTests
{
    [Fact]
    public void GetDirectDownlineLevel_Super_ReturnsMaster()
    {
        // Arrange
        var custLevel = UserLevel.Super;

        // Act
        var actual = custLevel.GetDirectDownlineLevel();

        // Assert
        Assert.Equal(UserLevel.Master, actual);
    }

    [Fact]
    public void GetDirectDownlineLevel_Master_ReturnsAgent()
    {
        // Arrange
        var custLevel = UserLevel.Master;

        // Act
        var actual = custLevel.GetDirectDownlineLevel();

        // Assert
        Assert.Equal(UserLevel.Agent, actual);
    }

    [Fact]
    public void GetDirectDownlineLevel_Agent_ReturnsMember()
    {
        // Arrange
        var custLevel = UserLevel.Agent;

        // Act
        var actual = custLevel.GetDirectDownlineLevel();

        // Assert
        Assert.Equal(UserLevel.Member, actual);
    }
}