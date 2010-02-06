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
using System.Windows.Forms;
using Plain.Forms;
using Plain.IO;

namespace DosboxApp {
	public class GameConfig : BaseConfig {
		public GameConfig() {
			base.Name = CAT_NAME;
			m_Games = new Dictionary<string,GameObject>();
		}

		public override void SaveTo(INI ini) {
			ini.Section = base.Name;
			writeProp(ini, KEY_NUMGAMES, m_Games.Count);
			int i = 0;
			foreach(GameObject gobj in m_Games.Values) {
				ini.Section = CAT_NAME_MORE + i;
				writeProp(ini, KEY_DIR, gobj.Directory);
				writeProp(ini, KEY_EXE, gobj.Executable);
				++i;
			}
		}

		public override void LoadFrom(INI ini) {
			ini.Section = base.Name;
			uint number = readNumber(ini, KEY_NUMGAMES);
			if (number > int.MaxValue) {
				number = 0;
			}
			m_Games = new Dictionary<string, GameObject>((int) number);
			for (int i = 0; i < number; ++i) {
				ini.Section = CAT_NAME_MORE + i;
				string dir = readString(ini, KEY_DIR);
				if (string.IsNullOrEmpty(dir) == false) {
					string exe = readString(ini, KEY_EXE);
					GameObject gobj = new GameObject(dir);
					gobj.Executable = exe;
					m_Games.Add(gobj.Directory, gobj);
				}
			}
		}

		public Dictionary<string, GameObject> Games {
			get { return m_Games; }
		}

		const string CAT_NAME = "Games";
		const string KEY_NUMGAMES = "Count";
		const string CAT_NAME_MORE = "Game";
		const string KEY_DIR = "Dir";
		const string KEY_EXE = "Exe";
		Dictionary<string, GameObject> m_Games;
	}
}
