using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.ServiceProcess;
using Microsoft.Win32;
using KnihovnaSouboru;

namespace PCManagerSluzba
{
	public class Server
	{
		private const int Verze = 111;

		private readonly List<string> _overeneMac;
		private readonly Thread _listeningThreat;
		private TcpListener _listener;
		private readonly List<Socket> _clients = new List<Socket>();

		public Soubor ToDo { get; }
		public Soubor ServerLog { get; }
		public Soubor MacAdresy { get; }
		public Soubor Log { get; }

		private string AppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

		public Server()
		{
			/*OvereneMac.Add("02:00:00:00:00:01");
			OvereneMac.Add("48-45-20-B9-53-12");
			OvereneMac.Add("2C:59:8A:57:42:8F");*/
			string toDoLoc = "";
			string serverLogLoc = "";
			string logLoc = "";

			RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Opencube\PCManager\");
			if (key != null) {
				try {
					key.SetValue("ServiceVersion", Verze);

					toDoLoc = key.GetValue("Todo").ToString();
					serverLogLoc = key.GetValue("ServerLog").ToString();
					logLoc = key.GetValue("Log").ToString();
					key.Close();
				} catch {
					throw new Exception("Nelze přečíst záznamy v registrech");
				}
			} else {
				throw new
					Exception("Nelze najít nezbytné záznamy v registrech! \n Nejdříve spusťte aplikaci PCManager.exe");
			}
			MacAdresy = new Soubor(@"C:\temp\", "Mac.txt", false);
			ToDo = new Soubor(toDoLoc, true);
			ServerLog = new Soubor(serverLogLoc, false);
			Log = new Soubor(logLoc, false);
			Log.Vlozit("Soubor úspěšně načteny");

			_overeneMac = MacAdresy.VratL();
			Log.Vlozit("Ověřené MAC adresy úspěšně načteny");

			_listeningThreat = new Thread(ListeningThread);
		}

		public void Start()
		{
			_listeningThreat.Start();
		}

		public void Stop()
		{
			ServerLog.Vlozit("Vypínání serveru!");
			foreach (Socket s in _clients) {
				s.Close();
			}
			_clients.Clear();
			_listeningThreat.Abort();
			Log.Vlozit("Server vypnut");
		}

		public void ListeningThread()
		{
			string ip = GetLocalIPv4();
			int port = 7314;

			_listener = new TcpListener(IPAddress.Any, port);

			try {
				_listener.Start();
				ServerLog.Vlozit("Server spuštěn!");
				Log.Vlozit("Server spuštěn");
			} catch (Exception ex) {
				Log.Vlozit($"Chyba: {ex.ToString()}");
				_listeningThreat.Abort();
				ProjectInstaller po = new ProjectInstaller();
				po.serviceController1.Stop();
				po.serviceController1.WaitForStatus(ServiceControllerStatus.Stopped);
				throw new Exception("Nepodařilo se spustit server");
			}
			ServerLog.Vlozit($"Naslouchání: {ip}:{port}");
			Log.Vlozit($"Naslouchání: {ip}:{port}");
			while (true) {
				if (_listener.Pending()) {
					_clients.Add(_listener.AcceptSocket());
					ServerLog.Vlozit("Klient připojen!");
					Log.Vlozit("Klient připojen");
				}

				try {
					foreach (Socket sock in _clients) {
						if (sock.Poll(0, SelectMode.SelectError)) {
							_clients.Remove(sock);
						} else if (sock.Poll(0, SelectMode.SelectRead)) {
							Decrypt(sock);
						}
					}
					Thread.Sleep(30);
#pragma warning disable CS0168 // Variable is declared but never used
				} catch (InvalidOperationException ex) { } catch {
#pragma warning restore CS0168 // Variable is declared but never used
					_clients.Clear();
					ServerLog.Vlozit("Všichni klienti byli násilně odpojeni!");
					Log.Vlozit("Všichni klienti byli násilně odpojeni!");
				}
			}
		}

		private void Decrypt(Socket s)
		{
			string prichoziText = "";
			try {
				byte[] data = new byte[s.SendBufferSize];
				int pocet = s.Receive(data);
				byte[] dataA = new byte[pocet];
				for (int i = 0; i < pocet; i++) {
					dataA[i] = data[i];
				}
				prichoziText = Encoding.Default.GetString(dataA);
			} catch {
				RemoveClientInfo(s);
				s.Close();
				ServerLog.Vlozit("Klient násilně odpojen!");
				Log.Vlozit("Klient násilně odpojen!");
				return;
			}
			if (prichoziText.Length == 0 || prichoziText.ToLower() == "dc" || prichoziText.ToLower() == "disconnect") {
				RemoveClientInfo(s);
				ServerLog.Vlozit("Klient odpojen!");
				Log.Vlozit("Klient násilně odpojen!");
				return;
			}
			CheckAndDo(prichoziText);
		}

		private void CheckAndDo(string vstup)
		{
			if (!vstup.Contains(";")) {
				Log.Vlozit($"Příchozí text: {vstup}");
				ToDo.VlozitBezDate(vstup);
				return;
			}
			string[] casti = vstup.Split(';');
			if (casti.Length < 3) {
				return;
			}
			if (!_overeneMac.Contains(casti[0])) {
				Log.Vlozit($"Zablokován pokus o připojení z neověřené Mac Adresy, Příchozí text {vstup}");
				return;
			}
			if (casti[1] != "A3b7h6!(q1") {
				Log.Vlozit($"Zablokován pokus o připojení z důvodu nesprávného hesla, Příchozí text {vstup}");
				return;
			}
			Log.Vlozit($"Příchozí příkaz od {casti[0]}: {casti[2]}");
			ToDo.VlozitBezDate(casti[2].ToLower());
		}

		private void RemoveClientInfo(Socket socket)
		{
			for (int i = 0; i < _clients.Count; i++) {
				if (_clients[i] == socket) {
					_clients.RemoveAt(i);
					socket.Close();
					break;
				}
			}
		}

		public string GetLocalIPv4()
		{
			foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces()) {
				if ((item.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
					 item.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
					item.OperationalStatus == OperationalStatus.Up) {
					foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses) {
						if (ip.Address.AddressFamily == AddressFamily.InterNetwork) {
							return ip.Address.ToString();
						}
					}
				}
			}
			return string.Empty;
		}
	}
}