using System.Collections.Generic;
using Alpha.Reports.Configurations;
using Xunit;

namespace Alpha.Reports.Tests.Configurations;

public class MinimizeUMTests
{
    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange & Act
        var actual = new MinimizeUM(
            "3:00",
            "4:30",
            new Dictionary<string, bool>
            {
                ["WinLoss"] = true
            });

        // Assert
        Assert.True("3:00" == actual.StartTime
            && "4:30" == actual.EndTime
            && actual.ControllerNames["WinLoss"]);
    }
}