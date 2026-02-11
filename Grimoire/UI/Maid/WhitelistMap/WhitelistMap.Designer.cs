using MaidRemake.LockedMapHandle;
using Properties;

namespace MaidRemake.WhitelistMap
{
	partial class WhitelistMapForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhitelistMapForm));
			this.lblClear = new System.Windows.Forms.LinkLabel();
			this.lblCheck = new System.Windows.Forms.LinkLabel();
			this.lbCounter = new DarkUI.Controls.DarkLabel();
			this.tbGrabMapResult = new DarkUI.Controls.DarkTextBox();
			this.darkLabel1 = new DarkUI.Controls.DarkLabel();
			this.tbWhitelistMap = new DarkUI.Controls.DarkTextBox();
			this.btnAddToList = new DarkUI.Controls.DarkButton();
			this.btnGrabMap = new DarkUI.Controls.DarkButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblClear
			// 
			this.lblClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblClear.AutoSize = true;
			this.lblClear.LinkColor = System.Drawing.Color.DeepSkyBlue;
			this.lblClear.Location = new System.Drawing.Point(278, 292);
			this.lblClear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblClear.Name = "lblClear";
			this.lblClear.Size = new System.Drawing.Size(54, 20);
			this.lblClear.TabIndex = 19;
			this.lblClear.TabStop = true;
			this.lblClear.Text = "[Clear]";
			this.lblClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblClear_LinkClicked);
			// 
			// lblCheck
			// 
			this.lblCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCheck.AutoSize = true;
			this.lblCheck.LinkColor = System.Drawing.Color.DeepSkyBlue;
			this.lblCheck.Location = new System.Drawing.Point(216, 292);
			this.lblCheck.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCheck.Name = "lblCheck";
			this.lblCheck.Size = new System.Drawing.Size(62, 20);
			this.lblCheck.TabIndex = 18;
			this.lblCheck.TabStop = true;
			this.lblCheck.Text = "[Check]";
			this.lblCheck.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCheck_LinkClicked);
			// 
			// lbCounter
			// 
			this.lbCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbCounter.AutoSize = true;
			this.lbCounter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lbCounter.Location = new System.Drawing.Point(18, 292);
			this.lbCounter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbCounter.Name = "lbCounter";
			this.lbCounter.Size = new System.Drawing.Size(153, 20);
			this.lbCounter.TabIndex = 17;
			this.lbCounter.Text = "Number of map(s): 2";
			// 
			// tbGrabMapResult
			// 
			this.tbGrabMapResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbGrabMapResult.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
			this.tbGrabMapResult.Location = new System.Drawing.Point(18, 326);
			this.tbGrabMapResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tbGrabMapResult.Name = "tbGrabMapResult";
			this.tbGrabMapResult.Size = new System.Drawing.Size(314, 29);
			this.tbGrabMapResult.TabIndex = 16;
			this.tbGrabMapResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// darkLabel1
			// 
			this.darkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel1.Location = new System.Drawing.Point(18, 14);
			this.darkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.darkLabel1.Name = "darkLabel1";
			this.darkLabel1.Size = new System.Drawing.Size(315, 55);
			this.darkLabel1.TabIndex = 14;
			this.darkLabel1.Text = "Here\'s the whitelisted map(s) that don\'t need Maid to help you";
			this.darkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbWhitelistMap
			// 
			this.tbWhitelistMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbWhitelistMap.Font = new System.Drawing.Font("Leelawadee UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbWhitelistMap.Location = new System.Drawing.Point(18, 74);
			this.tbWhitelistMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tbWhitelistMap.Multiline = true;
			this.tbWhitelistMap.Name = "tbWhitelistMap";
			this.tbWhitelistMap.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbWhitelistMap.Size = new System.Drawing.Size(314, 213);
			this.tbWhitelistMap.TabIndex = 13;
			this.tbWhitelistMap.Text = "newbie;all\r\nbattleunderb;Enter;Spawn";
			this.tbWhitelistMap.TextChanged += new System.EventHandler(this.tbWhitelistMap_TextChanged);
			// 
			// btnAddToList
			// 
			this.btnAddToList.Checked = false;
			this.btnAddToList.Location = new System.Drawing.Point(0, 0);
			this.btnAddToList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnAddToList.Name = "btnAddToList";
			this.btnAddToList.Size = new System.Drawing.Size(159, 38);
			this.btnAddToList.TabIndex = 5;
			this.btnAddToList.Text = "Add To List";
			this.btnAddToList.Click += new System.EventHandler(this.btnAddToList_Click);
			// 
			// btnGrabMap
			// 
			this.btnGrabMap.Checked = false;
			this.btnGrabMap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnGrabMap.Location = new System.Drawing.Point(0, 0);
			this.btnGrabMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnGrabMap.Name = "btnGrabMap";
			this.btnGrabMap.Size = new System.Drawing.Size(150, 38);
			this.btnGrabMap.TabIndex = 4;
			this.btnGrabMap.Text = "Get Current Map";
			this.btnGrabMap.Click += new System.EventHandler(this.btnGrabMap_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(18, 369);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btnGrabMap);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btnAddToList);
			this.splitContainer1.Size = new System.Drawing.Size(315, 38);
			this.splitContainer1.SplitterDistance = 150;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 20;
			// 
			// WhitelistMapForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(351, 420);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.lblClear);
			this.Controls.Add(this.lblCheck);
			this.Controls.Add(this.lbCounter);
			this.Controls.Add(this.tbGrabMapResult);
			this.Controls.Add(this.darkLabel1);
			this.Controls.Add(this.tbWhitelistMap);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "WhitelistMapForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Whitelist Map";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel lblClear;
		private System.Windows.Forms.LinkLabel lblCheck;
		private DarkUI.Controls.DarkLabel lbCounter;
		private DarkUI.Controls.DarkTextBox tbGrabMapResult;
		private DarkUI.Controls.DarkLabel darkLabel1;
		public DarkUI.Controls.DarkTextBox tbWhitelistMap;
		private DarkUI.Controls.DarkButton btnAddToList;
		private DarkUI.Controls.DarkButton btnGrabMap;
		private System.Windows.Forms.SplitContainer splitContainer1;
	}
}