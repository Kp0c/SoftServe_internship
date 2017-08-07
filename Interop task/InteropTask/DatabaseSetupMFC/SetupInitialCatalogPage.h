#pragma once
#include <string>


// SetupInitialCatalogPage dialog

class SetupInitialCatalogPage : public CPropertyPage
{
	DECLARE_DYNAMIC(SetupInitialCatalogPage)

public:
	SetupInitialCatalogPage(std::wstring initialCatalog);
	virtual ~SetupInitialCatalogPage();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_SETUPINITIALCATALOGPAGE };
#endif

protected:
    void DoDataExchange(CDataExchange* pDX) override;    // DDX/DDV support

    BOOL OnSetActive() override;

	DECLARE_MESSAGE_MAP()

private:
    std::wstring initialCatalog;
};
