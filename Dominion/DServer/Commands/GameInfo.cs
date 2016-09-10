using System;

namespace DServer.Commands
{
	[Serializable]
	public class GameInfo
	{
		public Command Command { get; set; }
		public GameData GameData { get; set; }
	}
}