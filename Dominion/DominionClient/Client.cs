using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace DominionClient
{
	public static class Client
	{
		public static void Main()
		{
			const string ip = "10.0.0.25";
			const int port = 8001;

			try
			{
				TcpClient tcpClient = new TcpClient();

				Console.WriteLine(@"Connecting...");

				tcpClient.Connect(ip, port);

				Console.WriteLine(@"Connected");
				Console.Write(@"Enter the string to be transmitted: ");

				string input = Console.ReadLine();
				Stream stream = tcpClient.GetStream();

				ASCIIEncoding asen = new ASCIIEncoding();
				byte[] ba = asen.GetBytes(input);

				Console.WriteLine(@"Transmitting");

				stream.Write(ba, 0, ba.Length);

				byte[] bb = new byte[100];

				int k = stream.Read(bb, 0, 100);

				for (int i = 0; i < k; i++)
				{
					Console.Write(Convert.ToChar(bb[i]));
				}

				tcpClient.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}