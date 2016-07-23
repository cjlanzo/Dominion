using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DominionClient
{
	public class ClientController
	{
		#region Constants
		const int port = 8001;
		#endregion Constants

		private TcpClient tcpClient;

		public ClientController()
		{
			tcpClient = new TcpClient();
			tcpClient.Connect("10.0.0.215", port);
		}

		#region Public Methods
		public void TestMethod()
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
		#endregion Public methods

		#region Private Methods
		private static string GetLocalIPAddress()
		{
			if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
			{
				return null;
			}

			IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

			return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();
		}
		#endregion Private Methods
	}
}