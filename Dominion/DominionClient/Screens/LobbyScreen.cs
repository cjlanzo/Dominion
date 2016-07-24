using System.Windows.Forms;

namespace DominionClient.Screens
{
	public partial class fmLobby : Form
	{
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
		private void btnReady_Click(object sender, System.EventArgs e)
		{

		}

		/// <summary>
		/// Runs when the lobby is launched
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void fmLobby_Load(object sender, System.EventArgs e)
		{

		}

		private void lvwPlayers_SelectedIndexChanged(object sender, System.EventArgs e)
		{

		}

		public void UpdatePlayersList(string name, bool ready)
		{
			ListViewItem player = new ListViewItem(name);
			player.SubItems.Add(name);
			player.SubItems.Add(ready ? "Ready" : "Not Ready");
			lvwPlayers.Items.Add(player);
		}
		#endregion Event Handlers
	}
}
