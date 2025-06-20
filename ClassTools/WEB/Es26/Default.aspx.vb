
Imports Antlr.Runtime.Misc

Partial Class _Default
    Inherits Page

    'Bool to check if the cart is empty
    Private Property isCartEmpty As Boolean
        Get
            Return If(Session("isCartEmpty") IsNot Nothing, CInt(Session("isCartEmpty")), 0)
        End Get
        Set(value As Boolean)
            Session("isCartEmpty") = value
        End Set
    End Property

    'Integer IdCopy to track the current order ID
    Public Property IdCopy As Integer

        Get
            Return If(Session("IdCopy") IsNot Nothing, CInt(Session("IdCopy")), 0)
        End Get

        Set(value As Integer)
            Session("IdCopy") = value
        End Set

    End Property

    'List of products in the cart
    Public Property ProductsCart As List(Of Object)

        Get
            If Session("ProductsCart") Is Nothing Then
                Session("ProductsCart") = New List(Of Object)
            End If
            Return CType(Session("ProductsCart"), List(Of Object))
        End Get

        Set(value As List(Of Object))
            Session("ProductsCart") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

            'Retrive teh list of products based on their category
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


            isCartEmpty = True
            ProductsCart.Clear()
            PopulateCart()

        End If

    End Sub

    ''' <summary>
    ''' Converts a byte array to a base64 image
    ''' </summary>
    Protected Function ConvertByteArrayToBase64Image(bytes() As Byte) As String

        'Check if the byte array is valid
        If bytes Is Nothing OrElse bytes.Length = 0 Then
            Return ""
        End If

        'Convert and return the image
        Dim base64String As String = Convert.ToBase64String(bytes)
        Return "data:image/png;base64," & base64String

    End Function

    ''' <summary>
    ''' Method used to add a product to the cart by clicking on a image
    ''' </summary>
    Protected Sub ProductSelected(source As Object, e As RepeaterCommandEventArgs) Handles RepeaterHamburgers.ItemCommand, RepeaterAppetizers.ItemCommand, RepeaterDessert.ItemCommand, RepeaterDrinks.ItemCommand, RepeaterSauce.ItemCommand

        If e.CommandName = "Select" Then

            Dim CreateNewDetail As Boolean = True
            Dim IdProduct As Integer = Convert.ToInt32(e.CommandArgument)

            'If the cart is empty, create a new order
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

            'For each product in the cart, check if it already exists to update the quantity or change the flag to create a new one
            For Each Product In ProductsCart
                If Product.IdProduct = IdProduct Then
                    Product.ProductQuantity = Product.productQuantity + 1
                    CreateNewDetail = False
                    Using context As New DbStructure.TotemDbContext()
                        Dim orderDetail = context.CopyOrderDetails.SingleOrDefault(Function(cod) cod.IdOrder = IdCopy AndAlso cod.IdProduct = IdProduct)
                        If orderDetail IsNot Nothing Then
                            orderDetail.OrderQuantity = orderDetail.OrderQuantity + 1
                            context.SaveChanges()
                        End If
                    End Using
                End If
            Next

            'Check the flag to create a new detail
            If CreateNewDetail Then
                Using context As New DbStructure.TotemDbContext()
                    Dim product = context.Products.SingleOrDefault(Function(p) p.IdProduct = IdProduct)
                    ProductsCart.Add(New With {.IdOrder = IdCopy, .IdProduct = product.IdProduct, .ProductName = product.ProductName, .ProductQuantity = 1, .BasePrice = product.ProductPrice})
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

        End If

        RepeaterSelected.DataSource = ProductsCart
        RepeaterSelected.DataBind()
        UpdatePrice()

    End Sub

    ''' <summary>
    ''' Populates the cart with the last order details from the database
    ''' </summary>
    Sub PopulateCart()

        Using context As New DbStructure.TotemDbContext()

            'Find the latest order in the CopyOrders if not found, return
            Dim lastOrder = context.CopyOrders.OrderByDescending(Function(co) co.IdOrders).FirstOrDefault()
            If lastOrder Is Nothing Then
                Return
            End If

            IdCopy = lastOrder.IdOrders
            isCartEmpty = False

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

            'Initialize the products in the cart
            Dim NewOrderDetails = context.CopyOrderDetails.ToList()
            For Each OrderDetail In NewOrderDetails
                Dim product = context.Products.SingleOrDefault(Function(p) p.IdProduct = OrderDetail.IdProduct)
                If product IsNot Nothing Then
                    ProductsCart.Add(New With {.IdOrder = IdCopy, .IdProduct = product.IdProduct, .ProductName = product.ProductName, .ProductQuantity = OrderDetail.OrderQuantity, .BasePrice = product.ProductPrice})
                End If
            Next

        End Using

        RepeaterSelected.DataSource = ProductsCart
        RepeaterSelected.DataBind()
        UpdatePrice()

    End Sub

    ''' <summary>
    ''' Method to empty the cart by clicking on the button
    ''' </summary>
    Protected Sub EmptyCart(sender As Object, e As EventArgs) Handles B_EmptyCart.Click
        EmptyCart()
    End Sub

    ''' <summary>
    ''' Method to empty the cart
    ''' </summary>
    Protected Sub EmptyCart()

        'Check if the cart is empty and return
        If ProductsCart.Count < 1 Then
            Return
        End If

        isCartEmpty = True
        L_TotalPrice.Text = $"Total Price: 0,00€"

        'Clear the cart and remove the order details from the database
        Using context As New DbStructure.TotemDbContext()
            context.CopyOrderDetails.RemoveRange(context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = IdCopy))
            context.CopyOrders.RemoveRange(context.CopyOrders.Where(Function(co) co.IdOrders = IdCopy))
            context.SaveChanges()
        End Using
        ProductsCart.Clear()
        RepeaterSelected.DataSource = ProductsCart
        RepeaterSelected.DataBind()

    End Sub






    ''' <summary>
    ''' Updates the total price of the cart
    ''' </summary>
    Sub UpdatePrice()

        Dim TotalPrice As Decimal
        For Each Product In ProductsCart
            TotalPrice = TotalPrice + (Product.BasePrice * Product.ProductQuantity)
        Next
        L_TotalPrice.Text = $"Total Price: {TotalPrice.ToString("F2")}€"

    End Sub

    ''' <summary>
    ''' Creates a new order with the products in the cart and updates the database
    ''' </summary>
    Protected Sub CreateOrder(sender As Object, e As EventArgs) Handles B_Order.Click

        'Check if the list is empty 
        If ProductsCart.Count < 1 Then
            Return
        End If

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
            For Each Item In ProductsCart
                Dim OrderDetails As New DbStructure.OrderDetails With
                {
                    .IdOrder = Order.IdOrders,
                    .IdProduct = Item.IdProduct,
                    .OrderQuantity = Item.ProductQuantity
                }
                context.OrderDetails.Add(OrderDetails)
                context.SaveChanges()

                Dim IdProduct As Integer = Item.IdProduct

                'Create or update the summary fr the product
                Dim existingSummary = context.Summaries.SingleOrDefault(Function(s) s.IdProduct = IdProduct AndAlso s.RegistrationDate = Date.Today)
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

            EmptyCart()

        End Using

    End Sub

    ''' <summary>
    ''' Method to handle the item button click event for adding or removing products from the cart
    ''' </summary>
    Protected Sub ItemButton(sender As Object, e As CommandEventArgs)

        Dim IdProduct As String = e.CommandArgument
        Dim ProductToRemove As Object

        Using context As New DbStructure.TotemDbContext()

            'Check if the button pressed was + or -
            If e.CommandName = "Remove" Then

                'Seach for the product in the cart and decrease the quantity by 1, 
                For Each Product In ProductsCart
                    If Product.IdProduct = IdProduct Then
                        Product.ProductQuantity = Product.productQuantity - 1
                        '
                        'If the quantity is less than 1, save the product to remove it from the cart
                        If Product.ProductQuantity < 1 Then
                            ProductToRemove = Product
                        End If

                        Dim orderDetail = context.CopyOrderDetails.SingleOrDefault(Function(cod) cod.IdOrder = IdCopy AndAlso cod.IdProduct = IdProduct)
                        If orderDetail IsNot Nothing Then
                            orderDetail.OrderQuantity = orderDetail.OrderQuantity - 1
                            context.SaveChanges()
                        End If

                    End If
                Next

                'Remove the product from the cart
                If ProductToRemove IsNot Nothing Then
                    context.CopyOrderDetails.Remove(context.CopyOrderDetails.SingleOrDefault(Function(od) od.IdOrder = IdCopy AndAlso od.IdProduct = IdProduct))
                    ProductsCart.Remove(ProductToRemove)
                    context.SaveChanges()
                End If

            End If

            If e.CommandName = "Add" Then

                'Seach for the product in the cart and increase the quantity by 1 
                For Each Product In ProductsCart
                    If Product.IdProduct = IdProduct Then
                        Product.ProductQuantity = Product.productQuantity + 1
                        Dim orderDetail = context.CopyOrderDetails.SingleOrDefault(Function(cod) cod.IdOrder = IdCopy AndAlso cod.IdProduct = IdProduct)
                        If orderDetail IsNot Nothing Then
                            orderDetail.OrderQuantity = orderDetail.OrderQuantity + 1
                            context.SaveChanges()
                        End If
                    End If
                Next

            End If

        End Using

        RepeaterSelected.DataSource = ProductsCart
        RepeaterSelected.DataBind()
        UpdatePrice()

    End Sub

End Class