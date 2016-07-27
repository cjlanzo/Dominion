using System;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using DServer;
using DServer.Clients;
using DServer.Commands;
using DServer.Utilities;

namespace DominionClient.Screens
{
	public partial class fmLobby : Form
	{
		#region Delegates
		//private delegate void AddPlayerDelegate(string username, bool ready);
		//private delegate void AddPlayerCallback(string username, bool ready, Control control);
		private delegate void ControlInvokeDelegate();
		#endregion Delegates

		#region Member Variables
		//private GameModel _gameModel;
		#endregion Member Variables

		#region Properties
		//private GameModel GameModel => _gameModel ?? (_gameModel = GameModel.Instance);
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructs an instance of a Lobby
		/// </summary>
		public fmLobby()
		{
			InitializeComponent();
		}
		#endregion Constructors

		#region Event Handlers
		/// <summary>
		/// Handles the Ready button being clicked
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void btnReady_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Runs when the lobby is launched
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void fmLobby_Load(object sender, EventArgs e)
		{
			//lvwPlayers.Columns.Add("Username", lvwPlayers.Width / 2);
			//lvwPlayers.Columns.Add("Status", lvwPlayers.Width / 2);

			//ListViewItem player = new ListViewItem(new string[] {"lanzo", "not ready"});
			//lvwPlayers.Items.Add(player);
		}

		//public void WaitForStart(TcpClient tcpClient)
		//{
		//	byte[] buffer = new byte[100];

		//	new Thread(() =>
		//	{

		//		NetworkStream stream = tcpClient.GetStream();
		//		stream.Read(buffer, 0, buffer.Length);

		//		Command command = new Command(buffer.ConvertToString());

		//		switch (command.Action)
		//		{
		//			case "Begin":
		//				break;
		//			case "Login":
		//				if (lvwPlayers.InvokeRequired)
		//				{
		//					AddPlayerCallback addPlayerCallback = AddPlayer;
		//					addPlayerCallback(command.Client, false);
		//				}
		//				else
		//				{
		//					AddPlayer(command.Client, false);
		//				}
		//				break;
		//			case "Logout":
		//				RemovePlayer(command.Client);
		//				break;
		//			case "Ready":
		//				UpdatePlayer(command.Client, true);
		//				break;
		//			default:
		//				Console.WriteLine(@"Invalid command");
		//				break;
		//		}
		//	}).Start();

		//}

		public void WaitForStart(ConnectedClient client)
		{
			

			new Thread(() =>
			{
				while (true)
				{
					Thread.Sleep(2000);
					client.SendMessage("a");

					Thread.Sleep(10000);

					string message = client.Read();

					ControlInvoke(lvwPlayers, () =>
					{
						lvwPlayers.Items.Clear();
						string[] users = message.Split(',');
						foreach (string user in users)
						{
							lvwPlayers.Items.Add(new ListViewItem(new string[]
							{
							user,
							"Not ready"
							}));
						}
					});

				}
			}).Start();

			//ThreadStart starter = () =>
			//{

			//	NetworkStream stream = tcpClient.GetStream();
			//	stream.Read(buffer, 0, buffer.Length);
			//};
			//starter += () =>
			//{
			//	ControlInvoke(lvwPlayers, () =>
			//	{
			//		lvwPlayers.Items.Clear();
			//		string[] users = buffer.ConvertToString().Split(',');
			//		foreach (string user in users)
			//		{
			//			lvwPlayers.Items.Add(new ListViewItem(new string[]
			//			{
			//				user,
			//				"Not ready"
			//			}));
			//		}
			//	});

				//uncomment this and integrate

				//Command command = new Command(buffer.ConvertToString());

				//switch (command.Action)
				//{
				//	case "Begin":
				//		break;
				//	case "Login":
				//		//ListViewItem[] updatedList = new ListViewItem[GameModel.ConnectedClients.Count];

				//		//for (int i = 0; i < GameModel.ConnectedClients.Count; i++)
				//		//{
				//		//	updatedList[i] = new ListViewItem(new string[]
				//		//	{
				//		//		GameModel.ConnectedClients[i].Username,
				//		//		GameModel.ConnectedClients[i].Ready ? "Ready" : "Not Ready"
				//		//	});
				//		//}
				//		//lvwPlayers.Items.
				//		ControlInvoke(lvwPlayers, () =>
				//		{
				//			lvwPlayers.Items.Clear();
				//			GameModel gameModel = GameModel.Acquire();
				//			foreach (ConnectedClient connectedClient in gameModel.ConnectedClients)
				//			{
				//				lvwPlayers.Items.Add(new ListViewItem(new string[]
				//				{
				//					connectedClient.Username,
				//					connectedClient.Ready ? "Ready" : "Not ready"
				//				}));
				//			}
				//			GameModel.Release();
				//		});
				//		break;
				//	case "Logout":
				//		RemovePlayer(command.Client);
				//		break;
				//	case "Ready":
				//		UpdatePlayer(command.Client, true);
				//		break;
				//	default:
				//		Console.WriteLine(@"Invalid command");
				//		break;
				//}
			//};
			//Thread thread = new Thread(starter) { IsBackground = true };
			//thread.Start();

		}

		private void ControlInvoke(Control control, Action function)
		{
			if (control.IsDisposed || control.Disposing)
			{
				return;
			}

			if (control.InvokeRequired)
			{
				control.Invoke(new ControlInvokeDelegate(() => ControlInvoke(control, function)));
				return;
			}

			function();
			//if (control.InvokeRequired)
			//{
			//	control.Invoke(new ControlInvokeDelegate(ControlInvoke), 
			//		control, 
			//		propertyName, 
			//		propertyValue);
			//}
			//else
			//{
			//	control.GetType().InvokeMember(
			//		propertyName,
			//		BindingFlags.SetProperty,
			//		null,
			//		control,
			//		new[] { propertyValue });
			//}
		}

		//private void AddPlayer(string username, bool ready)
		//{
		//	lvwPlayers.Invoke(new AddPlayerDelegate(AddPlayer), username, ready);
		//}

		//private void AddPlayer(string username, bool ready, Control control)
		//{
		//	if (control.InvokeRequired)
		//	{
		//		control.Invoke(new AddPlayerCallback(AddPlayer), username, ready, control);

		//	}
		//	else
		//	{
		//		control.GetType().InvokeMember(
		//			username,
		//			BindingFlags.SetProperty,
		//			null,
		//			control,
		//			new object[] { username, ready });
		//	}

		//	ListViewItem player = new ListViewItem(username);
		//	player.SubItems.Add(username);
		//	player.SubItems.Add(ready ? "Ready" : "Not Ready");
		//	lvwPlayers.Items.Add(player);
		//}

		private void RemovePlayer(string username)
		{
			foreach (ListViewItem playerToRemove in lvwPlayers.Items)
			{
				if (playerToRemove.SubItems[0].ToString() == username)
				{
					lvwPlayers.Items.Remove(playerToRemove);
				}
			}
		}

		private void UpdatePlayer(string username, bool ready)
		{
			ListViewItem player = new ListViewItem(username);
			player.SubItems.Add(username);
			player.SubItems.Add(ready ? "Ready" : "Not Ready");
			lvwPlayers.Items.Add(player);
		}

		//public void UpdatePlayersList(List<ConnectedClient> connectedClients)
		//{
		//	lvwPlayers.Items.Clear();

		//	foreach (ConnectedClient client in connectedClients)
		//	{
		//		ListViewItem player = new ListViewItem(client.Username);
		//		player.SubItems.Add(client.Username);
		//		player.SubItems.Add(client.Ready ? "Ready" : "Not Ready");
		//		lvwPlayers.Items.Add(player);
		//	}


		//}
		#endregion Event Handlers

		private void lvwPlayers_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			e.NewWidth = lvwPlayers.Columns[e.ColumnIndex].Width;
			e.Cancel = true;
		}
	}
}
