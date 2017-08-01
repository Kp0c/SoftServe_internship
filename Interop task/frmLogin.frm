VERSION 5.00
Begin VB.Form frmLogin 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Login"
   ClientHeight    =   2175
   ClientLeft      =   12150
   ClientTop       =   7560
   ClientWidth     =   3975
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1285.062
   ScaleMode       =   0  'User
   ScaleWidth      =   3732.31
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

Private Sub CmdCancel_Click()
    Unload Me
End Sub

Private Function Validate(field As String, fieldName As String) As Boolean
    Validate = True
    
    If field = "" Then
        Validate = False
        MsgBox fieldName & " cannot be empty"
    End If
    
     If InStr(1, field, "'") + InStr(1, field, " ") > 0 Then
        Validate = False
        MsgBox fieldName & " cannot contain ' or space symbol"
     End If
End Function

Private Function ValidateLogin() As Boolean
    ValidateLogin = Validate(txtUserName.text, "Login")
    If Not ValidateLogin Then
        txtUserName.SetFocus
    End If
End Function

Private Function ValidatePassword() As Boolean
    ValidatePassword = Validate(txtPassword.text, "Password")
    If Not ValidatePassword Then
        txtPassword.SetFocus
    End If
End Function
    
Private Sub CmdOK_Click()
   Dim a As New DbCon
   
   a.AddUser txtUserName.text, txtPassword.text
   
    If ValidateLogin() And ValidatePassword() Then
        MsgBox "validated"
    End If
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

Private Sub TxtPassword_GotFocus()
    SendKeys "{Home}+{End}"
End Sub

Private Sub TxtUserName_GotFocus()
    SendKeys "{Home}+{End}"
End Sub
