using System;
using System.Windows.Forms;

namespace DominionClient
{
	public partial class GameClient : Form
	{
		private ClientController controller;

		public GameClient()
		{
			InitializeComponent();
		}

		private void Send_Click(object sender, EventArgs e)
		{
			controller.TestMethod();
		}

		private void GUI_Load(object sender, EventArgs e)
		{
			controller = new ClientController();
		}
	}
}