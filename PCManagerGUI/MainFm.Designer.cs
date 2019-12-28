namespace PCManagerGUI
{
	partial class MainFm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFm));
			this.CxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Separator1 = new System.Windows.Forms.ToolStripSeparator();
			this.TlStrip1 = new System.Windows.Forms.ToolStripMenuItem();
			this.TlStrip2 = new System.Windows.Forms.ToolStripMenuItem();
			this.TlStrip2a = new System.Windows.Forms.ToolStripMenuItem();
			this.TlStrip2b = new System.Windows.Forms.ToolStripMenuItem();
			this.TlStrip3 = new System.Windows.Forms.ToolStripMenuItem();
			this.Separator2 = new System.Windows.Forms.ToolStripSeparator();
			this.label1 = new System.Windows.Forms.Label();
			this.LblActive = new System.Windows.Forms.Label();
			this.CheckServiceTimer = new System.Windows.Forms.Timer(this.components);
			this.ReadTimer = new System.Windows.Forms.Timer(this.components);
			this.TxtLog = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ChBox_AutoLoad = new System.Windows.Forms.CheckBox();
			this.NIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.VypisTimer = new System.Windows.Forms.Timer(this.components);
			this.Btn_End = new System.Windows.Forms.Button();
			this.Btn_ZapVyp = new System.Windows.Forms.Button();
			this.CxMenu.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// CxMenu
			// 
			this.CxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.Separator1,
            this.TlStrip1,
            this.TlStrip2,
            this.TlStrip3,
            this.Separator2});
			this.CxMenu.Name = "Menu";
			this.CxMenu.ShowImageMargin = false;
			this.CxMenu.Size = new System.Drawing.Size(115, 104);
			// 
			// OpenToolStripMenuItem
			// 
			this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
			this.OpenToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.OpenToolStripMenuItem.Text = "Otevřít";
			this.OpenToolStripMenuItem.Click += new System.EventHandler(this.Show_Click);
			// 
			// Separator1
			// 
			this.Separator1.Name = "Separator1";
			this.Separator1.Size = new System.Drawing.Size(111, 6);
			// 
			// TlStrip1
			// 
			this.TlStrip1.Name = "TlStrip1";
			this.TlStrip1.Size = new System.Drawing.Size(114, 22);
			this.TlStrip1.Text = "O programu";
			this.TlStrip1.Click += new System.EventHandler(this.About_Click);
			// 
			// TlStrip2
			// 
			this.TlStrip2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TlStrip2a,
            this.TlStrip2b});
			this.TlStrip2.Name = "TlStrip2";
			this.TlStrip2.Size = new System.Drawing.Size(114, 22);
			this.TlStrip2.Text = "Ukončit";
			// 
			// TlStrip2a
			// 
			this.TlStrip2a.Name = "TlStrip2a";
			this.TlStrip2a.Size = new System.Drawing.Size(120, 22);
			this.TlStrip2a.Text = "Službu";
			this.TlStrip2a.Click += new System.EventHandler(this.EndService_click);
			// 
			// TlStrip2b
			// 
			this.TlStrip2b.Name = "TlStrip2b";
			this.TlStrip2b.Size = new System.Drawing.Size(120, 22);
			this.TlStrip2b.Text = "Program";
			this.TlStrip2b.Click += new System.EventHandler(this.EndApp_Click);
			// 
			// TlStrip3
			// 
			this.TlStrip3.Name = "TlStrip3";
			this.TlStrip3.Size = new System.Drawing.Size(114, 22);
			this.TlStrip3.Text = "Konec";
			this.TlStrip3.Click += new System.EventHandler(this.EndApp_Click);
			// 
			// Separator2
			// 
			this.Separator2.Name = "Separator2";
			this.Separator2.Size = new System.Drawing.Size(111, 6);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.Location = new System.Drawing.Point(15, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Služba:\r\n";
			// 
			// LblActive
			// 
			this.LblActive.AutoSize = true;
			this.LblActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.LblActive.ForeColor = System.Drawing.Color.Green;
			this.LblActive.Location = new System.Drawing.Point(73, 9);
			this.LblActive.Name = "LblActive";
			this.LblActive.Size = new System.Drawing.Size(47, 16);
			this.LblActive.TabIndex = 2;
			this.LblActive.Text = "Aktivní";
			// 
			// CheckServiceTimer
			// 
			this.CheckServiceTimer.Interval = 1000;
			this.CheckServiceTimer.Tick += new System.EventHandler(this.CheckServiceTimer_Tick);
			// 
			// ReadTimer
			// 
			this.ReadTimer.Enabled = true;
			this.ReadTimer.Interval = 1000;
			this.ReadTimer.Tick += new System.EventHandler(this.ReadTimer_Tick);
			// 
			// TxtLog
			// 
			this.TxtLog.BackColor = System.Drawing.SystemColors.Control;
			this.TxtLog.Location = new System.Drawing.Point(6, 19);
			this.TxtLog.Multiline = true;
			this.TxtLog.Name = "TxtLog";
			this.TxtLog.ReadOnly = true;
			this.TxtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TxtLog.Size = new System.Drawing.Size(327, 160);
			this.TxtLog.TabIndex = 3;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ChBox_AutoLoad);
			this.groupBox1.Controls.Add(this.TxtLog);
			this.groupBox1.Location = new System.Drawing.Point(12, 68);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(339, 185);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Log";
			// 
			// ChBox_AutoLoad
			// 
			this.ChBox_AutoLoad.AutoSize = true;
			this.ChBox_AutoLoad.Checked = true;
			this.ChBox_AutoLoad.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ChBox_AutoLoad.Location = new System.Drawing.Point(244, 0);
			this.ChBox_AutoLoad.Name = "ChBox_AutoLoad";
			this.ChBox_AutoLoad.Size = new System.Drawing.Size(74, 17);
			this.ChBox_AutoLoad.TabIndex = 4;
			this.ChBox_AutoLoad.Text = "AutoScroll";
			this.ChBox_AutoLoad.UseVisualStyleBackColor = true;
			this.ChBox_AutoLoad.CheckedChanged += new System.EventHandler(this.ChBox_AutoLoad_CheckedChanged);
			// 
			// NIcon
			// 
			this.NIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.NIcon.BalloonTipTitle = "PCManager";
			this.NIcon.ContextMenuStrip = this.CxMenu;
			this.NIcon.Icon = global::PCManagerGUI.Properties.Resources.Ikona;
			this.NIcon.Visible = true;
			this.NIcon.BalloonTipClicked += new System.EventHandler(this.NIcon_BalloonTipClicked);
			this.NIcon.BalloonTipClosed += new System.EventHandler(this.NIcon_BalloonTipClosed);
			this.NIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NIcon_MouseClick);
			// 
			// VypisTimer
			// 
			this.VypisTimer.Enabled = true;
			this.VypisTimer.Interval = 1000;
			this.VypisTimer.Tick += new System.EventHandler(this.VypisTimer_Tick);
			// 
			// Btn_End
			// 
			this.Btn_End.Location = new System.Drawing.Point(270, 33);
			this.Btn_End.Name = "Btn_End";
			this.Btn_End.Size = new System.Drawing.Size(75, 23);
			this.Btn_End.TabIndex = 6;
			this.Btn_End.Text = "Ukončit vše";
			this.Btn_End.UseVisualStyleBackColor = true;
			this.Btn_End.Click += new System.EventHandler(this.Btn_End_Click);
			// 
			// Btn_ZapVyp
			// 
			this.Btn_ZapVyp.Location = new System.Drawing.Point(45, 33);
			this.Btn_ZapVyp.Name = "Btn_ZapVyp";
			this.Btn_ZapVyp.Size = new System.Drawing.Size(75, 23);
			this.Btn_ZapVyp.TabIndex = 7;
			this.Btn_ZapVyp.Text = "Vypnout";
			this.Btn_ZapVyp.UseVisualStyleBackColor = true;
			this.Btn_ZapVyp.Click += new System.EventHandler(this.Btn_ZapVyp_Click);
			// 
			// MainFm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(363, 265);
			this.Controls.Add(this.Btn_ZapVyp);
			this.Controls.Add(this.Btn_End);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.LblActive);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainFm";
			this.Text = "PCManager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFm_FormClosed);
			this.Resize += new System.EventHandler(this.MainFm_Resize);
			this.CxMenu.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStripMenuItem TlStrip1;
		private System.Windows.Forms.ToolStripMenuItem TlStrip2;
		private System.Windows.Forms.ToolStripMenuItem TlStrip2a;
		private System.Windows.Forms.ToolStripMenuItem TlStrip2b;
		private System.Windows.Forms.ToolStripMenuItem TlStrip3;
		private System.Windows.Forms.ToolStripSeparator Separator1;
		private System.Windows.Forms.ToolStripSeparator Separator2;
		private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label LblActive;
		private System.Windows.Forms.Timer CheckServiceTimer;
		private System.Windows.Forms.Timer ReadTimer;
		public System.Windows.Forms.ContextMenuStrip CxMenu;
		private System.Windows.Forms.TextBox TxtLog;
		private System.Windows.Forms.GroupBox groupBox1;
		public System.Windows.Forms.NotifyIcon NIcon;
		private System.Windows.Forms.CheckBox ChBox_AutoLoad;
		private System.Windows.Forms.Timer VypisTimer;
		private System.Windows.Forms.Button Btn_End;
		private System.Windows.Forms.Button Btn_ZapVyp;
	}
}

