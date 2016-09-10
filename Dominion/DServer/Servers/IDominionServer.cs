namespace DServer.Servers
{
	public interface IDominionServer
	{
		/// <summary>
		/// Broadcasts a message to all connected clients
		/// </summary>
		/// <param name="message">Message to broadcast</param>
		void SendGameInfo(string message);

		/// <summary>
		/// Runs the server indefinitely
		/// </summary>
		/// <param name="gameModel">Game Model</param>
		void Run();

		/// <summary>
		/// Starts the server
		/// </summary>
		void Start();
	}
}