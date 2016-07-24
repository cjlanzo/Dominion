using System;
using System.Windows.Forms;
using DominionClient.Events;

namespace DominionClient.Screens
{
	public partial class fmGameClient : Form
	{
		#region Member Variables
		private ClientController _controller;
		#endregion Member Variables

		#region Properties
		private ClientController Controller => _controller ?? (_controller = new ClientController());
		private string Username { get; set; }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructs an instance of a Game Client
		/// </summary>
		public fmGameClient()
		{
			InitializeComponent();
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
			Controller.SendMessage($"{Username}:Chat:what's up");
		}

		/// <summary>
		/// Runs when the game client is launched
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void fmGameClient_Load(object sender, EventArgs e)
		{
			fmLogin loginScreen = new fmLogin();
			loginScreen.OnLogin += UpdateUsername;
			loginScreen.ShowDialog();

			Controller.SendMessage($"{Username}:Login");

			fmLobby lobbyScreen = new fmLobby();
			lobbyScreen.ShowDialog();

			Focus();
		}

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
	}
}