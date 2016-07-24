namespace DominionFramework.Loggers
{
	public interface ILogger
	{
		/// <summary>
		/// Creates a new directory to store the log output file
		/// </summary>
		/// <param name="message">Message to log</param>
		void LogMessage(string message);
	}
}