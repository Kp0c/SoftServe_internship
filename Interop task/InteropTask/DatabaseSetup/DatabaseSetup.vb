Imports System.Data.SqlClient

Public Class DatabaseSetup
    Private Sub chkTrustedConnection_CheckedChanged(sender As Object, e As EventArgs) Handles chkTrustedConnection.CheckedChanged
        If chkTrustedConnection.Checked Then
            txtUsername.ReadOnly = True
            txtPassword.ReadOnly = True
            txtUsername.TabStop = False
            txtPassword.TabStop = False
        Else
            txtUsername.ReadOnly = False
            txtPassword.ReadOnly = False
            txtUsername.TabStop = True
            txtPassword.TabStop = True
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim connectionString As String = $"Data Source={txtDataSource.Text};Initial Catalog={txtDataSource.Text};"

        If chkTrustedConnection.Checked Then
            connectionString += "Trusted_Connection=yes;"
        Else
            connectionString += $"username={txtUsername.Text};password={txtPassword.Text};"
        End If

        connectionString += $"Connection Timeout={nudConnectionTimeout.Value.ToString()};"

        Dim connection As New SqlConnection(connectionString)
        Try
            connection.Open()

            SaveSetting("LastTask", "Database", "Data Source", txtDataSource.Text)
            SaveSetting("LastTask", "Database", "Initial Catalog", txtInitialCatalog.Text)
            SaveSetting("LastTask", "Database", "username", txtUsername.Text)
            SaveSetting("LastTask", "Database", "password", txtPassword.Text)
            SaveSetting("LastTask", "Database", "ConnectionTimeout", nudConnectionTimeout.Value)

            If chkTrustedConnection.Checked Then
                SaveSetting("LastTask", "Database", "Trusted_Connection", "yes")
            Else
                SaveSetting("LastTask", "Database", "Trusted_Connection", "no")
            End If

            Close()
        Catch ex As Exception
            MessageBox.Show($"Bad connection string. Error: {ex.Message}")
        End Try
    End Sub

    Private Sub DatabaseSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDataSource.Text = GetSetting("LastTask", "Database", "Data Source", "")
        txtInitialCatalog.Text = GetSetting("LastTask", "Database", "Initial Catalog", "")
        txtUsername.Text = GetSetting("LastTask", "Database", "username", "")
        txtPassword.Text = GetSetting("LastTask", "Database", "password", "")
        nudConnectionTimeout.Value = GetSetting("LastTask", "Database", "ConnectionTimeout", "1")

        Dim trustedConnection As String = GetSetting("LastTask", "Database", "Trusted_Connection", "False")
        If trustedConnection = "yes" Then
            chkTrustedConnection.Checked = True
        Else
            chkTrustedConnection.Checked = False
        End If

    End Sub
End Class
