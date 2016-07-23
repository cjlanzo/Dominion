using System;
using System.Threading;
using System.Windows.Forms;

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

			string username = HandleLoginScreen();

			Application.Run(new GameClient(username));


		}
		#endregion Main

		private static string HandleLoginScreen()
		{
			fmLogin loginScreen = new fmLogin();
			//loginScreen.Show();
			Application.Run(loginScreen);

			//loginScreen.

			while (string.IsNullOrEmpty(loginScreen.Username))
			{
				Thread.Sleep(1000);
			}

			string currentUser = loginScreen.Username;
			
			loginScreen.Close();

			return currentUser;

		}
	}
}