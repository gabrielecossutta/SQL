<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Database
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.LB_DataBase = New System.Windows.Forms.ListBox()
        Me.LB_Web = New System.Windows.Forms.ListBox()
        Me.TB_FileName = New System.Windows.Forms.TextBox()
        Me.B_Compare = New System.Windows.Forms.Button()
        Me.L_WebRequest = New System.Windows.Forms.Label()
        Me.L_DataBase = New System.Windows.Forms.Label()
        Me.L_NameFile = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LB_DataBase
        '
        Me.LB_DataBase.AllowDrop = True
        Me.LB_DataBase.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_DataBase.FormattingEnabled = True
        Me.LB_DataBase.HorizontalScrollbar = True
        Me.LB_DataBase.ItemHeight = 20
        Me.LB_DataBase.Location = New System.Drawing.Point(949, 25)
        Me.LB_DataBase.Name = "LB_DataBase"
        Me.LB_DataBase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LB_DataBase.ScrollAlwaysVisible = True
        Me.LB_DataBase.Size = New System.Drawing.Size(931, 444)
        Me.LB_DataBase.TabIndex = 6
        '
        'LB_Web
        '
        Me.LB_Web.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LB_Web.FormattingEnabled = True
        Me.LB_Web.HorizontalScrollbar = True
        Me.LB_Web.ItemHeight = 20
        Me.LB_Web.Location = New System.Drawing.Point(12, 25)
        Me.LB_Web.Name = "LB_Web"
        Me.LB_Web.ScrollAlwaysVisible = True
        Me.LB_Web.Size = New System.Drawing.Size(931, 444)
        Me.LB_Web.TabIndex = 7
        '
        'TB_FileName
        '
        Me.TB_FileName.Location = New System.Drawing.Point(827, 492)
        Me.TB_FileName.Name = "TB_FileName"
        Me.TB_FileName.Size = New System.Drawing.Size(241, 20)
        Me.TB_FileName.TabIndex = 8
        '
        'B_Compare
        '
        Me.B_Compare.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_Compare.Location = New System.Drawing.Point(841, 518)
        Me.B_Compare.Name = "B_Compare"
        Me.B_Compare.Size = New System.Drawing.Size(209, 52)
        Me.B_Compare.TabIndex = 9
        Me.B_Compare.Text = "Compare"
        Me.B_Compare.UseVisualStyleBackColor = True
        '
        'L_WebRequest
        '
        Me.L_WebRequest.AutoSize = True
        Me.L_WebRequest.Location = New System.Drawing.Point(404, 9)
        Me.L_WebRequest.Name = "L_WebRequest"
        Me.L_WebRequest.Size = New System.Drawing.Size(70, 13)
        Me.L_WebRequest.TabIndex = 10
        Me.L_WebRequest.Text = "WebRequest"
        '
        'L_DataBase
        '
        Me.L_DataBase.AutoSize = True
        Me.L_DataBase.Location = New System.Drawing.Point(1386, 9)
        Me.L_DataBase.Name = "L_DataBase"
        Me.L_DataBase.Size = New System.Drawing.Size(54, 13)
        Me.L_DataBase.TabIndex = 11
        Me.L_DataBase.Text = "DataBase"
        '
        'L_NameFile
        '
        Me.L_NameFile.AutoSize = True
        Me.L_NameFile.Location = New System.Drawing.Point(906, 476)
        Me.L_NameFile.Name = "L_NameFile"
        Me.L_NameFile.Size = New System.Drawing.Size(81, 13)
        Me.L_NameFile.TabIndex = 12
        Me.L_NameFile.Text = "Name of the file"
        '
        'Database
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1887, 596)
        Me.Controls.Add(Me.L_NameFile)
        Me.Controls.Add(Me.L_DataBase)
        Me.Controls.Add(Me.L_WebRequest)
        Me.Controls.Add(Me.B_Compare)
        Me.Controls.Add(Me.TB_FileName)
        Me.Controls.Add(Me.LB_Web)
        Me.Controls.Add(Me.LB_DataBase)
        Me.Name = "Database"
        Me.Text = "Form2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LB_DataBase As ListBox
    Friend WithEvents LB_Web As ListBox
    Friend WithEvents TB_FileName As TextBox
    Friend WithEvents B_Compare As Button
    Friend WithEvents L_WebRequest As Label
    Friend WithEvents L_DataBase As Label
    Friend WithEvents L_NameFile As Label
End Class
