Imports System.Web.Mvc

Public Class GetInfoController
    Inherits Controller


    ''' <summary>
    ''' Return the name and the price of the product
    ''' </summary>
    <HttpGet>  'Can accept only http GET requests
    Function GetProductNamePrice(id As Integer) As JsonResult

        'Retrieve the product by its ID and return its name and price with a json
        Using context As New DbStructure.TotemDbContext()
            Dim product = context.Products.FirstOrDefault(Function(p) p.IdProduct = id)
            Return Json(New With
                        {
                            .name = product.ProductName,
                            .price = product.ProductPrice
                        }, JsonRequestBehavior.AllowGet)
        End Using

    End Function

    ''' <summary>
    ''' Increase the quantity of the product in the order details
    ''' </summary>
    <HttpGet>'Can accept only http GET requests
    Sub IncreaseDetails(IdProduct As Integer, IdOrder As Integer)

        'Retrive the OrderDetails by IdOrder and IdProduct and Increase the quantity of the product by 1
        Using context As New DbStructure.TotemDbContext()
            Dim orderDetail = context.CopyOrderDetails.SingleOrDefault(Function(cod) cod.IdOrder = IdOrder AndAlso cod.IdProduct = IdProduct)
            If orderDetail IsNot Nothing Then
                orderDetail.OrderQuantity = orderDetail.OrderQuantity + 1
                context.SaveChanges()
            End If
        End Using

    End Sub

    ''' <summary>
    ''' Decrease the quantity of the product in the order details
    ''' </summary>
    <HttpGet>'Can accept only http GET requests
    Sub DecreaseDetails(IdProduct As Integer, IdOrder As Integer)

        'Retrive the OrderDetails by IdOrder and IdProduct and decrease the quantity of the product by 1
        Using context As New DbStructure.TotemDbContext()
            Dim orderDetail = context.CopyOrderDetails.SingleOrDefault(Function(cod) cod.IdOrder = IdOrder AndAlso cod.IdProduct = IdProduct)
            If orderDetail IsNot Nothing Then
                orderDetail.OrderQuantity = orderDetail.OrderQuantity - 1
                context.SaveChanges()
            End If
        End Using

    End Sub

    ''' <summary>
    ''' Delete the product from the order details
    ''' </summary>
    <HttpGet>'Can accept only http GET requests
    Sub DeleteDetails(IdProduct As Integer, IdOrder As Integer)

        'Retrive the order details by IdOrder and IdProduct and delete it
        Using context As New DbStructure.TotemDbContext()
            context.CopyOrderDetails.Remove(context.CopyOrderDetails.SingleOrDefault(Function(od) od.IdOrder = IdOrder AndAlso od.IdProduct = IdProduct))
            context.SaveChanges()
        End Using

    End Sub

    ''' <summary>
    ''' Delete the cart by removing all the products in the order details for a specific order
    ''' </summary>
    ''' <param name="IdOrder"></param>
    Sub EmptyCart(IdOrder As Integer)

        'Remove all the products in the order details for a specific order and delete them
        Using context As New DbStructure.TotemDbContext()
            context.CopyOrderDetails.RemoveRange(context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = IdOrder))
            context.SaveChanges()
        End Using

    End Sub

    ''' <summary>
    ''' Create a new order details for a specific product and order
    ''' </summary>
    <HttpGet>'Can accept only http GET requests
    Sub NewDetails(IdProduct As Integer, IdOrder As Integer)

        Using context As New DbStructure.TotemDbContext()

            'Try to retrieve the order details by IdOrder and IdProduct
            Dim orderDetail = context.CopyOrderDetails.SingleOrDefault(Function(cod) cod.IdOrder = IdOrder AndAlso cod.IdProduct = IdProduct)

            'If it exists, do nothing to avoid duplicates or create a new one with quantity 1
            If orderDetail IsNot Nothing Then
            Else
                Dim newDetail As New DbStructure.CopyOrderDetails With {
                    .IdOrder = IdOrder,
                    .IdProduct = IdProduct,
                    .OrderQuantity = 1
                }
                context.CopyOrderDetails.Add(newDetail)
                context.SaveChanges()
            End If

        End Using

    End Sub

    ''' <summary>
    ''' Create a new order and the order details for all the products in the cart
    ''' </summary>
    <HttpGet>'Can accept only http GET requests
    Sub CreateOrder(IdOrder As Integer)

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

            'Save the new order ID to create the order details
            Dim newIdOrder = Order.IdOrders

            'Retrive all the Products in the CopyOrderDetails to create the orderDetails
            Dim ProductsCart = context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = IdOrder AndAlso cod.OrderQuantity > 0).ToList()
            For Each Item In ProductsCart
                Dim OrderDetails As New DbStructure.OrderDetails With
                {
                    .IdOrder = newIdOrder,
                    .IdProduct = Item.IdProduct,
                    .OrderQuantity = Item.OrderQuantity
                }
                context.OrderDetails.Add(OrderDetails)
                context.SaveChanges()


                'Retrieve the product by the IdProduct to update the summary
                Dim IdProduct As Integer = Item.IdProduct
                Dim existingProduct = context.Products.SingleOrDefault(Function(p) p.IdProduct = IdProduct)

                'Retrive the summary for the product and update it, if it doesn't exist, create a new one
                Dim existingSummary = context.Summaries.SingleOrDefault(Function(s) s.IdProduct = IdProduct AndAlso s.RegistrationDate = Date.Today)
                If existingSummary IsNot Nothing Then
                    existingSummary.TotalQuantity += Item.OrderQuantity
                    existingSummary.TotalPrice += existingProduct.ProductPrice * Item.OrderQuantity
                Else
                    Dim newSummary As New DbStructure.Summaries With
                    {
                        .IdProduct = Item.IdProduct,
                        .RegistrationDate = Date.Now,
                        .TotalQuantity = Item.OrderQuantity,
                        .TotalPrice = existingProduct.ProductPrice * Item.OrderQuantity
                    }
                    context.Summaries.Add(newSummary)
                End If
                context.SaveChanges()
            Next

            'Remove all the details in CopyOrderDetails
            EmptyCart(IdOrder)

        End Using

    End Sub

End Class



