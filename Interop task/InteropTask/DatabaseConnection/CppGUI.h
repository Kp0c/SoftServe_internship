#pragma once
#include "resource.h"
#include <string>
#include <gdiplus.h>
#include <memory>
#include "DatabaseConnection_i.h"
#include <atlsafe.h>
#pragma comment(lib, "Gdiplus.lib")

#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace Gdiplus;
using namespace ATL;


class ATL_NO_VTABLE CCppGUI :
    public CComObjectRootEx<CComSingleThreadModel>,
    public CComCoClass<CCppGUI, &CLSID_CppGUI>,
    public IDispatchImpl<ICppGUI, &IID_ICppGUI, &LIBID_DatabaseConnectionLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:

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

    CCppGUI()
    {
        cppGUI = this;
    }

    ~CCppGUI()
    {
        cppGUI = nullptr;
    }

    static CCppGUI* cppGUI;
    static LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);

    STDMETHOD(ShowGUI)(LONG parentHwnd, BSTR username);
private:
    const int WIDTH = 640;
    const int HEIGHT = 480;
    const int COLUMNS = 3;
    const int ROWS = 15;
    const int TITLE_ROWS = 2;
    const int TOTAL_ROWS = ROWS + TITLE_ROWS;
    const int CELL_WIDTH = WIDTH / COLUMNS;
    const int CELL_HEIGHT = HEIGHT / (TOTAL_ROWS);

    CComSafeArray<BSTR> dbData;

    int yOffset = 0;
    int wheelDelta = 0;
    HWND parentHwnd;
    
    void GetDbData(BSTR username);
    void DrawString(Graphics& graphics, std::wstring string, int x1, int y1, int width, int height);
    void DrawDbInfo(Graphics& graphics);
    void DrawGridWithTitles(Graphics& graphics);
    void OnPaint(HDC hdc);

    LRESULT MyWndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
    HWND SetupWindow(std::wstring, HWND parentHwnd);
    void ShutdownWindow(HWND hwnd, std::wstring username);
};

OBJECT_ENTRY_AUTO(__uuidof(CppGUI), CCppGUI)
