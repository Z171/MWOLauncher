using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace MWOlauncher
{
	public class CheckVersion
	{
		readonly WebClient webClient = new WebClient();
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
		Uri downloadLauncher = new Uri ("http://itmakers.altervista.org/MWO/launcher/launcher.zip");
		Uri downloadZipper = new Uri ("http://itmakers.altervista.org/MWO/7za.exe");
		Uri GameLatestURL = new Uri ("http://itmakers.altervista.org/MWO/game/LatestVersion.txt");
		Uri LauncherLatestURL = new Uri ("http://itmakers.altervista.org/MWO/launcher/launcherVersion.txt");
		
		
		public void GameUpdate()
		{
			//boh trovato du internet
			System.Threading.AutoResetEvent waiter = new System.Threading.AutoResetEvent (false);
			//creo le cartelle
			CreateFolders();
			//inizializzo zipper e exe del programma
			ProcessStartInfo zip = new ProcessStartInfo();
			ProcessStartInfo MWO = new ProcessStartInfo();
			zip.FileName = ZipEXE;
			
			//scarico il file che contiene l'ultima versione
		    webClient.DownloadFile(GameLatestURL, GameLatestTXT);
			
		    //se non c'è un file di versione locale, lo creo e lo azzero
			if (!System.IO.File.Exists(GameActualTXT))
			{
				System.IO.File.Create(GameActualTXT).Close();
				System.IO.File.WriteAllText(GameActualTXT, "0");
			}
			
			//confronto le versioni
			string latestVersionRead = System.IO.File.ReadAllText(GameLatestTXT);
			int latestVersionF = int.Parse(latestVersionRead);
			
			string localVersionRead = System.IO.File.ReadAllText(GameActualTXT);
			int localVersionF = int.Parse(localVersionRead);
			
			if (localVersionF < latestVersionF)
			{
				//cartella del gioco alla versione attuale
				GameFolder = (gameFolder + "/" + latestVersionRead);
				//cartella dell'ultima versione
				string LastGameFolder = (gameFolder + "/" + localVersionRead);
				//se esiste una vecchia cartella, la elimino
				if (System.IO.Directory.Exists(LastGameFolder))
				{
					System.IO.Directory.Delete(LastGameFolder, true);
				}
				//creo la nuova cartella (chiamata ultimaVersione)
				System.IO.Directory.CreateDirectory(GameFolder);
				//debug
				System.Windows.Forms.MessageBox.Show("Downloading new version! " +latestVersionRead);
				//inizializzo la posizione del file zip
				GameZIP = (gameFolder +"/" + latestVersionRead + ".zip");
				//dovrebbe controllare lo stato del download, ma non va
				webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
				//scarica l'ultima versione zippata
				Uri downloadGame = new Uri(downloadGameBase + latestVersionRead + ".zip");
				webClient.DownloadFileAsync(downloadGame, GameZIP, waiter);
				//boh, trovato su internet
				waiter.WaitOne ();
				//scompatto
				zip.Arguments = string.Format("e "+GameZIP+" -o"+GameFolder+"");
				zip.WindowStyle = ProcessWindowStyle.Hidden;
				Process.Start(zip);
				//elimino il file zip (potrei togliere l'if, ma meglio tenerlo)
				if (System.IO.File.Exists(GameZIP))
				{
					System.IO.File.Delete(GameZIP);
				}
				//aggiorno la versione
				System.IO.File.WriteAllText(GameActualTXT, latestVersionRead);
			}	
			//directory dell'eseguibile			
			string tmpMWO = GameFolder + "/" + "MWO" + latestVersionRead + ".exe";
			//dichiaro l'eseguibile per il processo MWO
			MWO.FileName = tmpMWO;
			//debug
			System.Windows.Forms.MessageBox.Show("Starting MWO");
			//avvio il programma e chiudo il form
			Process.Start(MWO);
			MainForm mainForm = new MainForm();
			mainForm.Close();
		}
		
		public void CreateFolders()
		{
			//creo le cartelle fondamentali
			if (!System.IO.Directory.Exists(rootFolder))
			{
				System.IO.Directory.CreateDirectory(rootFolder);
				System.IO.Directory.CreateDirectory(launcherFolder);
				System.IO.Directory.CreateDirectory(gameFolder);
				System.IO.Directory.CreateDirectory(versionFolder);
			}			
			if (!System.IO.File.Exists(ZipEXE))
			{
				webClient.DownloadFile(downloadZipper, ZipEXE);
			}
		}
		
		//controllo versione launcher
		public void LauncherUpdate()
		{
			//creo le cartelle se non esistono
			CreateFolders();
			//inizializzo l'utility zip
			ProcessStartInfo zip = new ProcessStartInfo();
			zip.FileName = ZipEXE;			
			
			//creo il file latest se non esiste (versione)
			if (!System.IO.File.Exists(LauncherLatestTXT))
			{
				System.IO.File.Create(LauncherLatestTXT).Close();
			}
			//scarico il file della versione
			webClient.DownloadFile(LauncherLatestURL, LauncherLatestTXT);
			//creo il file della versione attuale, se non c'è, è 0
			if (!System.IO.File.Exists(LauncherActualTXT))
			{
				System.IO.File.Create(LauncherActualTXT).Close();
				System.IO.File.WriteAllText(LauncherActualTXT, "0");
			}
			//da file a int delle versioni
			string LauncherLatestVersionRead = System.IO.File.ReadAllText(LauncherLatestTXT);
			int LauncherLatestVersionF = int.Parse(LauncherLatestVersionRead);
			
			string LauncherLocalVersionRead = System.IO.File.ReadAllText(LauncherActualTXT);
			int LauncherLocalVersionF = int.Parse(LauncherLocalVersionRead);
			//confronto delle versioni
			if (LauncherLocalVersionF < LauncherLatestVersionF)
			{
				//creo la cartella della nuova versione del launcher
				LatestLauncherFolder = (launcherFolder + "/" + LauncherLatestVersionRead);
				System.IO.Directory.CreateDirectory(LatestLauncherFolder);
				//dove mettere lo zip dell'ultima versione
				LauncherZIP = (launcherFolder + "/" + LauncherLatestVersionRead + ".zip");
				System.Windows.Forms.MessageBox.Show("New Launcher update");
				//scarico il file zip del launcher
				webClient.DownloadFileAsync(downloadLauncher, LauncherZIP);
				//scompatto
				zip.Arguments = string.Format("x "+LauncherZIP+" -o"+LatestLauncherFolder+"");
				zip.WindowStyle = ProcessWindowStyle.Hidden;
				Process.Start(zip);
				//aggiorno il file della vrsione
				System.IO.File.WriteAllText(LauncherActualTXT, LauncherLatestVersionRead);
				System.IO.File.Delete(LauncherZIP);
			}
		}
		
		public void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			//in teoria aggiorna la barra di caricamento, ma non va
	    	double bytesIn = double.Parse(e.BytesReceived.ToString());
	    	double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
	    	double kbIn = bytesIn / 1024;
	    	double mbIn = kbIn / 1024;
	    	double percentage = bytesIn / totalBytes * 100;
	    	MainForm mainForm = new MainForm();
	    	//mainForm.progressBar1.Value = percentage.ToString();
		}
		
		void Completed(object sender, System.EventArgs e)
		{
			//in teoria manda un messaggio a download finito, ma non va
			System.Windows.Forms.MessageBox.Show("Update finished");
		}
	}
}
