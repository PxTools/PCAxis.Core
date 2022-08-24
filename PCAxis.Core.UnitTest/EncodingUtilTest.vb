Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PCAxis.Paxiom

Namespace PCAxis.Common.UnitTest
    <TestClass>
    Public Class EncodingUtilTest
        <TestMethod>
        Sub ShouldGetEncoding()

            'Arrange
            'Get a UTF-32 encoding by codepage.
            Dim e1 As System.Text.Encoding
            e1 = Encoding.GetEncoding(12000)

            'Act
            'Get a UTF-32 encoding by name.
            Dim e2 As System.Text.Encoding
            e2 = EncodingUtil.GetEncoding("utf-32")

            'Assert
            'Check their equality.
            Assert.AreEqual(e1, e2)

        End Sub
    End Class

End Namespace

