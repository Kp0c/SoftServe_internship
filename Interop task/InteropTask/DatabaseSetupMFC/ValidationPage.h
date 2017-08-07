#pragma once
#include <string>
#include "SetupInitialCatalogPage.h"
#include "SetupDataSourcePage.h"
#include <map>
#include <vector>

#import "msado15.dll" rename_namespace("ADO") rename("EOF", "EndOfFile") no_implementation

// ValidationPage dialog

class ValidationPage : public CPropertyPage
{
	DECLARE_DYNAMIC(ValidationPage)

public:
    ValidationPage(SetupDataSourcePage* dataSourcePage, SetupInitialCatalogPage* initialCatalogPage);
	virtual ~ValidationPage();

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_VALIDATIONPAGE };
#endif

protected:
    void DoDataExchange(CDataExchange* pDX) override;    // DDX/DDV support

    BOOL OnSetActive() override;

	DECLARE_MESSAGE_MAP()
private:
    SetupDataSourcePage* dataSourcePage;
    SetupInitialCatalogPage* initialCatalogPage;

    std::wstring BuildConnectionString() const;
    static bool TryConnect(std::wstring connectionString);
    static void SaveSettings(std::map<std::wstring, std::wstring> settings);
};
