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
		private static MainFm _mf;
		private const int Verze = 170429;
		private const int PotrebnySoubor = 113;

		[STAThread]
		static void Main(string[] args)
		{
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
			var errors = "";
			if (KnihovnaSouboru.Soubor.Verze < PotrebnySoubor) {
				errors +=
					$"KnihovnaSouboru.dll, Verze: {KnihovnaSouboru.Soubor.Verze}, Minimální potřebná verze: {PotrebnySoubor} {Environment.NewLine}";
			}
			if (errors != "") {
				MessageBox.Show($"Aplikace používá zastaralé knihovny: \n {errors} Aktualizujte je!",
								"Zastaralé knihovny", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
				return;
			}
			AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
			_mf = new MainFm(spousteni);
			RegistryKey key = Registry.LocalMachine.OpenSubKey(Registr, true);
			if (key == null) {
				key = Registry.LocalMachine.CreateSubKey(Registr);
				_mf.Log.Vlozit("Složka v registrech byla úspěšně vytvořena");
			}
			key.SetValue("ToDo", _mf.ToDo.CelaCesta);
			key.SetValue("ServerLog", _mf.ServerLog.CelaCesta);
			key.SetValue("Log", _mf.Log.CelaCesta);
			key.SetValue("GUIVersion", Verze);
			key.SetValue("Location", Application.ExecutablePath);
			key.SetValue("Localization", "CZ");
			_mf.Log.Vlozit("Všechny klíče byly uspěšně zapsány do registrů");
			key.Close();
			Application.Run(_mf);
		}

		static void OnProcessExit(object sender, EventArgs e)
		{
			try {
				var service =
					new System.ServiceProcess.ServiceController("PCManagerService");
				service.Stop();
				service.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped);
			} catch { }
		}

		private static string[] NormArgs(string[] args)
		{
			string[] newArgs = args;
			for (int i = 0; i < newArgs.Length; i++) {
				newArgs[i] = args[i].Replace("-", "");
				newArgs[i] = args[i].Replace("+", "");
				newArgs[i] = args[i].Replace("-", "");
				newArgs[i] = args[i].Replace("/", "");
				newArgs[i] = args[i].Replace("=", "");
			}
			return newArgs;
		}
	}
}