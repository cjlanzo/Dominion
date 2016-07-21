using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace DominionClient
{
	public partial class GUI : Form
	{
		public GUI()
		{
			InitializeComponent();
		}

		private void Send_Click(object sender, EventArgs e)
		{
			const string ip = "10.0.0.25";
			const int port = 8001;

			try
			{
				TcpClient tcpClient = new TcpClient();
				tcpClient.Connect(ip, port);

				////string input = Console.ReadLine();
				//string input = @"Send this please";

				//Stream stream = tcpClient.GetStream();

				//ASCIIEncoding asen = new ASCIIEncoding();
				//byte[] ba = asen.GetBytes(input);

				//stream.Write(ba, 0, ba.Length);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
			}
		}
	}
}
