using System;

namespace DominionClient.Events
{
	public class LoginEvent : EventArgs
	{
		#region Properties
		public string Username { get; private set; }
		#endregion Properties

		#region Constructors
		public LoginEvent(string username)
		{
			Username = username;
		}
		#endregion Constructors
	}
}