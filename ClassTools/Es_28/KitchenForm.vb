Imports System.Data.SqlClient

Public Class F_Kitchen

    Private Sub F_Kitchen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim ListOrders As New List(Of Integer)

        'Creates a new instance of the database context
        Using context As New DbStructure.MyDbContext()

            'Find al the orders that aren't completed
            Try
                For Each o In context.Orders
                    If o.OrderCompleted = False Then
                        ListOrders.Add(o.IdOrders)
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Using

        'Add the KitchenPrefab in the kitchen form
        For Each idOrder As Integer In ListOrders
            FLP_KitchenOrders.Controls.Add(New KitchenOrder(idOrder))
        Next

    End Sub

End Class