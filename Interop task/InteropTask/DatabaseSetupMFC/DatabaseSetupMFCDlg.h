
// DatabaseSetupMFCDlg.h : header file
//

#pragma once

#include <string>

#import "msado15.dll" rename_namespace("ADO") rename("EOF", "EndOfFile") no_implementation

// CDatabaseSetupMFCDlg dialog
class CDatabaseSetupMFCDlg : public CDialogEx
{
// Construction
public:
	CDatabaseSetupMFCDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DATABASESETUPMFC_DIALOG };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
    bool TryConnect(std::wstring connectionString);
    std::wstring BuildConnectionString();
    afx_msg void OnBnClickedOk();
};
