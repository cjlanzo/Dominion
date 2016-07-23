using System;

namespace DominionClient.Events
{
	public class LoginEvent : EventArgs
	{
		public string Username { get; private set; }

		public LoginEvent(string username)
		{
			Username = username;
		}
	}
}