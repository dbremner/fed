using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DosboxApp {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
			foreach (string a in args) {
				string param = a.ToLowerInvariant();
				if (param.StartsWith("/mode:")){
					if (param.EndsWith("multi")) {
						AppInfo.OperatingMode = OperatingMode.MultiUser;
					} else if (param.EndsWith("single")) {
						AppInfo.OperatingMode = OperatingMode.SingleUser;
					}
				}
			}
			s_AppConfig = AppInfo.LoadConfig();

			Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameListForm());
        }

		static void Application_ApplicationExit(object sender, EventArgs e) {
			AppInfo.SaveConfig(s_AppConfig);
		}

		public static AppConfig AppConfig {
			get { return s_AppConfig; }
		}

		static AppConfig s_AppConfig;
    }
}
