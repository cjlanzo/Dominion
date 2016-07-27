using System.Linq;
using System.Net;
using System.Net.Sockets;
using DServer.Clients;
using DServer.Loggers;

namespace DServer.Listeners
{
	public class Listener : IListener
	{
		#region Constants
		private const int Port = 8001;
		#endregion Constants

		#region Member Variables
		private ILogger _logger;
		private TcpListener _tcpListener;
		#endregion Member Variables

		#region Properties
		private ILogger Logger => _logger ?? (_logger = new Logger());
		private TcpListener TcpListener => _tcpListener ?? (_tcpListener = new TcpListener(GetIpAddress(), Port));
		#endregion Properties

		#region Public Methods
		/// <summary>
		/// Returns the location of the listener
		/// </summary>
		/// <returns>Location by ip address and port</returns>
		public EndPoint GetLocation()
		{
			return TcpListener.LocalEndpoint;
		}

		/// <summary>
		/// Waits until it receives a TcpClient connection
		/// </summary>
		/// <returns>Connected client</returns>
		public ConnectedClient ListenForClient()
		{
			TcpClient client = TcpListener.AcceptTcpClient();
			ConnectedClient connectedClient = new ConnectedClient(client);
			connectedClient.Username = connectedClient.Read();

			//Logger.LogMessage($"{client} has connected to the server");

			return connectedClient;
		}

		/// <summary>
		/// Starts the listener
		/// </summary>
		public void Start()
		{
			TcpListener.Start();

			//Logger.LogMessage("Server has started running");
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Returns the ip address of current machine
		/// </summary>
		/// <returns>The machine's ip address</returns>
		private IPAddress GetIpAddress()
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