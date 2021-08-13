using DarkUI.Controls;
using DarkUI.Forms;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Grimoire.UI
{
    public class Loaders : DarkForm
    {
        public enum Type
        {
            ShopItems,
            QuestIDs,
            Quests,
            InventoryItems,
            TempItems,
            BankItems,
            Monsters
        }

        private IContainer components;

        private DarkTextBox txtLoaders;

        private DarkComboBox cbLoad;

        private DarkButton btnLoad;

        private DarkComboBox cbGrab;

        private DarkButton btnGrab;

        private DarkButton btnSave;

        private Panel panel1;

        private Panel panel2;

        private SplitContainer splitContainer1;
        private DarkButton btnForceAccept;
        private DarkNumericUpDown numTQuests;
        private DarkComboBox cbOrderBy;
        private TreeView treeGrabbed;

        public static Loaders Instance
        {
            get;
        } = new Loaders();

        public static Type TreeType
        {
            get;
            set;
        }

        private Loaders()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            int result;
            switch (cbLoad.SelectedIndex)
            {
                case 0:
                    if (int.TryParse(txtLoaders.Text, out result))
                    {
                        Shop.LoadHairShop(result);
                    }
                    break;

                case 1:
                    if (int.TryParse(txtLoaders.Text, out result))
                    {
                        Shop.Load(result);
                    }
                    break;

                case 2:
                    if (this.txtLoaders.Text.Contains(","))
                    {
                        this.LoadQuests(this.txtLoaders.Text);

                        return;
                    }
                    int id;
                    if (int.TryParse(this.txtLoaders.Text, out id))
                    {
                        int questId = Int32.Parse(txtLoaders.Text);
                        string quests = "";
                        int increament = (int)numTQuests.Value;
                        if (increament > 0)
                        {
                            for (int i = 0; i < increament; i++)
                            {
                                quests += questId + (i < increament-1 && increament != 1 ? "," : "");
                                questId++;
                            }
                            Console.WriteLine("quests: " + quests);
                            LoadQuests(quests);
                        }
                        else
                        {
                            Player.Quests.Load(id);
                        }
                        return;
                    }
                    break;

                case 3:
                    Shop.LoadArmorCustomizer();
                    break;
            }
        }

        private void LoadQuests(string str)
        {
            string[] source = str.Split(',');
            if (source.All((string s) => s.All(char.IsDigit)))
            {
                Player.Quests.Load(source.Select(int.Parse).ToList());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Save grabber data",
                CheckFileExists = false,
                Filter = "XML files|*.xml"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //using (Stream file = File.Open(openFileDialog.FileName, FileMode.Create))
                //{
                //    BinaryFormatter bf = new BinaryFormatter();
                //    bf.Serialize(file, treeGrabbed.Nodes.Cast<TreeNode>().ToList());
                //}
                XmlTextWriter textWriter = new XmlTextWriter(openFileDialog.FileName, System.Text.Encoding.ASCII)
                {
                    // set formatting style to indented
                    Formatting = Formatting.Indented
                };
                // writing the xml declaration tag
                textWriter.WriteStartDocument();
                // format it with new lines
                textWriter.WriteRaw("\r\n");
                // writing the main tag that encloses all node tags
                textWriter.WriteStartElement("TreeView");
                // save the nodes, recursive method
                SaveNodes(treeGrabbed.Nodes, textWriter);

                textWriter.WriteEndElement();

                textWriter.Close();
            }
        }

        private const string XmlNodeTag = "n";
        private const string XmlNodeTextAtt = "t";
        private const string XmlNodeTagAtt = "tg";
        private const string XmlNodeImageIndexAtt = "imageindex";

        private void SaveNodes(TreeNodeCollection nodesCollection, XmlTextWriter textWriter)
        {
            for (int i = 0; i < nodesCollection.Count; i++)
            {
                TreeNode node = nodesCollection[i];
                textWriter.WriteStartElement(XmlNodeTag);
                try
                {
                    string toadd = "";
                    for (int times = node.Text.Split(':')[0].Length; 9 > times; times++)
                        toadd += " ";
                    textWriter.WriteAttributeString(XmlNodeTextAtt, $"{node.Text.Split(':')[0]}:{toadd}{node.Text.Split(':')[1]}");
                }
                catch
                {
                    //string toadd = "";
                    //for (int times = node.Text.Split(':')[0].Length; 15 > times; times++)
                    //    toadd += "-";
                    textWriter.WriteAttributeString(XmlNodeTextAtt, $"{node.Text}");
                }
                //textWriter.WriteAttributeString(node.Text.Split(':')[0], node.Text.Split(':')[1]);
                //textWriter.WriteAttributeString(XmlNodeImageIndexAtt, node.ImageIndex.ToString());
                if (node.Tag != null)
                    textWriter.WriteAttributeString(XmlNodeTagAtt, node.Tag.ToString());
                // add other node properties to serialize here  
                if (node.Nodes.Count > 0)
                {
                    SaveNodes(node.Nodes, textWriter);
                }
                textWriter.WriteEndElement();
            }
        }

        private void btnGrab_Click(object sender, EventArgs e)
        {
            treeGrabbed.BeginUpdate();
            treeGrabbed.Nodes.Clear();

            Grabber.OrderBy orderBy = Grabber.OrderBy.Name;
            switch (cbOrderBy.SelectedIndex)
            {
                case 0:
                    orderBy = Grabber.OrderBy.Name;
                    break;
                case 1:
                    orderBy = Grabber.OrderBy.Id;
                    break;
            }

            switch (cbGrab.SelectedIndex)
            {
                case 0:
                    Grabber.GrabShopItems(treeGrabbed);
                    break;

                case 1:
                    Grabber.GrabQuestIds(treeGrabbed, orderBy);
                    break;

                case 2:
                    Grabber.GrabQuests(treeGrabbed, orderBy);
                    break;

                case 3:
                    Grabber.GrabInventoryItems(treeGrabbed);
                    break;

                case 4:
                    Grabber.GrabTempItems(treeGrabbed);
                    break;

                case 5:
                    Grabber.GrabBankItems(treeGrabbed);
                    break;

                case 6:
                    Grabber.GrabMonsters(treeGrabbed);
                    break;
            }
            treeGrabbed.EndUpdate();
        }

        private void Loaders_FormClosing(object sender, FormClosingEventArgs e)
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
            this.txtLoaders = new DarkUI.Controls.DarkTextBox();
            this.cbLoad = new DarkUI.Controls.DarkComboBox();
            this.btnLoad = new DarkUI.Controls.DarkButton();
            this.cbGrab = new DarkUI.Controls.DarkComboBox();
            this.btnGrab = new DarkUI.Controls.DarkButton();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.treeGrabbed = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnForceAccept = new DarkUI.Controls.DarkButton();
            this.numTQuests = new DarkUI.Controls.DarkNumericUpDown();
            this.cbOrderBy = new DarkUI.Controls.DarkComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTQuests)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLoaders
            // 
            this.txtLoaders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLoaders.Location = new System.Drawing.Point(12, 12);
            this.txtLoaders.Name = "txtLoaders";
            this.txtLoaders.Size = new System.Drawing.Size(156, 20);
            this.txtLoaders.TabIndex = 29;
            // 
            // cbLoad
            // 
            this.cbLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLoad.FormattingEnabled = true;
            this.cbLoad.Items.AddRange(new object[] {
            "Hair shop",
            "Shop",
            "Quest",
            "Armor customizer"});
            this.cbLoad.Location = new System.Drawing.Point(12, 38);
            this.cbLoad.Name = "cbLoad";
            this.cbLoad.Size = new System.Drawing.Size(156, 21);
            this.cbLoad.TabIndex = 30;
            this.cbLoad.SelectedIndexChanged += new System.EventHandler(this.cbLoad_SelectedIndexChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Checked = false;
            this.btnLoad.Location = new System.Drawing.Point(12, 65);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(156, 23);
            this.btnLoad.TabIndex = 31;
            this.btnLoad.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cbGrab
            // 
            this.cbGrab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbGrab.FormattingEnabled = true;
            this.cbGrab.Items.AddRange(new object[] {
            "Shop items",
            "Quest IDs",
            "Quest items, drop rates",
            "Inventory items",
            "Temp inventory items",
            "Bank items",
            "Monsters"});
            this.cbGrab.Location = new System.Drawing.Point(12, 332);
            this.cbGrab.Name = "cbGrab";
            this.cbGrab.Size = new System.Drawing.Size(174, 21);
            this.cbGrab.TabIndex = 33;
            this.cbGrab.SelectedIndexChanged += new System.EventHandler(this.cbGrab_SelectedIndexChanged);
            // 
            // btnGrab
            // 
            this.btnGrab.Checked = false;
            this.btnGrab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGrab.Location = new System.Drawing.Point(0, 0);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(125, 26);
            this.btnGrab.TabIndex = 34;
            this.btnGrab.Text = "Grab";
            this.btnGrab.Click += new System.EventHandler(this.btnGrab_Click);
            // 
            // btnSave
            // 
            this.btnSave.Checked = false;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 26);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // treeGrabbed
            // 
            this.treeGrabbed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeGrabbed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.treeGrabbed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeGrabbed.ForeColor = System.Drawing.Color.Gainsboro;
            this.treeGrabbed.LabelEdit = true;
            this.treeGrabbed.Location = new System.Drawing.Point(12, 94);
            this.treeGrabbed.Name = "treeGrabbed";
            this.treeGrabbed.Size = new System.Drawing.Size(247, 232);
            this.treeGrabbed.TabIndex = 38;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(121, 26);
            this.panel1.TabIndex = 39;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGrab);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(125, 26);
            this.panel2.TabIndex = 40;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(12, 360);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(247, 26);
            this.splitContainer1.SplitterDistance = 121;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 41;
            // 
            // btnForceAccept
            // 
            this.btnForceAccept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForceAccept.Checked = false;
            this.btnForceAccept.Enabled = false;
            this.btnForceAccept.Location = new System.Drawing.Point(192, 38);
            this.btnForceAccept.Name = "btnForceAccept";
            this.btnForceAccept.Size = new System.Drawing.Size(67, 23);
            this.btnForceAccept.TabIndex = 44;
            this.btnForceAccept.Text = "F Accept";
            this.btnForceAccept.Click += new System.EventHandler(this.btnForceAccept_Click_1);
            // 
            // numTQuests
            // 
            this.numTQuests.Enabled = false;
            this.numTQuests.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numTQuests.Location = new System.Drawing.Point(192, 12);
            this.numTQuests.LoopValues = false;
            this.numTQuests.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numTQuests.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTQuests.Name = "numTQuests";
            this.numTQuests.Size = new System.Drawing.Size(67, 20);
            this.numTQuests.TabIndex = 168;
            this.numTQuests.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbOrderBy
            // 
            this.cbOrderBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOrderBy.Enabled = false;
            this.cbOrderBy.FormattingEnabled = true;
            this.cbOrderBy.Items.AddRange(new object[] {
            "Name",
            "Id"});
            this.cbOrderBy.Location = new System.Drawing.Point(192, 332);
            this.cbOrderBy.Name = "cbOrderBy";
            this.cbOrderBy.Size = new System.Drawing.Size(67, 21);
            this.cbOrderBy.TabIndex = 169;
            this.cbOrderBy.SelectedIndex = 0;
            // 
            // Loaders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 391);
            this.Controls.Add(this.cbOrderBy);
            this.Controls.Add(this.numTQuests);
            this.Controls.Add(this.btnForceAccept);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.treeGrabbed);
            this.Controls.Add(this.cbGrab);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.cbLoad);
            this.Controls.Add(this.txtLoaders);
            this.Icon = global::Properties.Resources.GrimoireIcon;
            this.MinimizeBox = false;
            this.Name = "Loaders";
            this.Text = "Loaders and grabbers";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Loaders_FormClosing);
            this.Load += new System.EventHandler(this.Loaders_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTQuests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private readonly string font = Config.Load(Application.StartupPath + "\\config.cfg").Get("font");
        private readonly float? fontSize = float.Parse(Config.Load(Application.StartupPath + "\\config.cfg").Get("fontSize") ?? "8.25", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

        private void Loaders_Load(object sender, EventArgs e)
        {
            this.FormClosing += this.Loaders_FormClosing;
            if (font != null && fontSize != null)
            {
                this.Font = new Font(font, (float)fontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }

        private void cbLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            numTQuests.Enabled = cbLoad.SelectedIndex == 2;
            btnForceAccept.Enabled = cbLoad.SelectedIndex == 2;
        }

        private void cbGrab_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbOrderBy.Enabled = cbGrab.SelectedIndex == 1 || cbGrab.SelectedIndex == 2;
        }

        private void btnForceAccept_Click(object sender, EventArgs e)
        {
            try
            {
                Player.Quests.Accept(int.Parse(txtLoaders.Text));
            }
            catch { }
        }

        private async void btnForceAccept_Click_1(object sender, EventArgs e)
        {
            btnForceAccept.Enabled = false;
            if (txtLoaders.Text == null) return;
            int questId = Int32.Parse(txtLoaders.Text);
            List<int> listQuests = new List<int>();
            for (int i = 0; i < (int)numTQuests.Value; i++)
            {
                listQuests.Add(questId);
                questId++;
            }

            await acceptBatchAsync(listQuests);
            btnForceAccept.Enabled = true;
        }
        private async Task acceptBatchAsync(List<int> listQuest)
        {
            Player.Quests.Get(listQuest);
            await Task.Delay(1000);
            for (int i = 0; i < listQuest.Count; i++)
            {
                if (!Player.Quests.IsInProgress(listQuest[i]))
                {
                    Player.Quests.Accept(listQuest[i]);
                    await Task.Delay(1000);
                }
            }
        }
    }
}