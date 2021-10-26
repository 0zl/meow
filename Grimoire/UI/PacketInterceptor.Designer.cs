
using DarkUI.Controls;

namespace Grimoire.UI
{
	partial class PacketInterceptor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PacketInterceptor));
			this.tabsInterceptor = new System.Windows.Forms.TabControl();
			this.tabLog = new System.Windows.Forms.TabPage();
			this.listPackets = new System.Windows.Forms.ListView();
			this.columnPackets = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lblBlocked = new System.Windows.Forms.Label();
			this.lblIn = new System.Windows.Forms.Label();
			this.lblOut = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panIn = new System.Windows.Forms.Panel();
			this.panOut = new System.Windows.Forms.Panel();
			this.lnkClearLog = new System.Windows.Forms.LinkLabel();
			this.chkLogPackets = new System.Windows.Forms.CheckBox();
			this.lblServer = new System.Windows.Forms.Label();
			this.btnConnect = new System.Windows.Forms.Button();
			this.cbServers = new System.Windows.Forms.ComboBox();
			this.tbPacketToSend = new System.Windows.Forms.TextBox();
			this.btnSendToClient = new System.Windows.Forms.Button();
			this.btnSendToServer = new System.Windows.Forms.Button();
			this.tabsInterceptor.SuspendLayout();
			this.tabLog.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabsInterceptor
			// 
			this.tabsInterceptor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabsInterceptor.Controls.Add(this.tabLog);
			this.tabsInterceptor.Location = new System.Drawing.Point(12, 39);
			this.tabsInterceptor.Name = "tabsInterceptor";
			this.tabsInterceptor.SelectedIndex = 0;
			this.tabsInterceptor.Size = new System.Drawing.Size(776, 429);
			this.tabsInterceptor.TabIndex = 7;
			// 
			// tabLog
			// 
			this.tabLog.Controls.Add(this.listPackets);
			this.tabLog.Controls.Add(this.lblBlocked);
			this.tabLog.Controls.Add(this.lblIn);
			this.tabLog.Controls.Add(this.lblOut);
			this.tabLog.Controls.Add(this.panel1);
			this.tabLog.Controls.Add(this.panIn);
			this.tabLog.Controls.Add(this.panOut);
			this.tabLog.Controls.Add(this.lnkClearLog);
			this.tabLog.Controls.Add(this.chkLogPackets);
			this.tabLog.Location = new System.Drawing.Point(4, 22);
			this.tabLog.Name = "tabLog";
			this.tabLog.Size = new System.Drawing.Size(768, 403);
			this.tabLog.TabIndex = 1;
			this.tabLog.Text = "Log";
			this.tabLog.UseVisualStyleBackColor = true;
			// 
			// listPackets
			// 
			this.listPackets.Alignment = System.Windows.Forms.ListViewAlignment.Left;
			this.listPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listPackets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPackets});
			this.listPackets.HideSelection = false;
			this.listPackets.Location = new System.Drawing.Point(3, 27);
			this.listPackets.Name = "listPackets";
			this.listPackets.Size = new System.Drawing.Size(765, 376);
			this.listPackets.TabIndex = 7;
			this.listPackets.UseCompatibleStateImageBehavior = false;
			// 
			// columnPackets
			// 
			this.columnPackets.Text = "Packets";
			// 
			// lblBlocked
			// 
			this.lblBlocked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblBlocked.AutoSize = true;
			this.lblBlocked.Location = new System.Drawing.Point(479, 5);
			this.lblBlocked.Name = "lblBlocked";
			this.lblBlocked.Size = new System.Drawing.Size(46, 13);
			this.lblBlocked.TabIndex = 6;
			this.lblBlocked.Text = "Blocked";
			// 
			// lblIn
			// 
			this.lblIn.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblIn.AutoSize = true;
			this.lblIn.Location = new System.Drawing.Point(373, 5);
			this.lblIn.Name = "lblIn";
			this.lblIn.Size = new System.Drawing.Size(46, 13);
			this.lblIn.TabIndex = 6;
			this.lblIn.Text = "Inbound";
			// 
			// lblOut
			// 
			this.lblOut.AutoSize = true;
			this.lblOut.Location = new System.Drawing.Point(256, 5);
			this.lblOut.Name = "lblOut";
			this.lblOut.Size = new System.Drawing.Size(54, 13);
			this.lblOut.TabIndex = 5;
			this.lblOut.Text = "Outbound";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.Red;
			this.panel1.ForeColor = System.Drawing.Color.White;
			this.panel1.Location = new System.Drawing.Point(457, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(16, 16);
			this.panel1.TabIndex = 4;
			// 
			// panIn
			// 
			this.panIn.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.panIn.BackColor = System.Drawing.Color.CornflowerBlue;
			this.panIn.ForeColor = System.Drawing.Color.White;
			this.panIn.Location = new System.Drawing.Point(351, 4);
			this.panIn.Name = "panIn";
			this.panIn.Size = new System.Drawing.Size(16, 16);
			this.panIn.TabIndex = 3;
			// 
			// panOut
			// 
			this.panOut.BackColor = System.Drawing.Color.Yellow;
			this.panOut.ForeColor = System.Drawing.Color.White;
			this.panOut.Location = new System.Drawing.Point(234, 4);
			this.panOut.Name = "panOut";
			this.panOut.Size = new System.Drawing.Size(16, 16);
			this.panOut.TabIndex = 2;
			// 
			// lnkClearLog
			// 
			this.lnkClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkClearLog.AutoSize = true;
			this.lnkClearLog.Location = new System.Drawing.Point(734, 4);
			this.lnkClearLog.Name = "lnkClearLog";
			this.lnkClearLog.Size = new System.Drawing.Size(31, 13);
			this.lnkClearLog.TabIndex = 1;
			this.lnkClearLog.TabStop = true;
			this.lnkClearLog.Text = "Clear";
			this.lnkClearLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkClearLog_LinkClicked);
			// 
			// chkLogPackets
			// 
			this.chkLogPackets.AutoSize = true;
			this.chkLogPackets.Location = new System.Drawing.Point(3, 4);
			this.chkLogPackets.Name = "chkLogPackets";
			this.chkLogPackets.Size = new System.Drawing.Size(86, 17);
			this.chkLogPackets.TabIndex = 0;
			this.chkLogPackets.Text = "Log Packets";
			this.chkLogPackets.UseVisualStyleBackColor = true;
			this.chkLogPackets.CheckedChanged += new System.EventHandler(this.chkLogPackets_CheckedChanged);
			// 
			// lblServer
			// 
			this.lblServer.AutoSize = true;
			this.lblServer.BackColor = System.Drawing.SystemColors.Control;
			this.lblServer.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblServer.Location = new System.Drawing.Point(13, 15);
			this.lblServer.Name = "lblServer";
			this.lblServer.Size = new System.Drawing.Size(41, 13);
			this.lblServer.TabIndex = 6;
			this.lblServer.Text = "Server:";
			// 
			// btnConnect
			// 
			this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConnect.Location = new System.Drawing.Point(650, 11);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(138, 23);
			this.btnConnect.TabIndex = 5;
			this.btnConnect.Text = "Connect";
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// cbServers
			// 
			this.cbServers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbServers.FormattingEnabled = true;
			this.cbServers.Location = new System.Drawing.Point(60, 12);
			this.cbServers.Name = "cbServers";
			this.cbServers.Size = new System.Drawing.Size(584, 21);
			this.cbServers.TabIndex = 4;
			// 
			// tbPacketToSend
			// 
			this.tbPacketToSend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbPacketToSend.Location = new System.Drawing.Point(12, 474);
			this.tbPacketToSend.Multiline = true;
			this.tbPacketToSend.Name = "tbPacketToSend";
			this.tbPacketToSend.Size = new System.Drawing.Size(625, 52);
			this.tbPacketToSend.TabIndex = 8;
			// 
			// btnSendToClient
			// 
			this.btnSendToClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSendToClient.Location = new System.Drawing.Point(650, 474);
			this.btnSendToClient.Name = "btnSendToClient";
			this.btnSendToClient.Size = new System.Drawing.Size(138, 23);
			this.btnSendToClient.TabIndex = 9;
			this.btnSendToClient.Text = "Send to Client";
			this.btnSendToClient.Click += new System.EventHandler(this.btnSendToClient_Click);
			// 
			// btnSendToServer
			// 
			this.btnSendToServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSendToServer.Location = new System.Drawing.Point(650, 503);
			this.btnSendToServer.Name = "btnSendToServer";
			this.btnSendToServer.Size = new System.Drawing.Size(138, 23);
			this.btnSendToServer.TabIndex = 10;
			this.btnSendToServer.Text = "Send to Server";
			this.btnSendToServer.Click += new System.EventHandler(this.btnSendToServer_Click);
			// 
			// PacketInterceptor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(800, 538);
			this.Controls.Add(this.btnSendToServer);
			this.Controls.Add(this.btnSendToClient);
			this.Controls.Add(this.tbPacketToSend);
			this.Controls.Add(this.tabsInterceptor);
			this.Controls.Add(this.lblServer);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.cbServers);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PacketInterceptor";
			this.Text = "Packet Interceptor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PacketInterceptor_FormClosing);
			this.tabsInterceptor.ResumeLayout(false);
			this.tabLog.ResumeLayout(false);
			this.tabLog.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabsInterceptor;
		private System.Windows.Forms.Label lblServer;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.ComboBox cbServers;
		private System.Windows.Forms.TextBox tbPacketToSend;
		private System.Windows.Forms.Button btnSendToClient;
		private System.Windows.Forms.Button btnSendToServer;
		private System.Windows.Forms.TabPage tabLog;
		private System.Windows.Forms.ListView listPackets;
		private System.Windows.Forms.ColumnHeader columnPackets;
		private System.Windows.Forms.Label lblBlocked;
		private System.Windows.Forms.Label lblIn;
		private System.Windows.Forms.Label lblOut;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panIn;
		private System.Windows.Forms.Panel panOut;
		private System.Windows.Forms.LinkLabel lnkClearLog;
		private System.Windows.Forms.CheckBox chkLogPackets;
	}
}