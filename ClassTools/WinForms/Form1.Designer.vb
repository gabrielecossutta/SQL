<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        ComboBox1 = New ComboBox()
        ConnectionStringBindingSource = New BindingSource(components)
        Button1 = New Button()
        TextBox2 = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Button2 = New Button()
        Label3 = New Label()
        TextBox3 = New TextBox()
        Label4 = New Label()
        TextBox4 = New TextBox()
        Label5 = New Label()
        TextBox5 = New TextBox()
        Label6 = New Label()
        TextBox6 = New TextBox()
        CType(ConnectionStringBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DataSource = ConnectionStringBindingSource
        ComboBox1.DisplayMember = "UserName"
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(12, 44)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(126, 23)
        ComboBox1.TabIndex = 16
        ComboBox1.Tag = ""
        ComboBox1.ValueMember = "UserName"
        ' 
        ' ConnectionStringBindingSource
        ' 
        ConnectionStringBindingSource.DataSource = GetType(ClassTools.ConnectionString)
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(408, 95)
        Button1.Name = "Button1"
        Button1.Size = New Size(123, 23)
        Button1.TabIndex = 0
        Button1.Text = "Save"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(12, 97)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(126, 23)
        TextBox2.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(39, 26)
        Label1.Name = "Label1"
        Label1.Size = New Size(62, 15)
        Label1.TabIndex = 3
        Label1.Text = "UserName"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(39, 78)
        Label2.Name = "Label2"
        Label2.Size = New Size(66, 15)
        Label2.TabIndex = 4
        Label2.Text = "ServicePort"
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(276, 95)
        Button2.Name = "Button2"
        Button2.Size = New Size(126, 23)
        Button2.TabIndex = 5
        Button2.Text = "Load"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(157, 78)
        Label3.Name = "Label3"
        Label3.Size = New Size(84, 15)
        Label3.TabIndex = 7
        Label3.Text = "ProtocolName"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(144, 96)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(126, 23)
        TextBox3.TabIndex = 6
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(174, 26)
        Label4.Name = "Label4"
        Label4.Size = New Size(57, 15)
        Label4.TabIndex = 11
        Label4.Text = "Password"
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(145, 44)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(125, 23)
        TextBox4.TabIndex = 10
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(426, 26)
        Label5.Name = "Label5"
        Label5.Size = New Size(87, 15)
        Label5.TabIndex = 15
        Label5.Text = "DatabaseName"
        ' 
        ' TextBox5
        ' 
        TextBox5.Location = New Point(408, 44)
        TextBox5.Name = "TextBox5"
        TextBox5.Size = New Size(126, 23)
        TextBox5.TabIndex = 14
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(291, 26)
        Label6.Name = "Label6"
        Label6.Size = New Size(92, 15)
        Label6.TabIndex = 13
        Label6.Text = "SQLServerName"
        ' 
        ' TextBox6
        ' 
        TextBox6.Location = New Point(276, 44)
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(123, 23)
        TextBox6.TabIndex = 12
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(550, 153)
        Controls.Add(ComboBox1)
        Controls.Add(Label5)
        Controls.Add(TextBox5)
        Controls.Add(Label6)
        Controls.Add(TextBox6)
        Controls.Add(Label4)
        Controls.Add(TextBox4)
        Controls.Add(Label3)
        Controls.Add(TextBox3)
        Controls.Add(Button2)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(TextBox2)
        Controls.Add(Button1)
        Name = "Form1"
        Text = "Form1"
        CType(ConnectionStringBindingSource, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub



    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ConnectionStringBindingSource As BindingSource

End Class
