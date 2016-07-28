using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using DominionClient.Screens;
using DominionFramework.Commands;
using DServer.Clients;

namespace DominionClient
{
	public class ClientController
	{
		#region Member Variables
		private ConnectedClient _client;
		private fmGameClient _gameClient;
		private fmLobby _lobbyScreen;
		private fmLogin _loginScreen;
		#endregion Member Varibles

		#region Properties
		private ConnectedClient Client => _client ?? (_client = new ConnectedClient(new TcpClient()));
		private fmGameClient GameClient => _gameClient ?? (_gameClient = new fmGameClient(Client));
		private fmLobby LobbyScreen => _lobbyScreen ?? (_lobbyScreen = new fmLobby(Client));
		private fmLogin LoginScreen => _loginScreen ?? (_loginScreen = new fmLogin());
		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Public Methods

		public Command HandleCommand(Command command)
		{
			switch (command.Action)
			{
				case ActionType.Connected:
					return HandleConnected(command);
				//case ActionType.Disconnected:
				//	return HandleDisconnected(command);
				default:
					return null;
			}
		}

		public void Run()
		{
			//LoginScreen.OnLogin += UpdateUsername;
			
			LoginScreen.ShowDialog();
			Client.Username = LoginScreen.Username;

			Client.Connect();

			//separates sending username from first command
			//Thread.Sleep(2000);

			LobbyScreen.WaitForStart(Client);
			LobbyScreen.ShowDialog();

			GameClient.ShowDialog();

			
		}
		#endregion Public methods

		#region Private Methods

		private Command HandleConnected(Command command)
		{
			lvwPlayers.Items.Clear();
			string[] commands = message.Split(':');
			string[] users = commands[2].Split(',');
			foreach (string user in users)
			{
				lvwPlayers.Items.Add(new ListViewItem(new string[]
				{
							user,
							"Not ready"
				}));
			}
		}
		#endregion Private Methods
	}
}