Namespace PCAxis.Paxiom

    ''' <summary>
    ''' Utility class for handling pivoting 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PivotUtil

        ''' <summary>
        ''' Gets the object model pivoted with all variables in stub
        ''' </summary>
        ''' <param name="oldModel">The model to be pivoted</param>
        ''' <returns>pivoted model</returns>

        Public Shared Function GetPivotedModelAllStub(ByVal oldModel As PXModel) As Paxiom.PXModel
            Dim pd As New List(Of PCAxis.Paxiom.Operations.PivotDescription)()

            Dim nonContentVariables = oldModel.Meta.Variables.Where(Function(v) v.IsTime = False And v.IsContentVariable = False)
            Dim timeVariable = oldModel.Meta.Variables.Where(Function(v) v.IsTime = True)
            Dim contentVariable = oldModel.Meta.Variables.Where(Function(v) v.IsContentVariable = True)

            For Each variable In nonContentVariables
                pd.Add(New PCAxis.Paxiom.Operations.PivotDescription(variable.Name, PCAxis.Paxiom.PlacementType.Stub))
            Next

            pd.Add(New PCAxis.Paxiom.Operations.PivotDescription(timeVariable.First().Name, PCAxis.Paxiom.PlacementType.Stub))
            pd.Add(New PCAxis.Paxiom.Operations.PivotDescription(contentVariable.First().Name, PCAxis.Paxiom.PlacementType.Stub))

            Dim pivot As New PCAxis.Paxiom.Operations.Pivot

            Return pivot.Execute(oldModel, pd.ToArray())
        End Function
    End Class

End Namespace