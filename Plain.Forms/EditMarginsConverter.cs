#if DEBUG

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Plain.Forms {
	public class EditMarginsConverter : TypeConverter {
		public EditMarginsConverter() { }
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
			return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
		}
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			string str = value as string;
			if (str == null) {
				return base.ConvertFrom(context, culture, value);
			}
			str = str.Trim();
			if (str.Length == 0) {
				return null;
			}
			if (culture == null) {
				culture = CultureInfo.CurrentCulture;
			}
			char ch = culture.TextInfo.ListSeparator[0];
			string[] strArray = str.Split(new char[] { ch });
			int[] valArray = new int[strArray.Length];
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
			for (int i = 0; i < valArray.Length; i++) {
				valArray[i] = (int) converter.ConvertFromString(context, culture, strArray[i]);
			}
			if (valArray.Length != 2) {
				throw new ArgumentException(string.Format("Text \"{0}\" cannot be parsed. The expected text format is \"{1}\"", new object[] { str, "Left, Right" }));
			}
			return new EditMargins(valArray[0], valArray[1]);
		}
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
		}
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			if (destinationType == null) {
				throw new ArgumentNullException("destinationType");
			}
			if ((destinationType == typeof(string)) && (value is EditMargins)) {
				EditMargins mg = (EditMargins) value;
				if (culture == null) {
					culture = CultureInfo.CurrentCulture;
				}
				string separator = culture.TextInfo.ListSeparator + " ";
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
				string[] strArray = new string[2];
				int num = 0;
				strArray[num++] = converter.ConvertToString(context, culture, mg.Left);
				strArray[num++] = converter.ConvertToString(context, culture, mg.Right);
				return string.Join(separator, strArray);
			}
			if ((destinationType == typeof(InstanceDescriptor)) && (value is EditMargins)) {
				EditMargins mg2 = (EditMargins) value;
				ConstructorInfo constructor = typeof(EditMargins).GetConstructor(new Type[] { typeof(int), typeof(int) });
				if (constructor != null) {
					return new InstanceDescriptor(constructor, new object[] { mg2.Left, mg2.Right });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
		public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues) {
			return new EditMargins((int) propertyValues["Left"], (int) propertyValues["Right"]);
		}
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) {
			return true;
		}
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes) {
			return TypeDescriptor.GetProperties(typeof(EditMargins), attributes).Sort(new string[] { "Left", "Right" });
		}
		public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
			return true;
		}
	}
}

#endif
