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
			foreach(GameObject ginfo in m_Games.Values) {
				ini.Section = CAT_NAME_MORE + i;
				writeProp(ini, KEY_DIR, ginfo.Directory);
				writeProp(ini, KEY_EXE, ginfo.Executable);
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
					GameObject ginfo = new GameObject(dir);
					ginfo.Executable = exe;
					m_Games.Add(ginfo.Directory, ginfo);
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
