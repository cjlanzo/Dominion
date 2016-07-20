using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DominionServer
{
	public static class Server
	{
		public static void Main()
		{
			try
			{
				IPAddress ipAddress = IPAddress.Parse("10.0.0.25");

				const int port = 8001;
				TcpListener tcpListener = new TcpListener(ipAddress, port);
				tcpListener.Start();

				Console.WriteLine($"The server is running at port {port}");
				Console.WriteLine($"The local End Point is {tcpListener.LocalEndpoint}");
				Console.WriteLine(@"Waiting for a connection...");

				Socket socket = tcpListener.AcceptSocket();

				Console.WriteLine($"Connection accepted from {socket.RemoteEndPoint}");

				byte[] bytes = new byte[100];
				int k = socket.Receive(bytes);

				Console.WriteLine(@"Received...");

				for (int i = 0; i < k; i++)
				{
					Console.Write(Convert.ToChar(bytes[i]));
				}

				ASCIIEncoding asen = new ASCIIEncoding();
				socket.Send(asen.GetBytes(@"The string was received by the server."));

				Console.WriteLine(@"\nSent Acknowledgement");

				socket.Close();
				tcpListener.Stop();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}