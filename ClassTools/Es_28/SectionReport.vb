Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports GrapeCity.ActiveReports.SectionReportModel
Imports Microsoft.SqlServer.Management.Sdk.Sfc
Imports System.Xml
Imports System.Text
Public Class SectionReport
    Inherits GrapeCity.ActiveReports.SectionReport

    Public Sub New(startDate As DateTime, endDate As DateTime)
        InitializeComponent()

        'Retrive the data from the db
        Dim dt As DataTable = GetSummaries(startDate, endDate)
        Me.DataSource = dt

        'Connect the textbox to the data
        TB_1.DataField = "IdProduct"
        TB_2.DataField = "RegistrationDate"
        TB_3.DataField = "TotalQuantity"
        TB_4.DataField = "TotalPrice"

    End Sub





    Private Function GetSummaries(startDate As DateTime, endDate As DateTime) As DataTable

        Dim connectionString As String = "Data Source=EIDF014641\SQLEXPRESS;Initial Catalog=McDonald;Integrated Security=False;User ID=UserName;Password=123"
        Dim query As String = "SELECT * FROM Summaries WHERE RegistrationDate >= @StartDate AND RegistrationDate <= @EndDate"
        Dim dt As New DataTable()
        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@StartDate", startDate)
                cmd.Parameters.AddWithValue("@EndDate", endDate)
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
        End Using
        Return dt

    End Function

End Class
