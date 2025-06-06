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
        Me.B_Download = New System.Windows.Forms.Button()
        Me.TB_SameDimension = New System.Windows.Forms.TextBox()
        Me.TB_NumberOfFileDownloaded = New System.Windows.Forms.TextBox()
        Me.TB_DownloadCompleted = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'B_Download
        '
        Me.B_Download.Location = New System.Drawing.Point(12, 90)
        Me.B_Download.Name = "B_Download"
        Me.B_Download.Size = New System.Drawing.Size(229, 20)
        Me.B_Download.TabIndex = 0
        Me.B_Download.Text = "Download"
        Me.B_Download.UseVisualStyleBackColor = True
        '
        'TB_SameDimension
        '
        Me.TB_SameDimension.Location = New System.Drawing.Point(12, 64)
        Me.TB_SameDimension.Name = "TB_SameDimension"
        Me.TB_SameDimension.Size = New System.Drawing.Size(229, 20)
        Me.TB_SameDimension.TabIndex = 1
        '
        'TB_NumberOfFileDownloaded
        '
        Me.TB_NumberOfFileDownloaded.Location = New System.Drawing.Point(12, 38)
        Me.TB_NumberOfFileDownloaded.Name = "TB_NumberOfFileDownloaded"
        Me.TB_NumberOfFileDownloaded.Size = New System.Drawing.Size(229, 20)
        Me.TB_NumberOfFileDownloaded.TabIndex = 2
        '
        'TB_DownloadCompleted
        '
        Me.TB_DownloadCompleted.Location = New System.Drawing.Point(12, 12)
        Me.TB_DownloadCompleted.Name = "TB_DownloadCompleted"
        Me.TB_DownloadCompleted.Size = New System.Drawing.Size(229, 20)
        Me.TB_DownloadCompleted.TabIndex = 3
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(256, 124)
        Me.Controls.Add(Me.TB_DownloadCompleted)
        Me.Controls.Add(Me.TB_NumberOfFileDownloaded)
        Me.Controls.Add(Me.TB_SameDimension)
        Me.Controls.Add(Me.B_Download)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents B_Download As Button
    Friend WithEvents TB_SameDimension As TextBox
    Friend WithEvents TB_NumberOfFileDownloaded As TextBox
    Friend WithEvents TB_DownloadCompleted As TextBox
End Class
