using System;
using System.Net.Sockets;
using System.Threading;
using DominionClient.Events;
using DominionClient.Screens;
using DServer.Clients;
using DServer.Commands;

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
		private fmLobby LobbyScreen => _lobbyScreen ?? (_lobbyScreen = new fmLobby());
		private fmLogin LoginScreen => _loginScreen ?? (_loginScreen = new fmLogin());
		#endregion Properties

		#region Events
		public event UserConnectedHandler OnUserConnected;
		#endregion Events

		#region Delegates
		public delegate void UserConnectedHandler(object sender, UserConnectedEvent e);
		#endregion Delegates

		#region Public Methods
		public void Run()
		{
			LoginScreen.OnLogin += UpdateUsername;

			LoginScreen.ShowDialog();

			Client.Connect();

			WaitForStart();
			LobbyScreen.ShowDialog();

			GameClient.ShowDialog();
			
			
		}

		private void UpdateUsername(object sender, LoginEvent e)
		{
			Client.Username = e.Username;
		}

		public void WaitForStart()
		{
			Thread thread = new Thread(() =>
			{
				while (true)
				{
					GameInfo gameInfo = Client.ReadGameInfo();

					if (gameInfo == null)
					{
						break;
					}

					HandleGameInfo(gameInfo);

					//ControlInvoke(lvwPlayers, () =>
					//{
					//	//need to be able to call HandleGameInfo
					//	UpdatePlayers(command);
					//});

				}
			});
			thread.Start();
		}

		private void HandleGameInfo(GameInfo gameInfo)
		{
			switch (gameInfo.Command.Action)
			{
				case ActionType.Connected:

					LobbyScreen.UpdatePlayers(gameInfo.GameData.Players);
					break;
				case ActionType.Disconnected:
					LobbyScreen.UpdatePlayers(gameInfo.GameData.Players);
					break;
			}
		}



		#endregion Public methods

		#region Private Methods

		#endregion Private Methods
	}
}