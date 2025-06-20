<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Synchronize
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
        Me.B_BackToTotem = New System.Windows.Forms.Button()
        Me.B_WebServiceOnOff = New System.Windows.Forms.Button()
        Me.B_TotemToBackOffice = New System.Windows.Forms.Button()
        Me.B_SendWebService = New System.Windows.Forms.Button()
        Me.L_OnOff = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'B_BackToTotem
        '
        Me.B_BackToTotem.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_BackToTotem.Location = New System.Drawing.Point(12, 12)
        Me.B_BackToTotem.Name = "B_BackToTotem"
        Me.B_BackToTotem.Size = New System.Drawing.Size(233, 46)
        Me.B_BackToTotem.TabIndex = 0
        Me.B_BackToTotem.Text = "Sync BackOffice to Totem"
        Me.B_BackToTotem.UseVisualStyleBackColor = True
        '
        'B_WebServiceOnOff
        '
        Me.B_WebServiceOnOff.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_WebServiceOnOff.Location = New System.Drawing.Point(251, 12)
        Me.B_WebServiceOnOff.Name = "B_WebServiceOnOff"
        Me.B_WebServiceOnOff.Size = New System.Drawing.Size(254, 46)
        Me.B_WebServiceOnOff.TabIndex = 1
        Me.B_WebServiceOnOff.Text = "START WebService"
        Me.B_WebServiceOnOff.UseVisualStyleBackColor = True
        '
        'B_TotemToBackOffice
        '
        Me.B_TotemToBackOffice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_TotemToBackOffice.Location = New System.Drawing.Point(12, 64)
        Me.B_TotemToBackOffice.Name = "B_TotemToBackOffice"
        Me.B_TotemToBackOffice.Size = New System.Drawing.Size(233, 46)
        Me.B_TotemToBackOffice.TabIndex = 2
        Me.B_TotemToBackOffice.Text = "Sync Totem to BackOffice"
        Me.B_TotemToBackOffice.UseVisualStyleBackColor = True
        '
        'B_SendWebService
        '
        Me.B_SendWebService.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_SendWebService.Location = New System.Drawing.Point(251, 64)
        Me.B_SendWebService.Name = "B_SendWebService"
        Me.B_SendWebService.Size = New System.Drawing.Size(254, 46)
        Me.B_SendWebService.TabIndex = 3
        Me.B_SendWebService.Text = "Send orders with web service"
        Me.B_SendWebService.UseVisualStyleBackColor = True
        '
        'L_OnOff
        '
        Me.L_OnOff.BackColor = System.Drawing.Color.Crimson
        Me.L_OnOff.Location = New System.Drawing.Point(512, 12)
        Me.L_OnOff.Name = "L_OnOff"
        Me.L_OnOff.Size = New System.Drawing.Size(106, 98)
        Me.L_OnOff.TabIndex = 4
        '
        'F_Synchronize
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(627, 122)
        Me.Controls.Add(Me.L_OnOff)
        Me.Controls.Add(Me.B_SendWebService)
        Me.Controls.Add(Me.B_TotemToBackOffice)
        Me.Controls.Add(Me.B_WebServiceOnOff)
        Me.Controls.Add(Me.B_BackToTotem)
        Me.Name = "F_Synchronize"
        Me.Text = "Synchronize"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents B_BackToTotem As Button
    Friend WithEvents B_WebServiceOnOff As Button
    Friend WithEvents B_TotemToBackOffice As Button
    Friend WithEvents B_SendWebService As Button
    Friend WithEvents L_OnOff As Label
End Class
