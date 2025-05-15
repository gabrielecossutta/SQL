Imports Microsoft.Data.SqlClient
Imports ClassTools
Imports System.Text
Public Class F_Es3
    Private connectionToServer As SqlConnection
    Private form1 As Form

    Public Sub New(ByVal Form1 As F_Es2, ByVal ConnectionToServer As SqlConnection)

        'Inizialize the components
        InitializeComponent()
        Me.StartPosition = FormStartPosition.CenterScreen

        Me.connectionToServer = ConnectionToServer
        Me.form1 = Form1

        'Check if the connection is open otherwise open it
        If ConnectionToServer.State = ConnectionState.Closed Then
            ConnectionToServer.Open()
        End If

        Dim tableNames As New List(Of String)()

        'Create a query to get the table names
        ' spost
        Dim command As New SqlCommand("SELECT table_name FROM information_schema.tables WHERE table_type = 'BASE TABLE' AND table_schema = 'dbo'", ConnectionToServer)

        'Create a reader that execute the command
        'spost
        Dim reader As SqlDataReader = command.ExecuteReader()

        'Every the reader reads the table name and adds it to the list
        While reader.Read()
            tableNames.Add(reader("table_name").ToString())
        End While

        'Close the reader
        reader.Close()

        'Try

        '    Dim sSQL As String = "SELECT table_name FROM information_schema.tables WHERE table_type = 'BASE TABLE' AND table_schema = 'dbo'"
        '    Dim dtTables As DataTable
        '    dtTables = Crud.GetDati(sSQL)
        '    For Each r As DataRow In dtTables.Rows
        '        tableNames.Add(r("table_name"))
        '    Next
        'Catch ex As Exception
        '    msg box
        '    log
        'End Try



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
            TC_TablesName.TabPages.Add(tabPage)

        Next
    End Sub

    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        'Show the first form when the second form is closed
        connectionToServer.Close()
        form1.Show()

    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TabPage1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub BT_Create_Click(sender As Object, e As EventArgs) Handles BT_Create.Click
        WriteLogMessage("Button ""Create"" pressed")
        Dim selectedGrid As DataGridView = Nothing

        For Each ctrl As Control In Me.TC_TablesName.SelectedTab.Controls
            If TypeOf ctrl Is DataGridView Then
                selectedGrid = DirectCast(ctrl, DataGridView)
                Exit For
            End If
        Next

        If selectedGrid IsNot Nothing Then
            Dim dataTable As DataTable = DirectCast(selectedGrid.DataSource, DataTable)
            If dataTable IsNot Nothing Then

                Dim newRow As DataRow = dataTable.NewRow()
                dataTable.Rows.Add(newRow)
            End If
        End If
    End Sub




    Private Sub TabPage2_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub BT_Update_Click(sender As Object, e As EventArgs) Handles BT_Update.Click

        WriteLogMessage("Button ""Update"" pressed")
        Dim selectedGrid As DataGridView = Nothing
        'Find the selected DataGridView in the selected TabPage
        For Each ctrl As Control In Me.TC_TablesName.SelectedTab.Controls
            If TypeOf ctrl Is DataGridView Then

                'If the control is a DataGridView cast it to the correct type
                selectedGrid = DirectCast(ctrl, DataGridView)

                Exit For
            End If
        Next

        'Check if the user selected a single cell
        If selectedGrid.SelectedCells.Count = 1 Then
            '------multi selection------If selectedGrid.SelectedCells.Count > 0 Then
            '------multi selection------For Each selectedCell As DataGridViewCell In selectedGrid.SelectedCells


            WriteLogMessage("Cell Column: " + selectedGrid.SelectedCells(0).ColumnIndex.ToString + " Row: " + selectedGrid.SelectedCells(0).RowIndex.ToString + " Value: " + selectedGrid.SelectedCells(0).Value.ToString)

            'Create a new InputForm to get the value from the user
            Dim inputForm As New InputForm(selectedGrid.SelectedCells(0).OwningColumn.Name, selectedGrid.SelectedCells(0).Value.GetType())

            If inputForm.ShowDialog() = DialogResult.OK Then
                '------multi selection------selectedCell.Value = inputForm.InputValue
                selectedGrid.SelectedCells(0).Value = inputForm.InputValue
                WriteLogMessage("Update with the value: " + selectedGrid.SelectedCells(0).Value.ToString)
            End If


            '------multi selection------Next
        Else
            MessageBox.Show("Please select a one cell to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            WriteLogMessage("ERROR: zero or more cells selected")
        End If


    End Sub



    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TC_TablesName.SelectedIndexChanged

    End Sub

    Private Sub BT_Delete_Click(sender As Object, e As EventArgs) Handles BT_Delete.Click
        WriteLogMessage("Button ""Delete"" pressed")
        Dim selectedGrid As DataGridView = Nothing

        For Each ctrl As Control In Me.TC_TablesName.SelectedTab.Controls
            If TypeOf ctrl Is DataGridView Then
                selectedGrid = DirectCast(ctrl, DataGridView)
                Exit For
            End If
        Next

        If selectedGrid IsNot Nothing Then
            For Each selectedRow As DataGridViewRow In selectedGrid.SelectedRows
                selectedGrid.Rows.Remove(selectedRow)
            Next
        End If
    End Sub

    Private Sub F_Es3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BT_Read_Click(sender As Object, e As EventArgs) Handles BT_Read.Click
        WriteLogMessage("Button ""Read"" pressed")
        Dim selectedGrid As DataGridView = Nothing
        'Find the selected DataGridView in the selected TabPage
        For Each ctrl As Control In Me.TC_TablesName.SelectedTab.Controls
            If TypeOf ctrl Is DataGridView Then

                'If the control is a DataGridView cast it to the correct type
                selectedGrid = DirectCast(ctrl, DataGridView)

                Exit For
            End If
        Next

        Dim InfoString As New StringBuilder()

        For Each selectedRow As DataGridViewRow In selectedGrid.SelectedRows
            For Each selectedCell As DataGridViewCell In selectedRow.Cells
                InfoString.Append(selectedCell.OwningColumn.Name & ":" & selectedCell.Value.ToString() & Environment.NewLine)
            Next
            MessageBox.Show(InfoString.ToString(), selectedGrid.SelectedRows(0).Cells(0).OwningColumn.Name + " " + selectedGrid.SelectedRows(0).Cells(0).Value.ToString, MessageBoxButtons.OK, MessageBoxIcon.Information)
            InfoString.Clear()
        Next
    End Sub
End Class


'__________________________________________________AGGIUNGERE CONTROLLO SUI TIPI DI VALORE e SUI VALORI NULL per i numeri
Public Class InputForm
    Inherits Form

    Private textBox As New TextBox()
    Private button As New Button()
    Private tipo As Type

    Public Property InputValue As String

    Public Sub New(name As String, Tipo As Type)
        Me.Text = name
        Me.tipo = Tipo
        Me.Size = New Size(350, 150)
        Me.StartPosition = FormStartPosition.CenterScreen
        textBox.Location = New Point(20, 20)
        textBox.Width = 300

        button.Text = "OK"
        button.Location = New Point(125, 60)
        AddHandler button.Click, AddressOf BtnOK_Click

        Me.Controls.Add(textBox)
        Me.Controls.Add(button)
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs)
        InputValue = textBox.Text
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class