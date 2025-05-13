Imports ClassTools

Public Class Form1
    Dim connection As Connections
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        '------------------------ -------------------Salva dati nella classe e poi poi nel json----------------
        ''Check if the textboxes are empty
        'If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then

        '    'If any of the textboxes are empty, show a message box
        '    MessageBox.Show("fill all the fields")
        '    Return
        'End If

        ''If all the textboxes are filled, pass the values to the WriteOnFile method
        'WriteOnFile(TextBox1.Text, TextBox2.Text, TextBox3.Text)

        ''Clear the textboxes
        'TextBox1.Text = ""
        'TextBox2.Text = ""
        'TextBox3.Text = ""
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        '-----------------------------------------CARICA DA Json nella classe poi visualizza NELLE TEXTBOX
        connection = Populate()
        ComboBox1.DataSource = connection.ConnectionStrings
        'ComboBox1.DisplayMember = "UserName"


        ''Retreve json string from file
        'Dim JsonString = ReadFromFile()

        ''Split the json string into segments
        'Dim SegmentedStrings = JsonString.Split(","c)

        ''Array to hold the data
        'Dim Data(2) As String

        ''Check if there are all the three segments
        'If SegmentedStrings.Length >= 3 Then
        '    For index = 0 To 2

        '        'Name of the Data from the SegmentedStrings
        '        Data(index) = SegmentedStrings(index).Split(":"c)(1)
        '    Next

        '    'Set the textboxes with the data
        '    TextBox1.Text = Data(0)
        '    TextBox2.Text = Data(1)
        '    TextBox3.Text = Data(2)

        'End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '----------------------------------------CARICARE IL JSON NELLA COMBOBOX------------------------


        'VVVVVVVVVVVVVVVVVVVVVVVVVVVVVV chiedi di questo
        'ComboBox1.DataSource = connection.ConnectionStrings
        'ComboBox1.DisplayMember = "UserName"

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'codice che cambia le text box quando cambi fai una scelta nell combobox
        Dim indice As Integer = ComboBox1.SelectedIndex
        TextBox4.Text = connection.ConnectionStrings(indice).Password
        TextBox6.Text = connection.ConnectionStrings(indice).SQLServerName
        TextBox5.Text = connection.ConnectionStrings(indice).DatabaseName


    End Sub

    Private Sub ConnectionStringBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles ConnectionStringBindingSource.CurrentChanged

    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub
End Class
