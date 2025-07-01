Imports System.IO
Imports System.Data.Entity
Imports System.Data.Entity.Core.Metadata.Edm
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Security.Cryptography
Imports System.ComponentModel.DataAnnotations.Schema
Imports ClassTools
Imports System.Runtime.Remoting.Contexts
Public Class F_Totem

    'Id of the copy order, to track the current order being created
    Dim IdCopy As Integer

    'Sum of the total price of all the items in the cart
    Property TotalPrice As Decimal

    'List of items in the cart, used to keep track of the items
    Property ListaItems As New List(Of PrefabItem)

    'Flag used to block the update of the price when populating the carte from a previous order to avoid duplicates
    Private BlockUpdatePrice As Boolean = False

    'Flag to check if a copy order has been already created
    Dim CopyOrderCreated As Boolean = False

    Sub New()

        InitializeComponent()

    End Sub

    ''' <summary>
    ''' Calculate and set the price of all the items in the cart
    ''' </summary>
    Public Sub CalculateTotalPrice()

        'Calculate and assign the value of the total price
        TotalPrice = 0
        For Each Panel As PrefabItem In ListaItems
            TotalPrice = TotalPrice + Panel.TotalItemPrice
        Next
        L_TotalPrice.Text = $"Total Price: {TotalPrice.ToString("F2")}€"

        If Not CopyOrderCreated Then
            CreateCopyOrder()
        End If

        If Not BlockUpdatePrice Then
            UpdateOrderDetails()
        End If

    End Sub

    ''' <summary>
    ''' Creates a new copy order in the database to track the current order being created
    ''' </summary>
    Sub CreateCopyOrder()

        Using context As New DbStructure.MyDbContext()

            Dim CopyOrders As New DbStructure.CopyOrders With
            {
                .OrderDate = Date.Now,
                .OrderCompleted = False,
                .OrderInsertDate = Date.Now,
                .OrderInsertUser = "Totem"
            }
            context.CopyOrders.Add(CopyOrders)
            context.SaveChanges()

            IdCopy = CopyOrders.IdOrders
            CopyOrderCreated = True

        End Using

    End Sub

    ''' <summary>
    ''' Create and update the Copy order details 
    ''' </summary>
    Sub UpdateOrderDetails()

        'Creates a new instance of the database context
        Using context As New DbStructure.MyDbContext()

            'For each item in the cart, create or update the CopyOrderDetails in the database
            For Each Item In ListaItems
                Dim UpdateCopyDetails = context.CopyOrderDetails.SingleOrDefault(Function(cod) cod.IdProduct = Item.IdProduct AndAlso cod.IdOrder = IdCopy)
                If UpdateCopyDetails IsNot Nothing Then
                    UpdateCopyDetails.OrderQuantity = Item.ItemQuantity
                Else
                    Dim CopyDetail As New DbStructure.CopyOrderDetails With
                    {
                    .IdOrder = IdCopy,
                    .IdProduct = Item.IdProduct,
                    .OrderQuantity = Item.ItemQuantity
                    }
                    context.CopyOrderDetails.Add(CopyDetail)
                End If
                context.SaveChanges()
            Next

        End Using

    End Sub

    'When the user clicks on the button it will create the order and save it into the database
    Private Sub B_Order_Click(sender As Object, e As EventArgs) Handles B_Order.Click

        'Check if the list is empty 
        If ListaItems.Count < 1 Then
            Return
        End If

        'Creates a new instance of the database context
        Using context As New DbStructure.MyDbContext()

            'Create the order
            Dim Order As New DbStructure.Orders With
            {
            .OrderDate = Date.Now,
            .OrderCompleted = False,
            .OrderInsertDate = Date.Now,
            .OrderInsertUser = "Totem"
            }
            context.Orders.Add(Order)
            context.SaveChanges()

            'Create the orderDetails of all the products in the order
            For Each Item As PrefabItem In ListaItems
                Dim OrderDetails As New DbStructure.OrderDetails With
                {
                .IdOrder = Order.IdOrders,
                .IdProduct = Item.IdProduct,
                .OrderQuantity = Item.ItemQuantity
                }
                context.OrderDetails.Add(OrderDetails)
                context.SaveChanges()

                'Create or update the summary fr the product
                Dim existingSummary = context.Summaries.SingleOrDefault(Function(s) s.IdProduct = Item.IdProduct AndAlso s.RegistrationDate = Date.Today)
                If existingSummary IsNot Nothing Then
                    existingSummary.TotalQuantity += Item.ItemQuantity
                    existingSummary.TotalPrice += Item.ItemQuantity * Item.Baseprice
                Else
                    Dim newSummary As New DbStructure.Summaries With
                    {
                        .IdProduct = Item.IdProduct,
                        .RegistrationDate = Date.Now,
                        .TotalQuantity = Item.ItemQuantity,
                        .TotalPrice = Item.ItemQuantity * Item.Baseprice
                    }
                    context.Summaries.Add(newSummary)
                End If

                context.SaveChanges()

            Next

            ResetCart()
            Utils.WriteLogMessage($"Order {Order.IdOrders} Placed", "EXE\LOG", "Es21TotemLog")

        End Using


    End Sub


    ''' <summary>
    ''' Populates the form with the products from the database and adds them to the respective FlowLayoutPanel
    ''' </summary>
    Private Sub PopulateForm()

        'Creates a new instance of the database 
        Using context As New DbStructure.MyDbContext()

            'Create an ProductPrefab for every product in the database
            Dim products = context.Products.ToList()
            For Each product In products
                Select Case product.ProductCategory

                    Case "Hamburgers"
                        FLP_Hamburgers.Controls.Add(New PrefabProduct(product.IdProduct, product.ProductName, product.ProductPrice, product.ProductPicture))

                    Case "Appetizers"
                        FLP_Appetizers.Controls.Add(New PrefabProduct(product.IdProduct, product.ProductName, product.ProductPrice, product.ProductPicture))

                    Case "Drinks"
                        FLP_Drinks.Controls.Add(New PrefabProduct(product.IdProduct, product.ProductName, product.ProductPrice, product.ProductPicture))

                    Case "Dessert"
                        FLP_Dessert.Controls.Add(New PrefabProduct(product.IdProduct, product.ProductName, product.ProductPrice, product.ProductPicture))

                    Case "Sauce"
                        FLP_Sauce.Controls.Add(New PrefabProduct(product.IdProduct, product.ProductName, product.ProductPrice, product.ProductPicture))

                End Select
            Next
        End Using

    End Sub

    ''' <summary>
    ''' Populates the cart with the last order from the CopyOrders table
    ''' </summary>
    Sub PopulateCart()

        'Creates a new instance of the database 
        Using context As New DbStructure.MyDbContext()

            'Find the latest order in the CopyOrders if not found, return
            Dim lastOrder = context.CopyOrders.OrderByDescending(Function(co) co.IdOrders).FirstOrDefault()
            If lastOrder Is Nothing Then
                Return
            End If

            BlockUpdatePrice = True
            CopyOrderCreated = True
            IdCopy = lastOrder.IdOrders

            'Retrive all the CopyOrderDetails associated with the last order and populate the FLP 
            Dim OrderDetails = context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = IdCopy).ToList()
            For Each OrderDetail In OrderDetails
                Dim product = context.Products.SingleOrDefault(Function(p) p.IdProduct = OrderDetail.IdProduct AndAlso OrderDetail.OrderQuantity > 0)
                If product IsNot Nothing Then
                    Dim prefabItem As New PrefabItem(product.IdProduct, product.ProductName, product.ProductPrice) With {.ItemQuantity = OrderDetail.OrderQuantity}
                    FLP_OrderList.Controls.Add(prefabItem)
                End If
            Next

            'Create a new copy of the last order
            Dim newOrder As New DbStructure.CopyOrders With
            {
                 .OrderDate = Date.Now,
                 .OrderCompleted = False,
                 .OrderInsertDate = Date.Now,
                 .OrderInsertUser = "Totem"
            }
            context.CopyOrders.Add(newOrder)
            context.SaveChanges()

            'Create a new CopyOrderDetails for each item in the last order
            For Each oldDetail In OrderDetails
                Dim newDetail As New DbStructure.CopyOrderDetails With
                {
                    .IdOrder = newOrder.IdOrders,
                    .IdProduct = oldDetail.IdProduct,
                    .OrderQuantity = oldDetail.OrderQuantity
                }
                context.CopyOrderDetails.Add(newDetail)
            Next

            'Remove the old CopyOrderDetails and CopyOrders, this is done to avoid duplication
            context.CopyOrderDetails.RemoveRange(context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = IdCopy))
            context.CopyOrders.RemoveRange(context.CopyOrders.Where(Function(co) co.IdOrders = IdCopy))
            context.SaveChanges()

            IdCopy = newOrder.IdOrders

        End Using

        Utils.WriteLogMessage("Cart Populated from an old order", "EXE\LOG", "Es21TotemLog")

    End Sub

    'When the form start populate the form with the products and the cart with the last order
    Private Sub F_Totem_Load(sender As Object, e As EventArgs) Handles Me.Load

        PopulateForm()
        Utils.WriteLogMessage("Totem started", "EXE\LOG", "Es21TotemLog")

        PopulateCart()
        BlockUpdatePrice = False

    End Sub

    'When the user clicks on the button it will reset the cart and remove all the items
    Private Sub B_EmptyCart_Click(sender As Object, e As EventArgs) Handles B_EmptyCart.Click

        ResetCart()
        Utils.WriteLogMessage("Cart Emptied", "EXE\LOG", "Es21TotemLog")

    End Sub

    ''' <summary>
    ''' Empty the cart and reset all the items
    ''' </summary>
    Sub ResetCart()
        'Check if the list is empty 
        If ListaItems.Count < 1 Then
            Return
        End If

        'Creates a new instance of the database context
        Using context As New DbStructure.MyDbContext()

            context.CopyOrderDetails.RemoveRange(context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = IdCopy))
            context.CopyOrders.RemoveRange(context.CopyOrders.Where(Function(co) co.IdOrders = IdCopy))
            context.SaveChanges()

        End Using

        For Each panel As PrefabItem In ListaItems
            panel.Dispose()
        Next

        CopyOrderCreated = False
        ListaItems.Clear()
        L_TotalPrice.Text = "Total Price: 0,00€"

    End Sub

End Class
