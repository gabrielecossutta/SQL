<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_Totem
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
        Me.B_Order = New System.Windows.Forms.Button()
        Me.L_TotalPrice = New System.Windows.Forms.Label()
        Me.TC_Menu = New System.Windows.Forms.TabControl()
        Me.TP_Hamburgers = New System.Windows.Forms.TabPage()
        Me.FLP_Hamburgers = New System.Windows.Forms.FlowLayoutPanel()
        Me.TP_Delicacies = New System.Windows.Forms.TabPage()
        Me.FLP_Appetizers = New System.Windows.Forms.FlowLayoutPanel()
        Me.TP_Dessert = New System.Windows.Forms.TabPage()
        Me.FLP_Dessert = New System.Windows.Forms.FlowLayoutPanel()
        Me.TP_Drinks = New System.Windows.Forms.TabPage()
        Me.FLP_Drinks = New System.Windows.Forms.FlowLayoutPanel()
        Me.TP_Sauce = New System.Windows.Forms.TabPage()
        Me.FLP_Sauce = New System.Windows.Forms.FlowLayoutPanel()
        Me.FLP_OrderList = New System.Windows.Forms.FlowLayoutPanel()
        Me.TC_Menu.SuspendLayout()
        Me.TP_Hamburgers.SuspendLayout()
        Me.TP_Delicacies.SuspendLayout()
        Me.TP_Dessert.SuspendLayout()
        Me.TP_Drinks.SuspendLayout()
        Me.TP_Sauce.SuspendLayout()
        Me.SuspendLayout()
        '
        'B_Order
        '
        Me.B_Order.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_Order.Location = New System.Drawing.Point(1293, 644)
        Me.B_Order.Name = "B_Order"
        Me.B_Order.Size = New System.Drawing.Size(284, 37)
        Me.B_Order.TabIndex = 0
        Me.B_Order.Text = "Order"
        Me.B_Order.UseVisualStyleBackColor = True
        '
        'L_TotalPrice
        '
        Me.L_TotalPrice.AutoSize = True
        Me.L_TotalPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.L_TotalPrice.Location = New System.Drawing.Point(1342, 616)
        Me.L_TotalPrice.Name = "L_TotalPrice"
        Me.L_TotalPrice.Size = New System.Drawing.Size(181, 25)
        Me.L_TotalPrice.TabIndex = 1
        Me.L_TotalPrice.Text = "Total Price: 0,00€"
        '
        'TC_Menu
        '
        Me.TC_Menu.Controls.Add(Me.TP_Hamburgers)
        Me.TC_Menu.Controls.Add(Me.TP_Delicacies)
        Me.TC_Menu.Controls.Add(Me.TP_Dessert)
        Me.TC_Menu.Controls.Add(Me.TP_Drinks)
        Me.TC_Menu.Controls.Add(Me.TP_Sauce)
        Me.TC_Menu.Location = New System.Drawing.Point(8, 2)
        Me.TC_Menu.Name = "TC_Menu"
        Me.TC_Menu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TC_Menu.SelectedIndex = 0
        Me.TC_Menu.Size = New System.Drawing.Size(1279, 679)
        Me.TC_Menu.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TC_Menu.TabIndex = 2
        '
        'TP_Hamburgers
        '
        Me.TP_Hamburgers.Controls.Add(Me.FLP_Hamburgers)
        Me.TP_Hamburgers.Location = New System.Drawing.Point(4, 22)
        Me.TP_Hamburgers.Name = "TP_Hamburgers"
        Me.TP_Hamburgers.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_Hamburgers.Size = New System.Drawing.Size(1271, 653)
        Me.TP_Hamburgers.TabIndex = 0
        Me.TP_Hamburgers.Text = "Hamburgers"
        Me.TP_Hamburgers.UseVisualStyleBackColor = True
        '
        'FLP_Hamburgers
        '
        Me.FLP_Hamburgers.AutoScroll = True
        Me.FLP_Hamburgers.Location = New System.Drawing.Point(0, 0)
        Me.FLP_Hamburgers.Name = "FLP_Hamburgers"
        Me.FLP_Hamburgers.Size = New System.Drawing.Size(1272, 652)
        Me.FLP_Hamburgers.TabIndex = 0
        '
        'TP_Delicacies
        '
        Me.TP_Delicacies.Controls.Add(Me.FLP_Appetizers)
        Me.TP_Delicacies.Location = New System.Drawing.Point(4, 22)
        Me.TP_Delicacies.Name = "TP_Delicacies"
        Me.TP_Delicacies.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_Delicacies.Size = New System.Drawing.Size(1271, 653)
        Me.TP_Delicacies.TabIndex = 1
        Me.TP_Delicacies.Text = "Appetizers "
        Me.TP_Delicacies.UseVisualStyleBackColor = True
        '
        'FLP_Appetizers
        '
        Me.FLP_Appetizers.AutoScroll = True
        Me.FLP_Appetizers.Location = New System.Drawing.Point(0, 0)
        Me.FLP_Appetizers.Name = "FLP_Appetizers"
        Me.FLP_Appetizers.Size = New System.Drawing.Size(1272, 652)
        Me.FLP_Appetizers.TabIndex = 1
        '
        'TP_Dessert
        '
        Me.TP_Dessert.Controls.Add(Me.FLP_Dessert)
        Me.TP_Dessert.Location = New System.Drawing.Point(4, 22)
        Me.TP_Dessert.Name = "TP_Dessert"
        Me.TP_Dessert.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_Dessert.Size = New System.Drawing.Size(1271, 653)
        Me.TP_Dessert.TabIndex = 2
        Me.TP_Dessert.Text = "Dessert"
        Me.TP_Dessert.UseVisualStyleBackColor = True
        '
        'FLP_Dessert
        '
        Me.FLP_Dessert.AutoScroll = True
        Me.FLP_Dessert.Location = New System.Drawing.Point(0, 0)
        Me.FLP_Dessert.Name = "FLP_Dessert"
        Me.FLP_Dessert.Size = New System.Drawing.Size(1272, 653)
        Me.FLP_Dessert.TabIndex = 1
        '
        'TP_Drinks
        '
        Me.TP_Drinks.Controls.Add(Me.FLP_Drinks)
        Me.TP_Drinks.Location = New System.Drawing.Point(4, 22)
        Me.TP_Drinks.Name = "TP_Drinks"
        Me.TP_Drinks.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_Drinks.Size = New System.Drawing.Size(1271, 653)
        Me.TP_Drinks.TabIndex = 3
        Me.TP_Drinks.Text = "Drinks"
        Me.TP_Drinks.UseVisualStyleBackColor = True
        '
        'FLP_Drinks
        '
        Me.FLP_Drinks.AutoScroll = True
        Me.FLP_Drinks.Location = New System.Drawing.Point(0, 0)
        Me.FLP_Drinks.Name = "FLP_Drinks"
        Me.FLP_Drinks.Size = New System.Drawing.Size(1272, 653)
        Me.FLP_Drinks.TabIndex = 2
        '
        'TP_Sauce
        '
        Me.TP_Sauce.Controls.Add(Me.FLP_Sauce)
        Me.TP_Sauce.Location = New System.Drawing.Point(4, 22)
        Me.TP_Sauce.Name = "TP_Sauce"
        Me.TP_Sauce.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_Sauce.Size = New System.Drawing.Size(1271, 653)
        Me.TP_Sauce.TabIndex = 4
        Me.TP_Sauce.Text = "Sauce"
        Me.TP_Sauce.UseVisualStyleBackColor = True
        '
        'FLP_Sauce
        '
        Me.FLP_Sauce.AutoScroll = True
        Me.FLP_Sauce.Location = New System.Drawing.Point(0, 0)
        Me.FLP_Sauce.Name = "FLP_Sauce"
        Me.FLP_Sauce.Size = New System.Drawing.Size(1272, 653)
        Me.FLP_Sauce.TabIndex = 2
        '
        'FLP_OrderList
        '
        Me.FLP_OrderList.AutoScroll = True
        Me.FLP_OrderList.Location = New System.Drawing.Point(1290, 24)
        Me.FLP_OrderList.Name = "FLP_OrderList"
        Me.FLP_OrderList.Size = New System.Drawing.Size(287, 589)
        Me.FLP_OrderList.TabIndex = 3
        '
        'F_Totem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1589, 693)
        Me.Controls.Add(Me.FLP_OrderList)
        Me.Controls.Add(Me.TC_Menu)
        Me.Controls.Add(Me.L_TotalPrice)
        Me.Controls.Add(Me.B_Order)
        Me.Name = "F_Totem"
        Me.Text = "Totem"
        Me.TC_Menu.ResumeLayout(False)
        Me.TP_Hamburgers.ResumeLayout(False)
        Me.TP_Delicacies.ResumeLayout(False)
        Me.TP_Dessert.ResumeLayout(False)
        Me.TP_Drinks.ResumeLayout(False)
        Me.TP_Sauce.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents B_Order As Button
    Friend WithEvents L_TotalPrice As Label
    Friend WithEvents TC_Menu As TabControl
    Friend WithEvents TP_Hamburgers As TabPage
    Friend WithEvents TP_Delicacies As TabPage
    Friend WithEvents TP_Dessert As TabPage
    Friend WithEvents TP_Drinks As TabPage
    Friend WithEvents FLP_OrderList As FlowLayoutPanel
    Friend WithEvents TP_Sauce As TabPage
    Friend WithEvents FLP_Hamburgers As FlowLayoutPanel
    Friend WithEvents FLP_Dessert As FlowLayoutPanel
    Friend WithEvents FLP_Drinks As FlowLayoutPanel
    Friend WithEvents FLP_Sauce As FlowLayoutPanel
    Friend WithEvents FLP_Appetizers As FlowLayoutPanel
End Class
