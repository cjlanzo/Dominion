using System;
using System.Net.Sockets;
using System.Text;
using DServer.Utilities;

namespace DServer.Clients
{
	public class ConnectedClient
	{
		#region Constants
		private const int Port = 8001;
		#endregion Constants

		#region Member Variables
		private ASCIIEncoding _encoder;
		#endregion Member Variables

		#region Properties
		private ASCIIEncoding Encoder => _encoder ?? (_encoder = new ASCIIEncoding());

		public bool Ready { get; set; }
		public TcpClient TcpClient { get; }
		public string Username { get; set; }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructs a connected client
		/// </summary>
		public ConnectedClient(TcpClient tcpClient)
		{
			TcpClient = tcpClient;
			Ready = false;
		}
		#endregion Constructors

		#region Public Methods

		public void Close()
		{
			TcpClient.Close();
		}

		public void Connect()
		{
			TcpClient.Connect("10.0.0.25", Port);
		}

		public bool Read(out string message)
		{
			byte[] b = new byte[100];

			int bytesRead = TcpClient.GetStream().Read(b, 0, 100);

			message = b.ConvertToString();

			return bytesRead > 0;
		}

		/// <summary>
		/// Sends a message from the client to the server
		/// </summary>
		/// <param name="message">Message to send to the server</param>
		public void SendMessage(string message)
		{
			try
			{
				byte[] bytes = Encoder.GetBytes(message);

				TcpClient.GetStream().Write(bytes, 0, bytes.Length);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
		#endregion Public Methods
	}
}