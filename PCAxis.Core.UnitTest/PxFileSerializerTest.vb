Imports System.IO
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PCAxis.Paxiom


<TestClass>
Public Class PxFileSerializerTest
    <TestMethod>
    Sub ShouldBeAbleToSerialize()

        'Arrange
        Dim pxModel As New PXModel()

        Dim timeVar As New Variable("Period", PlacementType.Heading)
        timeVar.IsTime = True
        timeVar.Values.Add(New Value("2018M01"))

        pxModel.Meta.AddVariable(timeVar)

        Dim regionVar As New Variable("Region", PlacementType.Stub)
        regionVar.IsTime = False
        regionVar.Values.Add(New Value("A"))
        regionVar.Values.Add(New Value("B"))

        pxModel.Meta.AddVariable(regionVar)

        pxModel.Meta.AxisVersion = "2018"
        pxModel.Meta.Language = "en"
        pxModel.Meta.SubjectArea = "TST"
        pxModel.Meta.SubjectCode = "TST"
        pxModel.Meta.Matrix = "TST01"
        pxModel.Meta.Title = "Test data"
        pxModel.Meta.Source = "PxTools"
        pxModel.Meta.Contents = "Test data"
        pxModel.Meta.Decimals = 0
        pxModel.Meta.Description = "Test file"
        pxModel.Meta.DescriptionDefault = False

        Dim contentInfo As New ContInfo()
        contentInfo.Units = "Amount"
        pxModel.Meta.ContentInfo = contentInfo
        pxModel.IsComplete = True

        pxModel.Data.SetMatrixSize(1, 1)

        pxModel.Data.WriteElement(0, 100)

        'Act
        'Get a UTF-32 encoding by name.
        Dim serializer As PXFileSerializer = New PXFileSerializer()

        serializer.Serialize(pxModel, Stream.Null)

        'Assert
        Try
            serializer.Serialize(pxModel, Stream.Null)
            'Assert
            'If no exception is thrown, the test passes.
            Assert.IsTrue(True)
        Catch ex As Exception
            'If an exception is thrown, the test fails.
            Assert.Fail("Serialization threw an exception: " & ex.Message)
        End Try

    End Sub
End Class
