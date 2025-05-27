Imports Microsoft.Data.SqlClient
Imports ClassTools
Imports System.Text
Imports Microsoft.VisualBasic.Logging
Imports System.Security.Cryptography.Xml
Imports System.Data.Common
Imports Microsoft.Identity
Imports System.Runtime.Remoting.Contexts
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Core.Metadata.Edm

''' <summary>
''' This Form is used to manage the CRUD operation on server
''' </summary>
Public Class Database_Entity

    'List of clients
    Dim ClientList As List(Of Client) = New List(Of Client)()

    'DBContext for Entity Framework
    Public Class CustomersDbContext
        Inherits DbContext

        Public Sub New(connectionString As String)
            MyBase.New(connectionString)
        End Sub

        Public Property Customers As DbSet(Of Client)

    End Class

    'Name of the table in the database
    <Table("Customers")>
    Public Class Client
        <Key> 'Primary key
        Public Property CustomerID As String
        Public Property CompanyName As String
        Public Property ContactName As String
        Public Property ContactTitle As String
        Public Property Address As String
        Public Property City As String
        Public Property Region As String
        Public Property PostalCode As String
        Public Property Country As String
        Public Property Phone As String
        Public Property Fax As String
    End Class

#Region "FORM"

    Private Sub Database_Entity_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "Database Entity Framework"

    End Sub

    Private Sub Database_Entity_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        PopulateList(ExternalArgumentsLoginCheck())

    End Sub

#End Region

#Region "Functions"

    ''' <summary>
    ''' funcion to populate the DataGridView with the data from the database using Entity Framework
    ''' It uses the CustomersDbContext to connect to the database and retrieve the data from the Customers table
    ''' </summary>
    Private Sub PopulateList(connectionString As String)

        Try
            'Instantiate the DbContext with the connection string
            Using db As New CustomersDbContext(connectionString)

                'retrive the data from the database
                ClientList = db.Customers.ToList()

            End Using

            'for each client in the ClientList, add a new row to the DataGridView
            For Each client As Client In ClientList

                CustomerDataGrid.Rows.Add(client.CustomerID, client.CompanyName, client.ContactName, client.ContactTitle, client.Address, client.City, client.Region, client.PostalCode, client.Country, client.Phone, client.Fax)

            Next

            'Set the datagrid to auto size the columns
            CustomerDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        Catch ex As Exception

            Console.WriteLine($"Error during che population of the list with Entity Framework: {ex.Message}")

        End Try

    End Sub

    ''' <summary>
    ''' Check if the are arguments passed from the command line, if so split the connection string and retrive the SQLServerName, DatabaseName, Username and Password then connect to the SQL Server
    ''' </summary>
    Private Function ExternalArgumentsLoginCheck()

        'Get the arguments from the command line
        Dim args As String() = Environment.GetCommandLineArgs()

        'connection for the SQL Server
        Dim connection As SqlConnection = Nothing

        'Check if the arguments are more than 1
        If args.Count > 1 Then

            Return args(1)

        End If

        Return connection

    End Function

#End Region

#Region "BUTTONS"

    'Creates a new row in the selected table
    Private Sub BT_Create_Click(sender As Object, e As EventArgs) Handles BT_Create.Click

        CustomerDataGrid.Rows.Add()

    End Sub

    'Read the selected row in the DataGridView
    Private Sub BT_Read_Click(sender As Object, e As EventArgs) Handles BT_Read.Click

        Dim stringValue As String = ""
        CustomerDataGrid.CurrentRow.Selected = True
        Dim selectedRow As DataGridViewRow = CustomerDataGrid.CurrentRow

        If selectedRow Is Nothing Then

            MessageBox.Show("No row selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return

        End If

        'Iterate through the cells of the selected row and concatenate the values into a string
        For Each cell In selectedRow.Cells

            If cell.Value Is Nothing Then

                stringValue += cell.OwningColumn.Name & ": " & "<empty>" & vbCrLf

            Else

                stringValue += cell.OwningColumn.Name & ": " & cell.Value.ToString() & vbCrLf

            End If

        Next

        MessageBox.Show(stringValue, "Selected Cell Value", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    'Update the selected cell in the DataGridView
    Private Sub BT_Update_Click(sender As Object, e As EventArgs) Handles BT_Update.Click

        Dim cells As DataGridViewSelectedCellCollection = CustomerDataGrid.SelectedCells

        'Check if the cells selected are from the same column
        For Each cell In CustomerDataGrid.SelectedCells

            If cell.OwningColumn.name <> cells(0).OwningColumn.Name Then

                MessageBox.Show("Choose cells from the same column", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return

            End If

        Next

        'Prompt the user for the new value to update the selected cells and set the value of the selected cells to the new value
        Dim stringInput = InputBox("Insert the new value", "Update Cell", " ")

        For Each cell In CustomerDataGrid.SelectedCells

            cell.value = stringInput

        Next

    End Sub

    'Deletes the selected rows in the DataGridView
    Private Sub BT_Delete_Click(sender As Object, e As EventArgs) Handles BT_Delete.Click

        'Check if there are any rows selected in the DataGridView
        If CustomerDataGrid.SelectedRows.Count = 0 Then

            MessageBox.Show("No row selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Return
        End If

        Dim Rows As DataGridViewSelectedRowCollection = CustomerDataGrid.SelectedRows

        'Iterate through the selected rows and remove them from the DataGridView
        For Each row As DataGridViewRow In Rows

            If Not row.IsNewRow Then

                CustomerDataGrid.Rows.Remove(row)

            End If

        Next

    End Sub

#End Region

End Class