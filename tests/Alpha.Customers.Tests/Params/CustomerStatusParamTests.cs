using Xunit;
using Alpha.Customers.Params;

namespace Alpha.Customers.Tests.Params;

public class CustomerStatusParamTests
{
    [Fact]
    public void Constructor_SetsProperties()
    {
        // Arrange
        int custId = 123;
        int subAccId = 456;

        // Act
        var param = new CustomerStatusParam(custId, subAccId);

        // Assert
        Assert.Equal(custId, param.CustId);
        Assert.Equal(subAccId, param.SubAccId);
    }

    [Fact]
    public void ToXml_ReturnsCorrectXmlString()
    {
        // Arrange
        int custId = 123;
        int subAccId = 456;
        var param = new CustomerStatusParam(custId, subAccId);
        string expected = $"<r id=\"{custId}\" sub=\"{subAccId}\"/>";

        // Act
        string result = param.ToXml();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 0, "<r id=\"0\" sub=\"0\"/>")]
    [InlineData(1, 2, "<r id=\"1\" sub=\"2\"/>")]
    [InlineData(-1, -2, "<r id=\"-1\" sub=\"-2\"/>")]
    public void ToXml_WithVariousInputs_ReturnsCorrectXmlString(int custId, int subAccId, string expected)
    {
        // Arrange
        var param = new CustomerStatusParam(custId, subAccId);

        // Act
        string result = param.ToXml();

        // Assert
        Assert.Equal(expected, result);
    }
}
