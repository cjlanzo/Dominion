using System;
using System.Linq;
using DominionFramework.Commands;

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

		public Command HandleCommand(Command command)
		{
			switch (command.Action)
			{
				case ActionType.Chat:
					HandleChat(command);
					return command;
				case ActionType.Connected:
					return HandleConnected(command);
				case ActionType.Disconnected:
					return HandleDisconnected(command);
				case ActionType.Ready:
					return command;
				default:
					return null;
			}
		}
		#endregion Public Methods

		#region Private Methods

		private void HandleChat(Command command)
		{
			Console.WriteLine($"{command.Username} sent message: {command.Message}");
		}

		private Command HandleConnected(Command command)
		{
			GameModel.Players.Add(command.Username);

			return new Command($"{command.Username}:{command.Action}:{GetPlayers()}");
		}

		private Command HandleDisconnected(Command command)
		{
			GameModel.Players.Remove(command.Username);

			return new Command($"{command.Username}:{command.Action}:{GetPlayers()}");
		}
		#endregion Private Methods

		#region Helper Methods

		private string GetPlayers()
		{
			string players = "";

			foreach (string player in GameModel.Players)
			{
				players += player;

				if (player != GameModel.Players.Last())
				{
					players += ",";
				}
			}

			return players;
		}
		#endregion Helper Methods
	}
}