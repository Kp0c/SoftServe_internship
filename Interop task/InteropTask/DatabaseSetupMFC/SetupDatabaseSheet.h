#pragma once



// SetupDatabaseSheet

class SetupDatabaseSheet : public CPropertySheet
{
	DECLARE_DYNAMIC(SetupDatabaseSheet)

public:
	SetupDatabaseSheet(UINT nIDCaption, CWnd* pParentWnd = NULL, UINT iSelectPage = 0);
	SetupDatabaseSheet(LPCTSTR pszCaption, CWnd* pParentWnd = NULL, UINT iSelectPage = 0);
	virtual ~SetupDatabaseSheet();

protected:
	DECLARE_MESSAGE_MAP()
};


