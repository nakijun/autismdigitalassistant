// FliteMFCDoc.cpp : implementation of the CFliteMFCDoc class
//

#include "stdafx.h"
#include "FliteMFC.h"

#include "FliteMFCDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CFliteMFCDoc

IMPLEMENT_DYNCREATE(CFliteMFCDoc, CDocument)

BEGIN_MESSAGE_MAP(CFliteMFCDoc, CDocument)
END_MESSAGE_MAP()

// CFliteMFCDoc construction/destruction

CFliteMFCDoc::CFliteMFCDoc()
{
	// TODO: add one-time construction code here

}

CFliteMFCDoc::~CFliteMFCDoc()
{
}

BOOL CFliteMFCDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}

// CFliteMFCDoc serialization

#ifndef _WIN32_WCE_NO_ARCHIVE_SUPPORT
void CFliteMFCDoc::Serialize(CArchive& ar)
{
	(ar);
}
#endif // !_WIN32_WCE_NO_ARCHIVE_SUPPORT


// CFliteMFCDoc diagnostics

#ifdef _DEBUG
void CFliteMFCDoc::AssertValid() const
{
	CDocument::AssertValid();
}
#endif //_DEBUG


// CFliteMFCDoc commands

