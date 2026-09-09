using Moq;
using PCAxis.Core.Tests.Fixtures;
using PCAxis.Paxiom;
using PCAxis.Paxiom.Parsers;
using System.Text;

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
    public void BuildForSelection_ShouldPreserveSwedishCharacters_FromIso88591Input()
    {
        var px = PxFileFixtures.OkFile.Replace("MATERIALÅTERVINNING", "Upplands Väsby");

        var path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.px");
        File.WriteAllText(path, px, Encoding.Latin1);

        try
        {
            var builder = new PXFileBuilder();
            builder.SetPath(path);

            var ok = builder.BuildForSelection();

            Assert.IsTrue(ok, "BuildForSelection failed for a valid ISO-8859-1 PX file.");

            var variable = builder.Model.Meta.Variables.FirstOrDefault(v => v.Code == "TREATMENT_CAT");
            Assert.IsNotNull(variable, "Variable with code 'TREATMENT_CAT' should exist.");
            Assert.IsTrue(variable.Values.Any(v => v.Value == "Upplands Väsby"), "Value 'Upplands Väsby' should be present.");
            Assert.IsTrue(builder.Model.Meta.Title.Contains("å"), "Swedish title should contain the character 'å'.");
        }
        finally
        {
            File.Delete(path);
        }

    }
}
