/*
FED (Front-End for Dosbox), a gaming graphical user interface for the DOSBox emulator.
Copyright (C) 2010  Ka Ki Cheung

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
using Plain.IO;

namespace DosboxApp {
	public class UpdateConfig : BaseConfig {
		public UpdateConfig() {
			base.Name = "ProgramUpdate";
		}

		public override void SaveTo(INI ini) {
			ini.Section = base.Name;
			base.writeProp(ini, "CheckOnStartup", CheckOnStartup);
			base.writeProp(ini, "UpdateInstall", UpdateInstall);
		}

		public override void LoadFrom(INI ini) {
			object value;
			ini.Section = base.Name;
			if (base.readProp(ini, "CheckOnStartup", out value)) {
				CheckOnStartup = (bool) value;
			}
			if (base.readProp(ini, "UpdateInstall", out value)) {
				UpdateInstall = (bool) value;
			}
		}

		public bool CheckOnStartup { set; get; }

	    public bool UpdateInstall { set; get; }

	    public bool DelayedInstall { set; get; }
	}
}
