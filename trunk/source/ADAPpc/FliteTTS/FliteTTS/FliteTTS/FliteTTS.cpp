// FliteTTS.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include "FliteTTS.h"
#include <windows.h>
#include <commctrl.h>

#include "..\..\Include\flite.h"
#include "..\..\Lang\cmu_us_kal\voxdefs.h"

#pragma comment(lib, "cmu_us_kal.lib")
#pragma comment(lib, "cmutex.lib")
#pragma comment(lib, "flitelib.lib")
#pragma comment(lib, "usenglish.lib")

const int MAX_AMP_DB = 10;

extern "C" 
{
	cst_voice *REGISTER_VOX(const char *voxdir);
	cst_voice *UNREGISTER_VOX(cst_voice *vox);
};

static cst_voice *g_v = NULL;

BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
    return TRUE;
}

FLITETTS_API bool FliteInitialize()
{
	flite_init();
	g_v = REGISTER_VOX(NULL);
	return NULL != g_v;
}

FLITETTS_API bool FliteTextToSpeech(LPSTR pstrText, LPSTR pstrFileName, float* pfSecond)
{
	if( g_v == NULL ) 
	{
		return false;
	}
	
	*pfSecond = flite_text_to_speech_normalized(pstrText, g_v, pstrFileName, MAX_AMP_DB);
	
	return true;
}

FLITETTS_API bool FlitePlaySound(LPSTR pstrFileName)
{
	TCHAR szTemp[MAX_PATH];
	int length = strlen(pstrFileName);
	for (int i = 0; i < length; i++)
	{
		szTemp[i] = (TCHAR) pstrFileName[i];
	}

	szTemp[length] = 0;

	return TRUE == ::PlaySound(szTemp, NULL, SND_FILENAME | SND_SYNC);
}

FLITETTS_API bool FliteSayIt(LPSTR pstrText)
{
	if( g_v == NULL ) 
	{
		return false;
	}
	
	flite_text_to_speech_normalized(pstrText, g_v, "play", MAX_AMP_DB);
	
	return true;
}

//FLITETTS_API bool FliteSayIt2(LPSTR pstrText)
//{
//	TCHAR szPath[MAX_PATH];
//	TCHAR szFileName[MAX_PATH];
//	::GetTempPath(MAX_PATH, szPath);
//	::GetTempFileName(szPath, _T("FLT"), 0, szFileName);
//
//	char szTemp[MAX_PATH];
//	int length = _tcslen(szFileName);
//	for (int i = 0; i < length; i++)
//	{
//		szTemp[i] = (char) szFileName[i];
//	}
//
//	szTemp[length] = 0;
//
//	if (FliteTextToSpeech(pstrText, szTemp)) 
//	{
//		bool ok = FlitePlaySound(szTemp);
//		::DeleteFile(szFileName);
//
//		return ok;
//	}
//
//	return false;
//}

FLITETTS_API void FliteDeinitialize()
{
	UNREGISTER_VOX(g_v);
}