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
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.P_AppetizersProducts = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.TP_Dessert = New System.Windows.Forms.TabPage()
        Me.FLP_Dessert = New System.Windows.Forms.FlowLayoutPanel()
        Me.P_DessertsProducts = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.TP_Drinks = New System.Windows.Forms.TabPage()
        Me.FLP_Drinks = New System.Windows.Forms.FlowLayoutPanel()
        Me.P_DrinksProducts = New System.Windows.Forms.Panel()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.TP_Sauce = New System.Windows.Forms.TabPage()
        Me.FLP_Sauce = New System.Windows.Forms.FlowLayoutPanel()
        Me.P_SaucesProducts = New System.Windows.Forms.Panel()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.FLP_OrderList = New System.Windows.Forms.FlowLayoutPanel()
        Me.TC_Menu.SuspendLayout()
        Me.TP_Hamburgers.SuspendLayout()
        Me.TP_Delicacies.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.P_AppetizersProducts.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Dessert.SuspendLayout()
        Me.FLP_Dessert.SuspendLayout()
        Me.P_DessertsProducts.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Drinks.SuspendLayout()
        Me.FLP_Drinks.SuspendLayout()
        Me.P_DrinksProducts.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP_Sauce.SuspendLayout()
        Me.FLP_Sauce.SuspendLayout()
        Me.P_SaucesProducts.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TP_Delicacies.Controls.Add(Me.FlowLayoutPanel1)
        Me.TP_Delicacies.Location = New System.Drawing.Point(4, 22)
        Me.TP_Delicacies.Name = "TP_Delicacies"
        Me.TP_Delicacies.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_Delicacies.Size = New System.Drawing.Size(1271, 653)
        Me.TP_Delicacies.TabIndex = 1
        Me.TP_Delicacies.Text = "Appetizers "
        Me.TP_Delicacies.UseVisualStyleBackColor = True
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.Controls.Add(Me.P_AppetizersProducts)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1272, 652)
        Me.FlowLayoutPanel1.TabIndex = 1
        '
        'P_AppetizersProducts
        '
        Me.P_AppetizersProducts.BackColor = System.Drawing.Color.Gainsboro
        Me.P_AppetizersProducts.Controls.Add(Me.PictureBox1)
        Me.P_AppetizersProducts.Controls.Add(Me.Label34)
        Me.P_AppetizersProducts.Controls.Add(Me.Label35)
        Me.P_AppetizersProducts.Location = New System.Drawing.Point(3, 3)
        Me.P_AppetizersProducts.Name = "P_AppetizersProducts"
        Me.P_AppetizersProducts.Size = New System.Drawing.Size(202, 200)
        Me.P_AppetizersProducts.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(200, 164)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(106, 176)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(61, 13)
        Me.Label34.TabIndex = 2
        Me.Label34.Text = "Price:0,00€"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(22, 176)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(59, 13)
        Me.Label35.TabIndex = 1
        Me.Label35.Text = "Hamburger"
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
        Me.FLP_Dessert.Controls.Add(Me.P_DessertsProducts)
        Me.FLP_Dessert.Location = New System.Drawing.Point(0, 0)
        Me.FLP_Dessert.Name = "FLP_Dessert"
        Me.FLP_Dessert.Size = New System.Drawing.Size(1272, 653)
        Me.FLP_Dessert.TabIndex = 1
        '
        'P_DessertsProducts
        '
        Me.P_DessertsProducts.BackColor = System.Drawing.Color.Gainsboro
        Me.P_DessertsProducts.Controls.Add(Me.PictureBox2)
        Me.P_DessertsProducts.Controls.Add(Me.Label36)
        Me.P_DessertsProducts.Controls.Add(Me.Label37)
        Me.P_DessertsProducts.Location = New System.Drawing.Point(3, 3)
        Me.P_DessertsProducts.Name = "P_DessertsProducts"
        Me.P_DessertsProducts.Size = New System.Drawing.Size(202, 200)
        Me.P_DessertsProducts.TabIndex = 1
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(200, 164)
        Me.PictureBox2.TabIndex = 3
        Me.PictureBox2.TabStop = False
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(106, 176)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(61, 13)
        Me.Label36.TabIndex = 2
        Me.Label36.Text = "Price:0,00€"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(22, 176)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(59, 13)
        Me.Label37.TabIndex = 1
        Me.Label37.Text = "Hamburger"
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
        Me.FLP_Drinks.Controls.Add(Me.P_DrinksProducts)
        Me.FLP_Drinks.Location = New System.Drawing.Point(0, 0)
        Me.FLP_Drinks.Name = "FLP_Drinks"
        Me.FLP_Drinks.Size = New System.Drawing.Size(1272, 653)
        Me.FLP_Drinks.TabIndex = 2
        '
        'P_DrinksProducts
        '
        Me.P_DrinksProducts.BackColor = System.Drawing.Color.Gainsboro
        Me.P_DrinksProducts.Controls.Add(Me.PictureBox3)
        Me.P_DrinksProducts.Controls.Add(Me.Label38)
        Me.P_DrinksProducts.Controls.Add(Me.Label39)
        Me.P_DrinksProducts.Location = New System.Drawing.Point(3, 3)
        Me.P_DrinksProducts.Name = "P_DrinksProducts"
        Me.P_DrinksProducts.Size = New System.Drawing.Size(202, 200)
        Me.P_DrinksProducts.TabIndex = 3
        '
        'PictureBox3
        '
        Me.PictureBox3.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(200, 164)
        Me.PictureBox3.TabIndex = 3
        Me.PictureBox3.TabStop = False
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(106, 176)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(61, 13)
        Me.Label38.TabIndex = 2
        Me.Label38.Text = "Price:0,00€"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(22, 176)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(59, 13)
        Me.Label39.TabIndex = 1
        Me.Label39.Text = "Hamburger"
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
        Me.FLP_Sauce.Controls.Add(Me.P_SaucesProducts)
        Me.FLP_Sauce.Location = New System.Drawing.Point(0, 0)
        Me.FLP_Sauce.Name = "FLP_Sauce"
        Me.FLP_Sauce.Size = New System.Drawing.Size(1272, 653)
        Me.FLP_Sauce.TabIndex = 2
        '
        'P_SaucesProducts
        '
        Me.P_SaucesProducts.BackColor = System.Drawing.Color.Gainsboro
        Me.P_SaucesProducts.Controls.Add(Me.PictureBox4)
        Me.P_SaucesProducts.Controls.Add(Me.Label40)
        Me.P_SaucesProducts.Controls.Add(Me.Label41)
        Me.P_SaucesProducts.Location = New System.Drawing.Point(3, 3)
        Me.P_SaucesProducts.Name = "P_SaucesProducts"
        Me.P_SaucesProducts.Size = New System.Drawing.Size(202, 200)
        Me.P_SaucesProducts.TabIndex = 1
        '
        'PictureBox4
        '
        Me.PictureBox4.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(200, 164)
        Me.PictureBox4.TabIndex = 3
        Me.PictureBox4.TabStop = False
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(106, 176)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(61, 13)
        Me.Label40.TabIndex = 2
        Me.Label40.Text = "Price:0,00€"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(22, 176)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(59, 13)
        Me.Label41.TabIndex = 1
        Me.Label41.Text = "Hamburger"
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
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.P_AppetizersProducts.ResumeLayout(False)
        Me.P_AppetizersProducts.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TP_Dessert.ResumeLayout(False)
        Me.FLP_Dessert.ResumeLayout(False)
        Me.P_DessertsProducts.ResumeLayout(False)
        Me.P_DessertsProducts.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TP_Drinks.ResumeLayout(False)
        Me.FLP_Drinks.ResumeLayout(False)
        Me.P_DrinksProducts.ResumeLayout(False)
        Me.P_DrinksProducts.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TP_Sauce.ResumeLayout(False)
        Me.FLP_Sauce.ResumeLayout(False)
        Me.P_SaucesProducts.ResumeLayout(False)
        Me.P_SaucesProducts.PerformLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents P_AppetizersProducts As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label34 As Label
    Friend WithEvents Label35 As Label
    Friend WithEvents P_DessertsProducts As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label36 As Label
    Friend WithEvents Label37 As Label
    Friend WithEvents P_DrinksProducts As Panel
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label38 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents P_SaucesProducts As Panel
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents Label40 As Label
    Friend WithEvents Label41 As Label
End Class
