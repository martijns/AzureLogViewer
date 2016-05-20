using log4net;
using log4net.Config;
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
        private static ILog Logger = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            Action<string[]> method = (args) =>
            {
                XmlConfigurator.Configure();
                Logger.Info("Starting...");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var form = new MainForm();
                Application.Run(form);
            };

            AppProgram.Start(
                applicationName: "AzureLogViewer",
                authorName: "Martijn Stolk",
                reportBugEndpoint: "http://martijn.tikkie.net/reportbug.php",
                feedbackEndpoint: "http://martijn.tikkie.net/feedback.php",
                args: arguments,
                mainMethod: method);
        }
    }
}
