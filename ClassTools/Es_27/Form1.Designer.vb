<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DTP_Start = New System.Windows.Forms.DateTimePicker()
        Me.DTP_End = New System.Windows.Forms.DateTimePicker()
        Me.B_Create = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Viewer1 = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
        Me.SuspendLayout()
        '
        'DTP_Start
        '
        Me.DTP_Start.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTP_Start.Location = New System.Drawing.Point(51, 9)
        Me.DTP_Start.Name = "DTP_Start"
        Me.DTP_Start.Size = New System.Drawing.Size(200, 20)
        Me.DTP_Start.TabIndex = 0
        '
        'DTP_End
        '
        Me.DTP_End.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTP_End.Location = New System.Drawing.Point(51, 35)
        Me.DTP_End.Name = "DTP_End"
        Me.DTP_End.Size = New System.Drawing.Size(200, 20)
        Me.DTP_End.TabIndex = 1
        '
        'B_Create
        '
        Me.B_Create.Location = New System.Drawing.Point(15, 61)
        Me.B_Create.Name = "B_Create"
        Me.B_Create.Size = New System.Drawing.Size(236, 23)
        Me.B_Create.TabIndex = 2
        Me.B_Create.Text = "Create"
        Me.B_Create.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "From:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "To:"
        '
        'Viewer1
        '
        Me.Viewer1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Viewer1.Location = New System.Drawing.Point(25, 149)
        Me.Viewer1.Name = "Viewer1"
        Me.Viewer1.PagesBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        '
        '
        '
        '
        '
        '
        Me.Viewer1.Sidebar.ParametersPanel.ContextMenu = Nothing
        Me.Viewer1.Sidebar.ParametersPanel.Text = "Parameters"
        '
        '
        '
        Me.Viewer1.Sidebar.SearchPanel.ContextMenu = Nothing
        Me.Viewer1.Sidebar.SearchPanel.Text = "Search results"
        '
        '
        '
        Me.Viewer1.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
        Me.Viewer1.Sidebar.ThumbnailsPanel.Text = "Page thumbnails"
        Me.Viewer1.Sidebar.ThumbnailsPanel.Zoom = 0.1R
        '
        '
        '
        Me.Viewer1.Sidebar.TocPanel.ContextMenu = Nothing
        Me.Viewer1.Sidebar.TocPanel.Expanded = True
        Me.Viewer1.Sidebar.TocPanel.Text = "Document map"
        Me.Viewer1.Sidebar.Width = 200
        Me.Viewer1.Size = New System.Drawing.Size(983, 357)
        Me.Viewer1.TabIndex = 5
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1105, 628)
        Me.Controls.Add(Me.Viewer1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.B_Create)
        Me.Controls.Add(Me.DTP_End)
        Me.Controls.Add(Me.DTP_Start)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DTP_Start As DateTimePicker
    Friend WithEvents DTP_End As DateTimePicker
    Friend WithEvents B_Create As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Viewer1 As GrapeCity.ActiveReports.Viewer.Win.Viewer
End Class
