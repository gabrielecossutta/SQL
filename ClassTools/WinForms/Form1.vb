Imports System.Text.Json.Nodes
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports ClassTools
Imports Microsoft.Data.SqlClient
Public Class Form1

    'reference of the class connection
    Dim connection As Connections
    'Index of the selected Username in the combobox
    Dim index As Integer

    'ConnectionToServerOpen
    Dim connectionToServer As SqlConnection

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Skip if the connection is not initialized
        If connection Is Nothing Then Return

        'Message for the log
        WriteLogMessage("Sovrascritto File JSON")

        'pass the connection to the function to write it on the file
        WriteOnFile(connection)

        'Read the connection from the file and assign it to the connection variable
        connection = ReadFromFile()

        'Update the combobox with the connection strings
        ComboBox1.DataSource = connection.ConnectionStrings

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'Message for the log
        WriteLogMessage("Caricati dati File JSON")

        'Load the information of the connections from the file
        connection = ReadFromFile()

        'set the combobox with the connection strings
        ComboBox1.DataSource = connection.ConnectionStrings

        'set the textboxes with the connection information
        TextBox2.Text = connection.ServicePort
        TextBox3.Text = connection.ProtocolName

    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        'save the selected index
        index = ComboBox1.SelectedIndex
        'Set the textboxes with the connection information of the selected index
        TextBox4.Text = connection.ConnectionStrings(index).Password
        TextBox6.Text = connection.ConnectionStrings(index).SQLServerName
        TextBox5.Text = connection.ConnectionStrings(index).DatabaseName

    End Sub

    Private Sub ComboBox1_TextChanged_1(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        If connection Is Nothing Then Return

        'New index to check if the what
        Dim nuovoIndice As Integer = ComboBox1.SelectedIndex

        'Check if the index is -1, if it is, set the index to the selected index
        If nuovoIndice <> -1 Then
            index = nuovoIndice
        End If

        'Set the new Username for the selected index
        connection.ConnectionStrings(index).UserName = ComboBox1.Text

    End Sub



    'Skip if the connection is not initialized but if the connection is initialized, update the connection string
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If connection Is Nothing Then Return
        connection.ServicePort = TextBox2.Text
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If connection Is Nothing Then Return
        connection.ProtocolName = TextBox3.Text
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If connection Is Nothing Then Return
        connection.ConnectionStrings(index).Password = TextBox4.Text
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If connection Is Nothing Then Return
        connection.ConnectionStrings(index).SQLServerName = TextBox6.Text
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If connection Is Nothing Then Return
        connection.ConnectionStrings(index).DatabaseName = TextBox5.Text
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '
        connectionToServer = ConnectToTheServer(ComboBox1.Text, TextBox4.Text, TextBox6.Text, TextBox5.Text)
        If connectionToServer IsNot Nothing Then
            If connectionToServer.State = ConnectionState.Open Then

                'Create the second form and pass the connection to it and this form
                Dim Form2 As New Form2(Me, connectionToServer)

                'Show the second form
                Form2.Show()

                'Hide this form
                Me.Hide()

            End If
        Else
            MessageBox.Show("Connection failed. Please check your credentials.")
        End If
    End Sub
End Class
