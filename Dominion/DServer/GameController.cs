using System;
using System.Linq;
using DServer.Commands;

namespace DServer
{
	public class GameController
	{
		#region Member Variables
		private GameModel _gameModel;
		#endregion Member Variables

		#region Properties
		private GameModel GameModel => _gameModel ?? (_gameModel = new GameModel());

		#endregion Properties

		#region Public Methods

		public GameInfo HandleCommand(Command command)
		{
			switch (command.Action)
			{
				case ActionType.Connected:
					return HandleConnected(command);
				case ActionType.Disconnected:
					return HandleDisconnected(command);
				//case ActionType.Ready:
				//	return HandleReady(command);
				default:
					return null;
			}
		}
		#endregion Public Methods

		#region Private Methods

		private GameInfo HandleConnected(Command command)
		{
			GameModel.Players.Add(new Player(command.Username));

			GameData gameData = new GameData();
			gameData.Players = GameModel.Players;

			GameInfo gameInfo = new GameInfo();
			gameInfo.Command = command;
			gameInfo.GameData = gameData;

			return gameInfo;

		}

		private GameInfo HandleDisconnected(Command command)
		{
			for (int i = 0; i < GameModel.Players.Count; i++)
			{
				if (GameModel.Players[i].Username == command.Username)
				{
					GameModel.Players.RemoveAt(i);
				}
			}

			GameData gameData = new GameData();
			gameData.Players = GameModel.Players;

			GameInfo gameInfo = new GameInfo();
			gameInfo.Command = command;
			gameInfo.GameData = gameData;

			return gameInfo;
		}

		//private Command HandleReady(Command command)
		//{
		//	for (int i = 0; i < GameModel.Players.Count; i++)
		//	{
		//		if (GameModel.Players[i].Username == command.Username)
		//		{
		//			GameModel.Players[i].Ready = true;
		//		}
		//	}

		//	return new Command($"{command.Username}:{command.Action}:{GetPlayers()}");
		//}
		//#endregion Private Methods

		//#region Helper Methods

		#endregion Helper Methods
	}
}