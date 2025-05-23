<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Database_Entity
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
        Me.components = New System.ComponentModel.Container()
        Me.BT_Create = New System.Windows.Forms.Button()
        Me.BT_Read = New System.Windows.Forms.Button()
        Me.BT_Update = New System.Windows.Forms.Button()
        Me.BT_Delete = New System.Windows.Forms.Button()
        Me.ConnectionStringBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CustomerDataGrid = New System.Windows.Forms.DataGridView()
        Me.CustomerID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CompanyName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContactName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContactTitle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Address = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.City = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Region = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PostalCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Country = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Phone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fax = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.ConnectionStringBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomerDataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BT_Create
        '
        Me.BT_Create.Font = New System.Drawing.Font("Segoe UI", 20.25!)
        Me.BT_Create.Location = New System.Drawing.Point(152, 497)
        Me.BT_Create.Name = "BT_Create"
        Me.BT_Create.Size = New System.Drawing.Size(129, 87)
        Me.BT_Create.TabIndex = 2
        Me.BT_Create.Text = "Create"
        Me.BT_Create.UseVisualStyleBackColor = True
        '
        'BT_Read
        '
        Me.BT_Read.Font = New System.Drawing.Font("Segoe UI", 20.25!)
        Me.BT_Read.Location = New System.Drawing.Point(422, 497)
        Me.BT_Read.Name = "BT_Read"
        Me.BT_Read.Size = New System.Drawing.Size(129, 87)
        Me.BT_Read.TabIndex = 3
        Me.BT_Read.Text = "Read"
        Me.BT_Read.UseVisualStyleBackColor = True
        '
        'BT_Update
        '
        Me.BT_Update.Font = New System.Drawing.Font("Segoe UI", 20.25!)
        Me.BT_Update.Location = New System.Drawing.Point(717, 497)
        Me.BT_Update.Name = "BT_Update"
        Me.BT_Update.Size = New System.Drawing.Size(129, 87)
        Me.BT_Update.TabIndex = 4
        Me.BT_Update.Text = "Update"
        Me.BT_Update.UseVisualStyleBackColor = True
        '
        'BT_Delete
        '
        Me.BT_Delete.Font = New System.Drawing.Font("Segoe UI", 20.25!)
        Me.BT_Delete.Location = New System.Drawing.Point(1000, 497)
        Me.BT_Delete.Name = "BT_Delete"
        Me.BT_Delete.Size = New System.Drawing.Size(129, 87)
        Me.BT_Delete.TabIndex = 5
        Me.BT_Delete.Text = "Delete"
        Me.BT_Delete.UseVisualStyleBackColor = True
        '
        'ConnectionStringBindingSource
        '
        Me.ConnectionStringBindingSource.DataSource = GetType(ClassTools.ApplicationConfig.ConnectionString)
        '
        'CustomerDataGrid
        '
        Me.CustomerDataGrid.AllowUserToAddRows = False
        Me.CustomerDataGrid.AllowUserToDeleteRows = False
        Me.CustomerDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CustomerDataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CustomerID, Me.CompanyName, Me.ContactName, Me.ContactTitle, Me.Address, Me.City, Me.Region, Me.PostalCode, Me.Country, Me.Phone, Me.Fax})
        Me.CustomerDataGrid.Location = New System.Drawing.Point(12, 12)
        Me.CustomerDataGrid.Name = "CustomerDataGrid"
        Me.CustomerDataGrid.ReadOnly = True
        Me.CustomerDataGrid.Size = New System.Drawing.Size(1348, 479)
        Me.CustomerDataGrid.TabIndex = 7
        '
        'CustomerID
        '
        Me.CustomerID.DataPropertyName = "SQLServerName"
        Me.CustomerID.HeaderText = "CustomerID"
        Me.CustomerID.Name = "CustomerID"
        Me.CustomerID.ReadOnly = True
        '
        'CompanyName
        '
        Me.CompanyName.HeaderText = "CompanyName"
        Me.CompanyName.Name = "CompanyName"
        Me.CompanyName.ReadOnly = True
        '
        'ContactName
        '
        Me.ContactName.HeaderText = "ContactName"
        Me.ContactName.Name = "ContactName"
        Me.ContactName.ReadOnly = True
        '
        'ContactTitle
        '
        Me.ContactTitle.HeaderText = "ContactTitle"
        Me.ContactTitle.Name = "ContactTitle"
        Me.ContactTitle.ReadOnly = True
        '
        'Address
        '
        Me.Address.HeaderText = "Address"
        Me.Address.Name = "Address"
        Me.Address.ReadOnly = True
        '
        'City
        '
        Me.City.HeaderText = "City"
        Me.City.Name = "City"
        Me.City.ReadOnly = True
        '
        'Region
        '
        Me.Region.HeaderText = "Region"
        Me.Region.Name = "Region"
        Me.Region.ReadOnly = True
        '
        'PostalCode
        '
        Me.PostalCode.HeaderText = "PostalCode"
        Me.PostalCode.Name = "PostalCode"
        Me.PostalCode.ReadOnly = True
        '
        'Country
        '
        Me.Country.HeaderText = "Country"
        Me.Country.Name = "Country"
        Me.Country.ReadOnly = True
        '
        'Phone
        '
        Me.Phone.HeaderText = "Phone"
        Me.Phone.Name = "Phone"
        Me.Phone.ReadOnly = True
        '
        'Fax
        '
        Me.Fax.HeaderText = "Fax"
        Me.Fax.Name = "Fax"
        Me.Fax.ReadOnly = True
        '
        'Database_Entity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1372, 596)
        Me.Controls.Add(Me.CustomerDataGrid)
        Me.Controls.Add(Me.BT_Delete)
        Me.Controls.Add(Me.BT_Update)
        Me.Controls.Add(Me.BT_Read)
        Me.Controls.Add(Me.BT_Create)
        Me.Name = "Database_Entity"
        Me.Text = "Form2"
        CType(Me.ConnectionStringBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomerDataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BT_Create As Button
    Friend WithEvents BT_Read As Button
    Friend WithEvents BT_Update As Button
    Friend WithEvents BT_Delete As Button
    Friend WithEvents ConnectionStringBindingSource As BindingSource
    Friend WithEvents CustomerDataGrid As DataGridView
    Friend WithEvents CustomerID As DataGridViewTextBoxColumn
    Friend WithEvents CompanyName As DataGridViewTextBoxColumn
    Friend WithEvents ContactName As DataGridViewTextBoxColumn
    Friend WithEvents ContactTitle As DataGridViewTextBoxColumn
    Friend WithEvents Address As DataGridViewTextBoxColumn
    Friend WithEvents City As DataGridViewTextBoxColumn
    Friend WithEvents Region As DataGridViewTextBoxColumn
    Friend WithEvents PostalCode As DataGridViewTextBoxColumn
    Friend WithEvents Country As DataGridViewTextBoxColumn
    Friend WithEvents Phone As DataGridViewTextBoxColumn
    Friend WithEvents Fax As DataGridViewTextBoxColumn
End Class
