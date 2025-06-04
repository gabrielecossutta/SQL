<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Database_Entity
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
        Me.components = New System.ComponentModel.Container()
        Me.BT_Create = New System.Windows.Forms.Button()
        Me.BT_Read = New System.Windows.Forms.Button()
        Me.BT_Update = New System.Windows.Forms.Button()
        Me.BT_Delete = New System.Windows.Forms.Button()
        Me.ConnectionStringBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.CustomerDataGrid = New System.Windows.Forms.DataGridView()
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
        Me.CustomerDataGrid.Location = New System.Drawing.Point(12, 12)
        Me.CustomerDataGrid.Name = "CustomerDataGrid"
        Me.CustomerDataGrid.ReadOnly = True
        Me.CustomerDataGrid.Size = New System.Drawing.Size(1348, 479)
        Me.CustomerDataGrid.TabIndex = 7
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
End Class
