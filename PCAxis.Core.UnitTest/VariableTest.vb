Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PCAxis.Paxiom

Namespace PCAxis.Core.UnitTest
    <TestClass>
    Public Class VariableTest
        <TestMethod>
        Sub ShouldAddValue()
            'Arrange
            Dim variable As New Variable

            'Act
            Dim value As Value = New Value("TestValue")
            variable.Values.Add(value)

            'Assert
            Assert.AreEqual(variable.Values.Count, 1)
        End Sub
    End Class
End Namespace

