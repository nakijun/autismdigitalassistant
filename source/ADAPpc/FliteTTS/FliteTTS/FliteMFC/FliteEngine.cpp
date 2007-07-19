#include "StdAfx.h"
#include "FliteEngine.h"

CFliteEngine::CFliteEngine(void)
{
	if (!FliteInitialize())
	{
		MessageBox(NULL, _T("Failed to init Flite TTS"), _T("TTS"), MB_OK);
	}
}

CFliteEngine::~CFliteEngine(void)
{
	FliteDeinitialize();
}

bool CFliteEngine::SayIt(LPSTR pstrText)
{
	return FliteSayIt(pstrText);
}

bool CFliteEngine::TextToSpeech(LPSTR pstrText, LPSTR pstrFileName, float* pfSecond)
{
	return FliteTextToSpeech(pstrText, pstrFileName, pfSecond);
}

