Imports System.Reflection
Imports System.Text.Json.Nodes
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports ClassTools
Imports Microsoft.Data.SqlClient
Public Class F_Es2

    'reference of the class connection
    Dim connection As Connections

    'Index of the selected Username in the combobox
    Dim index As Integer

    'ConnectionToServerOpen
    Dim connectionToServer As SqlConnection

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim args As String() = Environment.GetCommandLineArgs()



        'Message for the log
        WriteLogMessage("Caricati dati File JSON")

        'Load the information of the connections from the file
        connection = ReadFromFile()

        'set the combobox with the connection strings
        CB_Username.DataSource = connection.ConnectionStrings

        'set the textboxes with the connection information
        TB_ServicePort.Text = connection.ServicePort
        TB_Protocol.Text = connection.ProtocolName

        If args.Count > 1 Then
            B_Login.PerformClick()
        End If

    End Sub

    Private Sub ComboBox1_TextChanged_1(sender As Object, e As EventArgs) Handles CB_Username.TextChanged
        If connection Is Nothing Then Return

        'New index to check if the what
        Dim nuovoIndice As Integer = CB_Username.SelectedIndex

        'Check if the index is -1, if it is, set the index to the selected index
        If nuovoIndice <> -1 Then
            index = nuovoIndice
        End If

        'Set the new Username for the selected index
        connection.ConnectionStrings(index).UserName = CB_Username.Text

    End Sub

    Private Sub CB_Username_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Username.SelectedIndexChanged

        'save the selected index
        index = CB_Username.SelectedIndex
        'Set the textboxes with the connection information of the selected index
        TB_Password.Text = connection.ConnectionStrings(index).Password
        TB_ServerName.Text = connection.ConnectionStrings(index).SQLServerName
        TB_DatabaseName.Text = connection.ConnectionStrings(index).DatabaseName

    End Sub

    Private Sub TB_Password_TextChanged(sender As Object, e As EventArgs) Handles TB_Password.TextChanged
        If connection Is Nothing Then Return
        connection.ConnectionStrings(index).Password = TB_Password.Text

    End Sub

    Private Sub TB_Port_TextChanged(sender As Object, e As EventArgs) Handles TB_ServicePort.TextChanged
        If connection Is Nothing Then Return
        connection.ServicePort = TB_ServicePort.Text

    End Sub

    Private Sub TB_Protocol_TextChanged(sender As Object, e As EventArgs) Handles TB_Protocol.TextChanged
        If connection Is Nothing Then Return
        connection.ProtocolName = TB_Protocol.Text

    End Sub

    Private Sub B_Load_Click(sender As Object, e As EventArgs) Handles B_Load.Click

        'Message for the log
        WriteLogMessage("Caricati dati File JSON")

        'Load the information of the connections from the file
        connection = ReadFromFile()

        'set the combobox with the connection strings
        CB_Username.DataSource = connection.ConnectionStrings

        'set the textboxes with the connection information
        TB_ServicePort.Text = connection.ServicePort
        TB_Protocol.Text = connection.ProtocolName


    End Sub

    Private Sub TB_ServerName_TextChanged(sender As Object, e As EventArgs) Handles TB_ServerName.TextChanged
        If connection Is Nothing Then Return
        connection.ConnectionStrings(index).SQLServerName = TB_ServerName.Text

    End Sub

    Private Sub TB_DatabaseName_TextChanged(sender As Object, e As EventArgs) Handles TB_DatabaseName.TextChanged
        If connection Is Nothing Then Return
        connection.ConnectionStrings(index).DatabaseName = TB_DatabaseName.Text

    End Sub

    Private Sub B_Login_Click(sender As Object, e As EventArgs) Handles B_Login.Click
        'Assign the connection to the connectionToServer variable
        connectionToServer = ConnectToTheServer(CB_Username.Text, TB_Password.Text, TB_ServerName.Text, TB_DatabaseName.Text)
        If connectionToServer IsNot Nothing Then
            If connectionToServer.State = ConnectionState.Open Then

                'Create the second form and pass the connection to it and this form
                Dim Form2 As New F_Es3(Me, connectionToServer)

                'Show the second form
                Form2.Show()

                'Hide this form
                Me.Hide()

            End If
        Else
            MessageBox.Show("Connection failed. Please check your credentials.")
        End If

    End Sub

    Private Sub B_Save_Click(sender As Object, e As EventArgs) Handles B_Save.Click

        'Skip if the connection is not initialized
        If connection Is Nothing Then Return

        'Message for the log
        WriteLogMessage("Sovrascritto File JSON")

        'pass the connection to the function to write it on the file
        WriteOnFile(connection)

        'Read the connection from the file and assign it to the connection variable
        connection = ReadFromFile()

        'Update the combobox with the connection strings
        CB_Username.DataSource = connection.ConnectionStrings


    End Sub
End Class
