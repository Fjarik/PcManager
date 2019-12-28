namespace PCManagerGUI
{
	partial class OProgramu
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(421, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "AppManager";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(117, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(304, 153);
			this.label2.TabIndex = 1;
			this.label2.Text = "Hlavní grafická aplikace pro služu PCManager \r\n\r\n\r\n\r\n\r\n\r\n\r\nAutor: Jiří Falta, \r\n " +
    "      student Střední školy informatiky a ekonomie - DELTA\r\n\r\nVytvořeno: 10.4.20" +
    "17\r\n";
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label3.Location = new System.Drawing.Point(0, 182);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(421, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "Copyright © OsobniEu 2017";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// OProgramu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(421, 205);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OProgramu";
			this.ShowIcon = false;
			this.Text = "O Programu";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
	}
}