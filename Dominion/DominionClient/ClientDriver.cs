using System;
using System.IO;
using System.Text;
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

			//Application.Run(new fmLogin());

			Application.Run(new GameClient());


		}
		#endregion Main
	}
}