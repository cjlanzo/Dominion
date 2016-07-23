namespace DominionClient
{
	partial class fmLogin
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
			this.rtfUsername = new System.Windows.Forms.RichTextBox();
			this.lblUsername = new System.Windows.Forms.Label();
			this.btnLogin = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// rtfUsername
			// 
			this.rtfUsername.Location = new System.Drawing.Point(86, 147);
			this.rtfUsername.Name = "rtfUsername";
			this.rtfUsername.Size = new System.Drawing.Size(236, 48);
			this.rtfUsername.TabIndex = 0;
			this.rtfUsername.Text = "";
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Location = new System.Drawing.Point(177, 131);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(55, 13);
			this.lblUsername.TabIndex = 1;
			this.lblUsername.Text = "Username";
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point(171, 251);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(75, 23);
			this.btnLogin.TabIndex = 2;
			this.btnLogin.Text = "Login";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// fmLogin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(424, 356);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.lblUsername);
			this.Controls.Add(this.rtfUsername);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "fmLogin";
			this.ShowIcon = false;
			this.Text = "LoginScreen";
			this.Load += new System.EventHandler(this.LoginScreen_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox rtfUsername;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.Button btnLogin;
	}
}