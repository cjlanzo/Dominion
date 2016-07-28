using System;
using System.Windows.Forms;
using DominionClient.Events;
using DominionFramework.Commands;
using DServer.Clients;

namespace DominionClient.Screens
{
	public partial class fmGameClient : Form
	{
		#region Member Variables
		#endregion Member Variables

		#region Properties
		private readonly ConnectedClient _client;
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructs an instance of a Game Client
		/// </summary>
		public fmGameClient(ConnectedClient client)
		{
			InitializeComponent();

			_client = client;
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
			_client.SendMessage($"{_client.Username}:{ActionType.Chat}:what's up");
		}

		/// <summary>
		/// Runs when the game client is launched
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void fmGameClient_Load(object sender, EventArgs e)
		{
			
		}

		/// <summary>
		/// Updates the username field
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void UpdateUsername(object sender, LoginEvent e)
		{
			_client.Username = e.Username;
		}
		#endregion Event Handlers

		#region Helper Methods

		private void fmGameClient_FormClosing(object sender, FormClosingEventArgs e)
		{
			_client.SendMessage($"{_client.Username}:{ActionType.Disconnected}");
		}
		#endregion Helper Methods
	}
}