// FliteMFC.h : main header file for the FliteMFC application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#ifdef POCKETPC2003_UI_MODEL
#include "resourceppc.h"
#endif 
#ifdef SMARTPHONE2003_UI_MODEL
#include "resourcesp.h"
#endif

// CFliteMFCApp:
// See FliteMFC.cpp for the implementation of this class
//

class CFliteMFCApp : public CWinApp
{
public:
	CFliteMFCApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation
public:
#ifndef WIN32_PLATFORM_WFSP
	afx_msg void OnAppAbout();
#endif // !WIN32_PLATFORM_WFSP

	DECLARE_MESSAGE_MAP()
};

extern CFliteMFCApp theApp;
