Imports System.Windows
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Data.SqlClient

Public Class ConnectionString
    Public Property SQLServerName As String
    Public Property DatabaseName As String
    Public Property UserName As String
    Public Property Password As String
End Class

Public Class Connections
    Public Property ServicePort As String
    Public Property ProtocolName As String
    Public Property ConnectionStrings As List(Of ConnectionString)

End Class

Public Module AccessServer
    Public Function ConnectToTheServer(User As String, Password As String, SQLServerName As String, DatabaseName As String) As SqlConnection
        ' Create a connection string 
        Dim ConnectionStringComplite As String = "Server=" + SQLServerName + ";Database=" + DatabaseName + ";User Id=" + User + ";Password=" + Password + ";TrustServerCertificate=True"
        WriteLogMessage("Login by: " + User)

        ' Create a connection to the SQL Server
        Dim connectionToServer As New SqlConnection(ConnectionStringComplite)

        'Try open the connection
        Try
            connectionToServer.Open()
            WriteLogMessage("Connection open to: " + SQLServerName)
        Catch e As Exception
            WriteLogMessage("ERROR: " + e.Message)
            Return Nothing
        End Try

        Return connectionToServer
    End Function
End Module