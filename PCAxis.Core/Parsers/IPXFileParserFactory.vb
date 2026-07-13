Namespace PCAxis.Paxiom.Parsers
    Public Interface IPXFileParserFactory

        Function Create() As PXFileParser


    End Interface

    Public Class PXFileParserFactory
        Implements IPXFileParserFactory

        Public Sub New()
        End Sub

        Public Function Create() As PXFileParser Implements IPXFileParserFactory.Create
            Return New PXFileParser()
        End Function
    End Class

End Namespace