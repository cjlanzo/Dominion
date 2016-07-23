using System.Net.Sockets;

namespace DServer
{
	public class ServerThread
	{
		private Socket socket;

		public ServerThread(Socket socket)
		{
			this.socket = socket;
		}
	}
}