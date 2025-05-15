<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Es3
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
        Me.TC_TablesName = New System.Windows.Forms.TabControl()
        Me.ConnectionStringBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BT_Create = New System.Windows.Forms.Button()
        Me.BT_Read = New System.Windows.Forms.Button()
        Me.BT_Update = New System.Windows.Forms.Button()
        Me.BT_Delete = New System.Windows.Forms.Button()
        CType(Me.ConnectionStringBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TC_TablesName
        '
        Me.TC_TablesName.Location = New System.Drawing.Point(0, 0)
        Me.TC_TablesName.Name = "TC_TablesName"
        Me.TC_TablesName.SelectedIndex = 0
        Me.TC_TablesName.Size = New System.Drawing.Size(1373, 491)
        Me.TC_TablesName.TabIndex = 1
        '
        'ConnectionStringBindingSource
        '
        Me.ConnectionStringBindingSource.DataSource = GetType(ClassTools.ConnectionString)
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
        Me.BT_Read.Location = New System.Drawing.Point(435, 497)
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
        'F_Es3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1372, 596)
        Me.Controls.Add(Me.BT_Delete)
        Me.Controls.Add(Me.BT_Update)
        Me.Controls.Add(Me.BT_Read)
        Me.Controls.Add(Me.BT_Create)
        Me.Controls.Add(Me.TC_TablesName)
        Me.Name = "F_Es3"
        Me.Text = "Form2"
        CType(Me.ConnectionStringBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TC_TablesName As TabControl
    Friend WithEvents BT_Create As Button
    Friend WithEvents BT_Read As Button
    Friend WithEvents BT_Update As Button
    Friend WithEvents BT_Delete As Button
    Friend WithEvents ConnectionStringBindingSource As BindingSource
End Class
