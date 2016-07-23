using System;
using System.Windows.Forms;

namespace DominionClient
{
	public partial class GameClient : Form
	{
		private ClientController _controller;
		private string Username;

		public GameClient(string username)
		{
			InitializeComponent();

			Username = username;
		}

		private void Send_Click(object sender, EventArgs e)
		{
			_controller.TestMethod(Username);
		}

		private void GUI_Load(object sender, EventArgs e)
		{
			_controller = new ClientController();
		}
	}
}