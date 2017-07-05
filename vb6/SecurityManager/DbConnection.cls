VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "DbConnection"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Attribute VB_Ext_KEY = "SavedWithClassBuilder6" ,"Yes"
Attribute VB_Ext_KEY = "Top_Level" ,"Yes"
Option Explicit
Private connection As New ADODB.connection

Public Sub Disconnect()
    SMLogger.Log LoadResString(26)
    connection.Close
End Sub

Function CheckForInjection(username As String, Optional password As String = "", Optional program As String = "") As Boolean
    If InStr(1, username, "'") > 0 Then
        MsgBox (LoadResString(1) & " " & LoadResString(27))
        Exit Function
    ElseIf InStr(1, password, "'") > 0 Then
        MsgBox (LoadResString(2) & " " & LoadResString(27))
        Exit Function
    ElseIf InStr(1, program, "'") > 0 Then
        MsgBox (LoadResString(12) & " " & LoadResString(27))
        Exit Function
    End If
End Function

Function TryLogin(username As String, password As String) As Boolean
    TryLogin = False
    
    If CheckForInjection(username, password) Then
        Exit Function
    End If
    
    Dim record As New ADODB.Recordset
    
    password = Encryptor.Crypt(password)
    record.Open "SELECT username FROM Users WHERE username='" & username & "' AND pass='" & password & "';", connection, adOpenKeyset
        
    If record.RecordCount = 1 Then
        SMLogger.Log (username & " " & LoadResString(28))
        TryLogin = True
    Else
        TryLogin = False
    End If
End Function

Function IsSecure(username As String) As Boolean
    Dim record As New ADODB.Recordset
    
    record.Open "SELECT is_secure FROM USERS WHERE username='" & username & "';", connection, adOpenKeyset
    
    IsSecure = record.Fields("is_secure").Value
End Function

Function GetUsers() As user()
    Dim record As New ADODB.Recordset
    Dim i As Integer
    
    record.Open "SELECT * FROM USERS", connection, adOpenKeyset
    
    Dim users() As New user
    ReDim users(record.RecordCount - 1)
    
    For i = 0 To record.RecordCount - 1
        users(i).username = record.Fields("username").Value
        users(i).password = Encryptor.Decrypt(record.Fields("pass").Value)
        users(i).program = record.Fields("program").Value
        users(i).IsSecure = Abs(record.Fields("is_secure").Value)
        record.MoveNext
    Next i
    
    GetUsers = users()
End Function

Function GetCountOfAdmins() As Integer
    Dim record As New ADODB.Recordset
    
    record.Open "Select username FROM Users WHERE program='Admin-UI'", connection, adOpenKeyset
    
    GetCountOfAdmins = record.RecordCount
End Function

Function AddUserForm(username As String, password As String, program As String, IsSecure As Integer) As Boolean
    If CheckForInjection(username, password, program) Then
        Exit Function
    End If
    
    On Error GoTo ErrHandler
    password = Encryptor.Crypt(password)
    connection.Execute ("INSERT INTO Users VALUES('" & username & "', '" & password & "', '" & program & "', " & IsSecure & ");")
    SMLogger.Log (LoadResString(30) & " " & username & ", " & LoadResString(31) & " " & program & " " & LoadResString(32) & " " & IsSecure & " " & LoadResString(33))
    AddUserForm = True
    Exit Function
    
ErrHandler:
    AddUserForm = False
End Function

Sub RemoveUser(username As String)
    connection.Execute ("DELETE FROM Users WHERE username='" & username & "';")
    SMLogger.Log (LoadResString(29) & " " & username)
End Sub

Sub ChangeUserFrom(username As String, newPassword As String, newProgram As String, newSecure As Integer)
    If CheckForInjection(username, newPassword, newProgram) Then
        Exit Sub
    End If
    
    newPassword = Encryptor.Crypt(newPassword)
    connection.Execute ("UPDATE Users SET pass='" & newPassword & "', program='" & newProgram & "', is_secure='" & newSecure & "' WHERE username='" & username & "';")
    SMLogger.Log (LoadResString(39) & " " & username & " " & LoadResString(40) & " " & newProgram & " " & LoadResString(41) & " " & newSecure)
End Sub

Function GetProgram(username As String) As String
    Dim record As New ADODB.Recordset
    
    record.Open "SELECT program FROM USERS WHERE username='" & username & "';", connection, adOpenKeyset
    
    GetProgram = record.Fields("Program").Value
End Function

Public Sub Connect()
    SMLogger.Log LoadResString(34)
    connection.Open ("Provider=sqloleDb;Data Source=NETDAAN;Initial Catalog=VB6;Trusted_Connection=yes;")
    SMLogger.Log LoadResString(35)
End Sub


