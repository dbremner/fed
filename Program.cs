/*
FED (Front-End for Dosbox), a gaming graphical user interface for the DOSBox emulator.
Copyright (C) 2009  Ka Ki Cheung

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

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
				if (param.StartsWith("/mode:")) {
					if (param.EndsWith("multi")) {
						AppInfo.OperatingMode = OperatingMode.MultiUser;
					}
					else if (param.EndsWith("single")) {
						AppInfo.OperatingMode = OperatingMode.SingleUser;
					}
				}
			}
#if DEBUG
			Stopwatch.Mark("updater and command line");
#endif

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			AppConfig = AppInfo.LoadConfig();
			Updater = new Updater();

			if (Program.AppConfig.UpdateConfig.CheckOnStartup) {
				if (Program.Updater.GUICheck(false)) {
					// Don't ask me why Application.Restart() or .Exit() doesn't really exit here...
					return;
				}
			}

			Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
#if DEBUG
			Stopwatch.Mark("app config before form");
#endif
			Application.Run(new GameListForm());
		}

		static void Application_ApplicationExit(object sender, EventArgs e) {
			AppInfo.SaveConfig(AppConfig);
		}

		public static Updater Updater { get; private set; }

	    public static AppConfig AppConfig { get; private set; }
	}
}
