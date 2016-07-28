using System;
using System.Windows.Forms;

namespace DominionClient.Screens
{
	public partial class fmMainScreen : Form
	{
		#region Member Variables
		private ClientController _clientController;
		
		#endregion Member Variables

		#region Properties
		private ClientController ClientController => _clientController ?? (_clientController = new ClientController());
		
		#endregion Properties

		#region Public Methods
		public fmMainScreen()
		{
			InitializeComponent();
		}
		#endregion Public Methods

		#region Private Methods
		private void fmMainScreen_Load(object sender, EventArgs e)
		{
			ClientController.Run();
			Close();
		}
		#endregion Private Methods
	}
}
