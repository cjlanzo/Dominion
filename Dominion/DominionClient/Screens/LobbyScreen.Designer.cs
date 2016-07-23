namespace DominionClient.Screens
{
	partial class fmLobby
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnReady = new System.Windows.Forms.Button();
			this.lvwPlayers = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// btnReady
			// 
			this.btnReady.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnReady.Location = new System.Drawing.Point(12, 317);
			this.btnReady.Name = "btnReady";
			this.btnReady.Size = new System.Drawing.Size(264, 55);
			this.btnReady.TabIndex = 0;
			this.btnReady.Text = "Ready";
			this.btnReady.UseVisualStyleBackColor = true;
			// 
			// lvwPlayers
			// 
			this.lvwPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lvwPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
			this.lvwPlayers.Location = new System.Drawing.Point(12, 12);
			this.lvwPlayers.Name = "lvwPlayers";
			this.lvwPlayers.Size = new System.Drawing.Size(264, 299);
			this.lvwPlayers.TabIndex = 1;
			this.lvwPlayers.UseCompatibleStateImageBehavior = false;
			// 
			// fmLobby
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(288, 384);
			this.Controls.Add(this.lvwPlayers);
			this.Controls.Add(this.btnReady);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "fmLobby";
			this.ShowIcon = false;
			this.Text = "LobbyScreen";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnReady;
		private System.Windows.Forms.ListView lvwPlayers;
	}
}