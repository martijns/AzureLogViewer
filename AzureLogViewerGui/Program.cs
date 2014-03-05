using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureLogViewerGui
{
    static class Program
    {
        private static MainForm form;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                form = new MainForm();
                Application.Run(form);
            }
            catch (Exception ex)
            {
                var bugform = new ReportBugForm(ex);
                Application.Run(bugform);
            }
        }

        static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex == null)
                return;

            if (Application.MessageLoop)
            {
                new ReportBugForm(ex).ShowDialog();
            }
            else
            {
                var bugform = new ReportBugForm(ex);
                Application.Run(bugform);
            }
        }
    }
}
