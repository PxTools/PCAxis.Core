Imports System.IO
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public NotInheritable Class IniFileTests

    Private Const TestIniContent As String =
        "[section 1]" & vbCrLf &
        "key1=value1" & vbCrLf &
        "key2=value2 ; this is a comment" & vbCrLf &
        "" & vbCrLf &
        "[section 2]" & vbCrLf &
        "keyA=valueA \" & vbCrLf &
        "continued valueA" & vbCrLf &
        "keyB=valueB" & vbCrLf

    <TestMethod>
    <DataRow("section 1", "key1", "value1")>
    <DataRow("Section 1", "key1", "value1")>
    <DataRow("SECTION 1", "key1", "value1")>
    <DataRow("SECTION 1", "KEY1", "value1")>
    Public Sub LoadedIniFile_Ignore_Casing(section As String, key As String, expected As String)
        ' Arrange
        Dim iniFile = New IniFile(New MemoryStream(Encoding.UTF8.GetBytes(TestIniContent)))
        iniFile.Load()
        ' Act
        Dim value = iniFile.GetValue(section, key)
        ' Assert
        Assert.AreEqual(expected, value)
    End Sub

End Class