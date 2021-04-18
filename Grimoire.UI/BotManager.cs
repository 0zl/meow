using Grimoire.Botting;
using Grimoire.Botting.Commands;
using Grimoire.Botting.Commands.Combat;
using Grimoire.Botting.Commands.Item;
using Grimoire.Botting.Commands.Map;
using Grimoire.Botting.Commands.Misc;
using Grimoire.Botting.Commands.Misc.Statements;
using Grimoire.Botting.Commands.Quest;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.Properties;
using Grimoire.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Grimoire.UI;
using System.Drawing.Drawing2D;
using static Grimoire.Botting.Commands.Item.CmdWhitelist;
using System.Runtime.InteropServices;
using DarkUI.Forms;
using DarkUI.Controls;

namespace Grimoire.UI
{
    public class BotManager : DarkForm
    {
        private IBotEngine _activeBotEngine = new Bot();

        private List<StatementCommand> _statementCommands;

        private Dictionary<string, string> _defaultControlText;

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        };
        
        #region Definitions
        private IContainer components;
        private Panel[] _panels;
        public ListBox lstCommands;
        private ListBox lstSkills;
        private ListBox lstQuests;
        public ListBox lstDrops;
        private ListBox lstBoosts;
        public static LogForm Log;
        private string _customName;
        private string _customGuild;
        private ListBox lstItems;
        private FlatTabControl.FlatTabControl tabControl1;
        private TabPage tabCombat;
        private Panel pnlCombat;
        private DarkButton btnUseSkillSet;
        private DarkButton btnAddSkillSet;
        private DarkTextBox txtSkillSet;
        private DarkCheckBox chkSafeMp;
        private DarkLabel label17;
        private DarkLabel label16;
        private DarkButton btnRest;
        private DarkButton btnRestF;
        private DarkCheckBox chkSkillCD;
        private DarkLabel label12;
        private DarkLabel label11;
        private DarkLabel label10;
        private DarkButton btnKill;
        private DarkLabel label13;
        private DarkCheckBox chkExistQuest;
        private DarkNumericUpDown numRestMP;
        private DarkCheckBox chkMP;
        private DarkNumericUpDown numRest;
        private DarkCheckBox chkHP;
        private DarkCheckBox chkPacket;
        private DarkNumericUpDown numSkillD;
        private DarkLabel label2;
        private DarkNumericUpDown numSafe;
        private DarkButton btnAddSafe;
        private DarkButton btnAddSkill;
        private DarkNumericUpDown numSkill;
        private DarkCheckBox chkExitRest;
        private DarkCheckBox chkAllSkillsCD;
        private DarkTextBox txtKillFQ;
        private DarkTextBox txtKillFItem;
        private DarkTextBox txtKillFMon;
        private RadioButton rbTemp;
        private RadioButton rbItems;
        private DarkButton btnKillF;
        private DarkTextBox txtMonster;
        private Panel pnlItem;
        private DarkTextBox txtShopItem;
        private DarkNumericUpDown numShopId;
        private DarkLabel label15;
        private DarkButton btnBuy;
        private DarkButton btnBuyFast;
        private DarkButton btnLoadShop;
        private DarkButton btnBoost;
        private DarkComboBox cbBoosts;
        private DarkNumericUpDown numMapItem;
        private DarkButton btnMapItem;
        private DarkButton btnSwap;
        private DarkTextBox txtSwapInv;
        private DarkTextBox txtSwapBank;
        public DarkCheckBox chkPickup;
        private DarkButton btnBoth;
        private DarkTextBox txtWhitelist;
        private DarkButton btnItem;
        private DarkButton btnUnbanklst;
        private DarkTextBox txtItem;
        private DarkComboBox cbItemCmds;
        private TabPage tabMap;
        private Panel pnlMap;
        private DarkButton btnWalkCur;
        private DarkButton btnWalk;
        private DarkNumericUpDown numWalkY;
        private DarkNumericUpDown numWalkX;
        private DarkButton btnCellSwap;
        private DarkButton btnJump;
        private DarkButton btnCurrCell;
        private DarkTextBox txtPad;
        private DarkTextBox txtCell;
        private DarkButton btnJoin;
        private DarkTextBox txtJoinPad;
        private DarkTextBox txtJoinCell;
        private DarkTextBox txtJoin;
        private TabPage tabQuest;
        private Panel pnlQuest;
        private DarkButton btnQuestAccept;
        private DarkButton btnQuestComplete;
        private DarkButton btnQuestAdd;
        private DarkNumericUpDown numQuestItem;
        private DarkCheckBox chkQuestItem;
        private DarkNumericUpDown numQuestID;
        private DarkLabel label4;
        private TabPage tabMisc;
        private Panel pnlMisc;
        private DarkCheckBox chkRestartDeath;
        private DarkCheckBox chkMerge;
        private DarkButton button2;
        private DarkButton btnLogout;
        private DarkTextBox txtLabel;
        private DarkButton btnGotoLabel;
        private DarkButton btnAddLabel;
        private DarkTextBox txtDescription;
        private DarkTextBox txtAuthor;
        private DarkButton btnSave;
        private DarkButton btnDelay;
        private DarkNumericUpDown numDelay;
        private DarkLabel label3;
        private DarkNumericUpDown numBotDelay;
        private DarkButton btnBotDelay;
        private DarkTextBox txtPlayer;
        private DarkButton btnGoto;
        private DarkButton btnLoad;
        private DarkButton btnRestart;
        private DarkButton btnStop;
        private DarkButton btnLoadCmd;
        private DarkCheckBox chkSkip;
        private DarkButton btnStatementAdd;
        private DarkTextBox txtStatement2;
        private DarkTextBox txtStatement1;
        private DarkComboBox cbStatement;
        private DarkComboBox cbCategories;
        private DarkTextBox txtPacket;
        private DarkButton btnPacket;
        private TabPage tabOptions;
        private Panel pnlOptions;
        private DarkCheckBox chkEnableSettings;
        public DarkCheckBox chkDisableAnims;
        private DarkTextBox txtSoundItem;
        private DarkButton btnSoundAdd;
        private DarkButton btnSoundDelete;
        private DarkButton btnSoundTest;
        private ListBox lstSoundItems;
        private DarkLabel label9;
        private DarkNumericUpDown numWalkSpeed;
        private DarkLabel label8;
        public DarkCheckBox chkSkipCutscenes;
        public DarkCheckBox chkHidePlayers;
        public DarkCheckBox chkLag;
        public DarkCheckBox chkMagnet;
        public DarkCheckBox chkProvoke;
        public DarkCheckBox chkInfiniteRange;
        private DarkGroupBox grpLogin;
        private DarkComboBox cbServers;
        private DarkCheckBox chkRelogRetry;
        private DarkCheckBox chkRelog;
        private DarkNumericUpDown numRelogDelay;
        private DarkLabel label7;
        private DarkTextBox txtUsername;
        private DarkTextBox txtGuild;
        private DarkButton btnchangeName;
        private DarkButton btnchangeGuild;
        public DarkCheckBox chkGender;
        private TabPage tabBots;
        private Panel pnlSaved;
        private DarkLabel lblBoosts;
        private DarkLabel lblDrops;
        private DarkLabel lblQuests;
        private DarkLabel lblSkills;
        private DarkLabel lblCommands;
        private DarkLabel lblItems;
        private DarkTextBox txtSavedDesc;
        private DarkTextBox txtSavedAuthor;
        private DarkLabel lblBots;
        private TreeView treeBots;
        private DarkTextBox txtSavedAdd;
        private DarkButton btnSavedAdd;
        private DarkTextBox txtSaved;
        private DarkCheckBox chkBankOnStop;
        private DarkLabel labelProvoke;
        private DarkButton btnProvoke;
        private DarkButton btnProvokeOn;
        private DarkButton btnProvokeOff;
        private ListBox lstLogText;
        private DarkButton btnLogDebug;
        private DarkButton btnLog;
        private DarkTextBox txtLog;
        public DarkCheckBox chkUntarget;
        private DarkLabel label5;
        private DarkNumericUpDown numOptionsTimer;
        private DarkLabel label6;
        private DarkCheckBox chkEnsureComplete;
        private DarkLabel label14;
        private DarkNumericUpDown numEnsureTries;
        private DarkButton btnWalkRdm;
        private DarkButton btnBlank;
        private DarkCheckBox chkRejectAll;
        private DarkCheckBox chkPickupAll;
        private DarkCheckBox chkReject;
        public DarkCheckBox chkBuff;
        private DarkCheckBox chkAFK;
        private SplitContainer splitContainer1;
        private DarkComboBox cbLists;
        private DarkCheckBox chkAll;
        private DarkButton btnClear;
        private DarkButton btnDown;
        private DarkButton btnRemove;
        private DarkButton btnUp;
        private DarkButton btnBotStop;
        private DarkButton btnBotStart;
        private Panel panel1;
        private SplitContainer splitContainer2;
        private DarkButton DarkButton2;
        private DarkButton btnCurrBlank;
        private DarkButton btnSetSpawn;
        private DarkButton btnBeep;
        private DarkNumericUpDown numBeepTimes;
        private Panel panel3;
        private Panel panel2;
        private DarkButton btnWhitelist;
        private DarkButton btnSkillCmd;
        private TabPage tabItem;
        private DarkCheckBox checkBox1;
        private DarkCheckBox chkBuffup;
        private TabPage tabOptions2;
        private DarkButton btnSetUndecided;
        private DarkButton btnSetChaos;
        private DarkButton btnSetEvil;
        private DarkButton btnSetGood;
        private DarkGroupBox grpAlignment;
        private DarkGroupBox grpAccessLevel;
        private DarkButton btnSetMem;
        private DarkButton btnSetModerator;
        private DarkButton btnSetNonMem;
        private DarkCheckBox chkToggleMute;
        private ContextMenuStrip BotManagerMenuStrip;
        private ToolStripMenuItem changeFontsToolStripMenuItem;
        private DarkLabel label1;
        private DarkNumericUpDown numDropDelay;
        private DarkLabel label18;
        private DarkButton btnGoDownIndex;
        private DarkButton btnGoUpIndex;
        private DarkButton btnGotoIndex;
        private DarkNumericUpDown numIndexCmd;
        private ToolStripMenuItem multilineToggleToolStripMenuItem;
        private ToolStripMenuItem toggleTabpagesToolStripMenuItem;
        private ToolStripMenuItem commandColorsToolStripMenuItem;
        private DarkButton btnChangeNameCmd;
        private DarkButton btnChangeGuildCmd;
        private DarkCheckBox chkAntiAfk;
        private DarkCheckBox chkChangeRoomTag;
        private DarkCheckBox chkChangeChat;
        private DarkNumericUpDown numSetLevel;
        private DarkCheckBox chkSetJoinLevel;
        private DarkButton btnClientPacket;
        private DarkCheckBox chkHideYulgarPlayers;
        private DarkNumericUpDown numSetInt;
        private DarkTextBox txtSetInt;
        private DarkButton btnSetInt;
        private DarkLabel label19;
        private DarkButton btnDecreaseInt;
        private DarkButton btnIncreaseInt;
        private DarkCheckBox colorfulCommands;
        private DarkButton btnWhitelistOff;
        private DarkButton btnWhitelistOn;
        private DarkButton btnWhitelistToggle;
        private DarkLabel label20;
        private DarkButton btnSearchCmd;
        private DarkTextBox txtSearchCmd;
        private TabPage tabMisc2;
        private DarkButton btnSpammerReset;
        private DarkTextBox txtPacketSpammer;
        private DarkButton btnSpammerStop;
        private DarkButton btnSpammerStart;
        private DarkButton btnSpammerRemove;
        private DarkButton btnSpammerRefresh;
        private DarkNumericUpDown numSpammerDelay;
        private DarkButton btnSpammerSetDelay;
        private DarkNumericUpDown numDelayedPacket;
        private DarkButton btnDelayedPacket;
        private DarkGroupBox groupBox1;
        private DarkButton btnAddInfoMsg;
        private DarkButton btnAddWarnMsg;
        private DarkTextBox inputMsgClient;
        private Panel panel4;
        public DarkCheckBox chkPickupAcTag;
        private DarkButton btnSpammerAdd;
        private DarkButton btnAttack;
        #endregion

        public static BotManager Instance
        {
            get;
        }

        public IBotEngine ActiveBotEngine
        {
            get
            {
                return _activeBotEngine;
            }
            set
            {
                if (_activeBotEngine.IsRunning)
                {
                    throw new InvalidOperationException("Cannot set a new bot engine while the current one is running");
                }

                _activeBotEngine = value ?? throw new ArgumentNullException("value");
            }
        }

        private ListBox SelectedList
        {
            get
            {
                switch (cbLists.SelectedIndex)
                {
                    case 1:
                        return lstSkills;

                    case 2:
                        return lstQuests;

                    case 3:
                        return lstDrops;

                    case 4:
                        return lstBoosts;

                    case 5:
                        return lstItems;

                    default:
                        return lstCommands;
                }
            }
        }

        public string CustomName
        {
            get => _customName;

            set
            {
                _customName = value;
                Flash.Call("ChangeName", _customName);
            }
        }

        public string CustomGuild
        {
            get => _customGuild;
            set
            {
                _customGuild = value;
                Flash.Call("ChangeGuild", txtGuild.Text);
            }
        }

        public static int SliderValue
        {
            get;
            set;
        }

        private BotManager()
        {
            InitializeComponent();

            
        }

        private void BotManager_Load(object sender, EventArgs e)
        {
            _panels = new Panel[7]
            {
                pnlCombat,
                pnlMap,
                pnlQuest,
                pnlItem,
                pnlMisc,
                pnlOptions,
                pnlSaved
            };
            pnlCombat.Size = new Size(442, 315);
            pnlMap.Size = new Size(442, 315);
            pnlQuest.Size = new Size(442, 315);
            pnlItem.Size = new Size(442, 315);
            pnlMisc.Size = new Size(442, 315);
            pnlOptions.Size = new Size(442, 315);
            pnlSaved.Size = new Size(442, 315);
            lstBoosts.DisplayMember = "Text";
            lstQuests.DisplayMember = "Text";
            lstSkills.DisplayMember = "Text";
            cbBoosts.DisplayMember = "Name";
            cbServers.DisplayMember = "Name";
            lstItems.DisplayMember = "Text";
            cbStatement.DisplayMember = "Text";
            cbLists.SelectedIndex = 0;
            _statementCommands = JsonConvert.DeserializeObject<List<StatementCommand>>(Resources.statementcmds, _serializerSettings);
            _defaultControlText = JsonConvert.DeserializeObject<Dictionary<string, string>>(Resources.defaulttext, _serializerSettings);
            OptionsManager.StateChanged += OnOptionsStateChanged;
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            string font = c.Get("font");
            //float fontSize = float.Parse(Config.Load(Application.StartupPath + "\\config.cfg").Get("fontSize") ?? "8.25", System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            if (font != null)
                this.Font = new Font(font, 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstCommands.ItemHeight = int.Parse(c.Get("CommandsSize") ?? "60");
            //MessageBox.Show("" + lstCommands.ItemHeight);
            lstCommands.Font = new Font(font, lstCommands.ItemHeight / 4 - (float)6.5, FontStyle.Regular, GraphicsUnit.Point, 0);
            //MessageBox.Show("" + lstCommands.Font.Size);
            lstCommands.ItemHeight = lstCommands.ItemHeight / 4;
            //MessageBox.Show("" + lstCommands.ItemHeight);
        }

        private void TextboxEnter(object sender, EventArgs e)
        {
            DarkTextBox textBox = (DarkTextBox)sender;
            textBox.Clear();
        }

        private void TextboxLeave(object sender, EventArgs e)
        {
            DarkTextBox textBox = (DarkTextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text) && _defaultControlText.TryGetValue(textBox.Name, out string value))
            {
                textBox.Text = value;
            }
        }

        public void OnServersLoaded(Server[] servers)
        {
            if (servers != null && servers.Length != 0 && cbServers.Items.Count <= 1)
            {
                cbServers.Items.AddRange(servers);
                cbServers.SelectedIndex = 0;
            }
        }

        private void MoveListItem(int direction)
        {
            if (SelectedList.SelectedItem != null && SelectedList.SelectedIndex >= 0)
            {
                int num = SelectedList.SelectedIndex + direction;
                if (num >= 0 && num < SelectedList.Items.Count)
                {
                    object selectedItem = SelectedList.SelectedItem;
                    SelectedList.Items.Remove(selectedItem);
                    SelectedList.Items.Insert(num, selectedItem);
                    SelectedList.SetSelected(num, value: true);
                }
            }
        }

        private void MoveListItemByKey(int direction)
        {
            if (SelectedList.SelectedItem == null || SelectedList.SelectedIndex < 0)
            {
                return;
            }
            int num = SelectedList.SelectedIndex + direction;
            if (num >= 0 && num < SelectedList.Items.Count)
            {
                object selectedItem = SelectedList.SelectedItem;
                SelectedList.Items.Remove(selectedItem);
                SelectedList.Items.Insert(num, selectedItem);
                if (direction == 1)
                {
                    SelectedList.SetSelected(num - 1, value: true);
                }
            }
        }

        public Configuration GenerateConfiguration()
        {
            return new Configuration
            {
                Author = txtAuthor.Text,
                Description = txtDescription.Text,
                Commands = lstCommands.Items.Cast<IBotCommand>().ToList(),
                Skills = lstSkills.Items.Cast<Skill>().ToList(),
                Quests = lstQuests.Items.Cast<Quest>().ToList(),
                Boosts = lstBoosts.Items.Cast<InventoryItem>().ToList(),
                Drops = lstDrops.Items.Cast<string>().ToList(),
                Items = lstItems.Items.Cast<string>().ToList(),
                SkillDelay = (int)numSkillD.Value,
                ExitCombatBeforeRest = chkExitRest.Checked,
                ExitCombatBeforeQuest = chkExistQuest.Checked,
                Server = (Server)cbServers.SelectedItem,
                AutoRelogin = chkRelog.Checked,
                RelogDelay = (int)numRelogDelay.Value,
                RelogRetryUponFailure = chkRelogRetry.Checked,
                BotDelay = (int)numBotDelay.Value,
                EnablePickup = chkPickup.Checked,
                EnableRejection = chkReject.Checked,
                EnablePickupAll = chkPickupAll.Checked,
                EnablePickupAcTagged = chkPickupAcTag.Checked,
                EnableRejectAll = chkRejectAll.Checked,
                WaitForAllSkills = chkAllSkillsCD.Checked,
                WaitForSkill = chkSkillCD.Checked,
                SkipDelayIndexIf = chkSkip.Checked,
                InfiniteAttackRange = chkInfiniteRange.Checked,
                ProvokeMonsters = chkProvoke.Checked,
                EnemyMagnet = chkMagnet.Checked,
                LagKiller = chkLag.Checked,
                HidePlayers = chkHidePlayers.Checked,
                SkipCutscenes = chkSkipCutscenes.Checked,
                WalkSpeed = (int)numWalkSpeed.Value,
                NotifyUponDrop = lstSoundItems.Items.Cast<string>().ToList(),
                RestIfMp = chkMP.Checked,
                RestIfHp = chkHP.Checked,
                EnsureTries = (int)numEnsureTries.Value,
                Untarget = chkUntarget.Checked,
                BankOnStop = chkBankOnStop.Checked,
                RestMp = (int)numRestMP.Value,
                RestHp = (int)numRest.Value,
                RestartUponDeath = chkRestartDeath.Checked,
                EnsureComplete = chkEnsureComplete.Checked,
                AFK = chkAFK.Checked,
                DropDelay = (int)numDropDelay.Value
            };
        }

        public void ApplyConfiguration(Configuration config)
        {
            if (config != null)
            {
                if (!chkMerge.Checked || ActiveBotEngine.IsRunning)
                {
                    lstCommands.Items.Clear();
                    lstBoosts.Items.Clear();
                    lstDrops.Items.Clear();
                    lstQuests.Items.Clear();
                    lstSkills.Items.Clear();
                    lstItems.Items.Clear();
                    lstSoundItems.Items.Clear();
                }
                txtSavedAuthor.Text = config.Author ?? "Author";
                txtSavedDesc.Text = config.Description ?? "Description";
                List<IBotCommand> commands = config.Commands;
                if (commands != null && commands.Count > 0)
                {
                    ListBox.ObjectCollection items = lstCommands.Items;
                    object[] array = config.Commands.ToArray();
                    items.AddRange(array);
                }
                List<Skill> skills = config.Skills;
                if (skills != null && skills.Count > 0)
                {
                    ListBox.ObjectCollection items = lstSkills.Items;
                    object[] array = config.Skills.ToArray();
                    items.AddRange(array);
                }
                List<Quest> quests = config.Quests;
                if (quests != null && quests.Count > 0)
                {
                    ListBox.ObjectCollection items = lstQuests.Items;
                    object[] array = config.Quests.ToArray();
                    items.AddRange(array);
                }
                List<InventoryItem> boosts = config.Boosts;
                if (boosts != null && boosts.Count > 0)
                {
                    ListBox.ObjectCollection items = lstBoosts.Items;
                    object[] array = config.Boosts.ToArray();
                    items.AddRange(array);
                }
                List<string> drops = config.Drops;
                if (drops != null && drops.Count > 0)
                {
                    ListBox.ObjectCollection items = lstDrops.Items;
                    object[] array = config.Drops.ToArray();
                    items.AddRange(array);
                }
                List<string> item = config.Items;
                if (item != null && item.Count > 0)
                {
                    ListBox.ObjectCollection items = lstItems.Items;
                    object[] array = config.Items.ToArray();
                    items.AddRange(array);
                }
                numSkillD.Value = config.SkillDelay;
                chkExitRest.Checked = config.ExitCombatBeforeRest;
                chkExistQuest.Checked = config.ExitCombatBeforeQuest;
                if (config.Server != null)
                {
                    cbServers.SelectedIndex = cbServers.Items.Cast<Server>().ToList().FindIndex((Server s) => s.Name == config.Server.Name);
                }
                chkRelog.Checked = config.AutoRelogin;
                numRelogDelay.Value = config.RelogDelay;
                chkRelogRetry.Checked = config.RelogRetryUponFailure;
                numBotDelay.Value = config.BotDelay;
                chkPickup.Checked = config.EnablePickup;
                chkReject.Checked = config.EnableRejection;
                chkPickupAll.Checked = config.EnablePickupAll;
                chkRejectAll.Checked = config.EnableRejectAll;
                chkAllSkillsCD.Checked = config.WaitForAllSkills;
                chkSkillCD.Checked = config.WaitForSkill;
                chkSkip.Checked = config.SkipDelayIndexIf;
                chkInfiniteRange.Checked = config.InfiniteAttackRange;
                chkProvoke.Checked = config.ProvokeMonsters;
                chkLag.Checked = config.LagKiller;
                chkMagnet.Checked = config.EnemyMagnet;
                chkHidePlayers.Checked = config.HidePlayers;
                chkSkipCutscenes.Checked = config.SkipCutscenes;
                numWalkSpeed.Value = (config.WalkSpeed <= 0) ? 8 : config.WalkSpeed;
                List<string> notifyUponDrop = config.NotifyUponDrop;
                if (notifyUponDrop != null && notifyUponDrop.Count > 0)
                {
                    ListBox.ObjectCollection items14 = lstSoundItems.Items;
                    object[] array = config.NotifyUponDrop.ToArray();
                    object[] items15 = array;
                    items14.AddRange(items15);
                }
                numRestMP.Value = config.RestMp;
                numRest.Value = config.RestHp;
                chkMP.Checked = config.RestIfMp;
                chkHP.Checked = config.RestIfHp;
                numEnsureTries.Value = (config.EnsureTries <= 0) ? 20 : config.EnsureTries;
                chkUntarget.Checked = config.Untarget;
                chkBankOnStop.Checked = config.BankOnStop;
                chkRestartDeath.Checked = config.RestartUponDeath;
                chkEnsureComplete.Checked = config.EnsureComplete;
                chkAFK.Checked = config.AFK;
                numDropDelay.Value = config.DropDelay <= 0 ? 1000 : config.DropDelay;

                Configuration.Tempvalues.Clear();
                Configuration.Tempvariable.Clear();
            }
        }

        public void OnConfigurationChanged(Configuration config)
        {
            if (InvokeRequired)
            {
                Invoke((Action)delegate
                {
                    ApplyConfiguration(config);
                });
            }
            else
            {
                ApplyConfiguration(config);
            }
        }

        public void OnIndexChanged(int index)
        {
            if (index > -1)
            {
                if (InvokeRequired)
                {
                    Invoke((Action)delegate
                    {
                        lstCommands.SelectedIndex = index;
                    });
                }
                else
                {
                    lstCommands.SelectedIndex = index;
                }
            }
        }

        public void OnSkillIndexChanged(int index)
        {
            if (index > -1 && index < lstSkills.Items.Count)
            {
                Invoke((Action)delegate
                {
                    lstSkills.SelectedIndex = index;
                });
            }
        }

        public void OnIsRunningChanged(bool IsRunning)
        {
            Invoke((Action)delegate
            {
                if (!IsRunning)
                {
                    ActiveBotEngine.IsRunningChanged -= OnIsRunningChanged;
                    ActiveBotEngine.IndexChanged -= OnIndexChanged;
                    ActiveBotEngine.ConfigurationChanged -= OnConfigurationChanged;
                }
                BotStateChanged(IsRunning);
            });
        }

        private void lstCommands_DoubleClick(object sender, EventArgs e)
        {
            if (lstCommands.Items.Count <= 0)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Load bot";
                    openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
                    openFileDialog.Filter = "Grimoire bots|*.gbot";
                    openFileDialog.DefaultExt = ".gbot";
                    if (openFileDialog.ShowDialog() == DialogResult.OK && TryDeserialize(File.ReadAllText(openFileDialog.FileName), out Configuration config))
                    {
                        ApplyConfiguration(config);
                    }
                }
            }
            else if (lstCommands.SelectedIndex > -1)
            {
                int selectedIndex = lstCommands.SelectedIndex;
                object obj = lstCommands.Items[selectedIndex];
                string text = RawCommandEditor.Show(JsonConvert.SerializeObject(obj, Formatting.Indented, _serializerSettings));
                if (text != null)
                {
                    try
                    {
                        IBotCommand item = (IBotCommand)JsonConvert.DeserializeObject(text, obj.GetType());
                        lstCommands.Items.Remove(obj);
                        lstCommands.Items.Insert(selectedIndex, item);
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (SelectedList.SelectedItem != null)
            {
                int selectedIndex = SelectedList.SelectedIndex;
                if (selectedIndex > -1)
                {
                    _RemoveListBoxItem();
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            _MoveListBoxDown();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            _MoveListBoxUp();
        }

        private void _RemoveListBoxItem()
        {
            SelectedList.BeginUpdate();
            for (int x = SelectedList.SelectedIndices.Count - 1; x >= 0; x--)
            {
                int idx = SelectedList.SelectedIndices[x];
                SelectedList.Items.RemoveAt(idx);
            }
            SelectedList.EndUpdate();
        }

        private void _MoveListBoxUp()
        {
            //MoveListItem(-1);
            SelectedList.BeginUpdate();
            int numberOfSelectedItems = SelectedList.SelectedItems.Count;
            for (int i = 0; i < numberOfSelectedItems; i++)
            {
                // only if it's not the first item
                if (SelectedList.SelectedIndices[i] > 0)
                {
                    // the index of the item above the item that we wanna move up
                    int indexToInsertIn = SelectedList.SelectedIndices[i] - 1;
                    // insert UP the item that we want to move up
                    SelectedList.Items.Insert(indexToInsertIn, SelectedList.SelectedItems[i]);
                    // removing it from its old place
                    SelectedList.Items.RemoveAt(indexToInsertIn + 2);
                    // highlighting it in its new place
                    SelectedList.SelectedItem = SelectedList.Items[indexToInsertIn];
                }
            }
            SelectedList.EndUpdate();
        }

        private void _MoveListBoxDown()
        {
            //MoveListItem(1);
            SelectedList.BeginUpdate();
            int numberOfSelectedItems = SelectedList.SelectedItems.Count;
            // when going down, instead of moving through the selected items from top to bottom
            // we'll go from bottom to top, it's easier to handle this way.
            for (int i = numberOfSelectedItems - 1; i >= 0; i--)
            {
                // only if it's not the last item
                if (SelectedList.SelectedIndices[i] < SelectedList.Items.Count - 1)
                {
                    // the index of the item that is currently below the selected item
                    int indexToInsertIn = SelectedList.SelectedIndices[i] + 2;
                    // insert DOWN the item that we want to move down
                    SelectedList.Items.Insert(indexToInsertIn, SelectedList.SelectedItems[i]);
                    // removing it from its old place
                    SelectedList.Items.RemoveAt(indexToInsertIn - 2);
                    // highlighting it in its new place
                    SelectedList.SelectedItem = SelectedList.Items[indexToInsertIn - 1];
                }
            }
            SelectedList.EndUpdate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                lstBoosts.Items.Clear();
                lstCommands.Items.Clear();
                lstDrops.Items.Clear();
                lstItems.Items.Clear();
                lstQuests.Items.Clear();
                lstSkills.Items.Clear();
            }
            else
            {
                SelectedList.Items.Clear();
            }
        }

        private void cbLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoosts.Visible = SelectedList == lstBoosts;
            lstCommands.Visible = SelectedList == lstCommands;
            lstDrops.Visible = SelectedList == lstDrops;
            lstItems.Visible = SelectedList == lstItems;
            lstQuests.Visible = SelectedList == lstQuests;
            lstSkills.Visible = SelectedList == lstSkills;
        }

        private void BotManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            string monster = string.IsNullOrEmpty(txtMonster.Text) ? "*" : txtMonster.Text;
            if (txtMonster.Text == "Monster (*  = random)")
            {
                monster = "*";
            }
            AddCommand(new CmdKill
            {
                Monster = monster
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnKillF_Click(object sender, EventArgs e)
        {
            if (txtKillFItem.Text.Length > 0 && txtKillFQ.Text.Length > 0)
            {
                string monster = string.IsNullOrEmpty(txtKillFMon.Text) ? "*" : txtKillFMon.Text;
                string text = txtKillFItem.Text;
                string text2 = txtKillFQ.Text;
                AddCommand(new CmdKillFor
                {
                    ItemType = (!rbItems.Checked) ? ItemType.TempItems : ItemType.Items,
                    Monster = monster,
                    ItemName = text,
                    Quantity = text2
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnAddSkill_Click(object sender, EventArgs e)
        {
            string text = numSkill.Text;
            AddSkill(new Skill
            {
                Text = text + ": " + Skill.GetSkillName(text),
                Index = text,
                Type = Skill.SkillType.Normal
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnAddSafe_Click(object sender, EventArgs e)
        {
            string text = numSkill.Text;
            int safeHealth = (int)numSafe.Value;
            AddSkill(new Skill
            {
                Text = "[S] " + text + ": " + Skill.GetSkillName(text),
                Index = text,
                SafeHealth = safeHealth,
                Type = Skill.SkillType.Safe,
                SafeMp = chkSafeMp.Checked
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnRest_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdRest(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnRestF_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdRest
            {
                Full = true
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            string text = txtJoin.Text;
            string cell = string.IsNullOrEmpty(txtJoinCell.Text) ? "Enter" : txtJoinCell.Text;
            string pad = string.IsNullOrEmpty(txtJoinPad.Text) ? "Spawn" : txtJoinPad.Text;
            if (text.Length > 0)
            {
                AddCommand(new CmdJoin
                {
                    Map = text,
                    Cell = cell,
                    Pad = pad
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnCellSwap_Click(object sender, EventArgs e)
        {
            DarkButton s = sender as DarkButton;
            if (s.Text == "<")
            {
                txtJoin.Text = Player.Map + "-" + Flash.Call<string>("RoomNumber", new object[0]);
                txtJoinCell.Text = txtCell.Text;
                txtJoinPad.Text = txtPad.Text;
            }
            else if (s.Text == ">")
            {
                txtCell.Text = txtJoinCell.Text;
                txtPad.Text = txtJoinPad.Text;
            }
        }

        private void btnCurrCell_Click(object sender, EventArgs e)
        {
            txtCell.Text = Player.Cell;
            txtPad.Text = Player.Pad;
        }

        private void btnJump_Click(object sender, EventArgs e)
        {
            string cell = string.IsNullOrEmpty(txtCell.Text) ? "Enter" : txtCell.Text;
            string pad = string.IsNullOrEmpty(txtPad.Text) ? "Spawn" : txtPad.Text;
            AddCommand(new CmdMoveToCell
            {
                Cell = cell,
                Pad = pad
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnWalk_Click(object sender, EventArgs e)
        {
            string x = numWalkX.Value.ToString();
            string y = numWalkY.Value.ToString();
            AddCommand(new CmdWalk
            {
                X = x,
                Y = y
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnWalkCur_Click(object sender, EventArgs e)
        {
            float[] position = Player.Position;
            numWalkX.Value = (decimal)position[0];
            numWalkY.Value = (decimal)position[1];
        }

        private void btnWalkRdm_Click(object sender, EventArgs e)
        {
            string x = numWalkX.Value.ToString();
            string y = numWalkY.Value.ToString();
            AddCommand(new CmdWalk
            {
                Type = "Random",
                X = x,
                Y = y
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            string text = txtItem.Text;
            if (text.Length > 0 && cbItemCmds.SelectedIndex > -1)
            {
                IBotCommand cmd;
                switch (cbItemCmds.SelectedIndex)
                {
                    case 0:
                        cmd = new CmdGetDrop
                        {
                            ItemName = text
                        };
                        break;

                    case 1:
                        cmd = new CmdSell
                        {
                            ItemName = text
                        };
                        break;

                    case 2:
                        cmd = new CmdEquip
                        {
                            ItemName = text,
                            Safe = true
                        };
                        break;

                    case 3:
                        cmd = new CmdEquip
                        {
                            ItemName = text,
                            Safe = false
                        };
                        break;

                    case 4:
                        cmd = new CmdBankTransfer
                        {
                            ItemName = text,
                            TransferFromBank = false
                        };
                        break;

                    case 5:
                        cmd = new CmdBankTransfer
                        {
                            ItemName = text,
                            TransferFromBank = true
                        };
                        break;

                    case 6:
                        cmd = new CmdEquipSet
                        {
                            ItemName = text
                        };
                        break;

                    case 7:
                        cmd = new CmdWhitelist
                        {
                            ItemName = text,
                            state = State.Add
                        };
                        break;

                    case 8:
                        cmd = new CmdWhitelist
                        {
                            ItemName = text,
                            state = State.Remove
                        };
                        break;

                    default:
                        cmd = new CmdGetDrop
                        {
                            ItemName = text
                        };
                        break;
                }
                AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnMapItem_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdMapItem
            {
                ItemId = (int)numMapItem.Value
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnBoth_Click(object sender, EventArgs e)
        {
            string text = txtWhitelist.Text;
            if (text.Length > 0)
            {
                AddDrop(text);
                AddItem(text);
            }
        }

        private void btnWhitelist_Click(object sender, EventArgs e)
        {
            string text = txtWhitelist.Text;
            if (text.Length > 0)
            {
                AddDrop(text);
            }
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            string text = txtSwapBank.Text;
            string text2 = txtSwapInv.Text;
            if (text.Length > 0 && text2.Length > 0)
            {
                AddCommand(new CmdBankSwap
                {
                    InventoryItemName = text2,
                    BankItemName = text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnBoost_Click(object sender, EventArgs e)
        {
            if (cbBoosts.SelectedIndex > -1)
            {
                lstBoosts.Items.Add(cbBoosts.SelectedItem);
            }
        }

        private void cbBoosts_Click(object sender, EventArgs e)
        {
            cbBoosts.Items.Clear();
            DarkComboBox.ObjectCollection items = cbBoosts.Items;
            object[] array = Player.Inventory.Items.Where((InventoryItem i) => i.Category == "ServerUse").ToArray();
            object[] items2 = array;
            items.AddRange(items2);
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (txtShopItem.TextLength > 0)
            {
                AddCommand(new CmdBuy
                {
                    ItemName = txtShopItem.Text,
                    ShopId = (int)numShopId.Value
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnQuestAdd_Click(object sender, EventArgs e)
        {
            AddQuest((int)numQuestID.Value, chkQuestItem.Checked ? numQuestItem.Value.ToString() : null);
        }

        private void btnQuestComplete_Click(object sender, EventArgs e)
        {
            Quest quest = new Quest();
            CmdCompleteQuest cmdCompleteQuest = new CmdCompleteQuest();
            quest.Id = (int)numQuestID.Value;
            if (chkQuestItem.Checked)
            {
                quest.ItemId = numQuestItem.Value.ToString();
            }
            cmdCompleteQuest.Quest = quest;
            AddCommand(cmdCompleteQuest, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnQuestAccept_Click(object sender, EventArgs e)
        {
            Quest quest = new Quest
            {
                Id = (int)numQuestID.Value
            };
            AddCommand(new CmdAcceptQuest
            {
                Quest = quest
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkQuestItem_CheckedChanged(object sender, EventArgs e)
        {
            numQuestItem.Enabled = chkQuestItem.Checked;
        }

        private void btnPacket_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdPacket
            {
                Packet = txtPacket.Text
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnDelay_Click(object sender, EventArgs e)
        {
            int delay = (int)numDelay.Value;
            AddCommand(new CmdDelay
            {
                Delay = delay
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            string text = txtPlayer.Text;
            if (text.Length > 0)
            {
                AddCommand(new CmdGotoPlayer
                {
                    PlayerName = text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnBotDelay_Click(object sender, EventArgs e)
        {
            int delay = (int)numBotDelay.Value;
            AddCommand(new CmdBotDelay
            {
                Delay = delay
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdStop(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdRestart(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load bot";
                openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
                openFileDialog.Filter = "Grimoire bots|*.gbot";
                openFileDialog.DefaultExt = ".gbot";
                if (openFileDialog.ShowDialog() == DialogResult.OK && TryDeserialize(File.ReadAllText(openFileDialog.FileName), out Configuration config))
                {
                    ApplyConfiguration(config);
                }
            }
        }

        private bool TryDeserialize(string json, out Configuration config)
        {
            try
            {
                config = JsonConvert.DeserializeObject<Configuration>(json, _serializerSettings);
                return true;
            }
            catch
            {

            }
            config = null;
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save bot";
                saveFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
                saveFileDialog.DefaultExt = ".gbot";
                saveFileDialog.Filter = "Grimoire bots|*.gbot";
                saveFileDialog.CheckFileExists = false;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Configuration value = GenerateConfiguration();
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(value, Formatting.Indented, _serializerSettings));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to save bot: " + ex.Message);
                    }
                }
            }
        }

        private void btnLoadCmd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string initialDirectory = Path.Combine(Application.StartupPath, "Bots");
                openFileDialog.Title = "Select bot to load";
                openFileDialog.Filter = "Grimoire bots|*.gbot";
                openFileDialog.InitialDirectory = initialDirectory;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    AddCommand(new CmdLoadBot
                    {
                        BotFilePath = Extensions.MakeRelativePathFrom(Application.StartupPath, openFileDialog.FileName), // Path.GetFullPath(openFileDialog.FileName)
                        BotFileName = Path.GetFileName(openFileDialog.FileName)

                    }, (ModifierKeys & Keys.Control) == Keys.Control);
                }
            }
        }

        private void cbStatement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategories.SelectedIndex > -1 && cbStatement.SelectedIndex > -1)
            {
                StatementCommand statementCommand = (StatementCommand)cbStatement.SelectedItem;
                txtStatement1.Enabled = statementCommand.Description1 != null;
                txtStatement2.Enabled = statementCommand.Description2 != null;
                txtStatement1.Text = statementCommand.Description1;
                txtStatement2.Text = statementCommand.Description2;
            }
        }

        private void btnStatementAdd_Click(object sender, EventArgs e)
        {
            if (cbCategories.SelectedIndex > -1 && cbStatement.SelectedIndex > -1)
            {
                string text = txtStatement1.Text;
                string text2 = txtStatement2.Text;
                StatementCommand statementCommand = (StatementCommand)Activator.CreateInstance(cbStatement.SelectedItem.GetType());
                statementCommand.Value1 = text;
                statementCommand.Value2 = text2;
                AddCommand((IBotCommand)statementCommand, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void cbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategories.SelectedIndex > -1)
            {
                cbStatement.Items.Clear();
                string text = cbCategories.SelectedItem.ToString();
                DarkComboBox.ObjectCollection items = cbStatement.Items;
                object[] array = _statementCommands.Where((StatementCommand s) => s.Tag == text).ToArray();
                object[] items2 = array;
                items.AddRange(items2);
            }
        }

        private void btnGotoLabel_Click(object sender, EventArgs e)
        {
            if (txtLabel.TextLength > 0)
            {
                AddCommand(new CmdGotoLabel
                {
                    Label = txtLabel.Text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnAddLabel_Click(object sender, EventArgs e)
        {
            if (txtLabel.TextLength > 0)
            {
                AddCommand(new CmdLabel
                {
                    Name = txtLabel.Text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdLogout(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void UpdateTree()
        {
            if (!string.IsNullOrEmpty(txtSaved.Text) && Directory.Exists(txtSaved.Text))
            {
                lblBots.Text = string.Format("Number of Bots: {0}", Directory.EnumerateFiles(txtSaved.Text, "*.gbot", SearchOption.AllDirectories).Count());
                treeBots.Nodes.Clear();
                AddTreeNodes(treeBots, txtSaved.Text);
            }
        }

        private void treeBots_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string path = Path.Combine(txtSaved.Text, e.Node.FullPath);
            if (File.Exists(path))
            {
                if (!TryDeserialize(File.ReadAllText(path), out Configuration config))
                {
                    return;
                }
                ApplyConfiguration(config);
            }
            lblCommands.Text = $"Number of{Environment.NewLine}Commands: {lstCommands.Items.Count}";
            lblSkills.Text = $"Skills: {lstSkills.Items.Count}";
            lblQuests.Text = $"Quests: {lstQuests.Items.Count}";
            lblDrops.Text = $"Drops: {lstDrops.Items.Count}";
            lblBoosts.Text = $"Boosts: {lstBoosts.Items.Count}";
            lblItems.Text = $"Items: {lstItems.Items.Count}";
        }

        private void treeBots_AfterExpand(object sender, TreeViewEventArgs e)
        {
            string path = Path.Combine(txtSaved.Text, e.Node.FullPath);
            if (Directory.Exists(path))
            {
                AddTreeNodes(e.Node, path);
                if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text == "Loading...")
                {
                    e.Node.Nodes.RemoveAt(0);
                }
            }
        }

        private void AddTreeNodes(TreeNode node, string path)
        {
            foreach (string item in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
            {
                string add = Path.GetFileName(item);
                if (node.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add))
                {
                    node.Nodes.Add(add).Nodes.Add("Loading...");
                }
            }
            foreach (string item2 in Directory.EnumerateFiles(path, "*.gbot", SearchOption.TopDirectoryOnly))
            {
                string add2 = Path.GetFileName(item2);
                if (node.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add2))
                {
                    node.Nodes.Add(add2);
                }
            }
        }

        private void AddTreeNodes(TreeView tree, string path)
        {
            foreach (string item in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
            {
                string add = Path.GetFileName(item);
                if (tree.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add))
                {
                    tree.Nodes.Add(add).Nodes.Add("Loading...");
                }
            }
            foreach (string item2 in Directory.EnumerateFiles(path, "*.gbot", SearchOption.TopDirectoryOnly))
            {
                string add2 = Path.GetFileName(item2);
                if (tree.Nodes.Cast<TreeNode>().ToList().All((TreeNode n) => n.Text != add2))
                {
                    tree.Nodes.Add(add2);
                }
            }
        }

        private void btnSavedAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSaved.Text))
            {
                string path = Path.Combine(txtSaved.Text, txtSavedAdd.Text);
                if (!Directory.Exists(path))
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to create directory: " + ex.Message, "Grimoire", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                UpdateTree();
            }
        }

        private void btnSoundAdd_Click(object sender, EventArgs e)
        {
            if (txtSoundItem.TextLength > 0)
            {
                lstSoundItems.Items.Add(txtSoundItem.Text);
            }
        }

        private void btnSoundDelete_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstSoundItems.SelectedIndex;
            if (selectedIndex > -1)
            {
                lstSoundItems.Items.RemoveAt(selectedIndex);
            }
        }

        private void btnSoundClear_Click(object sender, EventArgs e)
        {
            lstSoundItems.Items.Clear();
        }

        private void btnSoundTest_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Beep();
            }
        }

        private void chkInfiniteRange_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.InfiniteRange = chkInfiniteRange.Checked;
            Root.Instance.infRangeToolStripMenuItem.Checked = chkInfiniteRange.Checked;
        }

        private void chkProvoke_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.ProvokeMonsters = chkProvoke.Checked;
            Root.Instance.provokeToolStripMenuItem1.Checked = chkProvoke.Checked;
        }

        private void chkMagnet_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.EnemyMagnet = chkMagnet.Checked;
            Root.Instance.enemyMagnetToolStripMenuItem.Checked = chkMagnet.Checked;
        }

        private void chkLag_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.LagKiller = chkLag.Checked;
            Root.Instance.lagKillerToolStripMenuItem.Checked = chkLag.Checked;
        }

        private void chkHidePlayers_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.HidePlayers = chkHidePlayers.Checked;
            Root.Instance.hidePlayersToolStripMenuItem.Checked = chkHidePlayers.Checked;
        }

        private void chkSkipCutscenes_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.SkipCutscenes = chkSkipCutscenes.Checked;
            Root.Instance.skipCutscenesToolStripMenuItem.Checked = chkSkipCutscenes.Checked;
        }

        private void numWalkSpeed_ValueChanged(object sender, EventArgs e)
        {
            OptionsManager.WalkSpeed = (int)numWalkSpeed.Value;
        }

        private void chkDisableAnims_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.DisableAnimations = chkDisableAnims.Checked;
            Root.Instance.disableAnimationsToolStripMenuItem.Checked = chkDisableAnims.Checked;
        }

        private void OnOptionsStateChanged(bool state)
        {
            if (InvokeRequired)
            {
                Invoke((Action)delegate
                {
                    chkEnableSettings.Checked = state;
                });
            }
            else
            {
                chkEnableSettings.Checked = state;
            }
        }

        private void chkEnableSettings_Click(object sender, EventArgs e)
        {
            if (chkEnableSettings.Checked)
                OptionsManager.Start();
            else
                OptionsManager.Stop();
        }
        
        private void lstBoxs_KeyPress(object sender, KeyEventArgs e)
        {
            if      (ModifierKeys == Keys.Control && e.KeyCode == Keys.Up)
            {
                _MoveListBoxUp();
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.Down)
            {
                _MoveListBoxDown();
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.Delete)
            {
                btnRemove.PerformClick();
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.D && SelectedList.SelectedIndex > -1)
            {
                var selectedItems = SelectedList.SelectedItems;
                for (int i = 0; selectedItems.Count > i; i++)
                {
                    SelectedList.Items.Insert(SelectedList.SelectedIndex + selectedItems.Count + i, selectedItems[i]);
                }
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.C && SelectedList.SelectedIndex > -1)
            {
                Clipboard.Clear();
                Configuration items = new Configuration {
                    Commands = lstCommands.SelectedItems.Cast<IBotCommand>().ToList(),
                    Skills = lstSkills.SelectedItems.Cast<Skill>().ToList(),
                    Quests = lstQuests.SelectedItems.Cast<Quest>().ToList(),
                    Boosts = lstBoosts.SelectedItems.Cast<InventoryItem>().ToList(),
                    Drops = lstDrops.SelectedItems.Cast<string>().ToList(),
                    Items = lstItems.SelectedItems.Cast<string>().ToList()
                };
                Clipboard.SetText(JsonConvert.SerializeObject(items, Formatting.Indented, _serializerSettings));
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.V)
            {
                TryDeserialize(Clipboard.GetText(), out Configuration config);
                List<IBotCommand> commands = config.Commands;
                if (commands != null && commands.Count > 0)
                {
                    ListBox.ObjectCollection items = lstCommands.Items;
                    object[] array = config.Commands.ToArray();
                    int selectedIndex = lstCommands.SelectedIndex;
                    lstCommands.SelectedIndex = -1;
                    for (int i = 0; array.Count() > i; i++)
                    {
                        items.Insert(selectedIndex + i + 1, array[i]);
                        lstCommands.SelectedIndex = selectedIndex + i + 1;
                    }
                }
                List<Skill> skills = config.Skills;
                if (skills != null && skills.Count > 0)
                {
                    ListBox.ObjectCollection items = lstSkills.Items;
                    object[] array = config.Skills.ToArray();
                    items.AddRange(array);
                }
                List<Quest> quests = config.Quests;
                if (quests != null && quests.Count > 0)
                {
                    ListBox.ObjectCollection items = lstQuests.Items;
                    object[] array = config.Quests.ToArray();
                    items.AddRange(array);
                }
                List<InventoryItem> boosts = config.Boosts;
                if (boosts != null && boosts.Count > 0)
                {
                    ListBox.ObjectCollection items = lstBoosts.Items;
                    object[] array = config.Boosts.ToArray();
                    items.AddRange(array);
                }
                List<string> drops = config.Drops;
                if (drops != null && drops.Count > 0)
                {
                    ListBox.ObjectCollection items = lstDrops.Items;
                    object[] array = config.Drops.ToArray();
                    items.AddRange(array);
                }
                List<string> item = config.Items;
                if (item != null && item.Count > 0)
                {
                    ListBox.ObjectCollection items = lstItems.Items;
                    object[] array = config.Items.ToArray();
                    items.AddRange(array);
                }
                e.Handled = true;
            }
            else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.S)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Save bot";
                    saveFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Bots");
                    saveFileDialog.DefaultExt = ".gbot";
                    saveFileDialog.Filter = "Grimoire bots|*.gbot";
                    saveFileDialog.CheckFileExists = false;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Configuration value = GenerateConfiguration();
                        try
                        {
                            File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(value, Formatting.Indented, _serializerSettings));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to save bot: " + ex.Message);
                        }
                    }
                }
                e.Handled = true;
            }
        }

        private void AddCommand(IBotCommand cmd, bool Insert)
        {
            if (Insert)
            {
                lstCommands.Items.Insert((lstCommands.SelectedIndex > -1) ? lstCommands.SelectedIndex : lstCommands.Items.Count, cmd);
            }
            else
            {
                lstCommands.Items.Add(cmd);
            }
        }

        private void AddSkill(Skill skill, bool Insert)
        {
            if (Insert)
            {
                lstSkills.Items.Insert((lstSkills.SelectedIndex > -1) ? lstSkills.SelectedIndex : lstSkills.Items.Count, skill);
            }
            else
            {
                lstSkills.Items.Add(skill);
            }
        }

        public void btnBotStart_ClickAsync(object sender, EventArgs e)
        {
            if (Player.IsAlive && Player.IsLoggedIn && lstCommands.Items.Count > 0)
            {
                this.lstCommands.SelectionMode = SelectionMode.One;
                this.lstItems.SelectionMode = SelectionMode.One;
                this.lstSkills.SelectionMode = SelectionMode.One;
                this.lstQuests.SelectionMode = SelectionMode.One;
                this.lstDrops.SelectionMode = SelectionMode.One;
                this.lstBoosts.SelectionMode = SelectionMode.One;
                ActiveBotEngine.IsRunningChanged += OnIsRunningChanged;
                ActiveBotEngine.IndexChanged += OnIndexChanged;
                ActiveBotEngine.ConfigurationChanged += OnConfigurationChanged;
                ActiveBotEngine.Start(GenerateConfiguration());
                BotStateChanged(IsRunning: true);
                Root.Instance.BotStateChanged(IsRunning: true);
            }
        }

        public async void btnBotStop_Click(object sender, EventArgs e)
        {
            if (lstItems != null && this.chkBankOnStop.Checked)
            {
                foreach (InventoryItem item in Player.Inventory.Items)
                {
                    if (!item.IsEquipped && item.IsAcItem && item.Category != "Class" && item.Name.ToLower() != "treasure potion" && lstItems.Items.Contains(item.Name))
                    {
                        Player.Bank.TransferToBank(item.Name);
                        await Task.Delay(70);
                        LogForm.Instance.AppendDebug("Transferred to Bank: " + item.Name + "\r\n");
                    }
                }
                LogForm.Instance.AppendDebug("Banked all AC Items in Items list \r\n");
            }
            btnBotStart.Enabled = false;
            ActiveBotEngine.Stop();
            this.lstCommands.SelectionMode = SelectionMode.MultiExtended;
            this.lstItems.SelectionMode = SelectionMode.MultiExtended;
            this.lstSkills.SelectionMode = SelectionMode.MultiExtended;
            this.lstQuests.SelectionMode = SelectionMode.MultiExtended;
            this.lstDrops.SelectionMode = SelectionMode.MultiExtended;
            this.lstBoosts.SelectionMode = SelectionMode.MultiExtended;
            await Task.Delay(2000);
            BotStateChanged(IsRunning: false);
            Root.Instance.BotStateChanged(IsRunning: false);
            btnBotStart.Enabled = true;
        }

        public void BotStateChanged(bool IsRunning)
        {
            if (IsRunning)
            {
                btnBotStart.Hide();
                btnBotStop.Show();
            }
            else
            {
                btnBotStop.Hide();
                btnBotStart.Show();
            }
            btnUp.Enabled = !IsRunning;
            btnDown.Enabled = !IsRunning;
            btnClear.Enabled = !IsRunning;
            btnRemove.Enabled = !IsRunning;
        }

        public void AddCommand(IBotCommand cmd)
        {
            lstCommands.Items.Add(cmd);
        }

        public void AddQuest(int QuestID, string ItemID = null)
        {
            Quest quest = new Quest
            {
                Id = QuestID,
                ItemId = ItemID
            };
            quest.Text = (quest.ItemId != null) ? $"{quest.Id}:{quest.ItemId}" : quest.Id.ToString();
            if (!lstQuests.Items.Contains(quest))
            {
                lstQuests.Items.Add(quest);
            }
        }

        public void AddDrop(string Name)
        {
            if (!lstDrops.Items.Contains(Name))
            {
                lstDrops.Items.Add(Name);
            }
        }

        private void btnAddSkillSet_Click(object sender, EventArgs e)
        {
            if (txtSkillSet.TextLength > 0)
            {
                AddSkill(new Skill
                {
                    Text = "[" + txtSkillSet.Text.ToUpper() + "]",
                    Type = Skill.SkillType.Label
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnUseSkillSet_Click(object sender, EventArgs e)
        {
            if (txtSkillSet.TextLength > 0)
            {
                AddCommand(new CmdSkillSet
                {
                    Name = txtSkillSet.Text.ToUpper()
                }, (ModifierKeys & Keys.Control) == Keys.Control);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BotManager));
            this.lstCommands = new System.Windows.Forms.ListBox();
            this.lstBoosts = new System.Windows.Forms.ListBox();
            this.lstDrops = new System.Windows.Forms.ListBox();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.lstQuests = new System.Windows.Forms.ListBox();
            this.lstSkills = new System.Windows.Forms.ListBox();
            this.tabControl1 = new FlatTabControl.FlatTabControl();
            this.tabCombat = new System.Windows.Forms.TabPage();
            this.pnlCombat = new System.Windows.Forms.Panel();
            this.colorfulCommands = new DarkUI.Controls.DarkCheckBox();
            this.btnUseSkillSet = new DarkUI.Controls.DarkButton();
            this.btnAddSkillSet = new DarkUI.Controls.DarkButton();
            this.txtSkillSet = new DarkUI.Controls.DarkTextBox();
            this.chkSafeMp = new DarkUI.Controls.DarkCheckBox();
            this.label17 = new DarkUI.Controls.DarkLabel();
            this.label16 = new DarkUI.Controls.DarkLabel();
            this.btnRest = new DarkUI.Controls.DarkButton();
            this.btnRestF = new DarkUI.Controls.DarkButton();
            this.chkSkillCD = new DarkUI.Controls.DarkCheckBox();
            this.label12 = new DarkUI.Controls.DarkLabel();
            this.label11 = new DarkUI.Controls.DarkLabel();
            this.label10 = new DarkUI.Controls.DarkLabel();
            this.btnAttack = new DarkUI.Controls.DarkButton();
            this.btnKill = new DarkUI.Controls.DarkButton();
            this.label13 = new DarkUI.Controls.DarkLabel();
            this.chkExistQuest = new DarkUI.Controls.DarkCheckBox();
            this.numRestMP = new DarkUI.Controls.DarkNumericUpDown();
            this.chkMP = new DarkUI.Controls.DarkCheckBox();
            this.numRest = new DarkUI.Controls.DarkNumericUpDown();
            this.chkHP = new DarkUI.Controls.DarkCheckBox();
            this.chkPacket = new DarkUI.Controls.DarkCheckBox();
            this.numSkillD = new DarkUI.Controls.DarkNumericUpDown();
            this.label2 = new DarkUI.Controls.DarkLabel();
            this.numSafe = new DarkUI.Controls.DarkNumericUpDown();
            this.btnAddSafe = new DarkUI.Controls.DarkButton();
            this.btnSkillCmd = new DarkUI.Controls.DarkButton();
            this.btnAddSkill = new DarkUI.Controls.DarkButton();
            this.numSkill = new DarkUI.Controls.DarkNumericUpDown();
            this.chkExitRest = new DarkUI.Controls.DarkCheckBox();
            this.chkAllSkillsCD = new DarkUI.Controls.DarkCheckBox();
            this.txtKillFQ = new DarkUI.Controls.DarkTextBox();
            this.txtKillFItem = new DarkUI.Controls.DarkTextBox();
            this.txtKillFMon = new DarkUI.Controls.DarkTextBox();
            this.rbTemp = new System.Windows.Forms.RadioButton();
            this.rbItems = new System.Windows.Forms.RadioButton();
            this.btnKillF = new DarkUI.Controls.DarkButton();
            this.txtMonster = new DarkUI.Controls.DarkTextBox();
            this.tabItem = new System.Windows.Forms.TabPage();
            this.pnlItem = new System.Windows.Forms.Panel();
            this.chkPickupAcTag = new DarkUI.Controls.DarkCheckBox();
            this.btnWhitelistOff = new DarkUI.Controls.DarkButton();
            this.btnWhitelistOn = new DarkUI.Controls.DarkButton();
            this.btnWhitelistToggle = new DarkUI.Controls.DarkButton();
            this.label20 = new DarkUI.Controls.DarkLabel();
            this.label1 = new DarkUI.Controls.DarkLabel();
            this.numDropDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.chkRejectAll = new DarkUI.Controls.DarkCheckBox();
            this.chkPickupAll = new DarkUI.Controls.DarkCheckBox();
            this.chkBankOnStop = new DarkUI.Controls.DarkCheckBox();
            this.txtShopItem = new DarkUI.Controls.DarkTextBox();
            this.numShopId = new DarkUI.Controls.DarkNumericUpDown();
            this.label15 = new DarkUI.Controls.DarkLabel();
            this.btnBuy = new DarkUI.Controls.DarkButton();
            this.btnBuyFast = new DarkUI.Controls.DarkButton();
            this.btnLoadShop = new DarkUI.Controls.DarkButton();
            this.btnBoost = new DarkUI.Controls.DarkButton();
            this.cbBoosts = new DarkUI.Controls.DarkComboBox();
            this.numMapItem = new DarkUI.Controls.DarkNumericUpDown();
            this.btnMapItem = new DarkUI.Controls.DarkButton();
            this.btnSwap = new DarkUI.Controls.DarkButton();
            this.txtSwapInv = new DarkUI.Controls.DarkTextBox();
            this.txtSwapBank = new DarkUI.Controls.DarkTextBox();
            this.chkReject = new DarkUI.Controls.DarkCheckBox();
            this.chkPickup = new DarkUI.Controls.DarkCheckBox();
            this.btnWhitelist = new DarkUI.Controls.DarkButton();
            this.btnBoth = new DarkUI.Controls.DarkButton();
            this.txtWhitelist = new DarkUI.Controls.DarkTextBox();
            this.btnItem = new DarkUI.Controls.DarkButton();
            this.btnUnbanklst = new DarkUI.Controls.DarkButton();
            this.txtItem = new DarkUI.Controls.DarkTextBox();
            this.cbItemCmds = new DarkUI.Controls.DarkComboBox();
            this.tabMap = new System.Windows.Forms.TabPage();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.btnCurrBlank = new DarkUI.Controls.DarkButton();
            this.btnSetSpawn = new DarkUI.Controls.DarkButton();
            this.btnWalkRdm = new DarkUI.Controls.DarkButton();
            this.btnWalkCur = new DarkUI.Controls.DarkButton();
            this.btnWalk = new DarkUI.Controls.DarkButton();
            this.numWalkY = new DarkUI.Controls.DarkNumericUpDown();
            this.numWalkX = new DarkUI.Controls.DarkNumericUpDown();
            this.button2 = new DarkUI.Controls.DarkButton();
            this.btnCellSwap = new DarkUI.Controls.DarkButton();
            this.btnJump = new DarkUI.Controls.DarkButton();
            this.btnCurrCell = new DarkUI.Controls.DarkButton();
            this.txtPad = new DarkUI.Controls.DarkTextBox();
            this.txtCell = new DarkUI.Controls.DarkTextBox();
            this.btnJoin = new DarkUI.Controls.DarkButton();
            this.txtJoinPad = new DarkUI.Controls.DarkTextBox();
            this.txtJoinCell = new DarkUI.Controls.DarkTextBox();
            this.txtJoin = new DarkUI.Controls.DarkTextBox();
            this.tabQuest = new System.Windows.Forms.TabPage();
            this.pnlQuest = new System.Windows.Forms.Panel();
            this.label14 = new DarkUI.Controls.DarkLabel();
            this.numEnsureTries = new DarkUI.Controls.DarkNumericUpDown();
            this.chkEnsureComplete = new DarkUI.Controls.DarkCheckBox();
            this.btnQuestAccept = new DarkUI.Controls.DarkButton();
            this.btnQuestComplete = new DarkUI.Controls.DarkButton();
            this.btnQuestAdd = new DarkUI.Controls.DarkButton();
            this.numQuestItem = new DarkUI.Controls.DarkNumericUpDown();
            this.chkQuestItem = new DarkUI.Controls.DarkCheckBox();
            this.numQuestID = new DarkUI.Controls.DarkNumericUpDown();
            this.label4 = new DarkUI.Controls.DarkLabel();
            this.tabMisc = new System.Windows.Forms.TabPage();
            this.pnlMisc = new System.Windows.Forms.Panel();
            this.label19 = new DarkUI.Controls.DarkLabel();
            this.btnDecreaseInt = new DarkUI.Controls.DarkButton();
            this.btnIncreaseInt = new DarkUI.Controls.DarkButton();
            this.numSetInt = new DarkUI.Controls.DarkNumericUpDown();
            this.txtSetInt = new DarkUI.Controls.DarkTextBox();
            this.btnSetInt = new DarkUI.Controls.DarkButton();
            this.label18 = new DarkUI.Controls.DarkLabel();
            this.btnGoDownIndex = new DarkUI.Controls.DarkButton();
            this.btnGoUpIndex = new DarkUI.Controls.DarkButton();
            this.btnGotoIndex = new DarkUI.Controls.DarkButton();
            this.numIndexCmd = new DarkUI.Controls.DarkNumericUpDown();
            this.btnBlank = new DarkUI.Controls.DarkButton();
            this.btnProvokeOff = new DarkUI.Controls.DarkButton();
            this.btnProvokeOn = new DarkUI.Controls.DarkButton();
            this.btnProvoke = new DarkUI.Controls.DarkButton();
            this.labelProvoke = new DarkUI.Controls.DarkLabel();
            this.chkRestartDeath = new DarkUI.Controls.DarkCheckBox();
            this.chkMerge = new DarkUI.Controls.DarkCheckBox();
            this.btnLogout = new DarkUI.Controls.DarkButton();
            this.txtLabel = new DarkUI.Controls.DarkTextBox();
            this.btnGotoLabel = new DarkUI.Controls.DarkButton();
            this.btnAddLabel = new DarkUI.Controls.DarkButton();
            this.txtDescription = new DarkUI.Controls.DarkTextBox();
            this.txtAuthor = new DarkUI.Controls.DarkTextBox();
            this.btnSave = new DarkUI.Controls.DarkButton();
            this.btnBeep = new DarkUI.Controls.DarkButton();
            this.numBeepTimes = new DarkUI.Controls.DarkNumericUpDown();
            this.btnDelay = new DarkUI.Controls.DarkButton();
            this.numDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.label3 = new DarkUI.Controls.DarkLabel();
            this.numBotDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.btnBotDelay = new DarkUI.Controls.DarkButton();
            this.txtPlayer = new DarkUI.Controls.DarkTextBox();
            this.btnGoto = new DarkUI.Controls.DarkButton();
            this.btnLoad = new DarkUI.Controls.DarkButton();
            this.btnRestart = new DarkUI.Controls.DarkButton();
            this.btnStop = new DarkUI.Controls.DarkButton();
            this.btnLoadCmd = new DarkUI.Controls.DarkButton();
            this.chkSkip = new DarkUI.Controls.DarkCheckBox();
            this.btnStatementAdd = new DarkUI.Controls.DarkButton();
            this.txtStatement2 = new DarkUI.Controls.DarkTextBox();
            this.txtStatement1 = new DarkUI.Controls.DarkTextBox();
            this.cbStatement = new DarkUI.Controls.DarkComboBox();
            this.cbCategories = new DarkUI.Controls.DarkComboBox();
            this.txtPacket = new DarkUI.Controls.DarkTextBox();
            this.btnClientPacket = new DarkUI.Controls.DarkButton();
            this.btnPacket = new DarkUI.Controls.DarkButton();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.chkBuff = new DarkUI.Controls.DarkCheckBox();
            this.label6 = new DarkUI.Controls.DarkLabel();
            this.numOptionsTimer = new DarkUI.Controls.DarkNumericUpDown();
            this.label5 = new DarkUI.Controls.DarkLabel();
            this.chkUntarget = new DarkUI.Controls.DarkCheckBox();
            this.lstLogText = new System.Windows.Forms.ListBox();
            this.btnLogDebug = new DarkUI.Controls.DarkButton();
            this.btnLog = new DarkUI.Controls.DarkButton();
            this.txtLog = new DarkUI.Controls.DarkTextBox();
            this.chkEnableSettings = new DarkUI.Controls.DarkCheckBox();
            this.chkDisableAnims = new DarkUI.Controls.DarkCheckBox();
            this.txtSoundItem = new DarkUI.Controls.DarkTextBox();
            this.btnSoundAdd = new DarkUI.Controls.DarkButton();
            this.btnSoundDelete = new DarkUI.Controls.DarkButton();
            this.btnSoundTest = new DarkUI.Controls.DarkButton();
            this.lstSoundItems = new System.Windows.Forms.ListBox();
            this.label9 = new DarkUI.Controls.DarkLabel();
            this.numWalkSpeed = new DarkUI.Controls.DarkNumericUpDown();
            this.label8 = new DarkUI.Controls.DarkLabel();
            this.chkSkipCutscenes = new DarkUI.Controls.DarkCheckBox();
            this.chkHidePlayers = new DarkUI.Controls.DarkCheckBox();
            this.chkLag = new DarkUI.Controls.DarkCheckBox();
            this.chkMagnet = new DarkUI.Controls.DarkCheckBox();
            this.chkProvoke = new DarkUI.Controls.DarkCheckBox();
            this.chkInfiniteRange = new DarkUI.Controls.DarkCheckBox();
            this.grpLogin = new DarkUI.Controls.DarkGroupBox();
            this.chkAFK = new DarkUI.Controls.DarkCheckBox();
            this.cbServers = new DarkUI.Controls.DarkComboBox();
            this.chkRelogRetry = new DarkUI.Controls.DarkCheckBox();
            this.chkRelog = new DarkUI.Controls.DarkCheckBox();
            this.numRelogDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.label7 = new DarkUI.Controls.DarkLabel();
            this.chkGender = new DarkUI.Controls.DarkCheckBox();
            this.tabOptions2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new DarkUI.Controls.DarkGroupBox();
            this.btnAddInfoMsg = new DarkUI.Controls.DarkButton();
            this.btnAddWarnMsg = new DarkUI.Controls.DarkButton();
            this.inputMsgClient = new DarkUI.Controls.DarkTextBox();
            this.btnSearchCmd = new DarkUI.Controls.DarkButton();
            this.txtSearchCmd = new DarkUI.Controls.DarkTextBox();
            this.numSetLevel = new DarkUI.Controls.DarkNumericUpDown();
            this.chkChangeRoomTag = new DarkUI.Controls.DarkCheckBox();
            this.chkChangeChat = new DarkUI.Controls.DarkCheckBox();
            this.chkSetJoinLevel = new DarkUI.Controls.DarkCheckBox();
            this.chkHideYulgarPlayers = new DarkUI.Controls.DarkCheckBox();
            this.chkAntiAfk = new DarkUI.Controls.DarkCheckBox();
            this.grpAccessLevel = new DarkUI.Controls.DarkGroupBox();
            this.chkToggleMute = new DarkUI.Controls.DarkCheckBox();
            this.btnSetMem = new DarkUI.Controls.DarkButton();
            this.btnSetModerator = new DarkUI.Controls.DarkButton();
            this.btnSetNonMem = new DarkUI.Controls.DarkButton();
            this.grpAlignment = new DarkUI.Controls.DarkGroupBox();
            this.btnSetChaos = new DarkUI.Controls.DarkButton();
            this.btnSetUndecided = new DarkUI.Controls.DarkButton();
            this.btnSetGood = new DarkUI.Controls.DarkButton();
            this.btnSetEvil = new DarkUI.Controls.DarkButton();
            this.txtUsername = new DarkUI.Controls.DarkTextBox();
            this.btnChangeNameCmd = new DarkUI.Controls.DarkButton();
            this.btnchangeName = new DarkUI.Controls.DarkButton();
            this.btnChangeGuildCmd = new DarkUI.Controls.DarkButton();
            this.btnchangeGuild = new DarkUI.Controls.DarkButton();
            this.txtGuild = new DarkUI.Controls.DarkTextBox();
            this.tabBots = new System.Windows.Forms.TabPage();
            this.pnlSaved = new System.Windows.Forms.Panel();
            this.lblBoosts = new DarkUI.Controls.DarkLabel();
            this.lblDrops = new DarkUI.Controls.DarkLabel();
            this.lblQuests = new DarkUI.Controls.DarkLabel();
            this.lblSkills = new DarkUI.Controls.DarkLabel();
            this.lblCommands = new DarkUI.Controls.DarkLabel();
            this.lblItems = new DarkUI.Controls.DarkLabel();
            this.txtSavedDesc = new DarkUI.Controls.DarkTextBox();
            this.txtSavedAuthor = new DarkUI.Controls.DarkTextBox();
            this.lblBots = new DarkUI.Controls.DarkLabel();
            this.treeBots = new System.Windows.Forms.TreeView();
            this.txtSavedAdd = new DarkUI.Controls.DarkTextBox();
            this.btnSavedAdd = new DarkUI.Controls.DarkButton();
            this.txtSaved = new DarkUI.Controls.DarkTextBox();
            this.tabMisc2 = new System.Windows.Forms.TabPage();
            this.numDelayedPacket = new DarkUI.Controls.DarkNumericUpDown();
            this.btnDelayedPacket = new DarkUI.Controls.DarkButton();
            this.numSpammerDelay = new DarkUI.Controls.DarkNumericUpDown();
            this.btnSpammerRefresh = new DarkUI.Controls.DarkButton();
            this.txtPacketSpammer = new DarkUI.Controls.DarkTextBox();
            this.btnSpammerStop = new DarkUI.Controls.DarkButton();
            this.btnSpammerStart = new DarkUI.Controls.DarkButton();
            this.btnSpammerRemove = new DarkUI.Controls.DarkButton();
            this.btnSpammerSetDelay = new DarkUI.Controls.DarkButton();
            this.btnSpammerReset = new DarkUI.Controls.DarkButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDown = new DarkUI.Controls.DarkButton();
            this.cbLists = new DarkUI.Controls.DarkComboBox();
            this.chkAll = new DarkUI.Controls.DarkCheckBox();
            this.btnClear = new DarkUI.Controls.DarkButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnRemove = new DarkUI.Controls.DarkButton();
            this.btnBotStart = new DarkUI.Controls.DarkButton();
            this.btnBotStop = new DarkUI.Controls.DarkButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUp = new DarkUI.Controls.DarkButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.checkBox1 = new DarkUI.Controls.DarkCheckBox();
            this.chkBuffup = new DarkUI.Controls.DarkCheckBox();
            this.BotManagerMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeFontsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multilineToggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleTabpagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSpammerAdd = new DarkUI.Controls.DarkButton();
            this.tabControl1.SuspendLayout();
            this.tabCombat.SuspendLayout();
            this.pnlCombat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRestMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSkillD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSafe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSkill)).BeginInit();
            this.tabItem.SuspendLayout();
            this.pnlItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDropDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShopId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMapItem)).BeginInit();
            this.tabMap.SuspendLayout();
            this.pnlMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkX)).BeginInit();
            this.tabQuest.SuspendLayout();
            this.pnlQuest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEnsureTries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuestItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuestID)).BeginInit();
            this.tabMisc.SuspendLayout();
            this.pnlMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSetInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIndexCmd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBeepTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBotDelay)).BeginInit();
            this.tabOptions.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOptionsTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkSpeed)).BeginInit();
            this.grpLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRelogDelay)).BeginInit();
            this.tabOptions2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSetLevel)).BeginInit();
            this.grpAccessLevel.SuspendLayout();
            this.grpAlignment.SuspendLayout();
            this.tabBots.SuspendLayout();
            this.pnlSaved.SuspendLayout();
            this.tabMisc2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayedPacket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpammerDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.BotManagerMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstCommands
            // 
            this.lstCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCommands.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstCommands.FormattingEnabled = true;
            this.lstCommands.HorizontalScrollbar = true;
            this.lstCommands.IntegralHeight = false;
            this.lstCommands.ItemHeight = 15;
            this.lstCommands.Location = new System.Drawing.Point(0, 0);
            this.lstCommands.Name = "lstCommands";
            this.lstCommands.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstCommands.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstCommands.Size = new System.Drawing.Size(238, 251);
            this.lstCommands.TabIndex = 1;
            this.lstCommands.Click += new System.EventHandler(this.lstCommands_Click);
            this.lstCommands.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCommands_DrawItem);
            this.lstCommands.DoubleClick += new System.EventHandler(this.lstCommands_DoubleClick);
            this.lstCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            this.lstCommands.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstCommands_KeyPress);
            this.lstCommands.MouseEnter += new System.EventHandler(this.lstCommands_MouseEnter);
            this.lstCommands.MouseLeave += new System.EventHandler(this.lstCommands_MouseLeave);
            // 
            // lstBoosts
            // 
            this.lstBoosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBoosts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstBoosts.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lstBoosts.FormattingEnabled = true;
            this.lstBoosts.HorizontalScrollbar = true;
            this.lstBoosts.Location = new System.Drawing.Point(0, 0);
            this.lstBoosts.Name = "lstBoosts";
            this.lstBoosts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBoosts.Size = new System.Drawing.Size(238, 251);
            this.lstBoosts.TabIndex = 25;
            this.lstBoosts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            // 
            // lstDrops
            // 
            this.lstDrops.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstDrops.FormattingEnabled = true;
            this.lstDrops.HorizontalScrollbar = true;
            this.lstDrops.Location = new System.Drawing.Point(0, 0);
            this.lstDrops.Name = "lstDrops";
            this.lstDrops.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstDrops.Size = new System.Drawing.Size(238, 251);
            this.lstDrops.TabIndex = 26;
            this.lstDrops.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            // 
            // lstItems
            // 
            this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstItems.FormattingEnabled = true;
            this.lstItems.HorizontalScrollbar = true;
            this.lstItems.Location = new System.Drawing.Point(0, 0);
            this.lstItems.Name = "lstItems";
            this.lstItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstItems.Size = new System.Drawing.Size(238, 251);
            this.lstItems.TabIndex = 145;
            this.lstItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            // 
            // lstQuests
            // 
            this.lstQuests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstQuests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstQuests.FormattingEnabled = true;
            this.lstQuests.HorizontalScrollbar = true;
            this.lstQuests.Location = new System.Drawing.Point(0, 0);
            this.lstQuests.Name = "lstQuests";
            this.lstQuests.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstQuests.Size = new System.Drawing.Size(238, 251);
            this.lstQuests.TabIndex = 27;
            this.lstQuests.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            // 
            // lstSkills
            // 
            this.lstSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSkills.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstSkills.FormattingEnabled = true;
            this.lstSkills.HorizontalScrollbar = true;
            this.lstSkills.Location = new System.Drawing.Point(0, 0);
            this.lstSkills.Name = "lstSkills";
            this.lstSkills.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSkills.Size = new System.Drawing.Size(238, 251);
            this.lstSkills.TabIndex = 28;
            this.lstSkills.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCombat);
            this.tabControl1.Controls.Add(this.tabItem);
            this.tabControl1.Controls.Add(this.tabMap);
            this.tabControl1.Controls.Add(this.tabQuest);
            this.tabControl1.Controls.Add(this.tabMisc);
            this.tabControl1.Controls.Add(this.tabMisc2);
            this.tabControl1.Controls.Add(this.tabOptions);
            this.tabControl1.Controls.Add(this.tabOptions2);
            this.tabControl1.Controls.Add(this.tabBots);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(3, 2);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(452, 325);
            this.tabControl1.TabIndex = 146;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabCombat
            // 
            this.tabCombat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabCombat.Controls.Add(this.pnlCombat);
            this.tabCombat.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabCombat.Location = new System.Drawing.Point(4, 23);
            this.tabCombat.Margin = new System.Windows.Forms.Padding(0);
            this.tabCombat.Name = "tabCombat";
            this.tabCombat.Padding = new System.Windows.Forms.Padding(3);
            this.tabCombat.Size = new System.Drawing.Size(444, 298);
            this.tabCombat.TabIndex = 0;
            this.tabCombat.Text = "Combat";
            // 
            // pnlCombat
            // 
            this.pnlCombat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pnlCombat.Controls.Add(this.colorfulCommands);
            this.pnlCombat.Controls.Add(this.btnUseSkillSet);
            this.pnlCombat.Controls.Add(this.btnAddSkillSet);
            this.pnlCombat.Controls.Add(this.txtSkillSet);
            this.pnlCombat.Controls.Add(this.chkSafeMp);
            this.pnlCombat.Controls.Add(this.label17);
            this.pnlCombat.Controls.Add(this.label16);
            this.pnlCombat.Controls.Add(this.btnRest);
            this.pnlCombat.Controls.Add(this.btnRestF);
            this.pnlCombat.Controls.Add(this.chkSkillCD);
            this.pnlCombat.Controls.Add(this.label12);
            this.pnlCombat.Controls.Add(this.label11);
            this.pnlCombat.Controls.Add(this.label10);
            this.pnlCombat.Controls.Add(this.btnAttack);
            this.pnlCombat.Controls.Add(this.btnKill);
            this.pnlCombat.Controls.Add(this.label13);
            this.pnlCombat.Controls.Add(this.chkExistQuest);
            this.pnlCombat.Controls.Add(this.numRestMP);
            this.pnlCombat.Controls.Add(this.chkMP);
            this.pnlCombat.Controls.Add(this.numRest);
            this.pnlCombat.Controls.Add(this.chkHP);
            this.pnlCombat.Controls.Add(this.chkPacket);
            this.pnlCombat.Controls.Add(this.numSkillD);
            this.pnlCombat.Controls.Add(this.label2);
            this.pnlCombat.Controls.Add(this.numSafe);
            this.pnlCombat.Controls.Add(this.btnAddSafe);
            this.pnlCombat.Controls.Add(this.btnSkillCmd);
            this.pnlCombat.Controls.Add(this.btnAddSkill);
            this.pnlCombat.Controls.Add(this.numSkill);
            this.pnlCombat.Controls.Add(this.chkExitRest);
            this.pnlCombat.Controls.Add(this.chkAllSkillsCD);
            this.pnlCombat.Controls.Add(this.txtKillFQ);
            this.pnlCombat.Controls.Add(this.txtKillFItem);
            this.pnlCombat.Controls.Add(this.txtKillFMon);
            this.pnlCombat.Controls.Add(this.rbTemp);
            this.pnlCombat.Controls.Add(this.rbItems);
            this.pnlCombat.Controls.Add(this.btnKillF);
            this.pnlCombat.Controls.Add(this.txtMonster);
            this.pnlCombat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCombat.Location = new System.Drawing.Point(3, 3);
            this.pnlCombat.Name = "pnlCombat";
            this.pnlCombat.Size = new System.Drawing.Size(438, 292);
            this.pnlCombat.TabIndex = 103;
            // 
            // colorfulCommands
            // 
            this.colorfulCommands.Checked = true;
            this.colorfulCommands.CheckState = System.Windows.Forms.CheckState.Checked;
            this.colorfulCommands.Location = new System.Drawing.Point(4, 250);
            this.colorfulCommands.Name = "colorfulCommands";
            this.colorfulCommands.Size = new System.Drawing.Size(240, 30);
            this.colorfulCommands.TabIndex = 66;
            this.colorfulCommands.Text = "Enable custom commands (CPU intensive)\r\n";
            // 
            // btnUseSkillSet
            // 
            this.btnUseSkillSet.Checked = false;
            this.btnUseSkillSet.Location = new System.Drawing.Point(227, 53);
            this.btnUseSkillSet.Name = "btnUseSkillSet";
            this.btnUseSkillSet.Size = new System.Drawing.Size(115, 22);
            this.btnUseSkillSet.TabIndex = 65;
            this.btnUseSkillSet.Text = "Use Skill Set";
            this.btnUseSkillSet.Click += new System.EventHandler(this.btnUseSkillSet_Click);
            // 
            // btnAddSkillSet
            // 
            this.btnAddSkillSet.Checked = false;
            this.btnAddSkillSet.Location = new System.Drawing.Point(227, 29);
            this.btnAddSkillSet.Name = "btnAddSkillSet";
            this.btnAddSkillSet.Size = new System.Drawing.Size(115, 22);
            this.btnAddSkillSet.TabIndex = 64;
            this.btnAddSkillSet.Text = "Add Skill Set";
            this.btnAddSkillSet.Click += new System.EventHandler(this.btnAddSkillSet_Click);
            // 
            // txtSkillSet
            // 
            this.txtSkillSet.Location = new System.Drawing.Point(227, 6);
            this.txtSkillSet.Name = "txtSkillSet";
            this.txtSkillSet.Size = new System.Drawing.Size(115, 20);
            this.txtSkillSet.TabIndex = 63;
            this.txtSkillSet.Text = "Skill Set Name";
            this.txtSkillSet.Click += new System.EventHandler(this.TextboxEnter);
            this.txtSkillSet.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtSkillSet.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // chkSafeMp
            // 
            this.chkSafeMp.AutoSize = true;
            this.chkSafeMp.Location = new System.Drawing.Point(170, 84);
            this.chkSafeMp.Name = "chkSafeMp";
            this.chkSafeMp.Size = new System.Drawing.Size(42, 17);
            this.chkSafeMp.TabIndex = 58;
            this.chkSafeMp.Text = "MP";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label17.Location = new System.Drawing.Point(137, 85);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(34, 13);
            this.label17.TabIndex = 62;
            this.label17.Text = "HP or";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label16.Location = new System.Drawing.Point(137, 101);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 61;
            this.label16.Text = "is less than";
            // 
            // btnRest
            // 
            this.btnRest.Checked = false;
            this.btnRest.Location = new System.Drawing.Point(224, 101);
            this.btnRest.Name = "btnRest";
            this.btnRest.Size = new System.Drawing.Size(44, 22);
            this.btnRest.TabIndex = 43;
            this.btnRest.Text = "Rest";
            this.btnRest.Click += new System.EventHandler(this.btnRest_Click);
            // 
            // btnRestF
            // 
            this.btnRestF.Checked = false;
            this.btnRestF.Location = new System.Drawing.Point(271, 101);
            this.btnRestF.Name = "btnRestF";
            this.btnRestF.Size = new System.Drawing.Size(71, 22);
            this.btnRestF.TabIndex = 44;
            this.btnRestF.Text = "Rest fully";
            this.btnRestF.Click += new System.EventHandler(this.btnRestF_Click);
            // 
            // chkSkillCD
            // 
            this.chkSkillCD.AutoSize = true;
            this.chkSkillCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.chkSkillCD.Location = new System.Drawing.Point(172, 202);
            this.chkSkillCD.Name = "chkSkillCD";
            this.chkSkillCD.Size = new System.Drawing.Size(144, 17);
            this.chkSkillCD.TabIndex = 60;
            this.chkSkillCD.Text = "Wait for skill to cooldown";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label12.Location = new System.Drawing.Point(222, 124);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 57;
            this.label12.Text = "Rest if";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label11.Location = new System.Drawing.Point(327, 156);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 56;
            this.label11.Text = "%";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label10.Location = new System.Drawing.Point(327, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 55;
            this.label10.Text = "%";
            // 
            // btnAttack
            // 
            this.btnAttack.Checked = false;
            this.btnAttack.Location = new System.Drawing.Point(66, 29);
            this.btnAttack.Name = "btnAttack";
            this.btnAttack.Size = new System.Drawing.Size(62, 22);
            this.btnAttack.TabIndex = 54;
            this.btnAttack.Text = "Attack";
            this.btnAttack.Click += new System.EventHandler(this.btnAttack_Click);
            // 
            // btnKill
            // 
            this.btnKill.Checked = false;
            this.btnKill.Location = new System.Drawing.Point(4, 29);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(62, 22);
            this.btnKill.TabIndex = 54;
            this.btnKill.Text = "Kill";
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label13.Location = new System.Drawing.Point(134, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 13);
            this.label13.TabIndex = 53;
            this.label13.Text = "Skill delay";
            // 
            // chkExistQuest
            // 
            this.chkExistQuest.AutoSize = true;
            this.chkExistQuest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.chkExistQuest.Location = new System.Drawing.Point(172, 227);
            this.chkExistQuest.Name = "chkExistQuest";
            this.chkExistQuest.Size = new System.Drawing.Size(197, 17);
            this.chkExistQuest.TabIndex = 51;
            this.chkExistQuest.Text = "Exit combat before completing quest";
            // 
            // numRestMP
            // 
            this.numRestMP.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numRestMP.Location = new System.Drawing.Point(291, 154);
            this.numRestMP.LoopValues = false;
            this.numRestMP.Name = "numRestMP";
            this.numRestMP.Size = new System.Drawing.Size(34, 20);
            this.numRestMP.TabIndex = 50;
            this.numRestMP.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // chkMP
            // 
            this.chkMP.AutoSize = true;
            this.chkMP.Location = new System.Drawing.Point(226, 157);
            this.chkMP.Name = "chkMP";
            this.chkMP.Size = new System.Drawing.Size(57, 17);
            this.chkMP.TabIndex = 49;
            this.chkMP.Text = "MP <=";
            // 
            // numRest
            // 
            this.numRest.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numRest.Location = new System.Drawing.Point(291, 135);
            this.numRest.LoopValues = false;
            this.numRest.Name = "numRest";
            this.numRest.Size = new System.Drawing.Size(34, 20);
            this.numRest.TabIndex = 48;
            this.numRest.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // chkHP
            // 
            this.chkHP.AutoSize = true;
            this.chkHP.Location = new System.Drawing.Point(226, 140);
            this.chkHP.Name = "chkHP";
            this.chkHP.Size = new System.Drawing.Size(56, 17);
            this.chkHP.TabIndex = 47;
            this.chkHP.Text = "HP <=";
            // 
            // chkPacket
            // 
            this.chkPacket.AutoSize = true;
            this.chkPacket.Location = new System.Drawing.Point(229, 80);
            this.chkPacket.Name = "chkPacket";
            this.chkPacket.Size = new System.Drawing.Size(109, 17);
            this.chkPacket.TabIndex = 47;
            this.chkPacket.Text = "Use Skill Packets";
            this.chkPacket.CheckStateChanged += new System.EventHandler(this.chkPacket_CheckChanged);
            // 
            // numSkillD
            // 
            this.numSkillD.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numSkillD.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSkillD.Location = new System.Drawing.Point(135, 153);
            this.numSkillD.LoopValues = false;
            this.numSkillD.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.numSkillD.Name = "numSkillD";
            this.numSkillD.Size = new System.Drawing.Size(81, 20);
            this.numSkillD.TabIndex = 45;
            this.numSkillD.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label2.Location = new System.Drawing.Point(185, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "%";
            // 
            // numSafe
            // 
            this.numSafe.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSafe.Location = new System.Drawing.Point(135, 117);
            this.numSafe.LoopValues = false;
            this.numSafe.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSafe.Name = "numSafe";
            this.numSafe.Size = new System.Drawing.Size(44, 20);
            this.numSafe.TabIndex = 41;
            this.numSafe.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnAddSafe
            // 
            this.btnAddSafe.Checked = false;
            this.btnAddSafe.Location = new System.Drawing.Point(135, 54);
            this.btnAddSafe.Name = "btnAddSafe";
            this.btnAddSafe.Size = new System.Drawing.Size(89, 22);
            this.btnAddSafe.TabIndex = 39;
            this.btnAddSafe.Text = "Safe skill";
            this.btnAddSafe.Click += new System.EventHandler(this.btnAddSafe_Click);
            // 
            // btnSkillCmd
            // 
            this.btnSkillCmd.Checked = false;
            this.btnSkillCmd.Location = new System.Drawing.Point(135, 29);
            this.btnSkillCmd.Name = "btnSkillCmd";
            this.btnSkillCmd.Size = new System.Drawing.Size(89, 21);
            this.btnSkillCmd.TabIndex = 38;
            this.btnSkillCmd.Text = "Add (cmd)";
            this.btnSkillCmd.Click += new System.EventHandler(this.btnAddSkillCmd_Click);
            // 
            // btnAddSkill
            // 
            this.btnAddSkill.Checked = false;
            this.btnAddSkill.Location = new System.Drawing.Point(162, 4);
            this.btnAddSkill.Name = "btnAddSkill";
            this.btnAddSkill.Size = new System.Drawing.Size(62, 21);
            this.btnAddSkill.TabIndex = 38;
            this.btnAddSkill.Text = "Add skill";
            this.btnAddSkill.Click += new System.EventHandler(this.btnAddSkill_Click);
            // 
            // numSkill
            // 
            this.numSkill.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSkill.Location = new System.Drawing.Point(135, 5);
            this.numSkill.LoopValues = false;
            this.numSkill.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numSkill.Name = "numSkill";
            this.numSkill.Size = new System.Drawing.Size(29, 20);
            this.numSkill.TabIndex = 37;
            this.numSkill.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkExitRest
            // 
            this.chkExitRest.AutoSize = true;
            this.chkExitRest.Checked = true;
            this.chkExitRest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExitRest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.chkExitRest.Location = new System.Drawing.Point(4, 227);
            this.chkExitRest.Name = "chkExitRest";
            this.chkExitRest.Size = new System.Drawing.Size(148, 17);
            this.chkExitRest.TabIndex = 36;
            this.chkExitRest.Text = "Exit combat before resting";
            // 
            // chkAllSkillsCD
            // 
            this.chkAllSkillsCD.AutoSize = true;
            this.chkAllSkillsCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.chkAllSkillsCD.Location = new System.Drawing.Point(4, 195);
            this.chkAllSkillsCD.Name = "chkAllSkillsCD";
            this.chkAllSkillsCD.Size = new System.Drawing.Size(165, 30);
            this.chkAllSkillsCD.TabIndex = 35;
            this.chkAllSkillsCD.Text = "Wait for all skills to cool down\r\nbefore attacking";
            // 
            // txtKillFQ
            // 
            this.txtKillFQ.Location = new System.Drawing.Point(4, 153);
            this.txtKillFQ.Name = "txtKillFQ";
            this.txtKillFQ.Size = new System.Drawing.Size(124, 20);
            this.txtKillFQ.TabIndex = 34;
            this.txtKillFQ.Text = "Quantity (* = any)";
            this.txtKillFQ.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtKillFQ.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtKillFItem
            // 
            this.txtKillFItem.Location = new System.Drawing.Point(4, 129);
            this.txtKillFItem.Name = "txtKillFItem";
            this.txtKillFItem.Size = new System.Drawing.Size(124, 20);
            this.txtKillFItem.TabIndex = 33;
            this.txtKillFItem.Text = "Item name";
            this.txtKillFItem.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtKillFItem.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtKillFMon
            // 
            this.txtKillFMon.Location = new System.Drawing.Point(4, 105);
            this.txtKillFMon.Name = "txtKillFMon";
            this.txtKillFMon.Size = new System.Drawing.Size(124, 20);
            this.txtKillFMon.TabIndex = 32;
            this.txtKillFMon.Text = "Monster (* = random)";
            this.txtKillFMon.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtKillFMon.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // rbTemp
            // 
            this.rbTemp.AutoSize = true;
            this.rbTemp.ForeColor = System.Drawing.Color.Gainsboro;
            this.rbTemp.Location = new System.Drawing.Point(52, 81);
            this.rbTemp.Name = "rbTemp";
            this.rbTemp.Size = new System.Drawing.Size(79, 17);
            this.rbTemp.TabIndex = 31;
            this.rbTemp.Text = "Temp items";
            this.rbTemp.UseVisualStyleBackColor = true;
            // 
            // rbItems
            // 
            this.rbItems.AutoSize = true;
            this.rbItems.Checked = true;
            this.rbItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.rbItems.Location = new System.Drawing.Point(4, 81);
            this.rbItems.Name = "rbItems";
            this.rbItems.Size = new System.Drawing.Size(50, 17);
            this.rbItems.TabIndex = 30;
            this.rbItems.TabStop = true;
            this.rbItems.Text = "Items";
            this.rbItems.UseVisualStyleBackColor = true;
            // 
            // btnKillF
            // 
            this.btnKillF.Checked = false;
            this.btnKillF.Location = new System.Drawing.Point(4, 54);
            this.btnKillF.Name = "btnKillF";
            this.btnKillF.Size = new System.Drawing.Size(123, 22);
            this.btnKillF.TabIndex = 29;
            this.btnKillF.Text = "Kill for...";
            this.btnKillF.Click += new System.EventHandler(this.btnKillF_Click);
            // 
            // txtMonster
            // 
            this.txtMonster.Location = new System.Drawing.Point(4, 4);
            this.txtMonster.Name = "txtMonster";
            this.txtMonster.Size = new System.Drawing.Size(124, 20);
            this.txtMonster.TabIndex = 27;
            this.txtMonster.Text = "Monster (*  = random)";
            this.txtMonster.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtMonster.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // tabItem
            // 
            this.tabItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabItem.Controls.Add(this.pnlItem);
            this.tabItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabItem.Location = new System.Drawing.Point(4, 23);
            this.tabItem.Margin = new System.Windows.Forms.Padding(0);
            this.tabItem.Name = "tabItem";
            this.tabItem.Padding = new System.Windows.Forms.Padding(3);
            this.tabItem.Size = new System.Drawing.Size(444, 298);
            this.tabItem.TabIndex = 1;
            this.tabItem.Text = "Item";
            // 
            // pnlItem
            // 
            this.pnlItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pnlItem.Controls.Add(this.chkPickupAcTag);
            this.pnlItem.Controls.Add(this.btnWhitelistOff);
            this.pnlItem.Controls.Add(this.btnWhitelistOn);
            this.pnlItem.Controls.Add(this.btnWhitelistToggle);
            this.pnlItem.Controls.Add(this.label20);
            this.pnlItem.Controls.Add(this.label1);
            this.pnlItem.Controls.Add(this.numDropDelay);
            this.pnlItem.Controls.Add(this.chkRejectAll);
            this.pnlItem.Controls.Add(this.chkPickupAll);
            this.pnlItem.Controls.Add(this.chkBankOnStop);
            this.pnlItem.Controls.Add(this.txtShopItem);
            this.pnlItem.Controls.Add(this.numShopId);
            this.pnlItem.Controls.Add(this.label15);
            this.pnlItem.Controls.Add(this.btnBuy);
            this.pnlItem.Controls.Add(this.btnBuyFast);
            this.pnlItem.Controls.Add(this.btnLoadShop);
            this.pnlItem.Controls.Add(this.btnBoost);
            this.pnlItem.Controls.Add(this.cbBoosts);
            this.pnlItem.Controls.Add(this.numMapItem);
            this.pnlItem.Controls.Add(this.btnMapItem);
            this.pnlItem.Controls.Add(this.btnSwap);
            this.pnlItem.Controls.Add(this.txtSwapInv);
            this.pnlItem.Controls.Add(this.txtSwapBank);
            this.pnlItem.Controls.Add(this.chkReject);
            this.pnlItem.Controls.Add(this.chkPickup);
            this.pnlItem.Controls.Add(this.btnWhitelist);
            this.pnlItem.Controls.Add(this.btnBoth);
            this.pnlItem.Controls.Add(this.txtWhitelist);
            this.pnlItem.Controls.Add(this.btnItem);
            this.pnlItem.Controls.Add(this.btnUnbanklst);
            this.pnlItem.Controls.Add(this.txtItem);
            this.pnlItem.Controls.Add(this.cbItemCmds);
            this.pnlItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlItem.Location = new System.Drawing.Point(3, 3);
            this.pnlItem.Name = "pnlItem";
            this.pnlItem.Size = new System.Drawing.Size(438, 292);
            this.pnlItem.TabIndex = 105;
            // 
            // chkPickupAcTag
            // 
            this.chkPickupAcTag.AutoSize = true;
            this.chkPickupAcTag.Location = new System.Drawing.Point(296, 46);
            this.chkPickupAcTag.Name = "chkPickupAcTag";
            this.chkPickupAcTag.Size = new System.Drawing.Size(135, 17);
            this.chkPickupAcTag.TabIndex = 157;
            this.chkPickupAcTag.Text = "Pick up ac-tagged only";
            // 
            // btnWhitelistOff
            // 
            this.btnWhitelistOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWhitelistOff.Checked = false;
            this.btnWhitelistOff.Location = new System.Drawing.Point(359, 195);
            this.btnWhitelistOff.Name = "btnWhitelistOff";
            this.btnWhitelistOff.Size = new System.Drawing.Size(64, 33);
            this.btnWhitelistOff.TabIndex = 156;
            this.btnWhitelistOff.Text = "Off";
            this.btnWhitelistOff.Visible = false;
            // 
            // btnWhitelistOn
            // 
            this.btnWhitelistOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWhitelistOn.Checked = false;
            this.btnWhitelistOn.Location = new System.Drawing.Point(296, 195);
            this.btnWhitelistOn.Name = "btnWhitelistOn";
            this.btnWhitelistOn.Size = new System.Drawing.Size(64, 33);
            this.btnWhitelistOn.TabIndex = 155;
            this.btnWhitelistOn.Text = "On";
            this.btnWhitelistOn.Visible = false;
            this.btnWhitelistOn.Click += new System.EventHandler(this.btnWhitelistOn_Click);
            // 
            // btnWhitelistToggle
            // 
            this.btnWhitelistToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWhitelistToggle.Checked = false;
            this.btnWhitelistToggle.Location = new System.Drawing.Point(296, 172);
            this.btnWhitelistToggle.Name = "btnWhitelistToggle";
            this.btnWhitelistToggle.Size = new System.Drawing.Size(127, 33);
            this.btnWhitelistToggle.TabIndex = 154;
            this.btnWhitelistToggle.Text = "Toggle";
            this.btnWhitelistToggle.Visible = false;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label20.Location = new System.Drawing.Point(295, 156);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(115, 13);
            this.label20.TabIndex = 153;
            this.label20.Text = "Whitelist";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label20.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label1.Location = new System.Drawing.Point(218, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 152;
            this.label1.Text = "Drop Delay";
            // 
            // numDropDelay
            // 
            this.numDropDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDropDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numDropDelay.Location = new System.Drawing.Point(147, 217);
            this.numDropDelay.LoopValues = false;
            this.numDropDelay.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numDropDelay.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numDropDelay.Name = "numDropDelay";
            this.numDropDelay.Size = new System.Drawing.Size(65, 20);
            this.numDropDelay.TabIndex = 151;
            this.numDropDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numDropDelay.ValueChanged += new System.EventHandler(this.numDropDelay_ValueChanged);
            // 
            // chkRejectAll
            // 
            this.chkRejectAll.AutoSize = true;
            this.chkRejectAll.Enabled = false;
            this.chkRejectAll.Location = new System.Drawing.Point(296, 109);
            this.chkRejectAll.Name = "chkRejectAll";
            this.chkRejectAll.Size = new System.Drawing.Size(97, 17);
            this.chkRejectAll.TabIndex = 150;
            this.chkRejectAll.Text = "Reject all items";
            this.chkRejectAll.Visible = false;
            // 
            // chkPickupAll
            // 
            this.chkPickupAll.AutoSize = true;
            this.chkPickupAll.Location = new System.Drawing.Point(296, 4);
            this.chkPickupAll.Name = "chkPickupAll";
            this.chkPickupAll.Size = new System.Drawing.Size(102, 17);
            this.chkPickupAll.TabIndex = 149;
            this.chkPickupAll.Text = "Pick up all items";
            // 
            // chkBankOnStop
            // 
            this.chkBankOnStop.AutoSize = true;
            this.chkBankOnStop.Location = new System.Drawing.Point(296, 88);
            this.chkBankOnStop.Name = "chkBankOnStop";
            this.chkBankOnStop.Size = new System.Drawing.Size(94, 17);
            this.chkBankOnStop.TabIndex = 148;
            this.chkBankOnStop.Text = "Bank on Stop ";
            this.chkBankOnStop.CheckedChanged += new System.EventHandler(this.chkBankOnStop_CheckedChanged);
            // 
            // txtShopItem
            // 
            this.txtShopItem.Location = new System.Drawing.Point(5, 193);
            this.txtShopItem.Name = "txtShopItem";
            this.txtShopItem.Size = new System.Drawing.Size(137, 20);
            this.txtShopItem.TabIndex = 41;
            this.txtShopItem.Text = "Shop Item";
            this.txtShopItem.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtShopItem.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // numShopId
            // 
            this.numShopId.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numShopId.Location = new System.Drawing.Point(5, 169);
            this.numShopId.LoopValues = false;
            this.numShopId.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numShopId.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numShopId.Name = "numShopId";
            this.numShopId.Size = new System.Drawing.Size(65, 20);
            this.numShopId.TabIndex = 40;
            this.numShopId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label15.Location = new System.Drawing.Point(3, 156);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 13);
            this.label15.TabIndex = 39;
            this.label15.Text = "Shop ID";
            // 
            // btnBuy
            // 
            this.btnBuy.Checked = false;
            this.btnBuy.Location = new System.Drawing.Point(4, 215);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(68, 22);
            this.btnBuy.TabIndex = 38;
            this.btnBuy.Text = "Buy item";
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // btnBuyFast
            // 
            this.btnBuyFast.Checked = false;
            this.btnBuyFast.Location = new System.Drawing.Point(74, 215);
            this.btnBuyFast.Name = "btnBuyFast";
            this.btnBuyFast.Size = new System.Drawing.Size(69, 22);
            this.btnBuyFast.TabIndex = 133;
            this.btnBuyFast.Text = "Buy fast";
            this.btnBuyFast.Click += new System.EventHandler(this.btnBuyFast_Click);
            // 
            // btnLoadShop
            // 
            this.btnLoadShop.Checked = false;
            this.btnLoadShop.Location = new System.Drawing.Point(76, 168);
            this.btnLoadShop.Name = "btnLoadShop";
            this.btnLoadShop.Size = new System.Drawing.Size(66, 22);
            this.btnLoadShop.TabIndex = 134;
            this.btnLoadShop.Text = "Load Shop";
            this.btnLoadShop.Click += new System.EventHandler(this.btnLoadShop_Click);
            // 
            // btnBoost
            // 
            this.btnBoost.Checked = false;
            this.btnBoost.Location = new System.Drawing.Point(146, 129);
            this.btnBoost.Name = "btnBoost";
            this.btnBoost.Size = new System.Drawing.Size(134, 22);
            this.btnBoost.TabIndex = 37;
            this.btnBoost.Text = "Add boost";
            this.btnBoost.Click += new System.EventHandler(this.btnBoost_Click);
            // 
            // cbBoosts
            // 
            this.cbBoosts.FormattingEnabled = true;
            this.cbBoosts.Location = new System.Drawing.Point(147, 105);
            this.cbBoosts.Name = "cbBoosts";
            this.cbBoosts.Size = new System.Drawing.Size(132, 21);
            this.cbBoosts.TabIndex = 36;
            this.cbBoosts.Click += new System.EventHandler(this.cbBoosts_Click);
            // 
            // numMapItem
            // 
            this.numMapItem.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numMapItem.Location = new System.Drawing.Point(148, 170);
            this.numMapItem.LoopValues = false;
            this.numMapItem.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMapItem.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMapItem.Name = "numMapItem";
            this.numMapItem.Size = new System.Drawing.Size(132, 20);
            this.numMapItem.TabIndex = 30;
            this.numMapItem.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnMapItem
            // 
            this.btnMapItem.Checked = false;
            this.btnMapItem.Location = new System.Drawing.Point(147, 193);
            this.btnMapItem.Name = "btnMapItem";
            this.btnMapItem.Size = new System.Drawing.Size(134, 22);
            this.btnMapItem.TabIndex = 29;
            this.btnMapItem.Text = "Get map item";
            this.btnMapItem.Click += new System.EventHandler(this.btnMapItem_Click);
            // 
            // btnSwap
            // 
            this.btnSwap.Checked = false;
            this.btnSwap.Location = new System.Drawing.Point(3, 129);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(141, 22);
            this.btnSwap.TabIndex = 28;
            this.btnSwap.Text = "Bank swap";
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            // 
            // txtSwapInv
            // 
            this.txtSwapInv.Location = new System.Drawing.Point(4, 105);
            this.txtSwapInv.Name = "txtSwapInv";
            this.txtSwapInv.Size = new System.Drawing.Size(139, 20);
            this.txtSwapInv.TabIndex = 27;
            this.txtSwapInv.Text = "Inventory item name";
            // 
            // txtSwapBank
            // 
            this.txtSwapBank.Location = new System.Drawing.Point(4, 80);
            this.txtSwapBank.Name = "txtSwapBank";
            this.txtSwapBank.Size = new System.Drawing.Size(139, 20);
            this.txtSwapBank.TabIndex = 26;
            this.txtSwapBank.Text = "Bank item name";
            // 
            // chkReject
            // 
            this.chkReject.AutoSize = true;
            this.chkReject.Location = new System.Drawing.Point(296, 67);
            this.chkReject.Name = "chkReject";
            this.chkReject.Size = new System.Drawing.Size(130, 17);
            this.chkReject.TabIndex = 25;
            this.chkReject.Text = "Reject non-whitelisted";
            // 
            // chkPickup
            // 
            this.chkPickup.AutoSize = true;
            this.chkPickup.Checked = true;
            this.chkPickup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPickup.Location = new System.Drawing.Point(296, 25);
            this.chkPickup.Name = "chkPickup";
            this.chkPickup.Size = new System.Drawing.Size(114, 17);
            this.chkPickup.TabIndex = 24;
            this.chkPickup.Text = "Pick up whitelisted";
            // 
            // btnWhitelist
            // 
            this.btnWhitelist.Checked = false;
            this.btnWhitelist.Location = new System.Drawing.Point(146, 54);
            this.btnWhitelist.Name = "btnWhitelist";
            this.btnWhitelist.Size = new System.Drawing.Size(132, 22);
            this.btnWhitelist.TabIndex = 23;
            this.btnWhitelist.Text = "Add to whitelist";
            this.btnWhitelist.Click += new System.EventHandler(this.btnWhitelist_Click);
            // 
            // btnBoth
            // 
            this.btnBoth.Checked = false;
            this.btnBoth.Location = new System.Drawing.Point(146, 29);
            this.btnBoth.Name = "btnBoth";
            this.btnBoth.Size = new System.Drawing.Size(132, 22);
            this.btnBoth.TabIndex = 23;
            this.btnBoth.Text = "Add to both";
            this.btnBoth.Click += new System.EventHandler(this.btnBoth_Click);
            // 
            // txtWhitelist
            // 
            this.txtWhitelist.Location = new System.Drawing.Point(148, 4);
            this.txtWhitelist.Name = "txtWhitelist";
            this.txtWhitelist.Size = new System.Drawing.Size(132, 20);
            this.txtWhitelist.TabIndex = 22;
            this.txtWhitelist.Text = "Item name";
            this.txtWhitelist.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtWhitelist.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnItem
            // 
            this.btnItem.Checked = false;
            this.btnItem.Location = new System.Drawing.Point(3, 54);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(141, 22);
            this.btnItem.TabIndex = 21;
            this.btnItem.Text = "Add command";
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
            // 
            // btnUnbanklst
            // 
            this.btnUnbanklst.Checked = false;
            this.btnUnbanklst.Location = new System.Drawing.Point(146, 79);
            this.btnUnbanklst.Name = "btnUnbanklst";
            this.btnUnbanklst.Size = new System.Drawing.Size(132, 22);
            this.btnUnbanklst.TabIndex = 147;
            this.btnUnbanklst.Text = "Unbank";
            this.btnUnbanklst.Click += new System.EventHandler(this.btnUnbanklst_Click);
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(4, 29);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(139, 20);
            this.txtItem.TabIndex = 20;
            this.txtItem.Text = "Item name";
            this.txtItem.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtItem.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // cbItemCmds
            // 
            this.cbItemCmds.FormattingEnabled = true;
            this.cbItemCmds.Items.AddRange(new object[] {
            "Get drop",
            "Sell",
            "Equip",
            "Unsafe Equip",
            "To bank from inv",
            "To inv from bank",
            "Equip Set",
            "Add to Whitelist",
            "Remove from Whitelist"});
            this.cbItemCmds.Location = new System.Drawing.Point(4, 4);
            this.cbItemCmds.Name = "cbItemCmds";
            this.cbItemCmds.Size = new System.Drawing.Size(139, 21);
            this.cbItemCmds.TabIndex = 19;
            // 
            // tabMap
            // 
            this.tabMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabMap.Controls.Add(this.pnlMap);
            this.tabMap.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabMap.Location = new System.Drawing.Point(4, 23);
            this.tabMap.Margin = new System.Windows.Forms.Padding(0);
            this.tabMap.Name = "tabMap";
            this.tabMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabMap.Size = new System.Drawing.Size(444, 298);
            this.tabMap.TabIndex = 2;
            this.tabMap.Text = "Map";
            // 
            // pnlMap
            // 
            this.pnlMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pnlMap.Controls.Add(this.btnCurrBlank);
            this.pnlMap.Controls.Add(this.btnSetSpawn);
            this.pnlMap.Controls.Add(this.btnWalkRdm);
            this.pnlMap.Controls.Add(this.btnWalkCur);
            this.pnlMap.Controls.Add(this.btnWalk);
            this.pnlMap.Controls.Add(this.numWalkY);
            this.pnlMap.Controls.Add(this.numWalkX);
            this.pnlMap.Controls.Add(this.button2);
            this.pnlMap.Controls.Add(this.btnCellSwap);
            this.pnlMap.Controls.Add(this.btnJump);
            this.pnlMap.Controls.Add(this.btnCurrCell);
            this.pnlMap.Controls.Add(this.txtPad);
            this.pnlMap.Controls.Add(this.txtCell);
            this.pnlMap.Controls.Add(this.btnJoin);
            this.pnlMap.Controls.Add(this.txtJoinPad);
            this.pnlMap.Controls.Add(this.txtJoinCell);
            this.pnlMap.Controls.Add(this.txtJoin);
            this.pnlMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMap.Location = new System.Drawing.Point(3, 3);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(438, 292);
            this.pnlMap.TabIndex = 104;
            // 
            // btnCurrBlank
            // 
            this.btnCurrBlank.Checked = false;
            this.btnCurrBlank.Location = new System.Drawing.Point(128, 29);
            this.btnCurrBlank.Name = "btnCurrBlank";
            this.btnCurrBlank.Size = new System.Drawing.Size(56, 23);
            this.btnCurrBlank.TabIndex = 143;
            this.btnCurrBlank.Text = "Blank";
            this.btnCurrBlank.Click += new System.EventHandler(this.btnCurrBlank_Click);
            // 
            // btnSetSpawn
            // 
            this.btnSetSpawn.Checked = false;
            this.btnSetSpawn.Location = new System.Drawing.Point(4, 190);
            this.btnSetSpawn.MaximumSize = new System.Drawing.Size(114, 22);
            this.btnSetSpawn.MinimumSize = new System.Drawing.Size(114, 22);
            this.btnSetSpawn.Name = "btnSetSpawn";
            this.btnSetSpawn.Size = new System.Drawing.Size(114, 22);
            this.btnSetSpawn.TabIndex = 142;
            this.btnSetSpawn.Text = "Set Spawnpoint";
            this.btnSetSpawn.Click += new System.EventHandler(this.btnSetSpawn_Click);
            // 
            // btnWalkRdm
            // 
            this.btnWalkRdm.Checked = false;
            this.btnWalkRdm.Location = new System.Drawing.Point(5, 162);
            this.btnWalkRdm.MaximumSize = new System.Drawing.Size(114, 22);
            this.btnWalkRdm.MinimumSize = new System.Drawing.Size(114, 22);
            this.btnWalkRdm.Name = "btnWalkRdm";
            this.btnWalkRdm.Size = new System.Drawing.Size(114, 22);
            this.btnWalkRdm.TabIndex = 142;
            this.btnWalkRdm.Text = "Walk Randomly";
            this.btnWalkRdm.Click += new System.EventHandler(this.btnWalkRdm_Click);
            // 
            // btnWalkCur
            // 
            this.btnWalkCur.Checked = false;
            this.btnWalkCur.Location = new System.Drawing.Point(4, 134);
            this.btnWalkCur.MaximumSize = new System.Drawing.Size(114, 22);
            this.btnWalkCur.MinimumSize = new System.Drawing.Size(114, 22);
            this.btnWalkCur.Name = "btnWalkCur";
            this.btnWalkCur.Size = new System.Drawing.Size(114, 22);
            this.btnWalkCur.TabIndex = 38;
            this.btnWalkCur.Text = "Current position";
            this.btnWalkCur.Click += new System.EventHandler(this.btnWalkCur_Click);
            // 
            // btnWalk
            // 
            this.btnWalk.Checked = false;
            this.btnWalk.Location = new System.Drawing.Point(4, 106);
            this.btnWalk.Name = "btnWalk";
            this.btnWalk.Size = new System.Drawing.Size(115, 22);
            this.btnWalk.TabIndex = 37;
            this.btnWalk.Text = "Walk to";
            this.btnWalk.Click += new System.EventHandler(this.btnWalk_Click);
            // 
            // numWalkY
            // 
            this.numWalkY.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numWalkY.Location = new System.Drawing.Point(66, 80);
            this.numWalkY.LoopValues = false;
            this.numWalkY.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numWalkY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWalkY.Name = "numWalkY";
            this.numWalkY.Size = new System.Drawing.Size(52, 20);
            this.numWalkY.TabIndex = 36;
            this.numWalkY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numWalkX
            // 
            this.numWalkX.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numWalkX.Location = new System.Drawing.Point(7, 80);
            this.numWalkX.LoopValues = false;
            this.numWalkX.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numWalkX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWalkX.Name = "numWalkX";
            this.numWalkX.Size = new System.Drawing.Size(54, 20);
            this.numWalkX.TabIndex = 35;
            this.numWalkX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button2
            // 
            this.button2.Checked = false;
            this.button2.Location = new System.Drawing.Point(144, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(21, 22);
            this.button2.TabIndex = 34;
            this.button2.Text = ">";
            this.button2.Click += new System.EventHandler(this.btnCellSwap_Click);
            // 
            // btnCellSwap
            // 
            this.btnCellSwap.Checked = false;
            this.btnCellSwap.Location = new System.Drawing.Point(144, 55);
            this.btnCellSwap.Name = "btnCellSwap";
            this.btnCellSwap.Size = new System.Drawing.Size(21, 22);
            this.btnCellSwap.TabIndex = 34;
            this.btnCellSwap.Text = "<";
            this.btnCellSwap.Click += new System.EventHandler(this.btnCellSwap_Click);
            // 
            // btnJump
            // 
            this.btnJump.Checked = false;
            this.btnJump.Location = new System.Drawing.Point(192, 53);
            this.btnJump.Name = "btnJump";
            this.btnJump.Size = new System.Drawing.Size(111, 22);
            this.btnJump.TabIndex = 33;
            this.btnJump.Text = "Jump";
            this.btnJump.Click += new System.EventHandler(this.btnJump_Click);
            // 
            // btnCurrCell
            // 
            this.btnCurrCell.Checked = false;
            this.btnCurrCell.Location = new System.Drawing.Point(192, 29);
            this.btnCurrCell.Name = "btnCurrCell";
            this.btnCurrCell.Size = new System.Drawing.Size(111, 20);
            this.btnCurrCell.TabIndex = 32;
            this.btnCurrCell.Text = "Get current cell";
            this.btnCurrCell.Click += new System.EventHandler(this.btnCurrCell_Click);
            // 
            // txtPad
            // 
            this.txtPad.Location = new System.Drawing.Point(250, 4);
            this.txtPad.Name = "txtPad";
            this.txtPad.Size = new System.Drawing.Size(54, 20);
            this.txtPad.TabIndex = 31;
            this.txtPad.Text = "Pad";
            this.txtPad.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtPad.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtCell
            // 
            this.txtCell.Location = new System.Drawing.Point(192, 4);
            this.txtCell.Name = "txtCell";
            this.txtCell.Size = new System.Drawing.Size(54, 20);
            this.txtCell.TabIndex = 30;
            this.txtCell.Text = "Cell";
            this.txtCell.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtCell.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnJoin
            // 
            this.btnJoin.Checked = false;
            this.btnJoin.Location = new System.Drawing.Point(6, 53);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(113, 22);
            this.btnJoin.TabIndex = 29;
            this.btnJoin.Text = "Join map";
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // txtJoinPad
            // 
            this.txtJoinPad.Location = new System.Drawing.Point(64, 29);
            this.txtJoinPad.Name = "txtJoinPad";
            this.txtJoinPad.Size = new System.Drawing.Size(54, 20);
            this.txtJoinPad.TabIndex = 28;
            this.txtJoinPad.Text = "Spawn";
            // 
            // txtJoinCell
            // 
            this.txtJoinCell.Location = new System.Drawing.Point(6, 29);
            this.txtJoinCell.Name = "txtJoinCell";
            this.txtJoinCell.Size = new System.Drawing.Size(54, 20);
            this.txtJoinCell.TabIndex = 27;
            this.txtJoinCell.Text = "Enter";
            // 
            // txtJoin
            // 
            this.txtJoin.Location = new System.Drawing.Point(6, 4);
            this.txtJoin.Name = "txtJoin";
            this.txtJoin.Size = new System.Drawing.Size(112, 20);
            this.txtJoin.TabIndex = 26;
            this.txtJoin.Text = "battleon-1e99";
            // 
            // tabQuest
            // 
            this.tabQuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabQuest.Controls.Add(this.pnlQuest);
            this.tabQuest.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabQuest.Location = new System.Drawing.Point(4, 23);
            this.tabQuest.Margin = new System.Windows.Forms.Padding(0);
            this.tabQuest.Name = "tabQuest";
            this.tabQuest.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuest.Size = new System.Drawing.Size(444, 298);
            this.tabQuest.TabIndex = 3;
            this.tabQuest.Text = "Quest";
            // 
            // pnlQuest
            // 
            this.pnlQuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pnlQuest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlQuest.Controls.Add(this.label14);
            this.pnlQuest.Controls.Add(this.numEnsureTries);
            this.pnlQuest.Controls.Add(this.chkEnsureComplete);
            this.pnlQuest.Controls.Add(this.btnQuestAccept);
            this.pnlQuest.Controls.Add(this.btnQuestComplete);
            this.pnlQuest.Controls.Add(this.btnQuestAdd);
            this.pnlQuest.Controls.Add(this.numQuestItem);
            this.pnlQuest.Controls.Add(this.chkQuestItem);
            this.pnlQuest.Controls.Add(this.numQuestID);
            this.pnlQuest.Controls.Add(this.label4);
            this.pnlQuest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQuest.Location = new System.Drawing.Point(3, 3);
            this.pnlQuest.Name = "pnlQuest";
            this.pnlQuest.Size = new System.Drawing.Size(438, 292);
            this.pnlQuest.TabIndex = 106;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label14.Location = new System.Drawing.Point(189, 95);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "Tries";
            this.label14.Visible = false;
            // 
            // numEnsureTries
            // 
            this.numEnsureTries.Enabled = false;
            this.numEnsureTries.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numEnsureTries.Location = new System.Drawing.Point(143, 92);
            this.numEnsureTries.LoopValues = false;
            this.numEnsureTries.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEnsureTries.Name = "numEnsureTries";
            this.numEnsureTries.Size = new System.Drawing.Size(42, 20);
            this.numEnsureTries.TabIndex = 15;
            this.numEnsureTries.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numEnsureTries.Visible = false;
            // 
            // chkEnsureComplete
            // 
            this.chkEnsureComplete.AutoSize = true;
            this.chkEnsureComplete.Location = new System.Drawing.Point(143, 75);
            this.chkEnsureComplete.Name = "chkEnsureComplete";
            this.chkEnsureComplete.Size = new System.Drawing.Size(59, 17);
            this.chkEnsureComplete.TabIndex = 14;
            this.chkEnsureComplete.Text = "Ensure";
            this.chkEnsureComplete.Visible = false;
            this.chkEnsureComplete.CheckedChanged += new System.EventHandler(this.chkEnsureComplete_CheckedChanged);
            // 
            // btnQuestAccept
            // 
            this.btnQuestAccept.Checked = false;
            this.btnQuestAccept.Location = new System.Drawing.Point(8, 99);
            this.btnQuestAccept.Name = "btnQuestAccept";
            this.btnQuestAccept.Size = new System.Drawing.Size(129, 22);
            this.btnQuestAccept.TabIndex = 13;
            this.btnQuestAccept.Text = "Accept command";
            this.btnQuestAccept.Click += new System.EventHandler(this.btnQuestAccept_Click);
            // 
            // btnQuestComplete
            // 
            this.btnQuestComplete.Checked = false;
            this.btnQuestComplete.Location = new System.Drawing.Point(8, 72);
            this.btnQuestComplete.Name = "btnQuestComplete";
            this.btnQuestComplete.Size = new System.Drawing.Size(129, 22);
            this.btnQuestComplete.TabIndex = 12;
            this.btnQuestComplete.Text = "Complete command";
            this.btnQuestComplete.Click += new System.EventHandler(this.btnQuestComplete_Click);
            // 
            // btnQuestAdd
            // 
            this.btnQuestAdd.Checked = false;
            this.btnQuestAdd.Location = new System.Drawing.Point(8, 45);
            this.btnQuestAdd.Name = "btnQuestAdd";
            this.btnQuestAdd.Size = new System.Drawing.Size(129, 22);
            this.btnQuestAdd.TabIndex = 11;
            this.btnQuestAdd.Text = "Add to quest list";
            this.btnQuestAdd.Click += new System.EventHandler(this.btnQuestAdd_Click);
            // 
            // numQuestItem
            // 
            this.numQuestItem.Enabled = false;
            this.numQuestItem.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numQuestItem.Location = new System.Drawing.Point(143, 21);
            this.numQuestItem.LoopValues = false;
            this.numQuestItem.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numQuestItem.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuestItem.Name = "numQuestItem";
            this.numQuestItem.Size = new System.Drawing.Size(76, 20);
            this.numQuestItem.TabIndex = 10;
            this.numQuestItem.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkQuestItem
            // 
            this.chkQuestItem.AutoSize = true;
            this.chkQuestItem.Location = new System.Drawing.Point(143, 5);
            this.chkQuestItem.Name = "chkQuestItem";
            this.chkQuestItem.Size = new System.Drawing.Size(60, 17);
            this.chkQuestItem.TabIndex = 9;
            this.chkQuestItem.Text = "Item ID";
            this.chkQuestItem.CheckedChanged += new System.EventHandler(this.chkQuestItem_CheckedChanged);
            // 
            // numQuestID
            // 
            this.numQuestID.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numQuestID.Location = new System.Drawing.Point(8, 21);
            this.numQuestID.LoopValues = false;
            this.numQuestID.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numQuestID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuestID.Name = "numQuestID";
            this.numQuestID.Size = new System.Drawing.Size(129, 20);
            this.numQuestID.TabIndex = 8;
            this.numQuestID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label4.Location = new System.Drawing.Point(5, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Quest ID";
            // 
            // tabMisc
            // 
            this.tabMisc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabMisc.Controls.Add(this.pnlMisc);
            this.tabMisc.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabMisc.Location = new System.Drawing.Point(4, 23);
            this.tabMisc.Margin = new System.Windows.Forms.Padding(0);
            this.tabMisc.Name = "tabMisc";
            this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
            this.tabMisc.Size = new System.Drawing.Size(444, 298);
            this.tabMisc.TabIndex = 4;
            this.tabMisc.Text = "Misc";
            // 
            // pnlMisc
            // 
            this.pnlMisc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pnlMisc.Controls.Add(this.label19);
            this.pnlMisc.Controls.Add(this.btnDecreaseInt);
            this.pnlMisc.Controls.Add(this.btnIncreaseInt);
            this.pnlMisc.Controls.Add(this.numSetInt);
            this.pnlMisc.Controls.Add(this.txtSetInt);
            this.pnlMisc.Controls.Add(this.btnSetInt);
            this.pnlMisc.Controls.Add(this.label18);
            this.pnlMisc.Controls.Add(this.btnGoDownIndex);
            this.pnlMisc.Controls.Add(this.btnGoUpIndex);
            this.pnlMisc.Controls.Add(this.btnGotoIndex);
            this.pnlMisc.Controls.Add(this.numIndexCmd);
            this.pnlMisc.Controls.Add(this.btnBlank);
            this.pnlMisc.Controls.Add(this.btnProvokeOff);
            this.pnlMisc.Controls.Add(this.btnProvokeOn);
            this.pnlMisc.Controls.Add(this.btnProvoke);
            this.pnlMisc.Controls.Add(this.labelProvoke);
            this.pnlMisc.Controls.Add(this.chkRestartDeath);
            this.pnlMisc.Controls.Add(this.chkMerge);
            this.pnlMisc.Controls.Add(this.btnLogout);
            this.pnlMisc.Controls.Add(this.txtLabel);
            this.pnlMisc.Controls.Add(this.btnGotoLabel);
            this.pnlMisc.Controls.Add(this.btnAddLabel);
            this.pnlMisc.Controls.Add(this.txtDescription);
            this.pnlMisc.Controls.Add(this.txtAuthor);
            this.pnlMisc.Controls.Add(this.btnSave);
            this.pnlMisc.Controls.Add(this.btnBeep);
            this.pnlMisc.Controls.Add(this.numBeepTimes);
            this.pnlMisc.Controls.Add(this.btnDelay);
            this.pnlMisc.Controls.Add(this.numDelay);
            this.pnlMisc.Controls.Add(this.label3);
            this.pnlMisc.Controls.Add(this.numBotDelay);
            this.pnlMisc.Controls.Add(this.btnBotDelay);
            this.pnlMisc.Controls.Add(this.txtPlayer);
            this.pnlMisc.Controls.Add(this.btnGoto);
            this.pnlMisc.Controls.Add(this.btnLoad);
            this.pnlMisc.Controls.Add(this.btnRestart);
            this.pnlMisc.Controls.Add(this.btnStop);
            this.pnlMisc.Controls.Add(this.btnLoadCmd);
            this.pnlMisc.Controls.Add(this.chkSkip);
            this.pnlMisc.Controls.Add(this.btnStatementAdd);
            this.pnlMisc.Controls.Add(this.txtStatement2);
            this.pnlMisc.Controls.Add(this.txtStatement1);
            this.pnlMisc.Controls.Add(this.cbStatement);
            this.pnlMisc.Controls.Add(this.cbCategories);
            this.pnlMisc.Controls.Add(this.txtPacket);
            this.pnlMisc.Controls.Add(this.btnClientPacket);
            this.pnlMisc.Controls.Add(this.btnPacket);
            this.pnlMisc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMisc.Location = new System.Drawing.Point(3, 3);
            this.pnlMisc.Name = "pnlMisc";
            this.pnlMisc.Size = new System.Drawing.Size(438, 292);
            this.pnlMisc.TabIndex = 107;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label19.Location = new System.Drawing.Point(42, 221);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(98, 13);
            this.label19.TabIndex = 159;
            this.label19.Text = "Temporary Integers";
            // 
            // btnDecreaseInt
            // 
            this.btnDecreaseInt.Checked = false;
            this.btnDecreaseInt.Location = new System.Drawing.Point(94, 262);
            this.btnDecreaseInt.Name = "btnDecreaseInt";
            this.btnDecreaseInt.Size = new System.Drawing.Size(78, 23);
            this.btnDecreaseInt.TabIndex = 158;
            this.btnDecreaseInt.Text = "Decrease";
            this.btnDecreaseInt.Click += new System.EventHandler(this.btnDecreaseInt_Click);
            // 
            // btnIncreaseInt
            // 
            this.btnIncreaseInt.Checked = false;
            this.btnIncreaseInt.Location = new System.Drawing.Point(7, 262);
            this.btnIncreaseInt.Name = "btnIncreaseInt";
            this.btnIncreaseInt.Size = new System.Drawing.Size(81, 23);
            this.btnIncreaseInt.TabIndex = 158;
            this.btnIncreaseInt.Text = "Increase";
            this.btnIncreaseInt.Click += new System.EventHandler(this.btnIncreaseInt_Click);
            // 
            // numSetInt
            // 
            this.numSetInt.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSetInt.Location = new System.Drawing.Point(70, 240);
            this.numSetInt.LoopValues = false;
            this.numSetInt.Name = "numSetInt";
            this.numSetInt.Size = new System.Drawing.Size(30, 20);
            this.numSetInt.TabIndex = 157;
            // 
            // txtSetInt
            // 
            this.txtSetInt.Location = new System.Drawing.Point(7, 240);
            this.txtSetInt.Name = "txtSetInt";
            this.txtSetInt.Size = new System.Drawing.Size(57, 20);
            this.txtSetInt.TabIndex = 156;
            // 
            // btnSetInt
            // 
            this.btnSetInt.Checked = false;
            this.btnSetInt.Location = new System.Drawing.Point(104, 238);
            this.btnSetInt.Name = "btnSetInt";
            this.btnSetInt.Size = new System.Drawing.Size(68, 23);
            this.btnSetInt.TabIndex = 155;
            this.btnSetInt.Text = "Set";
            this.btnSetInt.Click += new System.EventHandler(this.btnSetInt_Click);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label18.Location = new System.Drawing.Point(352, 201);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(33, 13);
            this.label18.TabIndex = 154;
            this.label18.Text = "Index";
            // 
            // btnGoDownIndex
            // 
            this.btnGoDownIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoDownIndex.Checked = false;
            this.btnGoDownIndex.Location = new System.Drawing.Point(371, 240);
            this.btnGoDownIndex.Name = "btnGoDownIndex";
            this.btnGoDownIndex.Size = new System.Drawing.Size(57, 23);
            this.btnGoDownIndex.TabIndex = 153;
            this.btnGoDownIndex.Text = "Down--";
            this.btnGoDownIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
            // 
            // btnGoUpIndex
            // 
            this.btnGoUpIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoUpIndex.Checked = false;
            this.btnGoUpIndex.Location = new System.Drawing.Point(311, 240);
            this.btnGoUpIndex.Name = "btnGoUpIndex";
            this.btnGoUpIndex.Size = new System.Drawing.Size(57, 23);
            this.btnGoUpIndex.TabIndex = 153;
            this.btnGoUpIndex.Text = "Up++";
            this.btnGoUpIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
            // 
            // btnGotoIndex
            // 
            this.btnGotoIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGotoIndex.Checked = false;
            this.btnGotoIndex.Location = new System.Drawing.Point(355, 216);
            this.btnGotoIndex.Name = "btnGotoIndex";
            this.btnGotoIndex.Size = new System.Drawing.Size(74, 23);
            this.btnGotoIndex.TabIndex = 153;
            this.btnGotoIndex.Text = "Goto Index";
            this.btnGotoIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
            // 
            // numIndexCmd
            // 
            this.numIndexCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numIndexCmd.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numIndexCmd.Location = new System.Drawing.Point(311, 217);
            this.numIndexCmd.LoopValues = false;
            this.numIndexCmd.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numIndexCmd.Name = "numIndexCmd";
            this.numIndexCmd.Size = new System.Drawing.Size(41, 20);
            this.numIndexCmd.TabIndex = 152;
            // 
            // btnBlank
            // 
            this.btnBlank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBlank.Checked = false;
            this.btnBlank.Location = new System.Drawing.Point(177, 153);
            this.btnBlank.Name = "btnBlank";
            this.btnBlank.Size = new System.Drawing.Size(128, 22);
            this.btnBlank.TabIndex = 151;
            this.btnBlank.Text = "Blank Command";
            this.btnBlank.Click += new System.EventHandler(this.btnBlank_Click);
            // 
            // btnProvokeOff
            // 
            this.btnProvokeOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProvokeOff.Checked = false;
            this.btnProvokeOff.Location = new System.Drawing.Point(241, 240);
            this.btnProvokeOff.Name = "btnProvokeOff";
            this.btnProvokeOff.Size = new System.Drawing.Size(64, 23);
            this.btnProvokeOff.TabIndex = 150;
            this.btnProvokeOff.Text = "Off";
            this.btnProvokeOff.Click += new System.EventHandler(this.btnProvokeOff_Click);
            // 
            // btnProvokeOn
            // 
            this.btnProvokeOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProvokeOn.Checked = false;
            this.btnProvokeOn.Location = new System.Drawing.Point(177, 240);
            this.btnProvokeOn.Name = "btnProvokeOn";
            this.btnProvokeOn.Size = new System.Drawing.Size(59, 22);
            this.btnProvokeOn.TabIndex = 149;
            this.btnProvokeOn.Text = "On";
            this.btnProvokeOn.Click += new System.EventHandler(this.btnProvokeOn_Click);
            // 
            // btnProvoke
            // 
            this.btnProvoke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProvoke.Checked = false;
            this.btnProvoke.Location = new System.Drawing.Point(177, 216);
            this.btnProvoke.Name = "btnProvoke";
            this.btnProvoke.Size = new System.Drawing.Size(127, 23);
            this.btnProvoke.TabIndex = 148;
            this.btnProvoke.Text = "Toggle";
            this.btnProvoke.Click += new System.EventHandler(this.btnProvoke_Click);
            // 
            // labelProvoke
            // 
            this.labelProvoke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProvoke.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.labelProvoke.Location = new System.Drawing.Point(182, 202);
            this.labelProvoke.Name = "labelProvoke";
            this.labelProvoke.Size = new System.Drawing.Size(115, 13);
            this.labelProvoke.TabIndex = 147;
            this.labelProvoke.Text = "Provoke";
            this.labelProvoke.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkRestartDeath
            // 
            this.chkRestartDeath.AutoSize = true;
            this.chkRestartDeath.Checked = true;
            this.chkRestartDeath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRestartDeath.Location = new System.Drawing.Point(7, 199);
            this.chkRestartDeath.Name = "chkRestartDeath";
            this.chkRestartDeath.Size = new System.Drawing.Size(133, 17);
            this.chkRestartDeath.TabIndex = 116;
            this.chkRestartDeath.Text = "Restart bot upon dying";
            // 
            // chkMerge
            // 
            this.chkMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMerge.AutoSize = true;
            this.chkMerge.Location = new System.Drawing.Point(376, 57);
            this.chkMerge.Name = "chkMerge";
            this.chkMerge.Size = new System.Drawing.Size(56, 17);
            this.chkMerge.TabIndex = 115;
            this.chkMerge.Text = "Merge";
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Checked = false;
            this.btnLogout.Location = new System.Drawing.Point(311, 54);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(58, 22);
            this.btnLogout.TabIndex = 114;
            this.btnLogout.Text = "Logout";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // txtLabel
            // 
            this.txtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLabel.Location = new System.Drawing.Point(311, 153);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(117, 20);
            this.txtLabel.TabIndex = 113;
            this.txtLabel.Text = "Label name";
            // 
            // btnGotoLabel
            // 
            this.btnGotoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGotoLabel.Checked = false;
            this.btnGotoLabel.Location = new System.Drawing.Point(310, 175);
            this.btnGotoLabel.Name = "btnGotoLabel";
            this.btnGotoLabel.Size = new System.Drawing.Size(58, 22);
            this.btnGotoLabel.TabIndex = 112;
            this.btnGotoLabel.Text = "Goto";
            this.btnGotoLabel.Click += new System.EventHandler(this.btnGotoLabel_Click);
            // 
            // btnAddLabel
            // 
            this.btnAddLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLabel.Checked = false;
            this.btnAddLabel.Location = new System.Drawing.Point(371, 175);
            this.btnAddLabel.Name = "btnAddLabel";
            this.btnAddLabel.Size = new System.Drawing.Size(57, 22);
            this.btnAddLabel.TabIndex = 111;
            this.btnAddLabel.Text = "Add";
            this.btnAddLabel.Click += new System.EventHandler(this.btnAddLabel_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(311, 103);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(117, 48);
            this.txtDescription.TabIndex = 109;
            this.txtDescription.Text = "Description";
            this.txtDescription.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtDescription.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // txtAuthor
            // 
            this.txtAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAuthor.Location = new System.Drawing.Point(311, 80);
            this.txtAuthor.Multiline = true;
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(117, 20);
            this.txtAuthor.TabIndex = 110;
            this.txtAuthor.Text = "Author";
            this.txtAuthor.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtAuthor.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Checked = false;
            this.btnSave.Location = new System.Drawing.Point(311, 28);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(58, 22);
            this.btnSave.TabIndex = 75;
            this.btnSave.Text = "Save bot";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBeep
            // 
            this.btnBeep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBeep.Checked = false;
            this.btnBeep.Location = new System.Drawing.Point(177, 177);
            this.btnBeep.Name = "btnBeep";
            this.btnBeep.Size = new System.Drawing.Size(75, 22);
            this.btnBeep.TabIndex = 74;
            this.btnBeep.Text = "Play Sound";
            this.btnBeep.Click += new System.EventHandler(this.btnBeep_Click);
            // 
            // numBeepTimes
            // 
            this.numBeepTimes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numBeepTimes.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numBeepTimes.Location = new System.Drawing.Point(253, 178);
            this.numBeepTimes.LoopValues = false;
            this.numBeepTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBeepTimes.Name = "numBeepTimes";
            this.numBeepTimes.Size = new System.Drawing.Size(52, 20);
            this.numBeepTimes.TabIndex = 73;
            this.numBeepTimes.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnDelay
            // 
            this.btnDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelay.Checked = false;
            this.btnDelay.Location = new System.Drawing.Point(177, 78);
            this.btnDelay.Name = "btnDelay";
            this.btnDelay.Size = new System.Drawing.Size(75, 22);
            this.btnDelay.TabIndex = 74;
            this.btnDelay.Text = "Delay";
            this.btnDelay.Click += new System.EventHandler(this.btnDelay_Click);
            // 
            // numDelay
            // 
            this.numDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numDelay.Location = new System.Drawing.Point(253, 79);
            this.numDelay.LoopValues = false;
            this.numDelay.Maximum = new decimal(new int[] {
            71000,
            0,
            0,
            0});
            this.numDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(52, 20);
            this.numDelay.TabIndex = 73;
            this.numDelay.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label3.Location = new System.Drawing.Point(7, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 26);
            this.label3.TabIndex = 72;
            this.label3.Text = "Overall \r\nDelay";
            // 
            // numBotDelay
            // 
            this.numBotDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numBotDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numBotDelay.Location = new System.Drawing.Point(52, 156);
            this.numBotDelay.LoopValues = false;
            this.numBotDelay.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.numBotDelay.Name = "numBotDelay";
            this.numBotDelay.Size = new System.Drawing.Size(48, 20);
            this.numBotDelay.TabIndex = 71;
            this.numBotDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // btnBotDelay
            // 
            this.btnBotDelay.Checked = false;
            this.btnBotDelay.Location = new System.Drawing.Point(102, 155);
            this.btnBotDelay.Name = "btnBotDelay";
            this.btnBotDelay.Size = new System.Drawing.Size(70, 23);
            this.btnBotDelay.TabIndex = 70;
            this.btnBotDelay.Text = "Set delay";
            this.btnBotDelay.Click += new System.EventHandler(this.btnBotDelay_Click);
            // 
            // txtPlayer
            // 
            this.txtPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlayer.Location = new System.Drawing.Point(178, 104);
            this.txtPlayer.Name = "txtPlayer";
            this.txtPlayer.Size = new System.Drawing.Size(127, 20);
            this.txtPlayer.TabIndex = 69;
            this.txtPlayer.Text = "Player name";
            this.txtPlayer.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtPlayer.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnGoto
            // 
            this.btnGoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoto.Checked = false;
            this.btnGoto.Location = new System.Drawing.Point(177, 129);
            this.btnGoto.Name = "btnGoto";
            this.btnGoto.Size = new System.Drawing.Size(128, 22);
            this.btnGoto.TabIndex = 68;
            this.btnGoto.Text = "Goto Command";
            this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Checked = false;
            this.btnLoad.Location = new System.Drawing.Point(370, 28);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(59, 22);
            this.btnLoad.TabIndex = 67;
            this.btnLoad.Text = "Load bot";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestart.Checked = false;
            this.btnRestart.Location = new System.Drawing.Point(370, 3);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(59, 22);
            this.btnRestart.TabIndex = 66;
            this.btnRestart.Text = "Restart bot";
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Checked = false;
            this.btnStop.Location = new System.Drawing.Point(311, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(58, 22);
            this.btnStop.TabIndex = 65;
            this.btnStop.Text = "Stop bot";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnLoadCmd
            // 
            this.btnLoadCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadCmd.Checked = false;
            this.btnLoadCmd.Location = new System.Drawing.Point(177, 53);
            this.btnLoadCmd.Name = "btnLoadCmd";
            this.btnLoadCmd.Size = new System.Drawing.Size(128, 22);
            this.btnLoadCmd.TabIndex = 63;
            this.btnLoadCmd.Text = "Load bot (command)";
            this.btnLoadCmd.Click += new System.EventHandler(this.btnLoadCmd_Click);
            // 
            // chkSkip
            // 
            this.chkSkip.AutoSize = true;
            this.chkSkip.Checked = true;
            this.chkSkip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSkip.Location = new System.Drawing.Point(7, 180);
            this.chkSkip.Name = "chkSkip";
            this.chkSkip.Size = new System.Drawing.Size(146, 17);
            this.chkSkip.TabIndex = 62;
            this.chkSkip.Text = "Skip bot delay for index/if";
            // 
            // btnStatementAdd
            // 
            this.btnStatementAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStatementAdd.Checked = false;
            this.btnStatementAdd.Location = new System.Drawing.Point(5, 129);
            this.btnStatementAdd.MaximumSize = new System.Drawing.Size(197, 20);
            this.btnStatementAdd.MinimumSize = new System.Drawing.Size(167, 20);
            this.btnStatementAdd.Name = "btnStatementAdd";
            this.btnStatementAdd.Size = new System.Drawing.Size(167, 20);
            this.btnStatementAdd.TabIndex = 61;
            this.btnStatementAdd.Text = "Add statement";
            this.btnStatementAdd.Click += new System.EventHandler(this.btnStatementAdd_Click);
            // 
            // txtStatement2
            // 
            this.txtStatement2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatement2.Location = new System.Drawing.Point(5, 105);
            this.txtStatement2.MaximumSize = new System.Drawing.Size(197, 20);
            this.txtStatement2.MinimumSize = new System.Drawing.Size(167, 20);
            this.txtStatement2.Name = "txtStatement2";
            this.txtStatement2.Size = new System.Drawing.Size(167, 20);
            this.txtStatement2.TabIndex = 60;
            // 
            // txtStatement1
            // 
            this.txtStatement1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatement1.Location = new System.Drawing.Point(5, 80);
            this.txtStatement1.MaximumSize = new System.Drawing.Size(197, 20);
            this.txtStatement1.MinimumSize = new System.Drawing.Size(167, 20);
            this.txtStatement1.Name = "txtStatement1";
            this.txtStatement1.Size = new System.Drawing.Size(167, 20);
            this.txtStatement1.TabIndex = 59;
            // 
            // cbStatement
            // 
            this.cbStatement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbStatement.FormattingEnabled = true;
            this.cbStatement.Location = new System.Drawing.Point(5, 55);
            this.cbStatement.MaximumSize = new System.Drawing.Size(197, 0);
            this.cbStatement.MinimumSize = new System.Drawing.Size(167, 0);
            this.cbStatement.Name = "cbStatement";
            this.cbStatement.Size = new System.Drawing.Size(167, 21);
            this.cbStatement.TabIndex = 58;
            this.cbStatement.SelectedIndexChanged += new System.EventHandler(this.cbStatement_SelectedIndexChanged);
            // 
            // cbCategories
            // 
            this.cbCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCategories.FormattingEnabled = true;
            this.cbCategories.Items.AddRange(new object[] {
            "Item",
            "This player",
            "Player",
            "Map",
            "Monster",
            "Quest",
            "Misc"});
            this.cbCategories.Location = new System.Drawing.Point(5, 30);
            this.cbCategories.MaximumSize = new System.Drawing.Size(197, 0);
            this.cbCategories.MinimumSize = new System.Drawing.Size(167, 0);
            this.cbCategories.Name = "cbCategories";
            this.cbCategories.Size = new System.Drawing.Size(167, 21);
            this.cbCategories.TabIndex = 57;
            this.cbCategories.SelectedIndexChanged += new System.EventHandler(this.cbCategories_SelectedIndexChanged);
            // 
            // txtPacket
            // 
            this.txtPacket.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPacket.Location = new System.Drawing.Point(4, 4);
            this.txtPacket.Name = "txtPacket";
            this.txtPacket.Size = new System.Drawing.Size(252, 20);
            this.txtPacket.TabIndex = 53;
            this.txtPacket.Text = "%xt%zm%cmd%1%tfer%PLAYERNAME%MAP-1e99%";
            // 
            // btnClientPacket
            // 
            this.btnClientPacket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClientPacket.Checked = false;
            this.btnClientPacket.Location = new System.Drawing.Point(178, 28);
            this.btnClientPacket.Name = "btnClientPacket";
            this.btnClientPacket.Size = new System.Drawing.Size(127, 22);
            this.btnClientPacket.TabIndex = 52;
            this.btnClientPacket.Text = "Client Packet";
            this.btnClientPacket.Click += new System.EventHandler(this.btnClientPacket_Click);
            // 
            // btnPacket
            // 
            this.btnPacket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPacket.Checked = false;
            this.btnPacket.Location = new System.Drawing.Point(253, 3);
            this.btnPacket.Name = "btnPacket";
            this.btnPacket.Size = new System.Drawing.Size(52, 22);
            this.btnPacket.TabIndex = 52;
            this.btnPacket.Text = "Packet";
            this.btnPacket.Click += new System.EventHandler(this.btnPacket_Click);
            // 
            // tabOptions
            // 
            this.tabOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabOptions.Controls.Add(this.pnlOptions);
            this.tabOptions.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabOptions.Location = new System.Drawing.Point(4, 23);
            this.tabOptions.Margin = new System.Windows.Forms.Padding(0);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions.Size = new System.Drawing.Size(444, 298);
            this.tabOptions.TabIndex = 5;
            this.tabOptions.Text = "Options";
            // 
            // pnlOptions
            // 
            this.pnlOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pnlOptions.Controls.Add(this.chkBuff);
            this.pnlOptions.Controls.Add(this.label6);
            this.pnlOptions.Controls.Add(this.numOptionsTimer);
            this.pnlOptions.Controls.Add(this.label5);
            this.pnlOptions.Controls.Add(this.chkUntarget);
            this.pnlOptions.Controls.Add(this.lstLogText);
            this.pnlOptions.Controls.Add(this.btnLogDebug);
            this.pnlOptions.Controls.Add(this.btnLog);
            this.pnlOptions.Controls.Add(this.txtLog);
            this.pnlOptions.Controls.Add(this.chkEnableSettings);
            this.pnlOptions.Controls.Add(this.chkDisableAnims);
            this.pnlOptions.Controls.Add(this.txtSoundItem);
            this.pnlOptions.Controls.Add(this.btnSoundAdd);
            this.pnlOptions.Controls.Add(this.btnSoundDelete);
            this.pnlOptions.Controls.Add(this.btnSoundTest);
            this.pnlOptions.Controls.Add(this.lstSoundItems);
            this.pnlOptions.Controls.Add(this.label9);
            this.pnlOptions.Controls.Add(this.numWalkSpeed);
            this.pnlOptions.Controls.Add(this.label8);
            this.pnlOptions.Controls.Add(this.chkSkipCutscenes);
            this.pnlOptions.Controls.Add(this.chkHidePlayers);
            this.pnlOptions.Controls.Add(this.chkLag);
            this.pnlOptions.Controls.Add(this.chkMagnet);
            this.pnlOptions.Controls.Add(this.chkProvoke);
            this.pnlOptions.Controls.Add(this.chkInfiniteRange);
            this.pnlOptions.Controls.Add(this.grpLogin);
            this.pnlOptions.Controls.Add(this.chkGender);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOptions.Location = new System.Drawing.Point(3, 3);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(438, 292);
            this.pnlOptions.TabIndex = 108;
            // 
            // chkBuff
            // 
            this.chkBuff.AutoSize = true;
            this.chkBuff.Location = new System.Drawing.Point(150, 184);
            this.chkBuff.Name = "chkBuff";
            this.chkBuff.Size = new System.Drawing.Size(60, 17);
            this.chkBuff.TabIndex = 158;
            this.chkBuff.Text = "Buff up";
            this.chkBuff.Visible = false;
            this.chkBuff.CheckedChanged += new System.EventHandler(this.chkBuffup_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label6.Location = new System.Drawing.Point(195, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 157;
            this.label6.Text = "Options Timer";
            // 
            // numOptionsTimer
            // 
            this.numOptionsTimer.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numOptionsTimer.Location = new System.Drawing.Point(150, 251);
            this.numOptionsTimer.LoopValues = false;
            this.numOptionsTimer.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numOptionsTimer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOptionsTimer.Name = "numOptionsTimer";
            this.numOptionsTimer.Size = new System.Drawing.Size(42, 20);
            this.numOptionsTimer.TabIndex = 156;
            this.numOptionsTimer.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.numOptionsTimer.ValueChanged += new System.EventHandler(this.numOptionsTimer_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label5.Location = new System.Drawing.Point(289, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 155;
            this.label5.Text = "Viable Log References";
            // 
            // chkUntarget
            // 
            this.chkUntarget.AutoSize = true;
            this.chkUntarget.Location = new System.Drawing.Point(150, 165);
            this.chkUntarget.Name = "chkUntarget";
            this.chkUntarget.Size = new System.Drawing.Size(86, 17);
            this.chkUntarget.TabIndex = 154;
            this.chkUntarget.Text = "Untarget self";
            this.chkUntarget.CheckedChanged += new System.EventHandler(this.chkUntarget_CheckedChanged);
            // 
            // lstLogText
            // 
            this.lstLogText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLogText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstLogText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstLogText.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstLogText.FormattingEnabled = true;
            this.lstLogText.Items.AddRange(new object[] {
            "{USERNAME}",
            "{MAP}",
            "{GOLD}",
            "{LEVEL}",
            "{CELL}",
            "{HEALTH}",
            "{TIME: 12}",
            "{TIME: 24}",
            "{CLEAR}",
            "{ITEM: item name}",
            "{ITEM MAX: item name}",
            "{REP XP: faction}",
            "{REP RANK: faction}",
            "{REP TOTAL: faction}",
            "{REP REMAINING: faction}",
            "{REP REQUIRED: faction}",
            "{REP CURRENT: faction}",
            "{INT VALUE: int}"});
            this.lstLogText.Location = new System.Drawing.Point(281, 122);
            this.lstLogText.Name = "lstLogText";
            this.lstLogText.Size = new System.Drawing.Size(252, 143);
            this.lstLogText.TabIndex = 153;
            this.lstLogText.DoubleClick += new System.EventHandler(this.lstLogText_DoubleClick);
            this.lstLogText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLogText_KeyDown);
            // 
            // btnLogDebug
            // 
            this.btnLogDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogDebug.Checked = false;
            this.btnLogDebug.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnLogDebug.Location = new System.Drawing.Point(353, 86);
            this.btnLogDebug.Name = "btnLogDebug";
            this.btnLogDebug.Size = new System.Drawing.Size(69, 22);
            this.btnLogDebug.TabIndex = 152;
            this.btnLogDebug.Text = "Log Debug";
            this.btnLogDebug.Click += new System.EventHandler(this.logDebug);
            // 
            // btnLog
            // 
            this.btnLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLog.Checked = false;
            this.btnLog.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnLog.Location = new System.Drawing.Point(281, 86);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(69, 22);
            this.btnLog.TabIndex = 148;
            this.btnLog.Text = "Log Script";
            this.btnLog.Click += new System.EventHandler(this.logScript);
            // 
            // txtLog
            // 
            this.txtLog.AcceptsReturn = true;
            this.txtLog.AcceptsTab = true;
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.AutoCompleteCustomSource.AddRange(new string[] {
            "{USERNAME}",
            "{MAP}",
            "{GOLD}",
            "{LEVEL}",
            "{CELL}",
            "{HEALTH}",
            "{TIME: 12}",
            "{TIME: 24}",
            "{CLEAR}",
            "{ITEM: item name}",
            "{ITEM MAX: item name}",
            "{REP XP: faction}",
            "{REP RANK: faction}",
            "{REP TOTAL: faction}",
            "{REP REMAINING: faction}",
            "{REP REQUIRED: faction}",
            "{REP CURRENT: faction}"});
            this.txtLog.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtLog.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtLog.Location = new System.Drawing.Point(281, 5);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(252, 225);
            this.txtLog.TabIndex = 147;
            this.txtLog.Text = "Logs";
            // 
            // chkEnableSettings
            // 
            this.chkEnableSettings.AutoSize = true;
            this.chkEnableSettings.Location = new System.Drawing.Point(151, 221);
            this.chkEnableSettings.Name = "chkEnableSettings";
            this.chkEnableSettings.Size = new System.Drawing.Size(97, 30);
            this.chkEnableSettings.TabIndex = 132;
            this.chkEnableSettings.Text = "Enable options\r\nwithout starting";
            this.chkEnableSettings.Click += new System.EventHandler(this.chkEnableSettings_Click);
            // 
            // chkDisableAnims
            // 
            this.chkDisableAnims.AutoSize = true;
            this.chkDisableAnims.Location = new System.Drawing.Point(150, 127);
            this.chkDisableAnims.Name = "chkDisableAnims";
            this.chkDisableAnims.Size = new System.Drawing.Size(122, 17);
            this.chkDisableAnims.TabIndex = 131;
            this.chkDisableAnims.Text = "Disable player anims";
            this.chkDisableAnims.CheckedChanged += new System.EventHandler(this.chkDisableAnims_CheckedChanged);
            // 
            // txtSoundItem
            // 
            this.txtSoundItem.Location = new System.Drawing.Point(6, 228);
            this.txtSoundItem.Name = "txtSoundItem";
            this.txtSoundItem.Size = new System.Drawing.Size(139, 20);
            this.txtSoundItem.TabIndex = 130;
            this.txtSoundItem.Enter += new System.EventHandler(this.TextboxEnter);
            this.txtSoundItem.Leave += new System.EventHandler(this.TextboxLeave);
            // 
            // btnSoundAdd
            // 
            this.btnSoundAdd.Checked = false;
            this.btnSoundAdd.Location = new System.Drawing.Point(51, 250);
            this.btnSoundAdd.Name = "btnSoundAdd";
            this.btnSoundAdd.Size = new System.Drawing.Size(43, 22);
            this.btnSoundAdd.TabIndex = 129;
            this.btnSoundAdd.Text = "Add";
            this.btnSoundAdd.Click += new System.EventHandler(this.btnSoundAdd_Click);
            // 
            // btnSoundDelete
            // 
            this.btnSoundDelete.Checked = false;
            this.btnSoundDelete.Location = new System.Drawing.Point(96, 250);
            this.btnSoundDelete.Name = "btnSoundDelete";
            this.btnSoundDelete.Size = new System.Drawing.Size(50, 22);
            this.btnSoundDelete.TabIndex = 128;
            this.btnSoundDelete.Text = "Delete";
            this.btnSoundDelete.Click += new System.EventHandler(this.btnSoundDelete_Click);
            // 
            // btnSoundTest
            // 
            this.btnSoundTest.Checked = false;
            this.btnSoundTest.Location = new System.Drawing.Point(5, 250);
            this.btnSoundTest.Name = "btnSoundTest";
            this.btnSoundTest.Size = new System.Drawing.Size(43, 22);
            this.btnSoundTest.TabIndex = 126;
            this.btnSoundTest.Text = "Test";
            this.btnSoundTest.Click += new System.EventHandler(this.btnSoundTest_Click);
            // 
            // lstSoundItems
            // 
            this.lstSoundItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.lstSoundItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSoundItems.ForeColor = System.Drawing.Color.Gainsboro;
            this.lstSoundItems.FormattingEnabled = true;
            this.lstSoundItems.Location = new System.Drawing.Point(6, 169);
            this.lstSoundItems.Name = "lstSoundItems";
            this.lstSoundItems.Size = new System.Drawing.Size(139, 54);
            this.lstSoundItems.TabIndex = 125;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label9.Location = new System.Drawing.Point(10, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 26);
            this.label9.TabIndex = 124;
            this.label9.Text = "If any of the following items\r\nare dropped, play a sound";
            // 
            // numWalkSpeed
            // 
            this.numWalkSpeed.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numWalkSpeed.Location = new System.Drawing.Point(214, 201);
            this.numWalkSpeed.LoopValues = false;
            this.numWalkSpeed.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numWalkSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWalkSpeed.Name = "numWalkSpeed";
            this.numWalkSpeed.Size = new System.Drawing.Size(28, 20);
            this.numWalkSpeed.TabIndex = 123;
            this.numWalkSpeed.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numWalkSpeed.ValueChanged += new System.EventHandler(this.numWalkSpeed_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label8.Location = new System.Drawing.Point(148, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 122;
            this.label8.Text = "Walk speed";
            // 
            // chkSkipCutscenes
            // 
            this.chkSkipCutscenes.AutoSize = true;
            this.chkSkipCutscenes.Location = new System.Drawing.Point(150, 107);
            this.chkSkipCutscenes.Name = "chkSkipCutscenes";
            this.chkSkipCutscenes.Size = new System.Drawing.Size(99, 17);
            this.chkSkipCutscenes.TabIndex = 121;
            this.chkSkipCutscenes.Text = "Skip cutscenes";
            this.chkSkipCutscenes.CheckedChanged += new System.EventHandler(this.chkSkipCutscenes_CheckedChanged);
            // 
            // chkHidePlayers
            // 
            this.chkHidePlayers.AutoSize = true;
            this.chkHidePlayers.Location = new System.Drawing.Point(150, 87);
            this.chkHidePlayers.Name = "chkHidePlayers";
            this.chkHidePlayers.Size = new System.Drawing.Size(84, 17);
            this.chkHidePlayers.TabIndex = 120;
            this.chkHidePlayers.Text = "Hide players";
            this.chkHidePlayers.CheckedChanged += new System.EventHandler(this.chkHidePlayers_CheckedChanged);
            // 
            // chkLag
            // 
            this.chkLag.AutoSize = true;
            this.chkLag.Location = new System.Drawing.Point(150, 66);
            this.chkLag.Name = "chkLag";
            this.chkLag.Size = new System.Drawing.Size(68, 17);
            this.chkLag.TabIndex = 119;
            this.chkLag.Text = "Lag killer";
            this.chkLag.CheckedChanged += new System.EventHandler(this.chkLag_CheckedChanged);
            // 
            // chkMagnet
            // 
            this.chkMagnet.AutoSize = true;
            this.chkMagnet.Location = new System.Drawing.Point(150, 46);
            this.chkMagnet.Name = "chkMagnet";
            this.chkMagnet.Size = new System.Drawing.Size(96, 17);
            this.chkMagnet.TabIndex = 118;
            this.chkMagnet.Text = "Enemy magnet";
            this.chkMagnet.CheckedChanged += new System.EventHandler(this.chkMagnet_CheckedChanged);
            // 
            // chkProvoke
            // 
            this.chkProvoke.AutoSize = true;
            this.chkProvoke.Location = new System.Drawing.Point(150, 25);
            this.chkProvoke.Name = "chkProvoke";
            this.chkProvoke.Size = new System.Drawing.Size(111, 17);
            this.chkProvoke.TabIndex = 117;
            this.chkProvoke.Text = "Provoke monsters";
            this.chkProvoke.CheckedChanged += new System.EventHandler(this.chkProvoke_CheckedChanged);
            // 
            // chkInfiniteRange
            // 
            this.chkInfiniteRange.AutoSize = true;
            this.chkInfiniteRange.Location = new System.Drawing.Point(150, 4);
            this.chkInfiniteRange.Name = "chkInfiniteRange";
            this.chkInfiniteRange.Size = new System.Drawing.Size(120, 17);
            this.chkInfiniteRange.TabIndex = 116;
            this.chkInfiniteRange.Text = "Infinite attack range";
            this.chkInfiniteRange.CheckedChanged += new System.EventHandler(this.chkInfiniteRange_CheckedChanged);
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.chkAFK);
            this.grpLogin.Controls.Add(this.cbServers);
            this.grpLogin.Controls.Add(this.chkRelogRetry);
            this.grpLogin.Controls.Add(this.chkRelog);
            this.grpLogin.Controls.Add(this.numRelogDelay);
            this.grpLogin.Controls.Add(this.label7);
            this.grpLogin.Location = new System.Drawing.Point(4, 3);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(141, 138);
            this.grpLogin.TabIndex = 115;
            this.grpLogin.TabStop = false;
            this.grpLogin.Text = "Auto relogin";
            // 
            // chkAFK
            // 
            this.chkAFK.AutoSize = true;
            this.chkAFK.Enabled = false;
            this.chkAFK.Location = new System.Drawing.Point(4, 62);
            this.chkAFK.Name = "chkAFK";
            this.chkAFK.Size = new System.Drawing.Size(100, 17);
            this.chkAFK.TabIndex = 159;
            this.chkAFK.Text = "Relogin on AFK";
            this.chkAFK.CheckedChanged += new System.EventHandler(this.chkAFK_CheckedChanged);
            // 
            // cbServers
            // 
            this.cbServers.FormattingEnabled = true;
            this.cbServers.Location = new System.Drawing.Point(4, 17);
            this.cbServers.Name = "cbServers";
            this.cbServers.Size = new System.Drawing.Size(123, 21);
            this.cbServers.TabIndex = 0;
            // 
            // chkRelogRetry
            // 
            this.chkRelogRetry.AutoSize = true;
            this.chkRelogRetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.chkRelogRetry.Location = new System.Drawing.Point(3, 120);
            this.chkRelogRetry.Name = "chkRelogRetry";
            this.chkRelogRetry.Size = new System.Drawing.Size(143, 17);
            this.chkRelogRetry.TabIndex = 88;
            this.chkRelogRetry.Text = "Relog again if in battleon";
            // 
            // chkRelog
            // 
            this.chkRelog.AutoSize = true;
            this.chkRelog.Location = new System.Drawing.Point(4, 43);
            this.chkRelog.Name = "chkRelog";
            this.chkRelog.Size = new System.Drawing.Size(82, 17);
            this.chkRelog.TabIndex = 1;
            this.chkRelog.Text = "Auto relogin";
            this.chkRelog.CheckedChanged += new System.EventHandler(this.chkRelog_CheckedChanged);
            // 
            // numRelogDelay
            // 
            this.numRelogDelay.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRelogDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numRelogDelay.Location = new System.Drawing.Point(3, 98);
            this.numRelogDelay.LoopValues = false;
            this.numRelogDelay.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numRelogDelay.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numRelogDelay.Name = "numRelogDelay";
            this.numRelogDelay.Size = new System.Drawing.Size(46, 20);
            this.numRelogDelay.TabIndex = 86;
            this.numRelogDelay.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.label7.Location = new System.Drawing.Point(0, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 13);
            this.label7.TabIndex = 87;
            this.label7.Text = "Delay before starting the bot";
            // 
            // chkGender
            // 
            this.chkGender.AutoSize = true;
            this.chkGender.Location = new System.Drawing.Point(150, 146);
            this.chkGender.Name = "chkGender";
            this.chkGender.Size = new System.Drawing.Size(89, 17);
            this.chkGender.TabIndex = 137;
            this.chkGender.Text = "Gender swap";
            this.chkGender.CheckedChanged += new System.EventHandler(this.changeGenderAsync);
            // 
            // tabOptions2
            // 
            this.tabOptions2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabOptions2.Controls.Add(this.groupBox1);
            this.tabOptions2.Controls.Add(this.btnSearchCmd);
            this.tabOptions2.Controls.Add(this.txtSearchCmd);
            this.tabOptions2.Controls.Add(this.numSetLevel);
            this.tabOptions2.Controls.Add(this.chkChangeRoomTag);
            this.tabOptions2.Controls.Add(this.chkChangeChat);
            this.tabOptions2.Controls.Add(this.chkSetJoinLevel);
            this.tabOptions2.Controls.Add(this.chkHideYulgarPlayers);
            this.tabOptions2.Controls.Add(this.chkAntiAfk);
            this.tabOptions2.Controls.Add(this.grpAccessLevel);
            this.tabOptions2.Controls.Add(this.grpAlignment);
            this.tabOptions2.Controls.Add(this.txtUsername);
            this.tabOptions2.Controls.Add(this.btnChangeNameCmd);
            this.tabOptions2.Controls.Add(this.btnchangeName);
            this.tabOptions2.Controls.Add(this.btnChangeGuildCmd);
            this.tabOptions2.Controls.Add(this.btnchangeGuild);
            this.tabOptions2.Controls.Add(this.txtGuild);
            this.tabOptions2.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabOptions2.Location = new System.Drawing.Point(4, 23);
            this.tabOptions2.Margin = new System.Windows.Forms.Padding(0);
            this.tabOptions2.Name = "tabOptions2";
            this.tabOptions2.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions2.Size = new System.Drawing.Size(444, 298);
            this.tabOptions2.TabIndex = 7;
            this.tabOptions2.Text = "Client";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddInfoMsg);
            this.groupBox1.Controls.Add(this.btnAddWarnMsg);
            this.groupBox1.Controls.Add(this.inputMsgClient);
            this.groupBox1.Location = new System.Drawing.Point(190, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 101);
            this.groupBox1.TabIndex = 148;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Messages";
            // 
            // btnAddInfoMsg
            // 
            this.btnAddInfoMsg.Checked = false;
            this.btnAddInfoMsg.Location = new System.Drawing.Point(6, 70);
            this.btnAddInfoMsg.Name = "btnAddInfoMsg";
            this.btnAddInfoMsg.Size = new System.Drawing.Size(112, 23);
            this.btnAddInfoMsg.TabIndex = 150;
            this.btnAddInfoMsg.Text = "Add Info";
            // 
            // btnAddWarnMsg
            // 
            this.btnAddWarnMsg.Checked = false;
            this.btnAddWarnMsg.Location = new System.Drawing.Point(6, 43);
            this.btnAddWarnMsg.Name = "btnAddWarnMsg";
            this.btnAddWarnMsg.Size = new System.Drawing.Size(112, 23);
            this.btnAddWarnMsg.TabIndex = 149;
            this.btnAddWarnMsg.Text = "Add Warning";
            // 
            // inputMsgClient
            // 
            this.inputMsgClient.Location = new System.Drawing.Point(6, 18);
            this.inputMsgClient.Name = "inputMsgClient";
            this.inputMsgClient.Size = new System.Drawing.Size(112, 20);
            this.inputMsgClient.TabIndex = 147;
            this.inputMsgClient.Text = "Hello World!";
            // 
            // btnSearchCmd
            // 
            this.btnSearchCmd.Checked = false;
            this.btnSearchCmd.Location = new System.Drawing.Point(189, 36);
            this.btnSearchCmd.Name = "btnSearchCmd";
            this.btnSearchCmd.Size = new System.Drawing.Size(125, 23);
            this.btnSearchCmd.TabIndex = 147;
            this.btnSearchCmd.Text = "Search";
            this.btnSearchCmd.Click += new System.EventHandler(this.btnSearchCmd_Click);
            // 
            // txtSearchCmd
            // 
            this.txtSearchCmd.Location = new System.Drawing.Point(190, 13);
            this.txtSearchCmd.Name = "txtSearchCmd";
            this.txtSearchCmd.Size = new System.Drawing.Size(124, 20);
            this.txtSearchCmd.TabIndex = 146;
            this.txtSearchCmd.Text = "Search Commands";
            // 
            // numSetLevel
            // 
            this.numSetLevel.Enabled = false;
            this.numSetLevel.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSetLevel.Location = new System.Drawing.Point(12, 195);
            this.numSetLevel.LoopValues = false;
            this.numSetLevel.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSetLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSetLevel.Name = "numSetLevel";
            this.numSetLevel.Size = new System.Drawing.Size(51, 20);
            this.numSetLevel.TabIndex = 145;
            this.numSetLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkChangeRoomTag
            // 
            this.chkChangeRoomTag.AutoSize = true;
            this.chkChangeRoomTag.Location = new System.Drawing.Point(12, 221);
            this.chkChangeRoomTag.Name = "chkChangeRoomTag";
            this.chkChangeRoomTag.Size = new System.Drawing.Size(107, 17);
            this.chkChangeRoomTag.TabIndex = 144;
            this.chkChangeRoomTag.Text = "Anonymous room";
            this.chkChangeRoomTag.CheckedChanged += new System.EventHandler(this.chkChangeRoomTag_CheckedChanged);
            // 
            // chkChangeChat
            // 
            this.chkChangeChat.AutoSize = true;
            this.chkChangeChat.Location = new System.Drawing.Point(12, 243);
            this.chkChangeChat.Name = "chkChangeChat";
            this.chkChangeChat.Size = new System.Drawing.Size(105, 17);
            this.chkChangeChat.TabIndex = 144;
            this.chkChangeChat.Text = "Anonymous chat";
            this.chkChangeChat.CheckedChanged += new System.EventHandler(this.chkChangeChat_CheckedChanged);
            // 
            // chkSetJoinLevel
            // 
            this.chkSetJoinLevel.AutoSize = true;
            this.chkSetJoinLevel.Enabled = false;
            this.chkSetJoinLevel.Location = new System.Drawing.Point(69, 197);
            this.chkSetJoinLevel.Name = "chkSetJoinLevel";
            this.chkSetJoinLevel.Size = new System.Drawing.Size(110, 17);
            this.chkSetJoinLevel.TabIndex = 144;
            this.chkSetJoinLevel.Text = "Toggle Join Level";
            this.chkSetJoinLevel.CheckedChanged += new System.EventHandler(this.chkSetJoinLevel_CheckedChanged);
            // 
            // chkHideYulgarPlayers
            // 
            this.chkHideYulgarPlayers.AutoSize = true;
            this.chkHideYulgarPlayers.Location = new System.Drawing.Point(12, 266);
            this.chkHideYulgarPlayers.Name = "chkHideYulgarPlayers";
            this.chkHideYulgarPlayers.Size = new System.Drawing.Size(126, 17);
            this.chkHideYulgarPlayers.TabIndex = 144;
            this.chkHideYulgarPlayers.Text = "Hide Players Upstairs";
            this.chkHideYulgarPlayers.CheckedChanged += new System.EventHandler(this.chkHideYulgarPlayers_CheckedChanged);
            // 
            // chkAntiAfk
            // 
            this.chkAntiAfk.AutoSize = true;
            this.chkAntiAfk.Location = new System.Drawing.Point(121, 221);
            this.chkAntiAfk.Name = "chkAntiAfk";
            this.chkAntiAfk.Size = new System.Drawing.Size(67, 17);
            this.chkAntiAfk.TabIndex = 144;
            this.chkAntiAfk.Text = "Anti-AFK";
            this.chkAntiAfk.CheckedChanged += new System.EventHandler(this.chkAntiAfk_CheckedChanged);
            // 
            // grpAccessLevel
            // 
            this.grpAccessLevel.Controls.Add(this.chkToggleMute);
            this.grpAccessLevel.Controls.Add(this.btnSetMem);
            this.grpAccessLevel.Controls.Add(this.btnSetModerator);
            this.grpAccessLevel.Controls.Add(this.btnSetNonMem);
            this.grpAccessLevel.Location = new System.Drawing.Point(98, 6);
            this.grpAccessLevel.Name = "grpAccessLevel";
            this.grpAccessLevel.Size = new System.Drawing.Size(86, 131);
            this.grpAccessLevel.TabIndex = 6;
            this.grpAccessLevel.TabStop = false;
            this.grpAccessLevel.Text = "Access";
            // 
            // chkToggleMute
            // 
            this.chkToggleMute.AutoSize = true;
            this.chkToggleMute.Location = new System.Drawing.Point(4, 107);
            this.chkToggleMute.Name = "chkToggleMute";
            this.chkToggleMute.Size = new System.Drawing.Size(86, 17);
            this.chkToggleMute.TabIndex = 6;
            this.chkToggleMute.Text = "Toggle Mute";
            this.chkToggleMute.CheckedChanged += new System.EventHandler(this.chkToggleMute_CheckedChanged);
            // 
            // btnSetMem
            // 
            this.btnSetMem.Checked = false;
            this.btnSetMem.Location = new System.Drawing.Point(6, 19);
            this.btnSetMem.Name = "btnSetMem";
            this.btnSetMem.Size = new System.Drawing.Size(75, 23);
            this.btnSetMem.TabIndex = 3;
            this.btnSetMem.Text = "Member";
            this.btnSetMem.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // btnSetModerator
            // 
            this.btnSetModerator.Checked = false;
            this.btnSetModerator.Location = new System.Drawing.Point(6, 77);
            this.btnSetModerator.Name = "btnSetModerator";
            this.btnSetModerator.Size = new System.Drawing.Size(75, 23);
            this.btnSetModerator.TabIndex = 5;
            this.btnSetModerator.Text = "Moderator";
            this.btnSetModerator.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // btnSetNonMem
            // 
            this.btnSetNonMem.Checked = false;
            this.btnSetNonMem.Location = new System.Drawing.Point(6, 48);
            this.btnSetNonMem.Name = "btnSetNonMem";
            this.btnSetNonMem.Size = new System.Drawing.Size(75, 23);
            this.btnSetNonMem.TabIndex = 4;
            this.btnSetNonMem.Text = "Non-Mem";
            this.btnSetNonMem.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // grpAlignment
            // 
            this.grpAlignment.Controls.Add(this.btnSetChaos);
            this.grpAlignment.Controls.Add(this.btnSetUndecided);
            this.grpAlignment.Controls.Add(this.btnSetGood);
            this.grpAlignment.Controls.Add(this.btnSetEvil);
            this.grpAlignment.Location = new System.Drawing.Point(6, 6);
            this.grpAlignment.Name = "grpAlignment";
            this.grpAlignment.Size = new System.Drawing.Size(86, 131);
            this.grpAlignment.TabIndex = 1;
            this.grpAlignment.TabStop = false;
            this.grpAlignment.Text = "Alignment";
            // 
            // btnSetChaos
            // 
            this.btnSetChaos.Checked = false;
            this.btnSetChaos.Location = new System.Drawing.Point(6, 74);
            this.btnSetChaos.Name = "btnSetChaos";
            this.btnSetChaos.Size = new System.Drawing.Size(75, 23);
            this.btnSetChaos.TabIndex = 0;
            this.btnSetChaos.Text = "Chaos";
            this.btnSetChaos.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // btnSetUndecided
            // 
            this.btnSetUndecided.Checked = false;
            this.btnSetUndecided.Location = new System.Drawing.Point(6, 103);
            this.btnSetUndecided.Name = "btnSetUndecided";
            this.btnSetUndecided.Size = new System.Drawing.Size(75, 23);
            this.btnSetUndecided.TabIndex = 0;
            this.btnSetUndecided.Text = "Undecided";
            this.btnSetUndecided.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // btnSetGood
            // 
            this.btnSetGood.Checked = false;
            this.btnSetGood.Location = new System.Drawing.Point(6, 16);
            this.btnSetGood.Name = "btnSetGood";
            this.btnSetGood.Size = new System.Drawing.Size(75, 23);
            this.btnSetGood.TabIndex = 0;
            this.btnSetGood.Text = "Good";
            this.btnSetGood.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // btnSetEvil
            // 
            this.btnSetEvil.Checked = false;
            this.btnSetEvil.Location = new System.Drawing.Point(6, 45);
            this.btnSetEvil.Name = "btnSetEvil";
            this.btnSetEvil.Size = new System.Drawing.Size(75, 23);
            this.btnSetEvil.TabIndex = 0;
            this.btnSetEvil.Text = "Evil";
            this.btnSetEvil.Click += new System.EventHandler(this.btnSetHero_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(10, 146);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(78, 20);
            this.txtUsername.TabIndex = 135;
            this.txtUsername.Text = "Username";
            // 
            // btnChangeNameCmd
            // 
            this.btnChangeNameCmd.AutoSize = true;
            this.btnChangeNameCmd.Checked = false;
            this.btnChangeNameCmd.Location = new System.Drawing.Point(136, 145);
            this.btnChangeNameCmd.Name = "btnChangeNameCmd";
            this.btnChangeNameCmd.Size = new System.Drawing.Size(53, 33);
            this.btnChangeNameCmd.TabIndex = 142;
            this.btnChangeNameCmd.Text = "(cmd)";
            this.btnChangeNameCmd.Click += new System.EventHandler(this.btnChangeCmd_Click);
            // 
            // btnchangeName
            // 
            this.btnchangeName.AutoSize = true;
            this.btnchangeName.Checked = false;
            this.btnchangeName.Location = new System.Drawing.Point(90, 145);
            this.btnchangeName.Name = "btnchangeName";
            this.btnchangeName.Size = new System.Drawing.Size(45, 33);
            this.btnchangeName.TabIndex = 142;
            this.btnchangeName.Text = "Set";
            this.btnchangeName.Click += new System.EventHandler(this.btnchangeName_Click);
            // 
            // btnChangeGuildCmd
            // 
            this.btnChangeGuildCmd.AutoSize = true;
            this.btnChangeGuildCmd.Checked = false;
            this.btnChangeGuildCmd.Location = new System.Drawing.Point(136, 168);
            this.btnChangeGuildCmd.Name = "btnChangeGuildCmd";
            this.btnChangeGuildCmd.Size = new System.Drawing.Size(53, 33);
            this.btnChangeGuildCmd.TabIndex = 143;
            this.btnChangeGuildCmd.Text = "(cmd)";
            this.btnChangeGuildCmd.Click += new System.EventHandler(this.btnChangeCmd_Click);
            // 
            // btnchangeGuild
            // 
            this.btnchangeGuild.AutoSize = true;
            this.btnchangeGuild.Checked = false;
            this.btnchangeGuild.Location = new System.Drawing.Point(90, 168);
            this.btnchangeGuild.Name = "btnchangeGuild";
            this.btnchangeGuild.Size = new System.Drawing.Size(45, 33);
            this.btnchangeGuild.TabIndex = 143;
            this.btnchangeGuild.Text = "Set";
            this.btnchangeGuild.Click += new System.EventHandler(this.btnchangeGuild_Click);
            // 
            // txtGuild
            // 
            this.txtGuild.Location = new System.Drawing.Point(10, 169);
            this.txtGuild.Name = "txtGuild";
            this.txtGuild.Size = new System.Drawing.Size(78, 20);
            this.txtGuild.TabIndex = 136;
            this.txtGuild.Text = "Guild";
            // 
            // tabBots
            // 
            this.tabBots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabBots.Controls.Add(this.pnlSaved);
            this.tabBots.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabBots.Location = new System.Drawing.Point(4, 23);
            this.tabBots.Margin = new System.Windows.Forms.Padding(0);
            this.tabBots.Name = "tabBots";
            this.tabBots.Padding = new System.Windows.Forms.Padding(3);
            this.tabBots.Size = new System.Drawing.Size(444, 298);
            this.tabBots.TabIndex = 6;
            this.tabBots.Text = "Bots";
            // 
            // pnlSaved
            // 
            this.pnlSaved.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.pnlSaved.Controls.Add(this.lblBoosts);
            this.pnlSaved.Controls.Add(this.lblDrops);
            this.pnlSaved.Controls.Add(this.lblQuests);
            this.pnlSaved.Controls.Add(this.lblSkills);
            this.pnlSaved.Controls.Add(this.lblCommands);
            this.pnlSaved.Controls.Add(this.lblItems);
            this.pnlSaved.Controls.Add(this.txtSavedDesc);
            this.pnlSaved.Controls.Add(this.txtSavedAuthor);
            this.pnlSaved.Controls.Add(this.lblBots);
            this.pnlSaved.Controls.Add(this.treeBots);
            this.pnlSaved.Controls.Add(this.txtSavedAdd);
            this.pnlSaved.Controls.Add(this.btnSavedAdd);
            this.pnlSaved.Controls.Add(this.txtSaved);
            this.pnlSaved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSaved.Location = new System.Drawing.Point(3, 3);
            this.pnlSaved.Name = "pnlSaved";
            this.pnlSaved.Size = new System.Drawing.Size(438, 292);
            this.pnlSaved.TabIndex = 109;
            // 
            // lblBoosts
            // 
            this.lblBoosts.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblBoosts.AutoSize = true;
            this.lblBoosts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblBoosts.Location = new System.Drawing.Point(251, 259);
            this.lblBoosts.Name = "lblBoosts";
            this.lblBoosts.Size = new System.Drawing.Size(42, 13);
            this.lblBoosts.TabIndex = 25;
            this.lblBoosts.Text = "Boosts:";
            this.lblBoosts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblDrops
            // 
            this.lblDrops.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblDrops.AutoSize = true;
            this.lblDrops.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblDrops.Location = new System.Drawing.Point(195, 259);
            this.lblDrops.Name = "lblDrops";
            this.lblDrops.Size = new System.Drawing.Size(38, 13);
            this.lblDrops.TabIndex = 24;
            this.lblDrops.Text = "Drops:";
            this.lblDrops.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblQuests
            // 
            this.lblQuests.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblQuests.AutoSize = true;
            this.lblQuests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblQuests.Location = new System.Drawing.Point(137, 259);
            this.lblQuests.Name = "lblQuests";
            this.lblQuests.Size = new System.Drawing.Size(43, 13);
            this.lblQuests.TabIndex = 23;
            this.lblQuests.Text = "Quests:";
            this.lblQuests.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblSkills
            // 
            this.lblSkills.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblSkills.AutoSize = true;
            this.lblSkills.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblSkills.Location = new System.Drawing.Point(87, 259);
            this.lblSkills.Name = "lblSkills";
            this.lblSkills.Size = new System.Drawing.Size(34, 13);
            this.lblSkills.TabIndex = 22;
            this.lblSkills.Text = "Skills:";
            this.lblSkills.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblCommands
            // 
            this.lblCommands.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblCommands.AutoSize = true;
            this.lblCommands.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblCommands.Location = new System.Drawing.Point(6, 246);
            this.lblCommands.Name = "lblCommands";
            this.lblCommands.Size = new System.Drawing.Size(62, 26);
            this.lblCommands.TabIndex = 21;
            this.lblCommands.Text = "Number of\r\nCommands:";
            this.lblCommands.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblItems
            // 
            this.lblItems.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblItems.AutoSize = true;
            this.lblItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblItems.Location = new System.Drawing.Point(313, 259);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(35, 13);
            this.lblItems.TabIndex = 146;
            this.lblItems.Text = "Items:";
            this.lblItems.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtSavedDesc
            // 
            this.txtSavedDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSavedDesc.Location = new System.Drawing.Point(247, 85);
            this.txtSavedDesc.Multiline = true;
            this.txtSavedDesc.Name = "txtSavedDesc";
            this.txtSavedDesc.Size = new System.Drawing.Size(188, 225);
            this.txtSavedDesc.TabIndex = 20;
            this.txtSavedDesc.Text = "Description";
            // 
            // txtSavedAuthor
            // 
            this.txtSavedAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSavedAuthor.Location = new System.Drawing.Point(247, 64);
            this.txtSavedAuthor.Name = "txtSavedAuthor";
            this.txtSavedAuthor.Size = new System.Drawing.Size(188, 20);
            this.txtSavedAuthor.TabIndex = 19;
            this.txtSavedAuthor.Text = "Author";
            // 
            // lblBots
            // 
            this.lblBots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBots.AutoSize = true;
            this.lblBots.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblBots.Location = new System.Drawing.Point(242, 50);
            this.lblBots.Name = "lblBots";
            this.lblBots.Size = new System.Drawing.Size(83, 13);
            this.lblBots.TabIndex = 18;
            this.lblBots.Text = "Number of Bots:";
            // 
            // treeBots
            // 
            this.treeBots.AllowDrop = true;
            this.treeBots.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeBots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.treeBots.Location = new System.Drawing.Point(4, 27);
            this.treeBots.Name = "treeBots";
            this.treeBots.Size = new System.Drawing.Size(252, 225);
            this.treeBots.TabIndex = 17;
            this.treeBots.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeBots_AfterExpand);
            this.treeBots.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeBots_AfterSelect);
            // 
            // txtSavedAdd
            // 
            this.txtSavedAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSavedAdd.Location = new System.Drawing.Point(247, 27);
            this.txtSavedAdd.Name = "txtSavedAdd";
            this.txtSavedAdd.Size = new System.Drawing.Size(121, 20);
            this.txtSavedAdd.TabIndex = 16;
            // 
            // btnSavedAdd
            // 
            this.btnSavedAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSavedAdd.Checked = false;
            this.btnSavedAdd.Location = new System.Drawing.Point(371, 27);
            this.btnSavedAdd.Name = "btnSavedAdd";
            this.btnSavedAdd.Size = new System.Drawing.Size(64, 22);
            this.btnSavedAdd.TabIndex = 15;
            this.btnSavedAdd.Text = "Add folder";
            this.btnSavedAdd.Click += new System.EventHandler(this.btnSavedAdd_Click);
            // 
            // txtSaved
            // 
            this.txtSaved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSaved.Location = new System.Drawing.Point(4, 4);
            this.txtSaved.Name = "txtSaved";
            this.txtSaved.Size = new System.Drawing.Size(431, 20);
            this.txtSaved.TabIndex = 13;
            this.txtSaved.TextChanged += new System.EventHandler(this.txtSaved_TextChanged);
            // 
            // tabMisc2
            // 
            this.tabMisc2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.tabMisc2.Controls.Add(this.numDelayedPacket);
            this.tabMisc2.Controls.Add(this.btnDelayedPacket);
            this.tabMisc2.Controls.Add(this.numSpammerDelay);
            this.tabMisc2.Controls.Add(this.btnSpammerRefresh);
            this.tabMisc2.Controls.Add(this.txtPacketSpammer);
            this.tabMisc2.Controls.Add(this.btnSpammerAdd);
            this.tabMisc2.Controls.Add(this.btnSpammerStop);
            this.tabMisc2.Controls.Add(this.btnSpammerStart);
            this.tabMisc2.Controls.Add(this.btnSpammerRemove);
            this.tabMisc2.Controls.Add(this.btnSpammerSetDelay);
            this.tabMisc2.Controls.Add(this.btnSpammerReset);
            this.tabMisc2.ForeColor = System.Drawing.Color.Gainsboro;
            this.tabMisc2.Location = new System.Drawing.Point(4, 23);
            this.tabMisc2.Margin = new System.Windows.Forms.Padding(0);
            this.tabMisc2.Name = "tabMisc2";
            this.tabMisc2.Padding = new System.Windows.Forms.Padding(3);
            this.tabMisc2.Size = new System.Drawing.Size(444, 298);
            this.tabMisc2.TabIndex = 8;
            this.tabMisc2.Text = "Misc 2";
            // 
            // numDelayedPacket
            // 
            this.numDelayedPacket.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numDelayedPacket.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numDelayedPacket.Location = new System.Drawing.Point(278, 64);
            this.numDelayedPacket.LoopValues = false;
            this.numDelayedPacket.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numDelayedPacket.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDelayedPacket.Name = "numDelayedPacket";
            this.numDelayedPacket.Size = new System.Drawing.Size(48, 20);
            this.numDelayedPacket.TabIndex = 58;
            this.numDelayedPacket.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // btnDelayedPacket
            // 
            this.btnDelayedPacket.Checked = false;
            this.btnDelayedPacket.Location = new System.Drawing.Point(154, 62);
            this.btnDelayedPacket.Name = "btnDelayedPacket";
            this.btnDelayedPacket.Size = new System.Drawing.Size(118, 23);
            this.btnDelayedPacket.TabIndex = 57;
            this.btnDelayedPacket.Text = "Add Delayed Packet";
            // 
            // numSpammerDelay
            // 
            this.numSpammerDelay.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numSpammerDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.numSpammerDelay.Location = new System.Drawing.Point(104, 65);
            this.numSpammerDelay.LoopValues = false;
            this.numSpammerDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numSpammerDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numSpammerDelay.Name = "numSpammerDelay";
            this.numSpammerDelay.Size = new System.Drawing.Size(44, 20);
            this.numSpammerDelay.TabIndex = 56;
            this.numSpammerDelay.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnSpammerRefresh
            // 
            this.btnSpammerRefresh.Checked = false;
            this.btnSpammerRefresh.Location = new System.Drawing.Point(216, 6);
            this.btnSpammerRefresh.Name = "btnSpammerRefresh";
            this.btnSpammerRefresh.Size = new System.Drawing.Size(86, 23);
            this.btnSpammerRefresh.TabIndex = 55;
            this.btnSpammerRefresh.Text = "Restart";
            this.btnSpammerRefresh.Click += new System.EventHandler(this.btnSpammer_Click);
            // 
            // txtPacketSpammer
            // 
            this.txtPacketSpammer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPacketSpammer.Location = new System.Drawing.Point(154, 38);
            this.txtPacketSpammer.Name = "txtPacketSpammer";
            this.txtPacketSpammer.Size = new System.Drawing.Size(284, 20);
            this.txtPacketSpammer.TabIndex = 54;
            this.txtPacketSpammer.Text = "%xt%zm%cmd%1%tfer%PLAYERNAME%MAP-1e99%";
            // 
            // btnSpammerStop
            // 
            this.btnSpammerStop.Checked = false;
            this.btnSpammerStop.Location = new System.Drawing.Point(376, 6);
            this.btnSpammerStop.Name = "btnSpammerStop";
            this.btnSpammerStop.Size = new System.Drawing.Size(65, 23);
            this.btnSpammerStop.TabIndex = 0;
            this.btnSpammerStop.Text = "Stop";
            this.btnSpammerStop.Click += new System.EventHandler(this.btnSpammer_Click);
            // 
            // btnSpammerStart
            // 
            this.btnSpammerStart.Checked = false;
            this.btnSpammerStart.Location = new System.Drawing.Point(308, 6);
            this.btnSpammerStart.Name = "btnSpammerStart";
            this.btnSpammerStart.Size = new System.Drawing.Size(62, 23);
            this.btnSpammerStart.TabIndex = 0;
            this.btnSpammerStart.Text = "Start";
            this.btnSpammerStart.Click += new System.EventHandler(this.btnSpammer_Click);
            // 
            // btnSpammerRemove
            // 
            this.btnSpammerRemove.Checked = false;
            this.btnSpammerRemove.Location = new System.Drawing.Point(95, 6);
            this.btnSpammerRemove.Name = "btnSpammerRemove";
            this.btnSpammerRemove.Size = new System.Drawing.Size(115, 23);
            this.btnSpammerRemove.TabIndex = 0;
            this.btnSpammerRemove.Text = "Remove";
            this.btnSpammerRemove.Click += new System.EventHandler(this.btnSpammer_Click);
            // 
            // btnSpammerSetDelay
            // 
            this.btnSpammerSetDelay.Checked = false;
            this.btnSpammerSetDelay.Location = new System.Drawing.Point(6, 62);
            this.btnSpammerSetDelay.Name = "btnSpammerSetDelay";
            this.btnSpammerSetDelay.Size = new System.Drawing.Size(92, 23);
            this.btnSpammerSetDelay.TabIndex = 0;
            this.btnSpammerSetDelay.Text = "Set Delay";
            this.btnSpammerSetDelay.Click += new System.EventHandler(this.btnSpammer_Click);
            // 
            // btnSpammerReset
            // 
            this.btnSpammerReset.Checked = false;
            this.btnSpammerReset.Location = new System.Drawing.Point(6, 6);
            this.btnSpammerReset.Name = "btnSpammerReset";
            this.btnSpammerReset.Size = new System.Drawing.Size(83, 23);
            this.btnSpammerReset.TabIndex = 0;
            this.btnSpammerReset.Text = "Clear";
            this.btnSpammerReset.Click += new System.EventHandler(this.btnSpammer_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 252);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.cbLists);
            this.splitContainer1.Panel1.Controls.Add(this.chkAll);
            this.splitContainer1.Panel1.Controls.Add(this.btnClear);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(238, 75);
            this.splitContainer1.SplitterDistance = 118;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 149;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.btnDown);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(116, 22);
            this.panel3.TabIndex = 148;
            // 
            // btnDown
            // 
            this.btnDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDown.Checked = false;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDown.Location = new System.Drawing.Point(0, 0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(116, 22);
            this.btnDown.TabIndex = 166;
            this.btnDown.Text = "▼";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // cbLists
            // 
            this.cbLists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLists.FormattingEnabled = true;
            this.cbLists.Items.AddRange(new object[] {
            "Commands",
            "Skills",
            "Quests",
            "Drops",
            "Boosts",
            "Items"});
            this.cbLists.Location = new System.Drawing.Point(1, 51);
            this.cbLists.Name = "cbLists";
            this.cbLists.Size = new System.Drawing.Size(115, 21);
            this.cbLists.TabIndex = 169;
            this.cbLists.SelectedIndexChanged += new System.EventHandler(this.cbLists_SelectedIndexChanged);
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(80, 28);
            this.chkAll.Name = "chkAll";
            this.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAll.Size = new System.Drawing.Size(36, 17);
            this.chkAll.TabIndex = 168;
            this.chkAll.Text = "all";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Checked = false;
            this.btnClear.Location = new System.Drawing.Point(0, 26);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(79, 22);
            this.btnClear.TabIndex = 167;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.panel4.Controls.Add(this.btnRemove);
            this.panel4.Controls.Add(this.btnBotStart);
            this.panel4.Controls.Add(this.btnBotStop);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 22);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(119, 51);
            this.panel4.TabIndex = 148;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRemove.Checked = false;
            this.btnRemove.Location = new System.Drawing.Point(0, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(119, 22);
            this.btnRemove.TabIndex = 166;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnBotStart
            // 
            this.btnBotStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBotStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBotStart.Checked = false;
            this.btnBotStart.Location = new System.Drawing.Point(0, 27);
            this.btnBotStart.Name = "btnBotStart";
            this.btnBotStart.Size = new System.Drawing.Size(119, 22);
            this.btnBotStart.TabIndex = 167;
            this.btnBotStart.Text = "Start";
            this.btnBotStart.Click += new System.EventHandler(this.btnBotStart_ClickAsync);
            // 
            // btnBotStop
            // 
            this.btnBotStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBotStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBotStop.Checked = false;
            this.btnBotStop.Location = new System.Drawing.Point(0, 27);
            this.btnBotStop.Name = "btnBotStop";
            this.btnBotStop.Size = new System.Drawing.Size(119, 22);
            this.btnBotStop.TabIndex = 168;
            this.btnBotStop.Text = "Stop";
            this.btnBotStop.Visible = false;
            this.btnBotStop.Click += new System.EventHandler(this.btnBotStop_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.btnUp);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(119, 22);
            this.panel2.TabIndex = 147;
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUp.Checked = false;
            this.btnUp.Location = new System.Drawing.Point(0, 0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(119, 22);
            this.btnUp.TabIndex = 165;
            this.btnUp.Text = "▲";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.lstCommands);
            this.panel1.Controls.Add(this.lstDrops);
            this.panel1.Controls.Add(this.lstBoosts);
            this.panel1.Controls.Add(this.lstQuests);
            this.panel1.Controls.Add(this.lstSkills);
            this.panel1.Controls.Add(this.lstItems);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 325);
            this.panel1.TabIndex = 150;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(7, 7);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(10);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            this.splitContainer2.Panel1MinSize = 30;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(694, 325);
            this.splitContainer2.SplitterDistance = 238;
            this.splitContainer2.TabIndex = 150;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(150, 184);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 17);
            this.checkBox1.TabIndex = 158;
            this.checkBox1.Text = "Placeholder";
            // 
            // chkBuffup
            // 
            this.chkBuffup.Location = new System.Drawing.Point(0, 0);
            this.chkBuffup.Name = "chkBuffup";
            this.chkBuffup.Size = new System.Drawing.Size(104, 24);
            this.chkBuffup.TabIndex = 0;
            // 
            // BotManagerMenuStrip
            // 
            this.BotManagerMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeFontsToolStripMenuItem,
            this.multilineToggleToolStripMenuItem,
            this.toggleTabpagesToolStripMenuItem,
            this.commandColorsToolStripMenuItem});
            this.BotManagerMenuStrip.Name = "contextMenuStrip1";
            this.BotManagerMenuStrip.Size = new System.Drawing.Size(195, 92);
            // 
            // changeFontsToolStripMenuItem
            // 
            this.changeFontsToolStripMenuItem.Name = "changeFontsToolStripMenuItem";
            this.changeFontsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.changeFontsToolStripMenuItem.Text = "Change Fonts";
            this.changeFontsToolStripMenuItem.Click += new System.EventHandler(this.changeFontsToolStripMenuItem_Click);
            // 
            // multilineToggleToolStripMenuItem
            // 
            this.multilineToggleToolStripMenuItem.Name = "multilineToggleToolStripMenuItem";
            this.multilineToggleToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.multilineToggleToolStripMenuItem.Text = "Multiline Toggle";
            this.multilineToggleToolStripMenuItem.Click += new System.EventHandler(this.multilineToggleToolStripMenuItem_Click);
            // 
            // toggleTabpagesToolStripMenuItem
            // 
            this.toggleTabpagesToolStripMenuItem.Name = "toggleTabpagesToolStripMenuItem";
            this.toggleTabpagesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.toggleTabpagesToolStripMenuItem.Text = "Toggle Tabpages";
            this.toggleTabpagesToolStripMenuItem.Click += new System.EventHandler(this.toggleTabpagesToolStripMenuItem_Click);
            // 
            // commandColorsToolStripMenuItem
            // 
            this.commandColorsToolStripMenuItem.Name = "commandColorsToolStripMenuItem";
            this.commandColorsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.commandColorsToolStripMenuItem.Text = "Command Customizer";
            this.commandColorsToolStripMenuItem.Click += new System.EventHandler(this.commandColorsToolStripMenuItem_Click);
            // 
            // btnSpammerAdd
            // 
            this.btnSpammerAdd.Checked = false;
            this.btnSpammerAdd.Location = new System.Drawing.Point(6, 91);
            this.btnSpammerAdd.Name = "btnSpammerAdd";
            this.btnSpammerAdd.Size = new System.Drawing.Size(142, 23);
            this.btnSpammerAdd.TabIndex = 0;
            this.btnSpammerAdd.Text = "Add Packet";
            this.btnSpammerAdd.Click += new System.EventHandler(this.btnSpammer_Click);
            // 
            // BotManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(708, 339);
            this.ContextMenuStrip = this.BotManagerMenuStrip;
            this.Controls.Add(this.splitContainer2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BotManager";
            this.Padding = new System.Windows.Forms.Padding(7);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " Bot";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BotManager_FormClosing);
            this.Load += new System.EventHandler(this.BotManager_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabCombat.ResumeLayout(false);
            this.pnlCombat.ResumeLayout(false);
            this.pnlCombat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRestMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSkillD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSafe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSkill)).EndInit();
            this.tabItem.ResumeLayout(false);
            this.pnlItem.ResumeLayout(false);
            this.pnlItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDropDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShopId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMapItem)).EndInit();
            this.tabMap.ResumeLayout(false);
            this.pnlMap.ResumeLayout(false);
            this.pnlMap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkX)).EndInit();
            this.tabQuest.ResumeLayout(false);
            this.pnlQuest.ResumeLayout(false);
            this.pnlQuest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEnsureTries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuestItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuestID)).EndInit();
            this.tabMisc.ResumeLayout(false);
            this.pnlMisc.ResumeLayout(false);
            this.pnlMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSetInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIndexCmd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBeepTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBotDelay)).EndInit();
            this.tabOptions.ResumeLayout(false);
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOptionsTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkSpeed)).EndInit();
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRelogDelay)).EndInit();
            this.tabOptions2.ResumeLayout(false);
            this.tabOptions2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSetLevel)).EndInit();
            this.grpAccessLevel.ResumeLayout(false);
            this.grpAccessLevel.PerformLayout();
            this.grpAlignment.ResumeLayout(false);
            this.tabBots.ResumeLayout(false);
            this.pnlSaved.ResumeLayout(false);
            this.pnlSaved.PerformLayout();
            this.tabMisc2.ResumeLayout(false);
            this.tabMisc2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayedPacket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpammerDelay)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.BotManagerMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        static BotManager()
        {
            Instance = new BotManager();
            Log = new LogForm();
        }

        private void btnBuyFast_Click(object sender, EventArgs e)
        {
            if (txtShopItem.TextLength > 0)
            {
                AddCommand(new CmdBuyFast
                {
                    ItemName = txtShopItem.Text
                }, (ModifierKeys & Keys.Control) == Keys.Control);
            }
        }

        private void btnLoadShop_Click(object sender, EventArgs e)
        {
            AddCommand(new Botting.Commands.Item.CmdLoad
            {
                ShopId = (int)numShopId.Value
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        public void changeGenderAsync(object sender, EventArgs e)
        {
            int num = Flash.Call<int>("UserID", new string[0]);
            string text = Flash.Call<string>("Gender", new string[0]);
            text = (!text.Contains("M")) ? "M" : "F";
            string data = $"{{\"t\":\"xt\",\"b\":{{\"r\":-1,\"o\":{{\"cmd\":\"genderSwap\",\"bitSuccess\":1,\"gender\":\"{text}\",\"intCoins\":0,\"uid\":\"{num}\",\"strHairFileName\":\"\",\"HairID\":\"\",\"strHairName\":\"\"}}}}}}";
            Proxy.Instance.SendToClient(data);
        }

        private void logScript(object sender, EventArgs e)
        {
            AddCommand(new CmdLog
            {
                Text = txtLog.Text
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void logDebug(object sender, EventArgs e)
        {
            AddCommand(new CmdLog
            {
                Text = txtLog.Text,
                Debug = true
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogForm.Instance.Show();
            LogForm.Instance.BringToFront();
        }

        private void btnYulgar_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdYulgar(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnProvoke_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdToggleProvoke
            {
                Type = 2
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnchangeName_Click(object sender, EventArgs e)
        {
            foreach (string n in Configuration.BlockedPlayers)
            {
                if (txtUsername.Text.ToLower() == n)
                {
                    Environment.Exit(0);
                }
            }
            CustomName = txtUsername.Text;
        }

        private void btnchangeGuild_Click(object sender, EventArgs e)
        {
            foreach (string n in Configuration.BlockedPlayers)
            {
                if (txtUsername.Text.ToLower() == n)
                {
                    Environment.Exit(0);
                }
            }
            CustomGuild = txtGuild.Text;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Join Discord", "discord.gg/aqwbots", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnProvokeOn_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdToggleProvoke
            {
                Type = 1
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnProvokeOff_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdToggleProvoke
            {
                Type = 0
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        public void AddItem(string Name)
        {
            if (!lstItems.Items.Contains(Name))
            {
                lstItems.Items.Add(Name);
            }
        }

        private void btnUnbanklst_Click(object sender, EventArgs e)
        {
            string text = txtWhitelist.Text;
            if (text.Length > 0)
            {
                AddItem(text);
            }
        }

        private void chkPacket_CheckChanged(object sender, EventArgs e)
        {
            OptionsManager.Packet = chkPacket.Checked;
        }

        private void lstLogText_KeyDown(object sender, KeyEventArgs e)
        {
            bool flag = e.Control && e.KeyCode == Keys.C;
            if (flag)
            {
                string data = this.lstLogText.SelectedItem.ToString();
                Clipboard.SetData(DataFormats.StringFormat, data);
            }
        }

        private void numOptionsTimer_ValueChanged(object sender, EventArgs e)
        {
            OptionsManager.Timer = (int)numOptionsTimer.Value;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabBots)
            {
                this.txtSaved.Text = Path.Combine(Application.StartupPath, "Bots");
                UpdateTree();
            }
        }

        private void txtSaved_TextChanged(object sender, EventArgs e)
        {
            UpdateTree();
        }

        private void chkEnsureComplete_CheckedChanged(object sender, EventArgs e)
        {
            bool chk = chkEnsureComplete.Checked;
            numEnsureTries.Enabled = chk;
        }

        private void lstLogText_DoubleClick(object sender, EventArgs e)
        {
            string data = this.txtLog.Text == "Logs" ? "" : txtLog.Text + " ";
            string data2 = this.lstLogText.SelectedItem.ToString();
            this.txtLog.Text = $"{data}{data2}";
        }

        public void MultiMode()
        {
            if (this.lstCommands.SelectionMode != SelectionMode.MultiExtended)
            {
                this.lstCommands.SelectionMode = SelectionMode.MultiExtended;
                this.lstItems.SelectionMode = SelectionMode.MultiExtended;
                this.lstSkills.SelectionMode = SelectionMode.MultiExtended;
                this.lstQuests.SelectionMode = SelectionMode.MultiExtended;
                this.lstDrops.SelectionMode = SelectionMode.MultiExtended;
                this.lstBoosts.SelectionMode = SelectionMode.MultiExtended;
            }
            else
            {
                this.lstCommands.SelectionMode = SelectionMode.One;
                this.lstItems.SelectionMode = SelectionMode.One;
                this.lstSkills.SelectionMode = SelectionMode.One;
                this.lstQuests.SelectionMode = SelectionMode.One;
                this.lstDrops.SelectionMode = SelectionMode.One;
                this.lstBoosts.SelectionMode = SelectionMode.One;
            }
        }

        private void chkUntarget_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.Untarget = chkUntarget.Checked;
        }

        private void lstCommands_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void lstCommands_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void lstCommands_Click(object sender, EventArgs e)
        {
            
        }
        
        private void lstCommands_MouseEnter(object sender, EventArgs e)
        {
            if (lstCommands.Items.Count <= 0)
            {
                Color c1 = lstCommands.BackColor;
                Color c2 = Color.FromArgb(c1.A, (int)(c1.R * 0.8), (int)(c1.G * 0.8), (int)(c1.B * 0.8));
                lstCommands.BackColor = c2;
            }
        }

        private void lstCommands_MouseLeave(object sender, EventArgs e)
        {
            Color lstCommandsBackColor = Color.FromArgb(60, 63, 65);
            lstCommands.BackColor = lstCommandsBackColor;
        }

        private void btnBlank_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdBlank2 { Text = " " }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkAFK_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.AFK = chkAFK.Checked;
        }

        private void chkRelog_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAFK.Enabled = chkRelog.Checked;
        }

        private void btnCurrBlank_Click(object sender, EventArgs e)
        {
            this.txtJoinCell.Text = "Blank";
            this.txtJoinPad.Text = "Satan";
            this.txtCell.Text = "Blank";
            this.txtPad.Text = "Satan";
        }
        private void btnSetSpawn_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdSetSpawn(), (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnBeep_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdBeep
            {
                Times = (int)numBeepTimes.Value
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnAddSkillCmd_Click(object sender, EventArgs e)
        {
            string text = numSkill.Text;
            AddCommand(new CmdUseSkill
            {
                Index = text,
                SafeHp = (int)this.numSafe.Value,
                SafeMp = (int)this.numSafe.Value,
                Wait = this.chkSkillCD.Checked,
                Skill = text + ": " + Skill.GetSkillName(text)
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkBuffup_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.Buff = chkBuffup.Checked;
        }

        private void btnSetHero_Click(object sender, EventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited
            Button s = (Button)sender;
            switch(s.Name)
            {
                case "btnSetGood":
                    Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%1%");
                    break;
                case "btnSetEvil":
                    Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%2%");
                    break;
                case "btnSetChaos":
                    Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%3%");
                    break;
                case "btnSetUndecided":
                    Proxy.Instance.SendToServer("%xt%zm%updateQuest%218701%41%5%");
                    break;
                case "btnSetMem":
                    Player.ChangeAccessLevel("Member");
                    break;
                case "btnSetNonMem":
                    Player.ChangeAccessLevel("Non Member");
                    break;
                case "btnSetModerator":
                    Player.ChangeAccessLevel("Moderator");
                    break;
            } 
#pragma warning restore CS4014 // Because this call is not awaited
        }

        private void chkToggleMute_CheckedChanged(object sender, EventArgs e)
        {
            Player.ToggleMute(chkToggleMute.Checked);
        }

        private void changeFontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fdlg = new FontDialog();
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                this.Font = new Font(fdlg.Font.FontFamily, fdlg.Font.Size, FontStyle.Regular, GraphicsUnit.Point, 0);
                foreach (Control control in this.Controls)
                {
                    control.Font = new Font(fdlg.Font.FontFamily, fdlg.Font.Size, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
                var selectedOption = MessageBox.Show("Would you like to Save style and have it load on the next startup?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (selectedOption == DialogResult.Yes)
                {
                    Config c = Config.Load(Application.StartupPath + "\\config.cfg");
                    c.Set("font", fdlg.Font.FontFamily.Name.ToString());
                    c.Set("fontSize", fdlg.Font.Size.ToString());
                    c.Save();
                }
            }
        }

        private void numDropDelay_ValueChanged(object sender, EventArgs e)
        {
            Bot.Instance.DropDelay = (int)numDropDelay.Value;
        }

        private void btnAttack_Click(object sender, EventArgs e)
        {
            string monster = string.IsNullOrEmpty(txtMonster.Text) ? "*" : txtMonster.Text;
            AddCommand(new CmdAttack
            {
                Monster = txtMonster.Text == "Monster (*  = random)" ? "*" : txtMonster.Text
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkBankOnStop_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.Instance.BankOnStop = chkBankOnStop.Checked;
        }

        private void btnGotoIndex_Click(object sender, EventArgs e)
        {
            IBotCommand cmd;
            switch (((Button)sender).Text)
            {
                case "Up++":
                    cmd = new CmdIndex
                    {
                        Type = CmdIndex.IndexCommand.Up,
                        Index = (int)numIndexCmd.Value
                    };
                    break;
                case "Down--":
                    cmd = new CmdIndex
                    {
                        Type = CmdIndex.IndexCommand.Down,
                        Index = (int)numIndexCmd.Value
                    };
                    break;
                default:
                    cmd = new CmdIndex
                    {
                        Type = CmdIndex.IndexCommand.Goto,
                        Index = (int)numIndexCmd.Value
                    };
                    break;
            }
            AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void TogglePages()
        {
            Size p1 = splitContainer2.Panel1.ClientSize;
            int p1w = splitContainer2.Panel1.ClientSize.Width;
            Size p2 = splitContainer2.Panel2.ClientSize;
            int p2w = splitContainer2.Panel2.ClientSize.Width;
            if (tabControl1.Visible)
            {
                this.ClientSize = new Size(p1w, ClientSize.Height);
                tabControl1.Visible = false;
                splitContainer2.Panel2Collapsed = true;
            }
            else
            {
                this.ClientSize = new Size(p1w + 500, ClientSize.Height);
                splitContainer2.Panel2Collapsed = false;
                tabControl1.Visible = true;
            }
        }

        private void lstCommands_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (!(e.Index > -1))
                return;
            e.DrawBackground();
            if (!colorfulCommands.Checked)
            {
                // Define the default color of the brush as black.
                Brush myBrush = Brushes.Black;
                // Draw the current item text based on the current Font 
                // and the custom brush settings.
                e.Graphics.DrawString(lstCommands.Items[e.Index].ToString(),
                    e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
                // If the ListBox has focus, draw a focus rectangle around the selected item.
                e.DrawFocusRectangle();
                return;
            }

            #region Settings
            IBotCommand cmd = (IBotCommand)lstCommands.Items[e.Index];
            string[] count = cmd.GetType().ToString().Split('.');
            string scmd = count[count.Count() - 1].Replace("Cmd", "");
            string WindowText = SystemColors.WindowText.ToArgb().ToString();
            SolidBrush color = new SolidBrush(Color.FromArgb(GetColor(scmd)));
            SolidBrush indexcolor = new SolidBrush(Color.FromArgb(GetColor(name: "Index")));
            RectangleF region = e.Bounds;
            Font font = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Regular);
            StringFormat centered = new StringFormat()
            {
                    Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            #endregion

            string[] LabelLess = new string[]
            {
                "Label",
                "GotoLabel",
                "Blank",
                "Blank2",
                "StatementCommand",
                "Index",
                "SkillSet"
            };

            if (cmd is CmdBlank || cmd is CmdBlank2)
            {
                string txt = lstCommands.Items[e.Index].ToString();
                Font b2 = new Font("Arial", e.Font.Size + (float)6.5, FontStyle.Bold, GraphicsUnit.Pixel);
                if (cmd is CmdBlank2 && txt.Contains("[RGB]"))
                    using (Font the_font = new Font("Times New Roman", e.Font.Size + (float)6.5, FontStyle.Bold, GraphicsUnit.Pixel))
                    {
                        var font_info = new FontInfo(e.Graphics, the_font);
                        SizeF text_size = e.Graphics.MeasureString(txt, the_font);
                        int x0 = (int)((this.ClientSize.Width - text_size.Width) / 2);
                        int y0 = (int)((this.ClientSize.Height - text_size.Height) / 2);
                        int brush_y0 = (int)(y0 + font_info.InternalLeadingPixels) + (int)font_info.InternalLeadingPixels;
                        int brush_y1 = (int)(y0 + font_info.AscentPixels) + 5;
                        using (LinearGradientBrush rainbowbrush = new LinearGradientBrush(new Point(x0, brush_y0), new Point(x0, brush_y1), Color.Red, Color.Violet))
                        {
                            Color[] colors = new Color[]
                            {
                                Color.FromArgb(255, 0, 0),
                                Color.FromArgb(255, 0, 0),
                                Color.FromArgb(255, 128, 0),
                                Color.FromArgb(255, 255, 0),
                                Color.FromArgb(0, 255, 0),
                                Color.FromArgb(0, 255, 128),
                                Color.FromArgb(0, 255, 255),
                                Color.FromArgb(0, 128, 255),
                                Color.FromArgb(0, 0, 255),
                                Color.FromArgb(0, 0, 255),
                            };
                            int num_colors = colors.Length;
                            float[] blend_positions = new float[num_colors];
                            for (int i = 0; i < num_colors; i++)
                                blend_positions[i] = i / (num_colors - 1f);
                            ColorBlend color_blend = new ColorBlend
                            {
                                Colors = colors,
                                Positions = blend_positions
                            };
                            rainbowbrush.InterpolationColors = color_blend;

                            // Draw the text.
                            txt = txt.Replace("[RGB]", "");
                            e.Graphics.DrawString(txt, the_font, rainbowbrush, e.Bounds, centered);
                        }
                    }
                else if(cmd is CmdBlank2 && txt.StartsWith("["))
                {
                    try
                    {
                        string[] rgbarray = txt.Replace("[", "").Split(']')[0].Split(',');
                        SolidBrush b2b = new SolidBrush(Color.Black);
                        if (rgbarray.Length == 3)
                            b2b = new SolidBrush(Color.FromArgb(int.Parse(rgbarray[0]), int.Parse(rgbarray[1]), int.Parse(rgbarray[2])));
                        else if (rgbarray.Length == 4)
                            b2b = new SolidBrush(Color.FromArgb(int.Parse(rgbarray[0]), int.Parse(rgbarray[1]), int.Parse(rgbarray[2]), int.Parse(rgbarray[3])));
                        txt = Regex.Replace(txt, @"\[.*?\]", "");
                        if (txt.Contains("(TROLL)"))
                            e.Graphics.DrawString(txt.Replace("(TROLL)", ""), e.Font, b2b, e.Bounds, StringFormat.GenericDefault);
                        else
                            e.Graphics.DrawString(txt, b2, b2b, e.Bounds, centered);
                    }catch{}
                }
                else if (txt.Contains("(TROLL)"))
                    e.Graphics.DrawString(txt.Replace("(TROLL)", ""), e.Font, new SolidBrush(Color.White), e.Bounds, StringFormat.GenericDefault);
                return;
            }

            if (!LabelLess.Contains(scmd))
            {
                //Draw Index
                //Region first = DrawString(e.Graphics, $"[{e.Index.ToString()}] ", this.Font, indexcolor, region, new StringFormat(StringFormatFlags.DirectionRightToLeft));
                Region first = DrawString(e.Graphics, $"[{e.Index.ToString()}] ", font, indexcolor, region, StringFormat.GenericDefault);
                // Adjust the region we wish to print with a +3 offset.
                region = new RectangleF(region.X + first.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
            }

            if (GetBoolCentered(scmd))
            {
                e.Graphics.DrawString(lstCommands.Items[e.Index].ToString(), font, color, e.Bounds, centered);
                return;
            }

            // Draw the second string (rest of the string, in this case Command text).
            DrawString(e.Graphics, lstCommands.Items[e.Index].ToString(), font, color, region, StringFormat.GenericDefault);
        }

        private int GetColor(string name)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            string WindowText = SystemColors.WindowText.ToArgb().ToString("X");
            return int.Parse(c.Get(name + "Color") ?? WindowText, System.Globalization.NumberStyles.HexNumber);
        }

        private bool GetBoolCentered(string name)
        {
            Config c = Config.Load(Application.StartupPath + "\\config.cfg");
            return bool.Parse(c.Get(name + "Centered") ?? "false");
        }

        private Region DrawString(Graphics g, string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            format.SetMeasurableCharacterRanges(new[] { new CharacterRange(0, s.Length) });
            format.Alignment = StringAlignment.Near;
            g.DrawString(s, font, brush, layoutRectangle, format);
            return g.MeasureCharacterRanges(s, font, layoutRectangle, format)[0];
        }

        private Region DrawRTLString(Graphics g, string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            var format = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.DirectionRightToLeft
            };
            format.SetMeasurableCharacterRanges(new[] { new CharacterRange(0, s.Length) });
            //g.DrawString(s, font, brush, layoutRectangle, format);
            Region length = g.MeasureCharacterRanges(s, font, layoutRectangle, format)[0];
            layoutRectangle = new RectangleF(layoutRectangle.Width, layoutRectangle.Y, length.GetBounds(g).Width, layoutRectangle.Height);
            DrawString(g, s, font, brush, layoutRectangle, format);
            return length;
        }

        private void multilineToggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MultiMode();
        }

        private void toggleTabpagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TogglePages();
        }

        private void commandColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Root.Instance.ShowForm(CommandColorForm.Instance);
        }

        private void btnChangeCmd_Click(object sender, EventArgs e)
        {
            IBotCommand cmd;
            switch(((Button)sender).Name)
            {
                case "btnChangeGuildCmd":
                    cmd = new CmdChange
                    {
                        Guild = true,
                        Text = txtGuild.Text
                    };
                    break;
                default:
                    cmd = new CmdChange
                    {
                        Guild = false,
                        Text = txtUsername.Text
                    };
                    break;
            }
            AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkAntiAfk_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.AFK2 = chkAntiAfk.Checked;
        }

        private void treeBots_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void chkChangeRoomTag_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.HideRoom = chkChangeRoomTag.Checked;
        }

        private void chkChangeChat_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.ChangeChat = chkChangeChat.Checked;
        }

        private void chkSetJoinLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSetJoinLevel.Checked)
                OptionsManager.SetLevelOnJoin = (int)numSetLevel.Value;
            else
                OptionsManager.SetLevelOnJoin = null;
        }

        private void btnClientPacket_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdPacket
            {
                Packet = txtPacket.Text,
                Client = true
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void chkHideYulgarPlayers_CheckedChanged(object sender, EventArgs e)
        {
            OptionsManager.HideYulgar = chkHideYulgarPlayers.Checked;
        }

        private void btnSetInt_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdInt
            {
                Int = txtSetInt.Text,
                Value = (int)numSetInt.Value,
                type = CmdInt.Types.Set
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnIncreaseInt_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdInt
            {
                Int = txtSetInt.Text,
                Value = (int)numSetInt.Value,
                type = CmdInt.Types.Upper
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnDecreaseInt_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdInt
            {
                Int = txtSetInt.Text,
                Value = (int)numSetInt.Value,
                type = CmdInt.Types.Lower
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void lstCommands_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnWhitelistOn_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdWhitelist
            {
                state = CmdWhitelist.State.On
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnWhitelistOff_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdWhitelist
            {
                state = CmdWhitelist.State.Off
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnWhitelistToggle_Click(object sender, EventArgs e)
        {
            AddCommand(new CmdWhitelist
            {
                state = CmdWhitelist.State.Toggle
            }, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        // Experimental!
        public int LastIndexedSearch = 0;
        public string SKeyword = "";
        public List<int> Filtered = new List<int>();

        private void btnSearchCmd_Click(object sender, EventArgs e)
        {
            string Keyword = this.txtSearchCmd.Text;
            ListBox.ObjectCollection lists = lstCommands.Items;

            if (Keyword != SKeyword)
            {
                SKeyword = "";
                Filtered.Clear();
            }

            // Collect all filtered result
            for (int i = 0; i < lists.Count; i++)
            {
                if (Keyword == SKeyword)
                {
                    Console.WriteLine("Using Cached List.");
                    break;
                }

                bool KeywordMatch = Regex.IsMatch(lists[i].ToString(), $@"(^|\s){Keyword}(\s|$)");

                // Collect all filtered result
                if (KeywordMatch)
                {
                    Filtered.Add(i);
                }
            }

            SKeyword = Keyword;
            // Use the filtered Index
            if ( Filtered.Count > 0 )
            {
                lstCommands.SelectedIndex = -1;
                lstCommands.SelectedIndex = Filtered[LastIndexedSearch];
                LastIndexedSearch++;

                if ( LastIndexedSearch >= Filtered.Count )
                {
                    LastIndexedSearch = 0;
                }
            }
        }
        public PacketSpammer botPacketSpammer = new PacketSpammer();
        private void btnSpammer_Click(object sender, EventArgs e)
        {
            IBotCommand cmd;
            string Packet = txtPacketSpammer.Text;
            switch (((Button)sender).Name)
            {
                case "btnSpammerAdd":
                    cmd = new CmdPacketSpammer {
                        type = CmdPacketSpammer.CommandType.Add,
                        Packet = Packet,
                        Spammer = botPacketSpammer
                    };
                    break;
                case "btnSpammerRemove":
                    cmd = new CmdPacketSpammer
                    {
                        type = CmdPacketSpammer.CommandType.Remove,
                        Packet = Packet
                    };
                    break;
                case "btnSpammerReset":
                    cmd = new CmdPacketSpammer
                    {
                        type = CmdPacketSpammer.CommandType.Clear,
                        Packet = Packet
                    };
                    break;
                case "btnSpammerStart":
                    cmd = new CmdPacketSpammer
                    {
                        type = CmdPacketSpammer.CommandType.Start,
                        Packet = Packet
                    };
                    break;
                case "btnSpammerStop":
                    cmd = new CmdPacketSpammer
                    {
                        type = CmdPacketSpammer.CommandType.Stop,
                        Packet = Packet
                    };
                    break;
                case "btnSpammerRefresh":
                    cmd = new CmdPacketSpammer
                    {
                        type = CmdPacketSpammer.CommandType.Restart,
                        Packet = Packet
                    };
                    break;
                case "btnSpammerSetDelay":
                    cmd = new CmdPacketSpammer
                    {
                        type = CmdPacketSpammer.CommandType.SetDelay,
                        Packet = Packet,
                        Delay = (int)numSpammerDelay.Value
                    };
                    break;
                case "btnDelayedPacket":
                    cmd = new CmdPacketDelay
                    {
                        Packet = Packet,
                        Delay = (int)numDelayedPacket.Value
                    };
                    break;
                default:
                    cmd = new CmdPacketSpammer
                    {
                        type = CmdPacketSpammer.CommandType.GoPressTheGodDamnCorrectButton,
                        Packet = Packet
                    };
                    break;
            }
            AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
        }

        private void btnClientMessageEvt(object sender, EventArgs e)
        {
            IBotCommand cmd;
            switch (((Button)sender).Name)
            {
                case "btnAddWarnMsg":
                    cmd = new CmdClientMessage
                    {
                        IsWarning = true,
                        Messages = (string)inputMsgClient.Text
                    };
                    break;
                case "btnAddInfoMsg":
                    cmd = new CmdClientMessage
                    {
                        Messages = (string)inputMsgClient.Text
                    };
                    break;
                default:
                    cmd = new CmdClientMessage
                    {
                        Messages = (string)inputMsgClient.Text
                    };
                    break;
            };

            AddCommand(cmd, (ModifierKeys & Keys.Control) == Keys.Control);
        }
    }
}