namespace DominionClient.Screens
{
	partial class fmGameClient
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
			this.Send = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Send
			// 
			this.Send.Location = new System.Drawing.Point(147, 96);
			this.Send.Name = "Send";
			this.Send.Size = new System.Drawing.Size(426, 109);
			this.Send.TabIndex = 0;
			this.Send.Text = "Send";
			this.Send.UseVisualStyleBackColor = true;
			this.Send.Click += new System.EventHandler(this.Send_Click);
			// 
			// fmGameClient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(822, 590);
			this.Controls.Add(this.Send);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MinimizeBox = false;
			this.Name = "fmGameClient";
			this.ShowIcon = false;
			this.Text = "GameClient";
			this.Load += new System.EventHandler(this.fmGameClient_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Send;
	}
}