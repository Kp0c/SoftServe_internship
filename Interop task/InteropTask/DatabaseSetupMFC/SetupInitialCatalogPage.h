#pragma once
#include <string>

class SetupInitialCatalogPage : public CPropertyPage
{
    DECLARE_DYNAMIC(SetupInitialCatalogPage)

public:
    SetupInitialCatalogPage(std::wstring& initialCatalog);
    virtual ~SetupInitialCatalogPage() = default;

// Dialog Data
#ifdef AFX_DESIGN_TIME
    enum { IDD = IDD_SETUPINITIALCATALOGPAGE };
#endif

protected:
    void DoDataExchange(CDataExchange* pDX) override;    // DDX/DDV support
    BOOL OnSetActive() override;

    DECLARE_MESSAGE_MAP()

private:
    bool _isInitialized = false;
    std::wstring _initialCatalog;
};
