VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "Logger"
Attribute VB_GlobalNameSpace = True
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private FileNum As Integer

Public Sub Class_Initialize()
    FileNum = FreeFile
    Open App.Path & "\log.txt" For Output As #FileNum
End Sub

Public Sub Log(text As String)
    Dim message As String
    message = Format$(Now, "dd.mm.yyyy") & " at " & Format$(Time, "hh:mm:ss") & ". Text: " & text
    Print #FileNum, message
End Sub

Public Sub Class_Terminate()
    Close #FileNum
End Sub
