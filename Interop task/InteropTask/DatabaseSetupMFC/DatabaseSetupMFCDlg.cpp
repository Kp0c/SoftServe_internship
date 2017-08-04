
// DatabaseSetupMFCDlg.cpp : implementation file
//

#include "stdafx.h"
#include "DatabaseSetupMFC.h"
#include "DatabaseSetupMFCDlg.h"
#include "afxdialogex.h"

#import "msado15.dll" rename_namespace("ADO") rename("EOF", "EndOfFile") implementation_only

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CDatabaseSetupMFCDlg dialog



CDatabaseSetupMFCDlg::CDatabaseSetupMFCDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_DATABASESETUPMFC_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CDatabaseSetupMFCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CDatabaseSetupMFCDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
    ON_BN_CLICKED(IDOK, &CDatabaseSetupMFCDlg::OnBnClickedOk)
END_MESSAGE_MAP()


// CDatabaseSetupMFCDlg message handlers

BOOL CDatabaseSetupMFCDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);		// Set big icon
	SetIcon(m_hIcon, FALSE);	// Set small icon

    std::vector<std::wstring> settingsName{ L"Data Source", L"Initial Catalog" };
    auto settings = GetSettings(settingsName);

    CWnd* dataSourceEdit = GetDlgItem(IDC_DATA_SOURCE_EDIT);
    dataSourceEdit->SetWindowTextW(settings[settingsName[0]].c_str());

    dataSourceEdit = GetDlgItem(IDC_INITIAL_CATALOG_EDIT);
    dataSourceEdit->SetWindowTextW(settings[settingsName[1]].c_str());

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.
void CDatabaseSetupMFCDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CDatabaseSetupMFCDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

bool CDatabaseSetupMFCDlg::TryConnect(std::wstring connectionString)
{
    ADO::Connection15Ptr connection = NULL;

    CoInitialize(NULL);
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

std::map<std::wstring, std::wstring> CDatabaseSetupMFCDlg::GetSettings(std::vector<std::wstring> settingsName)
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

void CDatabaseSetupMFCDlg::SaveSettings(std::map<std::wstring, std::wstring> settings)
{
    HKEY hkey;
    if (RegCreateKeyEx(HKEY_CURRENT_USER, L"Software\\VB and VBA Program Settings\\LastTask\\Database", 0, nullptr, REG_OPTION_VOLATILE, KEY_WRITE | KEY_WOW64_32KEY,
        nullptr, &hkey, nullptr) == ERROR_SUCCESS)
    {
        for (auto setting : settings)
        {
            LPCWSTR data = setting.second.c_str();
            int l = RegSetValueEx(hkey, setting.first.c_str(), 0, REG_SZ, (LPBYTE)data, setting.second.length() * sizeof(wchar_t));
            l = GetLastError();
            l++;
        }
        RegCloseKey(hkey);
    }
}

std::wstring CDatabaseSetupMFCDlg::BuildConnectionString()
{
    LPTSTR tempString = new TCHAR[100];
    CWnd* dataSourceEdit = GetDlgItem(IDC_DATA_SOURCE_EDIT);
    dataSourceEdit->GetWindowTextW(tempString, 100);
    std::wstring dataSource(L"Data Source=" + std::wstring(tempString) + L";");

    dataSourceEdit = GetDlgItem(IDC_INITIAL_CATALOG_EDIT);
    dataSourceEdit->GetWindowTextW(tempString, 100);
    std::wstring initialCatalog(L"Initial Catalog=" + std::wstring(tempString) + L";");

    delete[] tempString;

    return L"Provider=sqloleDb;" + dataSource + initialCatalog + L"Trusted_Connection=yes;";
}

void CDatabaseSetupMFCDlg::OnBnClickedOk()
{
    if (TryConnect(BuildConnectionString()))
    {

        LPTSTR tempString = new TCHAR[100];
        CWnd* dataSourceEdit = GetDlgItem(IDC_DATA_SOURCE_EDIT);
        dataSourceEdit->GetWindowTextW(tempString, 100);
        std::wstring dataSource(tempString);

        dataSourceEdit = GetDlgItem(IDC_INITIAL_CATALOG_EDIT);
        dataSourceEdit->GetWindowTextW(tempString, 100);
        std::wstring initialCatalog(tempString);

        delete[] tempString;

        std::map<std::wstring, std::wstring> settings{
            {L"Data Source", dataSource.c_str()},
            {L"Initial Catalog", initialCatalog.c_str()},
            {L"Trusted_Connection", L"yes"}
        };

        SaveSettings(settings);

        CDialogEx::OnOK();
    }
    else
    {
        MessageBox(L"Bad connection string, try again");
    }
}
