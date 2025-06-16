<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F_BackOffice
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
        Me.B_StampReport = New System.Windows.Forms.Button()
        Me.DTP_ReportDate = New System.Windows.Forms.DateTimePicker()
        Me.P_AddProduct = New System.Windows.Forms.Panel()
        Me.CB_Category = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TB_Price = New System.Windows.Forms.TextBox()
        Me.L_PanelAddProduct = New System.Windows.Forms.Label()
        Me.L_Category = New System.Windows.Forms.Label()
        Me.B_AddNewProduct = New System.Windows.Forms.Button()
        Me.L_ImageProduct = New System.Windows.Forms.Label()
        Me.PB_Product = New System.Windows.Forms.PictureBox()
        Me.L_Desctiption = New System.Windows.Forms.Label()
        Me.TB_Description = New System.Windows.Forms.TextBox()
        Me.L_Name = New System.Windows.Forms.Label()
        Me.TB_Name = New System.Windows.Forms.TextBox()
        Me.P_Report = New System.Windows.Forms.Panel()
        Me.L_StampReport = New System.Windows.Forms.Label()
        Me.P_AddProduct.SuspendLayout()
        CType(Me.PB_Product, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.P_Report.SuspendLayout()
        Me.SuspendLayout()
        '
        'B_StampReport
        '
        Me.B_StampReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_StampReport.Location = New System.Drawing.Point(7, 70)
        Me.B_StampReport.Name = "B_StampReport"
        Me.B_StampReport.Size = New System.Drawing.Size(372, 36)
        Me.B_StampReport.TabIndex = 0
        Me.B_StampReport.Text = "Stamp Report"
        Me.B_StampReport.UseVisualStyleBackColor = True
        '
        'DTP_ReportDate
        '
        Me.DTP_ReportDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_ReportDate.Location = New System.Drawing.Point(5, 38)
        Me.DTP_ReportDate.Name = "DTP_ReportDate"
        Me.DTP_ReportDate.Size = New System.Drawing.Size(374, 29)
        Me.DTP_ReportDate.TabIndex = 1
        '
        'P_AddProduct
        '
        Me.P_AddProduct.BackColor = System.Drawing.Color.Gainsboro
        Me.P_AddProduct.Controls.Add(Me.CB_Category)
        Me.P_AddProduct.Controls.Add(Me.Label1)
        Me.P_AddProduct.Controls.Add(Me.TB_Price)
        Me.P_AddProduct.Controls.Add(Me.L_PanelAddProduct)
        Me.P_AddProduct.Controls.Add(Me.L_Category)
        Me.P_AddProduct.Controls.Add(Me.B_AddNewProduct)
        Me.P_AddProduct.Controls.Add(Me.L_ImageProduct)
        Me.P_AddProduct.Controls.Add(Me.PB_Product)
        Me.P_AddProduct.Controls.Add(Me.L_Desctiption)
        Me.P_AddProduct.Controls.Add(Me.TB_Description)
        Me.P_AddProduct.Controls.Add(Me.L_Name)
        Me.P_AddProduct.Controls.Add(Me.TB_Name)
        Me.P_AddProduct.Location = New System.Drawing.Point(12, 12)
        Me.P_AddProduct.Name = "P_AddProduct"
        Me.P_AddProduct.Size = New System.Drawing.Size(382, 332)
        Me.P_AddProduct.TabIndex = 12
        '
        'CB_Category
        '
        Me.CB_Category.FormattingEnabled = True
        Me.CB_Category.Items.AddRange(New Object() {"Hamburgers", "Appetizers", "Dessert", "Drinks", "Sauce"})
        Me.CB_Category.Location = New System.Drawing.Point(3, 107)
        Me.CB_Category.Name = "CB_Category"
        Me.CB_Category.Size = New System.Drawing.Size(176, 21)
        Me.CB_Category.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 139)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 20)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Price"
        '
        'TB_Price
        '
        Me.TB_Price.Location = New System.Drawing.Point(3, 162)
        Me.TB_Price.Name = "TB_Price"
        Me.TB_Price.Size = New System.Drawing.Size(177, 20)
        Me.TB_Price.TabIndex = 23
        '
        'L_PanelAddProduct
        '
        Me.L_PanelAddProduct.AutoSize = True
        Me.L_PanelAddProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.L_PanelAddProduct.Location = New System.Drawing.Point(1, 1)
        Me.L_PanelAddProduct.Name = "L_PanelAddProduct"
        Me.L_PanelAddProduct.Size = New System.Drawing.Size(108, 20)
        Me.L_PanelAddProduct.TabIndex = 22
        Me.L_PanelAddProduct.Text = "Add Product"
        '
        'L_Category
        '
        Me.L_Category.AutoSize = True
        Me.L_Category.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.L_Category.Location = New System.Drawing.Point(3, 84)
        Me.L_Category.Name = "L_Category"
        Me.L_Category.Size = New System.Drawing.Size(73, 20)
        Me.L_Category.TabIndex = 21
        Me.L_Category.Text = "Category"
        '
        'B_AddNewProduct
        '
        Me.B_AddNewProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_AddNewProduct.Location = New System.Drawing.Point(23, 266)
        Me.B_AddNewProduct.Name = "B_AddNewProduct"
        Me.B_AddNewProduct.Size = New System.Drawing.Size(332, 50)
        Me.B_AddNewProduct.TabIndex = 13
        Me.B_AddNewProduct.Text = "Add New Product"
        Me.B_AddNewProduct.UseVisualStyleBackColor = True
        '
        'L_ImageProduct
        '
        Me.L_ImageProduct.AutoSize = True
        Me.L_ImageProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.L_ImageProduct.Location = New System.Drawing.Point(186, 38)
        Me.L_ImageProduct.Name = "L_ImageProduct"
        Me.L_ImageProduct.Size = New System.Drawing.Size(54, 20)
        Me.L_ImageProduct.TabIndex = 19
        Me.L_ImageProduct.Text = "Image"
        '
        'PB_Product
        '
        Me.PB_Product.BackColor = System.Drawing.Color.Gray
        Me.PB_Product.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PB_Product.Location = New System.Drawing.Point(186, 61)
        Me.PB_Product.Name = "PB_Product"
        Me.PB_Product.Size = New System.Drawing.Size(193, 184)
        Me.PB_Product.TabIndex = 14
        Me.PB_Product.TabStop = False
        '
        'L_Desctiption
        '
        Me.L_Desctiption.AutoSize = True
        Me.L_Desctiption.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.L_Desctiption.Location = New System.Drawing.Point(4, 185)
        Me.L_Desctiption.Name = "L_Desctiption"
        Me.L_Desctiption.Size = New System.Drawing.Size(89, 20)
        Me.L_Desctiption.TabIndex = 18
        Me.L_Desctiption.Text = "Description"
        '
        'TB_Description
        '
        Me.TB_Description.Location = New System.Drawing.Point(4, 208)
        Me.TB_Description.Multiline = True
        Me.TB_Description.Name = "TB_Description"
        Me.TB_Description.Size = New System.Drawing.Size(176, 37)
        Me.TB_Description.TabIndex = 15
        '
        'L_Name
        '
        Me.L_Name.AutoSize = True
        Me.L_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.L_Name.Location = New System.Drawing.Point(3, 38)
        Me.L_Name.Name = "L_Name"
        Me.L_Name.Size = New System.Drawing.Size(51, 20)
        Me.L_Name.TabIndex = 17
        Me.L_Name.Text = "Name"
        '
        'TB_Name
        '
        Me.TB_Name.Location = New System.Drawing.Point(3, 61)
        Me.TB_Name.Name = "TB_Name"
        Me.TB_Name.Size = New System.Drawing.Size(176, 20)
        Me.TB_Name.TabIndex = 16
        '
        'P_Report
        '
        Me.P_Report.BackColor = System.Drawing.Color.Gainsboro
        Me.P_Report.Controls.Add(Me.L_StampReport)
        Me.P_Report.Controls.Add(Me.DTP_ReportDate)
        Me.P_Report.Controls.Add(Me.B_StampReport)
        Me.P_Report.Location = New System.Drawing.Point(12, 357)
        Me.P_Report.Name = "P_Report"
        Me.P_Report.Size = New System.Drawing.Size(382, 110)
        Me.P_Report.TabIndex = 13
        '
        'L_StampReport
        '
        Me.L_StampReport.AutoSize = True
        Me.L_StampReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.L_StampReport.Location = New System.Drawing.Point(4, 4)
        Me.L_StampReport.Name = "L_StampReport"
        Me.L_StampReport.Size = New System.Drawing.Size(72, 24)
        Me.L_StampReport.TabIndex = 2
        Me.L_StampReport.Text = "Report"
        '
        'F_BackOffice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(406, 479)
        Me.Controls.Add(Me.P_Report)
        Me.Controls.Add(Me.P_AddProduct)
        Me.Name = "F_BackOffice"
        Me.Text = "BackOffice"
        Me.P_AddProduct.ResumeLayout(False)
        Me.P_AddProduct.PerformLayout()
        CType(Me.PB_Product, System.ComponentModel.ISupportInitialize).EndInit()
        Me.P_Report.ResumeLayout(False)
        Me.P_Report.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents B_StampReport As Button
    Friend WithEvents DTP_ReportDate As DateTimePicker
    Friend WithEvents P_AddProduct As Panel
    Friend WithEvents L_Category As Label
    Friend WithEvents B_AddNewProduct As Button
    Friend WithEvents L_ImageProduct As Label
    Friend WithEvents PB_Product As PictureBox
    Friend WithEvents L_Desctiption As Label
    Friend WithEvents TB_Description As TextBox
    Friend WithEvents L_Name As Label
    Friend WithEvents TB_Name As TextBox
    Friend WithEvents L_PanelAddProduct As Label
    Friend WithEvents P_Report As Panel
    Friend WithEvents L_StampReport As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TB_Price As TextBox
    Friend WithEvents CB_Category As ComboBox
End Class
