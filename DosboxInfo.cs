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
using System.IO;
using Plain.IO;

namespace DosboxApp {
	public class DosboxInfo {
		public static List<string> Installations {
			get {
				List<string> instances = new List<string>();
				try {
					// FIXME: what if StartupPath is the root/ProgramFiles directory?
					// FIXME: what if ProgramFiles is the root directory?
					if (AppInfo.OperatingMode == OperatingMode.SingleUser) {
						DirectoryInfo diStartup = new DirectoryInfo(AppInfo.GetAppSearchDirectory());
						FindDosboxFromDirectories(diStartup.GetDirectories(DOSBOX_DIR_WILDCARD, SearchOption.TopDirectoryOnly), instances);
					}
					string pathProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
					DirectoryInfo diProgramFilesParent = new DirectoryInfo(Path.GetDirectoryName(pathProgramFiles));
					foreach (DirectoryInfo diProgramFiles in diProgramFilesParent.GetFileSystemInfos(Path.GetFileName(pathProgramFiles) + "*")) {
						FindDosboxFromDirectories(diProgramFiles.GetDirectories(DOSBOX_DIR_WILDCARD, SearchOption.TopDirectoryOnly), instances);
					}
					DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.System)).Root;
					FindDosboxFromDirectories(di.GetDirectories(DOSBOX_DIR_WILDCARD, SearchOption.TopDirectoryOnly), instances);

					instances.Sort(CompareDosboxVersions);
				}
				catch (Exception) {
				}
				return instances;
			}
		}

		const string DOSBOX_DIR_WILDCARD = "DOSBox*";

		static void FindDosboxFromDirectories(DirectoryInfo[] directories, List<string> instances) {
			foreach (DirectoryInfo diDosbox in directories) {
				if (File.Exists(diDosbox.FullName + Path.DirectorySeparatorChar + DosboxInfo.EXECUTABLE)) {
					instances.Add(diDosbox.FullName);
				}
			}
		}

		static int CompareDosboxVersions(string s1, string s2) {
			string n1 = Path.GetFileName(s1);
			string n2 = Path.GetFileName(s2);
			int rtn = string.Compare(n2, n1);
			if (rtn == 0) {
				return s2.Length.CompareTo(s1.Length);
			}
			return rtn;
		}

		public const string EXECUTABLE = "dosbox.exe";
		public const string CONF_NAME = "dosbox.conf";
		public const string CONF_DIR = @"\DOSBox\";
		public const string CONF_EXT = ".conf";
		public const string NEWS_NAME = "NEWS.txt";

		public DosboxInfo(string directory) {
			m_Directory = Path.GetFullPath(directory);
			m_VersionString = Path.GetFileName(directory);
		}

		public string GetAppConfigFileName() {
			return m_Directory + Path.DirectorySeparatorChar + CONF_NAME;
		}

		public string GetUserConfigFileName() {
			return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + CONF_DIR + m_VersionString + CONF_EXT;
		}

		public INI GetUserConfigINI() {
			return new INI(GetUserConfigFileName());
		}

		public DosboxConfig LoadConfig() {
			DosboxConfig config = new DosboxConfig();

			INI ini = GetUserConfigINI();
			DosboxConfig.LoadFrom(ini, config);

			return config;
		}

		public void SaveConfig(DosboxConfig config) {
			INI ini = GetUserConfigINI();
			DosboxConfig.SaveTo(ini, config);
		}

		public string Directory {
			get { return m_Directory; }
		}

		public string FileName {
			get { return m_Directory + Path.DirectorySeparatorChar + EXECUTABLE; }
		}

		public string VersionString {
			get { return m_VersionString; }
		}

		public Version Version {
			get { return m_Version; }
		}

		string m_Directory;
		string m_VersionString;
		Version m_Version;

		void read_version() {
			StreamReader reader = null;
			try {
				reader = new StreamReader(m_Directory + Path.DirectorySeparatorChar + NEWS_NAME);
				string ver = reader.ReadLine();
				reader.Close();
				m_Version = new Version(ver);
			}
			catch {
			}
			finally {
				if (reader != null) {
					reader.Close();
				}
			}
		}
	}
}
