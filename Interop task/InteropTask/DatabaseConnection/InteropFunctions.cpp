#include "stdafx.h"

#using <mscorlib.dll>
#using <NewTransactionDll.dll>

#include "InteropFunctions.h"
using namespace System;
using namespace System::Collections::Generic;
using namespace NewTransactionDll;

void AddNewTransaction(std::wstring username)
{
    String^ str = gcnew String(username.c_str());
    NewTransaction^ form = gcnew NewTransaction(str);
}

USER_API void ShowGUI(HWND parentHwnd, BSTR username)
{
    ICppGUI* gui = NULL;
    CoCreateInstance(CLSID_CppGUI, NULL, CLSCTX_INPROC_SERVER, IID_ICppGUI, (void**)&gui);

    gui->ShowGUI((LONG)parentHwnd, username);
}