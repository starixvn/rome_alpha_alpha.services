using Alpha.Reports.Configurations;
using Xunit;

namespace Alpha.Reports.Tests.Configurations;

public class GroupFunctionTests
{
    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange & Act
        var actual = new GroupFunction
        {
            Name = "WinLoss Group",
            Controllers = new[] { "WinLoss" },
            IsActive = true
        };

        // Assert
        Assert.True("WinLoss Group" == actual.Name
            && "WinLoss" == actual.Controllers[0]
            && actual.IsActive);
    }
}