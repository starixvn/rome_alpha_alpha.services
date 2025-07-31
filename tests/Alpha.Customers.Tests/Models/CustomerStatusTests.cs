using Xunit;
using Alpha.Customers.Models;

namespace Alpha.Customers.Tests.Models;

public class CustomerStatusTests
{
    [Fact]
    public void IsSubAcc_WhenSubAccIdIsZero_ReturnsFalse()
    {
        // Arrange
        var status = new CustomerStatus(1, 0, true, false, false, "");

        // Act
        var result = status.IsSubAcc;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsSubAcc_WhenSubAccIdIsNotZero_ReturnsTrue()
    {
        // Arrange
        var status = new CustomerStatus(1, 2, true, false, false, "");

        // Act
        var result = status.IsSubAcc;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasReportPermission_WhenNotSubAcc_ReturnsTrue()
    {
        // Arrange
        var status = new CustomerStatus(1, 0, true, false, false, "");

        // Act
        var result = status.HasReportPermission;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasReportPermission_WhenSubAccWithReportPermission_ReturnsTrue()
    {
        // Arrange
        var status = new CustomerStatus(1, 2, true, false, false, "[E]");

        // Act
        var result = status.HasReportPermission;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasReportPermission_WhenSubAccWithoutReportPermission_ReturnsFalse()
    {
        // Arrange
        var status = new CustomerStatus(1, 2, true, false, false, "[A][B]");

        // Act
        var result = status.HasReportPermission;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasMemberInformationPermission_WhenNotSubAcc_ReturnsTrue()
    {
        // Arrange
        var status = new CustomerStatus(1, 0, true, false, false, "");

        // Act
        var result = status.HasMemberInformationPermission;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasMemberInformationPermission_WhenSubAccWithMemberInfoPermission_ReturnsTrue()
    {
        // Arrange
        var status = new CustomerStatus(1, 2, true, false, false, "[H]");

        // Act
        var result = status.HasMemberInformationPermission;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasMemberInformationPermission_WhenSubAccWithoutMemberInfoPermission_ReturnsFalse()
    {
        // Arrange
        var status = new CustomerStatus(1, 2, true, false, false, "[A][B]");

        // Act
        var result = status.HasMemberInformationPermission;

        // Assert
        Assert.False(result);
    }
}
