using System;
using System.Threading;
using System.Windows.Forms;
using DominionFramework.Commands;
using DServer.Clients;

namespace DominionClient.Screens
{
	public partial class fmLobby : Form
	{
		#region Delegates
		private delegate void ControlInvokeDelegate();
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
		public fmLobby(ConnectedClient client)
		{
			InitializeComponent();

			_client = client;
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
			_client.SendMessage($"{_client.Username}:{}");
		}

		/// <summary>
		/// Runs when the lobby is launched
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void fmLobby_Load(object sender, EventArgs e)
		{

		}

		public void WaitForStart(ConnectedClient client, Action function)
		{
			Thread thread = new Thread(() =>
			{
				while (true)
				{
					string message = client.Read();

					if (message == "Terminated")
					{
						break;
					}

					Command command = new Command(message);

					ControlInvoke(lvwPlayers, () =>
					{
						//need to be able to call HandleCommand
						UpdatePlayers(command);
					});

				}
			});
			thread.Start();
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
		}

		private void UpdatePlayers(Command command)
		{
			if (command.Message == null)
			{
				return;
			}

			lvwPlayers.Items.Clear();

			string[] users = command.Message.Split(',');

			foreach (string user in users)
			{
				lvwPlayers.Items.Add(new ListViewItem(new string[]
				{
					user,
					"Not ready"
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
