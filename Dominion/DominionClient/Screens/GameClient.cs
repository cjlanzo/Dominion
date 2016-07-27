using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using DominionClient.Events;
using DServer.Clients;

namespace DominionClient.Screens
{
	public partial class fmGameClient : Form
	{
		#region Member Variables
		private ConnectedClient _client;
		private fmLobby _lobbyScreen;
		private fmLogin _loginScreen;
		#endregion Member Variables

		#region Properties
		private ConnectedClient Client => _client ?? (_client = new ConnectedClient(new TcpClient()));
		private fmLobby LobbyScreen => _lobbyScreen ?? (_lobbyScreen = new fmLobby());
		private fmLogin LoginScreen => _loginScreen ?? (_loginScreen = new fmLogin());
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
			Client.SendMessage($"{Client.Username}:Chat:what's up");
		}

		/// <summary>
		/// Runs when the game client is launched
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void fmGameClient_Load(object sender, EventArgs e)
		{
			LoginScreen.OnLogin += UpdateUsername;
			LoginScreen.ShowDialog();

			Client.Connect();

			Thread.Sleep(2000);

			//dummy message just to trigger, shoudl be replaced with actual command
			Client.SendMessage("a");

			LobbyScreen.WaitForStart(Client);
			LobbyScreen.ShowDialog();

			Focus();
		}

		/// <summary>
		/// Updates the username field
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void UpdateUsername(object sender, LoginEvent e)
		{
			Client.Username = e.Username;
		}
		#endregion Event Handlers

		#region Helper Methods

		private void fmGameClient_FormClosing(object sender, FormClosingEventArgs e)
		{
			//Client.SendMessage("Logout");
		}
		#endregion Helper Methods
	}
}