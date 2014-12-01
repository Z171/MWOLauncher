using System.Net;
namespace MWOlauncher
{
	partial class MainForm
	{

		public System.ComponentModel.IContainer components = null;
		public System.Windows.Forms.Button Exit;
		public System.Windows.Forms.Button Play;

		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		//readonly CheckVersion versioner = new CheckVersion();
		public System.Windows.Forms.ProgressBar progressBar1;
		public System.Windows.Forms.LinkLabel linkLabel1;
		public System.Windows.Forms.Label recivedLabel;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		
		
		public void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.Exit = new System.Windows.Forms.Button();
			this.Play = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.recivedLabel = new System.Windows.Forms.Label();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.SuspendLayout();
			// 
			// Exit
			// 
			this.Exit.BackColor = System.Drawing.Color.Transparent;
			this.Exit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Exit.BackgroundImage")));
			this.Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.Exit.FlatAppearance.BorderSize = 0;
			this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Exit.ForeColor = System.Drawing.Color.Transparent;
			this.Exit.Location = new System.Drawing.Point(950, 9);
			this.Exit.Margin = new System.Windows.Forms.Padding(0);
			this.Exit.Name = "Exit";
			this.Exit.Size = new System.Drawing.Size(40, 40);
			this.Exit.TabIndex = 0;
			this.Exit.UseVisualStyleBackColor = false;
			this.Exit.Click += new System.EventHandler(this.ExitClick);
			// 
			// Play
			// 
			this.Play.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.Play.Font = new System.Drawing.Font("Roboto", 15F);
			this.Play.Location = new System.Drawing.Point(790, 540);
			this.Play.Margin = new System.Windows.Forms.Padding(0);
			this.Play.Name = "Play";
			this.Play.Size = new System.Drawing.Size(200, 50);
			this.Play.TabIndex = 1;
			this.Play.Text = "PLAY MWO!";
			this.Play.UseVisualStyleBackColor = true;
			this.Play.Click += new System.EventHandler(this.play);
			// 
			// progressBar1
			// 
			this.progressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
			this.progressBar1.Location = new System.Drawing.Point(280, 540);
			this.progressBar1.Margin = new System.Windows.Forms.Padding(0);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(500, 50);
			this.progressBar1.TabIndex = 2;
			// 
			// linkLabel1
			// 
			this.linkLabel1.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
			this.linkLabel1.Font = new System.Drawing.Font("Roboto", 10F);
			this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
			this.linkLabel1.Location = new System.Drawing.Point(10, 9);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(151, 23);
			this.linkLabel1.TabIndex = 3;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "ITMAKERS website";
			this.linkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
			this.linkLabel1.Click += new System.EventHandler(this.LinkLabel1LinkClicked);
			// 
			// recivedLabel
			// 
			this.recivedLabel.Font = new System.Drawing.Font("Roboto", 10F);
			this.recivedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
			this.recivedLabel.Location = new System.Drawing.Point(10, 540);
			this.recivedLabel.Margin = new System.Windows.Forms.Padding(0);
			this.recivedLabel.Name = "recivedLabel";
			this.recivedLabel.Size = new System.Drawing.Size(100, 50);
			this.recivedLabel.TabIndex = 4;
			this.recivedLabel.Text = "Mb recived: ";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.ClientSize = new System.Drawing.Size(1000, 600);
			this.Controls.Add(this.recivedLabel);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.Exit);
			this.Controls.Add(this.Play);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1000, 600);
			this.MinimumSize = new System.Drawing.Size(1000, 600);
			this.Name = "MainForm";
			this.Opacity = 0.9D;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MWOlauncher";
			this.ResumeLayout(false);

		}
			private void ExitClick(object sender, System.EventArgs e)
			{
   				 this.Close();
			}
			
			private void play(object sender, System.EventArgs e)
			{
				GameUpdate();
			}
			
			private void LinkLabel1LinkClicked (object sender, System.EventArgs e)
			{
				try
   				{
					System.Diagnostics.Process.Start("http://www.itmakers.altervista.org");
    			}
    			catch {}
			}
	}
}
