using Alpha.Customers.Criteria.AutoTransfer;
using Fanex.Data.Repository;
using Xunit;

namespace Alpha.Customers.Tests.Criteria.AutoTransfer;

public class UpdateScheduleCriteriaTests
{
    [Fact]
    public void GetSettingKey_ShouldReturnCorrectKey()
    {
        // Arrange
        var criteria = new UpdateScheduleCriteria();

        // Act
        var result = criteria.GetSettingKey();

        // Assert
        Assert.Equal("Age_AutoTransferSchedule_Upd", result);
    }

    [Theory]
    [InlineData(1, 2, true)]
    [InlineData(100, 200, true)]
    [InlineData(0, 1, false)]
    [InlineData(1, 0, false)]
    [InlineData(0, 0, false)]
    [InlineData(-1, 5, false)]
    [InlineData(5, -1, false)]
    public void IsValid_WithDifferentCustomerIds_ReturnsExpectedResult(int fromCustId, int toCustId, bool expected)
    {
        // Arrange
        var criteria = new UpdateScheduleCriteria
        {
            FromCustId = fromCustId,
            ToCustId = toCustId
        };

        // Act
        var result = criteria.IsValid();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Creator_ShouldEqualFromCustId()
    {
        // Arrange
        var fromCustId = 123;
        var criteria = new UpdateScheduleCriteria
        {
            FromCustId = fromCustId
        };

        // Act
        var result = criteria.Creator;

        // Assert
        Assert.Equal(fromCustId, result);
    }

    [Fact]
    public void UpdateScheduleCriteria_ShouldInheritFromNonQueryCommand()
    {
        // Arrange & Act
        var criteria = new UpdateScheduleCriteria();

        // Assert
        Assert.IsAssignableFrom<NonQueryCommand>(criteria);
    }

    [Fact]
    public void Properties_ShouldHaveCorrectDefaultValues()
    {
        // Arrange & Act
        var criteria = new UpdateScheduleCriteria();

        // Assert
        Assert.Equal(0, criteria.FromCustId);
        Assert.Equal(0, criteria.ToCustId);
        Assert.False(criteria.IsAutoJobRunning);
        Assert.Null(criteria.AutoTransferSchedule);
        Assert.Equal(0, criteria.SubAccId);
        Assert.Null(criteria.Language);
        Assert.Null(criteria.IP);
        Assert.Null(criteria.Old_TransferOption);
        Assert.Equal(0, criteria.ErrCode);
        Assert.Null(criteria.ErrMsg);
    }
}
