// FliteMFCView.h : interface of the CFliteMFCView class
//


#pragma once
#include "FliteEngine.h"

class CFliteMFCView : public CFormView
{
protected: // create from serialization only
	CFliteMFCView();
	DECLARE_DYNCREATE(CFliteMFCView)

public:
	enum{ IDD = IDD_FLITEMFC_FORM };

// Attributes
public:
	CFliteMFCDoc* GetDocument() const;

// Operations
public:

// Overrides
public:
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	virtual void OnInitialUpdate(); // called first time after construct

// Implementation
public:
	virtual ~CFliteMFCView();
#ifdef _DEBUG
	virtual void AssertValid() const;
#endif

protected:

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
public:
	CEdit m_ctrlEdit;

private:
	CFliteEngine m_fliteEngine;
	afx_msg void OnBnClickedButtonPlay();
public:
	afx_msg void OnMenuTest();
};

#ifndef _DEBUG  // debug version in FliteMFCView.cpp
inline CFliteMFCDoc* CFliteMFCView::GetDocument() const
   { return reinterpret_cast<CFliteMFCDoc*>(m_pDocument); }
#endif

