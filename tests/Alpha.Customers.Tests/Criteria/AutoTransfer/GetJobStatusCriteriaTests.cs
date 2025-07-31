using Alpha.Customers.Criteria.AutoTransfer;
using Xunit;

namespace Alpha.Customers.Tests.Criteria.AutoTransfer;

public class GetJobStatusCriteriaTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        int fromCustId = 123;
        int toCustId = 456;
        bool isAllowAutoTransfer = true;
        int siteDB = 1;

        // Act
        var criteria = new GetJobStatusCriteria(fromCustId, toCustId, isAllowAutoTransfer, siteDB);

        // Assert
        Assert.Equal(fromCustId, criteria.FromCustId);
        Assert.Equal(toCustId, criteria.ToCustId);
        Assert.Equal(isAllowAutoTransfer, criteria.IsAllowAutoTransfer);
        Assert.Equal("1", criteria.SiteDBGroupID);
        Assert.Equal(0, criteria.ErrCode);
        Assert.Equal(string.Empty, criteria.ErrMsg);
        Assert.False(criteria.IsAutoJobRunning);
    }

    [Fact]
    public void GetSettingKey_ShouldReturnCorrectKey()
    {
        // Arrange
        var criteria = new GetJobStatusCriteria(1, 2, true, 3);

        // Act
        string key = criteria.GetSettingKey();

        // Assert
        Assert.Equal("SiteDB3_Age_ClearBalanceCust_Job_StatusCheck", key);
    }

    [Fact]
    public void IsValid_ShouldAlwaysReturnTrue()
    {
        // Arrange
        var criteria = new GetJobStatusCriteria(1, 2, false, 4);

        // Act
        bool isValid = criteria.IsValid();

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void SiteDBGroupID_ShouldBeConvertedToString()
    {
        // Arrange
        int siteDB = 5;

        // Act
        var criteria = new GetJobStatusCriteria(1, 2, true, siteDB);

        // Assert
        Assert.Equal("5", criteria.SiteDBGroupID);
    }
}
