using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using DServer.Commands;
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
			SendCommand(new Command(Username, ActionType.Connected));

		}

		public Command ReadCommand()
		{
			try
			{
				return (Command)new BinaryFormatter().Deserialize(TcpClient.GetStream());
			}
			catch (Exception)
			{
				return null;
			}
		}

		public GameInfo ReadGameInfo()
		{
			try
			{
				return (GameInfo)new BinaryFormatter().Deserialize(TcpClient.GetStream());
			}
			catch (Exception)
			{
				return null;
			}
		}

		public void SendCommand(Command command)
		{
			try
			{
				new BinaryFormatter().Serialize(TcpClient.GetStream(), command);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

		public void SendGameInfo(GameInfo gameInfo)
		{
			try
			{
				new BinaryFormatter().Serialize(TcpClient.GetStream(), gameInfo);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}
		#endregion Public Methods

		#region Private Methods

		#endregion Private Methods
	}
}