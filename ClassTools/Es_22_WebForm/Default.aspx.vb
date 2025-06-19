
Imports Antlr.Runtime.Misc

Partial Class _Default
    Inherits Page



    'Id of the copy order, to track the current order being created
    Private Property isCartEmpty As Boolean
        Get
            Return If(Session("isCartEmpty") IsNot Nothing, CInt(Session("isCartEmpty")), 0)
        End Get
        Set(value As Boolean)
            Session("isCartEmpty") = value
        End Set
    End Property
    Public Property IdCopy As Integer

        Get
            Return If(Session("IdCopy") IsNot Nothing, CInt(Session("IdCopy")), 0)
        End Get
        Set(value As Integer)
            Session("IdCopy") = value
        End Set
    End Property

    'Sum of the total price of all the items in the cart
    Property TotalPrice As Decimal

    Public Property Prodotti As List(Of Object)
        Get
            If Session("Prodotti") Is Nothing Then
                Session("Prodotti") = New List(Of Object)
            End If
            Return CType(Session("Prodotti"), List(Of Object))
        End Get
        Set(value As List(Of Object))
            Session("Prodotti") = value
        End Set
    End Property


    'Flag used to block the update of the price when populating the carte from a previous order to avoid duplicates
    Private BlockUpdatePrice As Boolean = False

    'Flag to check if a copy order has been already created
    Dim CopyOrderCreated As Boolean = False


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            isCartEmpty = True

            Using context As New DbStructure.TotemDbContext()
                RepeaterHamburgers.DataSource = context.Products.Where(Function(p) p.ProductCategory = "Hamburgers").ToList()
                RepeaterHamburgers.DataBind()

                RepeaterAppetizers.DataSource = context.Products.Where(Function(p) p.ProductCategory = "Appetizers").ToList()
                RepeaterAppetizers.DataBind()

                RepeaterDessert.DataSource = context.Products.Where(Function(p) p.ProductCategory = "Dessert").ToList()
                RepeaterDessert.DataBind()

                RepeaterDrinks.DataSource = context.Products.Where(Function(p) p.ProductCategory = "Drinks").ToList()
                RepeaterDrinks.DataBind()

                RepeaterSauce.DataSource = context.Products.Where(Function(p) p.ProductCategory = "Sauce").ToList()
                RepeaterSauce.DataBind()

            End Using
            Prodotti.Clear()

            PopulateCart()

        End If






    End Sub

    Protected Function ConvertByteArrayToBase64Image(bytes() As Byte) As String
        If bytes Is Nothing OrElse bytes.Length = 0 Then
            Return ""
        End If

        Dim base64String As String = Convert.ToBase64String(bytes)
        Return "data:image/png;base64," & base64String
    End Function

    Protected Sub Product_Selected(source As Object, e As RepeaterCommandEventArgs) Handles RepeaterHamburgers.ItemCommand, RepeaterAppetizers.ItemCommand, RepeaterDessert.ItemCommand, RepeaterDrinks.ItemCommand, RepeaterSauce.ItemCommand

        If e.CommandName = "Select" Then
            Dim prodottoId As Integer = Convert.ToInt32(e.CommandArgument)
            Dim create As Boolean = True

            If isCartEmpty Then
                Using context As New DbStructure.TotemDbContext()
                    Dim newOrder As New DbStructure.CopyOrders With
                    {
                        .OrderDate = Date.Now,
                        .OrderCompleted = False,
                        .OrderInsertDate = Date.Now,
                        .OrderInsertUser = "Totem"
                    }
                    context.CopyOrders.Add(newOrder)
                    context.SaveChanges()
                    IdCopy = newOrder.IdOrders
                    isCartEmpty = False
                End Using
            End If

            For Each Prodotto In Prodotti

                If Prodotto.IdProduct = prodottoId Then

                    Prodotto.ProductQuantity = Prodotto.productQuantity + 1
                    create = False

                    'aggiornare db
                    Using context As New DbStructure.TotemDbContext()
                        Dim orderDetail = context.CopyOrderDetails.SingleOrDefault(Function(cod) cod.IdOrder = IdCopy AndAlso cod.IdProduct = prodottoId)
                        If orderDetail IsNot Nothing Then
                            orderDetail.OrderQuantity = orderDetail.OrderQuantity + 1
                            context.SaveChanges()
                        End If
                    End Using
                End If
            Next

            If create Then
                Using context As New DbStructure.TotemDbContext()
                    Dim product = context.Products.SingleOrDefault(Function(p) p.IdProduct = prodottoId)
                    Prodotti.Add(New With {.IdOrder = IdCopy, .IdProduct = product.IdProduct, .ProductName = product.ProductName, .ProductQuantity = 1, .BasePrice = product.ProductPrice})
                    Dim newOrderDetail As New DbStructure.CopyOrderDetails With
                    {
                        .IdOrder = IdCopy,
                        .IdProduct = product.IdProduct,
                        .OrderQuantity = 1
                    }
                    context.CopyOrderDetails.Add(newOrderDetail)
                    context.SaveChanges()
                End Using
            End If

            RepeaterSelected.DataSource = Prodotti
            RepeaterSelected.DataBind()



        End If

        UpdatePrice()
    End Sub


    Sub PopulateCart()

        'Creates a new instance of the database 
        Using context As New DbStructure.TotemDbContext()

            'Find the latest order in the CopyOrders if not found, return
            Dim lastOrder = context.CopyOrders.OrderByDescending(Function(co) co.IdOrders).FirstOrDefault()
            If lastOrder Is Nothing Then
                Return
            End If
            IdCopy = lastOrder.IdOrders
            isCartEmpty = False

            'Retrive all the CopyOrderDetails associated with the last order and populate the FLP 



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
            Dim OrderDetails = context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = IdCopy AndAlso cod.OrderQuantity >= 1).ToList()

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

            Dim NewOrderDetails = context.CopyOrderDetails.ToList()
            For Each OrderDetail In NewOrderDetails
                Dim product = context.Products.SingleOrDefault(Function(p) p.IdProduct = OrderDetail.IdProduct)
                If product IsNot Nothing Then
                    Prodotti.Add(New With {.IdOrder = IdCopy, .IdProduct = product.IdProduct, .ProductName = product.ProductName, .ProductQuantity = OrderDetail.OrderQuantity, .BasePrice = product.ProductPrice})
                End If
            Next
            Response.Write(Prodotti.Count)




        End Using
        RepeaterSelected.DataSource = Prodotti
        RepeaterSelected.DataBind()
        UpdatePrice()

    End Sub


    Protected Sub SvuotaCarrello(sender As Object, e As EventArgs) Handles B_EmptyCart.Click
        SvuotaCarrello1()
    End Sub


    Protected Sub SvuotaCarrello1()

        If Prodotti.Count < 1 Then
            Return
        End If

        'Creates a new instance of the database context
        Using context As New DbStructure.TotemDbContext()

            context.CopyOrderDetails.RemoveRange(context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = IdCopy))
            context.CopyOrders.RemoveRange(context.CopyOrders.Where(Function(co) co.IdOrders = IdCopy))
            context.SaveChanges()

        End Using
        Prodotti.Clear()
        RepeaterSelected.DataSource = Prodotti
        RepeaterSelected.DataBind()
        isCartEmpty = True
        L_TotalPrice.Text = $"Total Price: 0,00€"
    End Sub







    Sub UpdatePrice()

        Dim TotalPrice As Decimal
        For Each prodotto In Prodotti

            TotalPrice = TotalPrice + (prodotto.BasePrice * prodotto.ProductQuantity)
        Next
        L_TotalPrice.Text = $"Total Price: {TotalPrice.ToString("F2")}€"

    End Sub










    Protected Sub Ordina(sender As Object, e As EventArgs) Handles B_Order.Click

        'Check if the list is empty 
        If Prodotti.Count < 1 Then
            Return
        End If

        'Creates a new instance of the database context
        Using context As New DbStructure.TotemDbContext()

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
            For Each Item In Prodotti
                Dim OrderDetails As New DbStructure.OrderDetails With
                {
                .IdOrder = Order.IdOrders,
                .IdProduct = Item.IdProduct,
                .OrderQuantity = Item.ProductQuantity
                }
                context.OrderDetails.Add(OrderDetails)
                context.SaveChanges()
                Dim idprodotto As Integer = Item.IdProduct
                'Create or update the summary fr the product
                Dim existingSummary = context.Summaries.SingleOrDefault(Function(s) s.IdProduct = idprodotto AndAlso s.RegistrationDate = Date.Today)
                If existingSummary IsNot Nothing Then
                    existingSummary.TotalQuantity += Item.ProductQuantity
                    existingSummary.TotalPrice += Item.BasePrice * Item.ProductQuantity
                Else
                    Dim newSummary As New DbStructure.Summaries With
                    {
                        .IdProduct = Item.IdProduct,
                        .RegistrationDate = Date.Now,
                        .TotalQuantity = Item.ProductQuantity,
                        .TotalPrice = Item.BasePrice * Item.ProductQuantity
                    }
                    context.Summaries.Add(newSummary)
                End If

                context.SaveChanges()

            Next

            SvuotaCarrello1()

        End Using

    End Sub

    Protected Sub Button_Command(sender As Object, e As CommandEventArgs)
        Dim IdProduct As String = e.CommandArgument
        Using context As New DbStructure.TotemDbContext()

            If e.CommandName = "Remove" Then
                For Each Prodotto In Prodotti

                    If Prodotto.IdProduct = IdProduct Then

                        Prodotto.ProductQuantity = Prodotto.productQuantity - 1

                        Dim orderDetail = context.CopyOrderDetails.SingleOrDefault(Function(cod) cod.IdOrder = IdCopy AndAlso cod.IdProduct = IdProduct)
                        If orderDetail IsNot Nothing Then
                            orderDetail.OrderQuantity = orderDetail.OrderQuantity - 1
                            context.SaveChanges()
                        End If


                    End If
                Next
            End If

            If e.CommandName = "Add" Then
                For Each Prodotto In Prodotti

                    If Prodotto.IdProduct = IdProduct Then

                        Prodotto.ProductQuantity = Prodotto.productQuantity + 1

                        Dim orderDetail = context.CopyOrderDetails.SingleOrDefault(Function(cod) cod.IdOrder = IdCopy AndAlso cod.IdProduct = IdProduct)
                        If orderDetail IsNot Nothing Then
                            orderDetail.OrderQuantity = orderDetail.OrderQuantity + 1
                            context.SaveChanges()
                        End If


                    End If
                Next
            End If
        End Using

        RepeaterSelected.DataSource = Prodotti
        RepeaterSelected.DataBind()
    End Sub


End Class