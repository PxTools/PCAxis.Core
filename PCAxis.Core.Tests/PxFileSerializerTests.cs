using PCAxis.Paxiom;

namespace PCAxis.Core.Tests;

[TestClass]
public class PxFileSerializerTests
{
    [TestMethod]
    public void ShouldBeAbleToSerialize()
    {
        // Arrange
        var model = new PXModel();

        var timeVar = new Variable("Period", PlacementType.Heading);
        timeVar.IsTime = true;
        timeVar.Values.Add(new Value("2018M01"));

        model.Meta.AddVariable(timeVar);

        var regionVar = new Variable("Region", PlacementType.Stub);
        regionVar.IsTime = false;
        regionVar.Values.Add(new Value("A"));
        regionVar.Values.Add(new Value("B"));

        model.Meta.AddVariable(regionVar);

        model.Meta.AxisVersion = "2018";
        model.Meta.Language = "en";
        model.Meta.SubjectArea = "TST";
        model.Meta.SubjectCode = "TST";
        model.Meta.Matrix = "TST01";
        model.Meta.Title = "Test data";
        model.Meta.Source = "PxTools";
        model.Meta.Contents = "Test data";
        model.Meta.Decimals = 0;
        model.Meta.Description = "Test file";
        model.Meta.DescriptionDefault = false;

        var contentInfo = new ContInfo();
        contentInfo.Units = "Amount";
        model.Meta.ContentInfo = contentInfo;
        model.IsComplete = true;

        model.Data.SetMatrixSize(1, 1);

        model.Data.WriteElement(0, 100);

        // Act
        // Get a UTF-32 encoding by name.
        var serializer = new PXFileSerializer();

        //Assert
        try
        {
            serializer.Serialize(model, Stream.Null);
        }
        catch (Exception ex)
        {
            //If an exception is thrown, the test fails.
            Assert.Fail("Serialization threw an exception: " + ex.Message);
        }
    }
}
