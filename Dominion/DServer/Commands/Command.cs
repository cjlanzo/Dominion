using System;
using System.Collections.Generic;

namespace DServer.Commands
{
	public enum ActionType
	{
		Connected,
		Disconnected,
		Ready
	}

	[Serializable]
	public class Command
	{
		#region Properties
		public ActionType Action { get; set; }
		public string Username { get; set; }
		#endregion Properties

		#region Constructors
		public Command(string username, ActionType action)
		{
			Username = username;
			Action = action;
		}
		#endregion Constructors
	}
}