namespace DominionFramework.Servers
{
	public interface IDominionServer
	{
		/// <summary>
		/// Broadcasts a message to all connected clients
		/// </summary>
		/// <param name="message">Message to broadcast</param>
		void BroadcastMessage(string message);

		/// <summary>
		/// Runs the server indefinitely
		/// </summary>
		void Run();

		/// <summary>
		/// Starts the server
		/// </summary>
		void Start();
	}
}