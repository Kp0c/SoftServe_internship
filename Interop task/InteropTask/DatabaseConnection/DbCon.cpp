// DbCon.cpp : Implementation of CDbCon

#include "stdafx.h"
#include "DbCon.h"
#include <atlsafe.h>
#include <string>

#import "msado15.dll" rename_namespace("ADO") rename("EOF", "EndOfFile") implementation_only

// CDbCon

CDbCon::CDbCon()
{
    CoInitialize(NULL);

    connection.CreateInstance(__uuidof(ADO::Connection));

    connection->Open(L"Provider=sqloleDb;Data Source=NETDAAN;Initial Catalog=LastTask;Trusted_Connection=yes;", L"", L"", -1);
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

    if (elementsCount != 0)
    {
        record->MoveFirst();
    }

    CComSafeArrayBound bound[2];
    bound[0].SetCount(elementsCount);
    bound[1].SetCount(3);
     
    CComSafeArray<BSTR> trans(bound, 2);

    LONG indexes[2];

    for(int i = 0; i < elementsCount; ++i)
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

    transactions->parray = trans.Detach();

    return S_OK;
}
