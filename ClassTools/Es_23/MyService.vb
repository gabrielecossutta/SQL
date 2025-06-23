Imports System.IO
Imports System.ServiceProcess
Imports System.Web.Script.Serialization

Imports System.Diagnostics
Imports ClassTools

Public Class MyService
    Inherits ServiceBase
    Implements WebService.IWebServiceHandler

    'Web service instance to handle incoming requests
    Private WebService As WebService

    'Task to start the web service asynchronously
    Private TaskServer As Task


    ''' <summary>
    ''' Initilize the service and start the listener for incoming requests
    ''' </summary>
    Protected Overrides Sub OnStart(args() As String)


        WebService = New WebService(Me)
        TaskServer = Task.Run(Async Function()
                                  Return Await WebService.StartWebService("http://localhost:81/ReceiveOrder/")
                              End Function)

    End Sub

    ''' <summary>
    ''' Stop the windows service 
    ''' </summary>
    Protected Overrides Sub OnStop()

        If WebService IsNot Nothing Then
            WebService.StopWebService()
        End If

    End Sub


    'Class to represent the structure of the incoming json data
    Public Class OrderPackage
        Public Property OrderJson As Dictionary(Of String, Object)
        Public Property OrderDetailsJSON As List(Of Dictionary(Of String, Object))
    End Class

    ''' <summary>
    ''' Method called when a message is received by the web service, deserializes the json data and save the order in the database
    ''' </summary>
    Public Sub OnMessageReceived(jsonBody As String) Implements WebService.IWebServiceHandler.OnMessageReceived

        'Deserialize the incoming Json Data and for each package save the order and its details in the database
        Dim deserializer As New JavaScriptSerializer()
        Dim jsonData = deserializer.Deserialize(Of List(Of OrderPackage))(jsonBody)
        For Each package In jsonData

            'Extract the order and order details from the package
            Dim orderJson = package.OrderJson
            Dim orderDetailsJson = package.OrderDetailsJSON


            Using context As New DbStructure.BackOfficeDbContext()

                'Create a new order object and populate it with the data from the json
                Dim newOrder As New DbStructure.Orders() With
                {
                    .OrderCompleted = Convert.ToBoolean(orderJson("OrderCompleted")),
                    .OrderDate = Convert.ToDateTime(orderJson("OrderDate")),
                    .OrderInsertDate = Convert.ToDateTime(orderJson("OrderInsertDate")),
                    .OrderInsertUser = Convert.ToString(orderJson("OrderInsertUser")),
                    .OrderModificationUser = Convert.ToString(orderJson("OrderModificationUser")),
                    .OrderModificationDate = Convert.ToDateTime(orderJson("OrderModificationDate"))
                }
                context.Orders.Add(newOrder)
                context.SaveChanges()

                'Save each order details in the database
                For Each detail In orderDetailsJson
                    Dim newDetail As New DbStructure.OrderDetails() With
                    {
                        .IdOrder = newOrder.IdOrders,
                        .IdProduct = Convert.ToInt32(detail("IdProduct")),
                        .OrderQuantity = Convert.ToInt32(detail("OrderQuantity"))
                    }
                    context.OrderDetails.Add(newDetail)
                Next
                context.SaveChanges()

            End Using

        Next

    End Sub

End Class