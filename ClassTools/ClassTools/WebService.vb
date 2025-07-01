
Imports System.IO
Imports System.Net
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Tab
Imports Azure.Core
Public Class WebService

    Public Listener As HttpListener
    Private Handler As IWebServiceHandler

    Public Sub New(handler As IWebServiceHandler)
        Me.Handler = handler
    End Sub

    'netsh http add urlacl url=https://localhost:82/createorder/ user=Everyone
    Public Async Function StartWebService(URL As String) As Task(Of HttpListener)
        Listener = New HttpListener()
        Listener.Prefixes.Add(URL)
        Listener.Start()
        'Select Case URL.ToLower().TrimEnd("/"c).Substring(URL.LastIndexOf("/") + 1)

        '    Case "/receiveorder/"
        '        '...
        '    Case "/senddata/"
        '        '...
        '    Case Else
        '        Return Nothing
        'End Select
        While Listener.IsListening
            Try
                Dim context As HttpListenerContext = Await Listener.GetContextAsync()
                Await ProcessRequestAsync(context)
            Catch ex As Exception
            End Try
        End While
        Return Listener
    End Function

    Public Sub StopWebService()
        Listener.Stop()
        Listener.Close()
    End Sub

    Public Async Function ProcessRequestAsync(context As HttpListenerContext) As Task(Of String)
        Dim request As HttpListenerRequest = context.Request
        Dim response As HttpListenerResponse = context.Response

        ' Legge il corpo della richiesta
        Dim body As String
        Using reader As New StreamReader(request.InputStream, request.ContentEncoding)
            body = Await reader.ReadToEndAsync()
            Handler.OnMessageReceived(body)
        End Using


        Return body
    End Function

    Public Interface IWebServiceHandler
        Sub OnMessageReceived(jsonBody As String)
    End Interface



End Class

