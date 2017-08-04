#pragma once
#include "resource.h"

#ifndef __AFXWIN_H__
    #error "include 'stdafx.h' before including this file for PCH"
#endif

// CDatabaseSetupMFCApp:
// See DatabaseSetupMFC.cpp for the implementation of this class
//

class CDatabaseSetupMFCApp : public CWinApp
{
public:
    CDatabaseSetupMFCApp();

// Overrides
    virtual BOOL InitInstance();

// Implementation

    DECLARE_MESSAGE_MAP()
};

extern CDatabaseSetupMFCApp theApp;
