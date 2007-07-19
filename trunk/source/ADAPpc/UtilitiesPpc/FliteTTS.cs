using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace UtilitiesPpc
{
    public class FliteTTS
    {
        static private bool _initFliteTTSDone = false;

        public FliteTTS()
        {
            if (!_initFliteTTSDone)
            {
                _initFliteTTSDone = FliteInitialize();
            }
        }

        ~FliteTTS()
        {
            if (_initFliteTTSDone)
            {
                FliteDeinitialize();
            }
        }

        public bool SayIt(string text)
        {
            Encoding ascii = System.Text.Encoding.ASCII;

            return FliteSayIt(ascii.GetBytes(text));
        }

        public bool TextToSpeech(string text, string fileName, out float duration)
        {
            Encoding ascii = System.Text.Encoding.ASCII;

            bool ok = FliteTextToSpeech(ascii.GetBytes(text),
                ascii.GetBytes(fileName),
                out duration);

            return ok;
        }

        public bool PlaySound(string fileName)
        {
            Encoding ascii = System.Text.Encoding.ASCII;
            return FlitePlaySound(ascii.GetBytes(fileName));
        }

        #region Dll Imports
        [DllImport("FliteTTS.dll")]
        private static extern bool FliteInitialize();

        [DllImport("FliteTTS.dll")]
        private static extern bool FliteTextToSpeech([MarshalAs(UnmanagedType.LPArray)] byte[] pstrText, [MarshalAs(UnmanagedType.LPArray)] byte[] pstrFileName, out float pfSecond);

        [DllImport("FliteTTS.dll")]
        private static extern bool FliteSayIt(
            [MarshalAs(UnmanagedType.LPArray)] byte[] pstrText);

        [DllImport("FliteTTS.dll")]
        private static extern bool FlitePlaySound(
            [MarshalAs(UnmanagedType.LPArray)] byte[] pstrFileName);

        [DllImport("FliteTTS.dll")]
        private static extern void FliteDeinitialize();
        #endregion
    }
}
