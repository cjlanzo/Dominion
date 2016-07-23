using System;
using System.Windows.Forms;

namespace DominionClient
{
	public partial class fmLogin : Form
	{
		public string Username { get; set; }

		public fmLogin()
		{
			InitializeComponent();
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			Username = rtfUsername.Text;
			//rtfUsername.Enabled = false;
			//Close();

		}

		private void LoginScreen_Load(object sender, EventArgs e)
		{

		}
	}
}
