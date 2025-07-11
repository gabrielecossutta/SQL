Imports System.IO
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.Export.Pdf.Page
Imports GrapeCity.ActiveReports.Rendering.IO

Public Class Form1
    Private Sub B_Create_Click(sender As Object, e As EventArgs) Handles B_Create.Click

        'Retrive the report and assign the dates
        Dim reportPath As String = "C:\Users\Gabriele\Desktop\SQL\ClassTools\Es_27\Report.rdlx"
        Dim report As New PageReport(New FileInfo(reportPath))
        report.Report.ReportParameters.Item(0).DefaultValue.Values.Clear()
        report.Report.ReportParameters.Item(0).DefaultValue.Values.Add(DTP_Start.Value.Date.ToString("MM/dd/yyyy"))
        report.Report.ReportParameters.Item(1).DefaultValue.Values.Clear()
        report.Report.ReportParameters.Item(1).DefaultValue.Values.Add(DTP_End.Value.Date.ToString("MM/dd/yyyy"))
        report.Run()

        'Export the report in PFD
        Dim document As New PageDocument(report)
        Viewer1.LoadDocument(document)
        Dim pdfExport As New PdfRenderingExtension()
        Dim reportoutputPath As String = "C:\Users\Gabriele\Desktop\SQL\ClassTools\PDF"
        Dim outputDir As New DirectoryInfo(reportoutputPath)
        Dim provider As New FileStreamProvider(outputDir, $"From-{DTP_Start.Value.Day}-{DTP_Start.Value.Month}-{DTP_Start.Value.Year}-To-{DTP_End.Value.Day}-{DTP_End.Value.Month}-{DTP_End.Value.Year}")
        provider.OverwriteOutputFile = True
        document.Render(pdfExport, provider)

        'Open the PDF
        Process.Start(Path.Combine(reportoutputPath, $"From-{DTP_Start.Value.Day}-{DTP_Start.Value.Month}-{DTP_Start.Value.Year}-To-{DTP_End.Value.Day}-{DTP_End.Value.Month}-{DTP_End.Value.Year}.pdf"))

    End Sub

    Private Sub Viewer1_Load(sender As Object, e As EventArgs) Handles Viewer1.Load

    End Sub
End Class
