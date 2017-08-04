#pragma once

#include <map>
#include <vector>

#import "msado15.dll" rename_namespace("ADO") rename("EOF", "EndOfFile") no_implementation

class CDatabaseSetupMFCDlg : public CDialogEx
{
// Construction
public:
    CDatabaseSetupMFCDlg(CWnd* pParent = nullptr);

// Dialog Data
#ifdef AFX_DESIGN_TIME
    enum { IDD = IDD_DATABASESETUPMFC_DIALOG };
#endif

protected:
    void DoDataExchange(CDataExchange* pDX) override;   // DDX/DDV support

// Implementation
protected:
    HICON m_hIcon;

    // Generated message map functions
    BOOL OnInitDialog() override;
    afx_msg void OnPaint();
    afx_msg HCURSOR OnQueryDragIcon();
    DECLARE_MESSAGE_MAP()
public:
    afx_msg void OnBnClickedOk();
private:
    std::wstring BuildConnectionString() const;
    static bool TryConnect(std::wstring connectionString);
    static std::map<std::wstring, std::wstring> GetSettings(std::vector<std::wstring> settingsName);
    static void SaveSettings(std::map<std::wstring, std::wstring> settings);
};
