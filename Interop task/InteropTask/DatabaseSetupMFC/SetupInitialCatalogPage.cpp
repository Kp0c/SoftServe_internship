#include "stdafx.h"
#include "DatabaseSetupMFC.h"
#include "SetupInitialCatalogPage.h"

IMPLEMENT_DYNAMIC(SetupInitialCatalogPage, CPropertyPage)

SetupInitialCatalogPage::SetupInitialCatalogPage(std::wstring& initialCatalog)
    : CPropertyPage(IDD_SETUPINITIALCATALOGPAGE),
    _initialCatalog(initialCatalog)
{
}

BOOL SetupInitialCatalogPage::OnSetActive()
{
    if (!_isInitialized)
    {
        GetDlgItem(IDC_INITIALCATALOG)->SetWindowTextW(_initialCatalog.c_str());
        _isInitialized = true;
    }
    CPropertySheet* sheet = static_cast<CPropertySheet*>(GetParent());
    sheet->SetWizardButtons(PSWIZB_NEXT | PSWIZB_BACK | PSWIZB_CANCEL);

    return CPropertyPage::OnSetActive();
}

void SetupInitialCatalogPage::DoDataExchange(CDataExchange* pDX)
{
    CPropertyPage::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(SetupInitialCatalogPage, CPropertyPage)
END_MESSAGE_MAP()
