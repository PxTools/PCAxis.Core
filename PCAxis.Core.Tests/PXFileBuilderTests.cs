using Moq;
using PCAxis.Core.Tests.Fixtures;
using PCAxis.Paxiom;
using PCAxis.Paxiom.Parsers;

namespace PCAxis.Core.Tests;

[TestClass]
public class PXFileBuilderTests
{
    [TestMethod]
    public void Reading_a_valid_px_file_should_not_throw_exception()
    {
        // Arrange
        var parser = new Utils.PxFileParserProxy(PxFileFixtures.OkFile);
        var parserFactory = new Mock<IPXFileParserFactory>();
        parserFactory.Setup(f => f.Create()).Returns(parser);


        try
        {
            var builder = new PXFileBuilder(parserFactory.Object);
            builder.SetPath("Dummy path");
            builder.BuildForSelection();
        }
        catch (Exception ex)
        {
            Assert.Fail($"An exception was thrown while reading a valid PX file: {ex.Message}");
        }

    }

    [TestMethod]
    public void BuildForSelection_should_preserve_encoding()
    {
        // Arrange
        var fixturePath = Path.Combine(AppContext.BaseDirectory, "Test_files", "TAB003.px");
        
        // Act
        // The fixture declares CODEPAGE="iso-8859-1".
        var builder = new PXFileBuilder();
        builder.SetPath(fixturePath);
            
        var result = builder.BuildForSelection();

        // Assert
        Assert.IsTrue(result, "BuildForSelection should succeed.");

        var title = builder.Model.Meta.Title;

        StringAssert.Contains(
            title,
            "efter behandlingstyp");

        StringAssert.Contains(title, "år");
        
    }
}
