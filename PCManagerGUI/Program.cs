using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCManagerGUI
{
	static class Program
	{
		private static string Registr = @"SOFTWARE\Opencube\PCManager";
		private static MainFm Mf;
		public static int Verze = 170429;
		public static int PotrebnySoubor = 113;

		[STAThread]
		static void Main(string[] args) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "KnihovnaSouboru.dll")) {
				MessageBox.Show("Aplikaci chybí soubor KnihovnaSouboru.dll");
				return;
			}
			string[] spousteni = args;
			if (spousteni != null && spousteni.Length > 0) {
				spousteni = NormArgs(spousteni);
			}
			Mutex appMutex = new Mutex(true, Application.ProductName, out bool spusteno);
			if (!spusteno) {
				MessageBox.Show("Aplikace již běží!");
				Application.Exit();
				return;
			}
			string Errors = "";
			if (KnihovnaSouboru.Soubor.Verze < PotrebnySoubor) {
				Errors += $"KnihovnaSouboru.dll, Verze: {KnihovnaSouboru.Soubor.Verze}, Minimální potřebná verze: {PotrebnySoubor} {Environment.NewLine}";
			}
			if (Errors != "") {
				MessageBox.Show($"Aplikace používá zastaralé knihovny: \n {Errors} Aktualizujte je!", "Zastaralé knihovny", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
				return;
			}
			AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
			Mf = new MainFm(spousteni);
			RegistryKey key = Registry.LocalMachine.OpenSubKey(Registr, true);
			if (key == null) {
				key = Registry.LocalMachine.CreateSubKey(Registr);
				Mf.Log.Vlozit("Složka v registrech byla úspěšně vytvořena");
			}
			key.SetValue("ToDo", Mf.ToDo.CelaCesta);
			key.SetValue("ServerLog", Mf.ServerLog.CelaCesta);
			key.SetValue("Log", Mf.Log.CelaCesta);
			key.SetValue("GUIVersion", Verze);
			key.SetValue("Location", Application.ExecutablePath);
			key.SetValue("Localization", "CZ");
			Mf.Log.Vlozit("Všechny klíče byly uspěšně zapsány do registrů");
			key.Close();
			Application.Run(Mf);
		}

		static void OnProcessExit(object sender, EventArgs e) {
			try {
				System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController("PCService");
				service.Stop();
				service.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped);
			} catch { }
		}

		private static string[] NormArgs(string[] args) {
			string[] New = args;
			for (int i = 0; i < New.Length; i++) {
				New[i].Replace("-", "");
				New[i].Replace("+", "");
				New[i].Replace("-", "");
				New[i].Replace("/", "");
				New[i].Replace("=", "");
			}
			return New;
		}
	}
}
