using Alpha.Reports.Criteria.Outstanding;
using Alpha.Shared.Enumerations;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.Outstanding;

public class GetDownlineOutstandingCriteriaTests
{
    [Theory]
    [InlineData(4, "Age_Rpt_DownlineOutstanding_Master")]
    [InlineData(3, "Age_Rpt_DownlineOutstanding_Agent")]
    [InlineData(2, "Age_Rpt_DownlineOutstanding_Member")]
    [InlineData(1, "Age_Rpt_DownlineOutstanding_Member")]
    public void GetSettingKey_Always_ReturnsValidSettingKey(byte custLevel, string expected)
    {
        // Arrange
        var criteria = new GetDownlineOutstandingCriteria(
            Enumeration.FromValue<UserLevel>(custLevel),
            "1,2,3,4,5,6",
            1176070);

        // Act & Assert
        Assert.Equal(expected, criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_DownlineId_ReturnsExpectedResult(int downlineId, bool expected)
    {
        // Arrange
        var criteria = new GetDownlineOutstandingCriteria(UserLevel.Super, "1,2,3,4,5,6", downlineId);

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange
        var criteria = new GetDownlineOutstandingCriteria(UserLevel.Super, "1,2,3,4,5,6", 1176070);

        // Assert
        Assert.Equal("1,2,3,4,5,6", criteria.ProductIds);
    }
}
