using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Linq;
using System.Diagnostics;

namespace MWOlauncher
{
	public partial class MainForm : Form
	{
		public CheckVersion checkVersion = new CheckVersion();
		
		public MainForm()
		{
			InitializeComponent(); 
			// avvio il controllo della versione del launcher
			checkVersion.LauncherUpdate();
		}
	}
}
