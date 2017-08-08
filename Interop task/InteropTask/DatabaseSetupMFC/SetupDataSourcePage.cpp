#include "stdafx.h"
#include "DatabaseSetupMFC.h"
#include "SetupDataSourcePage.h"

IMPLEMENT_DYNAMIC(SetupDataSourcePage, CPropertyPage)

SetupDataSourcePage::SetupDataSourcePage(std::wstring& dataSource)
    : CPropertyPage(IDD_SETUP_DATASOURCE_PAGE),
    _dataSource(dataSource)
{
}

BOOL SetupDataSourcePage::OnSetActive()
{
    if (!_isInitialized)
    {
        GetDlgItem(IDC_DATASOURCE)->SetWindowTextW(_dataSource.c_str());
        _isInitialized = true;
    }
    CPropertySheet* sheet = static_cast<CPropertySheet*>(GetParent());

    sheet->SetWizardButtons(PSWIZB_NEXT | PSWIZB_CANCEL);

    return CPropertyPage::OnSetActive();
}

void SetupDataSourcePage::DoDataExchange(CDataExchange* pDX)
{
    CPropertyPage::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(SetupDataSourcePage, CPropertyPage)
END_MESSAGE_MAP()
