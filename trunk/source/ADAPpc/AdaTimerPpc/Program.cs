using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AdaTimerPpc
{
    static class Program
    {
        static public string ARG_WARNING = "-WARNING";
        static public string ARG_TIMEOUT = "-TIMEOUT";

        static private bool isWarning;
        static public bool IsWarning
        {
            get { return isWarning; }
        }

        static private bool isTimeOut;
        static public bool IsTimeOut
        {
            get { return isTimeOut; }
        }

        /// <summary>
        /// The main entry point for the appName.
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                //MessageBox.Show(args[0]);

                if (args[0] == ARG_WARNING)
                {
                    isWarning = true;
                }
                else if (args[0] == ARG_TIMEOUT)
                {
                    isTimeOut = true;
                }
            }

            Application.Run(new MainForm());
        }
    }
}