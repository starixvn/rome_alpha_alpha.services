using Alpha.Reports.Configurations;
using Xunit;

namespace Alpha.Reports.Tests.Configurations;

public class MinimizeUMTests
{
    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange & Act
        var actual = new MinimizeUM
        {
            FunctionName = "Outstandings",
            Controllers = new[] { "Outstanding" }
        };

        // Assert
        Assert.True("Outstandings" == actual.FunctionName
            && "Outstanding" == actual.Controllers[0]);
    }
}