using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using IWshRuntimeLibrary;
using System.Timers;
using Microsoft.Win32;

namespace MWOlauncher
{
	public partial class MainForm : Form
	{
        const string rootFolder = "c:/Games/MWO";
        const string launcherFolder = (rootFolder + "/launcher");
        const string gameFolder = (rootFolder + "/game");
        const string versionFolder = (rootFolder + "/versions");
        const string GameLatestTXT = (versionFolder + "/GameLatest.txt");
        const string GameActualTXT = (versionFolder + "/GameActual.txt");
        const string LauncherLatestTXT = (versionFolder + "/LauncherLatest.txt");
        const string LauncherActualTXT = (versionFolder + "/LauncherActual.txt");
        const string ZipEXE = (rootFolder + "/7zip.exe");
        string LatestLauncherFolder;
        string LauncherZIP;
        string GameFolder;
        string GameZIP;
        string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        const string downloadGameBase = "http://itmakers.altervista.org/MWO/game/MWO";
        Uri downloadLauncher = new Uri("http://itmakers.altervista.org/MWO/launcher/MWOlauncher.zip");
        Uri downloadZipper = new Uri("http://itmakers.altervista.org/MWO/7za.exe");
        Uri GameLatestURL = new Uri("http://itmakers.altervista.org/MWO/game/LatestVersion.txt");
        Uri LauncherLatestURL = new Uri("http://itmakers.altervista.org/MWO/launcher/launcherVersion.txt");

        private WebClient _webClient = null;
        private WebClient LauncherWebClient = null;
        private string _savedFile = string.Empty;
        private DateTime _startDate = DateTime.Now;
        public NotifyIcon icon = new NotifyIcon();
		
		public MainForm()
		{
			InitializeComponent();          
            _webClient = new WebClient();
            _webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(_webClient_DownloadProgressChanged);
            _webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(_webClient_DownloadFileCompleted);
            //update GUI
            if (System.IO.File.Exists(GameActualTXT))
            {
                GameBuild.Text = System.IO.File.ReadAllText(GameActualTXT);
            }
            else
            {
                GameBuild.Text = "0";
            }
            //check launcher version
            if (LauncherUpdate())
            {
                DownloadLauncher();
            }            
		}

        //register key on windows registry
        private void runAtStartup(string launcherLocation)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //if there's a key, delete it
            if (rkApp.GetValue("MyApp") != null)
            {
                //delete key
                rkApp.DeleteValue("MWOLauncher");
            }
            //set new key
            rkApp.SetValue("MWOLauncher", (launcherLocation + "/MWOlauncher.exe"));
        }

        private void ExitClick(object sender, System.EventArgs e)
        {
            //minimize in taskbar
            this.Hide();
            //show taskbar icon
            notifyIcon.Visible = true;
            //start new timer for sheduled update (every 30 minutes)
            System.Timers.Timer t = new System.Timers.Timer(1800000);
            //enable autoreset
            t.AutoReset = true;
            //assign background worker
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            //start timer
            t.Start();
        }

        //sheduled update
        private static void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //reference to MainForm
            MainForm mainForm = new MainForm();
            //check launcher version (and download updates)
            if (mainForm.LauncherUpdate())
            {
                mainForm.DownloadLauncher();
            }
            //check game version (and download updates)
            if (mainForm.GameUpdate())
            {
                mainForm.DownloadGame();
            }
        }

        //click play button
        private void Play_Click(object sender, EventArgs e)
        {
            if (GameUpdate())
            {
                DownloadGame();
            }
            else
            {
                DownloadBar.Value = 100;
                StartGame();
            }
        }

        //mouse over play button
        private void Play_Hover(object sender, EventArgs e)
        {
            //change play button color
            Play.BackColor = System.Drawing.ColorTranslator.FromHtml("#99cc00");
        }

        //mouse leaves play button
        private void Play_Leave(object sender, EventArgs e)
        {
            //restore default play button colo
            Play.BackColor = System.Drawing.ColorTranslator.FromHtml("#eeeeee");
        }

        //download latest game
        public void DownloadGame()
        {
            //file to int
            string latestVersionRead = System.IO.File.ReadAllText(GameLatestTXT);
            int latestVersionF = int.Parse(latestVersionRead);

            string localVersionRead = System.IO.File.ReadAllText(GameActualTXT);
            int localVersionF = int.Parse(localVersionRead);

            //assign latest version folder
            string LastGameFolder = (gameFolder + "/" + localVersionRead);
            //if an older version exists, erase it
            if (System.IO.Directory.Exists(LastGameFolder))
            {
                System.IO.Directory.Delete(LastGameFolder, true);
            }
            //create latest version folder
            GameFolder = (gameFolder + "/" + latestVersionRead);
            System.IO.Directory.CreateDirectory(GameFolder);
            //initializing zip link
            GameZIP = (gameFolder + "/" + latestVersionRead + ".zip");
            //creating URI based on version
            Uri downloadGame = new Uri(downloadGameBase + latestVersionRead + ".zip");
            _startDate = DateTime.Now;
            _webClient.DownloadFileAsync(downloadGame, GameZIP);
            unzip(GameZIP, GameFolder);
            System.IO.File.WriteAllText(GameActualTXT, latestVersionRead);
            //update GUI
            GameBuild.Text = System.IO.File.ReadAllText(GameActualTXT);
        }

        //background worker on download complete
        public void _webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //unzip game zip
            unzip(GameZIP, GameFolder);
            //start game
            StartGame();
            //enable play button
            Play.Enabled = true;
        }

        //background worker on download progress change
        public void _webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //disable play button
            Play.Enabled = false;
            //set download bar value
            DownloadBar.Value = e.ProgressPercentage;
            //timer for remaining time and download speed
            if ((DateTime.Now - _startDate).Seconds > 0)
            {
                double kbPerSecond = (e.BytesReceived / 1000) / (DateTime.Now - _startDate).Seconds;
                double remainingTimeSeconds = (((e.TotalBytesToReceive - e.BytesReceived) / 1000) / kbPerSecond);
                string remainingTimeDisplay = string.Empty;
                if (remainingTimeSeconds > 3600)
                {
                    remainingTimeDisplay += ((int)(remainingTimeSeconds) / 3600).ToString("n0") + " hours, ";
                    remainingTimeSeconds %= 3600;
                }
                if (remainingTimeSeconds > 60)
                {
                    remainingTimeDisplay += ((int)(remainingTimeSeconds) / 60).ToString("n0") + " minutes, ";
                    remainingTimeSeconds %= 60;
                }
                remainingTimeDisplay += ((int)remainingTimeSeconds).ToString("n0") + " seconds remaining";
                //update statistic on GUI
                TimeLeft.Text = remainingTimeDisplay;
                downloadSpeed.Text = kbPerSecond.ToString() + " Kb/s";
                updateSize.Text = (((e.TotalBytesToReceive) / 1024) / 1024).ToString() + " MB";
                mbRecived.Text = (((e.BytesReceived) / 1024) / 1024).ToString() + " MB";
                string mbLeft = (((e.TotalBytesToReceive - e.BytesReceived) / 1024) / 1024).ToString() + " MB";
                MbLeft.Text = mbLeft;
            }

        }

        //background worker on download complete launcher
        public void _webClient_DownloadFileCompletedLauncher(object sender, AsyncCompletedEventArgs e)
        {
            //unzip
            unzip(LauncherZIP, LatestLauncherFolder);
            //create shortcut
            urlShortcutToDesktop("MWO-Launcher", (LatestLauncherFolder + "/MWOlauncher.exe"), (LatestLauncherFolder + "/mwoIcon.ico"));
            //allow press play
            Play.Enabled = true;
            //remove loading gif
            loadingLauncher.Visible = false;
            runAtStartup(LatestLauncherFolder);
        }

        //background worker on download progress change launcher
        public void _webClient_DownloadProgressChangedLauncher(object sender, DownloadProgressChangedEventArgs e)
        {
            //nothing here :P
        }

        //download launcher
        public void DownloadLauncher()
        {
            //enable loading icon
            loadingLauncher.Visible = true;
            //disable play button
            Play.Enabled = false;
            //initialize new webClient with backgorund worker
            LauncherWebClient = new WebClient();
            LauncherWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(_webClient_DownloadProgressChangedLauncher);
            LauncherWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(_webClient_DownloadFileCompletedLauncher);

            //txt to int
            string LauncherLatestVersionRead = System.IO.File.ReadAllText(LauncherLatestTXT);
            int LauncherLatestVersionF = int.Parse(LauncherLatestVersionRead);

            string LauncherLocalVersionRead = System.IO.File.ReadAllText(LauncherActualTXT);
            int LauncherLocalVersionF = int.Parse(LauncherLocalVersionRead);
         
            //create latest launcher folder
            LatestLauncherFolder = (launcherFolder + "/" + LauncherLatestVersionRead);
            System.IO.Directory.CreateDirectory(LatestLauncherFolder);
            //zip location
            LauncherZIP = (launcherFolder + "/" + LauncherLatestVersionRead + ".zip");
            //download zip
            LauncherWebClient.DownloadFileAsync(downloadLauncher, LauncherZIP);            
            //upgrade the version file
            System.IO.File.WriteAllText(LauncherActualTXT, LauncherLatestVersionRead);
            //update GUI
            LauncherBuild.Text = System.IO.File.ReadAllText(LauncherActualTXT);
        }

        //unzipper
        public void unzip(string file, string destination)
        {
            //instantiating the process
            ProcessStartInfo zip = new ProcessStartInfo();
            //adding .exe location
            zip.FileName = ZipEXE;
            //create arguments for 7zip
            zip.Arguments = string.Format("x " + file + " -o" + destination + "");
            zip.WindowStyle = ProcessWindowStyle.Normal;
            //start 7zip
            var process = Process.Start(zip);
            //process.WaitForExit();
            while (!process.HasExited)
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        //start game here
        public void StartGame()
        {
            ProcessStartInfo MWO = new ProcessStartInfo();

            //file to int
            string latestVersionRead = System.IO.File.ReadAllText(GameLatestTXT);
            int latestVersionF = int.Parse(latestVersionRead);

            string localVersionRead = System.IO.File.ReadAllText(GameActualTXT);
            int localVersionF = int.Parse(localVersionRead);
            string exePath = gameFolder + "/" + latestVersionRead + "/MWO.exe";
            //assign the .exe location to MWO process
            MWO.FileName = exePath;
            MWO.WindowStyle = ProcessWindowStyle.Normal;
            //start process
            Process.Start(MWO);
        }

        //game version control
        public bool GameUpdate()
        {
            WebClient webClient = new WebClient();
            //crating folders
            CreateFolders();

            //download latest version
            webClient.DownloadFile(GameLatestURL, GameLatestTXT);

            //if doesn't exists, crate new local version file
            if (!System.IO.File.Exists(GameActualTXT))
            {
                System.IO.File.Create(GameActualTXT).Close();
                System.IO.File.WriteAllText(GameActualTXT, "0");
            }

            //file to int
            string latestVersionRead = System.IO.File.ReadAllText(GameLatestTXT);
            int latestVersionF = int.Parse(latestVersionRead);

            string localVersionRead = System.IO.File.ReadAllText(GameActualTXT);
            int localVersionF = int.Parse(localVersionRead);

            GameBuild.Text = System.IO.File.ReadAllText(GameActualTXT);
            if (System.IO.File.ReadAllText(GameActualTXT) == null && System.IO.File.ReadAllText(GameActualTXT) == "0")
            {
                GameBuild.Text = "0";
            }

            //version comparison
            if (localVersionF < latestVersionF)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //launcher version control
        public bool LauncherUpdate()
        {
            WebClient webClient = new WebClient();
            //crating folders
            CreateFolders();

            //download latest version
            webClient.DownloadFile(LauncherLatestURL, LauncherLatestTXT);

            //if doesn't exists, crate new local version file
            if (!System.IO.File.Exists(LauncherActualTXT))
            {
                System.IO.File.Create(LauncherActualTXT).Close();
                System.IO.File.WriteAllText(LauncherActualTXT, "0");
            }

            //file to int
            string latestVersionRead = System.IO.File.ReadAllText(LauncherLatestTXT);
            int latestVersionF = int.Parse(latestVersionRead);

            string localVersionRead = System.IO.File.ReadAllText(LauncherActualTXT);
            int localVersionF = int.Parse(localVersionRead);

            LauncherBuild.Text = localVersionRead;

            runAtStartup(launcherFolder+"/"+localVersionRead);

            //version comparison
            if (localVersionF < latestVersionF)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //folder creation
        public void CreateFolders()
        {
            WebClient webClient = new WebClient();
            //creating base folders
            if (!System.IO.Directory.Exists(rootFolder))
            {
                System.IO.Directory.CreateDirectory(rootFolder);
                System.IO.Directory.CreateDirectory(launcherFolder);
                System.IO.Directory.CreateDirectory(gameFolder);
                System.IO.Directory.CreateDirectory(versionFolder);
            }
            //if doesn't exists, download 7zip.exe
            if (!System.IO.File.Exists(ZipEXE))
            {
                webClient.DownloadFile(downloadZipper, ZipEXE);
            }
        }

        //shortcut
        private void urlShortcutToDesktop(string linkName, string linkUrl, string iconLocation)
        {
            //desktop directory
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //if a link already exists, delete it
            if (System.IO.File.Exists((deskDir+"/MWOLauncher.lnk")))
            {
                System.IO.File.Delete((deskDir+"/MWOLauncher.lnk"));
            }
            //instantiating the class
            WshShell shell = new WshShell();
            //create shortcut
            IWshShortcut link = (IWshShortcut)shell.CreateShortcut((deskDir+"/MWO-Launcher.lnk"));
            //shortcut description
            link.Description = "MWO launcher";
            //shortcut icon
            link.IconLocation = iconLocation;
            //shortcut destination
            link.TargetPath = linkUrl;
            //save shortcut
            link.Save();
        }

        //double click on icon in tray 
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //show form
            this.Show();
            //hide icon in tray
            notifyIcon.Visible = false;
        }
        
        //icon menu, click on exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //close form
            this.Close();
        }

        //icon menu, click on open mwo-launcher
        private void openMWOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //show form
            this.Show();
            //hide icon
            notifyIcon.Visible = false;
        }

        //icon menu, click on play MWO
        private void playMWOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check for update. if not, start game
            if (GameUpdate())
            {
                DownloadGame();
            }
            else
            {
                //set loading bar to full
                DownloadBar.Value = 100;
                StartGame();
            }
        }

	}
}
