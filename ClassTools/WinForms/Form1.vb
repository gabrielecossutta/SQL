Imports System.Text.Json.Nodes
Imports ClassTools

Public Class Form1

    'reference of the class connection
    Dim connection As Connections

    'Index of the selected Username in the combobox
    Dim index As Integer

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Skip if the connection is not initialized
        If connection Is Nothing Then Return

        'pass the connection to the function to write it on the file
        WriteOnFile(connection)

        'Read the connection from the file and assign it to the connection variable
        connection = ReadFromFile()

        'Update the combobox with the connection strings
        ComboBox1.DataSource = connection.ConnectionStrings

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

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
End Class
