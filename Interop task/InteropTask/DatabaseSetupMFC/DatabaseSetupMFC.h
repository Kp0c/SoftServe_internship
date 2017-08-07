#pragma once
#include "resource.h"
#include <vector>
#include <map>

#ifndef __AFXWIN_H__
    #error "include 'stdafx.h' before including this file for PCH"
#endif

class CDatabaseSetupMFCApp : public CWinApp
{
public:
    CDatabaseSetupMFCApp();

    BOOL InitInstance() override;
private:
    static std::map<std::wstring, std::wstring> GetSettings(std::vector<std::wstring> settingsName);

    DECLARE_MESSAGE_MAP()
};

extern CDatabaseSetupMFCApp theApp;
