using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
		public void SendGameInfo(string message)
		{
			//To do
		}

		/// <summary>
		/// Runs the server 
		/// </summary>
		public void Run()
		{
			ConnectedClient client = Listener.ListenForClient();

			//string input = client.ReadCommand();
			Command command = (Command)new BinaryFormatter().Deserialize(client.TcpClient.GetStream());

			//Command command = GameController.HandleCommand(new Command(input));
			client.Username = command.Username;

			Console.WriteLine($"{client.Username} has connected to the server");

			ConnectedClients.Add(client);

			GameInfo gameInfo = GameController.HandleCommand(command);
			SendGameInfo(gameInfo);

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

		private void SendGameInfo(GameInfo gameInfo)
		{
			foreach (ConnectedClient client in ConnectedClients)
			{
				client.SendGameInfo(gameInfo);
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
					Command command = (Command)new BinaryFormatter().Deserialize(client.TcpClient.GetStream());

					//Console.WriteLine($"{command.Username}, {command.Action}");

					GameInfo gameInfo = GameController.HandleCommand(command);

					SendGameInfo(gameInfo);

					if (command.Action == ActionType.Disconnected)
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