using Alpha.Customers.Criteria;
using Xunit;

namespace Alpha.Customers.Tests.Criteria;

public class GetDomainsCriteriaTests
{
    [Fact]
    public void GetSettingKey_Always_ReturnsValidSettingKey()
    {
        // Arrange
        var criteria = new GetDomainsCriteria("BEST-ODDS");

        // Act & Assert
        Assert.Equal("Age_DomainListSel", criteria.GetSettingKey());
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("      ", false)]
    [InlineData("BEST-ODDS", true)]
    public void IsValid_Always_ReturnsCorrectValue(string site, bool expected)
    {
        // Arrange
        var criteria = new GetDomainsCriteria(site);

        // Act & Assert
        Assert.Equal(expected, criteria.IsValid());
    }
}
