// SetupDataSourcePage.cpp : implementation file
//

#include "stdafx.h"
#include "DatabaseSetupMFC.h"
#include "SetupDataSourcePage.h"
#include "afxdialogex.h"


// SetupDataSourcePage dialog

IMPLEMENT_DYNAMIC(SetupDataSourcePage, CPropertyPage)

SetupDataSourcePage::SetupDataSourcePage(std::wstring dataSource)
    : CPropertyPage(IDD_SETUP_DATASOURCE_PAGE),
    dataSource(dataSource)
{
}

BOOL SetupDataSourcePage::OnSetActive()
{
    GetDlgItem(IDC_DATASOURCE)->SetWindowTextW(dataSource.c_str());
    return CPropertyPage::OnSetActive();
}

SetupDataSourcePage::~SetupDataSourcePage()
{
}

void SetupDataSourcePage::DoDataExchange(CDataExchange* pDX)
{
	CPropertyPage::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(SetupDataSourcePage, CPropertyPage)
END_MESSAGE_MAP()
