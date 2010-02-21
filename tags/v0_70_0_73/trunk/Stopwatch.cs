using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DosboxApp {
	static class Stopwatch {
		static Dictionary<string, DateTime> things = new Dictionary<string, DateTime>(20);

		static Stopwatch() {
			things.Add("Beginning", DateTime.Now);
		}

		public static void Mark(string desc) {
			things.Add(desc, DateTime.Now);
		}

		public static void Report() {
			StringBuilder sb = new StringBuilder(1024);
			KeyValuePair<string, DateTime>? prev = null;
			foreach (KeyValuePair<string, DateTime> mark in things) {
				if (prev != null) {
					sb.Append(mark.Value - prev.Value.Value);
					sb.Append(" spent in ");
					sb.Append(mark.Key);
					sb.Append('\n');
				}
				prev = mark;
			}
			MessageBox.Show(sb.ToString());
		}
	}
}
