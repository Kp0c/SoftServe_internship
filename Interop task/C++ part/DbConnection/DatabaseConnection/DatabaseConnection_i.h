

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Tue Aug 01 11:08:21 2017
 */
/* Compiler settings for DatabaseConnection.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.00.0603 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __DatabaseConnection_i_h__
#define __DatabaseConnection_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IDbCon_FWD_DEFINED__
#define __IDbCon_FWD_DEFINED__
typedef interface IDbCon IDbCon;

#endif 	/* __IDbCon_FWD_DEFINED__ */


#ifndef __DbCon_FWD_DEFINED__
#define __DbCon_FWD_DEFINED__

#ifdef __cplusplus
typedef class DbCon DbCon;
#else
typedef struct DbCon DbCon;
#endif /* __cplusplus */

#endif 	/* __DbCon_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IDbCon_INTERFACE_DEFINED__
#define __IDbCon_INTERFACE_DEFINED__

/* interface IDbCon */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IDbCon;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("B95AA554-8DEF-4CC3-B2D9-CA5AC8ED5BCA")
    IDbCon : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE AddUser( 
            /* [in] */ BSTR username,
            /* [in] */ BSTR password) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IDbConVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IDbCon * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IDbCon * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IDbCon * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IDbCon * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IDbCon * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IDbCon * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IDbCon * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *AddUser )( 
            IDbCon * This,
            /* [in] */ BSTR username,
            /* [in] */ BSTR password);
        
        END_INTERFACE
    } IDbConVtbl;

    interface IDbCon
    {
        CONST_VTBL struct IDbConVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IDbCon_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IDbCon_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IDbCon_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IDbCon_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IDbCon_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IDbCon_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IDbCon_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IDbCon_AddUser(This,username,password)	\
    ( (This)->lpVtbl -> AddUser(This,username,password) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IDbCon_INTERFACE_DEFINED__ */



#ifndef __DatabaseConnectionLib_LIBRARY_DEFINED__
#define __DatabaseConnectionLib_LIBRARY_DEFINED__

/* library DatabaseConnectionLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_DatabaseConnectionLib;

EXTERN_C const CLSID CLSID_DbCon;

#ifdef __cplusplus

class DECLSPEC_UUID("B6E08CA7-27B6-4961-B983-7841A664B88E")
DbCon;
#endif
#endif /* __DatabaseConnectionLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


