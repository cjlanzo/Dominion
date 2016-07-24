using System;
using System.Windows.Forms;
using DominionClient.Events;

namespace DominionClient.Screens
{
	public partial class fmLogin : Form
	{
		#region Events
		public delegate void LoginHandler(object sender, LoginEvent e);
		public event LoginHandler OnLogin;
		#endregion Events

		#region Constructors
		/// <summary>
		/// Constructs an instance of a Login Screen
		/// </summary>
		public fmLogin()
		{
			InitializeComponent();
		}
		#endregion Constructors

		#region Event Handlers
		/// <summary>
		/// Handles the Login button being clicked
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void btnLogin_Click(object sender, EventArgs e)
		{
			if (OnLogin == null)
			{
				return;
			}

			LoginEvent loginEvent = new LoginEvent(rtfUsername.Text);

			OnLogin(this, loginEvent);
			Close();
		}

		/// <summary>
		/// Runs when the login screen is launched
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void fmLogin_Load(object sender, EventArgs e)
		{

		}
		#endregion Event Handlers
	}
}
