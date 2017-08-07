#include "stdafx.h"
#include "DatabaseSetupMFC.h"
#include "DatabaseSetupMFCDlg.h"
#include "SetupDatabaseSheet.h"
#include "SetupDataSourcePage.h"
#include "SetupInitialCatalogPage.h"
#include "ValidationPage.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

BEGIN_MESSAGE_MAP(CDatabaseSetupMFCApp, CWinApp)
END_MESSAGE_MAP()

CDatabaseSetupMFCApp::CDatabaseSetupMFCApp()
{
    // support Restart Manager
    m_dwRestartManagerSupportFlags = AFX_RESTART_MANAGER_SUPPORT_RESTART;
}

CDatabaseSetupMFCApp theApp;

BOOL CDatabaseSetupMFCApp::InitInstance()
{
    // InitCommonControlsEx() is required on Windows XP if an application
    // manifest specifies use of ComCtl32.dll version 6 or later to enable
    // visual styles.  Otherwise, any window creation will fail.
    INITCOMMONCONTROLSEX InitCtrls;
    InitCtrls.dwSize = sizeof(InitCtrls);
    // Set this to include all the common control classes you want to use
    // in your application.
    InitCtrls.dwICC = ICC_WIN95_CLASSES;
    InitCommonControlsEx(&InitCtrls);

    CWinApp::InitInstance();

    AfxEnableControlContainer();

    std::vector<std::wstring> settingsName{ L"Data Source", L"Initial Catalog" };

    auto settings = GetSettings(settingsName);

    SetupDatabaseSheet db(L"Setup database");
    SetupDataSourcePage dataSourcePage(settings[settingsName[0]]);
    SetupInitialCatalogPage initialCatalogPage(settings[settingsName[1]]);
    ValidationPage validationPage(&dataSourcePage, &initialCatalogPage);

    db.AddPage(&dataSourcePage);
    db.AddPage(&initialCatalogPage);
    db.AddPage(&validationPage);

    db.SetWizardMode();

    m_pMainWnd = &db;
    db.DoModal();

#ifndef _AFXDLL
    ControlBarCleanUp();
#endif

    // Since the dialog has been closed, return FALSE so that we exit the
    //  application, rather than start the application's message pump.
    return FALSE;
}

std::map<std::wstring, std::wstring> CDatabaseSetupMFCApp::GetSettings(std::vector<std::wstring> settingsName)
{
    std::map<std::wstring, std::wstring> settings;
    HKEY hkey;
    if (RegCreateKeyEx(HKEY_CURRENT_USER, L"Software\\VB and VBA Program Settings\\LastTask\\Database", 0, nullptr, REG_OPTION_VOLATILE, KEY_READ | KEY_WOW64_32KEY,
        nullptr, &hkey, nullptr) == ERROR_SUCCESS)
    {
        for (std::wstring setting : settingsName)
        {
            TCHAR data[255];
            DWORD  buff = sizeof(data);
            if (RegGetValue(hkey, nullptr, setting.c_str(), RRF_RT_REG_SZ, nullptr, data, &buff) == ERROR_SUCCESS)
            {
                settings[setting] = data;
            }
        }

        RegCloseKey(hkey);
    }

    return settings;
}
