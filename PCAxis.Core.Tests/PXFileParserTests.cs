using PCAxis.Paxiom.Parsers;
using System.IO;

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

    [TestMethod]
    public void GetEncoding_ShouldReadAliasFile()
    {
        // Arrange
        var fixturePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Test_files", "Alias_en.txt"));

        // Act
        var encoding = PXFileParser.GetEncoding(fixturePath);

        // Assert
        Assert.IsNotNull(encoding);
    }
}
