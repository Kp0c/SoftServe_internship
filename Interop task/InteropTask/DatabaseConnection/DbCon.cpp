// DbCon.cpp : Implementation of CDbCon

#include "stdafx.h"
#include "DbCon.h"
#include <atlsafe.h>
#include <string>

#import "msado15.dll" rename_namespace("ADO") rename("EOF", "EndOfFile") implementation_only

// CDbCon

std::map<std::wstring, std::wstring> CDbCon::GetSettings(std::vector<std::wstring> settingsName)
{
    std::map<std::wstring, std::wstring> settings;
    HKEY hkey;
    if (RegCreateKeyEx(HKEY_CURRENT_USER, L"Software\\VB and VBA Program Settings\\LastTask\\Database", 0, nullptr, REG_OPTION_VOLATILE, KEY_READ | KEY_WOW64_32KEY,
        nullptr, &hkey, nullptr) == ERROR_SUCCESS)
    {
        for (std::wstring setting : settingsName)
        {
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

CDbCon::CDbCon()
{
    CoInitialize(NULL);

    connection.CreateInstance(__uuidof(ADO::Connection));

    std::vector<std::wstring> settingsName{ L"Data Source", L"Initial Catalog", L"Trusted_Connection" };

    std::map<std::wstring, std::wstring> settings = GetSettings(settingsName);

    std::wstring connectionString(L"Provider=sqloleDb;");

    for (auto setting : settings)
    {
        connectionString += setting.first + L"=" + setting.second + L";";
    }
    connection->Open(connectionString.c_str(), L"", L"", -1);
}

CDbCon::~CDbCon()
{
    connection.Release();
    CoUninitialize();
}

STDMETHODIMP CDbCon::AddUser(BSTR username, BSTR password)
{
    connection->Execute(L"INSERT INTO Users VALUES('" + (bstr_t)username + "', '" + (bstr_t)password + "')", nullptr, 1);

    return S_OK;
}

STDMETHODIMP CDbCon::TryLogIn(BSTR username, BSTR password, VARIANT_BOOL* isSuccess)
{
    *isSuccess = VARIANT_FALSE;

    ADO::Recordset15Ptr record;
    record.CreateInstance(__uuidof(ADO::Recordset));

    record = connection->Execute(L"SELECT username FROM Users WHERE username='" + (bstr_t)username + "' AND password='" + (bstr_t)password + "'", nullptr, 1);

    if (!record->EndOfFile)
        *isSuccess = VARIANT_TRUE;

    record.Release();
    return S_OK;
}

STDMETHODIMP CDbCon::SendMoney(BSTR from, BSTR to, LONG count)
{
    std::wstring moneyCountString = std::to_wstring(count);

    connection->Execute(L"EXECUTE make_transaction '" + (bstr_t)from + "', '" + (bstr_t)to + "', " + SysAllocString(moneyCountString.c_str()) + "", nullptr, 1);

    return S_OK;
}


STDMETHODIMP CDbCon::DeleteUser(BSTR username)
{
    connection->Execute(L"DELETE FROM Users WHERE username='" + (bstr_t)username + "'", nullptr, 1);

    return S_OK;
}

STDMETHODIMP CDbCon::GetTransactions(BSTR username, VARIANT* transactions)
{
    ADO::Recordset15Ptr record;
    record.CreateInstance(__uuidof(ADO::Recordset));

    record = connection->Execute(L"SELECT debitUser, creditUser, #sum FROM Transactions WHERE debitUser='" + (bstr_t)username + "' OR creditUser='" + (bstr_t)username + "'", nullptr, 1);

    int elementsCount = 0;

    while (!record->EndOfFile)
    {
        elementsCount++;
        record->MoveNext();
    }


    CComSafeArrayBound bound[2];
    bound[0].SetCount(elementsCount);
    bound[1].SetCount(3);
     
    CComSafeArray<BSTR> trans(bound, 2);

    if (elementsCount != 0)
    {
        record->MoveFirst();

        LONG indexes[2];

        for (int i = 0; i < elementsCount; ++i)
        {
            indexes[0] = i;
            indexes[1] = 0;

            trans.MultiDimSetAt(indexes, record->Fields->GetItem("debitUser")->Value.bstrVal);
            indexes[1] = 1;
            trans.MultiDimSetAt(indexes, record->Fields->GetItem("creditUser")->Value.bstrVal);
            indexes[1] = 2;
            std::wstring money = std::to_wstring(record->Fields->GetItem("#sum")->Value.lVal);
            trans.MultiDimSetAt(indexes, SysAllocString(money.c_str()));
            record->MoveNext();
        }
    }

    transactions->parray = trans.Detach();

    return S_OK;
}
