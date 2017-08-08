#include "stdafx.h"
#include "DatabaseSetupMFC.h"
#include "SetupDataSourcePage.h"

IMPLEMENT_DYNAMIC(SetupDataSourcePage, CPropertyPage)

SetupDataSourcePage::SetupDataSourcePage(std::wstring& dataSource)
    : CPropertyPage(IDD_SETUP_DATASOURCE_PAGE),
    dataSource(dataSource)
{
}

BOOL SetupDataSourcePage::OnSetActive()
{
    if (!isInitialized)
    {
        GetDlgItem(IDC_DATASOURCE)->SetWindowTextW(dataSource.c_str());
        isInitialized = true;
    }
    CPropertySheet* sheet = (CPropertySheet*)GetParent();

    sheet->SetWizardButtons(PSWIZB_NEXT | PSWIZB_CANCEL);

    return CPropertyPage::OnSetActive();
}

void SetupDataSourcePage::DoDataExchange(CDataExchange* pDX)
{
    CPropertyPage::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(SetupDataSourcePage, CPropertyPage)
END_MESSAGE_MAP()
