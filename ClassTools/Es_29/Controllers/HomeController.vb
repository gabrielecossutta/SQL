Public Class HomeController
    Inherits System.Web.Mvc.Controller


    'Populates the ViewBags and retrive the suspended order from the db
    Function Index() As ActionResult

        Using context As New DbStructure.TotemDbContext()

            'Populate the viewbags with the products from the database, to populate che products area
            ViewBag.Hamburgers = context.Products.Where(Function(p) p.ProductCategory = "Hamburgers").ToList()
            ViewBag.Appetizers = context.Products.Where(Function(p) p.ProductCategory = "Appetizers").ToList()
            ViewBag.Dessert = context.Products.Where(Function(p) p.ProductCategory = "Dessert").ToList()
            ViewBag.Drinks = context.Products.Where(Function(p) p.ProductCategory = "Drinks").ToList()
            ViewBag.Sauce = context.Products.Where(Function(p) p.ProductCategory = "Sauce").ToList()

            'Find the latest order in the CopyOrders, if there is no order, create a new one
            Dim lastOrder = context.CopyOrders.OrderByDescending(Function(co) co.IdOrders).FirstOrDefault()
            If lastOrder Is Nothing Then
                Dim newOrderCreated As New DbStructure.CopyOrders With
                {
                    .OrderDate = Date.Now,
                    .OrderCompleted = False,
                    .OrderInsertDate = Date.Now,
                    .OrderInsertUser = "Totem"
                }
                context.CopyOrders.Add(newOrderCreated)
                context.SaveChanges()
                ViewBag.IdCopy = newOrderCreated.IdOrders
                Return View()
            End If

            Dim IdCopy = lastOrder.IdOrders

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

            'Create the ViewBags
            ViewBag.IdCopy = newOrder.IdOrders
            ViewBag.orderDetails = context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = newOrder.IdOrders AndAlso cod.OrderQuantity > 0).ToList()

        End Using

        Return View()

    End Function

End Class

