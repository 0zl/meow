using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Networking;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Grimoire.UI
{
    public class PacketSpammer : DarkForm
    {
        private IContainer components;

        public DarkListBox lstPackets;

        public DarkTextBox txtPacket;

        public DarkButton btnAdd;

        public DarkButton btnClear;

        public DarkButton btnLoad;

        public DarkButton btnSave;

        public DarkButton btnStart;

        public DarkButton btnStop;

        public DarkNumericUpDown numDelay;

        private DarkButton btnSend;
        private TableLayoutPanel tableLayoutPanel1;
        public DarkButton btnRemove;

        public static PacketSpammer Instance
        {
            get;
        } = new PacketSpammer();

        public PacketSpammer()
        {
            InitializeComponent();
        }

        public void btnClear_Click(object sender, EventArgs e)
        {
            lstPackets.Items.Clear();
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtPacket.Text.Length > 0)
            {
                lstPackets.Items.Add(txtPacket.Text);
                txtPacket.Clear();
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (lstPackets.Items.Count > 0)
            {
                SaveConfig();
            }
        }

        public void btnLoad_Click(object sender, EventArgs e)
        {
            lstPackets.Items.Clear();
            LoadConfig();
        }

        public void btnStop_Click(object sender, EventArgs e)
        {
            Spammer.Instance.Stop();
            Spammer.Instance.IndexChanged -= IndexChanged;
            SetButtonsEnabled(enabled: true);
        }

        public void btnStart_Click(object sender, EventArgs e)
        {
            if (lstPackets.Items.Count > 0)
            {
                SetButtonsEnabled(enabled: false);
                List<string> packets = lstPackets.Items.Cast<string>().ToList();
                int delay = (int)numDelay.Value;
                Spammer.Instance.IndexChanged += IndexChanged;
                Spammer.Instance.Start(packets, delay);
            }
        }

        public async void btnSend_Click(object sender, EventArgs e)
        {
            if (txtPacket.TextLength > 0)
            {
                btnSend.Enabled = false;
                await Proxy.Instance.SendToServer(txtPacket.Text);
                btnSend.Enabled = true;
            }
        }

        private void PacketSpammer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void SaveConfig()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Save spammer file";
                openFileDialog.Filter = "XML files|*.xml";
                openFileDialog.CheckFileExists = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(openFileDialog.FileName))
                    {
                        xmlWriter.WriteStartElement("autoer");
                        foreach (string item in lstPackets.Items)
                        {
                            xmlWriter.WriteElementString("packet", item);
                        }
                        xmlWriter.WriteElementString("author", "Author");
                        xmlWriter.WriteElementString("spamspeed", numDelay.Value.ToString());
                        xmlWriter.WriteEndElement();
                    }
                }
            }
        }

        private void LoadConfig()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load spammer file";
                openFileDialog.Filter = "XML files|*.xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (XElement item in XElement.Load(openFileDialog.FileName).Nodes())
                    {
                        if (item.Name == "packet")
                        {
                            lstPackets.Items.Add(item.Value);
                        }
                        else if (item.Name == "spamspeed")
                        {
                            string text = item.Name.ToString();
                            numDelay.Value = text.All(char.IsDigit) ? int.Parse(text) : 2000;
                        }
                    }
                }
            }
        }

        private void SetButtonsEnabled(bool enabled)
        {
            btnStart.Enabled = enabled;
            btnAdd.Enabled = enabled;
            btnClear.Enabled = enabled;
            btnLoad.Enabled = enabled;
        }

        private void IndexChanged(int index)
        {
            lstPackets.Invoke((Action)delegate
            {
                lstPackets.SelectedIndex = index;
            });
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstPackets.SelectedIndex;
            if (selectedIndex > -1)
            {
                lstPackets.Items.RemoveAt(selectedIndex);
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
			this.components = new System.ComponentModel.Container();
			this.lstPackets = new DarkUI.Controls.DarkListBox(this.components);
			this.txtPacket = new DarkUI.Controls.DarkTextBox();
			this.btnAdd = new DarkUI.Controls.DarkButton();
			this.btnClear = new DarkUI.Controls.DarkButton();
			this.btnLoad = new DarkUI.Controls.DarkButton();
			this.btnSave = new DarkUI.Controls.DarkButton();
			this.btnStart = new DarkUI.Controls.DarkButton();
			this.btnStop = new DarkUI.Controls.DarkButton();
			this.numDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.btnSend = new DarkUI.Controls.DarkButton();
			this.btnRemove = new DarkUI.Controls.DarkButton();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstPackets
			// 
			this.lstPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstPackets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
			this.lstPackets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstPackets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lstPackets.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstPackets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lstPackets.FormattingEnabled = true;
			this.lstPackets.ItemHeight = 18;
			this.lstPackets.Location = new System.Drawing.Point(12, 12);
			this.lstPackets.Name = "lstPackets";
			this.lstPackets.Size = new System.Drawing.Size(325, 110);
			this.lstPackets.TabIndex = 0;
			// 
			// txtPacket
			// 
			this.txtPacket.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPacket.Location = new System.Drawing.Point(12, 125);
			this.txtPacket.Name = "txtPacket";
			this.txtPacket.Size = new System.Drawing.Size(325, 20);
			this.txtPacket.TabIndex = 27;
			// 
			// btnAdd
			// 
			this.btnAdd.Checked = false;
			this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnAdd.Location = new System.Drawing.Point(245, 3);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(76, 23);
			this.btnAdd.TabIndex = 28;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnClear
			// 
			this.btnClear.Checked = false;
			this.btnClear.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnClear.Location = new System.Drawing.Point(164, 3);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 29;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Checked = false;
			this.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLoad.Location = new System.Drawing.Point(164, 32);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(75, 23);
			this.btnLoad.TabIndex = 30;
			this.btnLoad.Text = "Load";
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// btnSave
			// 
			this.btnSave.Checked = false;
			this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnSave.Location = new System.Drawing.Point(245, 32);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(76, 23);
			this.btnSave.TabIndex = 31;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnStart
			// 
			this.btnStart.Checked = false;
			this.btnStart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnStart.Location = new System.Drawing.Point(245, 61);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(76, 25);
			this.btnStart.TabIndex = 32;
			this.btnStart.Text = "Start";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Checked = false;
			this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnStop.Location = new System.Drawing.Point(164, 61);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 25);
			this.btnStop.TabIndex = 33;
			this.btnStop.Text = "Stop";
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// numDelay
			// 
			this.numDelay.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numDelay.Location = new System.Drawing.Point(3, 3);
			this.numDelay.LoopValues = false;
			this.numDelay.Maximum = new decimal(new int[] {
            61000,
            0,
            0,
            0});
			this.numDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numDelay.Name = "numDelay";
			this.numDelay.Size = new System.Drawing.Size(81, 20);
			this.numDelay.TabIndex = 34;
			this.numDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// btnSend
			// 
			this.btnSend.Checked = false;
			this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnSend.Location = new System.Drawing.Point(3, 61);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(81, 25);
			this.btnSend.TabIndex = 35;
			this.btnSend.Text = "Send once";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Checked = false;
			this.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnRemove.Location = new System.Drawing.Point(90, 3);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(68, 23);
			this.btnRemove.TabIndex = 36;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Controls.Add(this.btnAdd, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnRemove, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnSave, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.numDelay, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnClear, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnLoad, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnStart, 3, 2);
			this.tableLayoutPanel1.Controls.Add(this.btnStop, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.btnSend, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 151);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(324, 89);
			this.tableLayoutPanel1.TabIndex = 37;
			// 
			// PacketSpammer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(348, 242);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.txtPacket);
			this.Controls.Add(this.lstPackets);
			this.Icon = global::Properties.Resources.GrimoireIcon;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PacketSpammer";
			this.Text = "Packet spammer";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PacketSpammer_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }
    }
}