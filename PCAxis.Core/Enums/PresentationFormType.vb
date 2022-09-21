Namespace PCAxis.Paxiom
    ''' <summary>
    ''' Specifies presentation form that is displayed   
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum PresentationFormType
        ''' <summary>
        ''' Only code should be displayed
        ''' </summary>
        ''' <remarks></remarks>
        Code = 0
        ''' <summary>
        ''' Only text should be displayed
        ''' </summary>
        ''' <remarks></remarks>
        Text = 1
        ''' <summary>
        '''  Both code and value should be displayed and the order should be the code and then the value text. 
        ''' </summary>
        ''' <remarks></remarks>
        Code_Value = 2
        ''' <summary>
        '''  Both code and value should be displayed and the order should be the value and then the code text. 
        ''' </summary>
        ''' <remarks></remarks>
        Value_Code = 3
    End Enum

End Namespace


