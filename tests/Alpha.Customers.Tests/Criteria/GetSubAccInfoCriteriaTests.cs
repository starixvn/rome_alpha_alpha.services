namespace Alpha.Customers.Tests.Criteria;

public class GetSubAccInfoCriteriaTests
{
    [Fact]
    public void GetSettingKey_Always_ReturnsValidSettingKey()
    {
        // Arrange
        var criteria = new GetSubAccInfoCriteria(1176070, 87413);

        // Act & Assert
        Assert.Equal("Age_CustSubSelInfo", criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expected)
    {
        // Arrange
        var criteria = new GetSubAccInfoCriteria(custId, 87413);

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-87413, false)]
    [InlineData(87413, true)]
    public void IsValid_SubAccId_ReturnsExpectedResult(int subAccId, bool expected)
    {
        // Arrange
        var criteria = new GetSubAccInfoCriteria(1176070, subAccId);

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }
}
