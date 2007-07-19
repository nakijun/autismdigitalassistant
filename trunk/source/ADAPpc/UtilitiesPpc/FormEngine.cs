using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace UtilitiesPpc
{
    public class FormEngine
    {
        [DllImport("coredll.dll", EntryPoint = "GetSystemMetrics")]
        private extern static int GetSystemMetrics(int nIndex);

        [DllImport("coredll.dll")]
        private extern static int ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_SHOWNA = 8;
        private const int SW_SHOWMAXIMIZED = 11;
        private const int SW_MAXIMIZE = 12;
        private const int SW_RESTORE = 13;

        [DllImport("coredll.dll")]
        private extern static int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, int bRepaint);

        [DllImport("coredll.dll")]
        private extern static int SetRect(ref RECT lprc, int xLeft, int yTop, int xRight, int yBottom);

        [DllImport("coredll.dll")]
        private extern static int GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("coredll.dll")]
        private extern static int SystemParametersInfo(int uiAction, int uiParam, ref RECT pvParam, int fWinIni);

        [DllImport("coredll.dll")]
        private extern static IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("coredll.dll")]
        private extern static int SetForegroundWindow(IntPtr hWnd);

        [DllImport("coredll.dll")]
        private extern static IntPtr GetForegroundWindow();

        [DllImport("coredll.dll")]
        private extern static IntPtr GetFocus();

        [DllImport("aygshell.dll")]
        private extern static bool SHFullScreen(IntPtr hWnd, uint dwState);

        private const uint SHFS_SHOWTASKBAR = 0x1;
        private const uint SHFS_HIDETASKBAR = 0x2;
        private const uint SHFS_SHOWSIPBUTTON = 0x4;
        private const uint SHFS_HIDESIPBUTTON = 0x8;
        private const uint SHFS_SHOWSTARTICON = 0x10;
        private const uint SHFS_HIDESTARTICON = 0x20;

        private const int HWND_TOPMOST = -1;
        private const int HWND_NOTOPMOST = -2;

        private const uint SWP_SHOWWINDOW = 0x40;

        private const int SM_CXSCREEN = 0x0;
        private const int SM_CYSCREEN = 0x1;

        private const int HHTASKBARHEIGHT = 26;

        private const int SPI_SETWORKAREA = 47;

        private const int SPI_GETWORKAREA = 48;

        public static void SetFullScreen(Form f)
        {
            f.Focus();
            IntPtr hwnd = GetFocus();

            uint dwState;

            // To get info full screen mode, first hide all of the shell parts.
            dwState = (SHFS_HIDESTARTICON | SHFS_HIDESIPBUTTON);
            //dwState = (SHFS_HIDETASKBAR | SHFS_HIDESTARTICON | SHFS_HIDESIPBUTTON);
            bool result = SHFullScreen(hwnd, dwState);

            //IntPtr hWndSipButton = FindWindow("MS_SIPBUTTON", null);
            //if (hWndSipButton != null)
            //{
            //    int r = ShowWindow(hWndSipButton, SW_HIDE);
            //}

            //Next resize the main window to be the size of the screen.
            //RECT rc = new RECT();
            //SetRect(ref rc, 0, 0, GetSystemMetrics(SM_CXSCREEN), GetSystemMetrics(SM_CYSCREEN));
            //MoveWindow(hwnd, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, 1);
        }

        public static void SetNormal(Form f)
        {
            f.Focus();
            IntPtr hwnd = GetFocus();

            RECT rc = new RECT();
            uint dwState;

            // To get into normal mode, first show all of the shell parts.
            dwState = (SHFS_SHOWTASKBAR | SHFS_SHOWSTARTICON | SHFS_SHOWSIPBUTTON);
            bool result = SHFullScreen(hwnd, dwState);

            // Then resize the main window to be the size of the work area.
            SystemParametersInfo(SPI_GETWORKAREA, 0, ref rc, 0);
            MoveWindow(hwnd, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, 1);
        }

        public static bool BringWindowToTop(Form f)
        {
            int result = SetForegroundWindow(f.Handle);
            return result != 0;
        }

        public static bool BringWindowToTop(string windowName, string appPath)
        {
            IntPtr hwnd = FindWindow(null, windowName);
            IntPtr hwndTop = GetForegroundWindow();

            if (hwnd.ToInt32() != 0)
            {
                if (hwndTop != hwnd)
                {
                    int result = SetForegroundWindow(hwnd);
                    return result != 0;
                }
            }
            else
            {
                string arguments = "";

                try
                {
                    Process.Start(appPath, arguments);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return false;
                }
            }

            return true;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;

            public int top;

            public int right;

            public int bottom;
        }

    }
}
