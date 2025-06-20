
Imports System.ComponentModel
Imports System.Data.Entity
Imports System.IO
Imports System.Net
Imports System.Runtime.Remoting.Contexts
Imports System.Text
Imports System.Web
Imports System.Web.Script.Serialization
Imports ClassTools
Imports Newtonsoft
Imports Newtonsoft.Json.Linq

Public Class F_Synchronize
    Implements WebService.IWebServiceHandler
    Dim IsWebServiceRunning As Boolean = False

    Private Listener As HttpListener
    Dim webService As New WebService(Me)


    Private Sub B_BackToTotem_Click(sender As Object, e As EventArgs) Handles B_BackToTotem.Click
        SyncronizeBackOfficeOnTotem()

    End Sub

    Sub SyncronizeBackOfficeOnTotem()
        Using contextBackOffice As New DbStructure.BackOfficeDbContext()
            Dim productsToSync = contextBackOffice.Products.ToList()
            Using contextTotem As New DbStructure.TotemDbContext()
                For Each product In productsToSync
                    Dim existingProduct = contextTotem.Products.FirstOrDefault(Function(p) p.ProductName = product.ProductName)
                    If existingProduct IsNot Nothing Then

                        If existingProduct.ProductModificationDate <> Date.Now.Date Then
                            existingProduct.ProductCategory = product.ProductCategory
                            existingProduct.ProductPrice = product.ProductPrice
                            existingProduct.ProductPicture = product.ProductPicture
                            existingProduct.ProductDescription = product.ProductDescription
                            existingProduct.ProductModificationDate = Date.Now.Date
                            existingProduct.ProductModificationUser = product.ProductModificationUser
                        Else


                            existingProduct.ProductModificationDate = Date.Now.Date
                            existingProduct.ProductModificationUser = product.ProductModificationUser
                        End If
                    Else
                        product.ProductModificationDate = Date.Now.Date
                        product.ProductModificationUser = product.ProductInsertUser
                        contextTotem.Products.Add(product)
                    End If
                Next
                contextTotem.SaveChanges()
                Dim productsToCheck = contextTotem.Products.ToList()
                Dim productsToDelete = productsToCheck.Where(Function(ptc) ptc.ProductModificationDate <> Date.Now.Date)
                If productsToDelete.Count > 0 Then
                    contextTotem.Products.RemoveRange(productsToDelete)
                End If
            End Using
        End Using
    End Sub

    Private Sub B_TotemToBackOffice_Click(sender As Object, e As EventArgs) Handles B_TotemToBackOffice.Click
        SyncronizeTotemOnBackOffice()
    End Sub

    Sub SyncronizeTotemOnBackOffice()
        Using contextTotem As New DbStructure.TotemDbContext()
            Dim OrdersToSent = contextTotem.Orders.ToList()
            Dim OrderDetailsToSent = contextTotem.OrderDetails.ToList()
            Dim SummaryToSent = contextTotem.Summaries.ToList()

            Using contextBackOffice As New DbStructure.BackOfficeDbContext()
                For Each order In OrdersToSent
                    Dim OrderToSync As New DbStructure.Orders() With {
                        .OrderCompleted = order.OrderCompleted,
                        .OrderDate = order.OrderDate,
                        .OrderInsertDate = order.OrderInsertDate,
                        .OrderInsertUser = order.OrderInsertUser,
                        .OrderModificationDate = order.OrderModificationDate,
                        .OrderModificationUser = order.OrderModificationUser
                    }
                    contextBackOffice.Orders.Add(OrderToSync)
                    contextBackOffice.SaveChanges()

                    For Each Details In OrderDetailsToSent

                        If order.IdOrders = Details.IdOrder Then

                            Dim OrderDetailsToSync As New DbStructure.OrderDetails() With {
                                .IdOrder = OrderToSync.IdOrders,
                                .IdProduct = Details.IdProduct,
                                .OrderQuantity = Details.OrderQuantity
                            }
                            contextBackOffice.OrderDetails.Add(OrderDetailsToSync)


                        End If
                    Next
                    contextBackOffice.SaveChanges()
                Next
                For Each Summary In SummaryToSent
                    Dim existingSummary = contextBackOffice.Summaries.SingleOrDefault(Function(s) s.IdProduct = Summary.IdProduct AndAlso s.RegistrationDate = Date.Today)

                    If existingSummary IsNot Nothing Then
                        existingSummary.TotalQuantity += Summary.TotalQuantity
                        existingSummary.TotalPrice += Summary.TotalPrice
                    Else
                        Dim newSummary As New DbStructure.Summaries With
                        {
                            .IdProduct = Summary.IdProduct,
                            .RegistrationDate = Date.Now,
                            .TotalQuantity = Summary.TotalQuantity,
                            .TotalPrice = Summary.TotalPrice
                        }
                        contextBackOffice.Summaries.Add(newSummary)
                    End If
                    contextBackOffice.SaveChanges()
                Next


                contextTotem.OrderDetails.RemoveRange(OrderDetailsToSent)
                contextTotem.Orders.RemoveRange(OrdersToSent)
                contextTotem.Summaries.RemoveRange(SummaryToSent)
                contextTotem.SaveChanges()
            End Using


        End Using
    End Sub

    Private Sub B_SendWebService_Click(sender As Object, e As EventArgs) Handles B_SendWebService.Click

        If Not IsWebServiceRunning Then
            Return
        End If
        Dim CompositObject As New List(Of Object)
        Dim url As String = "http://localhost:81/ReceiveOrder/"
        Dim jsonData As String
        Using context As New DbStructure.TotemDbContext()
            Dim orders = context.Orders.ToList()
            If orders.Count < 1 Then
                Return
            End If
            For Each order In orders
                Dim details = context.OrderDetails.Where(Function(cod) cod.IdOrder = order.IdOrders).ToList()
                Dim CompositJson = New With
                    {
                        Key .OrderJson = order,
                        Key .OrderDetailsJSON = details
                    }
                CompositObject.Add(CompositJson)
                context.OrderDetails.RemoveRange(details)
            Next
            Dim serializer As New JavaScriptSerializer()
            jsonData = serializer.Serialize(CompositObject)

            context.Orders.RemoveRange(orders)


            context.SaveChanges()
        End Using

        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"

        Dim byteData As Byte() = Encoding.UTF8.GetBytes(jsondata)
        request.ContentLength = byteData.Length

        Using stream = request.GetRequestStream()
            stream.Write(byteData, 0, byteData.Length)
        End Using

    End Sub

    Private Async Sub B_StartWebService_Click(sender As Object, e As EventArgs) Handles B_WebServiceOnOff.Click

        If IsWebServiceRunning Then
            IsWebServiceRunning = False
            B_WebServiceOnOff.Text = "START Web Service"
            L_OnOff.BackColor = Color.Red
            webService.StopWebService()
        Else
            IsWebServiceRunning = True
            B_WebServiceOnOff.Text = "STOP Web Service"
            L_OnOff.BackColor = Color.Green
            Await webService.StartWebService("http://localhost:81/ReceiveOrder/")

        End If

    End Sub

    Private Sub IWebServiceHandler_OnMessageReceived(jsonBody As String) Implements WebService.IWebServiceHandler.OnMessageReceived
        Dim deserializer As New JavaScriptSerializer()
        Dim jsonData = deserializer.Deserialize(Of List(Of Object))(jsonBody)
        For Each Jsosn In jsonData
            Dim orderJson = Jsosn("OrderJson")
            Dim orderDetailsJson = Jsosn("OrderDetailsJSON")
            Using context As New DbStructure.BackOfficeDbContext()
                Dim newOrder As New DbStructure.Orders() With {
                    .OrderCompleted = orderJson("OrderCompleted"),
                    .OrderDate = orderJson("OrderDate"),
                    .OrderInsertDate = orderJson("OrderInsertDate"),
                    .OrderInsertUser = orderJson("OrderInsertUser"),
                    .OrderModificationDate = orderJson("OrderModificationDate"),
                    .OrderModificationUser = orderJson("OrderModificationUser")
                }
                context.Orders.Add(newOrder)
                context.SaveChanges()
                For Each detail In orderDetailsJson
                    Dim newDetail As New DbStructure.OrderDetails() With {
                        .IdOrder = newOrder.IdOrders,
                        .IdProduct = detail("IdProduct"),
                        .OrderQuantity = detail("OrderQuantity")
                    }
                    context.OrderDetails.Add(newDetail)
                Next
                context.SaveChanges()
            End Using
        Next
    End Sub

    Private Sub F_Synchronize_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If webService.Listener IsNot Nothing AndAlso webService.Listener.IsListening Then
            webService.StopWebService()
        End If
    End Sub
End Class