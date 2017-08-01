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
    connection->Execute(L"INSERT INTO Users VALUES('" + (_bstr_t)username + "', '" + (_bstr_t)password + "');", nullptr, 1);

    return S_OK;
}
