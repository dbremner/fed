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
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace DosboxApp {
	public class Updater : IDisposable {
		public const string DOWNLOADSITE = "http://code.google.com/p/fed/downloads/list";

		public Updater() {
			m_ReleasesUri = new Uri("http://fed.googlecode.com/files/Releases.txt");
			m_LatestUpdate = new UpdateInfo();
			m_LatestUpdate.Version = new Version(Application.ProductVersion);
			m_DownloadedUpdate = new UpdateInfo();
		}

		~Updater() {
			Dispose();
		}

		#region IDisposable Members

		public void Dispose() {
			try {
				FileInfo fi = new FileInfo(m_DownloadedUpdate.Path);
				if (fi.Exists) {
					fi.Delete();
				}
			}
			catch { }
			finally {
				m_DownloadedUpdate.Path = null;
			}
		}

		#endregion

		public bool GUICheck(bool bVerbose) {
			// FIXME: remove blocking dialog (esp on startup)?
			// TODO: better message box (custom button text)?
			if (this.Check()) {
				if (this.IsUpdateAvailable) {
					if (Program.AppConfig.UpdateConfig.UpdateInstall) {
						if (this.IsUpdateCompatible) {
							if (this.DownloadAndVerify()) {
								DialogResult ans = MessageBox.Show("Version " + this.LatestVersion.ToString() + " is downloaded. Install and restart now?" + Environment.NewLine + "Otherwise, it will be installed when the program exits.", "Software Update", MessageBoxButtons.YesNoCancel);
								if (ans == DialogResult.Yes) {
									if (Program.Updater.InstallUpdate()) {
										Application.Restart();
										return true;
									}
									else {
										MessageBox.Show("Version " + this.LatestVersion.ToString() + " failed to be installed.");
									}
								}
								else if (ans == DialogResult.No) {
									Program.AppConfig.UpdateConfig.DelayedInstall = true;
								}
							}
							else if (bVerbose) {
								MessageBox.Show("Version " + this.LatestVersion.ToString() + " download cannot be completed at this time. Please try again later.", "Software Update");
							}
						}
						else {
							MessageBox.Show("Update is not compatible with this version of updater. Please go to " + Updater.DOWNLOADSITE + " to download the latest version.", "Software Update");
						}
					}
					else {
						MessageBox.Show("Version " + this.LatestVersion.ToString() + " is available.", "Software Update");
					}
				}
				else if (bVerbose) {
					MessageBox.Show("You are running the latest version.", "Software Update");
				}
			}
			else if (bVerbose) {
				MessageBox.Show("Update check cannot be completed at this time. Please try again later.", "Software Update");
			}
			return false;
		}

		public bool Check() {
			HttpWebRequest request = null;
			HttpWebResponse response = null;
			StreamReader reader = null;
			try {
				request = (HttpWebRequest) WebRequest.Create(m_ReleasesUri);
				response = (HttpWebResponse) request.GetResponse();
				if (response.StatusCode == HttpStatusCode.OK && response.ContentType.StartsWith("text/plain")) {
					reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
					string[] tokensFromRestOfLine = reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					if (tokensFromRestOfLine.Length == 3 && tokensFromRestOfLine[0].Length == MD5LENGTH) {
						m_LatestUpdate.MD5Sum = tokensFromRestOfLine[0];
						m_LatestUpdate.Version = new Version(tokensFromRestOfLine[1]);
						m_LatestUpdate.Path = tokensFromRestOfLine[2];
					}
					return true;
				}
			}
			catch { }
			finally {
				if (reader != null) {
					reader.Close();
				}
				if (response != null) {
					response.Close();
				}
				if (request != null) {
					request.Abort();
				}
			}
			return false;
		}

		public bool DownloadAndVerify() {
			if (verifyDownload(null)) {
				return true;
			}

			lock (m_LatestUpdate) {
				if (this.IsUpdateAvailable) {
					FileStream outStream = null;
					HttpWebRequest request = null;
					HttpWebResponse response = null;
					int length = 0;
					try {
						string tmpPath = Path.GetTempFileName();
						m_DownloadedUpdate.Path = tmpPath + ".exe";
						FileInfo fi = new FileInfo(tmpPath);
						fi.MoveTo(m_DownloadedUpdate.Path);

						outStream = fi.OpenWrite();
						request = (HttpWebRequest) WebRequest.Create(m_LatestUpdate.Path);
						response = (HttpWebResponse) request.GetResponse();
						if (response.StatusCode == HttpStatusCode.OK && response.ContentType.StartsWith("application/x-dosexec")) {
							length = (int) response.ContentLength;
							byte[] updateData = new byte[length];
							response.GetResponseStream().Read(updateData, 0, length);
							outStream.Write(updateData, 0, length);
							outStream.Close();
							outStream = null;

							// Compare checksum.
							if (verifyDownload(fi)) {
								return true;
							}
						}
					}
					catch { }
					finally {
						if (outStream != null) {
							outStream.Close();
						}
						if (response != null) {
							response.Close();
						}
					}
				}
			}
			return false;
		}

		public bool InstallUpdate() {
			if (m_DownloadedUpdate.IsCompatible) {
				string backupPath = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar
					+ Path.GetFileNameWithoutExtension(Application.ExecutablePath) + "." + Application.ProductVersion + ".exe";
				try {
					if (File.Exists(backupPath)) {
						File.Delete(backupPath);
					}
					string path = Application.ExecutablePath;
					File.Move(path, backupPath);
					try {
						File.Move(m_DownloadedUpdate.Path, path);
						return true;
					}
					catch (Exception ex) {
						MessageBox.Show(ex.Message, "Inner");
						File.Move(backupPath, path);
					}
				}
				catch (Exception ex) {
					MessageBox.Show(ex.Message, "Outer");
				}
			}
			return false;
		}

		public bool IsUpdateAvailable {
			get { return m_LatestUpdate.Version != null && m_LatestUpdate.Version > new Version(Application.ProductVersion); }
		}

		public bool IsUpdateCompatible {
			get { return m_LatestUpdate.IsCompatible; }
		}

		public Version LatestVersion {
			get { return m_LatestUpdate.Version; }
		}

		const int MD5LENGTH = 32;
		Uri m_ReleasesUri;
		UpdateInfo m_LatestUpdate, m_DownloadedUpdate;

		bool verifyDownload(FileInfo fi) {
			lock (m_LatestUpdate) {
				if (this.IsUpdateAvailable) {
					FileStream stream = null;
					try {
						if (fi == null) {
							fi = new FileInfo(m_DownloadedUpdate.Path);
						}
						stream = fi.OpenRead();
						MD5 md5 = MD5.Create();
						byte[] md5InBytes = md5.ComputeHash(stream);
						StringBuilder sb = new StringBuilder(MD5LENGTH);
						for (int i = 0; i < md5InBytes.Length; ++i) {
							sb.Append(md5InBytes[i].ToString("x2"));
						}

						m_DownloadedUpdate.MD5Sum = sb.ToString();
						if (m_DownloadedUpdate.MD5Sum == m_LatestUpdate.MD5Sum) {
							return true;
						}
					}
					catch { }
					finally {
						if (stream != null) {
							stream.Close();
						}
					}
				}
			}
			return false;
		}



		class UpdateInfo {
			public UpdateInfo() {
				MD5Sum = string.Empty;
			}
			public string MD5Sum;
			public Version Version;
			public string Path;
			public bool IsCompatible {
				get { return MD5Sum.Length == MD5LENGTH; }
			}
		}
	}
}
