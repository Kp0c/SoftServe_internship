#include <iostream>
#include "Header.h"

#import "msado15.dll" rename_namespace("ADO") rename("EOF", "EndOfFile") implementation_only


ADO::Connection15Ptr connection = NULL;

int main()
{
    CoInitialize(NULL);

    connection.CreateInstance(__uuidof(ADO::Connection));

    connection->Open(L"Provider=sqloleDb;Data Source=NETDAAN;Initial Catalog=LastTask;Trusted_Connection=yes;", L"", L"", -1);
   
    ADO::Recordset15Ptr record;
    record.CreateInstance(__uuidof(ADO::Recordset));

    BSTR username = SysAllocString(L"1");
    BSTR password = SysAllocString(L"1");

    record = connection->Execute(L"SELECT username FROM Users WHERE username='" + (bstr_t)username + "' AND password='" + (bstr_t)password + "'", nullptr, 1);

    //ADO::Field15Ptr user;

    if (record->EndOfFile)
        std::cout << "bad";
    else
        std::cout << "good";

  /*  while (!record->EndOfFile)
    {
        std::cout << record->Fields->GetItem("username");
        record->MoveNext();
    }*/

    return 0;
}