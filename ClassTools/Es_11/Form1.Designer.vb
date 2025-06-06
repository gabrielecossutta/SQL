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
        Me.TB_ResultDownload = New System.Windows.Forms.TextBox()
        Me.L_Download = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'B_Download
        '
        Me.B_Download.Location = New System.Drawing.Point(12, 51)
        Me.B_Download.Name = "B_Download"
        Me.B_Download.Size = New System.Drawing.Size(100, 20)
        Me.B_Download.TabIndex = 0
        Me.B_Download.Text = "Download"
        Me.B_Download.UseVisualStyleBackColor = True
        '
        'TB_ResultDownload
        '
        Me.TB_ResultDownload.Location = New System.Drawing.Point(12, 25)
        Me.TB_ResultDownload.Name = "TB_ResultDownload"
        Me.TB_ResultDownload.Size = New System.Drawing.Size(100, 20)
        Me.TB_ResultDownload.TabIndex = 1
        '
        'L_Download
        '
        Me.L_Download.AutoSize = True
        Me.L_Download.Location = New System.Drawing.Point(33, 9)
        Me.L_Download.Name = "L_Download"
        Me.L_Download.Size = New System.Drawing.Size(55, 13)
        Me.L_Download.TabIndex = 3
        Me.L_Download.Text = "Download"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(124, 86)
        Me.Controls.Add(Me.L_Download)
        Me.Controls.Add(Me.TB_ResultDownload)
        Me.Controls.Add(Me.B_Download)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents B_Download As Button
    Friend WithEvents TB_ResultDownload As TextBox
    Friend WithEvents L_Download As Label
End Class
