using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace DominionClient
{
	public class ClientController
	{
		#region Constants
		private const int Port = 8001;
		#endregion Constants

		#region Member Variables
		private ASCIIEncoding _encoder;
		private TcpClient _tcpClient;
		#endregion Member Varibles

		#region Properties
		private ASCIIEncoding Encoder => _encoder ?? (_encoder = new ASCIIEncoding());
		private TcpClient TcpClient => _tcpClient ?? (_tcpClient = new TcpClient());
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructs an instance of a client controller
		/// </summary>
		public ClientController()
		{
			TcpClient.Connect("10.0.0.215", Port);
		}
		#endregion Constructors

		#region Public Methods
		/// <summary>
		/// Sends a message from the client to the server
		/// </summary>
		/// <param name="message">Message to send to the server</param>
		public void SendMessage(string message)
		{
			try
			{
				Stream stream = _tcpClient.GetStream();

				byte[] bytes = Encoder.GetBytes(message);

				stream.Write(bytes, 0, bytes.Length);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
		#endregion Public methods
	}
}