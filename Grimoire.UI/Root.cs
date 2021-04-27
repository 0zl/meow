using AxShockwaveFlashObjects;
using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.Properties;
using Grimoire.Tools;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Botting;
using Grimoire.Game.Data;
using System.Diagnostics;
using EoL;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;
using DarkUI.Controls;
using DarkUI.Forms;

namespace Grimoire.UI
{
    public class Root : DarkForm
    {
        private IContainer components;

        private NotifyIcon nTray;

        private AxShockwaveFlash flashPlayer;

        private DarkComboBox cbPads;

        private DarkComboBox cbCells;

        private DarkButton btnJump;
        private ToolStripMenuItem botToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem fastTravelsToolStripMenuItem;
        private ToolStripMenuItem loadersgrabbersToolStripMenuItem;
        private ToolStripMenuItem hotkeysToolStripMenuItem;
        private ToolStripMenuItem pluginManagerToolStripMenuItem;
        private ToolStripMenuItem cosmeticsToolStripMenuItem;
        private ToolStripMenuItem bankToolStripMenuItem;
        private ToolStripMenuItem setsToolStripMenuItem;
        private ToolStripMenuItem eyeDropperToolStripMenuItem;
        private ToolStripMenuItem logsToolStripMenuItem1;
        private ToolStripMenuItem notepadToolStripMenuItem1;
        private ToolStripMenuItem packetsToolStripMenuItem;
        private ToolStripMenuItem snifferToolStripMenuItem;
        private ToolStripMenuItem spammerToolStripMenuItem;
        private ToolStripMenuItem tampererToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem discordToolStripMenuItem;
        private ToolStripMenuItem botRequestToolStripMenuItem;
        private ToolStripMenuItem grimoireSuggestionsToolStripMenuItem;
        public ToolStripMenuItem optionsToolStripMenuItem;
        public ToolStripMenuItem infRangeToolStripMenuItem;
        public ToolStripMenuItem provokeToolStripMenuItem1;
        public ToolStripMenuItem enemyMagnetToolStripMenuItem;
        public ToolStripMenuItem lagKillerToolStripMenuItem;
        public ToolStripMenuItem hidePlayersToolStripMenuItem;
        public ToolStripMenuItem skipCutscenesToolStripMenuItem;
        public ToolStripMenuItem disableAnimationsToolStripMenuItem;
        private ToolStripMenuItem bankToolStripMenuItem1;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripMenuItem grimliteToolStripMenuItem;
        public ToolStripMenuItem pluginsStrip;
        private Button btnMax;
        private Button btnExit;
        private Button btnMin;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem setFPSToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox1;
        private DarkMenuStrip darkMenuStrip1;
        private ToolStripMenuItem FPSToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox2;
        private ToolStripMenuItem getBotsToolStripMenuItem;
        private ToolStripMenuItem reloadToolStripMenuItem;
        
        public static Root Instance
        {
            get;
            private set;
        }

        public AxShockwaveFlash Client => flashPlayer;

        public Root()
        {
            if (!System.Diagnostics.Debugger.IsAttached)
                Process.Start(@"updater.exe");
            Bypass.Hook();
            InitializeComponent();
            Instance = this;
        }

        private void Root_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(Proxy.Instance.Start, TaskCreationOptions.LongRunning);
            Flash.flash = flashPlayer;
            flashPlayer.FlashCall += Flash.ProcessFlashCall;
            //this.OnLoadProgress(100);
            //Flash.SwfLoadProgress += OnLoadProgress;
            Hotkeys.Instance.LoadHotkeys();
            InitFlashMovie();
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
        }

        private void OnLoadProgress(int progress)
        {
            /* Deprecated
            if (progress < prgLoader.Maximum)
            {
                prgLoader.Value = progress;
                return;
            }
            Flash.SwfLoadProgress -= OnLoadProgress;
            flashPlayer.Visible = true;
            prgLoader.Visible = false;
            */
        }

        public BotManager botManager = BotManager.Instance;

        private void botToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(botManager);
        }

        private void fastTravelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Travel.Instance);
        }

        private void loadersgrabbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Loaders.Instance);
        }

        private void hotkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Hotkeys.Instance);
        }

        private void pluginManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PluginManager.Instance);
        }

        private void snifferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PacketLogger.Instance);
        }

        private void spammerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PacketSpammer.Instance);
        }

        private void tampererToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(PacketTamperer.Instance);
        }

        public void ShowForm(Form form)
        {
            if (form.WindowState == FormWindowState.Minimized)
            {
                form.WindowState = FormWindowState.Normal;
                form.Show();
                form.BringToFront();
                form.Focus();
                return;
            }
            else if (form.Visible)
            {
                form.Hide();
                return;
            }
            form.Show();
            form.BringToFront();
            form.Focus();
        }

        private void InitFlashMovie()
        {
            byte[] aqlitegrimoire;

            if (!System.Diagnostics.Debugger.IsAttached)
            {
                aqlitegrimoire = Resources.aqlitegrimoire;
            }
            else
            {
                aqlitegrimoire = Resources.aqlitegrimoiredebug;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(8 + aqlitegrimoire.Length);
                    binaryWriter.Write(1432769894);
                    binaryWriter.Write(aqlitegrimoire.Length);
                    binaryWriter.Write(aqlitegrimoire);
                    memoryStream.Seek(0L, SeekOrigin.Begin);
                    flashPlayer.OcxState = new AxHost.State(memoryStream, 1, manualUpdate: false, null);
                }
            }

            Bypass.Unhook();
        }

        private void btnBank_Click(object sender, EventArgs e)
        {
            Player.Bank.Show();
        }

        private void cbCells_Click(object sender, EventArgs e)
        {
            cbCells.Items.Clear();
            ComboBox.ObjectCollection items = cbCells.Items;
            object[] cells = World.Cells;
            object[] items2 = cells;
            items.AddRange(items2);
        }

        private void Root_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hotkeys.InstalledHotkeys.ForEach(delegate (Hotkey h)
            {
                h.Uninstall();
            });
            KeyboardHook.Instance.Dispose();
            Proxy.Instance.Stop(appClosing: true);
            CommandColorForm.Instance.Dispose();
            nTray.Icon.Dispose();
            nTray.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Root));
            this.nTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cbPads = new DarkUI.Controls.DarkComboBox();
            this.cbCells = new DarkUI.Controls.DarkComboBox();
            this.flashPlayer = new AxShockwaveFlashObjects.AxShockwaveFlash();
            this.btnJump = new DarkUI.Controls.DarkButton();
            this.botToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastTravelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadersgrabbersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cosmeticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eyeDropperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.notepadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.FPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.packetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snifferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spammerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tampererToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.botRequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grimoireSuggestionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.provokeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.enemyMagnetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lagKillerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hidePlayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skipCutscenesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableAnimationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.grimliteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.darkMenuStrip1 = new DarkUI.Controls.DarkMenuStrip();
            this.getBotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.flashPlayer)).BeginInit();
            this.darkMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nTray
            // 
            this.nTray.Icon = ((System.Drawing.Icon)(resources.GetObject("nTray.Icon")));
            this.nTray.Text = "GrimLite";
            this.nTray.Visible = true;
            this.nTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nTray_MouseClick);
            // 
            // cbPads
            // 
            this.cbPads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPads.FormattingEnabled = true;
            this.cbPads.Items.AddRange(new object[] {
            "Center",
            "Spawn",
            "Left",
            "Right",
            "Top",
            "Bottom",
            "Up",
            "Down"});
            this.cbPads.Location = new System.Drawing.Point(657, 5);
            this.cbPads.Name = "cbPads";
            this.cbPads.Size = new System.Drawing.Size(91, 21);
            this.cbPads.TabIndex = 17;
            // 
            // cbCells
            // 
            this.cbCells.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCells.FormattingEnabled = true;
            this.cbCells.Location = new System.Drawing.Point(563, 5);
            this.cbCells.Name = "cbCells";
            this.cbCells.Size = new System.Drawing.Size(91, 21);
            this.cbCells.TabIndex = 18;
            this.cbCells.Click += new System.EventHandler(this.cbCells_Click);
            // 
            // flashPlayer
            // 
            this.flashPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flashPlayer.Enabled = true;
            this.flashPlayer.Location = new System.Drawing.Point(2, 29);
            this.flashPlayer.Name = "flashPlayer";
            this.flashPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("flashPlayer.OcxState")));
            this.flashPlayer.Size = new System.Drawing.Size(956, 546);
            this.flashPlayer.TabIndex = 2;
            // 
            // btnJump
            // 
            this.btnJump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJump.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnJump.Checked = false;
            this.btnJump.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnJump.Location = new System.Drawing.Point(754, 3);
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(53, 23);
            this.btnJump.TabIndex = 28;
            this.btnJump.Text = "Jump";
            this.btnJump.Click += new System.EventHandler(this.btnJump_Click);
            // 
            // botToolStripMenuItem
            // 
            this.botToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.botToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.botToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.botToolStripMenuItem.Name = "botToolStripMenuItem";
            this.botToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.botToolStripMenuItem.Text = "Bot";
            this.botToolStripMenuItem.Click += new System.EventHandler(this.botToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.startToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fastTravelsToolStripMenuItem,
            this.loadersgrabbersToolStripMenuItem,
            this.hotkeysToolStripMenuItem,
            this.pluginManagerToolStripMenuItem,
            this.cosmeticsToolStripMenuItem,
            this.bankToolStripMenuItem,
            this.setsToolStripMenuItem,
            this.eyeDropperToolStripMenuItem,
            this.logsToolStripMenuItem1,
            this.notepadToolStripMenuItem1,
            this.FPSToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 23);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // fastTravelsToolStripMenuItem
            // 
            this.fastTravelsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.fastTravelsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.fastTravelsToolStripMenuItem.Name = "fastTravelsToolStripMenuItem";
            this.fastTravelsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.fastTravelsToolStripMenuItem.Text = "Fast travels";
            this.fastTravelsToolStripMenuItem.Click += new System.EventHandler(this.fastTravelsToolStripMenuItem_Click);
            // 
            // loadersgrabbersToolStripMenuItem
            // 
            this.loadersgrabbersToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.loadersgrabbersToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.loadersgrabbersToolStripMenuItem.Name = "loadersgrabbersToolStripMenuItem";
            this.loadersgrabbersToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.loadersgrabbersToolStripMenuItem.Text = "Loaders/grabbers";
            this.loadersgrabbersToolStripMenuItem.Click += new System.EventHandler(this.loadersgrabbersToolStripMenuItem_Click);
            // 
            // hotkeysToolStripMenuItem
            // 
            this.hotkeysToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.hotkeysToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.hotkeysToolStripMenuItem.Name = "hotkeysToolStripMenuItem";
            this.hotkeysToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.hotkeysToolStripMenuItem.Text = "Hotkeys";
            this.hotkeysToolStripMenuItem.Click += new System.EventHandler(this.hotkeysToolStripMenuItem_Click);
            // 
            // pluginManagerToolStripMenuItem
            // 
            this.pluginManagerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pluginManagerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.pluginManagerToolStripMenuItem.Name = "pluginManagerToolStripMenuItem";
            this.pluginManagerToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.pluginManagerToolStripMenuItem.Text = "Plugin manager";
            this.pluginManagerToolStripMenuItem.Click += new System.EventHandler(this.pluginManagerToolStripMenuItem_Click);
            // 
            // cosmeticsToolStripMenuItem
            // 
            this.cosmeticsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.cosmeticsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.cosmeticsToolStripMenuItem.Name = "cosmeticsToolStripMenuItem";
            this.cosmeticsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.cosmeticsToolStripMenuItem.Text = "Cosmetics";
            this.cosmeticsToolStripMenuItem.Click += new System.EventHandler(this.cosmeticsToolStripMenuItem_Click);
            // 
            // bankToolStripMenuItem
            // 
            this.bankToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.bankToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.bankToolStripMenuItem.Name = "bankToolStripMenuItem";
            this.bankToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.bankToolStripMenuItem.Text = "Bank";
            this.bankToolStripMenuItem.Click += new System.EventHandler(this.bankToolStripMenuItem_Click);
            // 
            // setsToolStripMenuItem
            // 
            this.setsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.setsToolStripMenuItem.Enabled = false;
            this.setsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.setsToolStripMenuItem.Name = "setsToolStripMenuItem";
            this.setsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.setsToolStripMenuItem.Text = "Sets";
            this.setsToolStripMenuItem.Click += new System.EventHandler(this.setsToolStripMenuItem_Click);
            // 
            // eyeDropperToolStripMenuItem
            // 
            this.eyeDropperToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.eyeDropperToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.eyeDropperToolStripMenuItem.Name = "eyeDropperToolStripMenuItem";
            this.eyeDropperToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.eyeDropperToolStripMenuItem.Text = "Eye Dropper";
            this.eyeDropperToolStripMenuItem.Click += new System.EventHandler(this.eyeDropperToolStripMenuItem_Click_1);
            // 
            // logsToolStripMenuItem1
            // 
            this.logsToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.logsToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.logsToolStripMenuItem1.Name = "logsToolStripMenuItem1";
            this.logsToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.logsToolStripMenuItem1.Text = "Logs";
            this.logsToolStripMenuItem1.Click += new System.EventHandler(this.logsToolStripMenuItem1_Click);
            // 
            // notepadToolStripMenuItem1
            // 
            this.notepadToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.notepadToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.notepadToolStripMenuItem1.Name = "notepadToolStripMenuItem1";
            this.notepadToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.notepadToolStripMenuItem1.Text = "Notepad";
            this.notepadToolStripMenuItem1.Click += new System.EventHandler(this.notepadToolStripMenuItem1_Click);
            // 
            // FPSToolStripMenuItem
            // 
            this.FPSToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.FPSToolStripMenuItem.CheckOnClick = true;
            this.FPSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox2});
            this.FPSToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.FPSToolStripMenuItem.Name = "FPSToolStripMenuItem";
            this.FPSToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.FPSToolStripMenuItem.Text = "Set FPS";
            this.FPSToolStripMenuItem.Click += new System.EventHandler(this.FPSToolStripMenuItem_Click);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox2.Text = "60";
            // 
            // packetsToolStripMenuItem
            // 
            this.packetsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.packetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snifferToolStripMenuItem,
            this.spammerToolStripMenuItem,
            this.tampererToolStripMenuItem});
            this.packetsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.packetsToolStripMenuItem.Name = "packetsToolStripMenuItem";
            this.packetsToolStripMenuItem.Size = new System.Drawing.Size(59, 23);
            this.packetsToolStripMenuItem.Text = "Packets";
            // 
            // snifferToolStripMenuItem
            // 
            this.snifferToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.snifferToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.snifferToolStripMenuItem.Name = "snifferToolStripMenuItem";
            this.snifferToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.snifferToolStripMenuItem.Text = "Sniffer";
            this.snifferToolStripMenuItem.Click += new System.EventHandler(this.snifferToolStripMenuItem_Click);
            // 
            // spammerToolStripMenuItem
            // 
            this.spammerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.spammerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.spammerToolStripMenuItem.Name = "spammerToolStripMenuItem";
            this.spammerToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.spammerToolStripMenuItem.Text = "Spammer";
            this.spammerToolStripMenuItem.Click += new System.EventHandler(this.spammerToolStripMenuItem_Click);
            // 
            // tampererToolStripMenuItem
            // 
            this.tampererToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tampererToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tampererToolStripMenuItem.Name = "tampererToolStripMenuItem";
            this.tampererToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.tampererToolStripMenuItem.Text = "Tamperer";
            this.tampererToolStripMenuItem.Click += new System.EventHandler(this.tampererToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discordToolStripMenuItem,
            this.botRequestToolStripMenuItem,
            this.grimoireSuggestionsToolStripMenuItem,
            this.toolStripMenuItem1});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 23);
            this.helpToolStripMenuItem.Text = "More";
            // 
            // discordToolStripMenuItem
            // 
            this.discordToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.discordToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.discordToolStripMenuItem.Name = "discordToolStripMenuItem";
            this.discordToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.discordToolStripMenuItem.Text = "Discord";
            this.discordToolStripMenuItem.Click += new System.EventHandler(this.discordToolStripMenuItem_Click);
            // 
            // botRequestToolStripMenuItem
            // 
            this.botRequestToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.botRequestToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.botRequestToolStripMenuItem.Name = "botRequestToolStripMenuItem";
            this.botRequestToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.botRequestToolStripMenuItem.Text = "Bot Request";
            this.botRequestToolStripMenuItem.Click += new System.EventHandler(this.botRequestToolStripMenuItem_Click);
            // 
            // grimoireSuggestionsToolStripMenuItem
            // 
            this.grimoireSuggestionsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grimoireSuggestionsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.grimoireSuggestionsToolStripMenuItem.Name = "grimoireSuggestionsToolStripMenuItem";
            this.grimoireSuggestionsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.grimoireSuggestionsToolStripMenuItem.Text = "Grimoire Suggestions";
            this.grimoireSuggestionsToolStripMenuItem.Click += new System.EventHandler(this.grimoireSuggestionsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItem1.Text = "Bot Portal";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infRangeToolStripMenuItem,
            this.provokeToolStripMenuItem1,
            this.enemyMagnetToolStripMenuItem,
            this.lagKillerToolStripMenuItem,
            this.hidePlayersToolStripMenuItem,
            this.skipCutscenesToolStripMenuItem,
            this.disableAnimationsToolStripMenuItem,
            this.setFPSToolStripMenuItem});
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 23);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // infRangeToolStripMenuItem
            // 
            this.infRangeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.infRangeToolStripMenuItem.CheckOnClick = true;
            this.infRangeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.infRangeToolStripMenuItem.Name = "infRangeToolStripMenuItem";
            this.infRangeToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.infRangeToolStripMenuItem.Text = "Infinite Range";
            this.infRangeToolStripMenuItem.Click += new System.EventHandler(this.infRangeToolStripMenuItem_Click);
            // 
            // provokeToolStripMenuItem1
            // 
            this.provokeToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.provokeToolStripMenuItem1.CheckOnClick = true;
            this.provokeToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.provokeToolStripMenuItem1.Name = "provokeToolStripMenuItem1";
            this.provokeToolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
            this.provokeToolStripMenuItem1.Text = "Provoke";
            this.provokeToolStripMenuItem1.Click += new System.EventHandler(this.provokeToolStripMenuItem1_Click);
            // 
            // enemyMagnetToolStripMenuItem
            // 
            this.enemyMagnetToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.enemyMagnetToolStripMenuItem.CheckOnClick = true;
            this.enemyMagnetToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.enemyMagnetToolStripMenuItem.Name = "enemyMagnetToolStripMenuItem";
            this.enemyMagnetToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.enemyMagnetToolStripMenuItem.Text = "Enemy Magnet";
            this.enemyMagnetToolStripMenuItem.Click += new System.EventHandler(this.enemyMagnetToolStripMenuItem_Click);
            // 
            // lagKillerToolStripMenuItem
            // 
            this.lagKillerToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lagKillerToolStripMenuItem.CheckOnClick = true;
            this.lagKillerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lagKillerToolStripMenuItem.Name = "lagKillerToolStripMenuItem";
            this.lagKillerToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.lagKillerToolStripMenuItem.Text = "Lag Killer";
            this.lagKillerToolStripMenuItem.Click += new System.EventHandler(this.lagKillerToolStripMenuItem_Click);
            // 
            // hidePlayersToolStripMenuItem
            // 
            this.hidePlayersToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.hidePlayersToolStripMenuItem.CheckOnClick = true;
            this.hidePlayersToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.hidePlayersToolStripMenuItem.Name = "hidePlayersToolStripMenuItem";
            this.hidePlayersToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.hidePlayersToolStripMenuItem.Text = "Hide Players";
            this.hidePlayersToolStripMenuItem.Click += new System.EventHandler(this.hidePlayersToolStripMenuItem_Click);
            // 
            // skipCutscenesToolStripMenuItem
            // 
            this.skipCutscenesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.skipCutscenesToolStripMenuItem.CheckOnClick = true;
            this.skipCutscenesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.skipCutscenesToolStripMenuItem.Name = "skipCutscenesToolStripMenuItem";
            this.skipCutscenesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.skipCutscenesToolStripMenuItem.Text = "Skip Cutscenes";
            this.skipCutscenesToolStripMenuItem.Click += new System.EventHandler(this.skipCutscenesToolStripMenuItem_Click);
            // 
            // disableAnimationsToolStripMenuItem
            // 
            this.disableAnimationsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.disableAnimationsToolStripMenuItem.CheckOnClick = true;
            this.disableAnimationsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.disableAnimationsToolStripMenuItem.Name = "disableAnimationsToolStripMenuItem";
            this.disableAnimationsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.disableAnimationsToolStripMenuItem.Text = "Disable Animations";
            this.disableAnimationsToolStripMenuItem.Click += new System.EventHandler(this.disableAnimationsToolStripMenuItem_Click);
            // 
            // setFPSToolStripMenuItem
            // 
            this.setFPSToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.setFPSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.setFPSToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.setFPSToolStripMenuItem.Name = "setFPSToolStripMenuItem";
            this.setFPSToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.setFPSToolStripMenuItem.Text = "Set FPS";
            this.setFPSToolStripMenuItem.Click += new System.EventHandler(this.setFPSToolStripMenuItem_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.Text = "24";
            // 
            // grimliteToolStripMenuItem
            // 
            this.grimliteToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.grimliteToolStripMenuItem.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grimliteToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.grimliteToolStripMenuItem.Name = "grimliteToolStripMenuItem";
            this.grimliteToolStripMenuItem.Size = new System.Drawing.Size(79, 23);
            this.grimliteToolStripMenuItem.Text = "Grimlite";
            this.grimliteToolStripMenuItem.Click += new System.EventHandler(this.grimliteToolStripMenuItem_Click);
            // 
            // bankToolStripMenuItem1
            // 
            this.bankToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.bankToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem});
            this.bankToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.bankToolStripMenuItem1.Name = "bankToolStripMenuItem1";
            this.bankToolStripMenuItem1.Size = new System.Drawing.Size(45, 23);
            this.bankToolStripMenuItem1.Text = "Bank";
            this.bankToolStripMenuItem1.Click += new System.EventHandler(this.bankToolStripMenuItem1_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.reloadToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // pluginsStrip
            // 
            this.pluginsStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pluginsStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.pluginsStrip.Name = "pluginsStrip";
            this.pluginsStrip.Size = new System.Drawing.Size(58, 23);
            this.pluginsStrip.Text = "Plugins";
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMax.ForeColor = System.Drawing.Color.Black;
            this.btnMax.Location = new System.Drawing.Point(859, 2);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(49, 24);
            this.btnMax.TabIndex = 32;
            this.btnMax.Text = "🗖";
            this.btnMax.UseVisualStyleBackColor = false;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(909, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(49, 24);
            this.btnExit.TabIndex = 33;
            this.btnExit.Text = "×";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(32)))), ((int)(((byte)(71)))));
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.ForeColor = System.Drawing.Color.Black;
            this.btnMin.Location = new System.Drawing.Point(809, 2);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(49, 24);
            this.btnMin.TabIndex = 34;
            this.btnMin.Text = "🗕";
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // darkMenuStrip1
            // 
            this.darkMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.darkMenuStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkMenuStrip1.GripMargin = new System.Windows.Forms.Padding(2);
            this.darkMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grimliteToolStripMenuItem,
            this.botToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.packetsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.bankToolStripMenuItem1,
            this.helpToolStripMenuItem,
            this.pluginsStrip,
            this.getBotsToolStripMenuItem});
            this.darkMenuStrip1.Location = new System.Drawing.Point(2, 2);
            this.darkMenuStrip1.Name = "darkMenuStrip1";
            this.darkMenuStrip1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.darkMenuStrip1.Size = new System.Drawing.Size(956, 27);
            this.darkMenuStrip1.TabIndex = 35;
            this.darkMenuStrip1.Text = "darkMenuStrip1";
            this.darkMenuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenuMain_MouseDown);
            // 
            // getBotsToolStripMenuItem
            // 
            this.getBotsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.getBotsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.getBotsToolStripMenuItem.Name = "getBotsToolStripMenuItem";
            this.getBotsToolStripMenuItem.Size = new System.Drawing.Size(63, 23);
            this.getBotsToolStripMenuItem.Text = "Get Bots";
            this.getBotsToolStripMenuItem.Click += new System.EventHandler(this.getBotsToolStripMenuItem_Click);
            // 
            // Root
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.ClientSize = new System.Drawing.Size(960, 575);
            this.Controls.Add(this.btnMin);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnMax);
            this.Controls.Add(this.btnJump);
            this.Controls.Add(this.cbCells);
            this.Controls.Add(this.cbPads);
            this.Controls.Add(this.flashPlayer);
            this.Controls.Add(this.darkMenuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Root";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GrimLite";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Root_FormClosing);
            this.Load += new System.EventHandler(this.Root_Load);
            ((System.ComponentModel.ISupportInitialize)(this.flashPlayer)).EndInit();
            this.darkMenuStrip1.ResumeLayout(false);
            this.darkMenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Instance_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnBankReload_Click(object sender, EventArgs e)
        {
            Proxy.Instance.SendToServer($"%xt%zm%loadBank%{World.RoomId}%All%");
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(LogForm.Instance);
        }

        private void btnJump_Click(object sender, EventArgs e)
        {
            string Cell = (string)this.cbCells.SelectedItem;
            string Pad = (string)this.cbPads.SelectedItem;
            Player.MoveToCell(Cell ?? Player.Cell, Pad ?? Player.Pad);
        }

        private void cosmeticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(CosmeticForm.Instance);
        }

        private void bankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(BankForm.Instance);
        }

        private void discordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.io/AQWBots");
        }

        private void botRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSd2NSx1ezF-6bc2jRBuTniIka5z6kA2NbmC8CRCOFtpVxcRCA/viewform");
        }

        private void setsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(Set.Instance);
        }

        private void grimoireSuggestionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSetfV9zl18G9s7w_XReJ1yJNT9aZwxB1FLzU0l1UhdmXv5rIw/viewform?usp=sf_link");
        }

        private async void btnStop_ClickAsync(object sender, EventArgs e)
        {
            if (Configuration.Instance.Items != null && Configuration.Instance.BankOnStop)
            {
                foreach (InventoryItem item in Player.Inventory.Items)
                {
                    if (!item.IsEquipped && item.IsAcItem && item.Category != "Class" && item.Name.ToLower() != "treasure potion" && Configuration.Instance.Items.Contains(item.Name))
                    {
                        Player.Bank.TransferToBank(item.Name);
                        await Task.Delay(70);
                        LogForm.Instance.AppendDebug("Transferred to Bank: " + item.Name + "\r\n");
                    }
                }
                LogForm.Instance.AppendDebug("Banked all AC Items in Items list \r\n");
            }
            startToolStripMenuItem.Enabled = false;
            BotManager.Instance.ActiveBotEngine.Stop();
            BotManager.Instance.MultiMode();
            await Task.Delay(2000);
            BotManager.Instance.BotStateChanged(IsRunning: false);
            this.BotStateChanged(IsRunning: false);
            startToolStripMenuItem.Enabled = true;
        }

        public void BotStateChanged(bool IsRunning)
        {
            if (IsRunning)
            {
                startToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
            }
            else
            {
                startToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
            }
        }

        private void nTray_MouseClick(object sender, MouseEventArgs e)
        {
            ShowForm(this);
        }

        private void eyeDropperToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ShowForm(EyeDropper.Instance);
        }

        private void logsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(LogForm.Instance);
        }

        private void notepadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(Notepad.Instance);
        }

        private void infRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = infRangeToolStripMenuItem.Checked;
            OptionsManager.InfiniteRange = check;
            botManager.chkInfiniteRange.Checked = check;
        }

        private void provokeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool check = provokeToolStripMenuItem1.Checked;
            OptionsManager.ProvokeMonsters = check;
            botManager.chkProvoke.Checked = check;
        }

        private void enemyMagnetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = enemyMagnetToolStripMenuItem.Checked;
            OptionsManager.EnemyMagnet = check;
            botManager.chkMagnet.Checked = check;
        }

        private void lagKillerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = lagKillerToolStripMenuItem.Checked;
            OptionsManager.EnemyMagnet = check;
            botManager.chkLag.Checked = check;
        }

        private void hidePlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = hidePlayersToolStripMenuItem.Checked;
            OptionsManager.EnemyMagnet = check;
            botManager.chkHidePlayers.Checked = check;
        }

        private void skipCutscenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = skipCutscenesToolStripMenuItem.Checked;
            OptionsManager.EnemyMagnet = check;
            botManager.chkSkipCutscenes.Checked = check;
        }

        private void disableAnimationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool check = disableAnimationsToolStripMenuItem.Checked;
            OptionsManager.EnemyMagnet = check;
            botManager.chkDisableAnims.Checked = check;
        }
        
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void MenuMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const UInt32 WM_NCHITTEST = 0x0084;
            const UInt32 WM_MOUSEMOVE = 0x0200;

            const UInt32 HTLEFT = 10;
            const UInt32 HTRIGHT = 11;
            const UInt32 HTBOTTOMRIGHT = 17;
            const UInt32 HTBOTTOM = 15;
            const UInt32 HTBOTTOMLEFT = 16;
            const UInt32 HTTOP = 12;
            const UInt32 HTTOPLEFT = 13;
            const UInt32 HTTOPRIGHT = 14;

            const int RESIZE_HANDLE_SIZE = 10;
            bool handled = false;
            if (m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE)
            {
                Size formSize = this.Size;
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);

                Dictionary<UInt32, Rectangle> boxes = new Dictionary<UInt32, Rectangle>() {
            {HTBOTTOMLEFT, new Rectangle(0, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
            {HTBOTTOM, new Rectangle(RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, formSize.Width - 2*RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
            {HTBOTTOMRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, formSize.Height - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE)},
            {HTRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, formSize.Height - 2*RESIZE_HANDLE_SIZE)},
            {HTTOPRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, 0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE) },
            {HTTOP, new Rectangle(RESIZE_HANDLE_SIZE, 0, formSize.Width - 2*RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE) },
            {HTTOPLEFT, new Rectangle(0, 0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE) },
            {HTLEFT, new Rectangle(0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, formSize.Height - 2*RESIZE_HANDLE_SIZE) }
        };

                foreach (KeyValuePair<UInt32, Rectangle> hitBox in boxes)
                {
                    if (hitBox.Value.Contains(clientPoint))
                    {
                        m.Result = (IntPtr)hitBox.Key;
                        handled = true;
                        break;
                    }
                }
            }

            if (!handled)
                base.WndProc(ref m);
        }

        private void bankToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Player.Bank.Show();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Proxy.Instance.SendToServer($"%xt%zm%loadBank%{World.RoomId}%All%");
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn && BotManager.Instance.lstCommands.Items.Count > 0)
            {
                BotManager.Instance.MultiMode();
                BotManager.Instance.ActiveBotEngine.IsRunningChanged += BotManager.Instance.OnIsRunningChanged;
                BotManager.Instance.ActiveBotEngine.IndexChanged += BotManager.Instance.OnIndexChanged;
                BotManager.Instance.ActiveBotEngine.ConfigurationChanged += BotManager.Instance.OnConfigurationChanged;
                BotManager.Instance.ActiveBotEngine.Start(BotManager.Instance.GenerateConfiguration());
                BotManager.Instance.BotStateChanged(IsRunning: true);
                this.BotStateChanged(IsRunning: true);
            }
        }

        private async void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Configuration.Instance.Items != null && Configuration.Instance.BankOnStop)
            {
                foreach (InventoryItem item in Player.Inventory.Items)
                {
                    if (!item.IsEquipped && item.IsAcItem && item.Category != "Class" && item.Name.ToLower() != "treasure potion" && Configuration.Instance.Items.Contains(item.Name))
                    {
                        Player.Bank.TransferToBank(item.Name);
                        await Task.Delay(70);
                        LogForm.Instance.AppendDebug("Transferred to Bank: " + item.Name + "\r\n");
                    }
                }
                LogForm.Instance.AppendDebug("Banked all AC Items in Items list \r\n");
            }
            startToolStripMenuItem.Enabled = false;
            BotManager.Instance.ActiveBotEngine.Stop();
            BotManager.Instance.MultiMode();
            await Task.Delay(2000);
            BotManager.Instance.BotStateChanged(IsRunning: false);
            this.BotStateChanged(IsRunning: false);
            startToolStripMenuItem.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void grimliteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(AboutForm.Instance);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("https://adventurequest.life/");
        }

        private void setFPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flash.Call("SetFPS", int.Parse(toolStripTextBox1.Text));
        }

        private void FPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FPSToolStripMenuItem.Checked)
                Flash.Call("SetFPS", FPSToolStripMenuItem.DropDownItems[0].Text);
             else
                Flash.Call("SetFPS", 24);
        }

        private void getBotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://adventurequest.life/");
        }
    }
}
