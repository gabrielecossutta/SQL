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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.P_AddProduct = New System.Windows.Forms.Panel()
        Me.L_PanelAddProduct = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.L_ImageProduct = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.L_StampReport = New System.Windows.Forms.Label()
        Me.P_AddProduct.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
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
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'P_AddProduct
        '
        Me.P_AddProduct.BackColor = System.Drawing.Color.Gainsboro
        Me.P_AddProduct.Controls.Add(Me.L_PanelAddProduct)
        Me.P_AddProduct.Controls.Add(Me.Label1)
        Me.P_AddProduct.Controls.Add(Me.TextBox1)
        Me.P_AddProduct.Controls.Add(Me.Button1)
        Me.P_AddProduct.Controls.Add(Me.L_ImageProduct)
        Me.P_AddProduct.Controls.Add(Me.PictureBox1)
        Me.P_AddProduct.Controls.Add(Me.Label3)
        Me.P_AddProduct.Controls.Add(Me.TextBox3)
        Me.P_AddProduct.Controls.Add(Me.Label4)
        Me.P_AddProduct.Controls.Add(Me.TextBox2)
        Me.P_AddProduct.Location = New System.Drawing.Point(12, 12)
        Me.P_AddProduct.Name = "P_AddProduct"
        Me.P_AddProduct.Size = New System.Drawing.Size(382, 332)
        Me.P_AddProduct.TabIndex = 12
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 20)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Price"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(3, 107)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(177, 20)
        Me.TextBox1.TabIndex = 20
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(23, 266)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(332, 50)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Add New Product"
        Me.Button1.UseVisualStyleBackColor = True
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
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Gray
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(186, 61)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(193, 184)
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 130)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 20)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Description"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(4, 153)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(176, 92)
        Me.TextBox3.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 20)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Name"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(3, 61)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(176, 20)
        Me.TextBox2.TabIndex = 16
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.Controls.Add(Me.L_StampReport)
        Me.Panel1.Controls.Add(Me.DTP_ReportDate)
        Me.Panel1.Controls.Add(Me.B_StampReport)
        Me.Panel1.Location = New System.Drawing.Point(12, 357)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(382, 110)
        Me.Panel1.TabIndex = 13
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
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.P_AddProduct)
        Me.Name = "F_BackOffice"
        Me.Text = "BackOffice"
        Me.P_AddProduct.ResumeLayout(False)
        Me.P_AddProduct.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents B_StampReport As Button
    Friend WithEvents DTP_ReportDate As DateTimePicker
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents P_AddProduct As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents L_ImageProduct As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents L_PanelAddProduct As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents L_StampReport As Label
End Class
