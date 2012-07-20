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
		public static string CEPath { get; set; }

		/// <summary>
		/// Converts a Collada file to a CryENGINE-compatible CGF file.
		/// </summary>
		/// <param name="colladaPath">The path to the Collada file.</param>
		/// <returns>The path to the CGF file.</returns>
		public static string ToCgf(string colladaPath)
		{
			var startInfo = new ProcessStartInfo(Path.Combine(CEPath, "Bin32", "rc", "rc.exe"), "\"" + colladaPath + "\"") { RedirectStandardOutput = true, UseShellExecute = false };

			using(var process = Process.Start(startInfo))
				Log.Write(process.StandardOutput.ReadToEnd());

			return colladaPath.ToLower().Replace(".dae", ".cgf");
		}
	}
}
