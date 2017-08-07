#pragma once
#include <string>

class SetupDataSourcePage : public CPropertyPage
{
    DECLARE_DYNAMIC(SetupDataSourcePage)

public:
    SetupDataSourcePage(std::wstring& dataSource);
    virtual ~SetupDataSourcePage() = default;

// Dialog Data
#ifdef AFX_DESIGN_TIME
    enum { IDD = IDD_SETUP_DATASOURCE_PAGE };
#endif

protected:
    void DoDataExchange(CDataExchange* pDX) override;    // DDX/DDV support
    BOOL OnSetActive() override;

    DECLARE_MESSAGE_MAP()
private:
    std::wstring dataSource;
};
