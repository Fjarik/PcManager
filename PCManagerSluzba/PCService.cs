using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using System.Text;
using System.Threading.Tasks;

namespace PCManagerSluzba
{
	public partial class PCService : ServiceBase
	{
		public Server server;

		public PCService()
		{
			//InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			//Debugger.Launch();
			this.server = new Server();
			/*ProcessStartInfo start = new ProcessStartInfo() {
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				FileName = server.GuiCesta
			};
			Process.Start(start);*/
			this.server.Start();
		}

		internal void Stop()
		{
			this.OnStop();
		}

		protected override void OnStop()
		{
			this.server.Stop();
		}

		internal void StartDebug()
		{
			this.OnStart(null);
		}
	}
}