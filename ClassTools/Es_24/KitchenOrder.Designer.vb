<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KitchenOrder
    Inherits System.Windows.Forms.UserControl

    'UserControl esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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
        Me.P_Order = New System.Windows.Forms.Panel()
        Me.B_OrderComplete = New System.Windows.Forms.Button()
        Me.LB_ItemList = New System.Windows.Forms.ListBox()
        Me.P_Order.SuspendLayout()
        Me.SuspendLayout()
        '
        'P_Order
        '
        Me.P_Order.Controls.Add(Me.B_OrderComplete)
        Me.P_Order.Controls.Add(Me.LB_ItemList)
        Me.P_Order.Location = New System.Drawing.Point(0, 0)
        Me.P_Order.Name = "P_Order"
        Me.P_Order.Size = New System.Drawing.Size(259, 160)
        Me.P_Order.TabIndex = 3
        '
        'B_OrderComplete
        '
        Me.B_OrderComplete.Location = New System.Drawing.Point(3, 134)
        Me.B_OrderComplete.Name = "B_OrderComplete"
        Me.B_OrderComplete.Size = New System.Drawing.Size(253, 23)
        Me.B_OrderComplete.TabIndex = 1
        Me.B_OrderComplete.Text = "Complete Order"
        Me.B_OrderComplete.UseVisualStyleBackColor = True
        '
        'LB_ItemList
        '
        Me.LB_ItemList.FormattingEnabled = True
        Me.LB_ItemList.Location = New System.Drawing.Point(0, 0)
        Me.LB_ItemList.Name = "LB_ItemList"
        Me.LB_ItemList.Size = New System.Drawing.Size(259, 134)
        Me.LB_ItemList.TabIndex = 0
        '
        'KitchenOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.P_Order)
        Me.Name = "KitchenOrder"
        Me.Size = New System.Drawing.Size(259, 160)
        Me.P_Order.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents P_Order As Panel
    Friend WithEvents B_OrderComplete As Button
    Friend WithEvents LB_ItemList As ListBox
End Class
