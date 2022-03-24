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
using Grimoire.Networking;
using Grimoire.Game;
using Grimoire.Game.Data;

namespace Grimoire.UI
{
	public class Travel : DarkForm
	{
		private IContainer components;
		private DarkButton btnDage;
		private DarkButton btnEscherion;
		private DarkButton btnNulgath;
		private DarkButton btnSwindle;
		private DarkButton btnTaro;
		private DarkButton btnTwins;
		private DarkButton btnTercess;
		private DarkGroupBox grpTravel;
		private DarkNumericUpDown numPriv;
		private DarkCheckBox chkPriv;
		private DarkButton btnPolish;
		private DarkButton btnLae;
		private DarkButton btnCarnage;
		private DarkButton AweTravel;
		private DarkGroupBox aweGroup;
		private RadioButton aweWizard;
		private RadioButton aweLucky;
		private DarkPanel panel1;
		private RadioButton aweThief;
		private RadioButton aweHybrid;
		private RadioButton aweHealer;
		private TableLayoutPanel tableLayoutPanel1;
		private RadioButton aweFigther;
		private DarkButton btnKlunk;
		private DarkButton btnDoomvaultB;
		private DarkGroupBox darkGroupBox1;
		private DarkButton btnTreeTitan;
		private DarkButton btnUltraDrakath;
		private DarkButton btnTowerOfDoom;
		private DarkButton btnNecroDungeon;
		private DarkGroupBox darkGroupBox2;
		public DarkLabel lblVersion;
		private DarkNumericUpDown numStoryQuestId;
		private DarkButton btnUnlockMap;
		private DarkButton btnIceSpiritIcetormarena;
		private DarkTextBox tbGeneratedPacket;
		public DarkLabel darkLabel1;
		private DarkButton btnCopyPacket;
		private DarkButton btnBinky;

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
			if (!Player.IsLoggedIn) return;
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
			this.btnDage = new DarkUI.Controls.DarkButton();
			this.btnEscherion = new DarkUI.Controls.DarkButton();
			this.btnBinky = new DarkUI.Controls.DarkButton();
			this.btnNulgath = new DarkUI.Controls.DarkButton();
			this.btnSwindle = new DarkUI.Controls.DarkButton();
			this.btnTaro = new DarkUI.Controls.DarkButton();
			this.btnTwins = new DarkUI.Controls.DarkButton();
			this.btnTercess = new DarkUI.Controls.DarkButton();
			this.grpTravel = new DarkUI.Controls.DarkGroupBox();
			this.btnIceSpiritIcetormarena = new DarkUI.Controls.DarkButton();
			this.btnKlunk = new DarkUI.Controls.DarkButton();
			this.numPriv = new DarkUI.Controls.DarkNumericUpDown();
			this.btnPolish = new DarkUI.Controls.DarkButton();
			this.btnLae = new DarkUI.Controls.DarkButton();
			this.btnCarnage = new DarkUI.Controls.DarkButton();
			this.chkPriv = new DarkUI.Controls.DarkCheckBox();
			this.AweTravel = new DarkUI.Controls.DarkButton();
			this.aweGroup = new DarkUI.Controls.DarkGroupBox();
			this.panel1 = new DarkUI.Controls.DarkPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.aweLucky = new DarkUI.Controls.DarkRadioButton();
			this.aweHybrid = new DarkUI.Controls.DarkRadioButton();
			this.aweHealer = new DarkUI.Controls.DarkRadioButton();
			this.aweFigther = new DarkUI.Controls.DarkRadioButton();
			this.aweThief = new DarkUI.Controls.DarkRadioButton();
			this.aweWizard = new DarkUI.Controls.DarkRadioButton();
			this.btnDoomvaultB = new DarkUI.Controls.DarkButton();
			this.darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
			this.btnNecroDungeon = new DarkUI.Controls.DarkButton();
			this.btnTowerOfDoom = new DarkUI.Controls.DarkButton();
			this.btnUltraDrakath = new DarkUI.Controls.DarkButton();
			this.btnTreeTitan = new DarkUI.Controls.DarkButton();
			this.darkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
			this.darkLabel1 = new DarkUI.Controls.DarkLabel();
			this.btnCopyPacket = new DarkUI.Controls.DarkButton();
			this.tbGeneratedPacket = new DarkUI.Controls.DarkTextBox();
			this.numStoryQuestId = new DarkUI.Controls.DarkNumericUpDown();
			this.btnUnlockMap = new DarkUI.Controls.DarkButton();
			this.lblVersion = new DarkUI.Controls.DarkLabel();
			this.grpTravel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPriv)).BeginInit();
			this.aweGroup.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.darkGroupBox1.SuspendLayout();
			this.darkGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numStoryQuestId)).BeginInit();
			this.SuspendLayout();
			// 
			// btnDage
			// 
			this.btnDage.Checked = false;
			this.btnDage.Location = new System.Drawing.Point(9, 395);
			this.btnDage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnDage.Name = "btnDage";
			this.btnDage.Size = new System.Drawing.Size(228, 35);
			this.btnDage.TabIndex = 0;
			this.btnDage.Text = "Dage";
			this.btnDage.Click += new System.EventHandler(this.btnDage_Click);
			// 
			// btnEscherion
			// 
			this.btnEscherion.Checked = false;
			this.btnEscherion.Location = new System.Drawing.Point(9, 432);
			this.btnEscherion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnEscherion.Name = "btnEscherion";
			this.btnEscherion.Size = new System.Drawing.Size(228, 35);
			this.btnEscherion.TabIndex = 1;
			this.btnEscherion.Text = "Escherion";
			this.btnEscherion.Click += new System.EventHandler(this.btnEscherion_Click);
			// 
			// btnBinky
			// 
			this.btnBinky.Checked = false;
			this.btnBinky.Location = new System.Drawing.Point(9, 358);
			this.btnBinky.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnBinky.Name = "btnBinky";
			this.btnBinky.Size = new System.Drawing.Size(228, 35);
			this.btnBinky.TabIndex = 2;
			this.btnBinky.Text = "Binky (doomvault)";
			this.btnBinky.Click += new System.EventHandler(this.btnBinky_Click);
			// 
			// btnNulgath
			// 
			this.btnNulgath.Checked = false;
			this.btnNulgath.Location = new System.Drawing.Point(9, 211);
			this.btnNulgath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnNulgath.Name = "btnNulgath";
			this.btnNulgath.Size = new System.Drawing.Size(228, 35);
			this.btnNulgath.TabIndex = 3;
			this.btnNulgath.Text = "Nulgath / Skew (tercess)";
			this.btnNulgath.Click += new System.EventHandler(this.btnNulgath_Click);
			// 
			// btnSwindle
			// 
			this.btnSwindle.Checked = false;
			this.btnSwindle.Location = new System.Drawing.Point(9, 174);
			this.btnSwindle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnSwindle.Name = "btnSwindle";
			this.btnSwindle.Size = new System.Drawing.Size(228, 35);
			this.btnSwindle.TabIndex = 4;
			this.btnSwindle.Text = "Swindle (tercess)";
			this.btnSwindle.Click += new System.EventHandler(this.btnSwindle_Click);
			// 
			// btnTaro
			// 
			this.btnTaro.Checked = false;
			this.btnTaro.Location = new System.Drawing.Point(9, 137);
			this.btnTaro.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnTaro.Name = "btnTaro";
			this.btnTaro.Size = new System.Drawing.Size(228, 35);
			this.btnTaro.TabIndex = 5;
			this.btnTaro.Text = "VHL/Taro/Zee (tercess)";
			this.btnTaro.Click += new System.EventHandler(this.btnTaro_Click);
			// 
			// btnTwins
			// 
			this.btnTwins.Checked = false;
			this.btnTwins.Location = new System.Drawing.Point(9, 100);
			this.btnTwins.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnTwins.Name = "btnTwins";
			this.btnTwins.Size = new System.Drawing.Size(228, 35);
			this.btnTwins.TabIndex = 6;
			this.btnTwins.Text = "Twins (tercess)";
			this.btnTwins.Click += new System.EventHandler(this.btnTwins_Click);
			// 
			// btnTercess
			// 
			this.btnTercess.Checked = false;
			this.btnTercess.Location = new System.Drawing.Point(9, 63);
			this.btnTercess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnTercess.Name = "btnTercess";
			this.btnTercess.Size = new System.Drawing.Size(228, 35);
			this.btnTercess.TabIndex = 7;
			this.btnTercess.Text = "Oblivion (tercess)";
			this.btnTercess.Click += new System.EventHandler(this.btnTercess_Click);
			// 
			// grpTravel
			// 
			this.grpTravel.Controls.Add(this.btnIceSpiritIcetormarena);
			this.grpTravel.Controls.Add(this.btnKlunk);
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
			this.grpTravel.Location = new System.Drawing.Point(20, 20);
			this.grpTravel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.grpTravel.Name = "grpTravel";
			this.grpTravel.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.grpTravel.Size = new System.Drawing.Size(246, 597);
			this.grpTravel.TabIndex = 8;
			this.grpTravel.TabStop = false;
			this.grpTravel.Text = "Fast travels";
			// 
			// btnIceSpiritIcetormarena
			// 
			this.btnIceSpiritIcetormarena.Checked = false;
			this.btnIceSpiritIcetormarena.Location = new System.Drawing.Point(9, 506);
			this.btnIceSpiritIcetormarena.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnIceSpiritIcetormarena.Name = "btnIceSpiritIcetormarena";
			this.btnIceSpiritIcetormarena.Size = new System.Drawing.Size(228, 35);
			this.btnIceSpiritIcetormarena.TabIndex = 9;
			this.btnIceSpiritIcetormarena.Text = "Ice Spirit (icestormarena)";
			this.btnIceSpiritIcetormarena.Click += new System.EventHandler(this.btnIceSpiritIcetormarena_Click);
			// 
			// btnKlunk
			// 
			this.btnKlunk.Checked = false;
			this.btnKlunk.Location = new System.Drawing.Point(9, 469);
			this.btnKlunk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnKlunk.Name = "btnKlunk";
			this.btnKlunk.Size = new System.Drawing.Size(228, 35);
			this.btnKlunk.TabIndex = 8;
			this.btnKlunk.Text = "Klunk (underworld)";
			this.btnKlunk.Click += new System.EventHandler(this.btnKlunk_Click);
			// 
			// numPriv
			// 
			this.numPriv.Enabled = false;
			this.numPriv.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numPriv.Location = new System.Drawing.Point(96, 28);
			this.numPriv.LoopValues = false;
			this.numPriv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.numPriv.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numPriv.Name = "numPriv";
			this.numPriv.Size = new System.Drawing.Size(141, 26);
			this.numPriv.TabIndex = 1;
			this.numPriv.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			// 
			// btnPolish
			// 
			this.btnPolish.Checked = false;
			this.btnPolish.Location = new System.Drawing.Point(9, 248);
			this.btnPolish.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnPolish.Name = "btnPolish";
			this.btnPolish.Size = new System.Drawing.Size(228, 35);
			this.btnPolish.TabIndex = 0;
			this.btnPolish.Text = "Polish (tercess)";
			this.btnPolish.Click += new System.EventHandler(this.btnPolish_Click);
			// 
			// btnLae
			// 
			this.btnLae.Checked = false;
			this.btnLae.Location = new System.Drawing.Point(9, 322);
			this.btnLae.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnLae.Name = "btnLae";
			this.btnLae.Size = new System.Drawing.Size(228, 35);
			this.btnLae.TabIndex = 0;
			this.btnLae.Text = "Lae (tercess)";
			this.btnLae.Click += new System.EventHandler(this.btnLae_Click);
			// 
			// btnCarnage
			// 
			this.btnCarnage.Checked = false;
			this.btnCarnage.Location = new System.Drawing.Point(9, 285);
			this.btnCarnage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCarnage.Name = "btnCarnage";
			this.btnCarnage.Size = new System.Drawing.Size(228, 35);
			this.btnCarnage.TabIndex = 0;
			this.btnCarnage.Text = "Carnage / Ninja (tercess)";
			this.btnCarnage.Click += new System.EventHandler(this.btnCarnage_Click);
			// 
			// chkPriv
			// 
			this.chkPriv.AutoSize = true;
			this.chkPriv.Location = new System.Drawing.Point(9, 29);
			this.chkPriv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.chkPriv.Name = "chkPriv";
			this.chkPriv.Size = new System.Drawing.Size(83, 24);
			this.chkPriv.TabIndex = 0;
			this.chkPriv.Text = "Private";
			this.chkPriv.CheckedChanged += new System.EventHandler(this.chkPriv_CheckedChanged);
			// 
			// AweTravel
			// 
			this.AweTravel.Checked = false;
			this.AweTravel.Location = new System.Drawing.Point(9, 135);
			this.AweTravel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.AweTravel.Name = "AweTravel";
			this.AweTravel.Size = new System.Drawing.Size(228, 35);
			this.AweTravel.TabIndex = 8;
			this.AweTravel.Text = "Awe Shop (museum)";
			this.AweTravel.Click += new System.EventHandler(this.AweTravel_Click);
			// 
			// aweGroup
			// 
			this.aweGroup.Controls.Add(this.panel1);
			this.aweGroup.Controls.Add(this.AweTravel);
			this.aweGroup.Location = new System.Drawing.Point(274, 438);
			this.aweGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.aweGroup.Name = "aweGroup";
			this.aweGroup.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.aweGroup.Size = new System.Drawing.Size(246, 178);
			this.aweGroup.TabIndex = 9;
			this.aweGroup.TabStop = false;
			this.aweGroup.Text = "Awe Enchantment Shop";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tableLayoutPanel1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(4, 24);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(238, 108);
			this.panel1.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.40506F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.59494F));
			this.tableLayoutPanel1.Controls.Add(this.aweLucky, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.aweHybrid, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.aweHealer, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.aweFigther, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.aweThief, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.aweWizard, 1, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(238, 108);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// aweLucky
			// 
			this.aweLucky.AutoSize = true;
			this.aweLucky.Checked = true;
			this.aweLucky.Location = new System.Drawing.Point(4, 5);
			this.aweLucky.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.aweLucky.Name = "aweLucky";
			this.aweLucky.Size = new System.Drawing.Size(75, 24);
			this.aweLucky.TabIndex = 9;
			this.aweLucky.TabStop = true;
			this.aweLucky.Text = "Lucky";
			this.aweLucky.UseVisualStyleBackColor = true;
			// 
			// aweHybrid
			// 
			this.aweHybrid.AutoSize = true;
			this.aweHybrid.Location = new System.Drawing.Point(4, 39);
			this.aweHybrid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.aweHybrid.Name = "aweHybrid";
			this.aweHybrid.Size = new System.Drawing.Size(79, 24);
			this.aweHybrid.TabIndex = 14;
			this.aweHybrid.Text = "Hybrid";
			this.aweHybrid.UseVisualStyleBackColor = true;
			// 
			// aweHealer
			// 
			this.aweHealer.AutoSize = true;
			this.aweHealer.Location = new System.Drawing.Point(104, 39);
			this.aweHealer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.aweHealer.Name = "aweHealer";
			this.aweHealer.Size = new System.Drawing.Size(81, 24);
			this.aweHealer.TabIndex = 15;
			this.aweHealer.Text = "Healer";
			this.aweHealer.UseVisualStyleBackColor = true;
			// 
			// aweFigther
			// 
			this.aweFigther.AutoSize = true;
			this.aweFigther.Location = new System.Drawing.Point(4, 73);
			this.aweFigther.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.aweFigther.Name = "aweFigther";
			this.aweFigther.Size = new System.Drawing.Size(84, 24);
			this.aweFigther.TabIndex = 11;
			this.aweFigther.Text = "Fighter";
			this.aweFigther.UseVisualStyleBackColor = true;
			// 
			// aweThief
			// 
			this.aweThief.AutoSize = true;
			this.aweThief.Location = new System.Drawing.Point(104, 5);
			this.aweThief.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.aweThief.Name = "aweThief";
			this.aweThief.Size = new System.Drawing.Size(69, 24);
			this.aweThief.TabIndex = 13;
			this.aweThief.Text = "Thief";
			this.aweThief.UseVisualStyleBackColor = true;
			// 
			// aweWizard
			// 
			this.aweWizard.AutoSize = true;
			this.aweWizard.Location = new System.Drawing.Point(104, 73);
			this.aweWizard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.aweWizard.Name = "aweWizard";
			this.aweWizard.Size = new System.Drawing.Size(83, 24);
			this.aweWizard.TabIndex = 10;
			this.aweWizard.Text = "Wizard";
			this.aweWizard.UseVisualStyleBackColor = true;
			// 
			// btnDoomvaultB
			// 
			this.btnDoomvaultB.Checked = false;
			this.btnDoomvaultB.Location = new System.Drawing.Point(9, 29);
			this.btnDoomvaultB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnDoomvaultB.Name = "btnDoomvaultB";
			this.btnDoomvaultB.Size = new System.Drawing.Size(228, 35);
			this.btnDoomvaultB.TabIndex = 9;
			this.btnDoomvaultB.Text = "DoomvaultB";
			this.btnDoomvaultB.Click += new System.EventHandler(this.btnDoomvaultB_Click);
			// 
			// darkGroupBox1
			// 
			this.darkGroupBox1.Controls.Add(this.btnNecroDungeon);
			this.darkGroupBox1.Controls.Add(this.btnTowerOfDoom);
			this.darkGroupBox1.Controls.Add(this.btnUltraDrakath);
			this.darkGroupBox1.Controls.Add(this.btnTreeTitan);
			this.darkGroupBox1.Controls.Add(this.btnDoomvaultB);
			this.darkGroupBox1.Location = new System.Drawing.Point(274, 20);
			this.darkGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.darkGroupBox1.Name = "darkGroupBox1";
			this.darkGroupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.darkGroupBox1.Size = new System.Drawing.Size(246, 226);
			this.darkGroupBox1.TabIndex = 10;
			this.darkGroupBox1.TabStop = false;
			this.darkGroupBox1.Text = "Fast travels 2";
			// 
			// btnNecroDungeon
			// 
			this.btnNecroDungeon.Checked = false;
			this.btnNecroDungeon.Location = new System.Drawing.Point(9, 177);
			this.btnNecroDungeon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnNecroDungeon.Name = "btnNecroDungeon";
			this.btnNecroDungeon.Size = new System.Drawing.Size(228, 35);
			this.btnNecroDungeon.TabIndex = 13;
			this.btnNecroDungeon.Text = "Necrodungeon";
			this.btnNecroDungeon.Click += new System.EventHandler(this.btnNecroDungeon_Click);
			// 
			// btnTowerOfDoom
			// 
			this.btnTowerOfDoom.Checked = false;
			this.btnTowerOfDoom.Location = new System.Drawing.Point(9, 140);
			this.btnTowerOfDoom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnTowerOfDoom.Name = "btnTowerOfDoom";
			this.btnTowerOfDoom.Size = new System.Drawing.Size(228, 35);
			this.btnTowerOfDoom.TabIndex = 12;
			this.btnTowerOfDoom.Text = "Tower of Doom 10";
			this.btnTowerOfDoom.Click += new System.EventHandler(this.btnTowerOfDoom_Click);
			// 
			// btnUltraDrakath
			// 
			this.btnUltraDrakath.Checked = false;
			this.btnUltraDrakath.Location = new System.Drawing.Point(9, 103);
			this.btnUltraDrakath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnUltraDrakath.Name = "btnUltraDrakath";
			this.btnUltraDrakath.Size = new System.Drawing.Size(228, 35);
			this.btnUltraDrakath.TabIndex = 11;
			this.btnUltraDrakath.Text = "Ultradrakath";
			this.btnUltraDrakath.Click += new System.EventHandler(this.btnUltraDrakath_Click);
			// 
			// btnTreeTitan
			// 
			this.btnTreeTitan.Checked = false;
			this.btnTreeTitan.Location = new System.Drawing.Point(9, 66);
			this.btnTreeTitan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnTreeTitan.Name = "btnTreeTitan";
			this.btnTreeTitan.Size = new System.Drawing.Size(228, 35);
			this.btnTreeTitan.TabIndex = 10;
			this.btnTreeTitan.Text = "Treetitanbattle";
			this.btnTreeTitan.Click += new System.EventHandler(this.btnTreeTitan_Click);
			// 
			// darkGroupBox2
			// 
			this.darkGroupBox2.Controls.Add(this.darkLabel1);
			this.darkGroupBox2.Controls.Add(this.btnCopyPacket);
			this.darkGroupBox2.Controls.Add(this.tbGeneratedPacket);
			this.darkGroupBox2.Controls.Add(this.numStoryQuestId);
			this.darkGroupBox2.Controls.Add(this.btnUnlockMap);
			this.darkGroupBox2.Controls.Add(this.lblVersion);
			this.darkGroupBox2.Location = new System.Drawing.Point(274, 255);
			this.darkGroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.darkGroupBox2.Name = "darkGroupBox2";
			this.darkGroupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.darkGroupBox2.Size = new System.Drawing.Size(246, 174);
			this.darkGroupBox2.TabIndex = 14;
			this.darkGroupBox2.TabStop = false;
			this.darkGroupBox2.Text = "Map Bypasser";
			// 
			// darkLabel1
			// 
			this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel1.Location = new System.Drawing.Point(9, 112);
			this.darkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.darkLabel1.Name = "darkLabel1";
			this.darkLabel1.Size = new System.Drawing.Size(228, 23);
			this.darkLabel1.TabIndex = 21;
			this.darkLabel1.Text = "Generated packet";
			// 
			// btnCopyPacket
			// 
			this.btnCopyPacket.Checked = false;
			this.btnCopyPacket.Location = new System.Drawing.Point(129, 137);
			this.btnCopyPacket.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
			this.btnCopyPacket.Name = "btnCopyPacket";
			this.btnCopyPacket.Size = new System.Drawing.Size(108, 31);
			this.btnCopyPacket.TabIndex = 20;
			this.btnCopyPacket.Text = "Copy";
			this.btnCopyPacket.Click += new System.EventHandler(this.btnCopyPacket_Click);
			// 
			// tbGeneratedPacket
			// 
			this.tbGeneratedPacket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbGeneratedPacket.Location = new System.Drawing.Point(14, 137);
			this.tbGeneratedPacket.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tbGeneratedPacket.Name = "tbGeneratedPacket";
			this.tbGeneratedPacket.ReadOnly = true;
			this.tbGeneratedPacket.Size = new System.Drawing.Size(112, 26);
			this.tbGeneratedPacket.TabIndex = 19;
			// 
			// numStoryQuestId
			// 
			this.numStoryQuestId.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numStoryQuestId.Location = new System.Drawing.Point(14, 77);
			this.numStoryQuestId.LoopValues = false;
			this.numStoryQuestId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.numStoryQuestId.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.numStoryQuestId.Name = "numStoryQuestId";
			this.numStoryQuestId.Size = new System.Drawing.Size(112, 26);
			this.numStoryQuestId.TabIndex = 9;
			// 
			// btnUnlockMap
			// 
			this.btnUnlockMap.Checked = false;
			this.btnUnlockMap.Location = new System.Drawing.Point(129, 77);
			this.btnUnlockMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnUnlockMap.Name = "btnUnlockMap";
			this.btnUnlockMap.Size = new System.Drawing.Size(108, 31);
			this.btnUnlockMap.TabIndex = 14;
			this.btnUnlockMap.Text = "Unlock";
			this.btnUnlockMap.Click += new System.EventHandler(this.btnUnlockMap_Click);
			// 
			// lblVersion
			// 
			this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblVersion.Location = new System.Drawing.Point(9, 28);
			this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(228, 49);
			this.lblVersion.TabIndex = 15;
			this.lblVersion.Text = "Story Quest ID that required to unlock the map";
			// 
			// Travel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(538, 632);
			this.Controls.Add(this.darkGroupBox2);
			this.Controls.Add(this.darkGroupBox1);
			this.Controls.Add(this.aweGroup);
			this.Controls.Add(this.grpTravel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Travel";
			this.Text = "Fast travels";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Travel_FormClosing);
			this.grpTravel.ResumeLayout(false);
			this.grpTravel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPriv)).EndInit();
			this.aweGroup.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.darkGroupBox1.ResumeLayout(false);
			this.darkGroupBox2.ResumeLayout(false);
			this.darkGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numStoryQuestId)).EndInit();
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

		private void btnKlunk_Click(object sender, EventArgs e)
		{
			this.ExecuteTravel(new List<IBotCommand>
			{
				this.CreateJoinCommand("underworld", "r11", "Left")
			});
		}

		private async void btnDoomvaultB_Click(object sender, EventArgs e)
		{
			btnDoomvaultB.Enabled = false;
			string toClient1 = "{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"updateQuest\",\"iValue\":20,\"iIndex\":126}}}";
			await Proxy.Instance.SendToClient(toClient1);
			await Task.Delay(500);

			string toServer = "%xt%zm%setAchievement%79%ia0%18%1%";
			await Proxy.Instance.SendToServer(toServer);
			await Task.Delay(1000);

			string toClient2 = "{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"updateQuest\",\"iValue\":27,\"iIndex\":127}}}";
			await Proxy.Instance.SendToClient(toClient2);
			await Task.Delay(500);

			string room = chkPriv.Checked ? $"-{numPriv.Value}" : "";
			Player.JoinMap("doomvaultb" + room);

			btnDoomvaultB.Enabled = true;
		}

		private async Task SkipMap(string clientPacket, string map, string cell = null, string pad = null)
		{
			await Proxy.Instance.SendToClient(clientPacket);
			await Task.Delay(500);
			string room = chkPriv.Checked ? $"-{numPriv.Value}" : "";
			Player.JoinMap(map + room, cell, pad);
		}

		private async void btnTreeTitan_Click(object sender, EventArgs e)
		{
			btnTreeTitan.Enabled = false;
			await SkipMap("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"updateQuest\",\"iValue\":30,\"iIndex\":196}}}", "treetitanbattle");
			btnTreeTitan.Enabled = true;
		}

		private async void btnUltraDrakath_Click(object sender, EventArgs e)
		{
			btnUltraDrakath.Enabled = false;
			await SkipMap("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"updateQuest\",\"iValue\":30,\"iIndex\":182}}}", "ultradrakath");
			btnUltraDrakath.Enabled = true;
		}

		private async void btnTowerOfDoom_Click(object sender, EventArgs e)
		{
			btnTowerOfDoom.Enabled = false;
			await SkipMap("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"updateQuest\",\"iValue\":30,\"iIndex\":159}}}", "towerofdoom10");
			btnTowerOfDoom.Enabled = true;
		}

		private async void btnNecroDungeon_Click(object sender, EventArgs e)
		{
			btnNecroDungeon.Enabled = false;
			await SkipMap("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"updateQuest\",\"iValue\":30,\"iIndex\":77}}}", "necrodungeon");
			btnNecroDungeon.Enabled = true;
		}

		private async void btnIceSpiritIcetormarena_Click(object sender, EventArgs e)
		{
			btnIceSpiritIcetormarena.Enabled = false;
			await SkipMap("client {\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"levelUp\",\"intExpToLevel\":\"26580000\",\"intLevel\":100}}}", "icestormarena", "r3c", "Left");
			btnIceSpiritIcetormarena.Enabled = true;
		}

		private async void btnUnlockMap_Click(object sender, EventArgs e)
		{
			btnUnlockMap.Enabled = false;
			tbGeneratedPacket.Text = "";
			int questId = (int)numStoryQuestId.Value;
			bool questOnTree = Player.Quests.QuestTree.Exists(q => q.Id == questId);
			int waitCount = 0;
			if (!questOnTree)
			{
				Player.Quests.Load(questId);
				while (!questOnTree && waitCount < 8)
				{
					await Task.Delay(500);
					waitCount++;
				}
			}
			try
			{
				Quest quest = Player.Quests.QuestTree.Find(q => q.Id == questId);
				string packet = "{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"updateQuest\",\"iValue\":" + quest.IValue + ",\"iIndex\":" + quest.ISlot + "}}}";
				await Proxy.Instance.SendToClient(packet);
				tbGeneratedPacket.Text = packet;
			} 
			catch (Exception ex)
			{

			}
			btnUnlockMap.Enabled = true;
		}

		private void btnCopyPacket_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(tbGeneratedPacket.Text);
		}
	}
}