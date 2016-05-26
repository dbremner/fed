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

namespace DosboxApp {
	public class GameObject {
		public static List<string> GetExecutables(string path) {
			List<string> exes = new List<string>();
			try {
				DirectoryInfo diGame = new DirectoryInfo(path);
				if (diGame.Exists) {
					foreach (FileInfo fi in diGame.GetFiles()) {
						string ext = Path.GetExtension(fi.Name).ToLowerInvariant();
						if (ext == ".bat" || ext == ".exe" || ext == ".com") {
							exes.Add(fi.Name);
						}
					}
					exes.Sort();
				}
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
			Directory = Path.GetFullPath(directory);
		}

		public string Directory { get; }

	    public string Executable { set; get; }

	    public string FileName {
			get { return Directory + @"\" + Executable; }
		}
	}
}
