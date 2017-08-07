#include "stdafx.h"
#include "DatabaseSetupMFC.h"
#include "ValidationPage.h"

#import "msado15.dll" rename_namespace("ADO") rename("EOF", "EndOfFile") implementation_only

IMPLEMENT_DYNAMIC(ValidationPage, CPropertyPage)

ValidationPage::ValidationPage(SetupDataSourcePage& dataSourcePage, SetupInitialCatalogPage& initialCatalogPage)
    : CPropertyPage(IDD_VALIDATIONPAGE),
    dataSourcePage(dataSourcePage),
    initialCatalogPage(initialCatalogPage)
{
}

void ValidationPage::DoDataExchange(CDataExchange* pDX)
{
    CPropertyPage::DoDataExchange(pDX);
}

BOOL ValidationPage::OnSetActive()
{
    CPropertySheet* sheet = static_cast<CPropertySheet*>(GetParent());
    if (TryConnect(BuildConnectionString()))
    {
        std::map<std::wstring, std::wstring> settings{
            { L"Data Source", GetDataSource().c_str() },
            { L"Initial Catalog", GetInitialCatalog().c_str() },
            { L"Trusted_Connection", L"yes" }
        };

        SaveSettings(settings);

        sheet->GetDlgItem(IDCANCEL)->DestroyWindow();
        sheet->SetFinishText(L"Done");

        return TRUE;
    }
    sheet->SetActivePage(&dataSourcePage);
    MessageBox(L"Bad connection string, try again");
    
    return FALSE;
}

bool ValidationPage::TryConnect(std::wstring connectionString)
{
    ADO::Connection15Ptr connection = NULL;

    CoInitialize(nullptr);
    connection.CreateInstance(__uuidof(ADO::Connection));

    bool isOk = true;
    try
    {
        connection->Open(connectionString.c_str(), "", "", ADO::ConnectOptionEnum::adConnectUnspecified);
    }
    catch (...)
    {
        isOk = false;
    }

    connection.Release();
    CoUninitialize();
    return isOk;
}

void ValidationPage::SaveSettings(std::map<std::wstring, std::wstring> settings)
{
    HKEY hkey;
    if (RegCreateKeyEx(HKEY_CURRENT_USER, L"Software\\VB and VBA Program Settings\\LastTask\\Database", 0, nullptr, REG_OPTION_VOLATILE, KEY_WRITE | KEY_WOW64_32KEY,
        nullptr, &hkey, nullptr) == ERROR_SUCCESS)
    {
        for (auto setting : settings)
        {
            LPCWSTR data = setting.second.c_str();
            RegSetValueEx(hkey, setting.first.c_str(), 0, REG_SZ, LPBYTE(data), setting.second.length() * sizeof(wchar_t));
        }
        RegCloseKey(hkey);
    }
}

std::wstring ValidationPage::BuildConnectionString() const
{
    std::wstring dataSource = L"Data Source=" + GetDataSource() + L";";
    std::wstring initialCatalog = L"Initial Catalog=" + GetInitialCatalog() + L";";
    return L"Provider=sqloleDb;" + dataSource + initialCatalog + L"Trusted_Connection=yes;";
}

std::wstring ValidationPage::GetDataSource() const
{
    LPTSTR tempString = new TCHAR[100];

    CWnd* dataSourceEdit = dataSourcePage.GetDlgItem(IDC_DATASOURCE);
    dataSourceEdit->GetWindowTextW(tempString, 100);
    std::wstring dataSource(tempString);

    delete[] tempString;

    return dataSource;
}

std::wstring ValidationPage::GetInitialCatalog() const
{
    LPTSTR tempString = new TCHAR[100];

    CWnd* initialCatalogEdit = initialCatalogPage.GetDlgItem(IDC_INITIALCATALOG);
    initialCatalogEdit->GetWindowTextW(tempString, 100);
    std::wstring initialCatalog(tempString);

    delete[] tempString;

    return initialCatalog;
}

BEGIN_MESSAGE_MAP(ValidationPage, CPropertyPage)
END_MESSAGE_MAP()
