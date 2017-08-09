#include "stdafx.h"
#include "DbCon.h"
#include "InteropFunctions.h"
#include <string>

#import "msado15.dll" rename_namespace("ADO") rename("EOF", "EndOfFile") implementation_only

std::wstring CDbCon::GetConnectionString()
{
    std::vector<std::wstring> settingNames{ L"Data Source", L"Initial Catalog", L"Trusted_Connection" };

    auto settings = GetSettings(settingNames);
    std::wstring connectionString(L"Provider=sqloleDb;");

    for (auto setting : settings)
    {
        connectionString += setting.first + L"=" + setting.second + L";";
    }

    return connectionString;
}

CDbCon::CDbCon()
{
    CoInitialize(NULL);

    _connection.CreateInstance(__uuidof(ADO::Connection));

    try
    {
        _connection->Open(GetConnectionString().c_str(), L"", L"", ADO::ConnectOptionEnum::adConnectUnspecified);
    }
    catch(...)
    {
        SetupDatabase();
        _connection->Open(GetConnectionString().c_str(), L"", L"", ADO::ConnectOptionEnum::adConnectUnspecified);
    }
}

CDbCon::~CDbCon()
{
    _connection.Release();
    CoUninitialize();
}

std::map<std::wstring, std::wstring> CDbCon::GetSettings(std::vector<std::wstring> settingNames)
{
    std::map<std::wstring, std::wstring> settings;
    HKEY hkey;
    if (RegCreateKeyEx(HKEY_CURRENT_USER, settingsLocation, 0, nullptr, REG_OPTION_VOLATILE, KEY_READ | KEY_WOW64_32KEY, nullptr, &hkey, nullptr) == ERROR_SUCCESS)
    {
        for (std::wstring setting : settingNames)
        {
            TCHAR data[255];
            DWORD buff = sizeof(data);

            if (RegGetValue(hkey, nullptr, setting.c_str(), RRF_RT_REG_SZ, nullptr, data, &buff) == ERROR_SUCCESS)
            {
                settings[setting] = data;
            }
        }

        RegCloseKey(hkey);
    }

    return settings;
}

STDMETHODIMP CDbCon::AddUser(BSTR username, BSTR password)
{
    _connection->Execute(L"INSERT INTO Users VALUES('" + (bstr_t)username + "', '" + (bstr_t)password + "')", nullptr, ADO::ExecuteOptionEnum::adOptionUnspecified);

    return S_OK;
}

STDMETHODIMP CDbCon::TryLogIn(BSTR username, BSTR password, VARIANT_BOOL* isSuccess)
{
    *isSuccess = VARIANT_FALSE;

    ADO::Recordset15Ptr record;
    record.CreateInstance(__uuidof(ADO::Recordset));

    record = _connection->Execute(L"SELECT username FROM Users WHERE username='" + (bstr_t)username + "' AND password='" + (bstr_t)password + "'", nullptr, ADO::ExecuteOptionEnum::adOptionUnspecified);
    
    bool isHaveAtLeastOneRecord = !record->EndOfFile;
    if (isHaveAtLeastOneRecord)
    {
        *isSuccess = VARIANT_TRUE;
    }

    record.Release();
    return S_OK;
}

STDMETHODIMP CDbCon::SendMoney(BSTR from, BSTR to, LONG count)
{
    std::wstring moneyCountString = std::to_wstring(count);

    _connection->Execute(L"EXECUTE make_transaction '" + (bstr_t)from + "', '" + (bstr_t)to + "', " +
        SysAllocStringLen(moneyCountString.c_str(), moneyCountString.length()) + "", nullptr, ADO::ExecuteOptionEnum::adOptionUnspecified);

    return S_OK;
}


STDMETHODIMP CDbCon::DeleteUser(BSTR username)
{
    _connection->Execute(L"DELETE FROM Users WHERE username='" + (bstr_t)username + "'", nullptr, ADO::ExecuteOptionEnum::adOptionUnspecified);

    return S_OK;
}

CComSafeArray<BSTR> CDbCon::StructurizeInfo(ADO::Recordset15Ptr record)
{
    int elementsCount = 0;
    while (!record->EndOfFile)
    {
        elementsCount++;
        record->MoveNext();
    }

    CComSafeArrayBound bound[2];
    bound[0].SetCount(elementsCount);
    bound[1].SetCount(3);

    CComSafeArray<BSTR> transactions(bound, 2);

    if (elementsCount != 0)
    {
        record->MoveFirst();

        LONG indexes[2];

        for (int i = 0; i < elementsCount; ++i)
        {
            indexes[0] = i;
            indexes[1] = 0;

            transactions.MultiDimSetAt(indexes, record->Fields->GetItem("debitUser")->Value.bstrVal);
            indexes[1] = 1;
            transactions.MultiDimSetAt(indexes, record->Fields->GetItem("creditUser")->Value.bstrVal);
            indexes[1] = 2;
            std::wstring money = std::to_wstring(record->Fields->GetItem("#sum")->Value.lVal);
            transactions.MultiDimSetAt(indexes, SysAllocString(money.c_str()));
            record->MoveNext();
        }
    }

    return transactions;
}

STDMETHODIMP CDbCon::GetTransactions(BSTR username, VARIANT* transactions)
{
    ADO::Recordset15Ptr record = NULL;
    record.CreateInstance(__uuidof(ADO::Recordset));

    record = _connection->Execute(L"SELECT debitUser, creditUser, #sum FROM Transactions WHERE debitUser='" +
        (bstr_t)username + "' OR creditUser='" + (bstr_t)username + "'", nullptr, ADO::ExecuteOptionEnum::adOptionUnspecified);

    transactions->parray = StructurizeInfo(record).Detach();

    return S_OK;
}
