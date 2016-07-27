using DServer.Servers;

namespace DServer
{
	public static class Server
	{
		#region Main
		/// <summary>
		/// Main method to start the server
		/// </summary>
		public static void Main()
		{
			DominionServer server = new DominionServer();
			server.Start();

			//GameModel gameModel = GameModel.Instance;

			while (true)
			{
				server.Run();
			}
		}
		#endregion Main
	}
}