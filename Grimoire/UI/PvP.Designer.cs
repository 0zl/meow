
using DarkUI.Controls;

namespace Grimoire.UI
{
	partial class PvP
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PvP));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.btnGetRange = new DarkUI.Controls.DarkButton();
			this.btnSetCD = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.numRangeValue = new DarkUI.Controls.DarkNumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.numRangeIndex = new DarkUI.Controls.DarkNumericUpDown();
			this.btnGetCD = new DarkUI.Controls.DarkButton();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numCDValue = new DarkUI.Controls.DarkNumericUpDown();
			this.llb1 = new System.Windows.Forms.Label();
			this.numCDIndex = new DarkUI.Controls.DarkNumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkEnableAutoTarget = new System.Windows.Forms.CheckBox();
			this.dropPlayers = new DarkUI.Controls.DarkComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRangeValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numRangeIndex)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numCDValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numCDIndex)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.btnGetRange);
			this.groupBox1.Controls.Add(this.btnSetCD);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.numRangeValue);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.numRangeIndex);
			this.groupBox1.Controls.Add(this.btnGetCD);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.numCDValue);
			this.groupBox1.Controls.Add(this.llb1);
			this.groupBox1.Controls.Add(this.numCDIndex);
			this.groupBox1.ForeColor = System.Drawing.SystemColors.Window;
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(277, 117);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Skill Hack";
			// 
			// button1
			// 
			this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.ForeColor = System.Drawing.SystemColors.Window;
			this.button1.Location = new System.Drawing.Point(213, 76);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(51, 23);
			this.button1.TabIndex = 19;
			this.button1.Text = "Set";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.btnSetRange_Click);
			// 
			// btnGetRange
			// 
			this.btnGetRange.Checked = false;
			this.btnGetRange.Location = new System.Drawing.Point(182, 76);
			this.btnGetRange.Name = "btnGetRange";
			this.btnGetRange.Size = new System.Drawing.Size(25, 23);
			this.btnGetRange.TabIndex = 13;
			this.btnGetRange.Visible = false;
			this.btnGetRange.Click += new System.EventHandler(this.btnGetRange_Click);
			// 
			// btnSetCD
			// 
			this.btnSetCD.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.btnSetCD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSetCD.ForeColor = System.Drawing.SystemColors.Window;
			this.btnSetCD.Location = new System.Drawing.Point(213, 31);
			this.btnSetCD.Name = "btnSetCD";
			this.btnSetCD.Size = new System.Drawing.Size(51, 23);
			this.btnSetCD.TabIndex = 18;
			this.btnSetCD.Text = "Set";
			this.btnSetCD.UseVisualStyleBackColor = true;
			this.btnSetCD.Click += new System.EventHandler(this.btnSetCD_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(78, 65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Range";
			// 
			// numRangeValue
			// 
			this.numRangeValue.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numRangeValue.IncrementAlternate = new decimal(new int[] {
            100,
            0,
            0,
            65536});
			this.numRangeValue.Location = new System.Drawing.Point(81, 79);
			this.numRangeValue.LoopValues = false;
			this.numRangeValue.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numRangeValue.Name = "numRangeValue";
			this.numRangeValue.Size = new System.Drawing.Size(74, 20);
			this.numRangeValue.TabIndex = 9;
			this.numRangeValue.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 65);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(33, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Index";
			// 
			// numRangeIndex
			// 
			this.numRangeIndex.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numRangeIndex.Location = new System.Drawing.Point(6, 79);
			this.numRangeIndex.LoopValues = false;
			this.numRangeIndex.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numRangeIndex.Name = "numRangeIndex";
			this.numRangeIndex.Size = new System.Drawing.Size(70, 20);
			this.numRangeIndex.TabIndex = 7;
			// 
			// btnGetCD
			// 
			this.btnGetCD.Checked = false;
			this.btnGetCD.Location = new System.Drawing.Point(182, 31);
			this.btnGetCD.Name = "btnGetCD";
			this.btnGetCD.Size = new System.Drawing.Size(25, 23);
			this.btnGetCD.TabIndex = 6;
			this.btnGetCD.Visible = false;
			this.btnGetCD.Click += new System.EventHandler(this.btnGetCD_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(156, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "ms";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(78, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Cooldown";
			// 
			// numCDValue
			// 
			this.numCDValue.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numCDValue.IncrementAlternate = new decimal(new int[] {
            1000,
            0,
            0,
            65536});
			this.numCDValue.Location = new System.Drawing.Point(81, 34);
			this.numCDValue.LoopValues = false;
			this.numCDValue.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
			this.numCDValue.Name = "numCDValue";
			this.numCDValue.Size = new System.Drawing.Size(74, 20);
			this.numCDValue.TabIndex = 2;
			this.numCDValue.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// llb1
			// 
			this.llb1.AutoSize = true;
			this.llb1.Location = new System.Drawing.Point(7, 20);
			this.llb1.Name = "llb1";
			this.llb1.Size = new System.Drawing.Size(33, 13);
			this.llb1.TabIndex = 1;
			this.llb1.Text = "Index";
			// 
			// numCDIndex
			// 
			this.numCDIndex.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numCDIndex.Location = new System.Drawing.Point(6, 34);
			this.numCDIndex.LoopValues = false;
			this.numCDIndex.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numCDIndex.Name = "numCDIndex";
			this.numCDIndex.Size = new System.Drawing.Size(70, 20);
			this.numCDIndex.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkEnableAutoTarget);
			this.groupBox2.Controls.Add(this.dropPlayers);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.ForeColor = System.Drawing.SystemColors.Window;
			this.groupBox2.Location = new System.Drawing.Point(12, 135);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(277, 87);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Auto Target";
			// 
			// chkEnableAutoTarget
			// 
			this.chkEnableAutoTarget.AutoSize = true;
			this.chkEnableAutoTarget.ForeColor = System.Drawing.SystemColors.Window;
			this.chkEnableAutoTarget.Location = new System.Drawing.Point(159, 41);
			this.chkEnableAutoTarget.Name = "chkEnableAutoTarget";
			this.chkEnableAutoTarget.Size = new System.Drawing.Size(50, 17);
			this.chkEnableAutoTarget.TabIndex = 19;
			this.chkEnableAutoTarget.Text = "Lock";
			this.chkEnableAutoTarget.UseVisualStyleBackColor = true;
			this.chkEnableAutoTarget.Click += new System.EventHandler(this.chkEnableAutoTarget_CheckedChanged);
			// 
			// dropPlayers
			// 
			this.dropPlayers.DisplayMember = "Name";
			this.dropPlayers.FormattingEnabled = true;
			this.dropPlayers.Location = new System.Drawing.Point(10, 39);
			this.dropPlayers.MaxDropDownItems = 16;
			this.dropPlayers.Name = "dropPlayers";
			this.dropPlayers.Size = new System.Drawing.Size(123, 21);
			this.dropPlayers.TabIndex = 17;
			this.dropPlayers.ValueMember = "Name";
			this.dropPlayers.Click += new System.EventHandler(this.dropPlayers_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 65);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(196, 13);
			this.label6.TabIndex = 16;
			this.label6.Text = "*sync with \'Auto Target\' in Hotkeys tools";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "Player";
			// 
			// PvP
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.ClientSize = new System.Drawing.Size(301, 232);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PvP";
			this.Text = "PvP";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PvP_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRangeValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numRangeIndex)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numCDValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numCDIndex)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label4;
		private DarkNumericUpDown numRangeValue;
		private System.Windows.Forms.Label label5;
		private DarkNumericUpDown numRangeIndex;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private DarkNumericUpDown numCDValue;
		private System.Windows.Forms.Label llb1;
		private DarkNumericUpDown numCDIndex;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		private DarkButton btnGetRange;
		private DarkButton btnGetCD;
		private System.Windows.Forms.Label label6;
		private DarkComboBox dropPlayers;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnSetCD;
		private System.Windows.Forms.CheckBox chkEnableAutoTarget;
	}
}