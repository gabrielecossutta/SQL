Imports System.ServiceProcess
Imports System.Web.Script.Serialization
Imports ClassTools

Public Class TotemService
    Inherits ServiceBase
    Implements WebService.IWebServiceHandler

    Private WebService As WebService
    Private TaskServer As Task

    Protected Overrides Sub OnStart(args() As String)
        WebService = New WebService(Me) ' Ora "Me" è valido!
        TaskServer = Task.Run(Async Function()
                                  Await WebService.StartWebService("http://localhost:81/ReceiveOrder/")
                              End Function)
    End Sub

    Protected Overrides Sub OnStop()
        WebService?.StopWebService()
    End Sub

    Public Sub OnMessageReceived(jsonBody As String) Implements WebService.IWebServiceHandler.OnMessageReceived
        Utils.WriteLogMessage("webservice", "AAA", "aaa")
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
End Class