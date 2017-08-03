// CppGUI.h : Declaration of the CCppGUI

#pragma once
#include "resource.h"       // main symbols
#include <string>
#include <gdiplus.h>
#include <atlsafe.h>
#pragma comment(lib, "Gdiplus.lib")

#include "DatabaseConnection_i.h"
#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace Gdiplus;
using namespace ATL;

constexpr int WIDTH = 639;
constexpr int HEIGHT = 480;
constexpr int COLUMNS = 3;
constexpr int ROWS = 15;
constexpr int TITLE_ROWS = 3;
constexpr int TOTAL_ROWS = ROWS + TITLE_ROWS;
constexpr int CELL_WIDTH = WIDTH / COLUMNS;
constexpr int CELL_HEIGHT = HEIGHT / (TOTAL_ROWS);

class ATL_NO_VTABLE CCppGUI :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CCppGUI, &CLSID_CppGUI>,
	public IDispatchImpl<ICppGUI, &IID_ICppGUI, &LIBID_DatabaseConnectionLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:
	CCppGUI()
	{
       cppGUI = this;
	}

    ~CCppGUI()
    {
        cppGUI = nullptr;
    }

DECLARE_REGISTRY_RESOURCEID(IDR_CPPGUI)


BEGIN_COM_MAP(CCppGUI)
	COM_INTERFACE_ENTRY(ICppGUI)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()

	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:
    static CCppGUI* cppGUI;
    static LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);

    STDMETHOD(ShowGUI)(LONG parentHwnd, BSTR username);
private:
    int yOffset = 0;
    CComSafeArray<BSTR> dbData;

    LRESULT MyWndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
    void GetDbData(BSTR username);
    void DrawString(Graphics& graphics, std::wstring string, int x1, int y1, int x2, int y2);
    void ShutdownWindow(HWND hwnd, std::wstring username);
    HWND SetupWindow(std::wstring, HWND parentHwnd);
    void OnPaint(HDC hdc);
};

OBJECT_ENTRY_AUTO(__uuidof(CppGUI), CCppGUI)
