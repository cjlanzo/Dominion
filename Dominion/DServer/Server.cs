using DominionFramework.Servers;

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
			server.Run();
		}
		#endregion Main
	}
}