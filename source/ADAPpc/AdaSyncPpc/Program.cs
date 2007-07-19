using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

namespace AdaSyncPpc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the appName.
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {
            bool autoSync = (args.Length > 0);
            //MessageBox.Show(args.Length.ToString());

            Application.Run(new MainForm(autoSync));
        }
    }
}