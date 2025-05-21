Imports System.Data.Common
Imports System.Windows
Imports System.Windows.Markup
Imports Microsoft.Data.SqlClient

''' <summary>
''' Retrive Data from the database.
''' Perform the CRUD operation, Create, Read, Update and Delete on the database
''' </summary>
Public Class Crud

    ''' <summary>
    ''' Create a new row in the database.
    ''' </summary>
    ''' <param name="query">The SQL query to create the row.</param>
    ''' <param name="connectionToServer">The connection to the SQL server.</param>
    ''' <param name="tableName">The name of the table where the row will be created.</param>
    Public Shared Sub CreateRow(query As String, connectionToServer As SqlConnection, tableName As String)

        'activate the identity insert
        EnableIdentityInsert(tableName, connectionToServer)

        ' Create a SqlCommand object to execute the query
        Using cmd As New SqlCommand(query, connectionToServer)

            Try

                ' Execute the command
                cmd.ExecuteNonQuery()
                WriteLogMessage("Row created in table: " + tableName)

            Catch ex As SqlException

                Dim ErrorMessage As String = ex.Message

                ' Check for specific SQL error codes for duplicate key violation and customize the error message
                If ex.Number = 2627 Or ex.Number = 2601 Then

                    Dim SegmentedString = ErrorMessage.Split("(")
                    Dim DuplicatedValue As String = SegmentedString(1)
                    DuplicatedValue = Left(0, DuplicatedValue.Length - 1)
                    MessageBox.Show("Duplicated Id Found" + Environment.NewLine + "Duplicated Id: " + DuplicatedValue)

                Else
                    Try

                        Dim SegmentsString() As String = ErrorMessage.Split("""")
                        Dim TableNameString As String = SegmentsString(3).Substring(4)
                        ErrorMessage = ex.Message
                        SegmentsString = ErrorMessage.Split("'")
                        Dim ColumnNameString = SegmentsString(2)
                        MessageBox.Show("Missing Reference Found" + Environment.NewLine + "Table: " & TableNameString + Environment.NewLine + "Column: " + ColumnNameString)

                    Catch

                        MessageBox.Show(ex.Message)

                    End Try

                End If

                WriteLogMessage("Error occurred: " + ex.Message)

            End Try

        End Using

        'Disable identity insert
        DisableIdentityInsert(tableName, connectionToServer)

    End Sub

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
                WriteLogMessage("Identity insert enabled")

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
                WriteLogMessage("Identity insert disabled")

            End Using

        Catch
        End Try

    End Sub

    ''' <summary>
    ''' Read a row from the database.
    ''' </summary>
    ''' <param name="query">The SQL query to read the row.</param>
    ''' <param name="connectionToServer">The connection to the SQL server.</param>
    Public Shared Function ReadRow(query As String, connectionToServer As SqlConnection) As DataTable

        Dim resultTable As New DataTable()

        Try

            'Create a SqlCommand object to execute the query
            Using cmd As New SqlCommand(query, connectionToServer)

                ' Create a SqlDataAdapter to fill the DataTable
                Using adapter As New SqlDataAdapter(cmd)

                    ' Fill the DataTable with the results of the query
                    adapter.Fill(resultTable)
                    WriteLogMessage("Row Readed")


                End Using

            End Using

        Catch ex As Exception

            MessageBox.Show("Error during the table reading: " & ex.Message)
            WriteLogMessage("Error during the table reading: " & ex.Message)

        End Try

        Return resultTable

    End Function

    ''' <summary>
    ''' Update a cell in the database.
    ''' </summary>
    ''' <param name="query">The SQL query to delete the row.</param>
    ''' <param name="connectionToServer">The connection to the SQL server.</param>
    ''' <param name="tableName">The name of the table to enable/disable the identityInsert.</param>
    Public Shared Sub UpdateCell(query As String, connectionToServer As SqlConnection, tableName As String)

        'Activate the identity insert
        EnableIdentityInsert(tableName, connectionToServer)

        'Create a SqlCommand object to execute the query
        Using cmd As New SqlCommand(query, connectionToServer)

            Try

                'Execute the command
                cmd.ExecuteNonQuery()
                WriteLogMessage("Cell Updated")

            Catch ex As Exception

                MessageBox.Show("Error occurred during the Update of a cell: " & ex.Message)
                WriteLogMessage(ex.Message)

            End Try

        End Using

        'Disable identity insert
        DisableIdentityInsert(tableName, connectionToServer)

    End Sub

    ''' <summary>
    ''' Delete a row from the database.
    ''' </summary>
    ''' <param name="query">The SQL query to delete the row.</param>
    ''' <param name="connectionToServer">The connection to the SQL server.</param>
    ''' <param name="valueKey">The value of the key to delete the row.</param>
    Public Shared Sub DeleteRows(query As String, connectionToServer As SqlConnection, valueKey As Object)

        'Create a SqlCommand object to execute the query
        Using cmd As New SqlCommand(query, connectionToServer)

            'Add the parameter to the command
            cmd.Parameters.AddWithValue("@ValueKey", valueKey)

            Try

                'Execute the command
                cmd.ExecuteNonQuery()
                WriteLogMessage("Row deleted")

            Catch ex As SqlException

                'Customize the error message
                Dim ErrorMessage As String = ex.Message
                Dim SegmentsString() As String = ErrorMessage.Split("""")
                Dim TableNameString As String = SegmentsString(3).Substring(4)
                ErrorMessage = ex.Message
                SegmentsString = ErrorMessage.Split("'")
                Dim ColumnNameString As String = SegmentsString(2)
                MessageBox.Show("Reference Found" + Environment.NewLine + "Table: " & TableNameString + Environment.NewLine + "Column: " + ColumnNameString)
                WriteLogMessage(ex.Message)

            End Try

        End Using

    End Sub


    ''' <summary>
    ''' Return the list of the tables name to populate tabpages
    ''' </summary>
    ''' <param name="query">The SQL query to retrive the tablename.</param>
    ''' <param name="conncetionToServer">The connection to the SQL server.</param>
    Public Shared Function GetDati(ByVal query As String, conncetionToServer As SqlConnection) As List(Of String)

        Dim tableNames As New List(Of String)()

        Try

            'Create a SqlCommand object to execute the query
            Using cmd As New SqlCommand(query, conncetionToServer)

                'Create a reader that execute the command
                Using reader As SqlDataReader = cmd.ExecuteReader()

                    'The reader reads the table name and adds it to the list
                    While reader.Read()

                        tableNames.Add(reader("table_name").ToString())

                    End While

                End Using

            End Using

        Catch ex As Exception

            WriteLogMessage(ex.Message)

        End Try

        Return tableNames

    End Function

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

            MessageBox.Show("Error during the fill up of the table : " & ex.Message)
            WriteLogMessage(ex.Message)

        End Try

        Return dataTable

    End Function

End Class