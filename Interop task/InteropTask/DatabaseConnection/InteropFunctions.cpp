#include "stdafx.h"
#include "InteropFunctions.h"
#using <mscorlib.dll>
#using <System.dll>
#using <System.Windows.Forms.dll>
#using <DatabaseConnectionAdmin.dll>
#using <DatabaseSetup.exe>

using namespace System;
using namespace System::Windows::Forms;
using namespace DatabaseConnectionAdmin;

void AddNewTransaction(std::wstring username)
{
    String^ str = gcnew String(username.c_str());
    NewTransaction^ newTransaction = gcnew NewTransaction(str);
    newTransaction->ShowDialog();
    //NewTransaction^ form = gcnew NewTransaction(str);
}

void SetupDatabase()
{
    DatabaseSetup::DatabaseSetup^ dbSetup = gcnew DatabaseSetup::DatabaseSetup();
    dbSetup->ShowDialog();
}

USER_API void ShowGUI(HWND parentHwnd, BSTR username)
{
    ICppGUI* gui = NULL;
    CoCreateInstance(CLSID_CppGUI, NULL, CLSCTX_INPROC_SERVER, IID_ICppGUI, (void**)&gui);

    gui->ShowGUI((LONG)parentHwnd, username);
}
