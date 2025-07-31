using Alpha.Customers.Criteria.AutoTransfer;
using Xunit;

namespace Alpha.Customers.Tests.Criteria.AutoTransfer;

public class GetIsAllDayBalTransferCriteriaTests
{
    [Fact]
    public void Constructor_SetsProperties()
    {
        // Arrange
        int custId = 123;

        // Act
        var criteria = new GetIsAllDayBalTransferCriteria(custId);

        // Assert
        Assert.Equal(custId, criteria.CustId);
    }

    [Fact]
    public void GetSettingKey_ReturnsCorrectKey()
    {
        // Arrange
        var criteria = new GetIsAllDayBalTransferCriteria(1);

        // Act
        string key = criteria.GetSettingKey();

        // Assert
        Assert.Equal("Age_GetIsAllDayBalTransfer", key);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(100, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void IsValid_ValidatesCustomerId(int custId, bool expectedResult)
    {
        // Arrange
        var criteria = new GetIsAllDayBalTransferCriteria(custId);

        // Act
        bool isValid = criteria.IsValid();

        // Assert
        Assert.Equal(expectedResult, isValid);
    }
}
