#include "stdafx.h"
#include "DatabaseConnection_i.h"
#include "dllmain.h"

CDatabaseConnectionModule _AtlModule;

// DLL Entry Point
extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
    return _AtlModule.DllMain(dwReason, lpReserved); 
}
