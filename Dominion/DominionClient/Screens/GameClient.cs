using System;
using System.Windows.Forms;
using DominionClient.Events;

namespace DominionClient.Screens
{
	public partial class fmGameClient : Form
	{
		private ClientController _controller;
		private string _username;

		public fmGameClient()
		{
			InitializeComponent();
		}

		private void Send_Click(object sender, EventArgs e)
		{
			_controller.SendMessage($"{_username}:Chat:what's up");
		}

		private void UpdateUsername(object sender, LoginEvent e)
		{
			_username = e.Username;
		}

		private void fmGameClient_Load(object sender, EventArgs e)
		{
			_controller = new ClientController();

			fmLogin loginScreen = new fmLogin();
			loginScreen.OnLogin += UpdateUsername;
			loginScreen.ShowDialog();

			_controller.SendMessage($"{_username}:Login");

			fmLobby lobbyScreen = new fmLobby();

			_con

			lobbyScreen.ShowDialog();

			Focus();
		}
	}
}