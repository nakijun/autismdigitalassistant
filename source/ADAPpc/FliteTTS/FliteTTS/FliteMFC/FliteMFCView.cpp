// FliteMFCView.cpp : implementation of the CFliteMFCView class
//

#include "stdafx.h"
#include "FliteMFC.h"

#include "FliteMFCDoc.h"
#include "FliteMFCView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CFliteMFCView

IMPLEMENT_DYNCREATE(CFliteMFCView, CFormView)

BEGIN_MESSAGE_MAP(CFliteMFCView, CFormView)
	ON_BN_CLICKED(IDC_BUTTON_PLAY, &CFliteMFCView::OnBnClickedButtonPlay)
	ON_COMMAND(ID_MENU_TEST, &CFliteMFCView::OnMenuTest)
END_MESSAGE_MAP()

// CFliteMFCView construction/destruction

CFliteMFCView::CFliteMFCView()
: CFormView(CFliteMFCView::IDD)
{
	// TODO: add construction code here

}

CFliteMFCView::~CFliteMFCView()
{
}

void CFliteMFCView::DoDataExchange(CDataExchange* pDX)
{
	CFormView::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT_TEXT, m_ctrlEdit);
}

BOOL CFliteMFCView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CFormView::PreCreateWindow(cs);
}



void CFliteMFCView::OnInitialUpdate()
{
	CFormView::OnInitialUpdate();

	m_ctrlEdit.SetWindowTextW(_T("Merry Christmas and Happy New Year!"));
}


// CFliteMFCView diagnostics

#ifdef _DEBUG
void CFliteMFCView::AssertValid() const
{
	CFormView::AssertValid();
}

CFliteMFCDoc* CFliteMFCView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CFliteMFCDoc)));
	return (CFliteMFCDoc*)m_pDocument;
}
#endif //_DEBUG


// CFliteMFCView message handlers
void CFliteMFCView::OnBnClickedButtonPlay()
{
	int iLen = (m_ctrlEdit.GetWindowTextLength() + 1) * sizeof(TCHAR);
	LPTSTR pstrText = (LPTSTR) malloc(iLen);
	if( pstrText == NULL ) 
	{
		::SHShowOutOfMemory(m_hWnd, 0);
		return;
	}

	m_ctrlEdit.GetWindowText(pstrText, iLen);
	
	USES_CONVERSION;

	DWORD dwOldTime = ::GetTickCount();

	if( m_fliteEngine.SayIt(W2A(pstrText)) ) 
	{
		DWORD dwTimeElapsed = ::GetTickCount() - dwOldTime;
		
		TCHAR szMsg[MAX_PATH];
		_stprintf(szMsg, _T("%d ms"), dwTimeElapsed);
		MessageBox(
			szMsg, _T("Execution Time"), MB_OK);
	}

	free(pstrText);
}

void CFliteMFCView::OnMenuTest()
{
	int iLen = (m_ctrlEdit.GetWindowTextLength() + 1) * sizeof(TCHAR);
	LPTSTR pstrText = (LPTSTR) malloc(iLen);
	if( pstrText == NULL ) {
		::SHShowOutOfMemory(m_hWnd, 0);
		return;
	}
	m_ctrlEdit.GetWindowText(pstrText, iLen);

	TCHAR szFileName[MAX_PATH] = _T("\\Storage Card\\TTS_MFC.wav");

	USES_CONVERSION;

	DWORD dwOldTime = GetTickCount();

	float fDuration;
	if (m_fliteEngine.TextToSpeech(W2A(pstrText), W2A(szFileName), &fDuration)) 
	{
		DWORD dwTimeElapsed = GetTickCount() - dwOldTime;
		
		//::PlaySound(szFileName, NULL, SND_FILENAME | SND_SYNC);
		//::DeleteFile(szFileName);
		
		TCHAR szMsg[MAX_PATH];
		_stprintf(szMsg, _T("%d ms\nDuration %f s"), dwTimeElapsed, fDuration);
		MessageBox(
			szMsg, _T("Execution Time"), MB_OK);
	}

	free(pstrText);
}
