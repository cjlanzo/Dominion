using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DServer
{
	public static class Server
	{
		#region Constants
		private const int Port = 8001;
		#endregion Constants

		#region Member Variables
		private static Dictionary<string, Socket> _activeSockets;
		private static ASCIIEncoding _encoder;
		#endregion Member Variables

		#region Properties
		private static Dictionary<string, Socket> ActiveSockets => _activeSockets ?? (_activeSockets = new Dictionary<string, Socket>());
		private static ASCIIEncoding Encoder => _encoder ?? (_encoder = new ASCIIEncoding());
		#endregion Properties

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

					ActiveSockets.Add(socket.GetHashCode().ToString(), socket);

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
		/// Broadcasts a message to all clients connected to the server
		/// </summary>
		/// <param name="mesasge">Message to broadcast</param>
		private static void BroadcastMessage(string mesasge)
		{
			Thread thread = new Thread(() =>
			{
				foreach (Socket socket in ActiveSockets.Values)
				{
					byte[] bytes = Encoder.GetBytes(mesasge);

					socket.Send(bytes);
				}
			});
			thread.Start();
		}
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

					BroadcastMessage(message);
				}
				catch (SocketException)
				{
					string message = ConvertBytesToString(bytes, bytesReceived);
					string[] user = message.Split(':');

					Console.WriteLine($"{user[0]} has disconnected from the server");
					
					ActiveSockets.Remove(socket.GetHashCode().ToString());

					string messageToBroadcast = $"{user[0]}:Logout";
					BroadcastMessage(messageToBroadcast);

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