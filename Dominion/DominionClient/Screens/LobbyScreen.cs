using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using DominionClient.Events;
using DServer;
using DServer.Clients;
using DServer.Commands;

namespace DominionClient.Screens
{
	public partial class fmLobby : Form
	{
		#region Delegates
		
		#endregion Delegates

		#region Member Variables
		private readonly ConnectedClient _client;
		private LobbyController _lobbyController;
		#endregion Member Variables

		#region Properties
		private LobbyController LobbyController => _lobbyController ?? (_lobbyController = new LobbyController());
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructs an instance of a Lobby
		/// </summary>
		public fmLobby()
		{
			InitializeComponent();

			//UserConnectedEvent += UpdatePlayers()

			//_client = client;
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
			//_client.SendCommand($"{_client.Username}:{ActionType.Ready}");
			_client.SendCommand(new Command(_client.Username, ActionType.Ready));

		}

		/// <summary>
		/// Runs when the lobby is launched
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void fmLobby_Load(object sender, EventArgs e)
		{

		}

		//public void UpdatePlayers(List<string> users)
		//{
			
		//}

		

		

		

		public void UpdatePlayers(List<Player> players)
		{
			if (players == null)
			{
				return;
			}

			lvwPlayers.Items.Clear();

			

			foreach (Player player in players)
			{
				lvwPlayers.Items.Add(new ListViewItem(new string[]
				{
					player.Username,
					player.Ready ? "Ready" : "Not Ready"
				}));
			}
		}

		#endregion Event Handlers

		private void lvwPlayers_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			e.NewWidth = lvwPlayers.Columns[e.ColumnIndex].Width;
			e.Cancel = true;
		}
	}
}
