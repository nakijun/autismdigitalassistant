// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the FLITETTS_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// FLITETTS_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef FLITETTS_EXPORTS
#define FLITETTS_API __declspec(dllexport)
#else
#define FLITETTS_API __declspec(dllimport)
#endif

extern "C" 
{
	FLITETTS_API bool FliteInitialize();
	FLITETTS_API bool FliteTextToSpeech(LPSTR pstrText, LPSTR pstrFileName, float* pfSecond);
	FLITETTS_API bool FlitePlaySound(LPSTR pstrFileName);
	FLITETTS_API bool FliteSayIt(LPSTR pstrText);
	FLITETTS_API void FliteDeinitialize();
};