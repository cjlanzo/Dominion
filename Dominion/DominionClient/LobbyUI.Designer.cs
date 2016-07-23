namespace DominionClient
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
			this.lvwPlayers = new System.Windows.Forms.ListView();
			this.btnReady = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lvwPlayers
			// 
			this.lvwPlayers.Location = new System.Drawing.Point(12, 12);
			this.lvwPlayers.Name = "lvwPlayers";
			this.lvwPlayers.Size = new System.Drawing.Size(245, 307);
			this.lvwPlayers.TabIndex = 0;
			this.lvwPlayers.UseCompatibleStateImageBehavior = false;
			this.lvwPlayers.SelectedIndexChanged += new System.EventHandler(this.lvwPlayers_SelectedIndexChanged);
			// 
			// btnReady
			// 
			this.btnReady.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnReady.Location = new System.Drawing.Point(12, 325);
			this.btnReady.Name = "btnReady";
			this.btnReady.Size = new System.Drawing.Size(245, 38);
			this.btnReady.TabIndex = 1;
			this.btnReady.Text = "Ready";
			this.btnReady.UseVisualStyleBackColor = true;
			this.btnReady.Click += new System.EventHandler(this.btnReady_Click);
			// 
			// fmLobby
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(269, 375);
			this.Controls.Add(this.btnReady);
			this.Controls.Add(this.lvwPlayers);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "fmLobby";
			this.ShowIcon = false;
			this.Text = "Game Lobby";
			this.Load += new System.EventHandler(this.fmLobby_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvwPlayers;
		private System.Windows.Forms.Button btnReady;
	}
}