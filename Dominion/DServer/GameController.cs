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
					return HandleReady(command);
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
			GameModel.Players.Add(new Player(command.Username));

			return new Command($"{command.Username}:{command.Action}:{GetPlayers()}");
		}

		private Command HandleDisconnected(Command command)
		{
			for (int i = 0; i < GameModel.Players.Count; i++)
			{
				if (GameModel.Players[i].Username == command.Username)
				{
					GameModel.Players.RemoveAt(i);
				}
			}

			return new Command($"{command.Username}:{command.Action}:{GetPlayers()}");
		}

		private Command HandleReady(Command command)
		{
			for (int i = 0; i < GameModel.Players.Count; i++)
			{
				if (GameModel.Players[i].Username == command.Username)
				{
					GameModel.Players[i].Ready = true;
				}
			}

			return new Command($"{command.Username}:{command.Action}:{GetPlayers()}");
		}
		#endregion Private Methods

		#region Helper Methods

		private string GetPlayers()
		{
			string players = "";

			foreach (Player player in GameModel.Players)
			{
				players += $"{player.Username},{player.Ready}";

				if (player != GameModel.Players.Last())
				{
					players += ";";
				}
			}

			return players;
		}
		#endregion Helper Methods
	}
}