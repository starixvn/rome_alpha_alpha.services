using Alpha.Reports.Criteria.Outstanding;
using Alpha.Shared.Enumerations;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.Outstanding;

public class GetTopOutstandingCommandTests
{
    [Theory]
    [InlineData(4, "Age_Rpt_Outstanding_Master")]
    [InlineData(3, "Age_Rpt_Outstanding_Agent")]
    [InlineData(2, "Age_Rpt_Outstanding_Member")]
    [InlineData(1, "Age_Rpt_Outstanding_Member")]
    public void GetSettingKey_Always_ReturnsValidSettingKey(byte custLevel, string expected)
    {
        // Arrange & Act
        var criteria = new GetTopOutstandingCommand(
            1176070,
            Enumeration.FromValue<UserLevel>(custLevel),
            "1,2,3,4,5,6",
            50);

        // Assert
        Assert.Equal(expected, criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_CustIdId_ReturnsExpectedResult(int custId, bool expected)
    {
        // Arrange & Act
        var criteria = new GetTopOutstandingCommand(
            custId,
            UserLevel.Super,
            "1,2,3,4,5,6",
            50);

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange
        var criteria = new GetTopOutstandingCommand(
            1176070,
            UserLevel.Super,
            "1,2,3,4,5,6",
            50)
        {
            TotalMemberOutstanding = 12,
            TotalAgentOutstanding = 34,
            TotalMasterOutstanding = 56,
            TotalSuperOutstanding = 78
        };

        // Assert
        Assert.True("1,2,3,4,5,6" == criteria.ProductIds
            && criteria.IsChatBot
            && 50 == criteria.PageSize
            && 12 == criteria.TotalMemberOutstanding
            && 34 == criteria.TotalAgentOutstanding
            && 56 == criteria.TotalMasterOutstanding
            && 78 == criteria.TotalSuperOutstanding);
    }
}
