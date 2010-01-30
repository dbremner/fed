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
				if (v != string.Empty) {
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
