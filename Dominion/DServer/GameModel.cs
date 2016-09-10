using System.Collections.Generic;

namespace DServer
{
	public class GameModel
	{
		#region Member Variables
		private List<Player> _players;

		#endregion Member Variables

		#region Properties
		public List<Player> Players => _players ?? (_players = new List<Player>());
		#endregion Properties

		#region Constructors


		#endregion Constructors



	}
}