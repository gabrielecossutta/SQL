<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class SectionReport
    Inherits GrapeCity.ActiveReports.SectionReport

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
        End If
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the ActiveReports Designer
    'It can be modified using the ActiveReports Designer.
    'Do not modify it using the code editor.
    Private WithEvents PageHeader As GrapeCity.ActiveReports.SectionReportModel.PageHeader
    Private WithEvents Detail As GrapeCity.ActiveReports.SectionReportModel.Detail
    Private WithEvents PageFooter As GrapeCity.ActiveReports.SectionReportModel.PageFooter
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(SectionReport))
        Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.TB_1 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.TB_2 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.TB_3 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.TB_4 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
        Me.Label1 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label2 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        CType(Me.TB_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TB_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TB_3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TB_4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Label2})
        Me.PageHeader.Height = 0.335!
        Me.PageHeader.Name = "PageHeader"
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.TB_1, Me.TB_2, Me.TB_3, Me.TB_4})
        Me.Detail.Height = 0.1979166!
        Me.Detail.Name = "Detail"
        '
        'TB_1
        '
        Me.TB_1.Height = 0.2!
        Me.TB_1.Left = 0!
        Me.TB_1.Name = "TB_1"
        Me.TB_1.Text = "TextBox1"
        Me.TB_1.Top = 0!
        Me.TB_1.Width = 1.49!
        '
        'TB_2
        '
        Me.TB_2.Height = 0.2!
        Me.TB_2.Left = 1.49!
        Me.TB_2.Name = "TB_2"
        Me.TB_2.Text = "TextBox1"
        Me.TB_2.Top = 0!
        Me.TB_2.Width = 1.49!
        '
        'TB_3
        '
        Me.TB_3.Height = 0.2!
        Me.TB_3.Left = 2.98!
        Me.TB_3.Name = "TB_3"
        Me.TB_3.Text = "TextBox1"
        Me.TB_3.Top = 0!
        Me.TB_3.Width = 1.49!
        '
        'TB_4
        '
        Me.TB_4.Height = 0.2!
        Me.TB_4.Left = 4.47!
        Me.TB_4.Name = "TB_4"
        Me.TB_4.Text = "TextBox1"
        Me.TB_4.Top = 0!
        Me.TB_4.Width = 1.49!
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Label1})
        Me.PageFooter.Name = "PageFooter"
        '
        'Label1
        '
        Me.Label1.Height = 0.2!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 0.1359999!
        Me.Label1.Name = "Label1"
        Me.Label1.Text = "Label1"
        Me.Label1.Top = 0!
        Me.Label1.Width = 1.0!
        '
        'Label2
        '
        Me.Label2.Height = 0.2!
        Me.Label2.HyperLink = Nothing
        Me.Label2.Left = 0.1359999!
        Me.Label2.Name = "Label2"
        Me.Label2.Text = "Label2"
        Me.Label2.Top = 0.052!
        Me.Label2.Width = 1.0!
        '
        'SectionReport
        '
        Me.MasterReport = False
        Me.CompatibilityMode = GrapeCity.ActiveReports.Document.CompatibilityModes.CrossPlatform
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.Sections.Add(Me.PageHeader)
        Me.Sections.Add(Me.Detail)
        Me.Sections.Add(Me.PageFooter)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" &
            "l; font-size: 10pt; color: Black; ddo-char-set: 204", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" &
            "lic", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"))
        CType(Me.TB_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TB_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TB_3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TB_4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private WithEvents TB_1 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents TB_2 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents TB_3 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents TB_4 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label2 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label1 As GrapeCity.ActiveReports.SectionReportModel.Label
End Class
