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
using System.IO;
using Plain.IO;

namespace DosboxApp {
	public static class AppInfo {
		static AppInfo() {
			s_Mode = OperatingMode.MultiUser;
		}

		public static string GetAppSearchDirectory() {
			return Path.GetFullPath(System.Windows.Forms.Application.StartupPath);
		}

		public static string GetRelativePath(string path) {
			string lowpath = Path.GetFullPath(path);
			string apppath = GetAppSearchDirectory();
			return PathEx.GetRelativePath(apppath, lowpath);
		}

		public static string GetAppConfigFileName() {
			return GetAppSearchDirectory() + @"\fed.app.ini";
		}

		public static string GetUserConfigFileName() {
			return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\FED\fed.user.ini";
		}

		public static AppConfig LoadConfig() {
			if (s_Mode == OperatingMode.MultiUser) {
				return LoadConfig(GetUserConfigFileName());
			}
			else {
				return LoadConfig(GetAppConfigFileName());
			}
		}

		public static AppConfig LoadConfig(string path) {
			AppConfig config = new AppConfig();

			if (File.Exists(path)) {
				INI ini = new INI(path);
				config.GameListFormConfig.LoadFrom(ini);
				config.GameConfig.LoadFrom(ini);
				config.UpdateConfig.LoadFrom(ini);
			}

			return config;
		}

		public static void SaveConfig(AppConfig config) {
			if (s_Mode == OperatingMode.MultiUser) {
				SaveConfig(config, GetUserConfigFileName());
			}
			else {
				SaveConfig(config, GetAppConfigFileName());
			}
		}

		public static void SaveConfig(AppConfig config, string path) {
			string dir = Path.GetDirectoryName(path);
			if (Directory.Exists(dir) == false) {
				Directory.CreateDirectory(dir);
			}
			INI ini = new INI(path);
			config.GameListFormConfig.SaveTo(ini);
			config.GameConfig.SaveTo(ini);
			config.UpdateConfig.SaveTo(ini);
		}

		public static OperatingMode OperatingMode {
			set { s_Mode = value; }
			get { return s_Mode; }
		}

		static OperatingMode s_Mode;
	}

	public enum OperatingMode {
		MultiUser,
		SingleUser,
		MultiToSingle,
		SingleToMulti
	}
}
