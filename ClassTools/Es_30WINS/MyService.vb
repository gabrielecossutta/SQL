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

        'ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True

        'Initialize the HttpListener and add the prefixes to listen fro requests
        Listener = New HttpListener()
        Listener.Prefixes.Clear()
        Listener.Prefixes.Add("http://localhost:82/getallproducts/")
        Listener.Prefixes.Add("http://localhost:82/getoldorder/")
        Listener.Prefixes.Add("http://localhost:82/increasedetail/")
        Listener.Prefixes.Add("http://localhost:82/decreasedetail/")
        Listener.Prefixes.Add("http://localhost:82/deletedetail/")
        Listener.Prefixes.Add("http://localhost:82/deletealldetails/")
        Listener.Prefixes.Add("http://localhost:82/newdetails/")
        Listener.Prefixes.Add("http://localhost:82/createorder/")
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
    ''' Processes the incoming HTTP request based on the URL path
    ''' </summary>
    Private Sub ProcessRequest(context As HttpListenerContext)

        'Get the request and response objects from the context
        Dim request = context.Request
        Dim response = context.Response

        'Add CORS(Cross-Origin Resource Sharing) headers to the response
        response.AddHeader("Access-Control-Allow-Origin", "*")
        response.AddHeader("Access-Control-Allow-Methods", "POST, GET, OPTIONS")
        response.AddHeader("Access-Control-Allow-Headers", "Content-Type")

        'If the request is an OPTIONS request, respond with 200 OK and return
        If request.HttpMethod = "OPTIONS" Then
            response.StatusCode = 200
            response.Close()
            Return
        End If

        Dim responseString As String = ""

        'Read the request body
        Dim body As String = Nothing
        Using reader As New StreamReader(request.InputStream, request.ContentEncoding)
            body = reader.ReadToEnd()
        End Using

        'Get the absolute path of the request URL and convert it to lowercase, then remove the / and use it to determine the action to take
        Dim path = request.Url.AbsolutePath.ToLower().Trim("/"c)
        Select Case path
            Case "getallproducts"
                responseString = HandleGetAllProducts(body)
            Case "getoldorder"
                responseString = HandleGetOldOrder(body)
            Case "increasedetail"
                HandleIncreaseDetail(body)
            Case "decreasedetail"
                HandleDecreaseDetail(body)
            Case "deletedetail"
                HandleDeleteDetail(body)
            Case "deletealldetails"
                HandleDeleteAllDetails(body)
            Case "newdetails"
                HandleNewDetails(body)
            Case "createorder"
                HandleCreateOrder(body)
            Case Else
                response.StatusCode = 404
                responseString = "ERROR"
        End Select

        'Create the response string and write it to the response output stream
        Dim buffer() As Byte = System.Text.Encoding.UTF8.GetBytes(responseString)
        response.ContentLength64 = buffer.Length
        response.ContentType = "application/json"
        response.OutputStream.Write(buffer, 0, buffer.Length)
        response.OutputStream.Close()

    End Sub

    ''' <summary>
    ''' Handles the creation of a new order
    ''' </summary>
    Private Sub HandleCreateOrder(body As String)
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
    End Sub

    ''' <summary>
    ''' Handles the addition of new details to an order
    ''' </summary>
    Private Sub HandleNewDetails(body As String)

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

    End Sub

    ''' <summary>
    ''' Handles the deletion of all details for a specific order
    ''' </summary>
    Private Sub HandleDeleteAllDetails(body As String)

        Using context As New DbStructure.TotemDbContext()

            'Deserialize the request body to get the order id
            Dim serializer As New JavaScriptSerializer()
            Dim DeleteRequest = serializer.Deserialize(Of DeleteRequest)(body)

            'Delete all details for the specified order
            Dim orders = context.CopyOrderDetails.Where(Function(cod) cod.IdOrder = DeleteRequest.IdOrder).ToList()
            context.CopyOrderDetails.RemoveRange(orders)
            context.SaveChanges()

        End Using

    End Sub

    ''' <summary>
    ''' Handles the deletion of a specific detail from an order
    ''' </summary>
    Private Sub HandleDeleteDetail(body As String)

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
    End Sub

    ''' <summary>
    ''' Handles the increase of the detail quantity for a specific order and product
    ''' </summary>
    Private Sub HandleIncreaseDetail(body As String)
        DetailSearch(body, 1)
    End Sub

    ''' <summary>
    ''' Handles the decrease of the detail quantity for a specific order and product
    ''' </summary>
    Private Sub HandleDecreaseDetail(body As String)
        DetailSearch(body, -1)
    End Sub

    ''' <summary>
    ''' Searches for an order detail and updates the quantity based on the provided quantity parameter
    ''' </summary>
    Private Sub DetailSearch(body As String, quantity As Integer)
        Using context As New DbStructure.TotemDbContext()

            'Deserialize the request body to get the order and product Ids
            Dim serializer As New JavaScriptSerializer()
            Dim filter As IDs = serializer.Deserialize(Of IDs)(body)
            Dim IdOrder As Integer = filter.IdOrder
            Dim IdProduct As Integer = filter.IdProduct

            'Retrieve the existing order detail and update the quantity
            Dim existingOrder = context.CopyOrderDetails.FirstOrDefault(Function(cod) cod.IdOrder = IdOrder AndAlso cod.IdProduct = IdProduct)
            existingOrder.OrderQuantity = existingOrder.OrderQuantity + quantity
            context.SaveChanges()

        End Using
    End Sub

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
