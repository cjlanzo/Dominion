using System;
using System.Collections.Generic;
using System.Net.Sockets;
using DominionFramework.Listeners;
using DominionFramework.Threads;

namespace DominionFramework.Servers
{
	public class DominionServer : IDominionServer
	{
		#region Member Variables
		private List<TcpClient> _connectedClients;
		private IListener _listener;
		#endregion Member Variables

		#region Properties
		private List<TcpClient> ConnectedClients => _connectedClients ?? (_connectedClients = new List<TcpClient>());
		private IListener Listener => _listener ?? (_listener = new Listener());
		#endregion Properties

		#region Public Methods
		/// <summary>
		/// Broadcasts a message to all connected clients
		/// </summary>
		/// <param name="message">Message to broadcast</param>
		public void BroadcastMessage(string message)
		{
			//To do
		}

		/// <summary>
		/// Runs the server indefinitely
		/// </summary>
		public void Run()
		{
			while (true)
			{
				TcpClient client = Listener.ListenForClient();
				ConnectedClients.Add(client);
				ManageClient(client);
			}
		}

		/// <summary>
		/// Starts the server
		/// </summary>
		public void Start()
		{
			Listener.Start();

			Console.WriteLine($"The server is running at {Listener.GetLocation()}");
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Creates a server thread that manages the connection between the client and the server
		/// </summary>
		/// <param name="client">The client connection to manage</param>
		private void ManageClient(TcpClient client)
		{
			ServerThread thread = new ServerThread();
			thread.Start(client);
		}
		#endregion Private Methods
	}
}