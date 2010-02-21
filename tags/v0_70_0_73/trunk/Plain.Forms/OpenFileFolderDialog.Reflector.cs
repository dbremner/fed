using System;
using System.Reflection;

namespace Plain.Forms {
	partial class OpenFileFolderDialog {
		private class Reflector {
			public Reflector(string ns) {
				_ns = ns;
				_asmb = null;
				foreach (AssemblyName an in Assembly.GetExecutingAssembly().GetReferencedAssemblies()) {
					if (an.FullName.StartsWith(_ns)) {
						_asmb = Assembly.Load(an);
						break;
					}
				}
			}

			public Type GetType(string typeName) {
				Type type = null;
				string[] names = typeName.Split('.');
				if (names.Length > 0) {
					type = _asmb.GetType(_ns + "." + names[0]);
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
					}
					catch { }
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
				Type type = obj.GetType();
				PropertyInfo propInfo = type.GetProperty(prop, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				return propInfo.GetValue(obj, null);
			}

			public object GetEnum(string typeName, string name) {
				Type type = GetType(typeName);
				FieldInfo fieldInfo = type.GetField(name);
				return fieldInfo.GetValue(null);
			}

			string _ns;
			Assembly _asmb;
		}
	}
}
