// SetupDatabaseSheet.cpp : implementation file
//

#include "stdafx.h"
#include "DatabaseSetupMFC.h"
#include "SetupDatabaseSheet.h"
#include "SetupInitialCatalogPage.h"
#include "SetupDataSourcePage.h"


// SetupDatabaseSheet

IMPLEMENT_DYNAMIC(SetupDatabaseSheet, CPropertySheet)

SetupDatabaseSheet::SetupDatabaseSheet(UINT nIDCaption, CWnd* pParentWnd, UINT iSelectPage)
	:CPropertySheet(nIDCaption, pParentWnd, iSelectPage)
{
}

SetupDatabaseSheet::SetupDatabaseSheet(LPCTSTR pszCaption, CWnd* pParentWnd, UINT iSelectPage)
	:CPropertySheet(pszCaption, pParentWnd, iSelectPage)
{
}

SetupDatabaseSheet::~SetupDatabaseSheet()
{
}


BEGIN_MESSAGE_MAP(SetupDatabaseSheet, CPropertySheet)
END_MESSAGE_MAP()


// SetupDatabaseSheet message handlers
