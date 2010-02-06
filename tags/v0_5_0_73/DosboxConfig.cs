﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms.Design;
using Plain.Design;
using Plain.IO;

namespace DosboxApp {
	[DefaultProperty("cmd")]
	public class DosboxConfig {
		public class ResolutionTypeConverter : ExpandableObjectConverter {
			public const string RESOLUTION_ORIGINAL = "original";
			public const string RESOLUTION_SEPARATOR = "x";
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
				return true;
			}
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
				string str = value as string;
				if (str != null) {
					str = str.Trim().ToLowerInvariant();
					if (str == RESOLUTION_ORIGINAL) {
						return ResolutionType.Empty;
					}
				}
				return ConvertFrom2(context, culture, value);
			}
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
				return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
			}
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
				if ((destinationType == typeof(string)) && (value is ResolutionType)) {
					ResolutionType rt = (ResolutionType) value;
					if (rt.IsEmpty) {
						return RESOLUTION_ORIGINAL;
					}
				}
				return ConvertTo2(context, culture, value, destinationType);
			}
			public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues) {
				return new ResolutionType((UInt16) propertyValues["width"], (UInt16) propertyValues["height"]);
			}
			public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) {
				return true;
			}
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes) {
				return TypeDescriptor.GetProperties(typeof(ResolutionType), attributes).Sort(new string[] { "width", "height" });
			}
			public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
				return true;
			}

			object ConvertFrom2(ITypeDescriptorContext context, CultureInfo culture, object value) {
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
				char ch = RESOLUTION_SEPARATOR[0];
				string[] strArray = str.Split(new char[] { ch });
				UInt16[] valArray = new UInt16[strArray.Length];
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(UInt16));
				for (int i = 0; i < valArray.Length; i++) {
					valArray[i] = (UInt16) converter.ConvertFromString(context, culture, strArray[i]);
				}
				if (valArray.Length != 2) {
					throw new ArgumentException(string.Format("Text \"{0}\" cannot be parsed. The expected text format is \"{1}\"", new object[] { str, "width,height" }));
				}
				return new ResolutionType(valArray[0], valArray[1]);
			}
			object ConvertTo2(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
				if (destinationType == null) {
					throw new ArgumentNullException("destinationType");
				}
				if ((destinationType == typeof(string)) && (value is ResolutionType)) {
					ResolutionType rt = (ResolutionType) value;
					if (culture == null) {
						culture = CultureInfo.CurrentCulture;
					}
					string separator = RESOLUTION_SEPARATOR;
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(UInt16));
					string[] strArray = new string[2];
					int num = 0;
					strArray[num++] = converter.ConvertToString(context, culture, rt.width);
					strArray[num++] = converter.ConvertToString(context, culture, rt.height);
					return string.Join(separator, strArray);
				}
				if ((destinationType == typeof(InstanceDescriptor)) && (value is ResolutionType)) {
					ResolutionType rt2 = (ResolutionType) value;
					ConstructorInfo constructor = typeof(ResolutionType).GetConstructor(new Type[] { typeof(UInt16), typeof(UInt16) });
					if (constructor != null) {
						return new InstanceDescriptor(constructor, new object[] { rt2.width, rt2.height });
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		public class PriorityTypeConverter : ExpandableObjectConverter {
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
				return true;
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
				PriorityValue[] valArray = new PriorityValue[strArray.Length];
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(PriorityValue));
				for (int i = 0; i < valArray.Length; i++) {
					valArray[i] = (PriorityValue) converter.ConvertFromString(context, culture, strArray[i]);
				}
				if (valArray.Length != 2) {
					throw new ArgumentException(string.Format("Text \"{0}\" cannot be parsed. The expected text format is \"{1}\"", new object[] { str, "when_focused,when_minimzed" }));
				}
				return new PriorityType(valArray[0], valArray[1]);
			}
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
				return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
			}
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
				if (destinationType == null) {
					throw new ArgumentNullException("destinationType");
				}
				if ((destinationType == typeof(string)) && (value is PriorityType)) {
					PriorityType pt = (PriorityType) value;
					if (culture == null) {
						culture = CultureInfo.CurrentCulture;
					}
					string separator = culture.TextInfo.ListSeparator;
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(PriorityValue));
					string[] strArray = new string[2];
					int num = 0;
					strArray[num++] = converter.ConvertToString(context, culture, pt.when_focused);
					strArray[num++] = converter.ConvertToString(context, culture, pt.when_minimzed);
					return string.Join(separator, strArray);
				}
				if ((destinationType == typeof(InstanceDescriptor)) && (value is PriorityType)) {
					PriorityType pt2 = (PriorityType) value;
					ConstructorInfo constructor = typeof(PriorityType).GetConstructor(new Type[] { typeof(PriorityValue), typeof(PriorityValue) });
					if (constructor != null) {
						return new InstanceDescriptor(constructor, new object[] { pt2.when_focused, pt2.when_minimzed });
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues) {
				return new PriorityType((PriorityValue) propertyValues["when_focused"], (PriorityValue) propertyValues["when_minimzed"]);
			}
			public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) {
				return true;
			}
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes) {
				return TypeDescriptor.GetProperties(typeof(PriorityType), attributes).Sort(new string[] { "when_focused", "when_minimzed" });
			}
			public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
				return true;
			}
		}
		public class PrefixedEnumConverter : EnumConverter {
			public const char PREFIX_CHAR = '_';
			public PrefixedEnumConverter(Type type)
				: base(type) {
			}
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
				string str = value as string;
				if (str != null) {
					str = str.Trim().ToLowerInvariant();
					if (str.StartsWith(PREFIX_CHAR.ToString()) == false) {
						value = PREFIX_CHAR + str;
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
				object obj = base.ConvertTo(context, culture, value, destinationType);
				string str = obj as string;
				if (str != null) {
					if (str.StartsWith(PREFIX_CHAR.ToString())) {
						return str.TrimStart(PREFIX_CHAR);
					}
				}
				return obj;
			}
		}
		public class CyclesConverter : UInt16Converter {
			public const string CYCLES_MAX = "max";
			public const string CYCLES_AUTO = "auto";
			public CyclesConverter()
				: base() {
			}
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
				string str = value as string;
				if (str != null) {
					str = str.Trim().ToLowerInvariant();
					if (str == CYCLES_MAX) {
						value = UInt16.MaxValue.ToString();
					}
					else if (str == CYCLES_AUTO) {
						value = 0.ToString();
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
				object obj = base.ConvertTo(context, culture, value, destinationType);
				string str = obj as string;
				if (str != null) {
					if (str == UInt16.MaxValue.ToString()) {
						return CYCLES_MAX;
					}
					else if (str == 0.ToString()) {
						return CYCLES_AUTO;
					}
				}
				return obj;
			}
		}
		public class CmdStringConverter : MultilineStringConverter {
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
				if (destinationType == null) {
					throw new ArgumentNullException("destinationType");
				}
				if ((destinationType == typeof(string)) && (value is string)) {
					string[] lines = (value as string).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
					if (lines.Length > 1) {
						return lines[0] + "...";
					}
					else if (lines.Length == 1) {
						return lines[0];
					}
					return string.Empty;
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}

		public const string SECTION_SDL = "sdl";
		public const string SECTION_DOSBOX = "dosbox";
		public const string SECTION_RENDER = "render";
		public const string SECTION_CPU = "cpu";
		public const string SECTION_MIXER = "mixer";
		public const string SECTION_MIDI = "midi";
		public const string SECTION_SBLASTER = "sblaster";
		public const string SECTION_GUS = "gus";
		public const string SECTION_SPEAKER = "speaker";
		public const string SECTION_JOYSTICK = "joystick";
		public const string SECTION_SERIAL = "serial";
		public const string SECTION_DOS = "dos";
		public const string SECTION_IPX = "ipx";
		public const string SECTION_AUTOEXEC = "autoexec";

		class ConfigMeta {
			public PropertyInfo PropertyInfo;
			public TypeConverter TypeConverter;
		}

		static DosboxConfig() {
			s_AllProperties = new Dictionary<string, List<ConfigMeta>>();
			List<ConfigMeta> metalist;
			foreach (PropertyInfo pi in typeof(DosboxConfig).GetProperties(BindingFlags.Instance | BindingFlags.Public)) {
				string cat = ((CategoryOrderAttribute) pi.GetCustomAttributes(typeof(CategoryOrderAttribute), true)[0]).Category;
				if (cat != DosboxConfig.SECTION_AUTOEXEC) {
					ConfigMeta meta = new ConfigMeta();
					meta.PropertyInfo = pi;
					object[] attribs = pi.GetCustomAttributes(typeof(TypeConverterAttribute), true);
					if (attribs.Length > 0) {
						string converterTypeString = ((TypeConverterAttribute) attribs[0]).ConverterTypeName;
						meta.TypeConverter = (TypeConverter) Type.GetType(converterTypeString).GetConstructor(Type.EmptyTypes).Invoke(null);
					}
					else {
						meta.TypeConverter = (TypeConverter) TypeDescriptor.GetConverter(pi.PropertyType);
					}
					if (s_AllProperties.ContainsKey(cat)) {
						metalist = s_AllProperties[cat];
					}
					else {
						metalist = new List<ConfigMeta>();
						s_AllProperties.Add(cat, metalist);
					}
					metalist.Add(meta);
				}
				else {
				}
			}
		}

		public static void SaveTo(INI ini, DosboxConfig config) {
			write_section(ini, DosboxConfig.SECTION_SDL, config);
			write_section(ini, DosboxConfig.SECTION_DOSBOX, config);
			write_section(ini, DosboxConfig.SECTION_RENDER, config);
			write_section(ini, DosboxConfig.SECTION_CPU, config);
			write_section(ini, DosboxConfig.SECTION_MIXER, config);
			write_section(ini, DosboxConfig.SECTION_MIDI, config);
			write_section(ini, DosboxConfig.SECTION_SBLASTER, config);
			write_section(ini, DosboxConfig.SECTION_GUS, config);
			write_section(ini, DosboxConfig.SECTION_SPEAKER, config);
			write_section(ini, DosboxConfig.SECTION_JOYSTICK, config);
			write_section(ini, DosboxConfig.SECTION_SERIAL, config);
			write_section(ini, DosboxConfig.SECTION_DOS, config);
			write_section(ini, DosboxConfig.SECTION_IPX, config);
			ini.Section = DosboxConfig.SECTION_AUTOEXEC;
			ini.WriteSection(config.cmd);
		}

		public static void SaveProperty(INI ini, string section, string name, object value) {
			ini.Section = section;
			ConfigMeta metaToSave = s_AllProperties[section].Find(
				new Predicate<ConfigMeta>(
					delegate(ConfigMeta meta) {
						return meta.PropertyInfo.Name.ToLowerInvariant() == name.ToLowerInvariant();
					}
				)
			);
			if (metaToSave != null) {
				ini.Write(metaToSave.PropertyInfo.Name, metaToSave.TypeConverter.ConvertToInvariantString(value));
			}
		}

		public static void LoadFrom(INI ini, DosboxConfig config) {
			if (File.Exists(ini.FileName)) {
				read_section(ini, DosboxConfig.SECTION_SDL, config);
				read_section(ini, DosboxConfig.SECTION_DOSBOX, config);
				read_section(ini, DosboxConfig.SECTION_RENDER, config);
				read_section(ini, DosboxConfig.SECTION_CPU, config);
				read_section(ini, DosboxConfig.SECTION_MIXER, config);
				read_section(ini, DosboxConfig.SECTION_MIDI, config);
				read_section(ini, DosboxConfig.SECTION_SBLASTER, config);
				read_section(ini, DosboxConfig.SECTION_GUS, config);
				read_section(ini, DosboxConfig.SECTION_SPEAKER, config);
				read_section(ini, DosboxConfig.SECTION_JOYSTICK, config);
				read_section(ini, DosboxConfig.SECTION_SERIAL, config);
				read_section(ini, DosboxConfig.SECTION_DOS, config);
				read_section(ini, DosboxConfig.SECTION_IPX, config);
				ini.Section = DosboxConfig.SECTION_AUTOEXEC;
				config.cmd = ini.ReadSection();
			}
		}

		public static CategoryOrderAttribute GetCategoryFromDescriptor(PropertyDescriptor descriptor) {
			foreach (Attribute attri in descriptor.Attributes) {
				CategoryOrderAttribute cat = attri as CategoryOrderAttribute;
				if (cat != null) {
					return cat;
				}
			}
			return null;
		}

		static Dictionary<string, List<ConfigMeta>> s_AllProperties;

		static void write_section(INI ini, string section, DosboxConfig config) {
			ini.Section = section;
			foreach (ConfigMeta meta in s_AllProperties[section]) {
				ini.Write(meta.PropertyInfo.Name, meta.TypeConverter.ConvertToInvariantString(meta.PropertyInfo.GetValue(config, null)));
			}
		}

		static void read_section(INI ini, string section, DosboxConfig config) {
			ini.Section = section;
			foreach (ConfigMeta meta in s_AllProperties[section]) {
				string s = ini.Read(meta.PropertyInfo.Name);
				if (string.IsNullOrEmpty(s) == false) {
					object val = null;
					try {
						val = meta.TypeConverter.ConvertFromInvariantString(s);
					}
					catch {
						continue;
					}
					meta.PropertyInfo.SetValue(config, val, null);
				}
			}
		}

		static void write_section_all(INI ini, string section, DosboxConfig config) {
		}

		static void read_section_all(INI ini, string section, DosboxConfig config) {
		}

		public DosboxConfig() {
			m_fullscreen = DEFAULT_FULLSCREEN;
			m_fulldouble = DEFAULT_FULLDOUBLE;
			m_fullresolution = (ResolutionType) TypeDescriptor.GetConverter(typeof(ResolutionType)).ConvertFromInvariantString(DEFAULT_FULLRESOLUTION);
			m_windowresolution = (ResolutionType) TypeDescriptor.GetConverter(typeof(ResolutionType)).ConvertFromInvariantString(DEFAULT_WINDOWRESOLUTION);
			m_output = DEFAULT_OUTPUT;
			m_autolock = DEFAULT_AUTOLOCK;
			m_sensitivity = (UInt16) TypeDescriptor.GetConverter(typeof(UInt16)).ConvertFromInvariantString(DEFAULT_SENSITIVITY);
			m_waitonerror = DEFAULT_WAITONERROR;
			m_priority = (PriorityType) TypeDescriptor.GetConverter(typeof(PriorityType)).ConvertFromInvariantString(DEFAULT_PRIORITY);
			m_mapperfile = DEFAULT_MAPPERFILE;
			m_usescancodes = DEFAULT_USESCANCODES;

			m_language = DEFAULT_LANGUAGE;
			m_memsize = (UInt16) TypeDescriptor.GetConverter(typeof(UInt16)).ConvertFromInvariantString(DEFAULT_MEMSIZE);
			m_machine = DEFAULT_MACHINE;
			m_captures = DEFAULT_CAPTURES;

			m_frameskip = (UInt16) TypeDescriptor.GetConverter(typeof(UInt16)).ConvertFromInvariantString(DEFAULT_FRAMESKIP);
			m_aspect = DEFAULT_ASPECT;
			m_scaler = DEFAULT_SCALER;

			m_core = DEFAULT_CORE;
			m_cputype = DEFAULT_CPUTYPE;
			m_cycles = (UInt16) new CyclesConverter().ConvertFromInvariantString(DEFAULT_CYCLES);
			m_cycleup = (UInt16) TypeDescriptor.GetConverter(typeof(UInt16)).ConvertFromInvariantString(DEFAULT_CYCLEUP);
			m_cycledown = (UInt16) TypeDescriptor.GetConverter(typeof(UInt16)).ConvertFromInvariantString(DEFAULT_CYCLEDOWN);

			m_nosound = DEFAULT_NOSOUND;
			m_rate = DEFAULT_RATE;
			m_blocksize = DEFAULT_BLOCKSIZE;
			m_prebuffer = (UInt16) TypeDescriptor.GetConverter(typeof(UInt16)).ConvertFromInvariantString(DEFAULT_PREBUF);

			m_mpu401 = DEFAULT_MPU401;
			m_mididevice = DEFAULT_MIDIDEVICE;
			m_midiconfig = DEFAULT_MIDICONFIG;

			m_sbtype = DEFAULT_SBTYPE;
			m_sbbase = DEFAULT_SBBASE;
			m_irq = DEFAULT_IRQ;
			m_dma = DEFAULT_DMA;
			m_hdma = DEFAULT_HDMA;
			m_sbmixer = DEFAULT_SBMIXER;
			m_oplmode = DEFAULT_OPLMODE;
			m_oplemu = DEFAULT_OPLEMU;
			m_oplrate = DEFAULT_OPLRATE;

			m_gus = DEFAULT_GUS;
			m_gusrate = DEFAULT_GUSRATE;
			m_gusbase = DEFAULT_GUSBASE;
			m_gusirq = DEFAULT_GUSIRQ;
			m_gusdma = DEFAULT_GUSDMA;
			m_ultradir = DEFAULT_ULTRADIR;

			m_pcspeaker = DEFAULT_PCSPEAKER;
			m_pcrate = DEFAULT_PCRATE;
			m_tandy = DEFAULT_TANDY;
			m_tandyrate = DEFAULT_TANDYRATE;
			m_disney = DEFAULT_DISNEY;

			m_joysticktype = DEFAULT_JOYSTICKTYPE;
			m_timed = DEFAULT_TIMED;
			m_autofired = DEFAULT_AUTOFIRED;
			m_swap34 = DEFAULT_SWAP34;
			m_buttonwrap = DEFAULT_BUTTONWRAP;

			m_serial1 = DEFAULT_SERIAL1;
			m_serial2 = DEFAULT_SERIAL2;
			m_serial3 = DEFAULT_SERIAL3;
			m_serial4 = DEFAULT_SERIAL4;

			m_xms = DEFAULT_XMS;
			m_ems = DEFAULT_EMS;
			m_umb = DEFAULT_UMB;
			m_keyboardlayout = DEFAULT_KEYBOARDLAYOUT;

			m_ipx = DEFAULT_IPX;

			m_cmd = DEFAULT_CMD;
		}

		#region SDL
		[TypeConverter(typeof(ResolutionTypeConverter))]
		public struct ResolutionType {
			static ResolutionType() {
				Empty = new ResolutionType();
			}
			public static readonly ResolutionType Empty;
			public ResolutionType(UInt16 width, UInt16 height) {
				m_width = width;
				m_height = height;
			}
			public UInt16 width {
				set { m_width = value; }
				get { return m_width; }
			}
			public UInt16 height {
				set { m_height = value; }
				get { return m_height; }
			}
			[Browsable(false)]
			public bool IsEmpty {
				get { return m_width == 0 && m_height == 0; }
			}
			UInt16 m_width;
			UInt16 m_height;
		}
		public enum OutputValue { surface, overlay, opengl, openglnb, ddraw };
		public enum PriorityValue { lowerest, lower, normal, higher, highest, pause };
		[TypeConverter(typeof(PriorityTypeConverter))]
		public struct PriorityType {
			public PriorityType(PriorityValue when_focused, PriorityValue when_minimzed) {
				m_when_focused = when_focused;
				m_when_minimzed = when_minimzed;
			}
			[DefaultValue(PriorityValue.higher)]
			public PriorityValue when_focused {
				set { m_when_focused = value; }
				get { return m_when_focused; }
			}
			[DefaultValue(PriorityValue.normal)]
			public PriorityValue when_minimzed {
				set { m_when_minimzed = value; }
				get { return m_when_minimzed; }
			}
			PriorityValue m_when_focused;
			PriorityValue m_when_minimzed;
		}

		public const bool DEFAULT_FULLSCREEN = false;
		public const bool DEFAULT_FULLDOUBLE = false;
		public const string DEFAULT_FULLRESOLUTION = ResolutionTypeConverter.RESOLUTION_ORIGINAL;
		public const string DEFAULT_WINDOWRESOLUTION = ResolutionTypeConverter.RESOLUTION_ORIGINAL;
		public const OutputValue DEFAULT_OUTPUT = OutputValue.surface;
		public const bool DEFAULT_AUTOLOCK = true;
		public const string DEFAULT_SENSITIVITY = "100";
		public const bool DEFAULT_WAITONERROR = true;
		public const string DEFAULT_PRIORITY = "higher,normal";
		public const string DEFAULT_MAPPERFILE = @"mapper.txt";
		public const bool DEFAULT_USESCANCODES = true;

		[CategoryOrder(SECTION_SDL, 0)]
		[DefaultValue(DEFAULT_FULLSCREEN)]
		[Description("Start DOSBox directly in fullscreen.")]
		public bool fullscreen {
			set { m_fullscreen = value; }
			get { return m_fullscreen; }
		}
		[CategoryOrder(SECTION_SDL)]
		[DefaultValue(DEFAULT_FULLDOUBLE)]
		[Description("Use double buffering in fullscreen.")]
		public bool fulldouble {
			set { m_fulldouble = value; }
			get { return m_fulldouble; }
		}
		[CategoryOrder(SECTION_SDL)]
		[DefaultValue(typeof(ResolutionType), DEFAULT_FULLRESOLUTION)]
		[Description("The resolution to use for fullscreen. If original is specified, DOSBox will try to switch the screen resolution to best match the resolution request by the application. For example, if a game in DOSBox is requesting a graphics screen resolution of (320 x 240) while your desktop is (1280 x 1024), DOSBox will perform the switch or try other resolutions that closely matches it, e.g. (400 x 300) if (320 x 240) is not available.")]
		public ResolutionType fullresolution {
			set { m_fullresolution = value; }
			get { return m_fullresolution; }
		}
		[CategoryOrder(SECTION_SDL)]
		[DefaultValue(typeof(ResolutionType), DEFAULT_WINDOWRESOLUTION)]
		[Description("Scale the window to this size IF the output device supports hardware scaling (i.e. any output other than surface). Note that the window size actually has a fixed 1.6 aspect ratio, so if you ask for 1280x1024, you'll end up with a squished 1280x800 window. original means 1x zoom and will resize as the emulator switches graphics modes.")]
		public ResolutionType windowresolution {
			set { m_windowresolution = value; }
			get { return m_windowresolution; }
		}
		[CategoryOrder(SECTION_SDL)]
		[DefaultValue(DEFAULT_OUTPUT)]
		[Description("What to use for output.")]
		public OutputValue output {
			set { m_output = value; }
			get { return m_output; }
		}
		[CategoryOrder(SECTION_SDL)]
		[DefaultValue(DEFAULT_AUTOLOCK)]
		[Description("Mouse will automatically lock, if you click on the screen.")]
		public bool autolock {
			set { m_autolock = value; }
			get { return m_autolock; }
		}
		[CategoryOrder(SECTION_SDL)]
		[DefaultValue(typeof(UInt16), DEFAULT_SENSITIVITY)]
		[Description("Mouse sensitivity.")]
		public UInt16 sensitivity {
			set { m_sensitivity = value; }
			get { return m_sensitivity; }
		}
		[CategoryOrder(SECTION_SDL)]
		[DefaultValue(DEFAULT_WAITONERROR)]
		[Description("Wait before closing the console if DOSBox has an error.")]
		public bool waitonerror {
			set { m_waitonerror = value; }
			get { return m_waitonerror; }
		}
		[CategoryOrder(SECTION_SDL)]
		[DefaultValue(typeof(PriorityType), DEFAULT_PRIORITY)]
		[Description("Priority levels for DOSBox. Second entry behind the comma is for when DOSBox is not focused/minimized.")]
		public PriorityType priority {
			set { m_priority = value; }
			get { return m_priority; }
		}
		[CategoryOrder(SECTION_SDL)]
		[DefaultValue(DEFAULT_MAPPERFILE)]
		[Description("File used to load/save the key/event mappings from.")]
		[Editor(typeof(FileNameEditorEx), typeof(UITypeEditor))]
		public string mapperfile {
			set { m_mapperfile = value; }
			get { return m_mapperfile; }
		}
		[CategoryOrder(SECTION_SDL)]
		[DefaultValue(DEFAULT_USESCANCODES)]
		[Description("Avoid usage of symkeys, might not work on all operating systems.")]
		public bool usescancodes {
			set { m_usescancodes = value; }
			get { return m_usescancodes; }
		}

		bool m_fullscreen;
		bool m_fulldouble;
		ResolutionType m_fullresolution;
		ResolutionType m_windowresolution;
		OutputValue m_output;
		bool m_autolock;
		UInt16 m_sensitivity;
		bool m_waitonerror;
		PriorityType m_priority;
		string m_mapperfile;
		bool m_usescancodes;
		#endregion

		#region DOSBOX
		public enum MachineValue { hercules, cga, tandy, pcjr, ega, vgaonly, svga_s3, svga_et3000, svga_et4000, svga_paradise, vesa_nolfb, vesa_oldvbe };

		public const string DEFAULT_LANGUAGE = @"";
		public const string DEFAULT_MEMSIZE = "16";
		public const MachineValue DEFAULT_MACHINE = MachineValue.svga_s3;
		public const string DEFAULT_CAPTURES = @"capture";

		[CategoryOrder(SECTION_DOSBOX, 1)]
		[DefaultValue(DEFAULT_LANGUAGE)]
		[Description("Select another language file.")]
		[Editor(typeof(FileNameEditorEx), typeof(UITypeEditor))]
		public string language {
			set { m_language = value; }
			get { return m_language; }
		}
		[CategoryOrder(SECTION_DOSBOX)]
		[DefaultValue(typeof(UInt16), DEFAULT_MEMSIZE)]
		[Description("Amount of high memory (in megabytes) available to programs.")]
		public UInt16 memsize {
			set { m_memsize = value; }
			get { return m_memsize; }
		}
		[CategoryOrder(SECTION_DOSBOX)]
		[DefaultValue(DEFAULT_MACHINE)]
		[Description("The type of machine (specifically the type of graphics hardware) DOSBox tries to emulate.")]
		public MachineValue machine {
			set { m_machine = value; }
			get { return m_machine; }
		}
		[CategoryOrder(SECTION_DOSBOX)]
		[DefaultValue(DEFAULT_CAPTURES)]
		[Description("Directory where things like music (wave and MIDI) and screenshots are captured when special keys CTRL-F5 and CTRL-F6 are used. Screenshots will be captured and saved as (PNG) files with a resolution of 320x200.")]
		[Editor(typeof(FileNameEditorEx), typeof(UITypeEditor))]
		public string captures {
			set { m_captures = value; }
			get { return m_captures; }
		}

		string m_language;
		UInt16 m_memsize;
		MachineValue m_machine;
		string m_captures;
		#endregion

		#region RENDER
		public enum ScalerValue { normal2x, normal3x, advmame2x, advmame3x, advinterp2x, advinterp3x, tv2x, tv3x, rgb2x, rgb3x, scan2x, scan3x };

		public const string DEFAULT_FRAMESKIP = "0";
		public const bool DEFAULT_ASPECT = false;
		public const ScalerValue DEFAULT_SCALER = ScalerValue.normal2x;

		[CategoryOrder(SECTION_RENDER, 2)]
		[DefaultValue(typeof(UInt16), DEFAULT_FRAMESKIP)]
		[Description("How many frames DOSBox skips before drawing one.")]
		public UInt16 frameskip {
			set { m_frameskip = value; }
			get { return m_frameskip; }
		}
		[CategoryOrder(SECTION_RENDER)]
		[DefaultValue(DEFAULT_ASPECT)]
		[Description("Do aspect correction. It only affects non-square pixel modes (like for example Mode 13h, which has a resolution of 320x200 pixels and it's used by many DOS games), and it's needed to get correct aspect ratio on square-pixel screens in windowed mode.")]
		public bool aspect {
			set { m_aspect = value; }
			get { return m_aspect; }
		}
		[CategoryOrder(SECTION_RENDER)]
		[DefaultValue(DEFAULT_SCALER)]
		[Description("Specifies which scaler is used to enlarge and enhance low resolution modes.")]
		public ScalerValue scaler {
			set { m_scaler = value; }
			get { return m_scaler; }
		}

		UInt16 m_frameskip;
		bool m_aspect;
		ScalerValue m_scaler;
		#endregion

		#region CPU
		public enum CoreValue { simple, normal, full, dynamic, auto };
		[TypeConverter(typeof(PrefixedEnumConverter))]
		public enum CputypeValue { _auto, _386, _386_slow, _486_slow, _pentium_slow, _386_prefetch };

		public const CoreValue DEFAULT_CORE = CoreValue.auto;
		public const CputypeValue DEFAULT_CPUTYPE = CputypeValue._auto;
		public const string DEFAULT_CYCLES = "0";
		public const string DEFAULT_CYCLEUP = "500";
		public const string DEFAULT_CYCLEDOWN = "20";

		[CategoryOrder(SECTION_CPU, 3)]
		[DefaultValue(DEFAULT_CORE)]
		[Description("CPU core used in emulation. auto switches to dynamic if appropriate.")]
		public CoreValue core {
			set { m_core = value; }
			get { return m_core; }
		}
		[CategoryOrder(SECTION_CPU)]
		[DefaultValue(DEFAULT_CPUTYPE)]
		[Description("CPU Type used in emulation. auto is the fastest choice.")]
		public CputypeValue cputype {
			set { m_cputype = value; }
			get { return m_cputype; }
		}
		[CategoryOrder(SECTION_CPU)]
		[DefaultValue(typeof(UInt16), DEFAULT_CYCLES)]
		[Description("Amount of instructions DOSBox tries to emulate each millisecond. Set to max to automatically run as many cycles as possible. auto setting switches to max if appropriate.")]
		[TypeConverter(typeof(CyclesConverter))]
		public UInt16 cycles {
			set { m_cycles = value; }
			get { return m_cycles; }
		}
		[CategoryOrder(SECTION_CPU)]
		[DefaultValue(typeof(UInt16), DEFAULT_CYCLEUP)]
		[Description("Amount of cycles to increase with keycombo.")]
		public UInt16 cycleup {
			set { m_cycleup = value; }
			get { return m_cycleup; }
		}
		[CategoryOrder(SECTION_CPU)]
		[DefaultValue(typeof(UInt16), DEFAULT_CYCLEDOWN)]
		[Description("Amount of cycles to decrease with keycombo.")]
		public UInt16 cycledown {
			set { m_cycledown = value; }
			get { return m_cycledown; }
		}

		CoreValue m_core;
		CputypeValue m_cputype;
		UInt16 m_cycles;
		UInt16 m_cycleup;
		UInt16 m_cycledown;
		#endregion

		#region MIXER
		[TypeConverter(typeof(PrefixedEnumConverter))]
		public enum RateValue { _8000, _11025, _16000, _22050, _32000, _44100, _48000, _49716 };
		[TypeConverter(typeof(PrefixedEnumConverter))]
		public enum BlocksizeValue { _256, _512, _1024, _2048, _8192, _4096 };

		public const bool DEFAULT_NOSOUND = false;
		public const RateValue DEFAULT_RATE = RateValue._22050;
		public const BlocksizeValue DEFAULT_BLOCKSIZE = BlocksizeValue._2048;
		public const string DEFAULT_PREBUF = "10";

		[CategoryOrder(SECTION_MIXER, 4)]
		[DefaultValue(DEFAULT_NOSOUND)]
		[Description("Enable silent mode, sound is still emulated though.")]
		public bool nosound {
			set { m_nosound = value; }
			get { return m_nosound; }
		}
		[CategoryOrder(SECTION_MIXER)]
		[DefaultValue(DEFAULT_RATE)]
		[Description("Mixer sample rate, setting any device's rate higher than this will probably lower their sound quality.")]
		public RateValue rate {
			set { m_rate = value; }
			get { return m_rate; }
		}
		[CategoryOrder(SECTION_MIXER)]
		[DefaultValue(DEFAULT_BLOCKSIZE)]
		[Description("Mixer block size, larger blocks might help sound stuttering but sound will also be more lagged.")]
		public BlocksizeValue blocksize {
			set { m_blocksize = value; }
			get { return m_blocksize; }
		}
		[CategoryOrder(SECTION_MIXER)]
		[DefaultValue(typeof(UInt16), DEFAULT_PREBUF)]
		[Description("How many milliseconds of data to keep on top of the blocksize.")]
		public UInt16 prebuffer {
			set { m_prebuffer = value; }
			get { return m_prebuffer; }
		}

		bool m_nosound;
		RateValue m_rate;
		BlocksizeValue m_blocksize;
		UInt16 m_prebuffer;
		#endregion

		#region MIDI
		public enum Mpu401Value { intelligent, uart, none };
		[TypeConverter(typeof(PrefixedEnumConverter))]
		public enum MidideviceValue { _default, _win32, _alsa, _oss, _coreaudio, _coremidi, _none };

		public const Mpu401Value DEFAULT_MPU401 = Mpu401Value.intelligent;
		public const MidideviceValue DEFAULT_MIDIDEVICE = MidideviceValue._default;
		public const string DEFAULT_MIDICONFIG = "";

		[CategoryOrder(SECTION_MIDI, 5)]
		[DefaultValue(DEFAULT_MPU401)]
		[Description("Type of MPU-401 to emulate.")]
		public Mpu401Value mpu401 {
			set { m_mpu401 = value; }
			get { return m_mpu401; }
		}
		[CategoryOrder(SECTION_MIDI)]
		[DefaultValue(DEFAULT_MIDIDEVICE)]
		[Description("Device that will receive the MIDI data from MPU-401.")]
		public MidideviceValue mididevice {
			set { m_mididevice = value; }
			get { return m_mididevice; }
		}
		[CategoryOrder(SECTION_MIDI)]
		[DefaultValue(DEFAULT_MIDICONFIG)]
		[Description("Special configuration options for the device driver. This is usually the id of the device you want to use.")]
		public string midiconfig {
			set { m_midiconfig = value; }
			get { return m_midiconfig; }
		}

		Mpu401Value m_mpu401;
		MidideviceValue m_mididevice;
		string m_midiconfig;
		#endregion

		#region SBLASTER
		public enum SbtypeValue { sb1, sb2, sbpro1, sbpro2, sb16, none };
		[TypeConverter(typeof(PrefixedEnumConverter))]
		public enum IobaseValue { _220, _240, _260, _280, _2a0, _2c0, _2e0, _300 };
		[TypeConverter(typeof(PrefixedEnumConverter))]
		public enum IrqValue { _3, _5, _7, _9, _10, _11, _12 };
		[TypeConverter(typeof(PrefixedEnumConverter))]
		public enum DmaValue { _0, _1, _3, _5, _6, _7 };
		public enum OplmodeValue { auto, cms, opl2, dualopl2, opl3, none };
		[TypeConverter(typeof(PrefixedEnumConverter))]
		public enum OplemuValue { _default, _compat, _fast, _old };

		public const SbtypeValue DEFAULT_SBTYPE = SbtypeValue.sb16;
		public const IobaseValue DEFAULT_SBBASE = IobaseValue._220;
		public const IrqValue DEFAULT_IRQ = IrqValue._7;
		public const DmaValue DEFAULT_DMA = DmaValue._1;
		public const DmaValue DEFAULT_HDMA = DmaValue._5;
		public const bool DEFAULT_SBMIXER = true;
		public const OplmodeValue DEFAULT_OPLMODE = OplmodeValue.auto;
		public const OplemuValue DEFAULT_OPLEMU = OplemuValue._default;
		public const RateValue DEFAULT_OPLRATE = RateValue._22050;

		[CategoryOrder(SECTION_SBLASTER, 6)]
		[DefaultValue(DEFAULT_SBTYPE)]
		[Description("Type of sblaster to emulate.")]
		public SbtypeValue sbtype {
			set { m_sbtype = value; }
			get { return m_sbtype; }
		}
		[CategoryOrder(SECTION_SBLASTER)]
		[DefaultValue(DEFAULT_SBBASE)]
		[Description("The IO address of the soundblaster.")]
		public IobaseValue sbbase {
			set { m_sbbase = value; }
			get { return m_sbbase; }
		}
		[CategoryOrder(SECTION_SBLASTER)]
		[DefaultValue(DEFAULT_IRQ)]
		[Description("The IRQ number of the soundblaster.")]
		public IrqValue irq {
			set { m_irq = value; }
			get { return m_irq; }
		}
		[CategoryOrder(SECTION_SBLASTER)]
		[DefaultValue(DEFAULT_DMA)]
		[Description("The DMA number of the soundblaster.")]
		public DmaValue dma {
			set { m_dma = value; }
			get { return m_dma; }
		}
		[CategoryOrder(SECTION_SBLASTER)]
		[DefaultValue(DEFAULT_HDMA)]
		[Description("The High DMA number of the soundblaster.")]
		public DmaValue hdma {
			set { m_hdma = value; }
			get { return m_hdma; }
		}
		[CategoryOrder(SECTION_SBLASTER)]
		[DefaultValue(DEFAULT_SBMIXER)]
		[Description("Allow the soundblaster mixer to modify the DOSBox mixer.")]
		public bool sbmixer {
			set { m_sbmixer = value; }
			get { return m_sbmixer; }
		}
		[CategoryOrder(SECTION_SBLASTER)]
		[DefaultValue(DEFAULT_OPLMODE)]
		[Description("Type of OPL emulation. On 'auto' the mode is determined by sblaster type. All OPL modes are Adlib-compatible, except for 'cms'.")]
		public OplmodeValue oplmode {
			set { m_oplmode = value; }
			get { return m_oplmode; }
		}
		[CategoryOrder(SECTION_SBLASTER)]
		[DefaultValue(DEFAULT_OPLEMU)]
		[Description("Provider for the OPL emulation. compat or old might provide better quality (see oplrate as well).")]
		public OplemuValue oplemu {
			set { m_oplemu = value; }
			get { return m_oplemu; }
		}
		[CategoryOrder(SECTION_SBLASTER)]
		[DefaultValue(DEFAULT_OPLRATE)]
		[Description("Sample rate of OPL music emulation. Use 49716 for highest quality (set the mixer rate accordingly).")]
		public RateValue oplrate {
			set { m_oplrate = value; }
			get { return m_oplrate; }
		}

		SbtypeValue m_sbtype;
		IobaseValue m_sbbase;
		IrqValue m_irq;
		DmaValue m_dma;
		DmaValue m_hdma;
		bool m_sbmixer;
		OplmodeValue m_oplmode;
		OplemuValue m_oplemu;
		RateValue m_oplrate;
		#endregion

		#region GUS
		public const bool DEFAULT_GUS = false;
		public const RateValue DEFAULT_GUSRATE = RateValue._22050;
		public const IobaseValue DEFAULT_GUSBASE = IobaseValue._240;
		public const IrqValue DEFAULT_GUSIRQ = IrqValue._5;
		public const DmaValue DEFAULT_GUSDMA = DmaValue._3;
		public const string DEFAULT_ULTRADIR = @"C:\ULTRASND";

		[CategoryOrder(SECTION_GUS, 7)]
		[DefaultValue(DEFAULT_GUS)]
		[Description("Enable the Gravis Ultrasound emulation.")]
		public bool gus {
			set { m_gus = value; }
			get { return m_gus; }
		}
		[CategoryOrder(SECTION_GUS)]
		[DefaultValue(DEFAULT_GUSRATE)]
		[Description("Sample rate of Ultrasound emulation.")]
		public RateValue gusrate {
			set { m_gusrate = value; }
			get { return m_gusrate; }
		}
		[CategoryOrder(SECTION_GUS)]
		[DefaultValue(DEFAULT_GUSBASE)]
		[Description("The IO base address of the Gravis Ultrasound.")]
		public IobaseValue gusbase {
			set { m_gusbase = value; }
			get { return m_gusbase; }
		}
		[CategoryOrder(SECTION_GUS)]
		[DefaultValue(DEFAULT_GUSIRQ)]
		[Description("The IRQ number of the Gravis Ultrasound.")]
		public IrqValue gusirq {
			set { m_gusirq = value; }
			get { return m_gusirq; }
		}
		[CategoryOrder(SECTION_GUS)]
		[DefaultValue(DEFAULT_GUSDMA)]
		[Description("The DMA channel of the Gravis Ultrasound.")]
		public DmaValue gusdma {
			set { m_gusdma = value; }
			get { return m_gusdma; }
		}
		[CategoryOrder(SECTION_GUS)]
		[DefaultValue(DEFAULT_ULTRADIR)]
		[Description("Path to Ultrasound directory. In this directory there should be a MIDI directory that contains the patch files for GUS playback. Patch sets used with Timidity should work fine.")]
		[Editor(typeof(FolderNameEditorEx), typeof(UITypeEditor))]
		public string ultradir {
			set { m_ultradir = value; }
			get { return m_ultradir; }
		}

		bool m_gus;
		RateValue m_gusrate;
		IobaseValue m_gusbase;
		IrqValue m_gusirq;
		DmaValue m_gusdma;
		string m_ultradir;
		#endregion

		#region SPEAKER
		public enum TandyValue { auto, on, off };

		public const bool DEFAULT_PCSPEAKER = true;
		public const RateValue DEFAULT_PCRATE = RateValue._22050;
		public const TandyValue DEFAULT_TANDY = TandyValue.auto;
		public const RateValue DEFAULT_TANDYRATE = RateValue._22050;
		public const bool DEFAULT_DISNEY = true;

		[CategoryOrder(SECTION_SPEAKER, 8)]
		[DefaultValue(DEFAULT_PCSPEAKER)]
		[Description("Enable PC-Speaker emulation.")]
		public bool pcspeaker {
			set { m_pcspeaker = value; }
			get { return m_pcspeaker; }
		}
		[CategoryOrder(SECTION_SPEAKER)]
		[DefaultValue(DEFAULT_PCRATE)]
		[Description("Sample rate of the PC-Speaker sound generation.")]
		public RateValue pcrate {
			set { m_pcrate = value; }
			get { return m_pcrate; }
		}
		[CategoryOrder(SECTION_SPEAKER)]
		[DefaultValue(DEFAULT_TANDY)]
		[Description("Enable Tandy Sound System emulation. For 'auto', emulation is present only if machine is set to 'tandy'.")]
		public TandyValue tandy {
			set { m_tandy = value; }
			get { return m_tandy; }
		}
		[CategoryOrder(SECTION_SPEAKER)]
		[DefaultValue(DEFAULT_TANDYRATE)]
		[Description("Sample rate of the Tandy 3-Voice generation.")]
		public RateValue tandyrate {
			set { m_tandyrate = value; }
			get { return m_tandyrate; }
		}
		[CategoryOrder(SECTION_SPEAKER)]
		[DefaultValue(DEFAULT_DISNEY)]
		[Description("Enable Disney Sound Source emulation. (Covox Voice Master and Speech Thing compatible).")]
		public bool disney {
			set { m_disney = value; }
			get { return m_disney; }
		}

		bool m_pcspeaker;
		RateValue m_pcrate;
		TandyValue m_tandy;
		RateValue m_tandyrate;
		bool m_disney;
		#endregion

		#region JOYSTICK
		[TypeConverter(typeof(PrefixedEnumConverter))]
		public enum JoysticktypeValue {
			[Description("Chooses emulation depending on real joystick(s).")]
			_auto,
			[Description("Supports two joysticks")]
			_2axis,
			[Description("Supports one joystick, first joystick used")]
			_4axis,
			[Description("Supports one joystick, second joystick used")]
			_4axis_2,
			[Description("Thrustmaster")]
			_fcs,
			[Description("CH Flightstick")]
			_ch,
			[Description("Disables joystick emulation")]
			_none
		};

		public const JoysticktypeValue DEFAULT_JOYSTICKTYPE = JoysticktypeValue._auto;
		public const bool DEFAULT_TIMED = true;
		public const bool DEFAULT_AUTOFIRED = false;
		public const bool DEFAULT_SWAP34 = false;
		public const bool DEFAULT_BUTTONWRAP = true;

		[CategoryOrder(SECTION_JOYSTICK, 9)]
		[DefaultValue(DEFAULT_JOYSTICKTYPE)]
		[Description("Type of joystick to emulate")]
		public JoysticktypeValue joysticktype {
			set { m_joysticktype = value; }
			get { return m_joysticktype; }
		}
		[CategoryOrder(SECTION_JOYSTICK)]
		[DefaultValue(DEFAULT_TIMED)]
		[Description("Enable timed intervals for axis. (False is old style behaviour).")]
		public bool timed {
			set { m_timed = value; }
			get { return m_timed; }
		}
		[CategoryOrder(SECTION_JOYSTICK)]
		[DefaultValue(DEFAULT_AUTOFIRED)]
		[Description("Continuously fires as long as you keep the button pressed.")]
		public bool autofired {
			set { m_autofired = value; }
			get { return m_autofired; }
		}
		[CategoryOrder(SECTION_JOYSTICK)]
		[DefaultValue(DEFAULT_SWAP34)]
		[Description("Swap the 3rd and the 4th axis. Can be useful for certain joysticks.")]
		public bool swap34 {
			set { m_swap34 = value; }
			get { return m_swap34; }
		}
		[CategoryOrder(SECTION_JOYSTICK)]
		[DefaultValue(DEFAULT_BUTTONWRAP)]
		[Description("Enable button wrapping at the number of emulated buttons.")]
		public bool buttonwrap {
			set { m_buttonwrap = value; }
			get { return m_buttonwrap; }
		}

		JoysticktypeValue m_joysticktype;
		bool m_timed;
		bool m_autofired;
		bool m_swap34;
		bool m_buttonwrap;
		#endregion

		#region SERIAL
		public enum SerialValue { dummy, disabled, modem, nullmodem, directserial };

		public const SerialValue DEFAULT_SERIAL1 = SerialValue.dummy;
		public const SerialValue DEFAULT_SERIAL2 = SerialValue.dummy;
		public const SerialValue DEFAULT_SERIAL3 = SerialValue.disabled;
		public const SerialValue DEFAULT_SERIAL4 = SerialValue.disabled;

		[CategoryOrder(SECTION_SERIAL, 10)]
		[DefaultValue(DEFAULT_SERIAL1)]
		[Description("Set type of device connected to com port.")]
		public SerialValue serial1 {
			set { m_serial1 = value; }
			get { return m_serial1; }
		}
		[CategoryOrder(SECTION_SERIAL)]
		[DefaultValue(DEFAULT_SERIAL2)]
		[Description("Set type of device connected to com port.")]
		public SerialValue serial2 {
			set { m_serial2 = value; }
			get { return m_serial2; }
		}
		[CategoryOrder(SECTION_SERIAL)]
		[DefaultValue(DEFAULT_SERIAL3)]
		[Description("Set type of device connected to com port.")]
		public SerialValue serial3 {
			set { m_serial3 = value; }
			get { return m_serial3; }
		}
		[CategoryOrder(SECTION_SERIAL)]
		[DefaultValue(DEFAULT_SERIAL4)]
		[Description("Set type of device connected to com port.")]
		public SerialValue serial4 {
			set { m_serial4 = value; }
			get { return m_serial4; }
		}

		SerialValue m_serial1;
		SerialValue m_serial2;
		SerialValue m_serial3;
		SerialValue m_serial4;
		#endregion

		#region DOS
		public const bool DEFAULT_XMS = true;
		public const bool DEFAULT_EMS = true;
		public const bool DEFAULT_UMB = true;
		public const string DEFAULT_KEYBOARDLAYOUT = "auto";

		[CategoryOrder(SECTION_DOS, 11)]
		[DefaultValue(DEFAULT_XMS)]
		[Description("Enable XMS support.")]
		public bool xms {
			set { m_xms = value; }
			get { return m_xms; }
		}
		[CategoryOrder(SECTION_DOS)]
		[DefaultValue(DEFAULT_EMS)]
		[Description("Enable EMS support.")]
		public bool ems {
			set { m_ems = value; }
			get { return m_ems; }
		}
		[CategoryOrder(SECTION_DOS)]
		[DefaultValue(DEFAULT_UMB)]
		[Description("Enable UMB support.")]
		public bool umb {
			set { m_umb = value; }
			get { return m_umb; }
		}
		[CategoryOrder(SECTION_DOS)]
		[DefaultValue(DEFAULT_KEYBOARDLAYOUT)]
		[Description("Language code of the keyboard layout (or none).")]
		public string keyboardlayout {
			set { m_keyboardlayout = value; }
			get { return m_keyboardlayout; }
		}

		bool m_xms;
		bool m_ems;
		bool m_umb;
		string m_keyboardlayout;
		#endregion

		#region IPX
		public const bool DEFAULT_IPX = false;

		[CategoryOrder(SECTION_IPX, 12)]
		[DefaultValue(DEFAULT_IPX)]
		[Description("Enable ipx over UDP/IP emulation.")]
		public bool ipx {
			set { m_ipx = value; }
			get { return m_ipx; }
		}

		bool m_ipx;
		#endregion

		#region AUTOEXEC
		public const string DEFAULT_CMD = "";

		[CategoryOrder(SECTION_AUTOEXEC, 13)]
		[DefaultValue(DEFAULT_CMD)]
		[Description("Lines in this section will be run at startup.")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		[TypeConverter(typeof(CmdStringConverter))]
		public string cmd {
			set { m_cmd = value; }
			get { return m_cmd; }
		}

		string m_cmd;
		#endregion
	}
}