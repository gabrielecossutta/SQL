Imports System.Net

Public Class Form1

    Private Sub B_Download_Click(sender As Object, e As EventArgs) Handles B_Download.Click

    End Sub

    Private Sub ConnectToFTP()
        Dim URLftp As String = "ftp://192.168.3.177/"
        Dim username As String = "Gabriele Cossutta" 'io lo sto facendo qui in modo non sicuro vedendo password e username,
        Dim password As String = "3663"              'c'è un modo per farlo in maniera sicura questo passaggio
        Dim Destination As String = "C:\Users\Gabriele Cossutta\Desktop\SQL\SQL\ClassTools\EXE\DWL"
        Dim request As FtpWebRequest = CType(WebRequest.Create(URLftp), FtpWebRequest)
        request.Method = WebRequestMethods.Ftp.ListDirectory
        request.Credentials = New NetworkCredential(username, password)

        Dim fileList As New List(Of String)
        Using response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)

        End Using
    End Sub

End Class
