using System;
using System.Threading;
using System.Windows.Forms;
using DominionClient.Screens;

namespace DominionClient
{
	public static class ClientDriver
	{
		#region Main
		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//fmLogin loginScreen = new fmLogin();
			//loginScreen.Show();
			//Application.Run(new fmLogin());

			//string username = HandleLoginScreen();
			//fmGameClient gameClient = new fmGameClient();
			//gameClient.Show();
			//gameClient.Hide();
			Application.Run(new fmGameClient());



		}
		#endregion Main

	}
}