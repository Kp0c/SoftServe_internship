#include <iostream>

#include <Windows.h>
#include <string>
#include <gdiplus.h>

#include <atlbase.h>
#include <atlsafe.h>
#include <objbase.h>
#include "DatabaseConnection_i.h"
#include "DatabaseConnection_i.c"

using namespace Gdiplus;
#pragma comment(lib, "Gdiplus.lib")

#import "D:\\Internship projects\\Interop task\\InteropTask\\DatabaseConnection\\Debug\\DatabaseConnection.tlb"

constexpr int WIDTH = 640;
constexpr int HEIGHT = 480;

constexpr int CELL_WIDTH = 213;
constexpr int CELL_HEIGHT = 24;

int additionalY = 0;

void DrawString(Graphics& graphics, std::wstring string, int x1, int y1, int x2, int y2)
{
    Font myFont(L"Arial", 12);
    RectF layoutRect(x1, y1, x2-x1, y2-y1);
    StringFormat format;
    format.SetAlignment(StringAlignmentCenter);
    SolidBrush blackBrush(Color(255, 0, 0, 0));

    graphics.DrawString(
        string.c_str(),
        string.length(),
        &myFont,
        layoutRect,
        &format,
        &blackBrush);
}

void OnPaint(HDC hdc)
{
    Graphics graphics(hdc);
    Pen pen(Color(255, 0, 0, 0));

    //db
    CoInitialize(NULL);

    IDbCon* dbCon = NULL;
    CoCreateInstance(CLSID_DbCon, NULL, CLSCTX_INPROC_SERVER, IID_IDbCon, (void**)&dbCon);

    VARIANT transactions;

    dbCon->GetTransactions(L"1", &transactions);

    CComSafeArray<BSTR> trans = transactions.parray;

    LONG indexes[2];

    for (unsigned int i = 0; i < trans.GetCount(1); i++)
    {
        for (unsigned int j = 1; j < trans.GetCount(0) + 1; j++)
        {
            indexes[0] = j - 1;
            indexes[1] = i;
            BSTR str;
            trans.MultiDimGetAt(indexes, str);
            int x = i*CELL_WIDTH;
            int y = j*CELL_HEIGHT + additionalY;
            if (y < CELL_HEIGHT) continue;
            DrawString(graphics, std::wstring(str, SysStringLen(str)), x, y, x+CELL_WIDTH, y + CELL_HEIGHT);
        }
    }

    CoUninitialize();

    std::wstring titles[]{ L"From", L"To", L"Count" };

    for (int i = 0, title = 0; title < 3; i+= CELL_WIDTH, ++title)
    {
        graphics.DrawLine(&pen, i, 0, i, HEIGHT);
        DrawString(graphics, titles[title], i, 0, i + CELL_WIDTH, CELL_HEIGHT);
    }

    for (int j = 0; j < HEIGHT; j+= CELL_HEIGHT)
    {
        graphics.DrawLine(&pen, 0, j, WIDTH, j);
    }    
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    static int wheelDelta = 0;
    HDC hdc;
    PAINTSTRUCT ps;

    switch (msg)
    {
    case WM_PAINT:
        hdc = BeginPaint(hwnd, &ps);
        OnPaint(hdc);
        EndPaint(hwnd, &ps);
        break;
    case WM_CLOSE:
        DestroyWindow(hwnd);
        break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    case WM_MOUSEWHEEL:
        wheelDelta += GET_WHEEL_DELTA_WPARAM(wParam);
        for (; wheelDelta > WHEEL_DELTA; wheelDelta -= WHEEL_DELTA)
        {
            additionalY += CELL_HEIGHT;
            InvalidateRgn(hwnd, NULL, TRUE);
        }
        for (; wheelDelta < 0; wheelDelta += WHEEL_DELTA)
        {
            additionalY -= CELL_HEIGHT;
            InvalidateRgn(hwnd, NULL, TRUE);
        }
        break;
    default:
        return DefWindowProc(hwnd, msg, wParam, lParam);
    }
    return 0;
}

#include <map>
#include <string>
#include <vector>

std::map<std::wstring, std::wstring> GetSettings(std::vector<std::wstring> settingsName)
{
    std::map<std::wstring, std::wstring> settings;
    HKEY hkey;
    if (RegCreateKeyEx(HKEY_CURRENT_USER, L"Software\\VB and VBA Program Settings\\LastTask\\Database", 0, nullptr, REG_OPTION_VOLATILE, KEY_READ | KEY_WOW64_32KEY,
        nullptr, &hkey, nullptr) == ERROR_SUCCESS)
    {
        for (std::wstring setting : settingsName)
        {
            //std::wstring data;
            TCHAR data[255];
            DWORD  buff = sizeof(data);
            if (RegGetValue(hkey, nullptr, setting.c_str(), RRF_RT_REG_SZ, nullptr, data, &buff) == ERROR_SUCCESS)
            {
                settings[setting] = data;
            }
        }

        RegCloseKey(hkey);
    }

    return settings;
}

#include <iostream>

const std::wstring initialCatalog(L"Initial Catalog");

int main()
{
    std::vector<std::wstring> settingsName{ L"Data Source", initialCatalog, L"Trusted_Connection" };

    std::map<std::wstring, std::wstring> settings = GetSettings(settingsName);

    std::wstring connectionString(L"Provider=sqloleDb;");

    for (auto setting : settings)
    {
        if (setting.first == initialCatalog)
        {
            if (setting.second == L"True")
            {
                setting.second = L"yes";
            }
            else
            {
                setting.second = L"no";
            }
        }
        connectionString += setting.first + L"=" + setting.second + L";";
    }

    std::wcout << connectionString << std::endl;

    CoInitialize(NULL);

    IDbCon* dbCon = NULL;
    CoCreateInstance(CLSID_DbCon, NULL, CLSCTX_INPROC_SERVER, IID_IDbCon, (void**)&dbCon);

    ICppGUI* gui = NULL;
    CoCreateInstance(CLSID_CppGUI, NULL, CLSCTX_INPROC_SERVER, IID_ICppGUI, (void**)&gui);

    gui->ShowGUI(NULL, L"1");

    CoUninitialize();

   /* WNDCLASSEX wc;
    HWND hwnd;
    MSG Msg;

    GdiplusStartupInput gdiplusStartupInput;
    ULONG_PTR           gdiplusToken;
    GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, nullptr);


    wc.cbSize = sizeof(WNDCLASSEX);
    wc.lpfnWndProc = WndProc;
    wc.cbClsExtra = 0;
    wc.cbWndExtra = 0;
    wc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
    wc.hCursor = LoadCursor(NULL, IDC_ARROW);
    wc.hbrBackground = (HBRUSH)(COLOR_WINDOWFRAME);
    wc.lpszMenuName = NULL;
    wc.lpszClassName = L"InteropTaskWindow";
    wc.hIconSm = LoadIcon(NULL, IDI_APPLICATION);

    if (!RegisterClassEx(&wc))
    {
        MessageBox(NULL, L"Window registration failed!", L"Error!",
            MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    std::wstring title = L"InteropTask - " + std::wstring(L"TODO: username");
    hwnd = CreateWindowEx(
        WS_EX_CLIENTEDGE,
        L"InteropTaskWindow",
        title.c_str(),
        WS_OVERLAPPED | WS_SYSMENU,
        CW_USEDEFAULT, CW_USEDEFAULT, 640, 480,
        NULL, NULL, NULL, NULL);

    if (hwnd == NULL)
    {
        MessageBox(NULL, L"Window Creation Failed!", L"Error!",
            MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    ShowWindow(hwnd, SW_SHOWNORMAL);
   // ??? UpdateWindow(hwnd);

    while (GetMessage(&Msg, NULL, 0, 0) > 0)
    {
        TranslateMessage(&Msg);
        DispatchMessage(&Msg);
    }

    GdiplusShutdown(gdiplusToken);
    */
    return 0;
}