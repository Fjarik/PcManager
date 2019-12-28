using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;

namespace PCManagerSluzba
{
	static class Program
	{
		static void Main(params string[] args)
		{
			var service = new PCManagerService();

			var parameters = args.Where(x => x.StartsWith("--")).ToList();
			if (parameters.Any(x => x == "--uninstall")) {
				EventLog.DeleteEventSource("PCManager");
				ManagedInstallerClass.InstallHelper(new[] {"/u", Assembly.GetExecutingAssembly().Location});
				return;
			}
			if (parameters.Any(x => x == "--install")) {
				ManagedInstallerClass.InstallHelper(new[] {Assembly.GetExecutingAssembly().Location});
				return;
			}

			if (args.Any(x => x == "debug")) {
				service.StartDebug();
				return;
			}

			ServiceBase.Run(service);
		}
	}
}