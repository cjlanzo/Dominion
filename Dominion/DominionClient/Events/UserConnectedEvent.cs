using System;

namespace DominionClient.Events
{
	public class UserConnectedEvent : EventArgs
	{
		#region Properties
		public string Username { get; private set; }
		#endregion Properties

		#region Constructors
		public UserConnectedEvent(string username)
		{
			Username = username;
		}
		#endregion Constructors
	}
}
