using System;
using System.IO;

namespace DServer.Loggers
{
	public class Logger : ILogger
	{
		#region Constants
		private const string OutputDirectory = @"C:\Users\Chris\Documents\Dominion\Logs";
		#endregion Constants

		#region Public Methods
		/// <summary>
		/// Creates a new directory to store the log output file
		/// </summary>
		/// <param name="message">Message to log</param>
		public void LogMessage(string message)
		{
			DateTime timestamp = DateTime.Now;

			File.WriteAllText($"{OutputDirectory}\\{timestamp}\\Log.txt", message);
		}
		#endregion Public Methods
	}
}