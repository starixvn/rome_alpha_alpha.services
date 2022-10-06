using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alpha.Reports.Configurations;
using Alpha.Reports.Services;
using Alpha.Shared.Exceptions;
using Fanex.Data.Repository;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Alpha.Reports.Tests.Services;

public class MinimizeUMServiceTests
{
    private readonly IDynamicRepository dynamicRepository;

    public MinimizeUMServiceTests()
        => dynamicRepository = Substitute.For<IDynamicRepository>();

    [Fact]
    public async Task ThrowIfUM_NotHaveUMConfig_ThrowsNothing()
    {
        // Arrange
        var custId = 1176070;
        var appSettings = new ReportAppSettings
        {
            MinimizeUM = Array.Empty<MinimizeUM>()
            // {
            //     new MinimizeUM
            //     {
            //         FunctionName = "Outstandings",
            //         Controllers = new[] { "Outstanding" }
            //     },
            //     new MinimizeUM
            //     {
            //         FunctionName = "Group Win Loss Reports",
            //         Controllers = new[] { "WinLoss" }
            //     }
            // }
        };
        var options = Options.Create(appSettings);
        var minimizeUMService = new MinimizeUMService(options, dynamicRepository);

        // Act
        var exception = await Record.ExceptionAsync(async () => await minimizeUMService.ThrowIfUM("Outstanding", custId));

        // Assert
        Assert.Null(exception);
    }
}