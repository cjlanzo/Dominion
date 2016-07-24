using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using DominionFramework.Commands;
using DominionFramework.Listeners;
using DominionFramework.Utilities;

namespace DominionFramework.Servers
{
	public class DominionServer : IDominionServer
	{
		#region Member Variables
		private Dictionary<string, TcpClient> _connectedClients;
		//private List<TcpClient> _connectedClients;
		private IListener _listener;
		#endregion Member Variables

		#region Properties
		private Dictionary<string, TcpClient> ConnectedClients => _connectedClients ?? (_connectedClients = new Dictionary<string, TcpClient>());
		//private List<TcpClient> ConnectedClients => _connectedClients ?? (_connectedClients = new List<TcpClient>());
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
		/// Runs the server 
		/// </summary>
		public void Run()
		{
			TcpClient client = Listener.ListenForClient();
			ConnectedClients.Add(client.GetHashCode().ToString(), client);
			ManageClient(client);
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
			ThreadStart starter = () =>
			{
				while (true)
				{
					byte[] buffer = new byte[100];

					NetworkStream stream = client.GetStream();

					if (stream.Read(buffer, 0, buffer.Length) == 0)
					{
						Console.WriteLine("Closing connection, no bytes received");
						stream.Close();
						client.Close();
						break;
					}

					Command command = new Command(buffer.ConvertToString());

					if (command.Action != "Logout")
					{
						continue;
					}

					stream.Close();
					client.Close();
					break;
				}
			};
			starter += () =>
			{
				ConnectedClients.Remove(client.GetHashCode().ToString());

				Console.WriteLine("Connected clients:");

				foreach (string clientHash in ConnectedClients.Keys)
				{
					Console.WriteLine(clientHash);
				}
			};

			Thread thread = new Thread(starter) { IsBackground = true };
			thread.Start();
		}
		#endregion Private Methods
	}
}