using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using KnihovnaSouboru;

namespace PCManagerGUI
{
	public partial class MainFm : Form
	{

		#region Imports
		[DllImport("user32")]
		public static extern void LockWorkStation();

		[DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
		private const int APPCOMMAND_VOLUME_UP = 0xA0000;
		private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
		private const int WM_APPCOMMAND = 0x319;
		#endregion

		private string[] args;
		public Soubor ServerLog { get; private set; }
		public Soubor ToDo { get; private set; }
		public Soubor Log { get; private set; }
		public string ServiceName = @"PCService";

		public MainFm(string[] Parametry) {
			InitializeComponent();
			ToDo = new Soubor(@"C:\temp\", "ToDo.txt", false);
			ServerLog = new Soubor(@"C:\temp\", "ServerLog.txt", true);
			Log = new Soubor(@"C:\temp\", "Log.txt", false);
			Log.Vlozit("Všechny soubory vytvořeny/načteny");
			if (Parametry != null && Parametry.Length > 0) {
				string argumenty = "";
				foreach (string a in Parametry) {
					argumenty += a + ", ";
				}
				Log.Vlozit($"Pokus o spuštění programu s následujícími paramatry: {argumenty}");
			}
			CheckServiceTimer.Enabled = true;
			ServiceController controller = new ServiceController(ServiceName);
			if (controller.Status == ServiceControllerStatus.Stopped) {
				controller.Start();
				ServerLog.Vlozit("Služba spuštěna");
				Log.Vlozit("Služba úspěšně spuštěna");
			}
			CheckService();
		}

		public void CheckService() {
			ServiceController controller = new ServiceController(ServiceName);
			if (controller.Status == ServiceControllerStatus.Stopped) {
				LblActive.ForeColor = Color.Red;
				LblActive.Text = "Neaktivní";
			} else if (controller.Status == ServiceControllerStatus.Running) {
				LblActive.ForeColor = Color.Green;
				LblActive.Text = "Aktivní";
			}
		}

		public void NIcon_BalloonTipClicked(object sender, EventArgs e) {
			ServerLog.Vlozit("Přikaz zrušen");
			Log.Vlozit("Příchozí příkaz byl zrušen uživatelem");
		}

		private void CheckAndDo() {
			if (args.Length <= 0 || args[0] == "") {
				return;
			}
			switch (args[0].ToLower()) {
				case "lock":
					BShow("Uzamknout", 300, true);
					break;
				case "hybernate":
					BShow("Hibernovat", 500, true);
					break;
				case "shutdown":
					BShow("Vypnout", 1000, true);
					break;
				case "sleep":
					BShow("Uspat", 500, true);
					break;
				case "restart":
					BShow("Restartovat", 1000, true);
					break;
				case "mute":
					SendMessageW(Handle, WM_APPCOMMAND, Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
					Log.Vlozit("Zvuky vypnuty");
					break;
				case "up":
					SendMessageW(Handle, WM_APPCOMMAND, Handle, (IntPtr)APPCOMMAND_VOLUME_UP);
					Log.Vlozit("Hlasitost zvýšena");
					break;
				case "down":
					SendMessageW(Handle, WM_APPCOMMAND, Handle, (IntPtr)APPCOMMAND_VOLUME_DOWN);
					Log.Vlozit("Počítač snížena");
					break;
				case "hide":
					CheckServiceTimer.Enabled = false;
					ReadTimer.Enabled = false;
					break;
				default:
					string poslat = "";
					foreach (string a in args) {
						poslat = $"{poslat} {a}";
					}
					BShow($"Příchozí zpráva: {poslat}", 1000, false);
					Log.Vlozit($"Zobrazena následující zpráva: {poslat}");
					break;
			}
		}

		public void BShow(string text, int cas, bool prikaz) {
			if (prikaz) {
				NIcon.BalloonTipText = $"{text} \r\nKlikni pro přerušení!";
			} else {
				NIcon.BalloonTipText = text;
			}
			NIcon.ShowBalloonTip(cas);
		}

		public void NIcon_MouseClick(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Right) {
				CxMenu.Show(Control.MousePosition);
			} else if (e.Button == MouseButtons.Left) {
				Show();
				CheckServiceTimer.Enabled = true;
				ReadTimer.Enabled = true;
			}
		}

		private void EndApp_Click(object sender, EventArgs e) {
			Log.Vlozit("Aplikace ukončena");
			Application.Exit();
			Environment.Exit(0);
		}

		private void EndService_click(object sender, EventArgs e) {
			Thread.Sleep(2000);
			if (LblActive.Text == "Neaktivní") {
				BShow("Služba je již zastavena", 200, false);
				Log.Vlozit("Pokus o zastavení již zastavené služby");
				return;
			}
			ServiceController controller = new ServiceController(ServiceName);
			controller.Stop();
			controller.WaitForStatus(ServiceControllerStatus.Stopped);
			BShow("Služba zastavena", 200, false);
			ServerLog.Vlozit("Služba zastavena uživatelem");
			Log.Vlozit("Služba zastavena uživatelem");
			ReadTimer.Enabled = false;
			Log.Vlozit("Čtění příkazů pozastaveno");
		}

		private void Show_Click(object sender, EventArgs e) {
			Show();
			CheckServiceTimer.Enabled = true;
			ReadTimer.Enabled = true;
		}

		private void About_Click(object sender, EventArgs e) {
			OProgramu About = new OProgramu();
			About.Show();
			Log.Vlozit("Zobrazeno okno 'O programu'");
		}

		private void MainFm_FormClosing(object sender, FormClosingEventArgs e) {
			if (e.CloseReason == CloseReason.UserClosing) {
				Hide();
				e.Cancel = true;
				Log.Vlozit("Aplikace minimalizována");
			}
		}

		private void CheckServiceTimer_Tick(object sender, EventArgs e) {
			if (WindowState == FormWindowState.Minimized) {
				CheckServiceTimer.Enabled = false;
			}
			CheckService();
		}

		private void MainFm_Resize(object sender, EventArgs e) {
			if (WindowState == FormWindowState.Minimized) {
				CheckServiceTimer.Enabled = false;
				Log.Vlozit("Kontolování služby pozastaveno");
			} else {
				CheckServiceTimer.Enabled = true;
				Log.Vlozit("Kontolování služby znovu spuštěno");
			}
		}

		private void ReadTimer_Tick(object sender, EventArgs e) {
			string[] precteno = ToDo.VratS();
			if (precteno.Length < 1) {
				return;
			}
			args = precteno;
			CheckAndDo();
			ToDo.Clear();
			Log.Vlozit("Soubor ToDo.txt vymazán");
		}

		private void ChBox_AutoLoad_CheckedChanged(object sender, EventArgs e) {
			if (ChBox_AutoLoad.Checked) {
				VypisTimer.Enabled = true;
				return;
			}
			VypisTimer.Enabled = false;
		}

		private void VypisTimer_Tick(object sender, EventArgs e) {
			TxtLog.Clear();
			foreach (string a in ServerLog.VratS()) {
				TxtLog.AppendText(a + Environment.NewLine);
			}
		}

		private void Btn_End_Click(object sender, EventArgs e) {
			Log.Vlozit("Žádost o celkové zastavení");
			EndService_click(sender, e);
			EndApp_Click(sender, e);
		}

		private void NIcon_BalloonTipClosed(object sender, EventArgs e) {
			if (args == null || args.Length <= 0) {
				return;
			}
			switch (args[0].ToLower()) {
				case "lock":
					LockWorkStation();
					Log.Vlozit("Počítač uzamknut");
					break;
				case "hybernate":
					Process.Start("shutdown", "/h /f");
					Log.Vlozit("Počítač hibernován");
					break;
				case "shutdown":
					Process.Start("shutdown", "/s /t 0");
					Log.Vlozit("Počítač vypnut");
					break;
				case "sleep":
					SetSuspendState(false, true, true);
					Log.Vlozit("Počítač uspán");
					break;
				case "restart":
					Process.Start("shutdown", "/r /t 0");
					Log.Vlozit("Počítač restartován");
					break;
				default:
					break;
			}
		}

		private void Btn_ZapVyp_Click(object sender, EventArgs e) {
			ServiceController controller = new ServiceController(ServiceName);
			if (controller.Status == ServiceControllerStatus.Running) {
				EndService_click(sender, e);
				Btn_ZapVyp.Text = "Zapnout";
			} else if (controller.Status == ServiceControllerStatus.Stopped) {
				controller.Start();
				controller.WaitForStatus(ServiceControllerStatus.Running);
				ServerLog.Vlozit("Služba spuštěna uživatelem");
				Btn_ZapVyp.Text = "Vypnout";
				ReadTimer.Enabled = true;
				Log.Vlozit("Čtení příkazů spuštěno");
			}
		}

		private void MainFm_FormClosed(object sender, FormClosedEventArgs e) {
			NIcon.Visible = false;
		}

	}
}
