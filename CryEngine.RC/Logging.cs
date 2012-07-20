using System;

namespace CryEngine.RC
{
	/// <summary>
	/// Provides logging utilities for the Resource Compiler wrapper.
	/// </summary>
	public static class Log
	{
		public static Action<string> Write { get; set; }

		static Log()
		{
			Write = Console.WriteLine;
		}
	}
}
