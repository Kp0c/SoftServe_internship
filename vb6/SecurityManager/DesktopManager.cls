VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "DesktopManager"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit
Dim originalDeskHandle As Long
Dim newDeskHandle As Long

Private Sub Class_Initialize()
    originalDeskHandle = GetThreadDesktop(GetCurrentThreadId())
End Sub

Public Sub DestroyDesktop()
    Dim result As Long
    result = SwitchDesktop(originalDeskHandle)
    result = CloseHandle(newDeskHandle)
    SMLogger.Log (LoadResString(38))
End Sub

Public Sub SwitchToNewDesktop()
    SMLogger.Log (LoadResString(42))
    SetThreadDesktop (newDeskHandle)
    SwitchDesktop (newDeskHandle)
End Sub

Public Sub CreateDesktop(name As String)
    newDeskHandle = OpenDesktopA(name, ByVal 0&, False, GENERIC_ALL)
    
    If newDeskHandle = 0 Then
        newDeskHandle = CreateDesktopA(name, vbNullString, ByVal 0&, 0, GENERIC_ALL, ByVal 0&)
    End If
    SMLogger.Log (LoadResString(37) & " " & newDeskHandle)
End Sub

