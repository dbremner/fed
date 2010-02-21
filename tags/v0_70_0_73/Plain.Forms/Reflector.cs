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
using System.Reflection;

namespace Plain.Forms {
	public class Reflector {
		public Reflector(string ns)
			: this(ns, ns) {
		}
		public Reflector(string an, string ns) {
			m_ns = ns;
			m_asmb = null;
			foreach (AssemblyName aN in Assembly.GetExecutingAssembly().GetReferencedAssemblies()) {
				if (aN.FullName.StartsWith(an)) {
					m_asmb = Assembly.Load(aN);
					break;
				}
			}
		}

		public Type GetType(string typeName) {
			Type type = null;
			string[] names = typeName.Split('.');
			if (names.Length > 0) {
				type = m_asmb.GetType(m_ns + "." + names[0]);
			}
			for (int i = 1; i < names.Length; ++i) {
				type = type.GetNestedType(names[i], BindingFlags.NonPublic);
			}
			return type;
		}

		public object New(string name, params object[] parameters) {
			Type type = GetType(name);
			ConstructorInfo[] ctorInfos = type.GetConstructors();
			foreach (ConstructorInfo ci in ctorInfos) {
				try {
					return ci.Invoke(parameters);
				} catch { }
			}
			return null;
		}

		public object Call(object obj, string func, params object[] parameters) {
			return Call2(obj, func, parameters);
		}

		public object Call2(object obj, string func, object[] parameters) {
			return CallAs2(obj.GetType(), obj, func, parameters);
		}

		public object CallAs(Type type, object obj, string func, params object[] parameters) {
			return CallAs2(type, obj, func, parameters);
		}

		public object CallAs2(Type type, object obj, string func, object[] parameters) {
			MethodInfo methInfo = type.GetMethod(func, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return methInfo.Invoke(obj, parameters);
		}

		public object Get(object obj, string prop) {
			return GetAs(obj.GetType(), obj, prop);
		}

		public object GetAs(Type type, object obj, string prop) {
			PropertyInfo propInfo = type.GetProperty(prop, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return propInfo.GetValue(obj, null);
		}

		public object GetEnum(string typeName, string name) {
			Type type = GetType(typeName);
			FieldInfo fieldInfo = type.GetField(name);
			return fieldInfo.GetValue(null);
		}

		string m_ns;
		Assembly m_asmb;
	}
}
