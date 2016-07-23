using System;
using System.Windows.Forms;
using DominionClient.Events;

namespace DominionClient.Screens
{
	public partial class fmGameClient : Form
	{
		private ClientController _controller;
		private string Username;

		public fmGameClient()
		{
			InitializeComponent();
		}

		private void Send_Click(object sender, EventArgs e)
		{
			_controller.TestMethod(Username);
		}

		private void UpdateUsername(object sender, LoginEvent e)
		{
			Username = e.Username;
		}

		private void fmGameClient_Load(object sender, EventArgs e)
		{
			_controller = new ClientController();

			fmLogin loginScreen = new fmLogin();
			loginScreen.OnLogin += UpdateUsername;
			loginScreen.ShowDialog();

		}
	}
}