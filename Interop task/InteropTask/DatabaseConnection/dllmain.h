#pragma once

class CDatabaseConnectionModule : public ATL::CAtlDllModuleT< CDatabaseConnectionModule >
{
public:
    DECLARE_LIBID(LIBID_DatabaseConnectionLib)
    DECLARE_REGISTRY_APPID_RESOURCEID(IDR_DATABASECONNECTION, "{6FF45AAB-1347-457F-B504-8973C45E5741}")
};

extern class CDatabaseConnectionModule _AtlModule;
