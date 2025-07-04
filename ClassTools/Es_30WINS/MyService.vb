Imports System.Data.Entity.Core.Mapping
Imports System.IO
Imports System.Net
Imports System.Runtime.Remoting.Contexts
Imports System.ServiceProcess
Imports System.Threading.Tasks
Imports System.Web.Script.Serialization
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ListView
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Tab
Imports ClassTools

'Public Class SslConfig
'    Public Const SslAppId As String = "{e5557ab2-260b-44c1-b81c-b955cf7fcd37}"
'End Class
Public Class MyService
    Inherits ServiceBase

    'Listener to handle HTTP requests
    Private Listener As HttpListener

    'Task to run the listener loop
    Private ListenerTask As Task

    ''' <summary>
    ''' Called when the service is started. It starts the listener add the prefixes
    ''' </summary>
    Protected Overrides Sub OnStart(args() As String)

        ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True

        'Initialize the HttpListener and add the prefixes to listen fro requests
        Listener = New HttpListener()
        Listener.Prefixes.Clear()
        Listener.Prefixes.Add("https://localhost:82/getallproducts/")
        Listener.Prefixes.Add("https://localhost:82/getoldorder/")
        Listener.Prefixes.Add("https://localhost:82/increasedetail/")
        Listener.Prefixes.Add("https://localhost:82/decreasedetail/")
        Listener.Prefixes.Add("https://localhost:82/deletedetail/")
        Listener.Prefixes.Add("https://localhost:82/deletealldetails/")
        Listener.Prefixes.Add("https://localhost:82/newdetails/")
        Listener.Prefixes.Add("https://localhost:82/createorder/")
        Listener.Start()

        'Start the listener loop in a separate task
        ListenerTask = Task.Run(AddressOf ListenLoop)

    End Sub

    ''' <summary>
    ''' Called when the service is stopped. It stops the listener and closes it
    ''' </summary>
    Protected Overrides Sub OnStop()
        If Listener IsNot Nothing AndAlso Listener.IsListening Then
            Listener.Stop()
            Listener.Close()
        End If
    End Sub

    ''' <summary>
    ''' Loop to listen for incoming HTTP requests asynchronously
    ''' </summary>
    Private Async Function ListenLoop() As Task

        'Loop to continuously listen fot incoming requests
        While True
            Try
                Dim context = Await Listener.GetContextAsync()
                ProcessRequest(context)
            Catch ex As Exception
            End Try
        End While

    End Function

    ''' <summary>
    ''' Check the Basic authentication
    ''' </summary>
    Private Function CheckBasicAuth(request As HttpListenerRequest) As Boolean

        Dim authHeader = request.Headers("Authorization")
        If String.IsNullOrEmpty(authHeader) OrElse Not authHeader.StartsWith("Basic ") Then
            Return False
        End If

        'Extract the base64 part after Basic
        Dim encodedCredentials = authHeader.Substring(6).Trim()
        Dim credentials As String
        Try
            Dim credentialBytes = Convert.FromBase64String(encodedCredentials)
            credentials = System.Text.Encoding.UTF8.GetString(credentialBytes)
        Catch ex As Exception
            Return False
        End Try
        Dim parts = credentials.Split(":"c)
        If parts.Length <> 2 Then Return False
        Dim username = parts(0)
        Dim password = parts(1)

        'Username and Password check
        Return username = "admin" AndAlso password = "admin"
    End Function


    ''' <summary>
    ''' Processes the incoming HTTP request based on the URL path
    ''' </summary>
    Private Sub ProcessRequest(context As HttpListenerContext)
        Dim request = context.Request
        Dim response = context.Response

        If request.HttpMethod = "OPTIONS" Then
            response.StatusCode = 200
            response.Headers.Add("Access-Control-Allow-Origin", "*")
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS, DELETE, PUT, PATCH")
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization")
            response.Close()
            Return
        End If

        If Not CheckBasicAuth(request) Then
            response.StatusCode = 401
            response.Headers.Add("WWW-Authenticate", "Basic realm=""MyRealm""")
            response.Headers.Add("Access-Control-Allow-Origin", "*")
            response.Headers.Add("Access-Control-Allow-Methods", "GET")
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization")
            response.Close()
            Return
        End If

        'Header CORS
        response.Headers.Add("Access-Control-Allow-Origin", "*")
        response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS, DELETE, PUT, PATCH")
        response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization")

        Dim responseString As String = ""

        'Read the body
        Dim body As String = Nothing
        Using reader As New StreamReader(request.InputStream, request.ContentEncoding)
            body = reader.ReadToEnd()
        End Using

        'Retrive the absolute path and use it to select the function
        Dim path = request.Url.AbsolutePath.ToLower().Trim("/"c)
        Select Case path
            Case "getallproducts"
                responseString = HandleGetAllProducts(body)
            Case "getoldorder"
                responseString = HandleGetOldOrder(body)
            Case "increasedetail"
                responseString = HandleIncreaseDetail(body)
            Case "decreasedetail"
                responseString = HandleDecreaseDetail(body)
            Case "deletedetail"
                responseString = HandleDeleteDetail(body)
            Case "deletealldetails"
                responseString = HandleDeleteAllDetails(body)
            Case "newdetails"
                responseString = HandleNewDetails(body)
            Case "createorder"
                responseString = HandleCreateOrder(body)
            Case Else
                responseString = "{""status"": ""error"",""message"":""Not found""}"
        End Select


        If responseString.Contains("error") Then
            response.StatusCode = 400
        Else
            response.StatusCode = 200

        End If



        'Serialize the answer
        Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(responseString)
        response.ContentType = "application/json"
        response.ContentLength64 = buffer.Length

        Try
            response.OutputStream.Write(buffer, 0, buffer.Length)
        Finally
            response.OutputStream.Close()
        End Try
    End Sub



    ''' <summary>
    ''' Handles the creation of a new order
    ''' </summary>
    Private Function HandleCreateOrder(body As String) As String
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
            Dim ProductsCart = context.CopyOrderDetails.Where(Function(cod) cod.OrderQuantity > 0).ToList()
            For Each Item In ProductsCart
                Dim OrderDetails As New DbStructure.OrderDetails With
                {
                    .IdOrder = Order.IdOrders,
                    .IdProduct = Item.IdProduct,
                    .OrderQuantity = Item.OrderQuantity
                }
                context.OrderDetails.Add(OrderDetails)
                context.SaveChanges()

                'Create or update the summary fr the product
                Dim IdProduct As Integer = Item.IdProduct
                Dim product = context.Products.FirstOrDefault(Function(p) p.IdProduct = Item.IdProduct)
                Dim existingSummary = context.Summaries.SingleOrDefault(Function(s) s.IdProduct = IdProduct AndAlso s.RegistrationDate = Date.Today)
                If existingSummary IsNot Nothing Then
                    existingSummary.TotalQuantity += Item.OrderQuantity
                    existingSummary.TotalPrice += product.ProductPrice * Item.OrderQuantity
                Else
                    Dim newSummary As New DbStructure.Summaries With
                    {
                        .IdProduct = Item.IdProduct,
                        .RegistrationDate = Date.Now,
                        .TotalQuantity = Item.OrderQuantity,
                        .TotalPrice = product.ProductPrice * Item.OrderQuantity
                    }
                    context.Summaries.Add(newSummary)
                End If
                context.SaveChanges()
            Next

        End Using
        Try
            Return "{""status"":""success"",""message"":""Order created successfully""}"
        Catch ex As Exception
            Return "{""status"":""error"",""message"":""" & ex.Message.Replace("""", "'") & """}"
            End Try
    End Function

    ''' <summary>
    ''' Handles the addition of new details to an order
    ''' </summary>
    Private Function HandleNewDetails(body As String) As String
        Try
            Using context As New DbStructure.TotemDbContext()

                'Deserialize the request body to get the order and product Ids
                Dim serializer As New JavaScriptSerializer()
                Dim filter As IDs = serializer.Deserialize(Of IDs)(body)
                Dim IdOrder As Integer = filter.IdOrder
                Dim IdProduct As Integer = filter.IdProduct

                'Create a new copy order detail
                Dim NewOrderDetail As New DbStructure.CopyOrderDetails With {
                .IdOrder = IdOrder,
                .IdProduct = IdProduct,
                .OrderQuantity = 1
            }
                context.CopyOrderDetails.Add(NewOrderDetail)
                context.SaveChanges()

            End Using

            Return "{""status"":""success"",""message"":""Detail created successfully""}"
        Catch ex As Exception
            Return "{""status"":""error"",""message"":""" & ex.Message.Replace("""", "'") & """}"
        End Try

    End Function

    ''' <summary>
    ''' Handles the deletion of all details for a specific order
    ''' </summary>
    Private Function HandleDeleteAllDetails(body As String) As String
        Try
            Using context As New DbStructure.TotemDbContext()

                'Deserialize the request body to get the order id
                Dim serializer As New JavaScriptSerializer()
                Dim DeleteRequest = serializer.Deserialize(Of DeleteRequest)(body)

                'Delete all details for the specified order
                Dim orders = context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = DeleteRequest.IdOrder).ToList()
                context.CopyOrderDetails.RemoveRange(orders)
                context.SaveChanges()

            End Using

            Return "{""status"":""success"",""message"":""All details deleted successfully""}"
        Catch ex As Exception
            Return "{""status"":""error"",""message"":""" & ex.Message.Replace("""", "'") & """}"
        End Try

    End Function

    ''' <summary>
    ''' Handles the deletion of a specific detail from an order
    ''' </summary>
    Private Function HandleDeleteDetail(body As String) As String
        Try
            Using context As New DbStructure.TotemDbContext()

                'Deserialize the request body to get the order and product Ids
                Dim serializer As New JavaScriptSerializer()
                Dim filter As IDs = serializer.Deserialize(Of IDs)(body)
                Dim IdOrder As Integer = filter.IdOrder
                Dim IdProduct As Integer = filter.IdProduct

                'Retrive the existing order detail and remove it
                Dim existingOrder = context.CopyOrderDetails.FirstOrDefault(Function(cod) cod.IdOrder = IdOrder AndAlso cod.IdProduct = IdProduct)
                context.CopyOrderDetails.Remove(existingOrder)
                context.SaveChanges()

            End Using

            Return "{""status"":""success"",""message"":""detail deleted successfully""}"
        Catch ex As Exception
            Return "{""status"":""error"",""message"":""" & ex.Message.Replace("""", "'") & """}"
        End Try
    End Function

    ''' <summary>
    ''' Handles the increase of the detail quantity for a specific order and product
    ''' </summary>
    Private Function HandleIncreaseDetail(body As String) As String
        Return DetailSearch(body, 1)
    End Function

    ''' <summary>
    ''' Handles the decrease of the detail quantity for a specific order and product
    ''' </summary>
    Private Function HandleDecreaseDetail(body As String) As String
        Return DetailSearch(body, -1)
    End Function

    ''' <summary>
    ''' Searches for an order detail and updates the quantity based on the provided quantity parameter
    ''' </summary>
    Private Function DetailSearch(body As String, quantity As Integer) As String
        Try
            Using context As New DbStructure.TotemDbContext()

                'Deserialize the request body to get the order and product Ids
                Dim serializer As New JavaScriptSerializer()
                Dim filter As IDs = serializer.Deserialize(Of IDs)(body)
                Dim IdOrder As Integer = filter.IdOrder
                Dim IdProduct As Integer = filter.IdProduct

                'Retrieve the existing order detail and update the quantity
                Dim existingOrder = context.CopyOrderDetails.FirstOrDefault(Function(cod) cod.IdOrder = IdOrder AndAlso cod.IdProduct = IdProduct)
                If existingOrder Is Nothing Then
                    Return "{""status"":""error"",""message"":""Order detail not found""}"
                End If
                existingOrder.OrderQuantity = existingOrder.OrderQuantity + quantity
                context.SaveChanges()

            End Using

            Return "{""status"":""success"",""message"":""Quantity modified successfully""}"
        Catch ex As Exception
            Return "{""status"":""error"",""message"":""" & ex.Message.Replace("""", "'") & """}"
        End Try
    End Function

    ''' <summary>
    ''' Handles the retrieval of the last order and its details
    ''' </summary>
    Private Function HandleGetOldOrder(body As String) As String
        Try

            Using context As New DbStructure.TotemDbContext()
                'Retrieve the last order and its details
                Dim lastOrder = context.CopyOrders.OrderByDescending(Function(co) co.IdOrders).FirstOrDefault()
                Dim details = context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = lastOrder.IdOrders AndAlso cod.OrderQuantity > 0).Select(Function(p) New With {
                    Key .IdProduct = p.IdProduct,
                    Key .IdOrder = p.IdOrder,
                    Key .OrderQuantity = p.OrderQuantity
                }).ToList()

                'Serialize the result in JSON format
                Dim result = New With
                {
                    Key .IdOrder = lastOrder.IdOrders,
                    Key .Details = details
                }
                Dim serializer As New JavaScriptSerializer()
                Return serializer.Serialize(result)

            End Using

        Catch ex As Exception
            Return "{""status"":""error"",""message"":""" & ex.Message.Replace("""", "'") & """}"
            End Try

    End Function

    ''' <summary>
    ''' Handles the retrieval of all products from the database
    ''' </summary>
    Private Function HandleGetAllProducts(body As String) As String

        Try
            Using context As New DbStructure.TotemDbContext()

                'Retrieve all products from the database
                Dim productList = context.Products.Select(Function(p) New With
                {
                    Key .IdProduct = p.IdProduct,
                    Key .ProductName = p.ProductName,
                    Key .ProductCategory = p.ProductCategory,
                    Key .ProductPrice = p.ProductPrice,
                    Key .ProductPicture = p.ProductPicture
                }).ToList()

                'Serialize the product list in JSON format
                Dim serializer As New JavaScriptSerializer()
                Return serializer.Serialize(productList)

            End Using

        Catch ex As Exception
            Return "{""status"":""error"",""message"":""" & ex.Message.Replace("""", "'") & """}"

        End Try

    End Function

    ''' <summary>
    ''' Method to run the service in debug mode
    ''' </summary>
    Public Sub OnDebug()
        OnStart(Nothing)
    End Sub


    ''' <summary>
    ''' Class to map the request structure for order and product IDs
    ''' </summary>
    Public Class IDs
        Public Property IdOrder As Integer
        Public Property IdProduct As Integer
    End Class

    ''' <summary>
    ''' Class to map the delete request structure
    ''' </summary>
    Private Class DeleteRequest
        Public Property IdOrder As Integer
    End Class

End Class
