using System;

namespace CryEngine.RC
{
	public enum LogType
	{
		Standard,
		Warning,
		Error
	}

	/// <summary>
	/// Provides logging utilities for the Resource Compiler wrapper.
	/// </summary>
	public static class Logging
	{
		/// <summary>
		/// This event is fired when a log message is written.
		/// </summary>
		public static event Action<string, LogType> Log;

		internal static void Write(string format, params object[] args)
		{
			if(Log != null)
				Log(string.Format(format, args), LogType.Standard);
		}

		internal static void WriteWarning(string format, params object[] args)
		{
			if(Log != null)
				Log(string.Format(format, args), LogType.Standard);
		}

		internal static void WriteError(string format, params object[] args)
		{
			if(Log != null)
				Log(string.Format(format, args), LogType.Standard);
		}
	}
}
