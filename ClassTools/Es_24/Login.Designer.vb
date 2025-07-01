<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        Me.B_Totem = New System.Windows.Forms.Button()
        Me.B_Kitchen = New System.Windows.Forms.Button()
        Me.B_BackOffice = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'B_Totem
        '
        Me.B_Totem.Location = New System.Drawing.Point(77, 74)
        Me.B_Totem.Name = "B_Totem"
        Me.B_Totem.Size = New System.Drawing.Size(75, 23)
        Me.B_Totem.TabIndex = 0
        Me.B_Totem.Text = "Totem"
        Me.B_Totem.UseVisualStyleBackColor = True
        '
        'B_Kitchen
        '
        Me.B_Kitchen.Location = New System.Drawing.Point(179, 74)
        Me.B_Kitchen.Name = "B_Kitchen"
        Me.B_Kitchen.Size = New System.Drawing.Size(75, 23)
        Me.B_Kitchen.TabIndex = 1
        Me.B_Kitchen.Text = "Kitchen"
        Me.B_Kitchen.UseVisualStyleBackColor = True
        '
        'B_BackOffice
        '
        Me.B_BackOffice.Location = New System.Drawing.Point(270, 74)
        Me.B_BackOffice.Name = "B_BackOffice"
        Me.B_BackOffice.Size = New System.Drawing.Size(75, 23)
        Me.B_BackOffice.TabIndex = 2
        Me.B_BackOffice.Text = "BackOffice"
        Me.B_BackOffice.UseVisualStyleBackColor = True
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.B_BackOffice)
        Me.Controls.Add(Me.B_Kitchen)
        Me.Controls.Add(Me.B_Totem)
        Me.Name = "Login"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents B_Totem As Button
    Friend WithEvents B_Kitchen As Button
    Friend WithEvents B_BackOffice As Button
End Class
