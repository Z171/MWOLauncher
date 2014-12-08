using System.Net;
namespace MWOlauncher
{
	partial class MainForm
    {
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
		public System.Windows.Forms.ProgressBar DownloadBar;
		public System.Windows.Forms.LinkLabel linkLabel1;
        public System.Windows.Forms.Label recivedLabel;
		
		
		public void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Exit = new System.Windows.Forms.Button();
            this.Play = new System.Windows.Forms.Button();
            this.DownloadBar = new System.Windows.Forms.ProgressBar();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.recivedLabel = new System.Windows.Forms.Label();
            this.leftLabel = new System.Windows.Forms.Label();
            this.MbLeft = new System.Windows.Forms.Label();
            this.mbRecived = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.downloadSpeed = new System.Windows.Forms.Label();
            this.webClient = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.TimeLeft = new System.Windows.Forms.Label();
            this.updateSize = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.loadingLauncher = new System.Windows.Forms.PictureBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openMWOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playMWOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launcherVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LauncherBuild = new System.Windows.Forms.Label();
            this.GameBuild = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.loadingLauncher)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.Exit, "Exit");
            this.Exit.FlatAppearance.BorderSize = 0;
            this.Exit.ForeColor = System.Drawing.Color.Transparent;
            this.Exit.Name = "Exit";
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.ExitClick);
            // 
            // Play
            // 
            this.Play.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            resources.ApplyResources(this.Play, "Play");
            this.Play.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.Play.Name = "Play";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            this.Play.MouseLeave += new System.EventHandler(this.Play_Leave);
            this.Play.MouseHover += new System.EventHandler(this.Play_Hover);
            // 
            // DownloadBar
            // 
            this.DownloadBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            resources.ApplyResources(this.DownloadBar, "DownloadBar");
            this.DownloadBar.Name = "DownloadBar";
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.linkLabel1.Click += new System.EventHandler(this.LinkLabel1LinkClicked);
            // 
            // recivedLabel
            // 
            resources.ApplyResources(this.recivedLabel, "recivedLabel");
            this.recivedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.recivedLabel.Name = "recivedLabel";
            // 
            // leftLabel
            // 
            resources.ApplyResources(this.leftLabel, "leftLabel");
            this.leftLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.leftLabel.Name = "leftLabel";
            // 
            // MbLeft
            // 
            resources.ApplyResources(this.MbLeft, "MbLeft");
            this.MbLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.MbLeft.Name = "MbLeft";
            // 
            // mbRecived
            // 
            resources.ApplyResources(this.mbRecived, "mbRecived");
            this.mbRecived.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.mbRecived.Name = "mbRecived";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.label2.Name = "label2";
            // 
            // downloadSpeed
            // 
            resources.ApplyResources(this.downloadSpeed, "downloadSpeed");
            this.downloadSpeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.downloadSpeed.Name = "downloadSpeed";
            // 
            // webClient
            // 
            this.webClient.WorkerReportsProgress = true;
            this.webClient.WorkerSupportsCancellation = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.label1.Name = "label1";
            // 
            // TimeLeft
            // 
            resources.ApplyResources(this.TimeLeft, "TimeLeft");
            this.TimeLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.TimeLeft.Name = "TimeLeft";
            // 
            // updateSize
            // 
            resources.ApplyResources(this.updateSize, "updateSize");
            this.updateSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.updateSize.Name = "updateSize";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(181)))), ((int)(((byte)(229)))));
            this.label4.Name = "label4";
            // 
            // loadingLauncher
            // 
            this.loadingLauncher.Image = global::MWOlauncher.Properties.Resources.launcherLoading;
            resources.ApplyResources(this.loadingLauncher, "loadingLauncher");
            this.loadingLauncher.Name = "loadingLauncher";
            this.loadingLauncher.TabStop = false;
            this.loadingLauncher.UseWaitCursor = true;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMWOToolStripMenuItem,
            this.playMWOToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // openMWOToolStripMenuItem
            // 
            this.openMWOToolStripMenuItem.Name = "openMWOToolStripMenuItem";
            resources.ApplyResources(this.openMWOToolStripMenuItem, "openMWOToolStripMenuItem");
            this.openMWOToolStripMenuItem.Click += new System.EventHandler(this.openMWOToolStripMenuItem_Click);
            // 
            // playMWOToolStripMenuItem
            // 
            this.playMWOToolStripMenuItem.Name = "playMWOToolStripMenuItem";
            resources.ApplyResources(this.playMWOToolStripMenuItem, "playMWOToolStripMenuItem");
            this.playMWOToolStripMenuItem.Click += new System.EventHandler(this.playMWOToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // launcherVersion
            // 
            resources.ApplyResources(this.launcherVersion, "launcherVersion");
            this.launcherVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            this.launcherVersion.Name = "launcherVersion";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            this.label3.Name = "label3";
            // 
            // LauncherBuild
            // 
            this.LauncherBuild.BackColor = System.Drawing.Color.Transparent;
            this.LauncherBuild.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            resources.ApplyResources(this.LauncherBuild, "LauncherBuild");
            this.LauncherBuild.Name = "LauncherBuild";
            // 
            // GameBuild
            // 
            this.GameBuild.BackColor = System.Drawing.Color.Transparent;
            this.GameBuild.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            resources.ApplyResources(this.GameBuild, "GameBuild");
            this.GameBuild.Name = "GameBuild";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.Controls.Add(this.GameBuild);
            this.Controls.Add(this.LauncherBuild);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.launcherVersion);
            this.Controls.Add(this.loadingLauncher);
            this.Controls.Add(this.updateSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TimeLeft);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.downloadSpeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mbRecived);
            this.Controls.Add(this.MbLeft);
            this.Controls.Add(this.leftLabel);
            this.Controls.Add(this.recivedLabel);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.DownloadBar);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Play);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Opacity = 0.92D;
            ((System.ComponentModel.ISupportInitialize)(this.loadingLauncher)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		    }
			
			
			private void LinkLabel1LinkClicked (object sender, System.EventArgs e)
			{
				try
   				{
					System.Diagnostics.Process.Start("http://www.itmakers.altervista.org");
    			}
    			catch {}
			}

            public System.Windows.Forms.Label leftLabel;
            public System.Windows.Forms.Label MbLeft;
            public System.Windows.Forms.Label mbRecived;
            public System.Windows.Forms.Label label2;
            public System.Windows.Forms.Label downloadSpeed;
            public System.ComponentModel.BackgroundWorker webClient;
            public System.Windows.Forms.Label label1;
            public System.Windows.Forms.Label TimeLeft;
            public System.Windows.Forms.Label updateSize;
            public System.Windows.Forms.Label label4;
            private System.Windows.Forms.PictureBox loadingLauncher;
            private System.Windows.Forms.NotifyIcon notifyIcon;
            private System.ComponentModel.IContainer components;
            private System.Windows.Forms.Label launcherVersion;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.Label LauncherBuild;
            private System.Windows.Forms.Label GameBuild;
            private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
            private System.Windows.Forms.ToolStripMenuItem openMWOToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem playMWOToolStripMenuItem;
            private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
            private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}