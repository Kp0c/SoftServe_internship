// SetupInitialCatalogPage.cpp : implementation file
//

#include "stdafx.h"
#include "DatabaseSetupMFC.h"
#include "SetupInitialCatalogPage.h"
#include "afxdialogex.h"


// SetupInitialCatalogPage dialog

IMPLEMENT_DYNAMIC(SetupInitialCatalogPage, CPropertyPage)

SetupInitialCatalogPage::SetupInitialCatalogPage(std::wstring initialCatalog)
	: CPropertyPage(IDD_SETUPINITIALCATALOGPAGE),
    initialCatalog(initialCatalog)
{
}

BOOL SetupInitialCatalogPage::OnSetActive()
{
    GetDlgItem(IDC_INITIALCATALOG)->SetWindowTextW(initialCatalog.c_str());

    return CPropertyPage::OnSetActive();
}

SetupInitialCatalogPage::~SetupInitialCatalogPage()
{
}

void SetupInitialCatalogPage::DoDataExchange(CDataExchange* pDX)
{
	CPropertyPage::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(SetupInitialCatalogPage, CPropertyPage)
END_MESSAGE_MAP()


// SetupInitialCatalogPage message handlers
