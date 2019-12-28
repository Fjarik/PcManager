using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.ServiceProcess.Design;

namespace PCManagerSluzba
{
	[RunInstaller(true)]
	public partial class ProjectInstaller : Installer
	{
		public ProjectInstaller() {
			InitializeComponent();
		}
	}
}
