VERSION 5.00
Begin VB.Form frmLogin 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Login"
   ClientHeight    =   2175
   ClientLeft      =   2835
   ClientTop       =   3480
   ClientWidth     =   3975
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1285.062
   ScaleMode       =   0  'User
   ScaleWidth      =   3732.31
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton cmdShowPassword 
      Appearance      =   0  'Flat
      BackColor       =   &H80000014&
      Height          =   375
      Left            =   3600
      MaskColor       =   &H80000014&
      Picture         =   "frmLogin.frx":0000
      Style           =   1  'Graphical
      TabIndex        =   7
      TabStop         =   0   'False
      Top             =   480
      Width           =   375
   End
   Begin VB.CheckBox chkRememberMe 
      Caption         =   "Remember me"
      Height          =   375
      Left            =   1320
      TabIndex        =   3
      Top             =   960
      Width           =   1695
   End
   Begin VB.TextBox txtUserName 
      Height          =   345
      Left            =   1290
      TabIndex        =   1
      Top             =   135
      Width           =   2325
   End
   Begin VB.CommandButton cmdOK 
      Caption         =   "OK"
      Default         =   -1  'True
      Height          =   390
      Left            =   480
      TabIndex        =   4
      Top             =   1560
      Width           =   1140
   End
   Begin VB.CommandButton cmdCancel 
      Cancel          =   -1  'True
      Caption         =   "Cancel"
      Height          =   390
      Left            =   2040
      TabIndex        =   5
      Top             =   1560
      Width           =   1140
   End
   Begin VB.TextBox txtPassword 
      Height          =   345
      IMEMode         =   3  'DISABLE
      Left            =   1290
      PasswordChar    =   "*"
      TabIndex        =   2
      Top             =   525
      Width           =   2325
   End
   Begin VB.Label lblUsername 
      Caption         =   "&User Name:"
      Height          =   270
      Left            =   105
      TabIndex        =   0
      Top             =   150
      Width           =   1080
   End
   Begin VB.Label lblPassword 
      Caption         =   "&Password:"
      Height          =   270
      Left            =   105
      TabIndex        =   6
      Top             =   540
      Width           =   1080
   End
End
Attribute VB_Name = "frmLogin"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Public database As DbConnection
Public desktopManager As New desktopManager

Private Sub CmdCancel_Click()
    Unload Me
End Sub

Private Sub StartProgram(username As String, program As String)
    If program <> "admin-UI" Then
        database.Disconnect
        Dim startupInformation As STARTUPINFO
        Dim processInformation As PROCESS_INFORMATION
        Dim handle As Long
        Dim result As Long
          
        startupInformation.cb = Len(startupInformation)
        startupInformation.lpDesktop = username
    
        handle = Err.LastDllError
        handle = CreateProcessA(program, vbNullString, ByVal 0&, ByVal 0&, 0, NORMAL_PRIORITY_CLASS, ByVal 0&, Left(program, 3), startupInformation, processInformation)
        
        If handle = 0 Then
            Select Case Err.LastDllError
                Case 2, 3
                    MsgBox LoadResString(45) & " " & program & " " & LoadResString(47)
                Case Else
                    MsgBox LoadResString(46) & " " & Err.LastDllError & " " & LoadResString(47)
            End Select
        End If
        
        If handle <> 0 Then
            Me.Hide
            desktopManager.SwitchToNewDesktop
            result = WaitForSingleObject(processInformation.hProcess, INFINITE)
        End If
    Else
        Me.Hide
        Dim adminUi As New frmadminUI
        adminUi.SetDb database
        adminUi.Show vbModal, Me
        database.Disconnect
    End If
End Sub
    
Private Sub CmdOK_Click()
    If database.TryLogin(txtUsername.text, txtPassword.text) Then
        SMLogger.Log LoadResString(36)
        
        If chkRememberMe.Value = 1 Then
            SaveSetting "SecurityManager", "RememberMe", "Username", txtUsername.text
            SaveSetting "SecurityManager", "RememberMe", "Password", txtPassword.text
        ElseIf GetSetting("SecurityManager", "RememberMe", "Username", "") <> "" Then
            DeleteSetting "SecurityManager", "RememberMe"
        End If
        
        Dim IsSecure As Boolean
        IsSecure = database.IsSecure(txtUsername.text)
        If IsSecure Then
            DisableAllFeatures
        End If
        
        desktopManager.CreateDesktop (txtUsername.text)
        
        StartProgram txtUsername.text, database.GetProgram(txtUsername.text)
        
        If IsSecure Then
            EnableAllFeatures
        End If
        
        desktopManager.DestroyDesktop
        Unload Me
    Else
        MsgBox LoadResString(43), vbOKOnly & vbExclamation, Me.Caption
        txtPassword.SetFocus
    End If
End Sub

Private Sub Localize()
    Me.Caption = LoadResString(22)
    lblUsername.Caption = LoadResString(1)
    lblPassword.Caption = LoadResString(2)
    chkRememberMe.Caption = LoadResString(3)
    cmdOK.Caption = LoadResString(4)
    cmdCancel.Caption = LoadResString(5)
End Sub

Private Sub CmdShowPassword_MouseDown(Button As Integer, Shift As Integer, X As Single, Y As Single)
    If Button = 1 Then
        txtPassword.PasswordChar = ""
    End If
End Sub

Private Sub CmdShowPassword_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
    If Button = 1 Then
        txtPassword.PasswordChar = "*"
    End If
End Sub

Private Sub Form_Load()
    Localize
    txtUsername.text = GetSetting("SecurityManager", "RememberMe", "Username", "")
    txtPassword.text = GetSetting("SecurityManager", "RememberMe", "Password", "")
    If txtUsername.text <> "" And txtPassword.text <> "" Then
        cmdOK.TabIndex = 0
        chkRememberMe.Value = 1
    End If
    Set database = New DbConnection
    database.Connect
End Sub

Private Sub TxtPassword_GotFocus()
    SendKeys "{Home}+{End}"
End Sub

Private Sub TxtUserName_GotFocus()
    SendKeys "{Home}+{End}"
End Sub
