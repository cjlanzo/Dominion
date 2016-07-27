using System;
using System.Net.Sockets;
using System.Windows.Forms;
using DominionClient.Events;
using DServer;
using DServer.Clients;
using DServer.Utilities;

namespace DominionClient.Screens
{
	public partial class fmGameClient : Form
	{
		#region Member Variables
		private ConnectedClient _client;
		//private static GameModel _gameModel;
		private fmLobby _lobbyScreen;
		private fmLogin _loginScreen;
		#endregion Member Variables

		#region Properties
		private ConnectedClient Client => _client ?? (_client = new ConnectedClient(new TcpClient()));
		//private static GameModel GameModel => _gameModel ?? (_gameModel = GameModel.Instance);
		private fmLobby LobbyScreen => _lobbyScreen ?? (_lobbyScreen = new fmLobby());
		private fmLogin LoginScreen => _loginScreen ?? (_loginScreen = new fmLogin());
		private string Message { get; set; }
		private string Username { get; set; }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructs an instance of a Game Client
		/// </summary>
		public fmGameClient()
		{
			InitializeComponent();

			FormClosing += fmGameClient_FormClosing;
		}
		#endregion Constructors

		#region Event Handlers
		/// <summary>
		/// Handles the send button being clicked
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void btnSend_Click(object sender, EventArgs e)
		{
			Client.SendMessage($"{Username}:Chat:what's up");
		}

		/// <summary>
		/// Runs when the game client is launched
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void fmGameClient_Load(object sender, EventArgs e)
		{
			Client.Connect();

			LoginScreen.OnLogin += UpdateUsername;
			LoginScreen.ShowDialog();

			Client.SendMessage($"{Username}:Login");

			//echo command from server - remove later
			string message;
			while (!Client.Read(out message))
			{
				
			}

			GameModel gameModel = GameModel.Acquire();
			MessageBox.Show($"[{message}],[{gameModel.ConnectedClients.Count}]");
			GameModel.Release();

			LobbyScreen.WaitForStart(Client.TcpClient);
			LobbyScreen.ShowDialog();

			Focus();
		}

		//private void ListenForMessages()
		//{

		//	Thread thread = new Thread(() =>
		//	{
		//		byte[] bytes = new byte[100];

		//		Controller.TcpClient.GetStream().Read(bytes, 0, bytes.Length);

		//		string message = bytes.ConvertToString();

		//		ParseMessage(message);
		//	});
		//	thread.Start();
		//}

		/// <summary>
		/// Updates the username field
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void UpdateUsername(object sender, LoginEvent e)
		{
			Username = e.Username;
		}
		#endregion Event Handlers

		#region Helper Methods
		//private void HandleLoginMessage()
		//{
		//	LobbyScreen.UpdatePlayersList(GameModel.ConnectedClients);
		//}

		//private void ParseMessage()
		//{
		//	string[] commands = Message.Split(':');

		//	switch (commands[1])
		//	{
		//		case "Action":
		//			break;
		//		case "Chat":
		//			break;
		//		case "Login":
		//			HandleLoginMessage(commands[0]);
		//			break;
		//		case "Logout":
		//			break;
		//		default:
		//			Console.WriteLine(@"Invalid message");
		//			break;
		//	}
		//}

		private void fmGameClient_FormClosing(object sender, FormClosingEventArgs e)
		{
			Client.SendMessage($"{Username}:Logout");
		}
		#endregion Helper Methods

		//private void Listener_DoWork(object sender, DoWorkEventArgs e)
		//{
		//	byte[] bytes = new byte[100];

		//	int bytesReceived = Controller.TcpClient.GetStream().Read(bytes, 0, bytes.Length);

		//	Message = ConvertBytesToString(bytes, bytesReceived);

		//	Listener.ReportProgress(100);
		//}

		//private void Listener_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		//{
		//	ParseMessage();
		//}
	}
}