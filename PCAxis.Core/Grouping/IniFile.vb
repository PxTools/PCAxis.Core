Imports System.IO

Public Class IniFile

    Private Class Section
        Public ReadOnly Property Name As String
        Public ReadOnly Property Data As Dictionary(Of String, KeyValuePair(Of String, String))

        Public Sub New(name As String)
            Me.Name = name
            Me.Data = New Dictionary(Of String, KeyValuePair(Of String, String))()
        End Sub
    End Class

    Private ReadOnly _stream As Stream
    Private ReadOnly _data As New Dictionary(Of String, Section)()
    Private ReadOnly _encoding As System.Text.Encoding

    Private Shared Function GetEncodeing(path As String) As System.Text.Encoding
        Dim cs As String
        Dim BUFFER_SIZE As Integer = 1024
        Dim buffer(BUFFER_SIZE - 1) As Byte
        Dim size As Integer
        Using fs As System.IO.FileStream = System.IO.File.OpenRead(path)
            Dim det As Ude.ICharsetDetector
            det = New Ude.CharsetDetector
            Dim fi As New System.IO.FileInfo(path)
            size = Math.Min(BUFFER_SIZE, Convert.ToInt32(fi.Length))
            size = fs.Read(buffer, 0, size)
            det.Feed(buffer, 0, size)
            det.DataEnd()

            cs = det.Charset
        End Using

        If cs Is Nothing Then
            Return System.Text.Encoding.Default
        End If

        If String.Compare(cs, "ASCII", True) = 0 Then
            Return System.Text.Encoding.Default
        End If

        Return System.Text.Encoding.GetEncoding(cs)
    End Function


    Public Sub New(path As String)
        If String.IsNullOrWhiteSpace(path) Then
            Throw New ArgumentException("Path must not be null or whitespace.", NameOf(path))
        End If
        _encoding = GetEncodeing(path)
        _stream = New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
    End Sub

    Public Sub New(stream As Stream)
        If stream Is Nothing Then
            Throw New ArgumentNullException(NameOf(stream))
        End If
        _encoding = System.Text.Encoding.Default
        _stream = stream
    End Sub

    Protected Iterator Function ReadLines() As IEnumerable(Of String)
        Using reader As New StreamReader(_stream, _encoding)
            Dim line As String = Nothing
            Do
                line = reader.ReadLine()
                If line Is Nothing Then Exit Do
                Yield line.Trim()
            Loop
        End Using
    End Function

    Public Sub Load()
        Dim currentSection As String = String.Empty
        For Each line In ReadLines()
            If line.StartsWith("[") AndAlso line.EndsWith("]") Then
                currentSection = line.Substring(1, line.Length - 2).Trim()
                Continue For
            End If

            If line.StartsWith(";") OrElse String.IsNullOrWhiteSpace(line) Then
                Continue For
            End If

            Dim equalsIndex As Integer = line.IndexOf("="c)
            If equalsIndex > 0 Then
                Dim key As String = line.Substring(0, equalsIndex).Trim()
                Dim value As String = line.Substring(equalsIndex + 1).Trim()
                Dim section = GetSection(currentSection)
                section.Data(key.ToUpper()) = New KeyValuePair(Of String, String)(key, value)
            End If
        Next

        _stream.Close()
    End Sub

    Private Function GetSection(section As String) As Section
        Dim sectionKey = section.ToUpper()
        Dim sectionData As Section = Nothing
        If Not _data.TryGetValue(sectionKey, sectionData) Then
            sectionData = New Section(section)
            _data(sectionKey) = sectionData
        End If
        Return sectionData
    End Function

    Public Function GetAllSections() As IEnumerable(Of String)
        Return _data.Keys
    End Function

    Public Function GetAllKeysInSection(section As String) As IEnumerable(Of String)
        Dim sectionKey = section.ToUpper()
        Dim sectionData As Section = Nothing
        If _data.TryGetValue(sectionKey, sectionData) Then
            Return sectionData.Data.Values.Select(Of String)(Function(kv) kv.Key)
        End If
        Return Array.Empty(Of String)()
    End Function

    Public Function GetValue(section As String, key As String, Optional defaultValue As String = "") As String
        Dim sectionKey = section.ToUpper()
        Dim sectionData As Section = Nothing
        Dim value As KeyValuePair(Of String, String)
        If _data.TryGetValue(sectionKey, sectionData) AndAlso sectionData.Data.TryGetValue(key.ToUpper(), value) Then
            Return value.Value
        End If
        Return defaultValue
    End Function

End Class