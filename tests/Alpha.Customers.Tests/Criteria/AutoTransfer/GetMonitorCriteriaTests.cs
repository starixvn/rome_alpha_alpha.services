using System;
using Alpha.Customers.Criteria.AutoTransfer;
using Xunit;

namespace Alpha.Customers.Tests.Criteria.AutoTransfer;

public class GetMonitorCriteriaTests
{
    [Fact]
    public void GetSettingKey_ReturnsCorrectKey()
    {
        // Arrange
        var criteria = new GetMonitorCriteria
        {
            SiteDBGroupID = "Test"
        };

        // Act
        var result = criteria.GetSettingKey();

        // Assert
        Assert.Equal("SiteDBTest_Age_ClearBalanceCust_ProcessSel", result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void IsValid_ChecksFromCustId_ReturnsExpectedResult(int fromCustId, bool expected)
    {
        // Arrange
        var criteria = new GetMonitorCriteria
        {
            FromCustId = fromCustId
        };

        // Act
        var result = criteria.IsValid();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var criteria = new GetMonitorCriteria();

        // Assert
        Assert.Equal(100000, criteria.PageSize);
        Assert.Equal(1, criteria.PageIndex);
    }

    [Fact]
    public void SpParamAttribute_IsSetOnSiteDBGroupID()
    {
        // Arrange
        var propertyInfo = typeof(GetMonitorCriteria).GetProperty("SiteDBGroupID");

        // Act
        var attribute = Attribute.GetCustomAttribute(
            propertyInfo,
            typeof(Fanex.Data.SpParamAttribute)) as Fanex.Data.SpParamAttribute;

        // Assert
        Assert.NotNull(attribute);
        Assert.True(attribute.Ignored);
    }
}