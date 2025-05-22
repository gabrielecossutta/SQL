Imports System.Data.Common
Imports System.Windows
Imports System.Windows.Forms
Imports System.Windows.Markup
Imports Microsoft.Data.SqlClient

''' <summary>
''' Retrive Data from the database.
''' Perform the CRUD operation, Create, Read, Update and Delete on the database
''' </summary>
Public Class Crud

    ''' <summary>
    ''' Connect to the SQL Server using the provided credentials and database name.
    ''' This method creates a connection string using the provided parameters and attempts to open a connection to the SQL Server.
    ''' If successful, it returns the SqlConnection object; otherwise, it returns null.
    ''' </summary>
    Public Shared Function ConnectToTheServer(User As String, Password As String, SQLServerName As String, DatabaseName As String) As SqlConnection

        ' Create a connection string 
        Dim ConnectionStringComplite As String = "Server=" + SQLServerName + ";Database=" + DatabaseName + ";User=" + User + ";Password=" + Password + ";TrustServerCertificate=True"
        WriteLogMessage("Login by: " + User, "EXE", "Log")
        WriteLogMessage(ConnectionStringComplite, "EXE", "Log")

        ' Create a connection to the SQL Server
        Dim connectionToServer As New SqlConnection(ConnectionStringComplite)

        'Try to open the connection
        Try

            connectionToServer.Open()
            WriteLogMessage("Connection open to: " + SQLServerName, "EXE", "Log")

        Catch e As ArgumentException

            WriteLogMessage("ERROR: One of the arguments is wrong", "EXE", "Log")

        Catch e As Exception

            WriteLogMessage("ERROR: " + e.Message, "EXE", "Log")

        End Try

        'Return the connection, so it can be used in the CRUD class
        Return connectionToServer

    End Function

#Region "OPERATIONS ON SERVER"

    ''' <summary>
    ''' Return a data table with the data from the table to fill the GridView
    ''' </summary>
    ''' <param name="query">The SQL query to retrive the datatable.</param>
    ''' <param name="conncetionToServer">The connection to the SQL server.</param>
    Public Shared Function FillTables(ByVal query As String, conncetionToServer As SqlConnection) As DataTable

        Dim dataTable As New DataTable()

        Try

            'Create a SqlCommand object to execute the query
            Using cmd As New SqlDataAdapter(query, conncetionToServer)

                'Execute the command
                cmd.Fill(dataTable)

            End Using

        Catch ex As Exception

            WriteLogMessage(ex.Message, "EXE", "Log")

        End Try

        Return dataTable

    End Function

    ''' <summary>
    ''' Enable identity insert for a specific table, This allows inserting explicit values into the identity column of the table.
    ''' </summary>
    ''' <param name="tableName">The name of the table where to enable the Identity insert</param>
    ''' <param name="connectionToServer">The name of the column where to enable the Identity insert</param>
    Private Shared Sub EnableIdentityInsert(tableName As String, connectionToServer As SqlConnection)

        Try

            'Create a SqlCommand object to execute the query
            Using cmd As New SqlCommand($"SET IDENTITY_INSERT {tableName} ON", connectionToServer)

                'Execute the command
                cmd.ExecuteNonQuery()
                WriteLogMessage("Identity insert enabled", "EXE", "Log")

            End Using

        Catch
        End Try

    End Sub

    ''' <summary>
    ''' Disable identity insert for a specific table
    ''' </summary>
    ''' <param name="tableName">The name of the table where to disable the Identity insert</param>
    ''' <param name="connectionToServer">The name of the column where to disable the Identity insert</param>
    Private Shared Sub DisableIdentityInsert(tableName As String, connectionToServer As SqlConnection)

        Try

            'Create a SqlCommand object to execute the query
            Using cmd As New SqlCommand($"SET IDENTITY_INSERT {tableName} OFF", connectionToServer)

                'Execute the command
                cmd.ExecuteNonQuery()
                WriteLogMessage("Identity insert disabled", "EXE", "Log")

            End Using

        Catch
        End Try

    End Sub

#End Region

#Region "CRUD OPERATIONS"

    ''' <summary>
    ''' Create a new row in the database.
    ''' </summary>
    ''' <param name="query">The SQL query to create the row.</param>
    ''' <param name="connectionToServer">The connection to the SQL server.</param>
    ''' <param name="tableName">The name of the table where the row will be created.</param>
    Public Shared Function CreateRow(query As String, connectionToServer As SqlConnection, tableName As String, ByRef errorMsg As String) As Boolean

        Dim isOperationCompleted As Boolean = False

        'activate the identity insert
        EnableIdentityInsert(tableName, connectionToServer)

        ' Create a SqlCommand object to execute the query
        Using cmd As New SqlCommand(query, connectionToServer)

            Try
                cmd.Transaction = connectionToServer.BeginTransaction()

                ' Execute the command
                cmd.ExecuteNonQuery()
                WriteLogMessage("Row created in table: " + tableName, "EXE", "Log")
                isOperationCompleted = True
                cmd.Transaction.Commit()

            Catch ex As SqlException

                cmd.Transaction.Rollback()

                ' Check for specific SQL error codes for duplicate key violation and customize the error message
                Dim ErrorMessage As String = ex.Message

                If ex.Number = 2627 Or ex.Number = 2601 Then

                    Dim SegmentedString = ErrorMessage.Split("(")
                    Dim DuplicatedValue As String = SegmentedString(1)
                    DuplicatedValue = Left(0, DuplicatedValue.Length - 1)
                    errorMsg = $"Duplicated Id Found{Environment.NewLine}Duplicated Id: {DuplicatedValue}"

                Else

                    Try

                        Dim SegmentsString() As String = ErrorMessage.Split("""")
                        Dim TableNameString As String = SegmentsString(3).Substring(4)
                        ErrorMessage = ex.Message
                        SegmentsString = ErrorMessage.Split("'")
                        Dim ColumnNameString = SegmentsString(2)
                        errorMsg = $"Missing Reference Found{Environment.NewLine}Table:{ TableNameString + Environment.NewLine}Column: { ColumnNameString}"

                    Catch

                        errorMsg = ex.Message

                    End Try

                End If

                WriteLogMessage("Error occurred: " + ex.Message, "EXE", "Log")

            End Try

        End Using

        'Disable identity insert
        DisableIdentityInsert(tableName, connectionToServer)

        Return isOperationCompleted

    End Function

    ''' <summary>
    ''' Read a row from the database (uncommited)
    ''' </summary>
    ''' <param name="query">The SQL query to read the row.</param>
    ''' <param name="connectionToServer">The connection to the SQL server.</param>
    Public Shared Function ReadRow(query As String, connectionToServer As SqlConnection) As DataTable

        Dim dataTable As New DataTable()
        Using cmd As New SqlCommand(query, connectionToServer)

            Try
                ' Apri connessione se non è già aperta
                If connectionToServer.State = ConnectionState.Closed Then
                    connectionToServer.Open()
                End If

                ' Avvia una transazione con isolamento ReadUncommitted
                Using adapter As New SqlDataAdapter(cmd)

                    cmd.Transaction = connectionToServer.BeginTransaction(IsolationLevel.ReadUncommitted)
                    adapter.Fill(dataTable)
                    cmd.Transaction.Commit()

                End Using

            Catch ex As Exception

                cmd.Transaction.Rollback()
                WriteLogMessage(ex.Message, "EXE", "Log")

            End Try

        End Using

        Return dataTable

    End Function

    ''' <summary>
    ''' Update a cell in the database.
    ''' </summary>
    ''' <param name="querys">The list of query to update the rows.</param>
    ''' <param name="connectionToServer">The connection to the SQL server.</param>
    ''' <param name="tableName">The name of the table to enable/disable the identityInsert.</param>          vvvv ByRef rowAffected As Integer vvvv
    Public Shared Function UpdateCell(querys As List(Of String), connectionToServer As SqlConnection, tableName As String, ByRef ErrorMsg As String) As Boolean

        'rowAffected = -5
        Dim isOperationCompleted = False

        'Activate the identity insert
        EnableIdentityInsert(tableName, connectionToServer)

        'Create a SqlCommand object to execute the query
        Using cmd As New SqlCommand()

            cmd.Connection = connectionToServer
            cmd.Transaction = connectionToServer.BeginTransaction()

            For Each query As String In querys

                Try

                    cmd.CommandText = query
                    'Execute the command
                    cmd.ExecuteNonQuery()
                    'rowAffected = cmd.ExecuteNonQuery()
                    WriteLogMessage("Cell Updated", "EXE", "Log")
                    isOperationCompleted = True


                Catch ex As Exception

                    cmd.Transaction.Rollback()
                    ErrorMsg = $"Error occurred during the Update of a cell: {ex.Message}"
                    WriteLogMessage(ex.Message, "EXE", "Log")
                    Return False

                End Try


            Next

            cmd.Transaction.Commit()

        End Using

        'Disable identity insert
        DisableIdentityInsert(tableName, connectionToServer)
        Return isOperationCompleted

    End Function

    ''' <summary>
    ''' Delete a row from the database.
    ''' </summary>
    ''' <param name="query">The SQL query to delete the row.</param>
    ''' <param name="connectionToServer">The connection to the SQL server.</param>
    ''' <param name="valueKey">The value of the key to delete the row.</param>
    Public Shared Function DeleteRows(query As String, connectionToServer As SqlConnection, valueKey As Object, ByRef ErrorMsg As String) As Boolean

        Dim isOperationCompleted = False
        'Create a SqlCommand object to execute the query
        Using cmd As New SqlCommand(query, connectionToServer)

            cmd.Transaction = connectionToServer.BeginTransaction()

            'Add the parameter to the command
            cmd.Parameters.AddWithValue("@ValueKey", valueKey)

            Try

                'Execute the command
                cmd.ExecuteNonQuery()
                WriteLogMessage("Row deleted", "EXE", "Log")
                isOperationCompleted = True
                cmd.Transaction.Commit()

            Catch ex As SqlException

                cmd.Transaction.Rollback()

                'Customize the error message
                Dim ErrorMessage As String = ex.Message
                Dim SegmentsString() As String = ErrorMessage.Split("""")
                Dim TableNameString As String = SegmentsString(3).Substring(4)
                ErrorMessage = ex.Message
                SegmentsString = ErrorMessage.Split("'")
                Dim ColumnNameString As String = SegmentsString(2)
                ErrorMsg = $"Reference Found{Environment.NewLine} Table: {TableNameString + Environment.NewLine}Column: {ColumnNameString}"
                WriteLogMessage(ex.Message, "EXE", "Log")

            End Try

        End Using

        Return isOperationCompleted

    End Function

#End Region


End Class