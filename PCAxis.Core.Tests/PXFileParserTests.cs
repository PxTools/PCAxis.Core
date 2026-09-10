using PCAxis.Paxiom.Parsers;
using System.Text;

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
        var fixturePath = Path.Combine(AppContext.BaseDirectory, "Test_files", "Alias_en.txt");

        // Act
        var encoding = PXFileParser.GetEncoding(fixturePath);

        // Assert
        Assert.IsNotNull(encoding);
        Assert.AreEqual(Encoding.Default, encoding);
    }

    [TestMethod]
    public void GetEncoding_With_Path_Should_Return_Latin1_From_CodePage()
    {
        // Latin1 = iso-8859-1

        // Arrange
        var fixturePath = Path.Combine(AppContext.BaseDirectory, "Test_files", "TAB003.px");

        // Act
        var encoding = PXFileParser.GetEncoding(fixturePath);

        // Assert
        Assert.IsNotNull(encoding);
        Assert.AreEqual(Encoding.Latin1, encoding);
    }

    [TestMethod]
    public void SetPath_Should_Set_Encoding_To_Latin1()
    {
        // Arrange
        var fixturePath = Path.Combine(AppContext.BaseDirectory, "Test_files", "TAB003.px");
        var parser = new PXFileParser();

        // Act
        parser.SetPath(fixturePath);

        var field = typeof(PXFileParser).GetField("_encoding",
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        var encoding = (Encoding?)field?.GetValue(parser);

        // Assert
        Assert.IsNotNull(field, "Could not find private field _encoding.");
        Assert.IsNotNull(encoding);
        Assert.AreEqual(Encoding.Latin1, encoding);
    }
}
