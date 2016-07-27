using System;
using System.Net.Sockets;
using System.Threading;
using DServer.Clients;
using DServer.Commands;
using DServer.Listeners;
using DServer.Utilities;

namespace DServer.Servers
{
	public class DominionServer : IDominionServer
	{
		#region Member Variables
		private IListener _listener;
		#endregion Member Variables

		#region Properties
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
		/// <param name="gameModel">Game model</param>
		public void Run()
		{
			ConnectedClient client = Listener.ListenForClient();
			
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
		/// <param name="gameModel">Game model</param>
		private void ManageClient(ConnectedClient client)
		{
			//NetworkStream stream = client.TcpClient.GetStream();

			ThreadStart starter = () =>
			{
				while (true)
				{
					//byte[] buffer = new byte[100];


					//stream.Read(buffer, 0, buffer.Length);
					//if (stream.Read(buffer, 0, buffer.Length) == 0)
					//{
					//	Console.WriteLine("Closing connection, no bytes received");
					//	stream.Close();
					//	client.Close();
					//	break;
					//}

					string message;
					client.Read(out message);

					Command command = new Command(message);

					client.Username = command.Client;

					

					if (command.Action == "Login")
					{
						GameModel gameModel = GameModel.Acquire();
						//GameModel.ConnectedClients.Add(client);
						gameModel.ConnectedClients.Add(client);
						GameModel.Release();
						Thread.Sleep(1000);

						//take this shit out
						//Console.WriteLine($"Connected clients: {GameModel.ConnectedClients.Count}");
						gameModel = GameModel.Acquire();
						Console.WriteLine($"Connected clients: {gameModel.ConnectedClients.Count}");
						GameModel.Release();
					}

					GameModel gm = GameModel.Acquire();
					foreach (ConnectedClient connectedClient in gm.ConnectedClients)
					//foreach (ConnectedClient connectedClient in GameModel.ConnectedClients)
					{
						//connectedClient.TcpClient.GetStream().Write(buffer, 0, buffer.Length);
						connectedClient.SendMessage(message);
					}
					GameModel.Release();

					if (command.Action != "Logout")
					{
						continue;
					}

					client.Close();
					break;
				}
			};
			starter += () =>
			{
				GameModel gameModel = GameModel.Acquire();
				gameModel.ConnectedClients.Remove(client);
				//GameModel.ConnectedClients.Remove(client);

				//take this out later
				//Console.WriteLine($"Connected clients: {GameModel.ConnectedClients.Count}");
				Console.WriteLine($"Connected clients: {gameModel.ConnectedClients.Count}");

				GameModel.Release();
			};

			Thread thread = new Thread(starter) { IsBackground = true };
			thread.Start();
		}
		#endregion Private Methods
	}
}