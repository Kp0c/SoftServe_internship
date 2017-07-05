VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "User"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private username_ As String
Private password_ As String
Private program_ As String
Private isSecure_ As Integer

Public Property Get username() As String
    username = username_
End Property

Public Property Let username(newValue As String)
    If newValue = "" Then Err.Raise 5
    username_ = newValue
End Property

Public Property Get password() As String
    password = password_
End Property

Public Property Let password(newValue As String)
    If newValue = "" Then Err.Raise 5
    password_ = newValue
End Property

Public Property Get program() As String
    program = program_
End Property

Public Property Let program(newValue As String)
    If newValue = "" Then Err.Raise 5
    program_ = newValue
End Property

Public Property Get IsSecure() As Integer
    IsSecure = isSecure_
End Property

Public Property Let IsSecure(newValue As Integer)
    isSecure_ = newValue
End Property
