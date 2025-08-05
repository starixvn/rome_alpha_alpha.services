using Alpha.Reports.Criteria;
using Xunit;

namespace Alpha.Reports.Tests.Criteria;

public class GetOfferedProductsExtraCriteriaTest
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WhenCalled()
    {
        // Arrange
        int custId = 123;
        byte settingStatus = 1;
        string cashServiceSites = "site1,site2";

        // Act
        var criteria = new GetOfferedProductsExtraCriteria(custId, settingStatus, cashServiceSites);

        // Assert
        Assert.Equal(custId, criteria.CustId);
        Assert.Equal(settingStatus, criteria.SettingStatus);
        Assert.Equal(cashServiceSites, criteria.CashServiceSites);
    }

    [Fact]
    public void GetSettingKey_ShouldReturnCorrectKey_WhenCalled()
    {
        // Arrange
        var criteria = new GetOfferedProductsExtraCriteria(1, 1, "site1");

        // Act
        string settingKey = criteria.GetSettingKey();

        // Assert
        Assert.Equal("Age_GetOfferedProductByCust_Extra", settingKey);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void IsValid_ShouldReturnExpectedResult_WhenCustIdHasValue(int custId, bool expected)
    {
        // Arrange
        var criteria = new GetOfferedProductsExtraCriteria(custId, 1, "site1");

        // Act
        bool isValid = criteria.IsValid();

        // Assert
        Assert.Equal(expected, isValid);
    }
}