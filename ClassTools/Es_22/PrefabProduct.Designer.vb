<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PrefabProduct
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
        Me.P_HamburgersProducts = New System.Windows.Forms.Panel()
        Me.PB_ImageProduct = New System.Windows.Forms.PictureBox()
        Me.L_PriceProduct = New System.Windows.Forms.Label()
        Me.L_ProductName = New System.Windows.Forms.Label()
        Me.P_HamburgersProducts.SuspendLayout()
        CType(Me.PB_ImageProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'P_HamburgersProducts
        '
        Me.P_HamburgersProducts.BackColor = System.Drawing.Color.Gainsboro
        Me.P_HamburgersProducts.Controls.Add(Me.PB_ImageProduct)
        Me.P_HamburgersProducts.Controls.Add(Me.L_PriceProduct)
        Me.P_HamburgersProducts.Controls.Add(Me.L_ProductName)
        Me.P_HamburgersProducts.Location = New System.Drawing.Point(0, 0)
        Me.P_HamburgersProducts.Name = "P_HamburgersProducts"
        Me.P_HamburgersProducts.Size = New System.Drawing.Size(200, 200)
        Me.P_HamburgersProducts.TabIndex = 2
        '
        'PB_ImageProduct
        '
        Me.PB_ImageProduct.Location = New System.Drawing.Point(0, 0)
        Me.PB_ImageProduct.Name = "PB_ImageProduct"
        Me.PB_ImageProduct.Size = New System.Drawing.Size(200, 164)
        Me.PB_ImageProduct.TabIndex = 3
        Me.PB_ImageProduct.TabStop = False
        '
        'L_PriceProduct
        '
        Me.L_PriceProduct.AutoSize = True
        Me.L_PriceProduct.Location = New System.Drawing.Point(106, 176)
        Me.L_PriceProduct.Name = "L_PriceProduct"
        Me.L_PriceProduct.Size = New System.Drawing.Size(61, 13)
        Me.L_PriceProduct.TabIndex = 2
        Me.L_PriceProduct.Text = "Price:0,00€"
        '
        'L_ProductName
        '
        Me.L_ProductName.AutoSize = True
        Me.L_ProductName.Location = New System.Drawing.Point(22, 176)
        Me.L_ProductName.Name = "L_ProductName"
        Me.L_ProductName.Size = New System.Drawing.Size(59, 13)
        Me.L_ProductName.TabIndex = 1
        Me.L_ProductName.Text = "Hamburger"
        '
        'PrefabProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.P_HamburgersProducts)
        Me.Name = "PrefabProduct"
        Me.Size = New System.Drawing.Size(200, 200)
        Me.P_HamburgersProducts.ResumeLayout(False)
        Me.P_HamburgersProducts.PerformLayout()
        CType(Me.PB_ImageProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents P_HamburgersProducts As Panel
    Friend WithEvents PB_ImageProduct As PictureBox
    Friend WithEvents L_PriceProduct As Label
    Friend WithEvents L_ProductName As Label
End Class
