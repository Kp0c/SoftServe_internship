#pragma once
#include "resource.h"       // main symbols
#include "DatabaseConnection_i.h"

#include <map>
#include <vector>
#include <string>

#import "msado15.dll" rename_namespace("ADO") rename("EOF", "EndOfFile") no_implementation

#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;

class ATL_NO_VTABLE CDbCon :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CDbCon, &CLSID_DbCon>,
	public IDispatchImpl<IDbCon, &IID_IDbCon, &LIBID_DatabaseConnectionLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
private:
    ADO::Connection15Ptr connection = NULL;

public:
    CDbCon();
    ~CDbCon();

DECLARE_REGISTRY_RESOURCEID(IDR_DBCON)

BEGIN_COM_MAP(CDbCon)
	COM_INTERFACE_ENTRY(IDbCon)
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
    STDMETHOD(AddUser)(BSTR username, BSTR password);
    STDMETHOD(TryLogIn)(BSTR username, BSTR password, VARIANT_BOOL* isSuccess);
    STDMETHOD(SendMoney)(BSTR from, BSTR to, LONG count);
    STDMETHOD(DeleteUser)(BSTR username);
    STDMETHOD(GetTransactions)(BSTR username, VARIANT* transactions);

private:
    std::map<std::wstring, std::wstring> GetSettings(std::vector<std::wstring> settingsName);
};

OBJECT_ENTRY_AUTO(__uuidof(DbCon), CDbCon)
