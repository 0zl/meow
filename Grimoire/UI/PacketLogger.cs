using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.FlashTools;
using Grimoire.Networking;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class PacketLogger : DarkForm
    {
        private IContainer components;

        private DarkTextBox txtPackets;

        private DarkButton btnStart;

        private DarkButton btnStop;

        private DarkButton btnCopy;
        private TableLayoutPanel tableLayoutPanel1;
        private Label x;
        private DarkButton btnSpam;
        private DarkButton btnSendOnce;
        private DarkTextBox textToSend;
        private DarkNumericUpDown numSpamTimes;
        private DarkNumericUpDown numSpamDelay;
        private Label label1;
        private Label label2;
        private DarkButton btnClear;

        public static PacketLogger Instance
        {
            get;
        } = new PacketLogger();

        private PacketLogger()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPackets.Clear();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtPackets.Text.Length > 0)
            {
                Clipboard.SetText(txtPackets.Text);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //Proxy.Instance.ReceivedFromClient -= PacketCaptured;
            btnStart.Enabled = true;
            Flash.FlashCall -= FlashUtil_FlashCall;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Proxy.Instance.ReceivedFromClient += PacketCaptured;
            btnStart.Enabled = false;
            Flash.FlashCall += FlashUtil_FlashCall;
        }

        private void FlashUtil_FlashCall(AxShockwaveFlashObjects.AxShockwaveFlash flash, string function, params object[] args)
        {
            if (function == "packet")
            {
                string packet = args[0].ToString();
                if (packet.Contains(":"))
                {
                    packet = packet.Remove(0, packet.IndexOf(':') + 1);
                }
                packet = packet.Trim(new char[] { ' ', '[', ']' });

                txtPackets.Invoke((Action)delegate
                {
                    txtPackets.AppendText(packet + Environment.NewLine);
                });
            }
        }

        private void PacketCaptured(Networking.Message msg)
        {
            if (msg.RawContent != null && msg.RawContent.Contains("%xt%zm%"))
            {
                txtPackets.Invoke((Action)delegate
                {
                    txtPackets.AppendText(msg.RawContent + Environment.NewLine);
                });
            }
        }

        private void PacketLogger_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtPackets = new DarkUI.Controls.DarkTextBox();
            this.btnStart = new DarkUI.Controls.DarkButton();
            this.btnStop = new DarkUI.Controls.DarkButton();
            this.btnCopy = new DarkUI.Controls.DarkButton();
            this.btnClear = new DarkUI.Controls.DarkButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.x = new System.Windows.Forms.Label();
            this.btnSpam = new DarkUI.Controls.DarkButton();
            this.btnSendOnce = new DarkUI.Controls.DarkButton();
            this.textToSend = new DarkUI.Controls.DarkTextBox();
            this.numSpamTimes = new DarkUI.Controls.DarkNumericUpDown();
            this.numSpamDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpamTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpamDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPackets
            // 
            this.txtPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPackets.Location = new System.Drawing.Point(12, 12);
            this.txtPackets.MaxLength = 2147483647;
            this.txtPackets.Multiline = true;
            this.txtPackets.Name = "txtPackets";
            this.txtPackets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPackets.Size = new System.Drawing.Size(427, 244);
            this.txtPackets.TabIndex = 15;
            // 
            // btnStart
            // 
            this.btnStart.Checked = false;
            this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStart.Location = new System.Drawing.Point(321, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(103, 24);
            this.btnStart.TabIndex = 16;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Checked = false;
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Location = new System.Drawing.Point(215, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 24);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Checked = false;
            this.btnCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopy.Location = new System.Drawing.Point(109, 3);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(100, 24);
            this.btnCopy.TabIndex = 18;
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnClear
            // 
            this.btnClear.Checked = false;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClear.Location = new System.Drawing.Point(3, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 24);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStart, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStop, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCopy, 1, 0);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 265);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(427, 30);
            this.tableLayoutPanel1.TabIndex = 20;
            // 
            // x
            // 
            this.x.AutoSize = true;
            this.x.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.x.Location = new System.Drawing.Point(251, 373);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(12, 13);
            this.x.TabIndex = 10;
            this.x.Text = "x";
            // 
            // btnSpam
            // 
            this.btnSpam.Checked = false;
            this.btnSpam.Location = new System.Drawing.Point(278, 366);
            this.btnSpam.Name = "btnSpam";
            this.btnSpam.Size = new System.Drawing.Size(77, 24);
            this.btnSpam.TabIndex = 17;
            this.btnSpam.Text = "Spam";
            this.btnSpam.Click += new System.EventHandler(this.btnSpam_Click);
            // 
            // btnSendOnce
            // 
            this.btnSendOnce.Checked = false;
            this.btnSendOnce.Location = new System.Drawing.Point(362, 366);
            this.btnSendOnce.Name = "btnSendOnce";
            this.btnSendOnce.Size = new System.Drawing.Size(77, 24);
            this.btnSendOnce.TabIndex = 18;
            this.btnSendOnce.Text = "Send Once";
            this.btnSendOnce.Click += new System.EventHandler(this.btnSendOnce_Click);
            // 
            // textToSend
            // 
            this.textToSend.Location = new System.Drawing.Point(12, 335);
            this.textToSend.Name = "textToSend";
            this.textToSend.Size = new System.Drawing.Size(427, 20);
            this.textToSend.TabIndex = 31;
            // 
            // numSpamTimes
            // 
            this.numSpamTimes.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSpamTimes.Location = new System.Drawing.Point(197, 368);
            this.numSpamTimes.LoopValues = false;
            this.numSpamTimes.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numSpamTimes.Name = "numSpamTimes";
            this.numSpamTimes.Size = new System.Drawing.Size(52, 20);
            this.numSpamTimes.TabIndex = 37;
            // 
            // numSpamDelay
            // 
            this.numSpamDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSpamDelay.Location = new System.Drawing.Point(12, 367);
            this.numSpamDelay.LoopValues = false;
            this.numSpamDelay.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numSpamDelay.Name = "numSpamDelay";
            this.numSpamDelay.Size = new System.Drawing.Size(64, 20);
            this.numSpamDelay.TabIndex = 38;
            this.numSpamDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(78, 372);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "ms";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(12, 317);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Spammer";
            // 
            // PacketLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 408);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numSpamDelay);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.numSpamTimes);
            this.Controls.Add(this.txtPackets);
            this.Controls.Add(this.textToSend);
            this.Controls.Add(this.btnSendOnce);
            this.Controls.Add(this.x);
            this.Controls.Add(this.btnSpam);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.MinimizeBox = false;
            this.Name = "PacketLogger";
            this.Text = "Packet logger";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PacketLogger_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSpamTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpamDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private async void btnSendOnce_Click(object sender, EventArgs e)
        {
            if (textToSend.TextLength > 0)
            {
                btnSendOnce.Enabled = false;
                await Proxy.Instance.SendToServer(textToSend.Text);
                btnSendOnce.Enabled = true;
            }
        }

        private async void btnSpam_Click(object sender, EventArgs e)
        {
            if (textToSend.TextLength < 1) return;
            if (btnSpam.Text.Equals("Spam"))
            {
                btnSpam.Text = "Stop";
                btnSendOnce.Enabled = false;
                List<string> listPackets = new List<string>();
                int spamTimes = Decimal.ToInt32(numSpamTimes.Value);
                int spamDelay = Decimal.ToInt32(numSpamDelay.Value);
                if (textToSend.TextLength > 0)
                {
                    listPackets.Add(textToSend.Text);
                    if (spamTimes > 0)
                    {
                        for (int i = 1; i <= spamTimes; i++)
                        {
                            if (btnSpam.Text.Equals("Stop"))
                            {
                                await Proxy.Instance.SendToServer(textToSend.Text);
                                await Task.Delay(spamDelay);
                            }
                        }
                        stopSpammer(false);
                    }
                    else
                    {
                        Spammer.Instance.Start(listPackets, spamDelay);
                    }
                }
            }
            else
            {
                stopSpammer(true);
            }
        }

        private void stopSpammer(bool stopInstance)
        {
            btnSpam.Text = "Spam";
            btnSendOnce.Enabled = true;
            if (stopInstance) Spammer.Instance.Stop();
        }
    }
}