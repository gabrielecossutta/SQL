'Imports System.Windows
'Imports Microsoft.VisualBasic.ApplicationServices
'Imports Microsoft.Data.SqlClient
'Imports System.Reflection


'''' <summary>
''''This module allow to connect to the SQL Server by passing the username, password, server name and database name contained in the connection string 
'''' </summary>
'Public Module AccessServer
'    Public Function ConnectToTheServer(User As String, Password As String, SQLServerName As String, DatabaseName As String) As SqlConnection

'        ' Create a connection string 
'        Dim ConnectionStringComplite As String = "Server=" + SQLServerName + ";Database=" + DatabaseName + ";User Id=" + User + ";Password=" + Password + ";TrustServerCertificate=True"
'        WriteLogMessage("Login by: " + User)
'        WriteLogMessage(ConnectionStringComplite)
'        ' Create a connection to the SQL Server
'        Dim connectionToServer As New SqlConnection(ConnectionStringComplite)

'        'Try to open the connection
'        Try

'            connectionToServer.Open()
'            WriteLogMessage("Connection open to: " + SQLServerName)

'        Catch e As ArgumentException

'            WriteLogMessage("ERROR: One of the arguments is wrong")

'        Catch e As Exception

'            WriteLogMessage("ERROR: " + e.Message)

'        End Try

'        'Return the connection, so it can be used in the CRUD class
'        Return connectionToServer

'    End Function
'End Module