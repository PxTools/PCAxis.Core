
Namespace PCAxis.Paxiom
    ''' <summary>
    ''' Writes a PXModel to file or a stream in CSV format.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Csv2FileSerializer
        Inherits CsvFileSerializer
        Implements PCAxis.Paxiom.IPXModelStreamSerializer

#Region "Constructors"
        Public Sub New()
        End Sub
#End Region

        ''' <summary>
        ''' Serializes the model to the stream in the csv format.
        ''' </summary>
        ''' <param name="model">The model to serialize</param>
        ''' <param name="wr">The stream to serialize to</param>
        ''' <remarks></remarks>
        Protected Overrides Sub DoSerialize(ByVal model As PXModel, ByVal wr As System.IO.StreamWriter)
            MyBase.Model = PivotUtil.GetPivotedModelAllStub(model)
            WriteTitle(wr)
            WriteHeading(wr)
            WriteTable(wr)
        End Sub

    End Class

End Namespace
