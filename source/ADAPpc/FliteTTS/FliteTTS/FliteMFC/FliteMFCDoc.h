// FliteMFCDoc.h : interface of the CFliteMFCDoc class
//


#pragma once

class CFliteMFCDoc : public CDocument
{
protected: // create from serialization only
	CFliteMFCDoc();
	DECLARE_DYNCREATE(CFliteMFCDoc)

// Attributes
public:

// Operations
public:

// Overrides
public:
	virtual BOOL OnNewDocument();
#ifndef _WIN32_WCE_NO_ARCHIVE_SUPPORT
	virtual void Serialize(CArchive& ar);
#endif // !_WIN32_WCE_NO_ARCHIVE_SUPPORT

// Implementation
public:
	virtual ~CFliteMFCDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
#endif

protected:

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
};


