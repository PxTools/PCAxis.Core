using PCAxis.Paxiom.Parsers;

namespace PCAxis.Core.Tests;

[TestClass]
public class PXFileParserTests
{
    [TestMethod]
    public void ShouldReturnDescription()
    {
        // Arrange
        var parser = new PXFileParser();

        // Act

        var description = parser.Description.ToLower();

        // Assert
        Assert.AreEqual("this is the default plugin which reads a classical pc-axis file", description);

    }
}
