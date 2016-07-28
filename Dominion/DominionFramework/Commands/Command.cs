namespace DominionFramework.Commands
{
	public enum ActionType
	{
		Chat,
		Connected,
		Disconnected,
		Ready
	}

	public class Command
	{
		#region Constants
		private const int ClientIndex = 0;
		private const int ActionIndex = 1;
		private const int MessageIndex = 2;
		#endregion Constants

		#region Properties
		public ActionType? Action { get; }
		public string Message { get; }
		public string Username { get; }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructs a command
		/// </summary>
		/// <param name="input">Message to </param>
		public Command(string input)
		{
			string[] commands = input.Split(':');

			Username = commands[ClientIndex];
			Action = GetActionFromString(commands[ActionIndex]);

			if (commands.Length == 3)
			{
				Message = commands[MessageIndex];
			}
		}
		#endregion Constructors

		#region Public Methods
		/// <summary>
		/// Returns the components of the command as a string in the same format that it was received in
		/// </summary>
		/// <returns>The components of the command as a string</returns>
		public new string ToString()
		{
			string s = $"{Username}:{Action}";

			if (!string.IsNullOrEmpty(Message))
			{
				s += $":{Message}";
			}

			return s;
		}
		#endregion Public Methods

		#region Private Methods

		private ActionType? GetActionFromString(string s)
		{
			switch (s)
			{
				case "Chat":
					return ActionType.Chat;
				case "Connected":
					return ActionType.Connected;
				case "Disconnected":
					return ActionType.Disconnected;
				case "Ready":
					return ActionType.Ready;
				default:
					return null;
			}
		}
		#endregion Private Methods
	}
}