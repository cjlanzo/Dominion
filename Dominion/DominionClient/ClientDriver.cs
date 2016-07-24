using System;
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
			Application.Run(new fmGameClient());
		}
		#endregion Main
	}
}