using Grimoire.Botting;
using Grimoire.Botting.Commands.Map;
using Grimoire.Botting.Commands.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using DarkUI.Forms;
using DarkUI.Controls;

namespace Grimoire.UI
{
    public class Travel : DarkForm
    {
        private IContainer components;

        private Button btnDage;

        private Button btnEscherion;

        private Button btnNulgath;

        private Button btnSwindle;

        private Button btnTaro;

        private Button btnTwins;

        private Button btnTercess;

        private GroupBox grpTravel;

        private NumericUpDown numPriv;

        private CheckBox chkPriv;
        private Button btnPolish;
        private Button btnLae;
        private Button btnCarnage;
        private Button AweTravel;
        private GroupBox aweGroup;
        private RadioButton aweWizard;
        private RadioButton aweLucky;
        private Panel panel1;
        private RadioButton aweThief;
        private RadioButton aweHybrid;
        private RadioButton aweHealer;
        private TableLayoutPanel tableLayoutPanel1;
        private RadioButton aweFigther;
        private Button btnBinky;

        public static Travel Instance
        {
            get;
        }

        private Travel()
        {
            InitializeComponent();
        }

        private void Travel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void chkPriv_CheckedChanged(object sender, EventArgs e)
        {
            numPriv.Enabled = chkPriv.Checked;
        }

        private void btnTercess_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim")
            });
        }

        private void btnTwins_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "Twins", "Left")
            });
        }

        private void btnTaro_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "Taro", "Left")
            });
        }

        private void btnSwindle_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "Swindle", "Left")
            });
        }

        private void btnNulgath_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "Boss2", "Right")
            });
        }

        private void btnEscherion_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("escherion", "Boss", "Left")
            });
        }

        private void btnDage_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("underworld", "s1", "Left")
            });
        }

        private CmdTravel CreateJoinCommand(string map, string cell = "Enter", string pad = "Spawn")
        {
            return new CmdTravel
            {
                Map = chkPriv.Checked ? (map + $"-{numPriv.Value}") : map,
                Cell = cell,
                Pad = pad
            };
        }

        private CmdLoadTravel CreateShopCommand(int shopid)
        {
            return new CmdLoadTravel
            {
                ShopId = shopid
            };
        }

        private async void ExecuteTravel(List<IBotCommand> cmds)
        {
            grpTravel.Enabled = false;
            aweGroup.Enabled  = false;
            foreach (IBotCommand cmd in cmds)
            {
                await cmd.Execute(null);
                await Task.Delay(1000);
            }
            if (InvokeRequired)
            {
                Invoke((Action)delegate
                {
                    grpTravel.Enabled = true;
                    aweGroup.Enabled  = true;
                });
            }
            else
            {
                grpTravel.Enabled = true;
                aweGroup.Enabled  = true;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Travel));
            this.btnDage = new DarkButton();
            this.btnEscherion = new DarkButton();
            this.btnBinky = new DarkButton();
            this.btnNulgath = new DarkButton();
            this.btnSwindle = new DarkButton();
            this.btnTaro = new DarkButton();
            this.btnTwins = new DarkButton();
            this.btnTercess = new DarkButton();
            this.grpTravel = new DarkGroupBox();
            this.numPriv = new DarkNumericUpDown();
            this.btnPolish = new DarkButton();
            this.btnLae = new DarkButton();
            this.btnCarnage = new DarkButton();
            this.chkPriv = new DarkCheckBox();
            this.AweTravel = new DarkButton();
            this.aweGroup = new DarkGroupBox();
            this.panel1 = new DarkPanel();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.aweLucky = new RadioButton();
            this.aweHybrid = new RadioButton();
            this.aweHealer = new RadioButton();
            this.aweFigther = new RadioButton();
            this.aweThief = new RadioButton();
            this.aweWizard = new RadioButton();
            this.grpTravel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriv)).BeginInit();
            this.aweGroup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDage
            // 
            this.btnDage.Location = new Point(6, 305);
            this.btnDage.Name = "btnDage";
            this.btnDage.Size = new Size(152, 23);
            this.btnDage.TabIndex = 0;
            this.btnDage.Text = "Dage";
            this.btnDage.TextAlign = ContentAlignment.MiddleRight;
            this.btnDage.UseVisualStyleBackColor = true;
            this.btnDage.Click += new EventHandler(this.btnDage_Click);
            // 
            // btnEscherion
            // 
            this.btnEscherion.Location = new Point(6, 334);
            this.btnEscherion.Name = "btnEscherion";
            this.btnEscherion.Size = new Size(152, 23);
            this.btnEscherion.TabIndex = 1;
            this.btnEscherion.Text = "Escherion";
            this.btnEscherion.TextAlign = ContentAlignment.MiddleRight;
            this.btnEscherion.UseVisualStyleBackColor = true;
            this.btnEscherion.Click += new EventHandler(this.btnEscherion_Click);
            // 
            // btnBinky
            // 
            this.btnBinky.Location = new Point(6, 276);
            this.btnBinky.Name = "btnBinky";
            this.btnBinky.Size = new Size(152, 23);
            this.btnBinky.TabIndex = 2;
            this.btnBinky.Text = "Binky (doomvault)";
            this.btnBinky.TextAlign = ContentAlignment.MiddleRight;
            this.btnBinky.UseVisualStyleBackColor = true;
            this.btnBinky.Click += new EventHandler(this.btnBinky_Click);
            // 
            // btnNulgath
            // 
            this.btnNulgath.Location = new Point(6, 160);
            this.btnNulgath.Name = "btnNulgath";
            this.btnNulgath.Size = new Size(152, 23);
            this.btnNulgath.TabIndex = 3;
            this.btnNulgath.Text = "Nulgath / Skew (tercess)";
            this.btnNulgath.TextAlign = ContentAlignment.MiddleRight;
            this.btnNulgath.UseVisualStyleBackColor = true;
            this.btnNulgath.Click += new EventHandler(this.btnNulgath_Click);
            // 
            // btnSwindle
            // 
            this.btnSwindle.Location = new Point(6, 131);
            this.btnSwindle.Name = "btnSwindle";
            this.btnSwindle.Size = new Size(152, 23);
            this.btnSwindle.TabIndex = 4;
            this.btnSwindle.Text = "Swindle (tercess)";
            this.btnSwindle.TextAlign = ContentAlignment.MiddleRight;
            this.btnSwindle.UseVisualStyleBackColor = true;
            this.btnSwindle.Click += new EventHandler(this.btnSwindle_Click);
            // 
            // btnTaro
            // 
            this.btnTaro.Location = new Point(6, 102);
            this.btnTaro.Name = "btnTaro";
            this.btnTaro.Size = new Size(152, 23);
            this.btnTaro.TabIndex = 5;
            this.btnTaro.Text = "VHL/Taro/Zee (tercess)";
            this.btnTaro.TextAlign = ContentAlignment.MiddleRight;
            this.btnTaro.UseVisualStyleBackColor = true;
            this.btnTaro.Click += new EventHandler(this.btnTaro_Click);
            // 
            // btnTwins
            // 
            this.btnTwins.Location = new Point(6, 73);
            this.btnTwins.Name = "btnTwins";
            this.btnTwins.Size = new Size(152, 23);
            this.btnTwins.TabIndex = 6;
            this.btnTwins.Text = "Twins (tercess)";
            this.btnTwins.TextAlign = ContentAlignment.MiddleRight;
            this.btnTwins.UseVisualStyleBackColor = true;
            this.btnTwins.Click += new EventHandler(this.btnTwins_Click);
            // 
            // btnTercess
            // 
            this.btnTercess.Location = new Point(6, 44);
            this.btnTercess.Name = "btnTercess";
            this.btnTercess.Size = new Size(152, 23);
            this.btnTercess.TabIndex = 7;
            this.btnTercess.Text = "Oblivion (tercess)";
            this.btnTercess.TextAlign = ContentAlignment.MiddleRight;
            this.btnTercess.UseVisualStyleBackColor = true;
            this.btnTercess.Click += new EventHandler(this.btnTercess_Click);
            // 
            // grpTravel
            // 
            this.grpTravel.Controls.Add(this.numPriv);
            this.grpTravel.Controls.Add(this.btnPolish);
            this.grpTravel.Controls.Add(this.btnLae);
            this.grpTravel.Controls.Add(this.btnCarnage);
            this.grpTravel.Controls.Add(this.btnDage);
            this.grpTravel.Controls.Add(this.btnEscherion);
            this.grpTravel.Controls.Add(this.btnBinky);
            this.grpTravel.Controls.Add(this.btnNulgath);
            this.grpTravel.Controls.Add(this.btnSwindle);
            this.grpTravel.Controls.Add(this.btnTaro);
            this.grpTravel.Controls.Add(this.btnTwins);
            this.grpTravel.Controls.Add(this.btnTercess);
            this.grpTravel.Controls.Add(this.chkPriv);
            this.grpTravel.Location = new Point(13, 13);
            this.grpTravel.Name = "grpTravel";
            this.grpTravel.Size = new Size(164, 366);
            this.grpTravel.TabIndex = 8;
            this.grpTravel.TabStop = false;
            this.grpTravel.Text = "Fast travels";
            // 
            // numPriv
            // 
            this.numPriv.Enabled = false;
            this.numPriv.Location = new Point(64, 18);
            this.numPriv.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numPriv.Name = "numPriv";
            this.numPriv.Size = new Size(94, 20);
            this.numPriv.TabIndex = 1;
            this.numPriv.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // btnPolish
            // 
            this.btnPolish.Location = new Point(6, 189);
            this.btnPolish.Name = "btnPolish";
            this.btnPolish.Size = new Size(152, 23);
            this.btnPolish.TabIndex = 0;
            this.btnPolish.Text = "Polish (tercess)";
            this.btnPolish.TextAlign = ContentAlignment.MiddleRight;
            this.btnPolish.UseVisualStyleBackColor = true;
            this.btnPolish.Click += new EventHandler(this.btnPolish_Click);
            // 
            // btnLae
            // 
            this.btnLae.Location = new Point(6, 247);
            this.btnLae.Name = "btnLae";
            this.btnLae.Size = new Size(152, 23);
            this.btnLae.TabIndex = 0;
            this.btnLae.Text = "Lae (tercess)";
            this.btnLae.TextAlign = ContentAlignment.MiddleRight;
            this.btnLae.UseVisualStyleBackColor = true;
            this.btnLae.Click += new EventHandler(this.btnLae_Click);
            // 
            // btnCarnage
            // 
            this.btnCarnage.Location = new Point(6, 218);
            this.btnCarnage.Name = "btnCarnage";
            this.btnCarnage.Size = new Size(152, 23);
            this.btnCarnage.TabIndex = 0;
            this.btnCarnage.Text = "Carnage / Ninja (tercess)";
            this.btnCarnage.TextAlign = ContentAlignment.MiddleRight;
            this.btnCarnage.UseVisualStyleBackColor = true;
            this.btnCarnage.Click += new EventHandler(this.btnCarnage_Click);
            // 
            // chkPriv
            // 
            this.chkPriv.AutoSize = true;
            this.chkPriv.Location = new Point(6, 19);
            this.chkPriv.Name = "chkPriv";
            this.chkPriv.Size = new Size(59, 17);
            this.chkPriv.TabIndex = 0;
            this.chkPriv.Text = "Private";
            this.chkPriv.UseVisualStyleBackColor = true;
            this.chkPriv.CheckedChanged += new EventHandler(this.chkPriv_CheckedChanged);
            // 
            // AweTravel
            // 
            this.AweTravel.Location = new Point(6, 93);
            this.AweTravel.Name = "AweTravel";
            this.AweTravel.Size = new Size(152, 23);
            this.AweTravel.TabIndex = 8;
            this.AweTravel.Text = "Awe Shop (museum)";
            this.AweTravel.TextAlign = ContentAlignment.MiddleRight;
            this.AweTravel.UseVisualStyleBackColor = true;
            this.AweTravel.Click += new EventHandler(this.AweTravel_Click);
            // 
            // aweGroup
            // 
            this.aweGroup.Controls.Add(this.panel1);
            this.aweGroup.Controls.Add(this.AweTravel);
            this.aweGroup.Location = new Point(183, 13);
            this.aweGroup.Name = "aweGroup";
            this.aweGroup.Size = new Size(164, 125);
            this.aweGroup.TabIndex = 9;
            this.aweGroup.TabStop = false;
            this.aweGroup.Text = "Awe Enchantment Shop";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(158, 70);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.40506F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57.59494F));
            this.tableLayoutPanel1.Controls.Add(this.aweLucky, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.aweHybrid, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.aweHealer, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.aweFigther, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.aweThief, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.aweWizard, 1, 3);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Location = new Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.Size = new Size(158, 70);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // aweLucky
            // 
            this.aweLucky.AutoSize = true;
            this.aweLucky.Checked = true;
            this.aweLucky.Location = new Point(3, 3);
            this.aweLucky.Name = "aweLucky";
            this.aweLucky.Size = new Size(54, 17);
            this.aweLucky.TabIndex = 9;
            this.aweLucky.TabStop = true;
            this.aweLucky.Text = "Lucky";
            this.aweLucky.UseVisualStyleBackColor = true;
            // 
            // aweHybrid
            // 
            this.aweHybrid.AutoSize = true;
            this.aweHybrid.Location = new Point(3, 26);
            this.aweHybrid.Name = "aweHybrid";
            this.aweHybrid.Size = new Size(55, 17);
            this.aweHybrid.TabIndex = 14;
            this.aweHybrid.Text = "Hybrid";
            this.aweHybrid.UseVisualStyleBackColor = true;
            // 
            // aweHealer
            // 
            this.aweHealer.AutoSize = true;
            this.aweHealer.Location = new Point(70, 26);
            this.aweHealer.Name = "aweHealer";
            this.aweHealer.Size = new Size(56, 17);
            this.aweHealer.TabIndex = 15;
            this.aweHealer.Text = "Healer";
            this.aweHealer.UseVisualStyleBackColor = true;
            // 
            // aweFigther
            // 
            this.aweFigther.AutoSize = true;
            this.aweFigther.Location = new Point(3, 49);
            this.aweFigther.Name = "aweFigther";
            this.aweFigther.Size = new Size(57, 17);
            this.aweFigther.TabIndex = 11;
            this.aweFigther.Text = "Fighter";
            this.aweFigther.UseVisualStyleBackColor = true;
            // 
            // aweThief
            // 
            this.aweThief.AutoSize = true;
            this.aweThief.Location = new Point(70, 3);
            this.aweThief.Name = "aweThief";
            this.aweThief.Size = new Size(49, 17);
            this.aweThief.TabIndex = 13;
            this.aweThief.Text = "Thief";
            this.aweThief.UseVisualStyleBackColor = true;
            // 
            // aweWizard
            // 
            this.aweWizard.AutoSize = true;
            this.aweWizard.Location = new Point(70, 49);
            this.aweWizard.Name = "aweWizard";
            this.aweWizard.Size = new Size(58, 17);
            this.aweWizard.TabIndex = 10;
            this.aweWizard.Text = "Wizard";
            this.aweWizard.UseVisualStyleBackColor = true;
            // 
            // Travel
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(359, 387);
            this.Controls.Add(this.aweGroup);
            this.Controls.Add(this.grpTravel);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Travel";
            this.Text = "Fast travels";
            this.TopMost = true;
            this.FormClosing += new FormClosingEventHandler(this.Travel_FormClosing);
            this.grpTravel.ResumeLayout(false);
            this.grpTravel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriv)).EndInit();
            this.aweGroup.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        static Travel()
        {
            Instance = new Travel();
        }

        private void btnBinky_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("doomvault", "r5", "Left")
            });
        }

        private void btnCarnage_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "m4", "Top")
            });
        }

        private void btnLae_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "m5", "Top")
            });
        }

        private void btnPolish_Click(object sender, EventArgs e)
        {
            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("citadel", "m22", "Left"),
                CreateJoinCommand("tercessuinotlim", "m12", "Top")
            });
        }

        private void AweTravel_Click(object sender, EventArgs e)
        {
            // Sorry Satan :*
            bool IsLucky   = aweLucky.Checked;
            bool IsWizard  = aweWizard.Checked;
            bool IsHybrid  = aweHybrid.Checked;
            bool IsThief   = aweThief.Checked;
            bool IsFighter = aweFigther.Checked;
            bool IsHealer  = aweHealer.Checked;

            ExecuteTravel(new List<IBotCommand>
            {
                CreateJoinCommand("museum"),
                
                // Staircase to hell.
                // Sorry Satan :3
                CreateShopCommand(
                    IsHybrid ? 633 :
                        IsFighter ? 635 :
                            IsWizard ? 636 :
                                IsThief ? 637 :
                                    IsHealer ? 638 :
                                        IsLucky ? 639 : 633
                )
            });
        }
    }
}