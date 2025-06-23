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

    'Flag to indicate if the web service is running
    Dim IsWebServiceRunning As Boolean = False

    'HttpListener instance to listen for incoming requests
    Private Listener As HttpListener

    'Web service instance to handle incoming requests
    Dim webService As New WebService(Me)


    ''' <summary>
    ''' Synchronize the products from the backoffice database to the totem database
    ''' </summary>
    Private Sub B_BackToTotem_Click(sender As Object, e As EventArgs) Handles B_BackToTotem.Click
        SyncronizeBackOfficeOnTotem()
    End Sub

    ''' <summary>
    ''' Synchronizes the products from the backoffice database to the totem database
    ''' </summary>
    Sub SyncronizeBackOfficeOnTotem()

        Using contextBackOffice As New DbStructure.BackOfficeDbContext()

            'Retrieve all products from the backoffice database
            Dim productsToSync = contextBackOffice.Products.ToList()

            Using contextTotem As New DbStructure.TotemDbContext()

                For Each product In productsToSync

                    'Find the existing product in the totem database by the name
                    Dim existingProduct = contextTotem.Products.FirstOrDefault(Function(p) p.ProductName = product.ProductName)

                    'If the product exists, update its details, otherwise create a new product 
                    If existingProduct IsNot Nothing Then

                        'If the existing product's modification date is not todat, update all the product details
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

                'Create a new list of products to check for deletion and filter by product modification date Different from today
                Dim productsToCheck = contextTotem.Products.ToList()
                Dim productsToDelete = productsToCheck.Where(Function(ptc) ptc.ProductModificationDate <> Date.Now.Date)
                If productsToDelete.Count > 0 Then
                    contextTotem.Products.RemoveRange(productsToDelete)
                End If

            End Using
        End Using
    End Sub

    ''' <summary>
    ''' Synchronizes the orders from the totem database to the backoffice database
    ''' </summary>
    Private Sub B_TotemToBackOffice_Click(sender As Object, e As EventArgs) Handles B_TotemToBackOffice.Click

        SyncronizeTotemOnBackOffice()

    End Sub

    ''' <summary>
    ''' Synchronizes the orders from the Totem database to the backoffice database
    ''' </summary>
    Sub SyncronizeTotemOnBackOffice()

        Using contextTotem As New DbStructure.TotemDbContext()

            'Retrieve all orders, details and summaries from the totem database
            Dim OrdersToSent = contextTotem.Orders.ToList()
            Dim OrderDetailsToSent = contextTotem.OrderDetails.ToList()
            Dim SummaryToSent = contextTotem.Summaries.ToList()

            Using contextBackOffice As New DbStructure.BackOfficeDbContext()

                'For each order create a new order object and populate it with the data from the totem database
                For Each order In OrdersToSent
                    Dim OrderToSync As New DbStructure.Orders() With
                    {
                        .OrderCompleted = order.OrderCompleted,
                        .OrderDate = order.OrderDate,
                        .OrderInsertDate = order.OrderInsertDate,
                        .OrderInsertUser = order.OrderInsertUser,
                        .OrderModificationDate = order.OrderModificationDate,
                        .OrderModificationUser = order.OrderModificationUser
                    }
                    contextBackOffice.Orders.Add(OrderToSync)
                    contextBackOffice.SaveChanges()

                    'Create a new details for each order and populate it with the data from the totem database
                    For Each Details In OrderDetailsToSent
                        If order.IdOrders = Details.IdOrder Then
                            Dim OrderDetailsToSync As New DbStructure.OrderDetails() With
                            {
                                .IdOrder = OrderToSync.IdOrders,
                                .IdProduct = Details.IdProduct,
                                .OrderQuantity = Details.OrderQuantity
                            }
                            contextBackOffice.OrderDetails.Add(OrderDetailsToSync)
                        End If
                    Next
                    contextBackOffice.SaveChanges()

                Next

                'For each summary check if the summary for the product already exists for today, if it exists, update summary, or else create a new summary
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

                'Remove the old orders, details and summary
                contextTotem.OrderDetails.RemoveRange(OrderDetailsToSent)
                contextTotem.Orders.RemoveRange(OrdersToSent)
                contextTotem.Summaries.RemoveRange(SummaryToSent)
                contextTotem.SaveChanges()

            End Using

        End Using

    End Sub

    ''' <summary>
    ''' Sends the current orders from the Totem database to the web service as a Json.
    ''' This method retrieves all orders and their details from the Totem database and send them as a json array
    ''' </summary>
    Private Sub B_SendWebService_Click(sender As Object, e As EventArgs) Handles B_SendWebService.Click

        '-------------------------------
        'Decomment the following lines for Es_22 |i can use the response to deactivate the protection|
        'If Not IsWebServiceRunning Then
        '    Return
        'End If
        '-------------------------------

        'Url of the web service to send the orders
        Dim url As String = "http://localhost:81/ReceiveOrder/"

        'List of objects to hold the composite order and details
        Dim CompositObject As New List(Of Object)

        'Json data to be sent
        Dim jsonData As String

        Using context As New DbStructure.TotemDbContext()

            'Retrieve all orders from the totem database
            Dim orders = context.Orders.ToList()
            If orders.Count < 1 Then
                Return
            End If

            'For each order, retrieve its details and create a composite object
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

            'Serialize the composite object to json format
            Dim serializer As New JavaScriptSerializer()
            jsonData = serializer.Serialize(CompositObject)

            context.Orders.RemoveRange(orders)
            context.SaveChanges()

        End Using

        'Send the json data to the web service
        Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json"

        'Set the content length of the request based on the json data length
        Dim byteData As Byte() = Encoding.UTF8.GetBytes(jsondata)
        request.ContentLength = byteData.Length

        'Send the json data in the request body
        Using stream = request.GetRequestStream()
            stream.Write(byteData, 0, byteData.Length)
        End Using

    End Sub

    ''' <summary>
    ''' Start or stop the web service based on its current state
    ''' </summary>
    Private Async Sub B_StartWebService_Click(sender As Object, e As EventArgs) Handles B_WebServiceOnOff.Click

        If IsWebServiceRunning Then

            'If the web service is running, stop it
            IsWebServiceRunning = False
            B_WebServiceOnOff.Text = "START Web Service"
            L_OnOff.BackColor = Color.Red
            webService.StopWebService()

        Else

            'If the web service is not running, start it
            IsWebServiceRunning = True
            B_WebServiceOnOff.Text = "STOP Web Service"
            L_OnOff.BackColor = Color.Green
            Await webService.StartWebService("http://localhost:81/ReceiveOrder/")

        End If

    End Sub

    ''' <summary>
    ''' Method called when a message is received by the web service, deserializes the json data and saves the order in the database
    ''' </summary>
    Private Sub IWebServiceHandler_OnMessageReceived(jsonBody As String) Implements WebService.IWebServiceHandler.OnMessageReceived

        'Deserialize the incoming JSON data and save it
        Dim deserializer As New JavaScriptSerializer()
        Dim jsonData = deserializer.Deserialize(Of List(Of Object))(jsonBody)

        'Save each order and its details in the database
        For Each Jsosn In jsonData

            'Extract the order and order details from the json
            Dim orderJson = Jsosn("OrderJson")
            Dim orderDetailsJson = Jsosn("OrderDetailsJSON")

            Using context As New DbStructure.BackOfficeDbContext()

                'Create a new order object and populate it with the data from the Json
                Dim newOrder As New DbStructure.Orders() With
                {
                    .OrderCompleted = orderJson("OrderCompleted"),
                    .OrderDate = orderJson("OrderDate"),
                    .OrderInsertDate = orderJson("OrderInsertDate"),
                    .OrderInsertUser = orderJson("OrderInsertUser"),
                    .OrderModificationDate = orderJson("OrderModificationDate"),
                    .OrderModificationUser = orderJson("OrderModificationUser")
                }
                context.Orders.Add(newOrder)
                context.SaveChanges()

                'Save each order detail in the database
                For Each detail In orderDetailsJson
                    Dim newDetail As New DbStructure.OrderDetails() With
                    {
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

    ''' <summary>
    ''' Handles the closing event of the form to stop the web service if it is running
    ''' </summary>
    Private Sub F_Synchronize_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If webService.Listener IsNot Nothing AndAlso webService.Listener.IsListening Then
            webService.StopWebService()
        End If
    End Sub

End Class