Imports System.Data.Entity.Core.Mapping
Imports System.IO
Imports System.Net
Imports System.Runtime.Remoting.Contexts
Imports System.ServiceProcess
Imports System.Threading.Tasks
Imports System.Web.Script.Serialization
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ListView
Imports System.IdentityModel.Tokens.Jwt
Imports Microsoft.IdentityModel.Tokens
Imports System.Security.Claims
Imports System.Text

Public Class MyService
    Inherits ServiceBase

    Private ListenerTask As Task
    Private Listener As HttpListener

    Protected Overrides Sub OnStart(args() As String)

        'Ignore certificate validation
        ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) True

        'Setup the listener and add all the prefixes
        Listener = New HttpListener()
        Listener.Prefixes.Clear()
        Listener.Prefixes.Add("https://localhost:82/")
        Listener.Prefixes.Add("https://localhost:82/swagger-ui/")
        Listener.Prefixes.Add("https://localhost:82/login/")
        Listener.Prefixes.Add("https://localhost:82/getallproducts/")
        Listener.Prefixes.Add("https://localhost:82/getoldorder/")
        Listener.Prefixes.Add("https://localhost:82/increasedetail/")
        Listener.Prefixes.Add("https://localhost:82/decreasedetail/")
        Listener.Prefixes.Add("https://localhost:82/deletedetail/")
        Listener.Prefixes.Add("https://localhost:82/deletealldetails/")
        Listener.Prefixes.Add("https://localhost:82/newdetails/")
        Listener.Prefixes.Add("https://localhost:82/createorder/")
        Listener.Start()

        ' Start the async listening loop
        ListenerTask = Task.Run(AddressOf ListenLoop)
    End Sub

    Protected Overrides Sub OnStop()
        If Listener IsNot Nothing AndAlso Listener.IsListening Then
            Listener.Stop()
            Listener.Close()
        End If
    End Sub

    ''' <summary>
    ''' JWT Token generator
    ''' </summary>
    Private Function GenerateJwt(username As String) As String

        Dim secretKey = "01234567891011121314151617181920"
        Dim key = New SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        Dim credentials = New SigningCredentials(key, SecurityAlgorithms.HmacSha256)

        'Define claims in the token (subject, unique ID, issued-at)
        Dim claims = New List(Of Claim) From {
            New Claim(JwtRegisteredClaimNames.Sub, username),
            New Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            New Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        }

        'Create and return the signed JWT
        Dim token = New JwtSecurityToken(
            issuer:="TotemService",
            audience:="TotemClient",
            claims:=claims,
            notBefore:=Date.UtcNow,
            expires:=Date.UtcNow.AddHours(1),
            signingCredentials:=credentials
        )

        Return New JwtSecurityTokenHandler().WriteToken(token)

    End Function

    ''' <summary>
    ''' Main asynchronous loop for handling HTTP requests
    ''' </summary>
    Private Async Function ListenLoop() As Task
        While True
            Try
                Dim context = Await Listener.GetContextAsync()
                ProcessRequest(context)
            Catch ex As Exception
                ' Optional: log exception
            End Try
        End While
    End Function

    ''' <summary>
    ''' Check if the token is valid
    ''' </summary>
    Private Function IsTokenValid(token As String) As Boolean

        Try

            Dim secretKey = "01234567891011121314151617181920"
            Dim key = New SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            Dim tokenHandler As New JwtSecurityTokenHandler()

            'Define validation parameters
            Dim validationParams As New TokenValidationParameters With {
                .ValidateIssuer = True,
                .ValidIssuer = "TotemService",
                .ValidateAudience = True,
                .ValidAudience = "TotemClient",
                .ValidateIssuerSigningKey = True,
                .IssuerSigningKey = key,
                .ValidateLifetime = True,
                .ClockSkew = TimeSpan.Zero
            }

            'Check the token
            Dim principal As ClaimsPrincipal = tokenHandler.ValidateToken(token, validationParams, Nothing)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    ''' <summary>
    ''' Checks for Bearer Authorization header
    ''' </summary>
    Private Function CheckBearerToken(request As HttpListenerRequest) As Boolean
        Dim authHeader = request.Headers("Authorization")
        If String.IsNullOrEmpty(authHeader) OrElse Not authHeader.StartsWith("Bearer ") Then Return False
        Dim token = authHeader.Substring(7).Trim()
        Return IsTokenValid(token)
    End Function

    ''' <summary>
    ''' Main request handler
    ''' </summary>
    Private Sub ProcessRequest(context As HttpListenerContext)

        Dim request = context.Request
        Dim response = context.Response

        'Handle CORS
        If request.HttpMethod = "OPTIONS" Then
            response.StatusCode = 200
            response.Headers.Add("Access-Control-Allow-Origin", "*")
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS, DELETE, PUT, PATCH")
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization")
            response.Headers.Remove("Content-Security-Policy")
            response.Close()
            Return
        End If

        'Retrive the username for the token
        Dim authHeader As String = request.Headers("Authorization")
        Dim username As String = ""
        If Not String.IsNullOrEmpty(authHeader) AndAlso authHeader.StartsWith("Basic ") Then
            Dim encoded = authHeader.Substring(6)
            Dim decoded = Text.Encoding.UTF8.GetString(Convert.FromBase64String(encoded))
            Dim parts = decoded.Split(":"c)
            If parts.Length = 2 Then
                username = parts(0)
            End If
        End If

        'Get the requested URL path
        Dim path = request.Url.AbsolutePath.ToLower().Trim("/"c)

        'Gneretare the token
        If path = "login" Then
            Dim loginResponse As String = HandleLogin(username)
            Dim loginBuffer() As Byte = System.Text.Encoding.UTF8.GetBytes(loginResponse)
            response.ContentType = "application/json"
            response.ContentLength64 = loginBuffer.Length
            response.StatusCode = If(loginResponse.Contains("token"), 200, 401)
            response.Headers.Add("Access-Control-Allow-Origin", "*")
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS, DELETE, PUT, PATCH")
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization")
            response.OutputStream.Write(loginBuffer, 0, loginBuffer.Length)
            response.OutputStream.Close()
            Return
        End If

        ' Serve Swagger UI statici
        If path.StartsWith("swagger-ui") Then
            Dim relativePath = path.Substring("swagger-ui".Length).TrimStart("/"c)
            Dim fullPath = System.IO.Path.Combine("C:\Users\Gabriele\Desktop\SQL\ClassTools\Es_30WINS\TotemSwaggerUI", relativePath)

            If String.IsNullOrWhiteSpace(relativePath) Then
                fullPath = "C:\Users\Gabriele\Desktop\SQL\ClassTools\Es_30WINS\TotemSwaggerUI\index.html"
            End If

            If ServeStaticFile(fullPath, response) Then Return
        End If

        If path = "swagger.json" Then
            Dim swaggerJsonPath = "C:\Users\Gabriele\Desktop\SQL\ClassTools\Es_30WINS\TotemSwaggerUI\swagger.json"
            If ServeStaticFile(swaggerJsonPath, response) Then Return
        End If

        'Check the Token to block the request
        If Not CheckBearerToken(request) Then
            response.StatusCode = 401
            response.Close()
            Return
        End If

        'Apply security headers
        response.Headers.Add("Access-Control-Allow-Origin", "*")
        response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS, DELETE, PUT, PATCH")
        response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization")
        response.Headers.Remove("Content-Security-Policy")
        response.Headers("Content-Security-Policy") = "default-src 'self'; connect-src 'self' https://localhost:82"

        'Read request body
        Dim requestBodyOther As String = Nothing
        Using reader As New StreamReader(request.InputStream, request.ContentEncoding)
            requestBodyOther = reader.ReadToEnd()
        End Using

        'Route the request
        Dim responseString As String = ""
        Select Case path
            Case "getallproducts"
                responseString = HandleGetAllProducts(requestBodyOther)
            Case "getoldorder"
                responseString = HandleGetOldOrder(requestBodyOther)
            Case "increasedetail"
                responseString = HandleIncreaseDetail(requestBodyOther)
            Case "decreasedetail"
                responseString = HandleDecreaseDetail(requestBodyOther)
            Case "deletedetail"
                responseString = HandleDeleteDetail(requestBodyOther)
            Case "deletealldetails"
                responseString = HandleDeleteAllDetails(requestBodyOther)
            Case "newdetails"
                responseString = HandleNewDetails(requestBodyOther)
            Case "createorder"
                responseString = HandleCreateOrder(requestBodyOther)
            Case "swagger.json"
                Dim json = File.ReadAllText("C:\Users\Gabriele\Desktop\SQL\ClassTools\Es_30WINS\TotemSwaggerUI\swagger.json")
                Dim swaggerBuffer() As Byte = System.Text.Encoding.UTF8.GetBytes(json)
                response.ContentType = "application/json"
                response.ContentLength64 = swaggerBuffer.Length
                response.StatusCode = 200
                response.OutputStream.Write(swaggerBuffer, 0, swaggerBuffer.Length)
                response.OutputStream.Close()
                Return
            Case Else
                responseString = "{""status"": ""error"",""message"":""Not found""}"
        End Select

        'Send response
        response.StatusCode = If(responseString.Contains("error"), 400, 200)
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
    ''' Generate the token
    ''' </summary>
    Private Function HandleLogin(username As String) As String
        Dim token = GenerateJwt(username)
        Return "{""token"":""" & token & """}"
    End Function

    'Swagger
    Private Function ServeStaticFile(filePath As String, response As HttpListenerResponse) As Boolean
        Try
            If Not File.Exists(filePath) Then
                Return False
            End If

            Dim extension = Path.GetExtension(filePath).ToLower()
            Dim contentType As String = "application/octet-stream"
            Select Case extension
                Case ".html"
                    contentType = "text/html"
                Case ".css"
                    contentType = "text/css"
                Case ".js"
                    contentType = "application/javascript"
                Case ".json"
                    contentType = "application/json"
                Case ".png"
                    contentType = "image/png"
            End Select

            Dim buffer As Byte() = File.ReadAllBytes(filePath)
            response.ContentType = contentType
            response.ContentLength64 = buffer.Length
            response.StatusCode = 200
            response.OutputStream.Write(buffer, 0, buffer.Length)
            response.OutputStream.Close()
            Return True
        Catch ex As Exception
            response.StatusCode = 500
            response.OutputStream.Close()
            Return True
        End Try
    End Function

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

            Return "{""status"":""success"",""message"":""Detail deleted successfully""}"
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
                If filter Is Nothing Then
                    Return "{""status"":""error""}"
                End If
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
