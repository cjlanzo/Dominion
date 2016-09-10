using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using DominionClient.Screens;
using DominionFramework.Commands;
using DServer;
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
		private fmLobby LobbyScreen => _lobbyScreen ?? (_lobbyScreen = new fmLobby());
		private fmLogin LoginScreen => _loginScreen ?? (_loginScreen = new fmLogin());
		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Events
		//private delegate void ControlInvokeDelegate();
		//public delegate void UserConnected(object sender, UserConnected e);
		//public event UserConnected OnUserConnected;
		#endregion Events

		#region Public Methods

		

		public void Run()
		{
			//LoginScreen.OnLogin += UpdateUsername;
			
			LoginScreen.ShowDialog();
			Client.Username = LoginScreen.Username;

			Client.Connect();

			//separates sending username from first command
			//Thread.Sleep(2000);
			LobbyScreen.
			WaitForStart();
			LobbyScreen.ShowDialog();

			GameClient.ShowDialog();

			
		}

		public void WaitForStart()
		{
			Thread thread = new Thread(() =>
			{
				while (true)
				{
					string message = Client.Read();

					if (message == "Terminated")
					{
						break;
					}

					Command command = new Command(message);

					HandleCommand(command);

					//ControlInvoke(lvwPlayers, () =>
					//{
					//	//need to be able to call HandleCommand
					//	UpdatePlayers(command);
					//});

				}
			});
			thread.Start();
		}

		public void HandleCommand(Command command)
		{
			switch (command.Action)
			{
				case ActionType.Connected:
					
					LobbyScreen.UpdatePlayers(BuildPlayersList(command.Message));
					break;
				case ActionType.Disconnected:
					LobbyScreen.UpdatePlayers(BuildPlayersList(command.Message));
					break;
				case ActionType.Ready:
					LobbyScreen.UpdatePlayers(BuildPlayersList(command.Message));
					break;
					//case ActionType.Disconnected:
					//	return HandleDisconnected(command);
			}
		}

		private List<Player> BuildPlayersList(string playerString)
		{
			if (string.IsNullOrEmpty(playerString))
			{
				return null;
			}

			List<Player> playerList = new List<Player>();

			string[] players = playerString.Split(';');

			foreach (string player in players)
			{
				string[] p = player.Split(',');
				playerList.Add(new Player(p[0], p[1] == "Ready"));
			}

			return playerList;
		}

		private void ControlInvoke(Control control, Action function)
		{
			if (control.IsDisposed || control.Disposing)
			{
				return;
			}

			if (control.InvokeRequired)
			{
				control.Invoke(new fmLobby.ControlInvokeDelegate(() => ControlInvoke(control, function)));
				return;
			}

			function();
		}

		//private void HandleCommand(Command command)
		//{

		//}

		//private void RaiseEvent(Command command)
		//{
		//	switch (command.Action)
		//	{
		//		case ActionType.Connected:

		//	}

		//	string[] users = command.Message.Split(',');
		//}
		#endregion Public methods

		#region Private Methods

		//private Command HandleConnected(Command command)
		//{
		//	lvwPlayers.Items.Clear();
		//	string[] commands = message.Split(':');
		//	string[] users = commands[2].Split(',');
		//	foreach (string user in users)
		//	{
		//		lvwPlayers.Items.Add(new ListViewItem(new string[]
		//		{
		//			user,
		//			"Not ready"
		//		}));
		//	}
		//}
		#endregion Private Methods
	}
}