Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Threading
Imports ClassTools
Imports Es_21.F_Totem

Public Class KitchenOrder

    'Id of the Order
    Property IdOrder As Integer

    Sub New(IdOrder As Integer)

        'Assign the Base data
        InitializeComponent()
        Me.IdOrder = IdOrder

    End Sub

    'When the user click on the button to complete the order, delete the order from the database and destroy the usercontrol
    Private Sub B_OrderComplete_Click(sender As Object, e As EventArgs) Handles B_OrderComplete.Click

        'Creates a new instance of the database context
        Using context As New DbStructure.MyDbContext()

            'Retrive all the OrdersDetails to delete having a specific IdOrder
            Dim toDeleteOrderDetails As List(Of DbStructure.OrderDetails) = context.OrderDetails.Where(Function(od) od.IdOrder = IdOrder).ToList()
            If toDeleteOrderDetails.Any() Then
                context.OrderDetails.RemoveRange(toDeleteOrderDetails)
                context.SaveChanges()
            End If

            'Retrice and delete a specific order
            Dim toDeleteOrder As DbStructure.Orders = context.Orders.SingleOrDefault(Function(o) o.IdOrders = IdOrder)
            If toDeleteOrder IsNot Nothing Then
                context.Orders.Remove(toDeleteOrder)
                context.SaveChanges()
            End If

        End Using

        Utils.WriteLogMessage($"Order {IdOrder} Completed", "EXE\LOG", "Es21KitchenLog")
        Me.Dispose()

    End Sub

    Private Sub KitchenOrder_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Creates a new instance of the database context
        Using context As New DbStructure.MyDbContext()

            Try
                'Retrive all the Items name of the product conteined in the order
                Dim query = From od In context.OrderDetails
                            Join p In context.Products On od.IdProduct Equals p.IdProduct
                            Where od.IdOrder = IdOrder
                            Group od By p.ProductName Into GroupItems = Group
                            Select ProductName, TotalQuantity = GroupItems.Sum(Function(x) x.OrderQuantity)

                LB_ItemList.Items.Clear()

                'Write all the names in the ListBox with the quantity
                For Each item In query
                    Dim itemText As String = $"{item.TotalQuantity} x {item.ProductName}"
                    LB_ItemList.Items.Add(itemText)
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Using

        Utils.WriteLogMessage("TotemKitchenStarted", "EXE\LOG", "Es21KitchenLog")

    End Sub

End Class