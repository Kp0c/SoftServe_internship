// CppGUI.cpp : Implementation of CCppGUI

#include "stdafx.h"
#include "CppGUI.h"

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

    dbData = transactions.parray;

    CoUninitialize();
}

void CCppGUI::DrawString(Graphics& graphics, std::wstring string, int x1, int y1, int x2, int y2)
{
    Gdiplus::Font myFont(L"Arial", 12); 
    SolidBrush blackBrush(Color(255, 0, 0, 0));
    StringFormat format;
    RectF layoutRect(x1, y1, x2 - x1, y2 - y1);
    format.SetAlignment(StringAlignmentCenter);

    graphics.DrawString(
        string.c_str(),
        string.length(),
        &myFont,
        layoutRect,
        &format,
        &blackBrush);
}

void CCppGUI::OnPaint(HDC hdc)
{
    //Graphics graphics(hdc);

    Bitmap bitmap(WIDTH, HEIGHT);
    Graphics* graphics = Graphics::FromImage(&bitmap);
    graphics->Clear(Color::White);

    graphics->SetCompositingQuality(CompositingQualityHighSpeed);
    graphics->SetPixelOffsetMode(PixelOffsetModeNone);
    graphics->SetSmoothingMode(SmoothingModeNone);
    graphics->SetInterpolationMode(InterpolationModeDefault);

    Pen pen(Color(255, 0, 0, 0));

    LONG indexes[2];

    int columns = dbData.GetCount(1);
    int rows = dbData.GetCount(0);

    for (int i = 0; i < columns; i++)
    {
        for (int j = 1; j < rows + 1; j++)
        {
            indexes[0] = j - 1;
            indexes[1] = i;
            BSTR str;
            dbData.MultiDimGetAt(indexes, str);

            int x = i*CELL_WIDTH;
            int y = j*CELL_HEIGHT + yOffset;

            if (y < CELL_HEIGHT)
            {
                continue;
            }
            DrawString(*graphics, std::wstring(str, SysStringLen(str)), x, y, x + CELL_WIDTH, y + CELL_HEIGHT);
        }
    }

    std::wstring titles[]{ L"From", L"To", L"Count" };
    for (int i = 0; i < COLUMNS; ++i)
    {
        graphics->DrawLine(&pen, i*CELL_WIDTH, 0, i*CELL_WIDTH, HEIGHT);
        DrawString(*graphics, titles[i], i*CELL_WIDTH, 0, i*CELL_WIDTH + CELL_WIDTH, CELL_HEIGHT);
    }

    for (int j = 0; j < TOTAL_ROWS; ++j)
    {
        graphics->DrawLine(&pen, 0, j*CELL_HEIGHT, WIDTH, j*CELL_HEIGHT);
    }

    Graphics gra(hdc); 
    gra.DrawImage(&bitmap, 0, 0);
}

LRESULT CALLBACK CCppGUI::WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    return cppGUI->MyWndProc(hwnd, msg, wParam, lParam);
}

LRESULT CCppGUI::MyWndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
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
            yOffset += CELL_HEIGHT;

            if (yOffset > 0)
            {
                yOffset = 0;
            }
            else
            {
                InvalidateRgn(hwnd, NULL, FALSE);
            }
        }
        for (; wheelDelta < 0; wheelDelta += WHEEL_DELTA)
        {
            yOffset -= CELL_HEIGHT;
            if (abs(yOffset) >((int)dbData.GetCount(0) - ROWS)*CELL_HEIGHT)
            {
                yOffset = -CELL_HEIGHT*(dbData.GetCount(0) - ROWS);
            }
            else
            {
                InvalidateRgn(hwnd, NULL, FALSE);
            }
        }
        break;
    default:
        return DefWindowProc(hwnd, msg, wParam, lParam);
    }
    return 0;
}

STDMETHODIMP CCppGUI::ShowGUI(BSTR username)
{
    GdiplusStartupInput gdiplusStartupInput;
    ULONG_PTR gdiplusToken;
    GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, nullptr);

    HWND hwnd = SetupWindow(username);

    if (hwnd == 0)
        return S_FALSE;

    GetDbData(username);

    ShowWindow(hwnd, SW_SHOWNORMAL);
    UpdateWindow(hwnd);

    MSG message;
    while (GetMessage(&message, NULL, 0, 0) > 0)
    {
        TranslateMessage(&message);
        DispatchMessage(&message);
    }
    GdiplusShutdown(gdiplusToken);    

    return S_OK;
}

HWND CCppGUI::SetupWindow(std::wstring username)
{
    WNDCLASSEX wc;
    HWND hwnd;
   
    wc.cbSize = sizeof(WNDCLASSEX);
    wc.lpfnWndProc = WndProc;
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
        WS_OVERLAPPED | WS_SYSMENU,
        CW_USEDEFAULT, CW_USEDEFAULT, WIDTH, HEIGHT,
        NULL, NULL, NULL, NULL);

    if (hwnd == NULL)
    {
        MessageBox(NULL, L"Window Creation Failed!", L"Error!",
            MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    return hwnd;
}
