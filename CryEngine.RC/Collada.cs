using System;
using System.Diagnostics;
using System.IO;

namespace CryEngine.RC
{
	/// <summary>
	/// Provides utilities for the conversion of CryENGINE-style Collada files into binary meshes.
	/// </summary>
	public static class ColladaConverter
	{
		/// <summary>
		/// The path to the CryENGINE project folder.
		/// This is used to construct the path to the resource compiler, and must be assigned before saving CryENGINE binary files.
		/// </summary>
		public static DirectoryInfo CEPath { get; set; }

		/// <summary>
		/// Converts a Collada file to a CryENGINE-compatible CGF file.
		/// </summary>
		/// <param name="colladaPath">The path to the Collada file.</param>
		/// <returns>The path to the CGF file.</returns>
		public static FileInfo ToCgf(FileInfo colladaPath)
		{
			if(colladaPath == null)
				throw new ArgumentNullException("colladaPath");

			var startInfo = new ProcessStartInfo(Path.Combine(CEPath.FullName, "Bin32", "rc", "rc.exe"), "\"" + colladaPath + "\"") { RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true };

			using(var process = Process.Start(startInfo))
			{
				var info = process.StandardOutput.ReadToEnd();
				Logging.Write(info);
				process.WaitForExit();
			}

			return new FileInfo(colladaPath.FullName.ToLower().Replace(".dae", ".cgf"));
		}
	}
}
