Imports System
Imports System.Collections.Generic
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

    Public Sub New(path As String)
        If String.IsNullOrWhiteSpace(path) Then
            Throw New ArgumentException("Path must not be null or whitespace.", NameOf(path))
        End If
        _stream = New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
    End Sub

    Public Sub New(stream As Stream)
        If stream Is Nothing Then
            Throw New ArgumentNullException(NameOf(stream))
        End If
        _stream = stream
    End Sub

    Protected Iterator Function ReadLines() As IEnumerable(Of String)
        Using reader As New StreamReader(_stream)
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

    Public Function GetSections() As IEnumerable(Of String)
        Return _data.Keys
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