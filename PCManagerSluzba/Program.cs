using System;
using System.Linq;
using System.ServiceProcess;

namespace PCManagerSluzba
{
	static class Program
	{
		static void Main(params string[] args) {
			if (string.Equals(args.FirstOrDefault(), "debug", StringComparison.OrdinalIgnoreCase)) {
				var service = new PCService();
				service.StartDebug();
				System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
			}

			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[]
			{
				new PCService()
			};
			ServiceBase.Run(ServicesToRun);
		}
	}
}
