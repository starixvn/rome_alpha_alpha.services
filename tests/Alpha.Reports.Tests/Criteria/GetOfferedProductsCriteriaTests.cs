using Alpha.Reports.Criteria;
using Xunit;

namespace Alpha.Reports.Tests.Criteria;

public class GetOfferedProductsCriteriaTests
{
    [Fact]
    public void GetSettingKey_Always_ReturnsValidSettingKey()
    {
        // Arrange
        var criteria = new GetOfferedProductsCriteria(1176070, 0);

        // Act & Assert
        Assert.Equal("Age_GetOfferedProductByCust", criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expected)
    {
        // Arrange
        var criteria = new GetOfferedProductsCriteria(custId, 1);

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange & Act
        var criteria = new GetOfferedProductsCriteria(1176070, 1);

        // Assert
        Assert.Equal(1, criteria.SettingStatus);
    }
}
