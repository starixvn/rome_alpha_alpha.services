using Alpha.Reports.Criteria.BalanceInfo;
using Xunit;

namespace Alpha.Reports.Tests.Criteria.BalanceInfo;

public class GetMemberBalanceCommandTests
{
    [Fact]
    public void GetSettingKey_Always_ReturnsValidSettingKey()
    {
        // Arrange & Act
        var criteria = new GetMemberBalanceCommand(1176070);

        // Assert
        Assert.Equal("SiteDB_Acc_BalanceMemberSel", criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(-1176070, false)]
    [InlineData(1176070, true)]
    public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expected)
    {
        // Arrange & Act
        var criteria = new GetMemberBalanceCommand(custId);

        // Assert
        Assert.Equal(expected, criteria.IsValid());
    }

    [Fact]
    public void Constructor_Assigns_CorrectPropertyValues()
    {
        // Arrange & Act
        var criteria = new GetMemberBalanceCommand(1176070)
        {
            Balance = 12,
            NetPosition = 34,
            PendingTransfer3rd = 56
        };

        // Assert
        Assert.True(12 == criteria.Balance
            && 34 == criteria.NetPosition
            && 56 == criteria.PendingTransfer3rd);
    }
}
