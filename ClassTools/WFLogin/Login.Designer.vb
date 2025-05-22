<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        Me.B_Load = New System.Windows.Forms.Button()
        Me.B_Save = New System.Windows.Forms.Button()
        Me.B_Login = New System.Windows.Forms.Button()
        Me.CB_Username = New System.Windows.Forms.ComboBox()
        Me.ConnectionStringBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TB_Protocol = New System.Windows.Forms.TextBox()
        Me.TB_ServicePort = New System.Windows.Forms.TextBox()
        Me.TB_ServerName = New System.Windows.Forms.TextBox()
        Me.TB_Password = New System.Windows.Forms.TextBox()
        Me.TB_DatabaseName = New System.Windows.Forms.TextBox()
        Me.L_Username = New System.Windows.Forms.Label()
        Me.L_Password = New System.Windows.Forms.Label()
        Me.L_SQLServerName = New System.Windows.Forms.Label()
        Me.L_DatabaseName = New System.Windows.Forms.Label()
        Me.L_ServicePort = New System.Windows.Forms.Label()
        Me.L_ProtocolName = New System.Windows.Forms.Label()
        CType(Me.ConnectionStringBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'B_Load
        '
        Me.B_Load.Location = New System.Drawing.Point(361, 73)
        Me.B_Load.Name = "B_Load"
        Me.B_Load.Size = New System.Drawing.Size(149, 23)
        Me.B_Load.TabIndex = 0
        Me.B_Load.Text = "Load"
        Me.B_Load.UseVisualStyleBackColor = True
        '
        'B_Save
        '
        Me.B_Save.Location = New System.Drawing.Point(516, 73)
        Me.B_Save.Name = "B_Save"
        Me.B_Save.Size = New System.Drawing.Size(149, 23)
        Me.B_Save.TabIndex = 1
        Me.B_Save.Text = "Save"
        Me.B_Save.UseVisualStyleBackColor = True
        '
        'B_Login
        '
        Me.B_Login.Location = New System.Drawing.Point(318, 118)
        Me.B_Login.Name = "B_Login"
        Me.B_Login.Size = New System.Drawing.Size(75, 23)
        Me.B_Login.TabIndex = 2
        Me.B_Login.Text = "Login"
        Me.B_Login.UseVisualStyleBackColor = True
        '
        'CB_Username
        '
        Me.CB_Username.DataSource = Me.ConnectionStringBindingSource
        Me.CB_Username.DisplayMember = "UserName"
        Me.CB_Username.FormattingEnabled = True
        Me.CB_Username.Location = New System.Drawing.Point(31, 32)
        Me.CB_Username.Name = "CB_Username"
        Me.CB_Username.Size = New System.Drawing.Size(149, 21)
        Me.CB_Username.TabIndex = 3
        Me.CB_Username.ValueMember = "UserName"
        '
        'ConnectionStringBindingSource
        '
        Me.ConnectionStringBindingSource.DataSource = GetType(ClassTools.ConnectionString)
        '
        'TB_Protocol
        '
        Me.TB_Protocol.Location = New System.Drawing.Point(196, 76)
        Me.TB_Protocol.Name = "TB_Protocol"
        Me.TB_Protocol.Size = New System.Drawing.Size(149, 20)
        Me.TB_Protocol.TabIndex = 4
        '
        'TB_ServicePort
        '
        Me.TB_ServicePort.Location = New System.Drawing.Point(31, 76)
        Me.TB_ServicePort.Name = "TB_ServicePort"
        Me.TB_ServicePort.Size = New System.Drawing.Size(149, 20)
        Me.TB_ServicePort.TabIndex = 7
        '
        'TB_ServerName
        '
        Me.TB_ServerName.Location = New System.Drawing.Point(361, 32)
        Me.TB_ServerName.Name = "TB_ServerName"
        Me.TB_ServerName.Size = New System.Drawing.Size(149, 20)
        Me.TB_ServerName.TabIndex = 8
        '
        'TB_Password
        '
        Me.TB_Password.Location = New System.Drawing.Point(196, 32)
        Me.TB_Password.Name = "TB_Password"
        Me.TB_Password.Size = New System.Drawing.Size(149, 20)
        Me.TB_Password.TabIndex = 10
        '
        'TB_DatabaseName
        '
        Me.TB_DatabaseName.Location = New System.Drawing.Point(516, 32)
        Me.TB_DatabaseName.Name = "TB_DatabaseName"
        Me.TB_DatabaseName.Size = New System.Drawing.Size(149, 20)
        Me.TB_DatabaseName.TabIndex = 9
        '
        'L_Username
        '
        Me.L_Username.AutoSize = True
        Me.L_Username.Location = New System.Drawing.Point(74, 16)
        Me.L_Username.Name = "L_Username"
        Me.L_Username.Size = New System.Drawing.Size(55, 13)
        Me.L_Username.TabIndex = 11
        Me.L_Username.Text = "Username"
        '
        'L_Password
        '
        Me.L_Password.AutoSize = True
        Me.L_Password.Location = New System.Drawing.Point(234, 13)
        Me.L_Password.Name = "L_Password"
        Me.L_Password.Size = New System.Drawing.Size(53, 13)
        Me.L_Password.TabIndex = 12
        Me.L_Password.Text = "Password"
        '
        'L_SQLServerName
        '
        Me.L_SQLServerName.AutoSize = True
        Me.L_SQLServerName.Location = New System.Drawing.Point(405, 16)
        Me.L_SQLServerName.Name = "L_SQLServerName"
        Me.L_SQLServerName.Size = New System.Drawing.Size(87, 13)
        Me.L_SQLServerName.TabIndex = 13
        Me.L_SQLServerName.Text = "SQLServerName"
        '
        'L_DatabaseName
        '
        Me.L_DatabaseName.AutoSize = True
        Me.L_DatabaseName.Location = New System.Drawing.Point(559, 13)
        Me.L_DatabaseName.Name = "L_DatabaseName"
        Me.L_DatabaseName.Size = New System.Drawing.Size(81, 13)
        Me.L_DatabaseName.TabIndex = 14
        Me.L_DatabaseName.Text = "DatabaseName"
        '
        'L_ServicePort
        '
        Me.L_ServicePort.AutoSize = True
        Me.L_ServicePort.Location = New System.Drawing.Point(76, 60)
        Me.L_ServicePort.Name = "L_ServicePort"
        Me.L_ServicePort.Size = New System.Drawing.Size(62, 13)
        Me.L_ServicePort.TabIndex = 15
        Me.L_ServicePort.Text = "ServicePort"
        '
        'L_ProtocolName
        '
        Me.L_ProtocolName.AutoSize = True
        Me.L_ProtocolName.Location = New System.Drawing.Point(234, 60)
        Me.L_ProtocolName.Name = "L_ProtocolName"
        Me.L_ProtocolName.Size = New System.Drawing.Size(74, 13)
        Me.L_ProtocolName.TabIndex = 16
        Me.L_ProtocolName.Text = "ProtocolName"
        '
        'F_Es2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 156)
        Me.Controls.Add(Me.L_ProtocolName)
        Me.Controls.Add(Me.L_ServicePort)
        Me.Controls.Add(Me.L_DatabaseName)
        Me.Controls.Add(Me.L_SQLServerName)
        Me.Controls.Add(Me.L_Password)
        Me.Controls.Add(Me.L_Username)
        Me.Controls.Add(Me.TB_Password)
        Me.Controls.Add(Me.TB_DatabaseName)
        Me.Controls.Add(Me.TB_ServerName)
        Me.Controls.Add(Me.TB_ServicePort)
        Me.Controls.Add(Me.TB_Protocol)
        Me.Controls.Add(Me.CB_Username)
        Me.Controls.Add(Me.B_Login)
        Me.Controls.Add(Me.B_Save)
        Me.Controls.Add(Me.B_Load)
        Me.Name = "F_Es2"
        Me.Text = "Form1"
        CType(Me.ConnectionStringBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents B_Load As Button
    Friend WithEvents B_Save As Button
    Friend WithEvents B_Login As Button
    Friend WithEvents CB_Username As ComboBox
    Friend WithEvents TB_Protocol As TextBox
    Friend WithEvents TB_ServicePort As TextBox
    Friend WithEvents TB_ServerName As TextBox
    Friend WithEvents TB_Password As TextBox
    Friend WithEvents TB_DatabaseName As TextBox
    Friend WithEvents L_Username As Label
    Friend WithEvents L_Password As Label
    Friend WithEvents L_SQLServerName As Label
    Friend WithEvents L_DatabaseName As Label
    Friend WithEvents L_ServicePort As Label
    Friend WithEvents L_ProtocolName As Label
    Friend WithEvents ConnectionStringBindingSource As BindingSource
End Class
