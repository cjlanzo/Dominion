using System;
using System.Net.Sockets;
using System.Threading;
using DominionFramework.Commands;
using DominionFramework.Utilities;

namespace DominionFramework.Threads
{
	public class ServerThread
	{
		#region Member Variables
		private byte[] _buffer;
		#endregion Member Variables

		#region Properties
		private byte[] Buffer => _buffer ?? (_buffer = new byte[100]);
		#endregion Properties

		#region Public Methods
		/// <summary>
		/// Creates a thread to process client input to the server
		/// </summary>
		/// <param name="client">Client input to process</param>
		public void Start(TcpClient client)
		{
			new Thread(() =>
			{
				while (true)
				{
					for (int i = 0; i < Buffer.Length; i++)
					{
						Buffer[i] = Convert.ToByte('\0');
					}

					NetworkStream stream = client.GetStream();

					if (stream.Read(Buffer, 0, Buffer.Length) == 0)
					{
						Console.WriteLine("Closing connection, no bytes received");
						stream.Close();
						client.Close();
						break;
					}
					
					Command command = new Command(Buffer.ConvertToString());

					if (command.Action != "Logout")
					{
						continue;
					}

					stream.Close();
					client.Close();
					break;
				}
			}).Start();
		}
		#endregion Public Methods
	}
}