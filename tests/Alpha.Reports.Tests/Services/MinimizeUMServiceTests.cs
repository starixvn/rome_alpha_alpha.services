using System.Collections.Generic;
using Alpha.Reports.Configurations;
using Alpha.Reports.Services;
using Alpha.Shared.Exceptions;
using Xunit;

namespace Alpha.Reports.Tests.Services;

public class MinimizeUMServiceTests
{
    [Fact]
    public void ThrowIfUM_ContainsKey_ThrowsMinimizeUMException()
    {
        // Arrange
        var minimizeUM = new MinimizeUM(
            "3:00",
            "4:30",
            new Dictionary<string, bool>
            {
                ["Outstanding"] = true,
                ["WinLoss"] = true
            });
        IMinimizeUMService minimizeUMService = new MinimizeUMService(minimizeUM);

        // Act & Assert
        Assert.Throws<MinimizeUMException>(() => minimizeUMService.ThrowIfUM("Outstanding"));
    }
}