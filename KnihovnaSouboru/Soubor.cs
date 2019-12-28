using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace KnihovnaSouboru
{
	public class Soubor
	{
		public static readonly int Verze = 113;

		public string Cesta { get; private set; }
		public string NazevSouboru { get; private set; }
		public string CelaCesta { get; private set; }
		private string[] RadkyS;
		List<string> RadkyL = new List<string>();

		public Soubor(string cesta, string NazevSouboru, bool Prepsat) {
			this.Cesta = cesta;
			this.NazevSouboru = NazevSouboru;
			this.CelaCesta = cesta + NazevSouboru;
			if (!Directory.Exists(cesta)) {
				Directory.CreateDirectory(cesta);
			}
			if (!File.Exists(CelaCesta)) {
				File.Create(CelaCesta).Close();
				this.Vlozit($"Soubor {NazevSouboru} vytvořen!");
			} else {
				if (Prepsat) {
					File.WriteAllText(CelaCesta, string.Empty);
				}
			}
		}

		public Soubor(string CelaCesta, bool Prepsat) {
			this.CelaCesta = CelaCesta;
			int od = CelaCesta.LastIndexOf('/');
			if (od == -1) {
				od = CelaCesta.LastIndexOf('\\');
			}

			this.NazevSouboru = CelaCesta.Substring(od + 1, (CelaCesta.Length - 1 - od));
			this.Cesta = CelaCesta.Substring(0, NazevSouboru.Length);

			if (!Directory.Exists(Cesta)) {
				Directory.CreateDirectory(Cesta);
			}
			if (!File.Exists(CelaCesta)) {
				File.Create(CelaCesta).Close();
				this.Vlozit($"Soubor {NazevSouboru} vytvořen!");
			} else {
				if (Prepsat) {
					File.WriteAllText(CelaCesta, string.Empty);
				}
			}
		}

		public void Vlozit(string text) {
			string vlozit = $"{DateTime.Now} {text}";
			File.AppendAllText(CelaCesta, vlozit + Environment.NewLine);
		}

		public void VlozitBezDate(string text) {
			File.AppendAllText(CelaCesta, text + Environment.NewLine);
		}

		public string[] VratS() {
			this.Precti();
			return RadkyS;
		}

		public List<string> VratL() {
			this.Precti();
			return RadkyL;
		}

		private void Precti() {
			this.RadkyS = File.ReadAllLines(CelaCesta);
			this.RadkyL = File.ReadLines(CelaCesta).ToList();
		}

		public void Clear() {
			this.RadkyL = null;
			this.RadkyS = null;
			File.WriteAllText(CelaCesta, string.Empty);
		}

	}
}
