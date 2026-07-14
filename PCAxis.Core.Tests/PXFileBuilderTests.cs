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
}
