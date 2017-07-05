VERSION 5.00
Begin VB.Form frmAdminUI 
   Caption         =   "Form1"
   ClientHeight    =   5430
   ClientLeft      =   8955
   ClientTop       =   6420
   ClientWidth     =   11415
   LinkTopic       =   "Form1"
   ScaleHeight     =   5430
   ScaleWidth      =   11415
   Begin VB.CommandButton cmdChange 
      Caption         =   "Change selected"
      Height          =   375
      Left            =   2280
      TabIndex        =   4
      Top             =   4920
      Width           =   1695
   End
   Begin VB.CommandButton cmdRemove 
      Caption         =   "Remove selected"
      Height          =   375
      Left            =   120
      TabIndex        =   3
      Top             =   4920
      Width           =   1935
   End
   Begin VB.CommandButton cmdRefresh 
      Caption         =   "Refresh"
      Height          =   375
      Left            =   9600
      TabIndex        =   2
      Top             =   120
      Width           =   1695
   End
   Begin VB.ListBox users 
      ForeColor       =   &H80000012&
      Height          =   4155
      Left            =   120
      TabIndex        =   1
      Top             =   600
      Width           =   11175
   End
   Begin VB.CommandButton cmdAddUser 
      Caption         =   "Add user"
      Height          =   375
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   2535
   End
End
Attribute VB_Name = "frmadminUI"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim database As DbConnection
Const bigTab = vbTab & vbTab & vbTab

Public Sub SetDb(db As DbConnection)
    Set database = db
End Sub

Private Sub CmdAddUser_Click()
    Dim AddUserForm As New frmUser
    With AddUserForm
        .SetDb database
        .SetMode Add
        .Show vbModal, Me
    End With

    RefreshList
End Sub

Private Sub RefreshList()
    Dim formattedUsers() As user
    formattedUsers() = database.GetUsers
    
    users.Clear
    users.AddItem (LoadResString(6) & bigTab & LoadResString(2) & bigTab & LoadResString(11) & bigTab & LoadResString(12))
    
    Dim formattedUser As Variant
    For Each formattedUser In formattedUsers
        users.AddItem formattedUser.username & bigTab & formattedUser.password & bigTab & formattedUser.IsSecure & bigTab & formattedUser.program
    Next
End Sub

Private Sub CmdChange_Click()
    If users.ListIndex < 1 Then
        MsgBox LoadResString(16)
        Exit Sub
    End If
    
    Dim ChangeUserFrom As New frmUser
    With ChangeUserFrom
        .SetDb database
        .SetMode Change
        .SetUser GetSelectedUser()
        .Show vbModal, Me
    End With
    
    RefreshList
End Sub

Private Sub CmdRefresh_Click()
    RefreshList
End Sub

Private Function GetSelectedUser() As user
    Dim selectedUser As New user
    Dim userFields() As String
    
    userFields = Split(users.List(users.ListIndex), bigTab)
    With selectedUser
        .username = userFields(0)
        .password = userFields(1)
        .IsSecure = userFields(2)
        .program = userFields(3)
    End With
    
    Set GetSelectedUser = selectedUser
End Function

Private Sub CmdRemove_Click()
    If users.ListIndex < 1 Then
        MsgBox LoadResString(16)
        Exit Sub
    End If
    
    If MsgBox(LoadResString(15), vbYesNo) = vbNo Then
        Exit Sub
    End If

    Dim selectedUser As user
    Set selectedUser = GetSelectedUser
    
    If selectedUser.program = "admin-UI" And database.GetCountOfAdmins = 1 Then
        MsgBox LoadResString(48)
        Exit Sub
    End If
    
    users.RemoveItem (users.ListIndex)
    database.RemoveUser (selectedUser.username)
End Sub

Private Sub Localize()
    Me.Caption = LoadResString(21)
    cmdAddUser.Caption = LoadResString(7)
    cmdRefresh.Caption = LoadResString(8)
    cmdRemove.Caption = LoadResString(9)
    cmdChange.Caption = LoadResString(10)
End Sub

Private Sub Form_Load()
    Localize
    RefreshList
End Sub

Private Sub Users_Click()
    If users.ListIndex = 0 Then
        users.ListIndex = -1
    End If
End Sub

Private Sub Users_DblClick()
    If users.ListIndex > 0 Then
        CmdChange_Click
    End If
End Sub


