using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AdaMoneyPpc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the appName.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new MainForm());
        }
    }
}