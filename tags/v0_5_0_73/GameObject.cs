using System;
using System.Collections.Generic;
using System.IO;

namespace DosboxApp {
	public class GameObject {
		public static List<string> GetExecutables(string path) {
			List<string> exes = new List<string>();
			try {
				DirectoryInfo diGame = new DirectoryInfo(path);
				foreach (FileInfo fi in diGame.GetFiles()) {
					string ext = Path.GetExtension(fi.Name).ToLowerInvariant();
					if (ext == ".bat" || ext == ".exe" || ext == ".com") {
						exes.Add(fi.Name);
					}
				}
				exes.Sort();
			}
			catch (Exception) {
			}
			return exes;
		}

		public static string FindPreferredExecutable(List<string> exes, string game) {
			foreach (string e in exes) {
				if (e.ToLowerInvariant() == "play.bat") {
					return e;
				}
			}
			foreach (string e in exes) {
				if (e.ToLowerInvariant() == "game.bat") {
					return e;
				}
			}
			foreach (string e in exes) {
				if (e.ToLowerInvariant() == game + ".bat") {
					return e;
				}
			}
			foreach (string e in exes) {
				if (e.ToLowerInvariant() == game + ".exe") {
					return e;
				}
			}
			foreach (string e in exes) {
				if (Path.GetExtension(e.ToLowerInvariant()) == ".exe") {
					return e;
				}
			}
			foreach (string e in exes) {
				if (Path.GetExtension(e.ToLowerInvariant()) == ".bat") {
					return e;
				}
			}
			foreach (string e in exes) {
				return e;
			}
			return string.Empty;
		}

		public GameObject(string directory) {
			m_Directory = Path.GetFullPath(directory);
		}

		public string Directory {
			get { return m_Directory; }
		}

		public string Executable {
			set { m_Executable = value; }
			get { return m_Executable; }
		}

		public string FileName {
			get { return m_Directory + @"\" + m_Executable; }
		}

		string m_Directory;
		string m_Executable;
	}
}
