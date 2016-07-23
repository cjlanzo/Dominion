using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DServer
{
	public static class Server
	{
		#region Main
		public static void Main()
		{
			const int port = 8001;

			try
			{
				TcpListener tcpListener = new TcpListener(GetLocalIPAddress(), port);
				tcpListener.Start();

				Console.WriteLine($"The server is running at port {port}");
				Console.WriteLine($"The local End Point is {tcpListener.LocalEndpoint}");
				Console.WriteLine(@"Waiting for a connection...");

				Socket socket = tcpListener.AcceptSocket();

				Console.WriteLine($"Connection accepted from {socket.RemoteEndPoint}");

				while (true)
				{
					byte[] bytes = new byte[100];
					int k = socket.Receive(bytes);

					Console.WriteLine(@"Received...");

					for (int i = 0; i < k; i++)
					{
						Console.Write(Convert.ToChar(bytes[i]));
					}

					ASCIIEncoding asen = new ASCIIEncoding();
					socket.Send(asen.GetBytes(@"The string was received by the server."));

					Console.WriteLine("\nSent Acknowledgement");
				}
				

				socket.Close();
				tcpListener.Stop();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
		#endregion Main

		#region Private Methods
		private static IPAddress GetLocalIPAddress()
		{
			if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
			{
				return null;
			}

			IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

			return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
		}
		#endregion Private Methods
	}
}