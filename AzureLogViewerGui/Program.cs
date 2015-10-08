using MsCommon.ClickOnce;
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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            Action<string[]> method = (args) =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var form = new MainForm();
                Application.Run(form);
            };

            AppProgram.Start(
                applicationName: "AzureLogViewer",
                authorName: "Martijn Stolk",
                reportBugEndpoint: "http://martijn.tikkie.net/reportbug.php",
                args: arguments,
                mainMethod: method);
        }
    }
}
