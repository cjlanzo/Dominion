using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace DominionClient
{
	public partial class GUI : Form
	{
		const int port = 8001;

		private TcpClient tcpClient;

		public GUI()
		{
			InitializeComponent();

			tcpClient = new TcpClient();
			tcpClient.Connect(GetLocalIPAddress(), port);
		}

		private void Send_Click(object sender, EventArgs e)
		{
			try
			{
				////string input = Console.ReadLine();
				string input = @"Send this please";

				Stream stream = tcpClient.GetStream();

				ASCIIEncoding asen = new ASCIIEncoding();
				byte[] ba = asen.GetBytes(input);

				stream.Write(ba, 0, ba.Length);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
			}
		}

		private string GetLocalIPAddress()
		{
			if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
			{
				return null;
			}

			IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

			return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();
		}
	}
}