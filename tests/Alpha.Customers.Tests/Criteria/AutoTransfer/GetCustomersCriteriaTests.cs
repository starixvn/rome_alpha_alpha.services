using System;
using Xunit;
using Alpha.Customers.Criteria.AutoTransfer;
using Fanex.Data;

namespace Alpha.Customers.Tests.Criteria.AutoTransfer;

public class GetCustomersCriteriaTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Act
        var criteria = new GetCustomersCriteria();

        // Assert
        Assert.Null(criteria.SiteDBGroupID);
        Assert.Equal(0, criteria.PageSize);
        Assert.Equal(0, criteria.PageIndex);
        Assert.Null(criteria.Username);
        Assert.Null(criteria.CustStatus);
        Assert.Null(criteria.AutoTransfer);
        Assert.Equal(0, criteria.TotalPages);
        Assert.False(criteria.IsYesterdayBalance);
        Assert.True(criteria.IsAllowTotalBalance);
    }

    [Fact]
    public void GetSettingKey_ShouldReturnEmptyString()
    {
        // Arrange
        var criteria = new GetCustomersCriteria();

        // Act
        var result = criteria.GetSettingKey();

        // Assert
        Assert.Equal("", result);
    }

    [Fact]
    public void IsValid_ShouldReturnTrue()
    {
        // Arrange
        var criteria = new GetCustomersCriteria();

        // Act
        var result = criteria.IsValid();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void SiteDBGroupID_ShouldHaveSpParamIgnoredAttribute()
    {
        // Arrange
        var propertyInfo = typeof(GetCustomersCriteria).GetProperty("SiteDBGroupID");

        // Act
        var attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(SpParamAttribute)) as SpParamAttribute;

        // Assert
        Assert.NotNull(attribute);
        Assert.True(attribute.Ignored);
    }
}
