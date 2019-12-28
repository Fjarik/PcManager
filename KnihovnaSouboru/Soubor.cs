using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace KnihovnaSouboru
{
	public class Soubor
	{
		public const int Verze = 113;

		public string Cesta { get; }
		public string NazevSouboru { get; }
		public string CelaCesta { get; }
		private string[] _radkyS;
		private List<string> _radkyL = new List<string>();

		public Soubor(string cesta, string nazevSouboru, bool prepsat)
		{
			this.Cesta = cesta;
			this.NazevSouboru = nazevSouboru;
			this.CelaCesta = cesta + nazevSouboru;
			if (!Directory.Exists(cesta)) {
				Directory.CreateDirectory(cesta);
			}
			if (!File.Exists(CelaCesta)) {
				File.Create(CelaCesta).Close();
				this.Vlozit($"Soubor {nazevSouboru} vytvořen!");
				return;
			}

			if (prepsat) {
				File.WriteAllText(CelaCesta, string.Empty);
			}
		}

		public Soubor(string celaCesta, bool prepsat)
		{
			this.CelaCesta = celaCesta;
			int od = celaCesta.LastIndexOf('/');
			if (od == -1) {
				od = celaCesta.LastIndexOf('\\');
			}

			this.NazevSouboru = celaCesta.Substring(od + 1, (celaCesta.Length - 1 - od));
			this.Cesta = celaCesta.Substring(0, NazevSouboru.Length);

			if (!Directory.Exists(Cesta)) {
				Directory.CreateDirectory(Cesta);
			}
			if (!File.Exists(celaCesta)) {
				File.Create(celaCesta).Close();
				this.Vlozit($"Soubor {NazevSouboru} vytvořen!");
				return;
			}
			if (prepsat) {
				File.WriteAllText(celaCesta, string.Empty);
			}
		}

		public void Vlozit(string text)
		{
			string vlozit = $"{DateTime.Now}: {text}";
			File.AppendAllText(CelaCesta, vlozit + Environment.NewLine);
		}

		public void VlozitBezDate(string text)
		{
			File.AppendAllText(CelaCesta, text + Environment.NewLine);
		}

		public string[] VratS()
		{
			this.Precti();
			return _radkyS;
		}

		public List<string> VratL()
		{
			this.Precti();
			return _radkyL;
		}

		private void Precti()
		{
			this._radkyS = File.ReadAllLines(CelaCesta);
			this._radkyL = File.ReadLines(CelaCesta).ToList();
		}

		public void Clear()
		{
			this._radkyL = null;
			this._radkyS = null;
			File.WriteAllText(CelaCesta, string.Empty);
		}
	}
}