// DbCon.cpp : Implementation of CDbCon

#include "stdafx.h"
#include "DbCon.h"

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

    VARIANT* recordsAffected;
    record = connection->Execute(L"SELECT username FROM Users WHERE username='" + (bstr_t)username + "' AND password='" + (bstr_t)password + "'", nullptr, 1);

    if (!record->EndOfFile)
        *isSuccess = VARIANT_TRUE;

    record.Release();
    return S_OK;
}
