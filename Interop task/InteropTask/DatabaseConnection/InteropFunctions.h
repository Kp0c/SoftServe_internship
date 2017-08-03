#pragma once
#include "DatabaseConnection_i.h"
#include <string>

#ifdef _USRDLL
#define USER_API EXTERN_C __declspec(dllexport) 
#elif
#define USER_API EXTERN_C __declspec(dllimport)
#endif // _USRDLL

void AddNewTransaction(std::wstring username);

USER_API void ShowGUI(HWND parentHwnd, BSTR username);