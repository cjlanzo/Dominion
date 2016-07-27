using System.Net;
using DServer.Clients;

namespace DServer.Listeners
{
	public interface IListener
	{
		/// <summary>
		/// Returns the location of the listener
		/// </summary>
		/// <returns>Location by ip address and port</returns>
		EndPoint GetLocation();

		/// <summary>
		/// Waits until it receives a TcpClient connection
		/// </summary>
		/// <returns>TcpClient connection</returns>
		ConnectedClient ListenForClient();

		/// <summary>
		/// Starts the listener
		/// </summary>
		void Start();
	}
}