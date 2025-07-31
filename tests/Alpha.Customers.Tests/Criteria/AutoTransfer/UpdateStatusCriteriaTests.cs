using Alpha.Customers.Criteria.AutoTransfer;
using Xunit;

namespace Alpha.Customers.Tests.Criteria.AutoTransfer;

public class UpdateStatusCriteriaTests
{
    [Fact]
    public void GetSettingKey_ShouldReturnCorrectValue()
    {
        // Arrange
        var criteria = new UpdateStatusCriteria();

        // Act
        var result = criteria.GetSettingKey();

        // Assert
        Assert.Equal("Age_ClearBalanceCust_SettingUpdate", result);
    }

    [Theory]
    [InlineData(1, 2, true)]
    [InlineData(100, 200, true)]
    [InlineData(0, 2, false)]
    [InlineData(1, 0, false)]
    [InlineData(0, 0, false)]
    [InlineData(-1, 2, false)]
    [InlineData(1, -2, false)]
    public void IsValid_ShouldReturnExpectedResult(int fromCustId, int toCustId, bool expected)
    {
        // Arrange
        var criteria = new UpdateStatusCriteria
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
    public void Creator_ShouldReturnFromCustId()
    {
        // Arrange
        var criteria = new UpdateStatusCriteria
        {
            FromCustId = 123
        };

        // Act
        var result = criteria.Creator;

        // Assert
        Assert.Equal(123, result);
    }

    [Fact]
    public void Properties_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var criteria = new UpdateStatusCriteria
        {
            FromCustId = 123,
            ToCustId = 456,
            IsAutoJobRunning = true,
            IsAllowAutoTransfer = false,
            AutoTransferSchedule = "* * * * *",
            SubAccId = 789,
            Language = "en-US",
            IP = "127.0.0.1",
            Old_TransferOption = "Option1",
            ErrCode = 0,
            ErrMsg = "No error"
        };

        // Assert
        Assert.Equal(123, criteria.FromCustId);
        Assert.Equal(456, criteria.ToCustId);
        Assert.True(criteria.IsAutoJobRunning);
        Assert.False(criteria.IsAllowAutoTransfer);
        Assert.Equal("* * * * *", criteria.AutoTransferSchedule);
        Assert.Equal(789, criteria.SubAccId);
        Assert.Equal("en-US", criteria.Language);
        Assert.Equal("127.0.0.1", criteria.IP);
        Assert.Equal("Option1", criteria.Old_TransferOption);
        Assert.Equal(0, criteria.ErrCode);
        Assert.Equal("No error", criteria.ErrMsg);
    }
}
