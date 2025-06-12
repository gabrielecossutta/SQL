Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Threading
Imports Es_21.F_Totem

Public Class KitchenOrder

    Property IdOrder As Integer

    Sub New(IdOrder As Integer)
        InitializeComponent()
        Me.IdOrder = IdOrder

    End Sub

    Private Sub B_OrderComplete_Click(sender As Object, e As EventArgs) Handles B_OrderComplete.Click
        Using context As New DbStructure.AppDbContext("Server=DESKTOP-6IEL0JH\SQLEXPRESS;Database=McDonald;User=UserName;Password=123;")

            Dim toDeleteOrderDetails As List(Of DbStructure.OrderDetails) = context.OrderDetails.Where(Function(od) od.IdOrder = IdOrder).ToList()

            If toDeleteOrderDetails.Any() Then
                context.OrderDetails.RemoveRange(toDeleteOrderDetails)
                context.SaveChanges()
            End If

            Dim toDeleteOrder As DbStructure.Orders = context.Orders.SingleOrDefault(Function(o) o.IdOrders = IdOrder)
            If toDeleteOrder IsNot Nothing Then
                context.Orders.Remove(toDeleteOrder)
                context.SaveChanges()
            End If

        End Using
        Me.Dispose()
    End Sub

    Private Sub KitchenOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Using context As New DbStructure.AppDbContext("Server=DESKTOP-6IEL0JH\SQLEXPRESS;Database=McDonald;User=UserName;Password=123;")
            Try
                Dim query = From od In context.OrderDetails
                            Join p In context.Products On od.IdProduct Equals p.IdProduct
                            Where od.IdOrder = IdOrder
                            Group od By p.ProductName Into GroupItems = Group
                            Select ProductName, TotalQuantity = GroupItems.Sum(Function(x) x.OrderQuantity)

                LB_ItemList.Items.Clear()

                For Each item In query
                    Dim itemText As String = $"{item.ProductName} x{item.TotalQuantity}"
                    LB_ItemList.Items.Add(itemText)
                Next

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LB_ItemList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_ItemList.SelectedIndexChanged

    End Sub
End Class
