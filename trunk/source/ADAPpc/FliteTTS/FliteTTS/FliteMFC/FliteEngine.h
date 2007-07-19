#pragma once

#include "..\FliteTTS\FliteTTS.h"

class CFliteEngine
{
public:
	CFliteEngine(void);
	virtual ~CFliteEngine(void);

	bool SayIt(LPSTR pstrText);
	bool TextToSpeech(LPSTR pstrText, LPSTR pstrFileName, float* pfSecond);
};
