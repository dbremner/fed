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
using System.ComponentModel;
using Plain.IO;

namespace DosboxApp {
	public abstract class BaseConfig {
		public string Name {
			set { m_Name = value; }
			get { return m_Name; }
		}

		public abstract void SaveTo(INI ini);

		public abstract void LoadFrom(INI ini);

		string m_Name;

		protected void writeProp(INI ini, string key, object value) {
			try {
				ini.Write(key, TypeDescriptor.GetConverter(value).ConvertToInvariantString(value));
			}
			catch { }
		}

		protected bool readProp(INI ini, string key, out object value) {
			try {
				string v = ini.Read(key);
				if (string.IsNullOrEmpty(v) == false) {
					Type type = this.GetType().GetProperty(key).PropertyType;
					value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(v);
					return true;
				}
			}
			catch { }
			value = null;
			return false;
		}

		protected void writeObject(INI ini, string key, object value) {
			try {
				ini.Write(key, value.ToString());
			}
			catch { }
		}

		protected string readString(INI ini, string key) {
			try {
				return ini.Read(key);
			}
			catch { }
			return string.Empty;
		}

		protected uint readNumber(INI ini, string key) {
			try {
				return ini.ReadInt(key);
			}
			catch { }
			return 0;
		}
	}
}
