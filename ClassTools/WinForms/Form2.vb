Imports Microsoft.Data.SqlClient
Imports ClassTools
Public Class Form2
    Private connectionToServer As SqlConnection
    Private form1 As Form

    Public Sub New(ByVal Form1 As Form, ByVal ConnectionToServer As SqlConnection)

        'Required for Windows Form Designer support
        InitializeComponent()
        Me.connectionToServer = ConnectionToServer
        Me.form1 = Form1

        'Check if the connection is open otherwise open it
        If ConnectionToServer.State = ConnectionState.Closed Then
            ConnectionToServer.Open()
        End If

        Dim tableNames As New List(Of String)()

        'Create a query to get the table names
        Dim command As New SqlCommand("SELECT table_name FROM information_schema.tables WHERE table_type = 'BASE TABLE' AND table_schema = 'dbo'", ConnectionToServer)

        'Create a reader that execute the command
        Dim reader As SqlDataReader = command.ExecuteReader()

        'Every the reader reads the table name and adds it to the list
        While reader.Read()
            tableNames.Add(reader("table_name").ToString())
        End While

        'Close the reader
        reader.Close()

        For Each tableName As String In tableNames

            'Create a new TabPage for each table
            Dim tabPage As New TabPage(tableName)

            'Create a new DataGridView for each table
            Dim dataGridView As New DataGridView()
            dataGridView.Dock = DockStyle.Fill
            dataGridView.AllowUserToAddRows = False
            dataGridView.ReadOnly = True

            'Execute a query to get the data from the table and fill the DataGridView
            Dim dataAdapter As New SqlDataAdapter("SELECT * FROM [" & tableName & "]", ConnectionToServer)
            Dim dataTable As New DataTable()
            dataAdapter.Fill(dataTable)

            'Import the data into the DataGridView
            dataGridView.DataSource = dataTable

            'Add the DataGridView to the TabPage
            tabPage.Controls.Add(dataGridView)

            'Add the TabPage to the TabControl
            TabControl1.TabPages.Add(tabPage)

        Next
    End Sub

    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        'Show the first form when the second form is closed
        form1.Show()

    End Sub


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TabPage1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub TabPage2_Click_1(sender As Object, e As EventArgs)

    End Sub
End Class