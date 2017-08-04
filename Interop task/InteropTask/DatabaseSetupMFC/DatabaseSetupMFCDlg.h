
// DatabaseSetupMFCDlg.h : header file
//

#pragma once

#include <string>
#include <map>
#include <vector>

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
    std::wstring BuildConnectionString();
    afx_msg void OnBnClickedOk();
private:
    bool TryConnect(std::wstring connectionString);
    std::map<std::wstring, std::wstring> GetSettings(std::vector<std::wstring> settingsName);
    void SaveSettings(std::map<std::wstring, std::wstring> settings);
};
