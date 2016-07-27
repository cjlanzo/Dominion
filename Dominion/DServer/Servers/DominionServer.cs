using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DServer.Clients;
using DServer.Commands;
using DServer.Listeners;

namespace DServer.Servers
{
	public class DominionServer : IDominionServer
	{
		#region Member Variables
		private List<ConnectedClient> _connectedClients;
		private IListener _listener;
		#endregion Member Variables

		#region Properties
		private List<ConnectedClient> ConnectedClients => _connectedClients ?? (_connectedClients = new List<ConnectedClient>());
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
			ConnectedClient client = Listener.ListenForClient();

			Console.WriteLine($"{client.Username} has connected to the server");

			ConnectedClients.Add(client);

			Console.WriteLine($"Connected clients: {ConnectedClients.Count}");

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
		private void ManageClient(ConnectedClient client)
		{
			new Thread(() =>
			{
				while (true)
				{
					try
					{
						string message = client.Read();

						//Command command = new Command(message);

						foreach (ConnectedClient recipient in ConnectedClients)
						{
							string users = "";

							foreach (ConnectedClient user in ConnectedClients)
							{
								users += user.Username;

								if (user != ConnectedClients.Last())
								{
									users += ",";
								}
							}
							recipient.SendMessage(users);
							//connectedClient.SendMessage(message);
						}

					}
					catch (Exception)
					{
						break;
					}
				}

				client.Close();

				Console.WriteLine($"{client.Username} has disconnected from the server");

				ConnectedClients.Remove(client);

				//take this out later
				Console.WriteLine($"Connected clients: {ConnectedClients.Count}");
			}).Start();
		}

		///// <summary>
		///// Creates a server thread that manages the connection between the client and the server
		///// </summary>
		///// <param name="client">The client connection to manage</param>
		//private void ManageClient(ConnectedClient client)
		//{
		//	ThreadStart starter = () =>
		//	{
		//		while (true)
		//		{
		//			string message;
		//			client.Read(out message);

		//			Command command = new Command(message);

		//			foreach (ConnectedClient connectedClient in ConnectedClients)
		//			{
		//				connectedClient.SendMessage(message);
		//			}

		//			if (command.Action != "Logout")
		//			{
		//				continue;
		//			}

		//			client.Close();
		//			break;
		//		}
		//	};
		//	starter += () =>
		//	{
		//		ConnectedClients.Remove(client);

		//		//take this out later
		//		Console.WriteLine($"Connected clients: {ConnectedClients.Count}");
		//	};

		//	Thread thread = new Thread(starter) { IsBackground = true };
		//	thread.Start();
		//}
		#endregion Private Methods
	}
}