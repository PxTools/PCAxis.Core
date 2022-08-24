Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PCAxis.Paxiom.Parsers

Namespace PCAxis.PX.Core.UnitTest
    <TestClass>
    Public Class PXFileBuilderTest
        <TestMethod>
        Sub ShouldReturnDescription()
            'Arrange
            Dim e As PXFileParser = New PXFileParser()

            'Act
            Dim testDesc As String = e.Description

            'Assert
            Assert.AreEqual(testDesc.ToLower(), "this is the default plugin which reads a classical pc-axis file")

        End Sub
    End Class
End Namespace

