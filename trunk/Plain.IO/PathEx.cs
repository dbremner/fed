using System;
using System.IO;

namespace Plain.IO {
	public static class PathEx {
		public static string GetRelativePath(string src_path, string dst_name) {
			// Examples:
			// : c:\user\desk\top\
			// 0 c:\user\desk\top\			(.)
			// 1 c:\user\					(..\..)
			// 2 c:\user\tab\				(..\..\tab\)
			// 3 c:\us\						(..\..\..\us\)
			// 4 c:\user\desk\top\stuff\	(stuff\)
			// 5 c:\user\desk\topmost\		(..\topmost\)
			// 6 c:\user\desk\nested\		(..\nested\)
			// 7 e:\omg\					(e:\\omg\)
			src_path = Path.GetFullPath(src_path);
			if (src_path.EndsWith(Path.DirectorySeparatorChar.ToString()) == false) {
				src_path += Path.DirectorySeparatorChar;
			}
			dst_name = Path.GetFullPath(dst_name);
			if (dst_name.EndsWith(Path.DirectorySeparatorChar.ToString()) == false && Directory.Exists(dst_name) == true) {
				dst_name += Path.DirectorySeparatorChar;
			}
			if (Path.GetPathRoot(src_path).ToLowerInvariant() == Path.GetPathRoot(dst_name).ToLowerInvariant()) {
				int minlen = Math.Min(src_path.Length, dst_name.Length);
				int i = 0;
				for (; i < minlen; ++i) {
					if (char.ToLowerInvariant(src_path[i]) != char.ToLowerInvariant(dst_name[i])) {
						break;
					}
				}
				if (i == src_path.Length && i == dst_name.Length) {
					// Case 0.
					dst_name = ".";
				}
				else {
					if (i == minlen) {
						--i;
					}
					string pre = string.Empty, post = string.Empty;
					for (int j = (dst_name[i] == Path.DirectorySeparatorChar ? i : i - 1); j >= 0; --j) {
						if (src_path[j] == Path.DirectorySeparatorChar) {
							for (int k = j + 1; k < src_path.Length; ++k) {
								if (src_path[k] == Path.DirectorySeparatorChar) {
									pre += ".." + Path.DirectorySeparatorChar;
								}
							}
							break;
						}
					}
					for (int j = (src_path[i] == Path.DirectorySeparatorChar ? i : i - 1); j >= 0; --j) {
						if (dst_name[j] == Path.DirectorySeparatorChar) {
							post = dst_name.Substring(j + 1);
							break;
						}
					}
					dst_name = pre + post;
					System.Diagnostics.Debug.WriteLine(Path.GetFullPath(src_path + dst_name) + ": " + src_path + ", " + pre + ", " + post);
				}
			}
			// else case 7.
			return dst_name;
		}
	}
}
