Imports System.Reflection
Imports System.Text.Json.Nodes
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports ClassTools
Imports Microsoft.Data.SqlClient

'This Form is used to manage the connection to the SQL Server, you can load the Json file, modify and save the connection strings
Public Class Login_Entity

    'reference of the class connection
    Dim connection As Connections

    'Index of the selected Username in the combobox
    Dim index As Integer

    'Connection to the SQL Server
    Dim connectionToServer As SqlConnection

#Region "FORM"

    'When the form is shown, load the information from the Json file and check if there are external arguments
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        LoadWhenOpened()
        CheckExternalArguments()

    End Sub

    'When the form is opened set the title of the form
    Private Sub F_Es2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Login"
    End Sub

#End Region

#Region "FUNCTIONS"

    ''' <summary>
    ''' Load the information from the Json file when the form is opened
    ''' </summary>
    Private Sub LoadWhenOpened()

        'Load the information of the connections from the file
        connection = ReadFromConfigFile()

        'set the combobox with the connection strings
        CB_Username.DataSource = connection.ConnectionStrings

        'set the textboxes with the connection information
        TB_ServicePort.Text = connection.ServicePort
        TB_Protocol.Text = connection.ProtocolName

        WriteLogMessage("Data loaded from Json File", "EXE", "Log")

    End Sub

    ''' <summary>
    ''' Check if the form is opened with an external argument, if so Login With that argument
    ''' </summary>
    Private Sub CheckExternalArguments()

        'Get the arguments from the command line
        Dim args As String() = Environment.GetCommandLineArgs()

        'Check if the arguments are more than 1
        If args.Count > 1 Then

            Dim x = 0

            For Each arg As String In args

                If Integer.TryParse(arg, x) Then

                    LoginByExternalArgument(args(x))
                    Me.Hide()

                    Exit For

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Execute the login with the external argument
    ''' </summary>
    ''' <param name="i">ID of the User</param>
    Private Sub LoginByExternalArgument(i As Integer)

        'Assign the connection to the connectionToServer variable
        connectionToServer = Crud.ConnectToTheServer(connection.ConnectionStrings(i).UserName, connection.ConnectionStrings(i).Password, connection.ConnectionStrings(i).SQLServerName, connection.ConnectionStrings(i).DatabaseName)
        LoginPerformed()

    End Sub

    ''' <summary>
    ''' Hide the first form, Open the second form and pass the connection to it,
    ''' </summary>
    Private Sub LoginPerformed()

        If connectionToServer IsNot Nothing Then
            If connectionToServer.State = ConnectionState.Open Then

                'Create the second form and pass the connection to it and this form
                Dim Form2 As New Database_Entity(Me, connectionToServer)

                'Show the second form
                Form2.Show()
                Me.Hide()

            End If
        Else

            MessageBox.Show("Connection failed. Please check your credentials.")

        End If
    End Sub

#End Region

#Region "COMBOBOXES"

    'Manage the problem of the combobox when the user insert a new username
    Private Sub CB_Username_TextChanged(sender As Object, e As EventArgs) Handles CB_Username.TextChanged

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

    'Show the information of the selected index in the textboxes
    Private Sub CB_Username_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Username.SelectedIndexChanged

        'Save the selected index
        index = CB_Username.SelectedIndex

        'Set the textboxes with the connection information of the selected index
        TB_Password.Text = connection.ConnectionStrings(index).Password
        TB_ServerName.Text = connection.ConnectionStrings(index).SQLServerName
        TB_DatabaseName.Text = connection.ConnectionStrings(index).DatabaseName

    End Sub

#End Region

#Region "BUTTONS"

    'When the user clicks the button Save, save the information in the Json file
    Private Sub B_Save_Click(sender As Object, e As EventArgs) Handles B_Save.Click

        'Skip if the connection is not initialized
        If connection Is Nothing Then Return

        'Message for the log
        WriteLogMessage("Sovrascritto File JSON", "EXE", "Log")

        'pass the connection to the function to write it on the file
        WriteOnConfigFile(connection)

        'Read the connection from the file and assign it to the connection variable
        connection = ReadFromConfigFile()

        'Update the combobox with the connection strings
        CB_Username.DataSource = connection.ConnectionStrings

    End Sub

    'When the user clicks the button Load, load the information from the Json file
    Private Sub B_Load_Click(sender As Object, e As EventArgs) Handles B_Load.Click

        'Load the information of the connections from the file
        connection = ReadFromConfigFile()

        'set the combobox with the connection strings
        CB_Username.DataSource = connection.ConnectionStrings

        'set the textboxes with the connection information
        TB_ServicePort.Text = connection.ServicePort
        TB_Protocol.Text = connection.ProtocolName

    End Sub

    'When the user click the button Login, check if the connection is valid and open the second form
    Private Sub B_Login_Click(sender As Object, e As EventArgs) Handles B_Login.Click

        'Assign the connection to the connectionToServer variable
        connectionToServer = Crud.ConnectToTheServer(CB_Username.Text, TB_Password.Text, TB_ServerName.Text, TB_DatabaseName.Text)

        LoginPerformed()

    End Sub

#End Region

#Region "TEXTBOXES"

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

    Private Sub TB_ServerName_TextChanged(sender As Object, e As EventArgs) Handles TB_ServerName.TextChanged

        If connection Is Nothing Then Return
        connection.ConnectionStrings(index).SQLServerName = TB_ServerName.Text

    End Sub

    Private Sub TB_DatabaseName_TextChanged(sender As Object, e As EventArgs) Handles TB_DatabaseName.TextChanged

        If connection Is Nothing Then Return
        connection.ConnectionStrings(index).DatabaseName = TB_DatabaseName.Text

    End Sub

#End Region

End Class
