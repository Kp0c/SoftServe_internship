VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form frmUser 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Change user"
   ClientHeight    =   2985
   ClientLeft      =   11820
   ClientTop       =   6915
   ClientWidth     =   6120
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2985
   ScaleWidth      =   6120
   ShowInTaskbar   =   0   'False
   Begin VB.CheckBox chkIsSecure 
      Caption         =   "Secure"
      Height          =   375
      Left            =   120
      TabIndex        =   4
      ToolTipText     =   "Disable many features from CTRL+ALT+DELETE menu"
      Top             =   1680
      Width           =   1575
   End
   Begin VB.CommandButton cmdSelectFile 
      Caption         =   "..."
      Height          =   375
      Left            =   5520
      TabIndex        =   6
      Top             =   2520
      Width           =   495
   End
   Begin MSComDlg.CommonDialog selectFileDialog 
      Left            =   3120
      Top             =   840
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.TextBox txtUsername 
      ForeColor       =   &H80000007&
      Height          =   375
      Left            =   120
      TabIndex        =   1
      Top             =   480
      Width           =   1575
   End
   Begin VB.TextBox txtPassword 
      Height          =   375
      Left            =   120
      TabIndex        =   3
      Top             =   1320
      Width           =   1575
   End
   Begin VB.TextBox txtProgram 
      BackColor       =   &H80000004&
      Enabled         =   0   'False
      Height          =   375
      Left            =   120
      TabIndex        =   0
      Top             =   2520
      Width           =   5415
   End
   Begin VB.CheckBox chkIsAdmin 
      Caption         =   "Admin"
      Height          =   255
      Left            =   1920
      TabIndex        =   5
      Top             =   2160
      Width           =   2295
   End
   Begin VB.CommandButton cmdCancel 
      Caption         =   "Cancel"
      Height          =   375
      Left            =   4800
      TabIndex        =   8
      Top             =   600
      Width           =   1215
   End
   Begin VB.CommandButton cmdOK 
      Caption         =   "OK"
      Height          =   375
      Left            =   4800
      TabIndex        =   7
      Top             =   120
      Width           =   1215
   End
   Begin VB.Label lblUsername 
      Caption         =   "Username:"
      Height          =   255
      Left            =   120
      TabIndex        =   10
      Top             =   120
      Width           =   1575
   End
   Begin VB.Label lblPassword 
      Caption         =   "Password:"
      Height          =   255
      Left            =   120
      TabIndex        =   9
      Top             =   960
      Width           =   1575
   End
   Begin VB.Label lblProgram 
      Caption         =   "Program:"
      Height          =   255
      Left            =   120
      TabIndex        =   2
      Top             =   2160
      Width           =   1575
   End
End
Attribute VB_Name = "frmUser"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Enum Mode
    Add
    Change
End Enum

Dim database As DbConnection
Dim myMode As Mode
Dim previousAdminStatus As Boolean

Public Sub SetDb(db As DbConnection)
    Set database = db
End Sub

Public Sub SetMode(newMode As Mode)
    myMode = newMode
End Sub

Public Sub SetUser(user As user)
    txtUsername.text = user.username
    txtPassword.text = user.password
    txtProgram.text = user.program
    chkIsSecure.Value = Abs(user.IsSecure)
    previousAdminStatus = (user.program = "admin-UI")
    If user.program = "admin-UI" Then
        chkIsAdmin.Value = 1
    End If
End Sub

Private Sub CmdCancel_Click()
    If MsgBox(LoadResString(15), vbYesNo) = vbYes Then
        Unload Me
    End If
End Sub

Private Sub ChkIsAdmin_Click()
    If chkIsAdmin.Value = 0 Then
        txtProgram.text = ""
        cmdSelectFile.Enabled = True
    Else
        txtProgram.text = "admin-UI"
        cmdSelectFile.Enabled = False
    End If
End Sub

Private Sub cmdSelectFile_Click()
    With selectFileDialog
        .Filter = LoadResString(18) & " (*.exe)|*.exe"
        .DefaultExt = "exe"
        .DialogTitle = LoadResString(17)
        .ShowOpen
    End With
    
    txtProgram.text = selectFileDialog.FileName
End Sub

Private Sub Localize()
    If myMode = Change Then
        Me.Caption = LoadResString(23)
    Else
        Me.Caption = LoadResString(7)
    End If
        
    lblUsername.Caption = LoadResString(1)
    lblPassword.Caption = LoadResString(2)
    chkIsSecure.Caption = LoadResString(13)
    lblProgram.Caption = LoadResString(12)
    chkIsAdmin.Caption = LoadResString(14)
    cmdOK.Caption = LoadResString(4)
    cmdCancel.Caption = LoadResString(5)
End Sub

Private Sub Form_Load()
    Localize
    
    If myMode = Change Then
        With txtUsername
            .TabIndex = 10
            .Locked = True
            .BackColor = vbMenuBar
        End With
    End If
End Sub

Private Sub CmdOK_Click()
    If LCase(txtUsername.text) = "default" Then
        MsgBox LoadResString(19)
        txtUsername.text = ""
        Exit Sub
    End If
        
    If txtUsername.text = "" Or txtPassword.text = "" Or txtProgram.text = "" Then
        MsgBox LoadResString(20)
    ElseIf myMode = Change Then
        If previousAdminStatus = True And chkIsAdmin.Value = "0" And database.GetCountOfAdmins = 1 Then
            MsgBox LoadResString(49)
        Else
            database.ChangeUserFrom txtUsername.text, txtPassword.text, txtProgram.text, Abs(chkIsSecure.Value)
            Unload Me
        End If
    ElseIf database.AddUserForm(txtUsername.text, txtPassword.text, txtProgram.text, chkIsSecure.Value) Then
        Unload Me
    Else
        MsgBox LoadResString(44)
    End If
End Sub

