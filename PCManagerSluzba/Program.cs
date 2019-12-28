using System;
using System.Linq;
using System.ServiceProcess;

namespace PCManagerSluzba
{
	static class Program
	{
		static void Main(params string[] args)
		{
			var service = new PCService();

			if (Environment.UserInteractive) {
				service.StartDebug();
				Console.WriteLine("Press any key to stop program");
				Console.Read();
				service.Stop();
				return;
			}

			if (string.Equals(args.FirstOrDefault(), "debug", StringComparison.OrdinalIgnoreCase)) {
				service.StartDebug();
				System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
			}

			ServiceBase.Run(service);
		}
	}
}