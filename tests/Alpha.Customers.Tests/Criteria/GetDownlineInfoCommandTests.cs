using Alpha.Customers.Criteria;
using Xunit;

namespace Alpha.Customers.Tests.Criteria;

public class GetDownlineInfoCommandTests
{
    [Fact]
    public void GetSettingKey_Always_ReturnsValidSettingKey()
    {
        // Arrange
        var criteria = new GetDownlineInfoCommand(1176070, 0, "master1");

        // Act & Assert
        Assert.Equal("Age_CustomerSelCheckBelonging_ByUsername", criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_UplineId_ReturnsExpectedResult(int uplineId, bool expected)
    {
        // Arrange
        var criteria = new GetDownlineInfoCommand(uplineId, 0, "master1");

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_SubAccountId_ReturnsExpectedResult(int subAccId, bool expected)
    {
        // Arrange
        var criteria = new GetDownlineInfoCommand(1176070, subAccId, "master1");

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("     ", false)]
    [InlineData("master1", true)]
    public void IsValid_Username_ReturnsExpectedResult(string username, bool expected)
    {
        // Arrange
        var criteria = new GetDownlineInfoCommand(1176070, 0, username);

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange
        var criteria = new GetDownlineInfoCommand(1176070, 1, "master1")
        {
            IsActive = true,
            CustId = 123456,
            IsSubAcc = true,
            RoleId = 2
        };

        // Act & Assert
        Assert.True(criteria.IsActive
            && 123456 == criteria.CustId
            && criteria.IsSubAcc
            && 2 == criteria.RoleId);
    }
}
