<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_Kitchen
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
        Me.FLP_KitchenOrders = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'FLP_KitchenOrders
        '
        Me.FLP_KitchenOrders.Location = New System.Drawing.Point(0, 0)
        Me.FLP_KitchenOrders.Name = "FLP_KitchenOrders"
        Me.FLP_KitchenOrders.Size = New System.Drawing.Size(796, 450)
        Me.FLP_KitchenOrders.TabIndex = 0
        '
        'F_Kitchen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.FLP_KitchenOrders)
        Me.Name = "F_Kitchen"
        Me.Text = "Kitchen"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents FLP_KitchenOrders As FlowLayoutPanel
End Class
