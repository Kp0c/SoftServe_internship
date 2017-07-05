Attribute VB_Name = "Registry"
Option Explicit

Public Const HKEY_LOCAL_MACHINE = &H80000002
Public Const HKEY_CURRENT_User = &H80000001
Public Const REG_DWORD As Long = 4

Public Sub ChangeAllFeatures(keyValue As Long)
    Dim keyBase As String
    keyBase = "Software\Microsoft\Windows\CurrentVersion\Policies\Explorer"
    
    UpdateValue HKEY_CURRENT_User, keyBase, "NoLogOff", keyValue
    UpdateValue HKEY_CURRENT_User, keyBase, "NoClose", keyValue
    
    keyBase = "Software\Microsoft\Windows\CurrentVersion\Policies\System"
    
    UpdateValue HKEY_CURRENT_User, keyBase, "DisableLockWorkstation", keyValue
    UpdateValue HKEY_CURRENT_User, keyBase, "DisableChangePassword", keyValue
    UpdateValue HKEY_CURRENT_User, keyBase, "DisableTaskMgr", keyValue
    UpdateValue HKEY_LOCAL_MACHINE, keyBase, "HideFastUserSwitching", keyValue
End Sub

Public Sub DisableAllFeatures()
    ChangeAllFeatures (1)
    SMLogger.Log LoadResString(24)
End Sub

Public Sub EnableAllFeatures()
    ChangeAllFeatures (0)
    SMLogger.Log LoadResString(25)
End Sub

Private Sub UpdateValue(ByVal RootKey As Long, ByVal RegKeyPath As String, ByVal RegSubKey As String, RegData As Long)
    Dim KeyHandle As Long
    
    RegCreateKey RootKey, RegKeyPath, KeyHandle
    RegSetValueEx KeyHandle, RegSubKey, 0&, REG_DWORD, RegData, Len(RegData)
             
    RegCloseKey KeyHandle
End Sub
