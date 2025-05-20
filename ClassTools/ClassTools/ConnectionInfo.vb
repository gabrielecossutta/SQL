Imports System.Windows
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Data.SqlClient

''' <summary>
''' Class to store service port and protocol name and a list of strings that contain the connection information
''' </summary>
Public Class Connections
    Public Property ServicePort As String
    Public Property ProtocolName As String
    Public Property ConnectionStrings As List(Of ConnectionString)

End Class

''' <summary>
''' Containt the connection information, username, password, server name and database name
''' </summary>
Public Class ConnectionString
    Public Property Id As Integer
    Public Property SQLServerName As String
    Public Property DatabaseName As String
    Public Property UserName As String
    Public Property Password As String
End Class

''' <summary>
'''This module allow to connect to the SQL Server by passing the username, password, server name and database name contained in the connection string 
''' </summary>
Public Module AccessServer
    Public Function ConnectToTheServer(User As String, Password As String, SQLServerName As String, DatabaseName As String) As SqlConnection

        ' Create a connection string 
        Dim ConnectionStringComplite As String = "Server=" + SQLServerName + ";Database=" + DatabaseName + ";User Id=" + User + ";Password=" + Password + ";TrustServerCertificate=True"
        WriteLogMessage("Login by: " + User)

        ' Create a connection to the SQL Server
        Dim connectionToServer As New SqlConnection(ConnectionStringComplite)

        'Try to open the connection
        Try

            connectionToServer.Open()
            WriteLogMessage("Connection open to: " + SQLServerName)

        Catch e As ArgumentException

            WriteLogMessage("ERROR: One of the arguments is wrong")

        Catch e As Exception

            WriteLogMessage("ERROR: " + e.Message)

        End Try

        'Return the connection, so it can be used in the CRUD class
        Return connectionToServer

    End Function
End Module