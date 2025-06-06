Imports Microsoft.Data.SqlClient
Imports ClassTools
Imports System.Text
Imports Microsoft.VisualBasic.Logging
Imports System.Security.Cryptography.Xml
Imports System.Data.Common
Imports System.IO
Imports System.Net.Http
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Runtime.Remoting.Contexts
Imports Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi

''' <summary>
''' This Form is used to manage the CRUD operation on server
''' </summary>
Public Class Database

    'Reference of the connection to the server
    Private connectionToServer As SqlConnection

    'Reference of the first form
    Private form1 As Form

    'List of the tables names
    Private tableNames As New List(Of String)()

    'Datatable from Server
    Dim dataTableDB As DataTable = Nothing

    'Datatable from Web Server
    Dim dataTableWeb As DataTable = Nothing

    'Url of the Webserver
    Dim Url As String

    'Port for the server
    Dim Port As String

    'Array of boolean to check if the lines are correct
    Dim confronti() As Boolean

#Region "FORM"

    'This constructor is used to initialize the form
    Public Sub New(ByVal Form1 As Login, ByVal ConnectionToServer As SqlConnection, Port As String, Url As String)


        'Inizialize components
        InitializeComponent()
        '---------------------------------------cambiare da proprietà -------------------------------------------------------------
        Me.Text = "ES10"
        Me.StartPosition = FormStartPosition.CenterScreen
        '--------------------------------------------------------------------------------------------------------------------------
        Me.connectionToServer = ConnectionToServer
        Me.form1 = Form1
        Me.Url = Url
        Me.Port = Port

        'Check if the connection is open otherwise open it
        If ConnectionToServer.State = ConnectionState.Closed Then

            ConnectionToServer.Open()

        End If


    End Sub

    'This event is triggered when the form is cloased
    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        'Show the first form when the second form is closed
        connectionToServer.Close()
        connectionToServer.Dispose()
        form1.Show()

    End Sub

#End Region

#Region "BUTTONS"

    Private Sub B_Compare_Click(sender As Object, e As EventArgs) Handles B_Compare.Click

        StartDatabaseRequest()
        StartWebRequest()

        'Redraw the Lists
        LB_DataBase.Invalidate()
        LB_Web.Invalidate()

    End Sub

#End Region

#Region "FUNCTIONS DB"

    ''' <summary>
    ''' Retrice the datatable from the Local Server, creare a csv and write it on the listBox
    ''' </summary>
    Private Sub StartDatabaseRequest()

        'Retrive the datatable
        LoadTables(TB_FileName.Text)

        'Create a string containing the csv and write it on a file after checking if is not null
        Dim sb As New StringBuilder()
        sb = CreateCsvFile(sb, dataTableDB)

        If dataTableDB.Columns.Count < 1 Then
            Return
        End If

        Utils.WriteAFile(sb.ToString(), "EXE/CSV", $"{TB_FileName.Text}Db", "csv", False)

        'Divide every line in a string to write in the ListBox
        Dim lines() As String = sb.ToString().Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)

        'Clear and write every line in the ListBox
        LB_DataBase.Items.Clear()
        For Each line As String In lines

            LB_DataBase.Items.Add(line)

        Next

    End Sub

    ''' <summary>
    ''' Load the tables name from the SQL Server and populate the TabControl with them
    ''' </summary>
    Private Sub LoadTables(tableName As String)

        'Put [] in case the tablename as space in it
        tableName = $"[{tableName}]"

        Dim query As String = $"SELECT * FROM {tableName}"

        'Try to get the Datatable or write the error in the log
        Try

            dataTableDB = Crud.FillTables(query, connectionToServer)

        Catch ex As Exception

            WriteLogMessage(ex.Message, "EXE", "LogEs10")

        End Try

    End Sub

#End Region

#Region "FUNCTIONS WEB"

    ''' <summary>
    ''' Retrice the datatable from the Web Server, creare a csv and write it on the listBox
    ''' </summary>
    Private Async Sub StartWebRequest()

        'Replace every space in the filename with the URL code for space 
        Dim fileName As String = TB_FileName.Text
        fileName = fileName.Replace(" ", "%20")

        'Replace the default value from the URL to create the specific URL for the page on the Webserver
        Dim SpecificUrl As String = Url.Replace("@Port", Port)
        SpecificUrl = SpecificUrl.Replace("@FileName", fileName)

        'Create a request and check if the server give a response, then save the result
        Using request As New HttpClient()

            Dim response As HttpResponseMessage = Await request.GetAsync(SpecificUrl)

            If response.IsSuccessStatusCode Then

                Dim result As String = Await response.Content.ReadAsStringAsync()

                'Trasform the string containing the html in a datatable
                dataTableWeb = HtmlToDataTable(result)

                'Save the html in a log file
                Utils.WriteAFile(result, "EXE/CSV/LOGS", TB_FileName.Text + "Log", "txt", False)

                'Trasform the datatable in a csv file and save it
                Dim sb As New StringBuilder()
                sb = CreateCsvFile(sb, dataTableWeb)

                If dataTableWeb Is Nothing Then
                    Return
                End If

                Utils.WriteAFile(sb.ToString(), "EXE/CSV", TB_FileName.Text + "Web", "csv", False)

                'Divide every line in a string to write in the ListBox
                Dim lines() As String = sb.ToString().Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)

                'Clear and write every line in the ListBox
                LB_Web.Items.Clear()
                For Each line As String In lines
                    LB_Web.Items.Add(line)
                Next

                'Compare the 2 listboxes
                CompareListBox()

            Else

                MessageBox.Show("Error: " & response.StatusCode.ToString())

            End If

        End Using


    End Sub

    ''' <summary>
    ''' Turn the HTML in a datatable
    ''' </summary>
    ''' <param name="html">String html to turn in Datatable</param>
    ''' <returns></returns>
    Public Function HtmlToDataTable(html As String) As DataTable

        Dim dt As New DataTable()

        'Find the tag Table and check if is not null
        Dim tableMatch As Match = Regex.Match(html, "<table.*?>(.*?)</table>", RegexOptions.Singleline Or RegexOptions.IgnoreCase)
        If Not tableMatch.Success Then

            Return Nothing

        End If

        'Save the match in a string
        Dim tableHtml As String = tableMatch.Groups(1).Value

        'Find every <tr> tag 
        Dim rowMatches As MatchCollection = Regex.Matches(tableHtml, "<tr.*?>(.*?)</tr>", RegexOptions.Singleline Or RegexOptions.IgnoreCase)

        Dim isFirstRow As Boolean = True

        For Each rowMatch As Match In rowMatches

            Dim rowHtml As String = rowMatch.Groups(1).Value

            ' Trova celle (th o td)
            Dim cellMatches As MatchCollection = Regex.Matches(rowHtml, "<t[dh].*?>(.*?)</t[dh]>", RegexOptions.Singleline Or RegexOptions.IgnoreCase)

            'Check if it is the first row set those as columns of the datatable, or set the value as the column data
            If isFirstRow Then

                For Each cellMatch As Match In cellMatches

                    'For every match remove the html tag and set the names of the columns
                    dt.Columns.Add(Regex.Replace(cellMatch.Groups(1).Value.Trim(), "<.*?>", ""))

                Next

                isFirstRow = False

            Else

                Dim i As Integer = 0

                'Create a new row and set all the data
                Dim newRow As DataRow = dt.NewRow()
                For Each cellMatch As Match In cellMatches

                    'For every match remove the html tag
                    newRow(i) = Regex.Replace(cellMatch.Groups(1).Value.Trim(), "<.*?>", "")
                    i += 1

                Next
                dt.Rows.Add(newRow)

            End If

        Next

        Return dt

    End Function

#End Region

#Region "Generic Functions"

    ''' <summary>
    ''' Convert a datatable in a csv on a string
    ''' </summary>
    ''' <param name="sb"> String to save the csv file</param>
    ''' <param name="datatable"> Datatable to turn in a csv string</param>
    Private Function CreateCsvFile(sb As StringBuilder, datatable As DataTable) As StringBuilder

        If datatable.Columns.Count < 1 Then
            Return Nothing
        End If

        datatable = OrderListBy(datatable)

        'Append every Columnname with a ;
        For Each col As DataColumn In datatable.Columns
            sb.Append(col.ColumnName & ";")
        Next

        'Check is the string is not empty
        If sb.Length < 1 Then

            Return Nothing

        End If

        'Remove the last ; and go to the next line 
        sb.Length -= 1
        sb.AppendLine()

        'Append every Data with a ;
        For Each row As DataRow In datatable.Rows

            For Each col As DataColumn In datatable.Columns

                sb.Append(row(col).ToString().TrimEnd() & ";")

            Next

            'Remove the last ; and go to the next line 
            sb.Length -= 1
            sb.AppendLine()

        Next

        Return sb

    End Function

    ''' <summary>
    ''' Randomly sort the datatable
    ''' </summary>
    ''' <param name="dataTable"></param>
    ''' <returns></returns>
    Private Function OrderListBy(dataTable As DataTable) As DataTable

        Dim rnd As New Random()
        dataTable.DefaultView.Sort = dataTable.Columns(rnd.Next(0, dataTable.Columns.Count - 1)).ColumnName
        Return dataTable.DefaultView.ToTable()

    End Function

    ''' <summary>
    ''' Compare every lines of the 2 lists
    ''' </summary>
    Private Sub CompareListBox()

        'Allow to change color of the listBox
        LB_Web.DrawMode = DrawMode.OwnerDrawFixed
        LB_DataBase.DrawMode = DrawMode.OwnerDrawFixed

        'Find the max Size of the array and resize it
        Dim maxItems = LB_Web.Items.Count - 1
        ReDim confronti(maxItems)

        'compare and save true every time the lines are equals
        For i = 0 To maxItems

            confronti(i) = (LB_DataBase.Items(i).ToString() = LB_Web.Items(i).ToString())

        Next

    End Sub

#End Region

#Region "ListBox"
    Private Sub LB_DataBase_DrawItem(sender As Object, e As DrawItemEventArgs) Handles LB_DataBase.DrawItem

        'Check the array of boolean and change color based on that, and draw the string
        Dim brush As Brush = If(confronti(e.Index), Brushes.LightGreen, Brushes.LightCoral)
        e.Graphics.DrawString(LB_DataBase.Items(e.Index).ToString(), e.Font, brush, e.Bounds)

    End Sub

    Private Sub LB_Web_DrawItem(sender As Object, e As DrawItemEventArgs) Handles LB_Web.DrawItem

        'Check the array of boolean and change color based on that, and draw the string
        Dim brush As Brush = If(confronti(e.Index), Brushes.LightGreen, Brushes.LightCoral)
        e.Graphics.DrawString(LB_Web.Items(e.Index).ToString(), e.Font, brush, e.Bounds)

    End Sub

#End Region


End Class