Imports System.Windows.Forms
Imports ClassTools
Imports Microsoft.Data.SqlClient
Module LINQ

    Dim ConnectionToServer As SqlConnection

    Sub Main()

        ConnectionToServer = ExternalArgumentsLoginCheck()
        PopulateList()

    End Sub

    Private Function ExternalArgumentsLoginCheck()

        'Get the arguments from the command line
        Dim args As String() = Environment.GetCommandLineArgs()

        'connection for the SQL Server
        Dim connection As SqlConnection = Nothing

        'Check if the arguments are more than 1
        If args.Count > 1 Then

            'Split the connection string and retrive the SQLServerName, DatabaseName, Username and Password
            Dim segmentedString = args(1).Split(";")
            Dim SQLServerName = segmentedString(0).Replace("Server=", "")
            Dim DatabaseName = segmentedString(1).Replace("Database=", "")
            Dim Username = segmentedString(2).Replace("UserID=", "")
            Dim Password = segmentedString(3).Replace("Password=", "")

            'connect to the SQL Server
            connection = ConnectToTheServer(Username, Password, SQLServerName, DatabaseName)

        End If

        Return connection

    End Function

    Private Function PopulateList()

        Dim query As String = $"SELECT * FROM Customers"
        Dim dataTable = Crud.ReadRow(query, ConnectionToServer)
        Dim rowData As String = String.Join(";", dataTable.Columns.Cast(Of DataColumn)().Select(Function(col, index) $"{col.ColumnName}: {dataTable.Rows(0).ItemArray(index)}"))
        MessageBox.Show(rowData, "Row Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

End Module
