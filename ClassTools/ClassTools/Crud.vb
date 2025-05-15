
Public Class Crud
    Private Sub Create()
        'Insert a new row
    End Sub
    Private Sub Read()
        'read a row
    End Sub
    Private Sub Update()
        'update a cell
    End Sub
    Private Sub Delete()
        'delete all the fields of a row
    End Sub

    Public Shared Function GetDati(ByVal ssql As String) As DataTable
        Try

        Catch ex As Exception
            Throw New ArgumentOutOfRangeException("non va")
        End Try
        Return New DataTable
    End Function

End Class