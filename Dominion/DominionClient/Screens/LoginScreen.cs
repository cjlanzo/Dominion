using System;
using System.Windows.Forms;
using DominionClient.Events;

namespace DominionClient.Screens
{
	public partial class fmLogin : Form
	{
		public delegate void LoginHandler(object sender, LoginEvent e);
		public event LoginHandler OnLogin;

		public fmLogin()
		{
			InitializeComponent();
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			if (OnLogin == null)
			{
				return;
			}

			LoginEvent loginEvent = new LoginEvent(rtfUsername.Text);

			OnLogin(this, loginEvent);

			//rtfUsername.Enabled = false;
			Close();

		}

		private void LoginScreen_Load(object sender, EventArgs e)
		{

		}
	}
}
