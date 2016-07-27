using System;

namespace DServer.Commands
{
	public class Command
	{
		#region Properties
		public string Action { get; set; }
		public string Client { get; set; }
		public string Message { get; set; }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructs a command
		/// </summary>
		/// <param name="input">Message to </param>
		public Command(string input)
		{
			ParseInput(input);
		}
		#endregion Constructors

		#region Public Methods
		/// <summary>
		/// Returns the components of the command as a string in the same format that it was received in
		/// </summary>
		/// <returns>The components of the command as a string</returns>
		public new string ToString()
		{
			string s = $"{Client}:{Action}";

			if (!string.IsNullOrEmpty(Message))
			{
				s += $":{Message}";
			}

			return s;
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Parses the command input and stores the components of the command
		/// </summary>
		/// <param name="commandInput">Command input to parse</param>
		private void ParseInput(string commandInput)
		{
			string[] commands = commandInput.Split(':');

			Client = commands[0];
			Action = commands[1];
			Message = "";

			switch (Action)
			{
				case "Action":
					break;
				case "Chat":
					Message = commands[2];
					Console.WriteLine($"{Client} sent message: {Message}");
					break;
				case "Login":
					Console.WriteLine($"{Client} has connected to the server");
					break;
				case "Logout":
					Console.WriteLine($"{Client} has disconnected from the server");
					break;
				default:
					Console.WriteLine("Invalid command");
					break;
			}
		}
		#endregion Private Methods
	}
}