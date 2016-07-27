//using System;
//using System.IO;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using DServer.Clients;

//namespace DominionClient
//{
//	public class ClientController
//	{
//		#region Constants
//		private const int Port = 8001;
//		#endregion Constants

//		#region Member Variables
//		private ASCIIEncoding _encoder;
//		//private TcpClient _tcpClient;
//		private ConnectedClient _client;
//		#endregion Member Varibles

//		#region Properties
//		public ASCIIEncoding Encoder => _encoder ?? (_encoder = new ASCIIEncoding());
//		//public TcpClient TcpClient => _tcpClient ?? (_tcpClient = new TcpClient());
//		public ConnectedClient Client => _client ?? (_client = new ConnectedClient(new TcpClient()));
//		#endregion Properties

//		#region Constructors
//		/// <summary>
//		/// Constructs an instance of a client controller
//		/// </summary>
//		public ClientController()
//		{
//			TcpClient.Connect("10.0.0.25", Port);
//		}
//		#endregion Constructors

//		#region Public Methods
//		/// <summary>
//		/// Sends a message from the client to the server
//		/// </summary>
//		/// <param name="message">Message to send to the server</param>
//		public void SendMessage(string message)
//		{
//			try
//			{
//				NetworkStream stream = TcpClient.GetStream();

//				byte[] bytes = Encoder.GetBytes(message);

//				stream.Write(bytes, 0, bytes.Length);
//			}
//			catch (Exception e)
//			{
//				Console.WriteLine(e.StackTrace);
//			}
//		}

//		public void ReceiveBroadcast()
//		{
//			//ThreadStart starter = () =>
//			//{
//			//	while (true)
//			//	{
//			//		byte[] buffer = new byte[100];

//			//		NetworkStream stream = TcpClient.GetStream();

//			//		if (stream.Read(buffer, 0, buffer.Length) == 0)
//			//		{
//			//			Console.WriteLine(@"Closing connection, no bytes received");
//			//			stream.Close();
//			//			TcpClient.Close();
//			//			break;
//			//		}

//			//		Command command = new Command(buffer.ConvertToString());
					
//			//		Console.WriteLine(command.ToString());

//			//		if (command.Action != "Quit")
//			//		{
//			//			continue;
//			//		}

//			//		stream.Close();
//			//		TcpClient.Close();
//			//		break;
//			//	}
//			//};
//			//starter += () =>
//			//{
//			//	ConnectedClients.Remove(connectedClient);

//			//	//take this out later
//			//	Console.WriteLine("Connected clients:");

//			//	foreach (ConnectedClient cclient in ConnectedClients)
//			//	{
//			//		Console.WriteLine(cclient.Username);
//			//	}
//			//};

//			//Thread thread = new Thread(starter) { IsBackground = true };
//			//thread.Start();
//		}
//		#endregion Public methods
//	}
//}