using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DServer
{
	public static class Server
	{
		#region Constants
		private const int Port = 8001;
		#endregion Constants

		#region Main
		/// <summary>
		/// Main method to start the server
		/// </summary>
		public static void Main()
		{
			try
			{
				TcpListener tcpListener = new TcpListener(GetServerIpAddress(), Port);
				tcpListener.Start();

				Console.WriteLine($"The server is running at {tcpListener.LocalEndpoint}");

				while (true)
				{
					Socket socket = tcpListener.AcceptSocket();

					Console.WriteLine($"Connection accepted from {socket.RemoteEndPoint}");

					Thread thread = new Thread(() => { ManageClientConnection(socket); });
					thread.Start();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
		#endregion Main

		#region Private Methods
		/// <summary>
		/// Returns the ip address of the server
		/// </summary>
		/// <returns>The server's ip address</returns>
		private static IPAddress GetServerIpAddress()
		{
			if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
			{
				return null;
			}

			IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

			return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
		}

		/// <summary>
		/// Handles the connection between the server and a client
		/// </summary>
		/// <param name="socket">Socket that connects the server to the client</param>
		private static void ManageClientConnection(Socket socket)
		{
			byte[] bytes = new byte[100];
			int bytesReceived = 0;

			while (true)
			{
				try
				{
					bytesReceived = socket.Receive(bytes);

					if (bytesReceived < 1)
					{
						Console.WriteLine("Socket closing, no bytes received.");
						socket.Close();
					}

					string message = ConvertBytesToString(bytes, bytesReceived);
					Console.WriteLine($"Message received: {message}");
					Console.WriteLine(ParseMessage(message));
				}
				catch (SocketException)
				{
					string message = ConvertBytesToString(bytes, bytesReceived);
					string[] user = message.Split(':');

					Console.WriteLine($"{user[0]} has disconnected from the server");
					socket.Close();
					break;
				}
			}
		}
		#endregion Private Methods

		#region Helper Methods
		/// <summary>
		/// Converts a byte array to a string
		/// </summary>
		/// <param name="bytes">Byte array</param>
		/// <param name="length">Length of the byte array</param>
		/// <returns>The byte array as a string</returns>
		private static string ConvertBytesToString(byte[] bytes, int length)
		{
			string s = "";

			for (int i = 0; i < length; i++)
			{
				s += Convert.ToChar(bytes[i]);
			}

			return s;
		}

		private static string ParseMessage(string message)
		{
			string[] commands = message.Split(':');

			switch (commands[1])
			{
				case "Action":
					return "Action not implemented yet";
				case "Chat":
					return $"{commands[0]} sent message: {commands[2]}";
				case "Login":
					return $"{commands[0]} has connected to the server";
				default:
					return "Invalid command";
			}
		}
		#endregion Helper methods
	}
}