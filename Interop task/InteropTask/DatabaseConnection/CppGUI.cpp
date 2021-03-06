#include "stdafx.h"
#include "CppGUI.h"
#include "InteropFunctions.h"
#include <Windows.h>
#include "DbCon.h"

CCppGUI* CCppGUI::cppGUI = nullptr;

void CCppGUI::GetDbData(BSTR username)
{
    CoInitialize(NULL);

    IDbCon* dbCon = NULL;
    CoCreateInstance(CLSID_DbCon, NULL, CLSCTX_INPROC_SERVER, IID_IDbCon, (void**)&dbCon);

    VARIANT transactions;
    dbCon->GetTransactions(username, &transactions);

    dbCon->Release();
    CoUninitialize();

    dbData = transactions.parray;
}

void CCppGUI::DrawString(Graphics& graphics, std::wstring string, int x1, int y1, int width, int height)
{
    SolidBrush blackBrush(Color(255, 0, 0, 0));
    Gdiplus::Font font(L"Arial", 12);
    StringFormat format;
    format.SetAlignment(StringAlignmentCenter);
    
    RectF layoutRect(x1, y1, width, height);

    graphics.DrawString(
        string.c_str(),
        string.length(),
        &font,
        layoutRect,
        &format,
        &blackBrush);
}

void CCppGUI::DrawDbInfo(Graphics& graphics)
{
    int columns = dbData.GetCount(1);
    int rows = dbData.GetCount(0);

    int cellsPassed = abs(_yOffset) / CELL_HEIGHT + 1;
    int maxRows = min(cellsPassed + TOTAL_ROWS, rows + 1);

    LONG indexes[2];
    for (int i = 0; i < columns; i++)
    {
        indexes[1] = i;
        for (int j = cellsPassed; j <= rows; j++)
        {
            indexes[0] = j - 1;

            BSTR str;
            dbData.MultiDimGetAt(indexes, str);

            int x = i*CELL_WIDTH;
            int y = j*CELL_HEIGHT + _yOffset;

            bool isFirstRow = y < CELL_HEIGHT;
            if (isFirstRow)
            {
                continue;
            }

            DrawString(graphics, std::wstring(str, SysStringLen(str)), x, y, CELL_WIDTH, CELL_HEIGHT);
        }
    }
}

void CCppGUI::DrawGridWithTitles(Graphics& graphics)
{
    Pen pen(Color(255, 0, 0, 0));
    std::wstring titles[]{ L"From", L"To", L"Count" };
    for (int i = 0; i < COLUMNS; ++i)
    {
        graphics.DrawLine(&pen, i*CELL_WIDTH, 0, i*CELL_WIDTH, HEIGHT);
        DrawString(graphics, titles[i], i*CELL_WIDTH, 0, CELL_WIDTH, CELL_HEIGHT);
    }

    for (int j = 0; j < TOTAL_ROWS; ++j)
    {
        graphics.DrawLine(&pen, 0, j*CELL_HEIGHT, WIDTH, j*CELL_HEIGHT);
    }
}

void CCppGUI::OnPaint(HDC hdc)
{
    Bitmap bitmap(WIDTH, HEIGHT);
    Graphics* graphics = Graphics::FromImage(&bitmap);
    graphics->Clear(Color::White);

    DrawGridWithTitles(*graphics);
    DrawDbInfo(*graphics);
   
    Graphics screen(hdc); 
    screen.DrawImage(&bitmap, 0, 0);
}

LRESULT CALLBACK CCppGUI::WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    return cppGUI->MyWndProc(hwnd, msg, wParam, lParam);
}

LRESULT CCppGUI::MyWndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg)
    {
    case WM_PAINT:
        HDC hdc;
        PAINTSTRUCT ps;

        hdc = BeginPaint(hwnd, &ps);
        OnPaint(hdc);
        EndPaint(hwnd, &ps);
        break;
    case WM_CLOSE:
        EnableWindow(_parentHwnd, TRUE);
        DestroyWindow(hwnd);
        break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    case WM_MOUSEWHEEL:
        _wheelDelta += GET_WHEEL_DELTA_WPARAM(wParam);

        int dataRows;
        if (dbData != nullptr)
        {
            dataRows = dbData.GetCount(0);
        }

        if (dataRows > ROWS)
        {
            for (; _wheelDelta < 0; _wheelDelta += WHEEL_DELTA)
            {
                _yOffset -= CELL_HEIGHT;

                if (abs(_yOffset) > (dataRows - ROWS)*CELL_HEIGHT)
                {
                    _yOffset = -CELL_HEIGHT*(dataRows - ROWS);
                }
            }

            for (; _wheelDelta > WHEEL_DELTA; _wheelDelta -= WHEEL_DELTA)
            {
                _yOffset += CELL_HEIGHT;

                if (_yOffset > 0)
                {
                    _yOffset = 0;
                }
            }
        }
        else
        {
            _wheelDelta = 0;
        }

        InvalidateRgn(hwnd, NULL, FALSE);
        break;
    default:
        return DefWindowProc(hwnd, msg, wParam, lParam);
    }
    return 0;
}

STDMETHODIMP CCppGUI::ShowGUI(LONG _parentHwnd, BSTR username)
{
    this->_parentHwnd = (HWND)_parentHwnd;

    GdiplusStartupInput gdiplusStartupInput;
    ULONG_PTR gdiplusToken;
    GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, nullptr);

    HWND hwnd = SetupWindow(username, (HWND)_parentHwnd);
    
    int returnCode = S_FALSE;
    if (hwnd != 0)
    {
        GetDbData(username);

        ShowWindow(hwnd, SW_SHOWNORMAL);

        MSG message;
        while (GetMessage(&message, NULL, 0, 0) > 0)
        {
            if (message.message == WM_HOTKEY)
            {
                AddNewTransaction(username);
                GetDbData(username);
                InvalidateRgn(hwnd, NULL, FALSE);
            }

            TranslateMessage(&message);
            DispatchMessage(&message);
        }

        GdiplusShutdown(gdiplusToken);

        returnCode = S_OK;
    }

    ShutdownWindow(hwnd, username);

    return returnCode;
}

HWND CCppGUI::SetupWindow(std::wstring username, HWND _parentHwnd)
{
    WNDCLASSEX wc;
    HWND hwnd;
   
    wc.cbSize = sizeof(WNDCLASSEX);
    wc.lpfnWndProc = WndProc;
    wc.style = CS_HREDRAW | CS_VREDRAW;
    wc.cbClsExtra = 0;
    wc.cbWndExtra = 0;
    wc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
    wc.hCursor = LoadCursor(NULL, IDC_ARROW);
    wc.hbrBackground = (HBRUSH)(COLOR_WINDOWFRAME);
    wc.lpszMenuName = NULL;
    wc.lpszClassName = username.c_str();
    wc.hIconSm = LoadIcon(NULL, IDI_APPLICATION);

    if (!RegisterClassEx(&wc))
    {
        MessageBox(NULL, (L"Window registration failed! Error code: " + std::to_wstring(GetLastError())).c_str(), L"Error!",
            MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    std::wstring title = L"InteropTask - " + username;
    hwnd = CreateWindowEx(
        WS_EX_CLIENTEDGE,
        username.c_str(),
        title.c_str(),
        WS_OVERLAPPED | WS_OVERLAPPED | WS_SYSMENU,
        CW_USEDEFAULT, CW_USEDEFAULT, WIDTH, HEIGHT,
        _parentHwnd, NULL, NULL, NULL);

    if (hwnd == NULL)
    {
        MessageBox(NULL, (L"Window creation failed! Error code: " + std::to_wstring(GetLastError())).c_str(), L"Error!",
            MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    if (!RegisterHotKey(NULL, 1, MOD_CONTROL | MOD_NOREPEAT, 0x54))
    {
        MessageBox(NULL, L"Hotkey registering failed!", L"Error!",
            MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    EnableWindow(_parentHwnd, FALSE);

    return hwnd;
}

void CCppGUI::ShutdownWindow(HWND hwnd, std::wstring username)
{
    UnregisterClass(username.c_str(), NULL);
    UnregisterHotKey(NULL , 1);
}
