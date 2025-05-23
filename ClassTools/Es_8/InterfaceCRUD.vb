Public Interface ICRUD
    Function Create(Of T)(ParamArray args() As Object) As T
    Function Read(Of T)(ParamArray args() As Object) As T
    Function Update(Of T)(ParamArray args() As Object) As T
    Function Delete(Of T)(ParamArray args() As Object) As T
End Interface




Public MustInherit Class BaseTable
    Implements ICRUD

    Public Overridable Function Create(Of T)(ParamArray args() As Object) As T Implements ICRUD.Create
        For Each arg As Object In args
            Console.WriteLine($"Create: {arg.ToString()} Value: {arg}")
        Next
        Return Nothing

    End Function

    Public Function Read(Of T)(ParamArray args() As Object) As T Implements ICRUD.Read
        For Each arg As Object In args
            Console.WriteLine($"Read: {arg.GetType.ToString()} Value: {arg}")
        Next
        Return Nothing
    End Function

    Public Overridable Function Update(Of T)(ParamArray args() As Object) As T Implements ICRUD.Update
        For Each arg As Object In args
            Console.WriteLine($"Update: {arg.GetType.ToString()} Value: {arg}")
        Next
        Return Nothing

    End Function

    Public Function Delete(Of T)(ParamArray args() As Object) As T Implements ICRUD.Delete
        For Each arg As Object In args
            Console.WriteLine($"Delete: {arg.GetType.ToString()} Value: {arg}")
        Next
        Return Nothing

    End Function
End Class

Public Class Table1
    Inherits BaseTable

    Public Overrides Function Create(Of T)(ParamArray args() As Object) As T
        For Each arg As Object In args
            Console.WriteLine($"CreateOverride: {arg.GetType.ToString()} Value: {arg}")
        Next
        Return Nothing
    End Function
End Class

Public Class Table2
    Inherits BaseTable

    Public Overrides Function Update(Of T)(ParamArray args() As Object) As T
        For Each arg As Object In args
            Console.WriteLine($"UpdateOverride Value: {arg}")
        Next
        Return Nothing
    End Function
End Class


Module InterfaceCRUD

    Sub Main()

        Dim table1 As New Table1()
        Dim table2 As New Table2()

        Dim intero As Integer = table1.Create(Of String)("Matteo", 1, 4D)
        Dim intero3 As Integer = table1.Create(Of Object)(1, "2", 3.4F)
        Dim intero2 As Integer = table2.Update(Of Integer)(1, 2, 3)
    End Sub

End Module
