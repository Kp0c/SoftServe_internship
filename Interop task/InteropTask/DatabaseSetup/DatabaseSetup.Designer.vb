<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatabaseSetup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblDataSource = New System.Windows.Forms.Label()
        Me.txtDataSource = New System.Windows.Forms.TextBox()
        Me.lblInitialCatalog = New System.Windows.Forms.Label()
        Me.txtInitialCatalog = New System.Windows.Forms.TextBox()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.chkTrustedConnection = New System.Windows.Forms.CheckBox()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.lblConnectionTimeout = New System.Windows.Forms.Label()
        Me.nudConnectionTimeout = New System.Windows.Forms.NumericUpDown()
        CType(Me.nudConnectionTimeout, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDataSource
        '
        Me.lblDataSource.AutoSize = True
        Me.lblDataSource.Location = New System.Drawing.Point(12, 9)
        Me.lblDataSource.Name = "lblDataSource"
        Me.lblDataSource.Size = New System.Drawing.Size(68, 13)
        Me.lblDataSource.TabIndex = 0
        Me.lblDataSource.Text = "Data source:"
        '
        'txtDataSource
        '
        Me.txtDataSource.Location = New System.Drawing.Point(86, 6)
        Me.txtDataSource.Name = "txtDataSource"
        Me.txtDataSource.Size = New System.Drawing.Size(381, 20)
        Me.txtDataSource.TabIndex = 1
        '
        'lblInitialCatalog
        '
        Me.lblInitialCatalog.AutoSize = True
        Me.lblInitialCatalog.Location = New System.Drawing.Point(12, 35)
        Me.lblInitialCatalog.Name = "lblInitialCatalog"
        Me.lblInitialCatalog.Size = New System.Drawing.Size(72, 13)
        Me.lblInitialCatalog.TabIndex = 0
        Me.lblInitialCatalog.Text = "Initial catalog:"
        '
        'txtInitialCatalog
        '
        Me.txtInitialCatalog.Location = New System.Drawing.Point(90, 32)
        Me.txtInitialCatalog.Name = "txtInitialCatalog"
        Me.txtInitialCatalog.Size = New System.Drawing.Size(377, 20)
        Me.txtInitialCatalog.TabIndex = 2
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(12, 84)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(58, 13)
        Me.lblUsername.TabIndex = 0
        Me.lblUsername.Text = "Username:"
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(73, 81)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(394, 20)
        Me.txtUsername.TabIndex = 4
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(11, 110)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 0
        Me.lblPassword.Text = "Password:"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(73, 107)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(393, 20)
        Me.txtPassword.TabIndex = 5
        '
        'chkTrustedConnection
        '
        Me.chkTrustedConnection.AutoSize = True
        Me.chkTrustedConnection.Location = New System.Drawing.Point(15, 58)
        Me.chkTrustedConnection.Name = "chkTrustedConnection"
        Me.chkTrustedConnection.Size = New System.Drawing.Size(118, 17)
        Me.chkTrustedConnection.TabIndex = 3
        Me.chkTrustedConnection.Text = "Trusted connection"
        Me.chkTrustedConnection.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(392, 158)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 7
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'lblConnectionTimeout
        '
        Me.lblConnectionTimeout.AutoSize = True
        Me.lblConnectionTimeout.Location = New System.Drawing.Point(11, 135)
        Me.lblConnectionTimeout.Name = "lblConnectionTimeout"
        Me.lblConnectionTimeout.Size = New System.Drawing.Size(101, 13)
        Me.lblConnectionTimeout.TabIndex = 0
        Me.lblConnectionTimeout.Text = "Connection timeout:"
        '
        'nudConnectionTimeout
        '
        Me.nudConnectionTimeout.Location = New System.Drawing.Point(118, 133)
        Me.nudConnectionTimeout.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.nudConnectionTimeout.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudConnectionTimeout.Name = "nudConnectionTimeout"
        Me.nudConnectionTimeout.Size = New System.Drawing.Size(65, 20)
        Me.nudConnectionTimeout.TabIndex = 6
        Me.nudConnectionTimeout.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'DatabaseSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 193)
        Me.Controls.Add(Me.nudConnectionTimeout)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.chkTrustedConnection)
        Me.Controls.Add(Me.lblConnectionTimeout)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.txtInitialCatalog)
        Me.Controls.Add(Me.lblInitialCatalog)
        Me.Controls.Add(Me.txtDataSource)
        Me.Controls.Add(Me.lblDataSource)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "DatabaseSetup"
        Me.Text = "Database setup"
        CType(Me.nudConnectionTimeout, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblDataSource As Label
    Friend WithEvents txtDataSource As TextBox
    Friend WithEvents lblInitialCatalog As Label
    Friend WithEvents txtInitialCatalog As TextBox
    Friend WithEvents lblUsername As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents lblPassword As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents chkTrustedConnection As CheckBox
    Friend WithEvents btnOk As Button
    Friend WithEvents lblConnectionTimeout As Label
    Friend WithEvents nudConnectionTimeout As NumericUpDown
End Class
