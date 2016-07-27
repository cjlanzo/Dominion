using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using DServer.Clients;

namespace DServer
{
	//public static class GameModel
	public class GameModel
	{
		#region Locking Variables
		private static GameModel instance;
		private static Mutex mutex;
		#endregion Locking Variables

		Guid token;

		#region Member Variables
		private List<ConnectedClient> _connectedClients;
		#endregion Member Variables

		#region Properties
		public List<ConnectedClient> ConnectedClients => _connectedClients ?? (_connectedClients = new List<ConnectedClient>());
		#endregion Properties

		#region Constructors

		private GameModel()
		{
			token = new Guid();
		}

		static GameModel()
		{
			instance = new GameModel();
			mutex = new Mutex();
		}

		public static GameModel Acquire()
		{
			mutex.WaitOne();
			return instance;
		}

		public static void Release()
		{
			mutex.ReleaseMutex();
		}

		//public static int GetPlayers()
		//{
		//	return ConnectedClients.Count;
		//}


		//public static GameModel Instance
		//{
		//	get
		//	{
		//		if (instance != null)
		//		{
		//			return instance;
		//		}

		//		lock (syncRoot)
		//		{
		//			if (instance == null)
		//			{
		//				instance = new GameModel();
		//			}
		//		}

		//		return instance;
		//	}
		//}
		#endregion Constructors

		//private static List<string> ConnectedPlayers { get; set; }



	}
}