using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DominionFramework.Commands;
using DServer.Clients;
using DServer.Listeners;

namespace DServer.Servers
{
	public class DominionServer : IDominionServer
	{
		#region Member Variables
		private List<ConnectedClient> _connectedClients;
		private GameController _gameController;
		private IListener _listener;
		#endregion Member Variables

		#region Properties
		private List<ConnectedClient> ConnectedClients => _connectedClients ?? (_connectedClients = new List<ConnectedClient>());
		private GameController GameController => _gameController ?? (_gameController = new GameController());
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

			string input = client.Read();

			Command command = GameController.HandleCommand(new Command(input));
			client.Username = command.Username;

			Console.WriteLine($"{client.Username} has connected to the server");

			ConnectedClients.Add(client);
			BroadcastMessage(command);

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

		private void BroadcastMessage(Command command)
		{
			foreach (ConnectedClient client in ConnectedClients)
			{
				client.SendMessage(command.ToString());
			}
		}

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
					string input = client.Read();

					Command inputCommand = new Command(input);

					Command outputCommand = GameController.HandleCommand(inputCommand);

					BroadcastMessage(outputCommand);

					if (inputCommand.Action == ActionType.Disconnected)
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
		#endregion Private Methods
	}
}