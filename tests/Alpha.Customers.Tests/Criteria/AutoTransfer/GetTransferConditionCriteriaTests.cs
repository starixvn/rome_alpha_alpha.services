using Alpha.Customers.Criteria.AutoTransfer;
using Xunit;

namespace Alpha.Customers.Tests.Criteria.AutoTransfer;

public class GetTransferConditionCriteriaTests
{
    [Fact]
    public void Constructor_ShouldSetCustId()
    {
        // Arrange
        int expectedCustId = 123;

        // Act
        var criteria = new GetTransferConditionCriteria(expectedCustId);

        // Assert
        Assert.Equal(expectedCustId, criteria.CustId);
    }

    [Fact]
    public void GetSettingKey_ShouldReturnCorrectKey()
    {
        // Arrange
        var criteria = new GetTransferConditionCriteria(1);

        // Act
        string result = criteria.GetSettingKey();

        // Assert
        Assert.Equal("Acc_CustomerSelTransferOption", result);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(100, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void IsValid_ShouldReturnExpectedResult(int custId, bool expectedResult)
    {
        // Arrange
        var criteria = new GetTransferConditionCriteria(custId);

        // Act
        bool result = criteria.IsValid();

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void TransferOption_CanBeSetAndRetrieved()
    {
        // Arrange
        var criteria = new GetTransferConditionCriteria(1);
        string expectedOption = "Bank";

        // Act
        criteria.TransferOption = expectedOption;

        // Assert
        Assert.Equal(expectedOption, criteria.TransferOption);
    }
}
