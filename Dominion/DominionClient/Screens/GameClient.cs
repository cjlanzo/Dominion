using System;
using System.ComponentModel;
using System.Windows.Forms;
using DominionClient.Events;

namespace DominionClient.Screens
{
	public partial class fmGameClient : Form
	{
		#region Member Variables
		private ClientController _controller;
		private fmLobby _lobbyScreen;
		private fmLogin _loginScreen;
		#endregion Member Variables

		#region Properties
		private ClientController Controller => _controller ?? (_controller = new ClientController());
		private fmLobby LobbyScreen => _lobbyScreen ?? (_lobbyScreen = new fmLobby());
		private fmLogin LoginScreen => _loginScreen ?? (_loginScreen = new fmLogin());
		private string Message { get; set; }
		private string Username { get; set; }
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Constructs an instance of a Game Client
		/// </summary>
		public fmGameClient()
		{
			InitializeComponent();
		}
		#endregion Constructors

		#region Event Handlers
		/// <summary>
		/// Handles the send button being clicked
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void btnSend_Click(object sender, EventArgs e)
		{
			Controller.SendMessage($"{Username}:Chat:what's up");
		}

		/// <summary>
		/// Runs when the game client is launched
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void fmGameClient_Load(object sender, EventArgs e)
		{
			LoginScreen.OnLogin += UpdateUsername;
			LoginScreen.ShowDialog();

			Listener.RunWorkerAsync();

			Controller.SendMessage($"{Username}:Login");

			LobbyScreen.ShowDialog();

			Focus();
		}

		//private void ListenForMessages()
		//{
		//	Thread thread = new Thread(() =>
		//	{
		//		byte[] bytes = new byte[100];

		//		Task<int> bytesReceived = Controller.TcpClient.GetStream().ReadAsync(bytes, 0, bytes.Length);

		//		string message = ConvertBytesToString(bytes, bytesReceived);

		//		ParseMessage(message);
		//	});
		//	thread.Start();
		//}

		/// <summary>
		/// Updates the username field
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void UpdateUsername(object sender, LoginEvent e)
		{
			Username = e.Username;
		}
		#endregion Event Handlers

		#region Helper Methods
		/// <summary>
		/// Converts a byte array to a string
		/// </summary>
		/// <param name="bytes">Byte array</param>
		/// <param name="length">Length of the byte array</param>
		/// <returns>The byte array as a string</returns>
		private string ConvertBytesToString(byte[] bytes, int length)
		{
			string s = "";

			for (int i = 0; i < length; i++)
			{
				s += Convert.ToChar(bytes[i]);
			}

			return s;
		}

		private void HandleLoginMessage(string player)
		{
			LobbyScreen.UpdatePlayersList(player, false);
		}

		private void ParseMessage()
		{
			string[] commands = Message.Split(':');

			switch (commands[1])
			{
				case "Action":
					break;
				case "Chat":
					break;
				case "Login":
					HandleLoginMessage(commands[0]);
					break;
				case "Logout":
					break;
				default:
					Console.WriteLine(@"Invalid message");
					break;
			}
		}
		#endregion Helper Methods

		private void Listener_DoWork(object sender, DoWorkEventArgs e)
		{
			byte[] bytes = new byte[100];

			int bytesReceived = Controller.TcpClient.GetStream().Read(bytes, 0, bytes.Length);

			Message = ConvertBytesToString(bytes, bytesReceived);

			Listener.ReportProgress(100);
		}

		private void Listener_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			ParseMessage();
		}
	}
}