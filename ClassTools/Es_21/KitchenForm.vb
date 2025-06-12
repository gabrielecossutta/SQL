Imports System.Data.SqlClient

Public Class F_Kitchen

    Private Sub F_Kitchen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connectionString As String = "Server=DESKTOP-6IEL0JH\SQLEXPRESS;Database=McDonald;User=UserName;Password=123;"
        Dim query As String = "SELECT IdOrders FROM Orders WHERE OrderCompleted = 0"
        Dim listaOrdini As New List(Of Integer)

        Using conn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand(query, conn)

            Try
                conn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    listaOrdini.Add(reader.GetInt32(0))
                End While
            Catch ex As Exception
                MessageBox.Show("Errore nel caricamento degli ordini: " & ex.Message)
            End Try
        End Using

        For Each idOrder As Integer In listaOrdini
            FLP_KitchenOrders.Controls.Add(New KitchenOrder(idOrder))
        Next
    End Sub

    Private Sub FLP_KitchenOrders_Paint(sender As Object, e As PaintEventArgs) Handles FLP_KitchenOrders.Paint

    End Sub
End Class