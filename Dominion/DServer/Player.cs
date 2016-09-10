namespace DServer
{
	public class Player
	{
		#region Properties
		public bool Ready { get; set; }
		public string Username { get; set; }
		#endregion Properties

		#region Constructors

		public Player(string username)
		{
			Ready = false;
			Username = username;
		}

		public Player(string username, bool ready)
		{
			Ready = ready;
			Username = username;
		}
		#endregion Constructors
	}
}