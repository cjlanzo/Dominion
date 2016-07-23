using System;
using System.Windows.Forms;

namespace DominionClient
{
	public static class Client
	{
		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new GUI());
		}
	}
}
