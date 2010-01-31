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
			//string apppath = GetAppSearchDirectory();
			string apppath = @"c:\user\desk\top";
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
