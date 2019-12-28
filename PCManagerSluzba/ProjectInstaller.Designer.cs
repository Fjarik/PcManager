namespace PCManagerSluzba
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
			this.Installer1 = new System.ServiceProcess.ServiceInstaller();
			this.serviceController1 = new System.ServiceProcess.ServiceController();
			// 
			// serviceProcessInstaller1
			// 
			this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.serviceProcessInstaller1.Password = null;
			this.serviceProcessInstaller1.Username = null;
			// 
			// Installer1
			// 
			this.Installer1.Description = "Hlavní služba pro PCManager server © Opencube, Autor: Jiří Falta";
			this.Installer1.DisplayName = "PCManager Služba";
			this.Installer1.ServiceName = "PCService";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.Installer1});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
		private System.ServiceProcess.ServiceInstaller Installer1;
		public System.ServiceProcess.ServiceController serviceController1;
	}
}