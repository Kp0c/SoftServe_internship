#pragma once
#include <string>
#include "SetupInitialCatalogPage.h"
#include "SetupDataSourcePage.h"
#include <map>

#import "msado15.dll" rename_namespace("ADO") rename("EOF", "EndOfFile") no_implementation

class ValidationPage : public CPropertyPage
{
    DECLARE_DYNAMIC(ValidationPage)

public:
    ValidationPage(SetupDataSourcePage& dataSourcePage, SetupInitialCatalogPage& initialCatalogPage);
    virtual ~ValidationPage() = default;

// Dialog Data
#ifdef AFX_DESIGN_TIME
    enum { IDD = IDD_VALIDATIONPAGE };
#endif

protected:
    void DoDataExchange(CDataExchange* pDX) override;    // DDX/DDV support
    BOOL OnSetActive() override;

    DECLARE_MESSAGE_MAP()
private:
    SetupDataSourcePage& dataSourcePage;
    SetupInitialCatalogPage& initialCatalogPage;

    std::wstring BuildConnectionString() const;
    std::wstring GetDataSource() const;
    std::wstring GetInitialCatalog() const;

    static bool TryConnect(std::wstring connectionString);
    static void SaveSettings(std::map<std::wstring, std::wstring> settings);
};
