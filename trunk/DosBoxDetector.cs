using System;
using System.Collections.Generic;
using System.IO;

namespace DosboxApp {
	class DosBoxDetector {
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
	}
}
