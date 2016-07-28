using System.Collections.Generic;

namespace DServer
{
	public class GameModel
	{
		#region Member Variables
		private List<string> _players;

		#endregion Member Variables

		#region Properties
		public List<string> Players => _players ?? (_players = new List<string>());
		#endregion Properties

		#region Constructors


		#endregion Constructors



	}
}