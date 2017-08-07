#include "stdafx.h"
#include "DatabaseSetupMFC.h"
#include "SetupDataSourcePage.h"
#include "SetupInitialCatalogPage.h"
#include "ValidationPage.h"
#include "Helper.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

BEGIN_MESSAGE_MAP(CDatabaseSetupMFCApp, CWinApp)
END_MESSAGE_MAP()
CDatabaseSetupMFCApp theApp;

CDatabaseSetupMFCApp::CDatabaseSetupMFCApp()
{
}


BOOL CDatabaseSetupMFCApp::InitInstance()
{
    CWinApp::InitInstance();

    std::vector<std::wstring> settingsName{ L"Data Source", L"Initial Catalog" };

    auto settings = GetSettings(settingsName);

    CPropertySheet db(L"Setup database");
    SetupDataSourcePage dataSourcePage(settings[settingsName[0]]);
    SetupInitialCatalogPage initialCatalogPage(settings[settingsName[1]]);
    ValidationPage validationPage(dataSourcePage, initialCatalogPage);

    db.AddPage(&dataSourcePage);
    db.AddPage(&initialCatalogPage);
    db.AddPage(&validationPage);

    db.SetWizardMode();

    m_pMainWnd = &db;
    db.DoModal();

    return FALSE;
}

std::map<std::wstring, std::wstring> CDatabaseSetupMFCApp::GetSettings(std::vector<std::wstring> settingsName)
{
    std::map<std::wstring, std::wstring> settings;
    HKEY hkey;
    if (RegCreateKeyEx(HKEY_CURRENT_USER, settingsLocation, 0, nullptr, REG_OPTION_VOLATILE, KEY_READ | KEY_WOW64_32KEY,
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
