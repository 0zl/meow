
namespace Grimoire.UI
{
	partial class DevTest
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevTest));
			this.btnCall = new DarkUI.Controls.DarkButton();
			this.darkLabel6 = new DarkUI.Controls.DarkLabel();
			this.tbGameFunction = new DarkUI.Controls.DarkTextBox();
			this.tbArgs = new DarkUI.Controls.DarkTextBox();
			this.darkLabel1 = new DarkUI.Controls.DarkLabel();
			this.chkArgs = new DarkUI.Controls.DarkCheckBox();
			this.tbLogs = new DarkUI.Controls.DarkTextBox();
			this.radGrimoire = new System.Windows.Forms.RadioButton();
			this.radGame = new System.Windows.Forms.RadioButton();
			this.radGetGame = new System.Windows.Forms.RadioButton();
			this.btnCopy = new DarkUI.Controls.DarkButton();
			this.btnFormat = new DarkUI.Controls.DarkButton();
			this.SuspendLayout();
			// 
			// btnCall
			// 
			this.btnCall.Checked = false;
			this.btnCall.Location = new System.Drawing.Point(7, 116);
			this.btnCall.Name = "btnCall";
			this.btnCall.Size = new System.Drawing.Size(235, 23);
			this.btnCall.TabIndex = 169;
			this.btnCall.Text = "Call";
			this.btnCall.Click += new System.EventHandler(this.btnCall_Click_1);
			// 
			// darkLabel6
			// 
			this.darkLabel6.AutoSize = true;
			this.darkLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel6.Location = new System.Drawing.Point(4, 29);
			this.darkLabel6.Name = "darkLabel6";
			this.darkLabel6.Size = new System.Drawing.Size(48, 13);
			this.darkLabel6.TabIndex = 170;
			this.darkLabel6.Text = "Function";
			// 
			// tbGameFunction
			// 
			this.tbGameFunction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbGameFunction.Location = new System.Drawing.Point(7, 47);
			this.tbGameFunction.Name = "tbGameFunction";
			this.tbGameFunction.Size = new System.Drawing.Size(235, 20);
			this.tbGameFunction.TabIndex = 171;
			this.tbGameFunction.Text = "world";
			// 
			// tbArgs
			// 
			this.tbArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbArgs.Location = new System.Drawing.Point(7, 86);
			this.tbArgs.Name = "tbArgs";
			this.tbArgs.Size = new System.Drawing.Size(214, 20);
			this.tbArgs.TabIndex = 173;
			this.tbArgs.Text = "arg1,arg2...";
			// 
			// darkLabel1
			// 
			this.darkLabel1.AutoSize = true;
			this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel1.Location = new System.Drawing.Point(4, 70);
			this.darkLabel1.Name = "darkLabel1";
			this.darkLabel1.Size = new System.Drawing.Size(28, 13);
			this.darkLabel1.TabIndex = 172;
			this.darkLabel1.Text = "Args";
			// 
			// chkArgs
			// 
			this.chkArgs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkArgs.AutoSize = true;
			this.chkArgs.Checked = true;
			this.chkArgs.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkArgs.Location = new System.Drawing.Point(227, 88);
			this.chkArgs.Name = "chkArgs";
			this.chkArgs.Size = new System.Drawing.Size(15, 14);
			this.chkArgs.TabIndex = 174;
			this.chkArgs.CheckedChanged += new System.EventHandler(this.chkArgs_CheckedChanged);
			// 
			// tbLogs
			// 
			this.tbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbLogs.Location = new System.Drawing.Point(7, 145);
			this.tbLogs.Multiline = true;
			this.tbLogs.Name = "tbLogs";
			this.tbLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbLogs.Size = new System.Drawing.Size(235, 178);
			this.tbLogs.TabIndex = 175;
			// 
			// radGrimoire
			// 
			this.radGrimoire.AutoSize = true;
			this.radGrimoire.Checked = true;
			this.radGrimoire.ForeColor = System.Drawing.Color.Gainsboro;
			this.radGrimoire.Location = new System.Drawing.Point(6, 6);
			this.radGrimoire.Name = "radGrimoire";
			this.radGrimoire.Size = new System.Drawing.Size(63, 17);
			this.radGrimoire.TabIndex = 177;
			this.radGrimoire.TabStop = true;
			this.radGrimoire.Text = "Grimoire";
			this.radGrimoire.UseVisualStyleBackColor = true;
			// 
			// radGame
			// 
			this.radGame.AutoSize = true;
			this.radGame.ForeColor = System.Drawing.Color.Gainsboro;
			this.radGame.Location = new System.Drawing.Point(75, 6);
			this.radGame.Name = "radGame";
			this.radGame.Size = new System.Drawing.Size(70, 17);
			this.radGame.TabIndex = 178;
			this.radGame.Text = "CallGame";
			this.radGame.UseVisualStyleBackColor = true;
			// 
			// radGetGame
			// 
			this.radGetGame.AutoSize = true;
			this.radGetGame.ForeColor = System.Drawing.Color.Gainsboro;
			this.radGetGame.Location = new System.Drawing.Point(148, 6);
			this.radGetGame.Name = "radGetGame";
			this.radGetGame.Size = new System.Drawing.Size(70, 17);
			this.radGetGame.TabIndex = 179;
			this.radGetGame.Text = "GetGame";
			this.radGetGame.UseVisualStyleBackColor = true;
			this.radGetGame.CheckedChanged += new System.EventHandler(this.radGetGame_CheckedChanged);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCopy.Checked = false;
			this.btnCopy.Location = new System.Drawing.Point(126, 331);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(116, 23);
			this.btnCopy.TabIndex = 180;
			this.btnCopy.Text = "Copy";
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnFormat
			// 
			this.btnFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnFormat.Checked = false;
			this.btnFormat.Location = new System.Drawing.Point(7, 331);
			this.btnFormat.Name = "btnFormat";
			this.btnFormat.Size = new System.Drawing.Size(116, 23);
			this.btnFormat.TabIndex = 181;
			this.btnFormat.Text = "Format JSON";
			this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
			// 
			// DevTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(254, 364);
			this.Controls.Add(this.btnFormat);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.radGetGame);
			this.Controls.Add(this.radGame);
			this.Controls.Add(this.radGrimoire);
			this.Controls.Add(this.tbLogs);
			this.Controls.Add(this.chkArgs);
			this.Controls.Add(this.tbArgs);
			this.Controls.Add(this.darkLabel1);
			this.Controls.Add(this.tbGameFunction);
			this.Controls.Add(this.darkLabel6);
			this.Controls.Add(this.btnCall);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DevTest";
			this.Text = "Dev Test";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DevTest_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DarkUI.Controls.DarkButton btnCall;
		private DarkUI.Controls.DarkLabel darkLabel6;
		private DarkUI.Controls.DarkTextBox tbGameFunction;
		private DarkUI.Controls.DarkTextBox tbArgs;
		private DarkUI.Controls.DarkLabel darkLabel1;
		public DarkUI.Controls.DarkCheckBox chkArgs;
		private DarkUI.Controls.DarkTextBox tbLogs;
		private System.Windows.Forms.RadioButton radGrimoire;
		private System.Windows.Forms.RadioButton radGame;
		private System.Windows.Forms.RadioButton radGetGame;
		private DarkUI.Controls.DarkButton btnCopy;
		private DarkUI.Controls.DarkButton btnFormat;
	}
}