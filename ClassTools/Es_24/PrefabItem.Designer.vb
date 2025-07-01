<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrefabItem
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
        Me.B_Remove = New System.Windows.Forms.Button()
        Me.L_ProductPrice = New System.Windows.Forms.Label()
        Me.B_Add = New System.Windows.Forms.Button()
        Me.L_ProductQuantity = New System.Windows.Forms.Label()
        Me.L_ItemName = New System.Windows.Forms.Label()
        Me.P_Order.SuspendLayout()
        Me.SuspendLayout()
        '
        'P_Order
        '
        Me.P_Order.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.P_Order.Controls.Add(Me.B_Remove)
        Me.P_Order.Controls.Add(Me.L_ProductPrice)
        Me.P_Order.Controls.Add(Me.B_Add)
        Me.P_Order.Controls.Add(Me.L_ProductQuantity)
        Me.P_Order.Controls.Add(Me.L_ItemName)
        Me.P_Order.Location = New System.Drawing.Point(0, 0)
        Me.P_Order.Name = "P_Order"
        Me.P_Order.Size = New System.Drawing.Size(260, 46)
        Me.P_Order.TabIndex = 7
        '
        'B_Remove
        '
        Me.B_Remove.Location = New System.Drawing.Point(85, 10)
        Me.B_Remove.Name = "B_Remove"
        Me.B_Remove.Size = New System.Drawing.Size(21, 23)
        Me.B_Remove.TabIndex = 5
        Me.B_Remove.Text = "--"
        Me.B_Remove.UseVisualStyleBackColor = True
        '
        'L_ProductPrice
        '
        Me.L_ProductPrice.AutoSize = True
        Me.L_ProductPrice.Location = New System.Drawing.Point(158, 15)
        Me.L_ProductPrice.Name = "L_ProductPrice"
        Me.L_ProductPrice.Size = New System.Drawing.Size(67, 13)
        Me.L_ProductPrice.TabIndex = 4
        Me.L_ProductPrice.Text = "Price:  1,00€"
        '
        'B_Add
        '
        Me.B_Add.Location = New System.Drawing.Point(131, 10)
        Me.B_Add.Name = "B_Add"
        Me.B_Add.Size = New System.Drawing.Size(21, 23)
        Me.B_Add.TabIndex = 3
        Me.B_Add.Text = "+"
        Me.B_Add.UseVisualStyleBackColor = True
        '
        'L_ProductQuantity
        '
        Me.L_ProductQuantity.AutoSize = True
        Me.L_ProductQuantity.Location = New System.Drawing.Point(112, 15)
        Me.L_ProductQuantity.Name = "L_ProductQuantity"
        Me.L_ProductQuantity.Size = New System.Drawing.Size(13, 13)
        Me.L_ProductQuantity.TabIndex = 1
        Me.L_ProductQuantity.Text = "1"
        '
        'L_ItemName
        '
        Me.L_ItemName.AutoSize = True
        Me.L_ItemName.Location = New System.Drawing.Point(20, 15)
        Me.L_ItemName.Name = "L_ItemName"
        Me.L_ItemName.Size = New System.Drawing.Size(59, 13)
        Me.L_ItemName.TabIndex = 0
        Me.L_ItemName.Text = "Hamburger"
        '
        'PrefabItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.P_Order)
        Me.Name = "PrefabItem"
        Me.Size = New System.Drawing.Size(260, 46)
        Me.P_Order.ResumeLayout(False)
        Me.P_Order.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents P_Order As Panel
    Friend WithEvents B_Remove As Button
    Friend WithEvents L_ProductPrice As Label
    Friend WithEvents B_Add As Button
    Friend WithEvents L_ProductQuantity As Label
    Friend WithEvents L_ItemName As Label
End Class
