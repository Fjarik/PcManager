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
	public partial class PCManagerService : ServiceBase
	{
		public Server server;
		public const string EventSource = "PCManager";
		public const string EventLogName = "Opencube";
		private int _eventId = 1;
		//private Timer _timer;

		public PCManagerService()
		{
			InitializeComponent();
			if (!EventLog.SourceExists(EventSource)) {
				EventLog.CreateEventSource(EventSource, EventLogName);
			}
			mainEventLog.Source = EventSource;
			mainEventLog.Log = EventLogName;
		}

		protected override void OnStart(string[] args)
		{
			//Debugger.Launch();
			mainEventLog.WriteEntry("Service start requested.", EventLogEntryType.Information, _eventId++);

			//_timer = new Timer(60000 /* 60 Seconds */);
			//_timer.Elapsed += TimerOnElapsed;
			//_timer.Start();

			this.server = new Server(mainEventLog);
			/*ProcessStartInfo start = new ProcessStartInfo() {
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				FileName = server.GuiCesta
			};
			Process.Start(start);*/
			this.server.Start();
		}

		private void TimerOnElapsed(object sender, ElapsedEventArgs e)
		{
			mainEventLog.WriteEntry("Time elapsed.", EventLogEntryType.Information, _eventId++);
		}

		protected override void OnStop()
		{
			mainEventLog.WriteEntry("Service stop requested.", EventLogEntryType.Information, _eventId++);
			//_timer.Stop();
			this.server.Stop();
			mainEventLog.WriteEntry("Service stopped.", EventLogEntryType.Information, _eventId++);
		}

		internal void StartDebug()
		{
			this.OnStart(null);
		}
	}
}