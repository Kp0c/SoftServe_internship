Attribute VB_Name = "SendKeysFix"
Option Explicit

Public Sub SendKeys(text As String, Optional wait As Boolean = False)
    Dim WshShell As Object
    Set WshShell = CreateObject("wscript.shell")
    WshShell.SendKeys text, wait
    Set WshShell = Nothing
End Sub
