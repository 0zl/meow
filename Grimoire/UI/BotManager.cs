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
using Newtonsoft.Json.Linq;
using Extensions = Grimoire.Botting.Extensions;
using Properties;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;
using Grimoire.Networking.Handlers;

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

		private readonly JsonSerializerSettings _saveSerializerSettings = new JsonSerializerSettings
		{
			DefaultValueHandling = DefaultValueHandling.Ignore,
			NullValueHandling = NullValueHandling.Ignore,
			TypeNameHandling = TypeNameHandling.All
		};

		#region Definitions
		private IContainer components;
		private Panel[] _panels;
		public ListBox lstCommands;
		public ListBox lstSkills;
		private ListBox lstQuests;
		public ListBox lstDrops;
		private ListBox lstBoosts;
		public static LogForm Log;
		private string _customName;
		private string _customGuild;
		private ListBox lstItems;
		public FlatTabControl.FlatTabControl mainTabControl;
		private TabPage tabCombat;
		private DarkButton btnUseSkillSet;
		private DarkButton btnAddSkillSet;
		private DarkTextBox txtSkillSet;
		private DarkCheckBox chkSafeMp;
		private DarkLabel label17;
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
		private TabPage tabMap;
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
		private TabPage tabHunt;
		private DarkButton btnQuestAccept;
		private DarkButton btnQuestComplete;
		private DarkButton btnQuestAdd;
		private DarkNumericUpDown numQuestItem;
		private DarkCheckBox chkQuestItem;
		private DarkNumericUpDown numQuestID;
		private DarkLabel label4;
		private TabPage tabMisc;
		private DarkCheckBox chkRestartDeath;
		private DarkCheckBox chkMerge;
		private DarkButton button2;
		private DarkButton btnLogout;
		private DarkTextBox txtDescription;
		private DarkButton btnSave;
		private DarkButton btnDelay;
		private DarkNumericUpDown numDelay;
		private DarkLabel label3;
		private DarkNumericUpDown numBotDelay;
		private DarkButton btnBotDelay;
		private DarkTextBox txtPlayer;
		private DarkButton btnGoto;
		public DarkButton btnLoad;
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
		private DarkCheckBox chkEnableSettings;
		public DarkCheckBox chkDisableAnims;
		private DarkTextBox txtSoundItem;
		private DarkButton btnSoundAdd;
		private DarkButton btnSoundDelete;
		private DarkButton btnSoundTest;
		private ListBox lstSoundItems;
		private DarkLabel label9;
		private DarkNumericUpDown numWalkSpeed;
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
		private TabPage tabBots;
		private DarkLabel lblBoosts;
		private DarkLabel lblDrops;
		private DarkLabel lblQuests;
		private DarkLabel lblSkills;
		private DarkLabel lblCommands;
		private DarkLabel lblItems;
		private DarkTextBox txtSavedAuthor;
		private DarkLabel lblBots;
		private TreeView treeBots;
		private DarkTextBox txtSavedAdd;
		private DarkButton btnSavedAdd;
		private DarkTextBox txtSaved;
		private DarkButton btnProvokeOn;
		private DarkButton btnProvokeOff;
		private ListBox lstLogText;
		private DarkButton btnLogDebug;
		private DarkButton btnLog;
		private DarkTextBox txtLog;
		private DarkLabel label5;
		private DarkNumericUpDown numOptionsTimer;
		private DarkLabel label6;
		private DarkLabel label14;
		private DarkNumericUpDown numEnsureTries;
		private DarkButton btnWalkRdm;
		private DarkButton btnBlank;
		private DarkCheckBox chkAFK;
		private SplitContainer splitContainer1;
		private DarkComboBox cbLists;
		private DarkCheckBox chkAll;
		private DarkButton btnClear;
		private DarkButton btnDown;
		private DarkButton btnRemove;
		private DarkButton btnUp;
		private Panel panel1;
		private SplitContainer splitContainer2;
		private DarkButton btnCurrBlank;
		private DarkButton btnSetSpawn;
		private DarkButton btnBeep;
		private DarkNumericUpDown numBeepTimes;
		private Panel panel3;
		private Panel panel2;
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
		private DarkButton btnGoDownIndex;
		private DarkButton btnGoUpIndex;
		private DarkButton btnGotoIndex;
		private DarkNumericUpDown numIndexCmd;
		private ToolStripMenuItem multilineToggleToolStripMenuItem;
		private ToolStripMenuItem toggleTabpagesToolStripMenuItem;
		private ToolStripMenuItem commandColorsToolStripMenuItem;
		private DarkButton btnChangeNameCmd;
		private DarkButton btnChangeGuildCmd;
		private DarkButton btnClientPacket;
		private DarkNumericUpDown numSetInt;
		private DarkTextBox txtSetInt;
		private DarkButton btnSetInt;
		private DarkButton btnDecreaseInt;
		private DarkButton btnIncreaseInt;
		private DarkButton btnSearchCmd;
		private DarkTextBox txtSearchCmd;
		private TabPage tabMisc2;
		private DarkGroupBox groupBox1;
		private DarkButton btnAddInfoMsg;
		private DarkButton btnAddWarnMsg;
		private DarkTextBox inputMsgClient;
		private Panel panel4;
		private DarkGroupBox darkGroupBox1;
		private DarkButton btnReturnCmd;
		private DarkButton btnClearTempVar;
		private DarkGroupBox darkGroupBox4;
		private DarkCheckBox chkPickupAll;
		public DarkCheckBox chkPickup;
		private DarkCheckBox chkReject;
		public DarkCheckBox chkPickupAcTag;
		private DarkCheckBox chkBankOnStop;
		private DarkCheckBox chkRejectAll;
		private DarkGroupBox darkGroupBox3;
		private DarkNumericUpDown numShopId;
		private DarkButton btnLoadShop;
		private DarkTextBox txtShopItem;
		private DarkButton btnBuy;
		private DarkButton btnBuyFast;
		private DarkGroupBox darkGroupBox2;
		private DarkButton btnWhitelistToggle;
		private DarkButton btnWhitelistOn;
		private DarkButton btnWhitelistOff;
		private DarkLabel label1;
		private DarkNumericUpDown numDropDelay;
		private DarkButton btnBoost;
		private DarkComboBox cbBoosts;
		private DarkNumericUpDown numMapItem;
		private DarkButton btnMapItem;
		private DarkButton btnSwap;
		private DarkTextBox txtSwapInv;
		private DarkTextBox txtSwapBank;
		private DarkButton btnWhitelist;
		private DarkButton btnBoth;
		private DarkTextBox txtWhitelist;
		private DarkButton btnItem;
		private DarkButton btnUnbanklst;
		private DarkTextBox txtItem;
		private DarkComboBox cbItemCmds;
		private DarkGroupBox darkGroupBox5;
		private DarkGroupBox darkGroupBox6;
		private TabPage tabInfo;
		private Panel panel5;
		public RichTextBox rtbInfo;
		private DarkGroupBox darkGroupBox7;
		private DarkGroupBox darkGroupBox8;
		private DarkGroupBox darkGroupBox9;
		private BackgroundWorker backgroundWorker1;
		private SplitContainer splitContainer3;
		private DarkGroupBox darkGroupBox12;
		private DarkGroupBox darkGroupBox13;
		private RichTextBox richTextBox2;
		private DarkPanel darkPanel1;
		private DarkGroupBox darkGroupBox14;
		private DarkButton btnAttack;
		#endregion
		private SplitContainer splitContainer5;
		public CheckBox chkEnable;
		private DarkComboBox cbSafeType;
		private DarkCheckBox chkAddToWhiteList;
		private DarkNumericUpDown numSpamTimes;
		private DarkButton btnSetLevelCmd;
		private DarkButton btnSetLevel;
		private DarkTextBox tbLevel;
		public DarkCheckBox chkWalkSpeed;
		private DarkCheckBox chkInBlankCell;
		private DarkCheckBox chkReloginCompleteQuest;
		private DarkLabel darkLabel1;
		private DarkTextBox darkTextBox1;
		private DarkCheckBox darkCheckBox1;
		private DarkButton btnAddCmdHunt;
		private DarkCheckBox chkIsTempF;
		private DarkLabel darkLabel3;
		private DarkTextBox tbGetAfterF;
		private DarkCheckBox chkGetAfterF;
		private DarkCheckBox chkAddToWhitelistF;
		private DarkTextBox tbItemQtyF;
		private DarkTextBox tbItemNameF;
		private DarkTextBox tbMonNameF;
		private DarkCheckBox cbBlankFirstF;
		private DarkButton btnGetMapF;
		private DarkTextBox tbPadF;
		private DarkTextBox tbCellF;
		private DarkTextBox tbMapF;
		private DarkLabel darkLabel2;
		private Label lblUP;
		private DarkButton btnAllSkill;
		private DarkCheckBox chkQRequirements;
		private DarkCheckBox chkQRewards;
		private DarkButton btnQAddToWhitelist;
		private DarkNumericUpDown numQQuestId;
		private DarkLabel darkLabel4;
		private DarkNumericUpDown numSetFPS;
		private DarkButton btnSetFPS;
		public DarkCheckBox chkGender;
		private DarkButton btnBSStop;
		private Label label8;
		private DarkTextBox tbBSLabel;
		private DarkNumericUpDown numBSDelay;
		private DarkTextBox tbBSPacket;
		private DarkButton btnBSStart;
		private DarkTextBox numPrivateRoom;
		public DarkCheckBox chkPrivateRoom;
		private DarkCheckBox chkAntiCounter;
		private DarkLabel darkLabel6;
		private DarkNumericUpDown numSaveProgress;
		private DarkCheckBox chkSaveProgress;

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

		public static System.Timers.Timer SaveProgressTimer = new System.Timers.Timer();
		private DarkButton btnSetSpawn2;
		private DarkGroupBox darkGroupBox15;
		private DarkButton btnFollowCmd;
		private DarkTextBox tbFollowPlayer;
		private DarkGroupBox darkGroupBox16;
		private DarkButton btnAMTest;
		private DarkCheckBox chkAMStopBot;
		private DarkCheckBox chkAMLogout;
		private DarkCheckBox chkAntiMod;
		private DarkGroupBox darkGroupBox10;
		private DarkButton btnStop;
		private DarkButton btnRestart;
		private DarkCheckBox colorfulCommands;
		private DarkButton btnAddLabel;
		private DarkButton btnGotoLabel;
		private DarkTextBox txtLabel;
		private DarkGroupBox darkGroupBox17;
		private DarkCheckBox chkLoginLock;
		private DarkTextBox tbLoginUsername;
		private DarkTextBox tbLoginPassword;
		private DarkTextBox txtAuthor;
		private DarkCheckBox chkUseSkillTargeted;
		public DarkCheckBox chkFollowOnly;
		private DarkTextBox tbFollowPlayer2;
        private DarkGroupBox darkGroupBox11;
        private DarkListBox lbLabels;
        private DarkTextBox txtSavedDesc;
        public PacketSpammer botPacketSpammer;

		private BotManager()
		{
			InitializeComponent();
			cbSafeType.SelectedIndex = 0;
			botPacketSpammer = new PacketSpammer();
		}

		private void BotManager_Load(object sender, EventArgs e)
		{
			lstBoosts.DisplayMember = "Text";
			//lstQuests.DisplayMember = "Text";
			//lstSkills.DisplayMember = "Text";
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
			lstCommands.Font = new Font(font, lstCommands.ItemHeight / 4 - (float)6.5, FontStyle.Regular, GraphicsUnit.Point, 0);
			lstCommands.ItemHeight = lstCommands.ItemHeight / 4;
		}

		private void TextboxEnter(object sender, EventArgs e)
		{
			TextBox t = (TextBox)sender;
			if (t.Text == _defaultControlText[t.Name])
				t.Clear();
		}

		private void TextboxLeave(object sender, EventArgs e)
		{
			TextBox t = (TextBox)sender;
			if (string.IsNullOrEmpty(t.Text))
			{
				if (_defaultControlText.TryGetValue(t.Name, out string def))
					t.Text = def;
			}
		}

		public void OnServersLoaded(Server[] servers)
		{
			if (servers != null && servers.Length != 0 && cbServers.Items.Count <= 1)
			{
				cbServers.Items.AddRange(servers);
				cbServers.SelectedIndex = 0;
				Root.Instance.changeServerList.Items.AddRange(servers);
				Root.Instance.changeServerList.SelectedIndex = 0;
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
				Description = rtbInfo.Rtf ?? rtbInfo.Text,
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
				BankOnStop = chkBankOnStop.Checked,
				RestMp = (int)numRestMP.Value,
				RestHp = (int)numRest.Value,
				RestartUponDeath = chkRestartDeath.Checked,
				AFK = chkAFK.Checked,
				AntiCounter = chkAntiCounter.Checked,
				DropDelay = (int)numDropDelay.Value,
				DisableAnimations = chkDisableAnims.Checked,
				FollowCheck = chkFollowOnly.Checked,
				FollowName = tbFollowPlayer2.Text
			};
		}

		public Configuration SaveConfiguration()
		{
			String author = txtAuthor.Text == "Author" ? "" : txtAuthor.Text;
			String desc = txtDescription.Text == "Description (Write in RTF)" ? "" : txtDescription.Text;

			var compressed = DocConvert.Zip<string>(desc);

			return new Configuration
			{
				Author = author,
				Description = compressed.Length > txtDescription.Text.Length ? txtDescription.Text : compressed,
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
				BankOnStop = chkBankOnStop.Checked,
				RestMp = (int)numRestMP.Value,
				RestHp = (int)numRest.Value,
				RestartUponDeath = chkRestartDeath.Checked,
				AFK = chkAFK.Checked,
				AntiCounter = chkAntiCounter.Checked,
				DropDelay = (int)numDropDelay.Value,
				DisableAnimations = chkDisableAnims.Checked,
				FollowCheck = chkFollowOnly.Checked,
				FollowName = tbFollowPlayer2.Text
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
				txtSavedDesc.Text = DocConvert.IsBase64Encoded(config.Description) ? DocConvert.Unzip(config.Description) : config.Description ?? "Description";
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
				chkDisableAnims.Checked = config.DisableAnimations;
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
				chkBankOnStop.Checked = config.BankOnStop;
				chkRestartDeath.Checked = config.RestartUponDeath;
				chkAFK.Checked = config.AFK;
				chkAntiCounter.Checked = config.AntiCounter;
				numDropDelay.Value = config.DropDelay <= 0 ? 500 : config.DropDelay;
				txtAuthor.Text = config.Author;
				txtDescription.Text =
				txtSavedDesc.Text = DocConvert.IsBase64Encoded(config.Description) ? DocConvert.Unzip(config.Description) : config.Description ?? "Description"; ;
				var description = txtSavedDesc.Text ?? "Description";
				if (description.StartsWith("{\\rtf") || description.StartsWith("{\rtf"))
					rtbInfo.Rtf = description; //mainTabControl.SelectedTab = tabInfo;
				else
					rtbInfo.Text = config.Description;
				chkFollowOnly.Checked = config.FollowCheck;
				tbFollowPlayer2.Text = config.FollowName == null ? "Player" : config.FollowName;
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

		public void OnIsRunningChanged(bool isRunning)
		{
			if (!isRunning)
			{
				ActiveBotEngine.IsRunningChanged -= OnIsRunningChanged;
				ActiveBotEngine.IndexChanged -= OnIndexChanged;
				ActiveBotEngine.ConfigurationChanged -= OnConfigurationChanged;

				void Action()
				{
					btnUp.Enabled = true;
					btnDown.Enabled = true;
					btnClear.Enabled = true;
					btnRemove.Enabled = true;
				}

				if (InvokeRequired)
					Invoke((Action)Action);
				else
					Action();
			}

			if (InvokeRequired)
				Invoke(new Action(() => { chkEnable.Checked = isRunning; }));
			else
				chkEnable.Checked = isRunning;

			Root.Instance.chkStartBot.Checked = isRunning;

			selectionMode(isRunning ? SelectionMode.One : SelectionMode.MultiExtended);

			BotStateChanged(isRunning);

			/*Invoke((Action)delegate
			{
				if (!IsRunning)
				{
					ActiveBotEngine.IsRunningChanged -= OnIsRunningChanged;
					ActiveBotEngine.IndexChanged -= OnIndexChanged;
					ActiveBotEngine.ConfigurationChanged -= OnConfigurationChanged;
				}
				BotStateChanged(IsRunning);
			});*/
		}

		private void lstSkills_DoubleClick(object sender, EventArgs e)
		{
			/*int index = lstSkills.SelectedIndex;
			if (index > -1)
			{
				object cmd = lstSkills.Items[index];
				string result = JsonConvert.SerializeObject(cmd, Formatting.Indented, _serializerSettings);
				string mod = RawCommandEditor.Show(result);

				if (mod != null)
				{
					try
					{
						Skill modifiedCmd = (Skill)JsonConvert.DeserializeObject(mod, cmd.GetType());
						lstSkills.Items.Remove(cmd);
						lstSkills.Items.Insert(index, modifiedCmd);
					}
					catch { }
				}
			}*/


			if (lstSkills.Items.Count <= 0)
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
			else if (lstSkills.SelectedIndex > -1)
			{
				int selectedIndex = lstSkills.SelectedIndex;
				object obj = lstSkills.Items[selectedIndex];
				string text = UserFriendlyCommandEditor.Show(obj);
				//string text = RawCommandEditor.Show(JsonConvert.SerializeObject(obj, Formatting.Indented, _serializerSettings));
				if (text != null)
				{
					try
					{
						Skill item = (Skill)JsonConvert.DeserializeObject(text, obj.GetType());
						lstSkills.Items.Remove(obj);
						lstSkills.Items.Insert(selectedIndex, item);
					}
					catch
					{

					}
				}
			}
		}

		private void lstQuests_DoubleClick(object sender, EventArgs e)
		{
			if (lstQuests.Items.Count <= 0)
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
			else if (lstQuests.SelectedIndex > -1)
			{
				int selectedIndex = lstQuests.SelectedIndex;
				object obj = lstQuests.Items[selectedIndex];
				string text = UserFriendlyCommandEditor.Show(obj);
				//string text = RawCommandEditor.Show(JsonConvert.SerializeObject(obj, Formatting.Indented, _serializerSettings));
				if (text != null)
				{
					try
					{
						Quest item = (Quest)JsonConvert.DeserializeObject(text, obj.GetType());
						lstQuests.Items.Remove(obj);
						lstQuests.Items.Insert(selectedIndex, item);
					}
					catch
					{

					}
				}
			}
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
				string text = UserFriendlyCommandEditor.Show(obj);
				//string text = RawCommandEditor.Show(JsonConvert.SerializeObject(obj, Formatting.Indented, _serializerSettings));
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
			GetAllCommands<CmdLabel>(lbLabels);
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
			if (txtMonster.Text == "Monster (* = random)")
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
				if (chkAddToWhiteList.Checked)
				{
					if (txtKillFItem.Text.Length <= 0) return;
					string[] items = txtKillFItem.Text.Split(new char[] {
						','
					});

					foreach (string _item in items)
					{
						if (!lstDrops.Items.Cast<string>().ToList().Any((string d) => d.Equals(_item, StringComparison.OrdinalIgnoreCase)))
							lstDrops.Items.Add(_item);
					}
				};

				string monster = string.IsNullOrEmpty(txtKillFMon.Text) || txtKillFMon.Text == "Monster (* = random)" ? "*" : txtKillFMon.Text;
				string text = txtKillFItem.Text;
				string text2 = string.IsNullOrEmpty(txtKillFQ.Text) || txtKillFQ.Text == "Quantity (* = any)" ? "*" : txtKillFQ.Text;
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
			string index = numSkill.Text;
			AddSkill(new Skill
			{
				Text = Skill.GetSkillName(index),
				Index = index,
				Type = Skill.SkillType.Normal
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnAddSafe_Click(object sender, EventArgs e)
		{
			string index = numSkill.Text;
			int safe = (int)numSafe.Value;

			Skill.SafeType safeType = Skill.SafeType.LowerThan;
			if (cbSafeType.SelectedIndex == 0)
			{
				safeType = Skill.SafeType.LowerThan;
			}
			else if (cbSafeType.SelectedIndex == 1)
			{
				safeType = Skill.SafeType.GreaterThan;
			}
			lstSkills.Items.Add(new Skill
			{
				Text = Skill.GetSkillName(index),
				Index = index,
				SafeValue = safe,
				SType = safeType,
				Type = Skill.SkillType.Safe,
				IsSafeMp = chkSafeMp.Checked
			});
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
					Map = txtJoin.Text,
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
			AddQuest((int)numQuestID.Value, chkQuestItem.Checked ? numQuestItem.Value.ToString() : null, chkInBlankCell.Checked);
		}

		private void btnQuestComplete_Click(object sender, EventArgs e)
		{
			Quest q = new Quest();
			CmdCompleteQuest cmd = new CmdCompleteQuest
			{
				CompleteTry = (int)numEnsureTries.Value,
				LogoutFailed = chkReloginCompleteQuest.Checked,
				InBlank = chkInBlankCell.Checked
			};
			q.Id = (int)numQuestID.Value;
			if (chkQuestItem.Checked)
				q.ItemId = numQuestItem.Value.ToString();
			cmd.Quest = q;
			this.AddCommand(cmd, (Control.ModifierKeys & Keys.Control) == Keys.Control);
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
				Packet = txtPacket.Text,
				SpamTimes = Decimal.ToInt32(numSpamTimes.Value)
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

		public void btnLoad_Click(object sender, EventArgs e)
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
					GetAllCommands<CmdLabel>(lbLabels);
				}
			}
		}

		private bool TryDeserialize(string json, out Configuration config)
		{
			try {
				config = JsonConvert.DeserializeObject<Configuration>(json, _saveSerializerSettings);
				return true;
			}
			catch (Exception e) { MessageBox.Show(e.ToString()); }
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
					Configuration value = SaveConfiguration();
					try
					{
						File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(value, Formatting.Indented, _saveSerializerSettings));
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
			GetAllCommands<CmdLabel>(lbLabels);
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
			GetAllCommands<CmdLabel>(lbLabels);
		}

		private void GetAllCommands<T>(ListBox lb)
		{
			lb.Items.Clear();
			T[] allItems = lstCommands.Items.OfType<T>().ToArray();
			string[] allStrings = new string[allItems.Count()];
			for(int i = 0; i < allItems.Count(); i++)
				allStrings[i] = allItems[i].ToString();
			lb.Items.AddRange(allStrings);
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
			else if ((ModifierKeys == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.C && SelectedList.SelectedIndex > -1))
			{
				Clipboard.Clear();
				Configuration items = new Configuration
				{
					Commands = lstCommands.SelectedItems.Cast<IBotCommand>().ToList()
				};
				string[] itemsString = new string[items.Commands.Count];
				for (int i = 0; i < items.Commands.Count; i++)
				{
					itemsString[i] = items.Commands[i].ToString();
				}
				Clipboard.SetText(String.Join(Environment.NewLine, itemsString));
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
				Clipboard.SetText(JsonConvert.SerializeObject(items, Formatting.Indented, _saveSerializerSettings));
				e.Handled = true;
			}
			else if (ModifierKeys == Keys.Control && e.KeyCode == Keys.V)
			{
				TryDeserialize(Clipboard.GetText(), out Configuration config);
				List<IBotCommand> commands = config.Commands;
				if (commands != null && commands.Count > 0)
				{
					List<IBotCommand> items = lstCommands.Items.Cast<IBotCommand>().ToList();
					int selectedIndex = lstCommands.SelectedIndex;
					lstCommands.SelectedIndex = -1;
					items.InsertRange(++selectedIndex, commands);
					lstCommands.Items.Clear();
					lstCommands.Items.AddRange(items.ToArray());
					for (int i = 0; i < commands.Count; i++)
						lstCommands.SelectedIndex = selectedIndex + i;
					
					/* Deprecated
					ListBox.ObjectCollection items = lstCommands.Items;
					object[] array = config.Commands.ToArray();
					int selectedIndex = lstCommands.SelectedIndex;
					lstCommands.SelectedIndex = -1;
					for (int i = 0; array.Count() > i; i++)
					{
						items.Insert(selectedIndex + i + 1, array[i]);
						lstCommands.SelectedIndex = selectedIndex + i + 1;
					}
					*/
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
			else if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.X && this.SelectedList.SelectedIndex > -1)
			{
				Clipboard.Clear();
				Configuration value = new Configuration
				{
					Commands = this.lstCommands.SelectedItems.Cast<IBotCommand>().ToList<IBotCommand>(),
					Skills = this.lstSkills.SelectedItems.Cast<Skill>().ToList<Skill>(),
					Quests = this.lstQuests.SelectedItems.Cast<Quest>().ToList<Quest>(),
					Boosts = this.lstBoosts.SelectedItems.Cast<InventoryItem>().ToList<InventoryItem>(),
					Drops = this.lstDrops.SelectedItems.Cast<string>().ToList<string>(),
				};
				Clipboard.SetText(JsonConvert.SerializeObject(value, Formatting.Indented, this._serializerSettings));
				this.btnRemove.PerformClick();
				e.Handled = true;
				return;
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
						Configuration value = SaveConfiguration();
						try
						{
							File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(value, Formatting.Indented, _saveSerializerSettings));
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

		public void AddCommand(IBotCommand cmd, bool Insert = false)
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

		private async void chkEnableBot_CheckedChanged(object sender, EventArgs e)
		{
			if (chkEnable.Checked && (lstCommands.Items.Count <= 0 || !Player.IsLoggedIn))
			{
				chkEnable.Checked = false;
				return;
			}

            chkAntiMod.Enabled = !chkEnable.Checked;

            if (chkEnable.Checked)
			{
				selectionMode(SelectionMode.One);
				ActiveBotEngine.IsRunningChanged += OnIsRunningChanged;
				ActiveBotEngine.IndexChanged += OnIndexChanged;
				ActiveBotEngine.ConfigurationChanged += OnConfigurationChanged;
				ActiveBotEngine.Start(GenerateConfiguration());
				BotStateChanged(IsRunning: true);
				Root.Instance.BotStateChanged(IsRunning: true);
				Root.Instance.chkStartBot.Checked = true;
			}
			else
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
				ActiveBotEngine.Stop();
				selectionMode(SelectionMode.MultiExtended);
				BotStateChanged(IsRunning: false);
				Root.Instance.BotStateChanged(IsRunning: false);
				Root.Instance.chkStartBot.Checked = false;
            }

            toggleAntiMod(chkAntiMod.Checked);
        }


		private void selectionMode(SelectionMode mode)
		{
			this.lstCommands.SelectionMode = mode;
			this.lstSkills.SelectionMode = mode;
			this.lstQuests.SelectionMode = mode;
			this.lstDrops.SelectionMode = mode;
			this.lstBoosts.SelectionMode = mode;
			this.lstItems.SelectionMode = mode;
		}

		public void BotStateChanged(bool IsRunning)
		{
			/*if (IsRunning)
			{
				btnBotStart.Hide();
				btnBotStop.Show();
			}
			else
			{
				btnBotStop.Hide();
				btnBotStart.Show();
			}*/
			btnUp.Enabled = !IsRunning;
			btnDown.Enabled = !IsRunning;
			btnClear.Enabled = !IsRunning;
			btnRemove.Enabled = !IsRunning;
		}

		public void AddQuest(int QuestID, string ItemID = null, bool completeInBlank = false)
		{
			Quest quest = new Quest
			{
				Id = QuestID,
				ItemId = ItemID,
				CompleteInBlank = completeInBlank
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
			this.lstCommands = new System.Windows.Forms.ListBox();
			this.lstBoosts = new System.Windows.Forms.ListBox();
			this.lstDrops = new System.Windows.Forms.ListBox();
			this.lstItems = new System.Windows.Forms.ListBox();
			this.lstQuests = new System.Windows.Forms.ListBox();
			this.lstSkills = new System.Windows.Forms.ListBox();
			this.mainTabControl = new FlatTabControl.FlatTabControl();
			this.tabCombat = new System.Windows.Forms.TabPage();
			this.chkUseSkillTargeted = new DarkUI.Controls.DarkCheckBox();
			this.btnAllSkill = new DarkUI.Controls.DarkButton();
			this.darkLabel1 = new DarkUI.Controls.DarkLabel();
			this.darkTextBox1 = new DarkUI.Controls.DarkTextBox();
			this.darkCheckBox1 = new DarkUI.Controls.DarkCheckBox();
			this.chkAddToWhiteList = new DarkUI.Controls.DarkCheckBox();
			this.cbSafeType = new DarkUI.Controls.DarkComboBox();
			this.btnUseSkillSet = new DarkUI.Controls.DarkButton();
			this.btnAddSkillSet = new DarkUI.Controls.DarkButton();
			this.txtSkillSet = new DarkUI.Controls.DarkTextBox();
			this.chkSafeMp = new DarkUI.Controls.DarkCheckBox();
			this.label17 = new DarkUI.Controls.DarkLabel();
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
			this.tabMap = new System.Windows.Forms.TabPage();
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
			this.tabItem = new System.Windows.Forms.TabPage();
			this.darkGroupBox5 = new DarkUI.Controls.DarkGroupBox();
			this.btnMapItem = new DarkUI.Controls.DarkButton();
			this.numMapItem = new DarkUI.Controls.DarkNumericUpDown();
			this.darkGroupBox4 = new DarkUI.Controls.DarkGroupBox();
			this.chkPickupAll = new DarkUI.Controls.DarkCheckBox();
			this.chkPickup = new DarkUI.Controls.DarkCheckBox();
			this.chkReject = new DarkUI.Controls.DarkCheckBox();
			this.chkPickupAcTag = new DarkUI.Controls.DarkCheckBox();
			this.chkBankOnStop = new DarkUI.Controls.DarkCheckBox();
			this.chkRejectAll = new DarkUI.Controls.DarkCheckBox();
			this.darkGroupBox3 = new DarkUI.Controls.DarkGroupBox();
			this.numShopId = new DarkUI.Controls.DarkNumericUpDown();
			this.btnLoadShop = new DarkUI.Controls.DarkButton();
			this.txtShopItem = new DarkUI.Controls.DarkTextBox();
			this.btnBuy = new DarkUI.Controls.DarkButton();
			this.btnBuyFast = new DarkUI.Controls.DarkButton();
			this.darkGroupBox2 = new DarkUI.Controls.DarkGroupBox();
			this.btnWhitelistToggle = new DarkUI.Controls.DarkButton();
			this.btnWhitelistOn = new DarkUI.Controls.DarkButton();
			this.btnWhitelistOff = new DarkUI.Controls.DarkButton();
			this.label1 = new DarkUI.Controls.DarkLabel();
			this.numDropDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.btnBoost = new DarkUI.Controls.DarkButton();
			this.cbBoosts = new DarkUI.Controls.DarkComboBox();
			this.btnSwap = new DarkUI.Controls.DarkButton();
			this.txtSwapInv = new DarkUI.Controls.DarkTextBox();
			this.txtSwapBank = new DarkUI.Controls.DarkTextBox();
			this.btnWhitelist = new DarkUI.Controls.DarkButton();
			this.btnBoth = new DarkUI.Controls.DarkButton();
			this.txtWhitelist = new DarkUI.Controls.DarkTextBox();
			this.btnItem = new DarkUI.Controls.DarkButton();
			this.btnUnbanklst = new DarkUI.Controls.DarkButton();
			this.txtItem = new DarkUI.Controls.DarkTextBox();
			this.cbItemCmds = new DarkUI.Controls.DarkComboBox();
			this.tabQuest = new System.Windows.Forms.TabPage();
			this.chkQRequirements = new DarkUI.Controls.DarkCheckBox();
			this.chkQRewards = new DarkUI.Controls.DarkCheckBox();
			this.btnQAddToWhitelist = new DarkUI.Controls.DarkButton();
			this.numQQuestId = new DarkUI.Controls.DarkNumericUpDown();
			this.darkLabel4 = new DarkUI.Controls.DarkLabel();
			this.chkReloginCompleteQuest = new DarkUI.Controls.DarkCheckBox();
			this.chkInBlankCell = new DarkUI.Controls.DarkCheckBox();
			this.label14 = new DarkUI.Controls.DarkLabel();
			this.numEnsureTries = new DarkUI.Controls.DarkNumericUpDown();
			this.btnQuestAccept = new DarkUI.Controls.DarkButton();
			this.btnQuestComplete = new DarkUI.Controls.DarkButton();
			this.btnQuestAdd = new DarkUI.Controls.DarkButton();
			this.numQuestItem = new DarkUI.Controls.DarkNumericUpDown();
			this.chkQuestItem = new DarkUI.Controls.DarkCheckBox();
			this.numQuestID = new DarkUI.Controls.DarkNumericUpDown();
			this.label4 = new DarkUI.Controls.DarkLabel();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.darkGroupBox11 = new DarkUI.Controls.DarkGroupBox();
			this.lbLabels = new DarkUI.Controls.DarkListBox(this.components);
			this.btnAddLabel = new DarkUI.Controls.DarkButton();
			this.btnGotoLabel = new DarkUI.Controls.DarkButton();
			this.txtLabel = new DarkUI.Controls.DarkTextBox();
			this.darkGroupBox10 = new DarkUI.Controls.DarkGroupBox();
			this.btnStop = new DarkUI.Controls.DarkButton();
			this.btnRestart = new DarkUI.Controls.DarkButton();
			this.numSpamTimes = new DarkUI.Controls.DarkNumericUpDown();
			this.darkGroupBox14 = new DarkUI.Controls.DarkGroupBox();
			this.txtPlayer = new DarkUI.Controls.DarkTextBox();
			this.btnGoto = new DarkUI.Controls.DarkButton();
			this.darkGroupBox12 = new DarkUI.Controls.DarkGroupBox();
			this.txtSetInt = new DarkUI.Controls.DarkTextBox();
			this.btnSetInt = new DarkUI.Controls.DarkButton();
			this.numSetInt = new DarkUI.Controls.DarkNumericUpDown();
			this.btnIncreaseInt = new DarkUI.Controls.DarkButton();
			this.btnDecreaseInt = new DarkUI.Controls.DarkButton();
			this.btnReturnCmd = new DarkUI.Controls.DarkButton();
			this.darkGroupBox8 = new DarkUI.Controls.DarkGroupBox();
			this.numIndexCmd = new DarkUI.Controls.DarkNumericUpDown();
			this.btnGotoIndex = new DarkUI.Controls.DarkButton();
			this.btnClearTempVar = new DarkUI.Controls.DarkButton();
			this.btnGoUpIndex = new DarkUI.Controls.DarkButton();
			this.btnGoDownIndex = new DarkUI.Controls.DarkButton();
			this.darkGroupBox7 = new DarkUI.Controls.DarkGroupBox();
			this.btnProvokeOff = new DarkUI.Controls.DarkButton();
			this.btnProvokeOn = new DarkUI.Controls.DarkButton();
			this.btnBlank = new DarkUI.Controls.DarkButton();
			this.btnLogout = new DarkUI.Controls.DarkButton();
			this.btnBeep = new DarkUI.Controls.DarkButton();
			this.numBeepTimes = new DarkUI.Controls.DarkNumericUpDown();
			this.btnDelay = new DarkUI.Controls.DarkButton();
			this.numDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.btnStatementAdd = new DarkUI.Controls.DarkButton();
			this.txtStatement2 = new DarkUI.Controls.DarkTextBox();
			this.txtStatement1 = new DarkUI.Controls.DarkTextBox();
			this.cbStatement = new DarkUI.Controls.DarkComboBox();
			this.cbCategories = new DarkUI.Controls.DarkComboBox();
			this.txtPacket = new DarkUI.Controls.DarkTextBox();
			this.btnClientPacket = new DarkUI.Controls.DarkButton();
			this.btnPacket = new DarkUI.Controls.DarkButton();
			this.btnLoadCmd = new DarkUI.Controls.DarkButton();
			this.tabMisc2 = new System.Windows.Forms.TabPage();
			this.darkGroupBox15 = new DarkUI.Controls.DarkGroupBox();
			this.btnFollowCmd = new DarkUI.Controls.DarkButton();
			this.tbFollowPlayer = new DarkUI.Controls.DarkTextBox();
			this.darkGroupBox13 = new DarkUI.Controls.DarkGroupBox();
			this.label3 = new DarkUI.Controls.DarkLabel();
			this.chkSkip = new DarkUI.Controls.DarkCheckBox();
			this.btnBotDelay = new DarkUI.Controls.DarkButton();
			this.numBotDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.chkRestartDeath = new DarkUI.Controls.DarkCheckBox();
			this.darkGroupBox9 = new DarkUI.Controls.DarkGroupBox();
			this.txtAuthor = new DarkUI.Controls.DarkTextBox();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.btnSave = new DarkUI.Controls.DarkButton();
			this.btnLoad = new DarkUI.Controls.DarkButton();
			this.txtDescription = new DarkUI.Controls.DarkTextBox();
			this.chkMerge = new DarkUI.Controls.DarkCheckBox();
			this.darkGroupBox1 = new DarkUI.Controls.DarkGroupBox();
			this.btnBSStop = new DarkUI.Controls.DarkButton();
			this.label8 = new System.Windows.Forms.Label();
			this.tbBSLabel = new DarkUI.Controls.DarkTextBox();
			this.numBSDelay = new DarkUI.Controls.DarkNumericUpDown();
			this.tbBSPacket = new DarkUI.Controls.DarkTextBox();
			this.btnBSStart = new DarkUI.Controls.DarkButton();
			this.tabOptions = new System.Windows.Forms.TabPage();
			this.chkFollowOnly = new DarkUI.Controls.DarkCheckBox();
			this.tbFollowPlayer2 = new DarkUI.Controls.DarkTextBox();
			this.chkWalkSpeed = new DarkUI.Controls.DarkCheckBox();
			this.darkGroupBox6 = new DarkUI.Controls.DarkGroupBox();
			this.splitContainer5 = new System.Windows.Forms.SplitContainer();
			this.btnLog = new DarkUI.Controls.DarkButton();
			this.btnLogDebug = new DarkUI.Controls.DarkButton();
			this.lstLogText = new System.Windows.Forms.ListBox();
			this.txtLog = new DarkUI.Controls.DarkTextBox();
			this.label5 = new DarkUI.Controls.DarkLabel();
			this.label6 = new DarkUI.Controls.DarkLabel();
			this.numOptionsTimer = new DarkUI.Controls.DarkNumericUpDown();
			this.chkEnableSettings = new DarkUI.Controls.DarkCheckBox();
			this.chkDisableAnims = new DarkUI.Controls.DarkCheckBox();
			this.txtSoundItem = new DarkUI.Controls.DarkTextBox();
			this.btnSoundAdd = new DarkUI.Controls.DarkButton();
			this.btnSoundDelete = new DarkUI.Controls.DarkButton();
			this.btnSoundTest = new DarkUI.Controls.DarkButton();
			this.lstSoundItems = new System.Windows.Forms.ListBox();
			this.label9 = new DarkUI.Controls.DarkLabel();
			this.numWalkSpeed = new DarkUI.Controls.DarkNumericUpDown();
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
			this.tabOptions2 = new System.Windows.Forms.TabPage();
			this.darkGroupBox17 = new DarkUI.Controls.DarkGroupBox();
			this.chkLoginLock = new DarkUI.Controls.DarkCheckBox();
			this.tbLoginUsername = new DarkUI.Controls.DarkTextBox();
			this.tbLoginPassword = new DarkUI.Controls.DarkTextBox();
			this.colorfulCommands = new DarkUI.Controls.DarkCheckBox();
			this.darkGroupBox16 = new DarkUI.Controls.DarkGroupBox();
			this.btnAMTest = new DarkUI.Controls.DarkButton();
			this.chkAMStopBot = new DarkUI.Controls.DarkCheckBox();
			this.chkAMLogout = new DarkUI.Controls.DarkCheckBox();
			this.chkAntiMod = new DarkUI.Controls.DarkCheckBox();
			this.btnSetSpawn2 = new DarkUI.Controls.DarkButton();
			this.darkLabel6 = new DarkUI.Controls.DarkLabel();
			this.numSaveProgress = new DarkUI.Controls.DarkNumericUpDown();
			this.chkSaveProgress = new DarkUI.Controls.DarkCheckBox();
			this.chkAntiCounter = new DarkUI.Controls.DarkCheckBox();
			this.numPrivateRoom = new DarkUI.Controls.DarkTextBox();
			this.chkPrivateRoom = new DarkUI.Controls.DarkCheckBox();
			this.chkGender = new DarkUI.Controls.DarkCheckBox();
			this.numSetFPS = new DarkUI.Controls.DarkNumericUpDown();
			this.btnSetFPS = new DarkUI.Controls.DarkButton();
			this.lblUP = new System.Windows.Forms.Label();
			this.btnSetLevelCmd = new DarkUI.Controls.DarkButton();
			this.btnSetLevel = new DarkUI.Controls.DarkButton();
			this.tbLevel = new DarkUI.Controls.DarkTextBox();
			this.groupBox1 = new DarkUI.Controls.DarkGroupBox();
			this.btnAddInfoMsg = new DarkUI.Controls.DarkButton();
			this.btnAddWarnMsg = new DarkUI.Controls.DarkButton();
			this.inputMsgClient = new DarkUI.Controls.DarkTextBox();
			this.btnSearchCmd = new DarkUI.Controls.DarkButton();
			this.txtSearchCmd = new DarkUI.Controls.DarkTextBox();
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
			this.tabHunt = new System.Windows.Forms.TabPage();
			this.btnAddCmdHunt = new DarkUI.Controls.DarkButton();
			this.chkIsTempF = new DarkUI.Controls.DarkCheckBox();
			this.darkLabel3 = new DarkUI.Controls.DarkLabel();
			this.tbGetAfterF = new DarkUI.Controls.DarkTextBox();
			this.chkGetAfterF = new DarkUI.Controls.DarkCheckBox();
			this.chkAddToWhitelistF = new DarkUI.Controls.DarkCheckBox();
			this.tbItemQtyF = new DarkUI.Controls.DarkTextBox();
			this.tbItemNameF = new DarkUI.Controls.DarkTextBox();
			this.tbMonNameF = new DarkUI.Controls.DarkTextBox();
			this.cbBlankFirstF = new DarkUI.Controls.DarkCheckBox();
			this.btnGetMapF = new DarkUI.Controls.DarkButton();
			this.tbPadF = new DarkUI.Controls.DarkTextBox();
			this.tbCellF = new DarkUI.Controls.DarkTextBox();
			this.tbMapF = new DarkUI.Controls.DarkTextBox();
			this.darkLabel2 = new DarkUI.Controls.DarkLabel();
			this.tabBots = new System.Windows.Forms.TabPage();
			this.txtSavedDesc = new DarkUI.Controls.DarkTextBox();
			this.darkPanel1 = new DarkUI.Controls.DarkPanel();
			this.treeBots = new System.Windows.Forms.TreeView();
			this.lblBoosts = new DarkUI.Controls.DarkLabel();
			this.lblDrops = new DarkUI.Controls.DarkLabel();
			this.lblQuests = new DarkUI.Controls.DarkLabel();
			this.lblSkills = new DarkUI.Controls.DarkLabel();
			this.lblCommands = new DarkUI.Controls.DarkLabel();
			this.lblItems = new DarkUI.Controls.DarkLabel();
			this.txtSavedAuthor = new DarkUI.Controls.DarkTextBox();
			this.lblBots = new DarkUI.Controls.DarkLabel();
			this.txtSavedAdd = new DarkUI.Controls.DarkTextBox();
			this.btnSavedAdd = new DarkUI.Controls.DarkButton();
			this.txtSaved = new DarkUI.Controls.DarkTextBox();
			this.tabInfo = new System.Windows.Forms.TabPage();
			this.panel5 = new System.Windows.Forms.Panel();
			this.richTextBox2 = new System.Windows.Forms.RichTextBox();
			this.rtbInfo = new System.Windows.Forms.RichTextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.cbLists = new DarkUI.Controls.DarkComboBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnDown = new DarkUI.Controls.DarkButton();
			this.chkAll = new DarkUI.Controls.DarkCheckBox();
			this.btnClear = new DarkUI.Controls.DarkButton();
			this.panel4 = new System.Windows.Forms.Panel();
			this.chkEnable = new System.Windows.Forms.CheckBox();
			this.btnRemove = new DarkUI.Controls.DarkButton();
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
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.mainTabControl.SuspendLayout();
			this.tabCombat.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRestMP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numRest)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSkillD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSafe)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSkill)).BeginInit();
			this.tabMap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numWalkY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numWalkX)).BeginInit();
			this.tabItem.SuspendLayout();
			this.darkGroupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numMapItem)).BeginInit();
			this.darkGroupBox4.SuspendLayout();
			this.darkGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numShopId)).BeginInit();
			this.darkGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numDropDelay)).BeginInit();
			this.tabQuest.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numQQuestId)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numEnsureTries)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuestItem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuestID)).BeginInit();
			this.tabMisc.SuspendLayout();
			this.darkGroupBox11.SuspendLayout();
			this.darkGroupBox10.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSpamTimes)).BeginInit();
			this.darkGroupBox14.SuspendLayout();
			this.darkGroupBox12.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSetInt)).BeginInit();
			this.darkGroupBox8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numIndexCmd)).BeginInit();
			this.darkGroupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBeepTimes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
			this.tabMisc2.SuspendLayout();
			this.darkGroupBox15.SuspendLayout();
			this.darkGroupBox13.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBotDelay)).BeginInit();
			this.darkGroupBox9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.darkGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBSDelay)).BeginInit();
			this.tabOptions.SuspendLayout();
			this.darkGroupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
			this.splitContainer5.Panel1.SuspendLayout();
			this.splitContainer5.Panel2.SuspendLayout();
			this.splitContainer5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numOptionsTimer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numWalkSpeed)).BeginInit();
			this.grpLogin.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRelogDelay)).BeginInit();
			this.tabOptions2.SuspendLayout();
			this.darkGroupBox17.SuspendLayout();
			this.darkGroupBox16.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSaveProgress)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSetFPS)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.grpAccessLevel.SuspendLayout();
			this.grpAlignment.SuspendLayout();
			this.tabHunt.SuspendLayout();
			this.tabBots.SuspendLayout();
			this.darkPanel1.SuspendLayout();
			this.tabInfo.SuspendLayout();
			this.panel5.SuspendLayout();
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
			this.lstCommands.AllowDrop = true;
			this.lstCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.lstCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstCommands.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lstCommands.ForeColor = System.Drawing.Color.Gainsboro;
			this.lstCommands.FormattingEnabled = true;
			this.lstCommands.HorizontalScrollbar = true;
			this.lstCommands.IntegralHeight = false;
			this.lstCommands.ItemHeight = 15;
			this.lstCommands.Location = new System.Drawing.Point(0, 0);
			this.lstCommands.Name = "lstCommands";
			this.lstCommands.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lstCommands.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstCommands.Size = new System.Drawing.Size(258, 254);
			this.lstCommands.TabIndex = 1;
			this.lstCommands.Click += new System.EventHandler(this.lstCommands_Click);
			this.lstCommands.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCommands_DrawItem);
			this.lstCommands.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstCommands_DragDrop);
			this.lstCommands.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstCommands_DragEnter);
			this.lstCommands.DoubleClick += new System.EventHandler(this.lstCommands_DoubleClick);
			this.lstCommands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			this.lstCommands.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstCommands_KeyPress);
			// 
			// lstBoosts
			// 
			this.lstBoosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstBoosts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.lstBoosts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.lstBoosts.FormattingEnabled = true;
			this.lstBoosts.HorizontalScrollbar = true;
			this.lstBoosts.Location = new System.Drawing.Point(0, 0);
			this.lstBoosts.Name = "lstBoosts";
			this.lstBoosts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstBoosts.Size = new System.Drawing.Size(258, 251);
			this.lstBoosts.TabIndex = 25;
			this.lstBoosts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			// 
			// lstDrops
			// 
			this.lstDrops.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstDrops.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.lstDrops.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.lstDrops.FormattingEnabled = true;
			this.lstDrops.HorizontalScrollbar = true;
			this.lstDrops.Location = new System.Drawing.Point(0, 0);
			this.lstDrops.Name = "lstDrops";
			this.lstDrops.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstDrops.Size = new System.Drawing.Size(258, 251);
			this.lstDrops.TabIndex = 26;
			this.lstDrops.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			// 
			// lstItems
			// 
			this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.lstItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.lstItems.FormattingEnabled = true;
			this.lstItems.HorizontalScrollbar = true;
			this.lstItems.Location = new System.Drawing.Point(0, 0);
			this.lstItems.Name = "lstItems";
			this.lstItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstItems.Size = new System.Drawing.Size(258, 251);
			this.lstItems.TabIndex = 145;
			this.lstItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			// 
			// lstQuests
			// 
			this.lstQuests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstQuests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.lstQuests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.lstQuests.FormattingEnabled = true;
			this.lstQuests.HorizontalScrollbar = true;
			this.lstQuests.Location = new System.Drawing.Point(0, 0);
			this.lstQuests.Name = "lstQuests";
			this.lstQuests.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstQuests.Size = new System.Drawing.Size(258, 251);
			this.lstQuests.TabIndex = 27;
			this.lstQuests.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			// 
			// lstSkills
			// 
			this.lstSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstSkills.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.lstSkills.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.lstSkills.FormattingEnabled = true;
			this.lstSkills.HorizontalScrollbar = true;
			this.lstSkills.Location = new System.Drawing.Point(0, 0);
			this.lstSkills.Name = "lstSkills";
			this.lstSkills.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lstSkills.Size = new System.Drawing.Size(258, 251);
			this.lstSkills.TabIndex = 28;
			this.lstSkills.DoubleClick += new System.EventHandler(this.lstSkills_DoubleClick);
			this.lstSkills.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstBoxs_KeyPress);
			// 
			// mainTabControl
			// 
			this.mainTabControl.Controls.Add(this.tabCombat);
			this.mainTabControl.Controls.Add(this.tabMap);
			this.mainTabControl.Controls.Add(this.tabItem);
			this.mainTabControl.Controls.Add(this.tabQuest);
			this.mainTabControl.Controls.Add(this.tabMisc);
			this.mainTabControl.Controls.Add(this.tabMisc2);
			this.mainTabControl.Controls.Add(this.tabOptions);
			this.mainTabControl.Controls.Add(this.tabOptions2);
			this.mainTabControl.Controls.Add(this.tabHunt);
			this.mainTabControl.Controls.Add(this.tabBots);
			this.mainTabControl.Controls.Add(this.tabInfo);
			this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTabControl.Location = new System.Drawing.Point(0, 0);
			this.mainTabControl.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.mainTabControl.Name = "mainTabControl";
			this.mainTabControl.Padding = new System.Drawing.Point(3, 2);
			this.mainTabControl.SelectedIndex = 0;
			this.mainTabControl.Size = new System.Drawing.Size(539, 328);
			this.mainTabControl.TabIndex = 146;
			this.mainTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
			// 
			// tabCombat
			// 
			this.tabCombat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabCombat.Controls.Add(this.chkUseSkillTargeted);
			this.tabCombat.Controls.Add(this.btnAllSkill);
			this.tabCombat.Controls.Add(this.darkLabel1);
			this.tabCombat.Controls.Add(this.darkTextBox1);
			this.tabCombat.Controls.Add(this.darkCheckBox1);
			this.tabCombat.Controls.Add(this.chkAddToWhiteList);
			this.tabCombat.Controls.Add(this.cbSafeType);
			this.tabCombat.Controls.Add(this.btnUseSkillSet);
			this.tabCombat.Controls.Add(this.btnAddSkillSet);
			this.tabCombat.Controls.Add(this.txtSkillSet);
			this.tabCombat.Controls.Add(this.chkSafeMp);
			this.tabCombat.Controls.Add(this.label17);
			this.tabCombat.Controls.Add(this.btnRest);
			this.tabCombat.Controls.Add(this.btnRestF);
			this.tabCombat.Controls.Add(this.chkSkillCD);
			this.tabCombat.Controls.Add(this.label12);
			this.tabCombat.Controls.Add(this.label11);
			this.tabCombat.Controls.Add(this.label10);
			this.tabCombat.Controls.Add(this.btnAttack);
			this.tabCombat.Controls.Add(this.btnKill);
			this.tabCombat.Controls.Add(this.label13);
			this.tabCombat.Controls.Add(this.chkExistQuest);
			this.tabCombat.Controls.Add(this.numRestMP);
			this.tabCombat.Controls.Add(this.chkMP);
			this.tabCombat.Controls.Add(this.numRest);
			this.tabCombat.Controls.Add(this.chkHP);
			this.tabCombat.Controls.Add(this.chkPacket);
			this.tabCombat.Controls.Add(this.numSkillD);
			this.tabCombat.Controls.Add(this.label2);
			this.tabCombat.Controls.Add(this.numSafe);
			this.tabCombat.Controls.Add(this.btnAddSafe);
			this.tabCombat.Controls.Add(this.btnSkillCmd);
			this.tabCombat.Controls.Add(this.btnAddSkill);
			this.tabCombat.Controls.Add(this.numSkill);
			this.tabCombat.Controls.Add(this.chkExitRest);
			this.tabCombat.Controls.Add(this.chkAllSkillsCD);
			this.tabCombat.Controls.Add(this.txtKillFQ);
			this.tabCombat.Controls.Add(this.txtKillFItem);
			this.tabCombat.Controls.Add(this.txtKillFMon);
			this.tabCombat.Controls.Add(this.rbTemp);
			this.tabCombat.Controls.Add(this.rbItems);
			this.tabCombat.Controls.Add(this.btnKillF);
			this.tabCombat.Controls.Add(this.txtMonster);
			this.tabCombat.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabCombat.Location = new System.Drawing.Point(4, 23);
			this.tabCombat.Margin = new System.Windows.Forms.Padding(0);
			this.tabCombat.Name = "tabCombat";
			this.tabCombat.Padding = new System.Windows.Forms.Padding(3);
			this.tabCombat.Size = new System.Drawing.Size(531, 301);
			this.tabCombat.TabIndex = 0;
			this.tabCombat.Text = "Combat";
			// 
			// chkUseSkillTargeted
			// 
			this.chkUseSkillTargeted.AutoSize = true;
			this.chkUseSkillTargeted.Checked = true;
			this.chkUseSkillTargeted.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkUseSkillTargeted.Location = new System.Drawing.Point(279, 32);
			this.chkUseSkillTargeted.Name = "chkUseSkillTargeted";
			this.chkUseSkillTargeted.Size = new System.Drawing.Size(69, 17);
			this.chkUseSkillTargeted.TabIndex = 73;
			this.chkUseSkillTargeted.Text = "Targeted";
			this.chkUseSkillTargeted.MouseHover += new System.EventHandler(this.chkUseSkillTargeted_MouseHover);
			// 
			// btnAllSkill
			// 
			this.btnAllSkill.Checked = false;
			this.btnAllSkill.Location = new System.Drawing.Point(274, 5);
			this.btnAllSkill.Name = "btnAllSkill";
			this.btnAllSkill.Size = new System.Drawing.Size(29, 21);
			this.btnAllSkill.TabIndex = 72;
			this.btnAllSkill.Text = "All";
			this.btnAllSkill.Click += new System.EventHandler(this.btnAllSkill_Click);
			// 
			// darkLabel1
			// 
			this.darkLabel1.AutoSize = true;
			this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel1.Location = new System.Drawing.Point(130, 108);
			this.darkLabel1.Name = "darkLabel1";
			this.darkLabel1.Size = new System.Drawing.Size(24, 13);
			this.darkLabel1.TabIndex = 71;
			this.darkLabel1.Text = "kills";
			// 
			// darkTextBox1
			// 
			this.darkTextBox1.Location = new System.Drawing.Point(94, 103);
			this.darkTextBox1.Name = "darkTextBox1";
			this.darkTextBox1.Size = new System.Drawing.Size(35, 20);
			this.darkTextBox1.TabIndex = 70;
			this.darkTextBox1.Text = "5";
			// 
			// darkCheckBox1
			// 
			this.darkCheckBox1.AutoSize = true;
			this.darkCheckBox1.Location = new System.Drawing.Point(6, 104);
			this.darkCheckBox1.Name = "darkCheckBox1";
			this.darkCheckBox1.Size = new System.Drawing.Size(90, 17);
			this.darkCheckBox1.TabIndex = 69;
			this.darkCheckBox1.Text = "Get drops per";
			// 
			// chkAddToWhiteList
			// 
			this.chkAddToWhiteList.AutoSize = true;
			this.chkAddToWhiteList.Checked = true;
			this.chkAddToWhiteList.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAddToWhiteList.Location = new System.Drawing.Point(6, 129);
			this.chkAddToWhiteList.Name = "chkAddToWhiteList";
			this.chkAddToWhiteList.Size = new System.Drawing.Size(99, 17);
			this.chkAddToWhiteList.TabIndex = 68;
			this.chkAddToWhiteList.Text = "add to Whitelist";
			// 
			// cbSafeType
			// 
			this.cbSafeType.FormattingEnabled = true;
			this.cbSafeType.Items.AddRange(new object[] {
            "<= Lower than",
            ">= Greater than"});
			this.cbSafeType.Location = new System.Drawing.Point(163, 104);
			this.cbSafeType.Name = "cbSafeType";
			this.cbSafeType.Size = new System.Drawing.Size(110, 21);
			this.cbSafeType.TabIndex = 67;
			// 
			// btnUseSkillSet
			// 
			this.btnUseSkillSet.Checked = false;
			this.btnUseSkillSet.Location = new System.Drawing.Point(387, 53);
			this.btnUseSkillSet.Name = "btnUseSkillSet";
			this.btnUseSkillSet.Size = new System.Drawing.Size(115, 22);
			this.btnUseSkillSet.TabIndex = 65;
			this.btnUseSkillSet.Text = "Use Skill Set";
			this.btnUseSkillSet.Click += new System.EventHandler(this.btnUseSkillSet_Click);
			// 
			// btnAddSkillSet
			// 
			this.btnAddSkillSet.Checked = false;
			this.btnAddSkillSet.Location = new System.Drawing.Point(387, 29);
			this.btnAddSkillSet.Name = "btnAddSkillSet";
			this.btnAddSkillSet.Size = new System.Drawing.Size(115, 22);
			this.btnAddSkillSet.TabIndex = 64;
			this.btnAddSkillSet.Text = "Add Skill Set";
			this.btnAddSkillSet.Click += new System.EventHandler(this.btnAddSkillSet_Click);
			// 
			// txtSkillSet
			// 
			this.txtSkillSet.Location = new System.Drawing.Point(387, 6);
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
			this.chkSafeMp.Location = new System.Drawing.Point(231, 83);
			this.chkSafeMp.Name = "chkSafeMp";
			this.chkSafeMp.Size = new System.Drawing.Size(42, 17);
			this.chkSafeMp.TabIndex = 58;
			this.chkSafeMp.Text = "MP";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label17.Location = new System.Drawing.Point(164, 84);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(66, 13);
			this.label17.TabIndex = 62;
			this.label17.Text = "When HP or";
			// 
			// btnRest
			// 
			this.btnRest.Checked = false;
			this.btnRest.Location = new System.Drawing.Point(384, 101);
			this.btnRest.Name = "btnRest";
			this.btnRest.Size = new System.Drawing.Size(44, 22);
			this.btnRest.TabIndex = 43;
			this.btnRest.Text = "Rest";
			this.btnRest.Click += new System.EventHandler(this.btnRest_Click);
			// 
			// btnRestF
			// 
			this.btnRestF.Checked = false;
			this.btnRestF.Location = new System.Drawing.Point(431, 101);
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
			this.chkSkillCD.Location = new System.Drawing.Point(6, 254);
			this.chkSkillCD.Name = "chkSkillCD";
			this.chkSkillCD.Size = new System.Drawing.Size(144, 17);
			this.chkSkillCD.TabIndex = 60;
			this.chkSkillCD.Text = "Wait for skill to cooldown";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label12.Location = new System.Drawing.Point(382, 124);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(37, 13);
			this.label12.TabIndex = 57;
			this.label12.Text = "Rest if";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label11.Location = new System.Drawing.Point(487, 156);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(15, 13);
			this.label11.TabIndex = 56;
			this.label11.Text = "%";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label10.Location = new System.Drawing.Point(487, 138);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(15, 13);
			this.label10.TabIndex = 55;
			this.label10.Text = "%";
			// 
			// btnAttack
			// 
			this.btnAttack.Checked = false;
			this.btnAttack.Location = new System.Drawing.Point(77, 29);
			this.btnAttack.Name = "btnAttack";
			this.btnAttack.Size = new System.Drawing.Size(70, 22);
			this.btnAttack.TabIndex = 54;
			this.btnAttack.Text = "Attack";
			this.btnAttack.Click += new System.EventHandler(this.btnAttack_Click);
			// 
			// btnKill
			// 
			this.btnKill.Checked = false;
			this.btnKill.Location = new System.Drawing.Point(4, 29);
			this.btnKill.Name = "btnKill";
			this.btnKill.Size = new System.Drawing.Size(70, 22);
			this.btnKill.TabIndex = 54;
			this.btnKill.Text = "Kill";
			this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label13.Location = new System.Drawing.Point(163, 160);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(54, 13);
			this.label13.TabIndex = 53;
			this.label13.Text = "Skill delay";
			// 
			// chkExistQuest
			// 
			this.chkExistQuest.AutoSize = true;
			this.chkExistQuest.Checked = true;
			this.chkExistQuest.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkExistQuest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.chkExistQuest.Location = new System.Drawing.Point(244, 278);
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
			this.numRestMP.Location = new System.Drawing.Point(451, 154);
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
			this.chkMP.Location = new System.Drawing.Point(386, 157);
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
			this.numRest.Location = new System.Drawing.Point(451, 135);
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
			this.chkHP.Location = new System.Drawing.Point(386, 140);
			this.chkHP.Name = "chkHP";
			this.chkHP.Size = new System.Drawing.Size(56, 17);
			this.chkHP.TabIndex = 47;
			this.chkHP.Text = "HP <=";
			// 
			// chkPacket
			// 
			this.chkPacket.AutoSize = true;
			this.chkPacket.Location = new System.Drawing.Point(389, 80);
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
			this.numSkillD.Location = new System.Drawing.Point(164, 174);
			this.numSkillD.LoopValues = false;
			this.numSkillD.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			this.numSkillD.Name = "numSkillD";
			this.numSkillD.Size = new System.Drawing.Size(81, 20);
			this.numSkillD.TabIndex = 45;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label2.Location = new System.Drawing.Point(211, 134);
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
			this.numSafe.Location = new System.Drawing.Point(164, 132);
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
            60,
            0,
            0,
            0});
			// 
			// btnAddSafe
			// 
			this.btnAddSafe.Checked = false;
			this.btnAddSafe.Location = new System.Drawing.Point(164, 54);
			this.btnAddSafe.Name = "btnAddSafe";
			this.btnAddSafe.Size = new System.Drawing.Size(109, 22);
			this.btnAddSafe.TabIndex = 39;
			this.btnAddSafe.Text = "Safe skill";
			this.btnAddSafe.Click += new System.EventHandler(this.btnAddSafe_Click);
			// 
			// btnSkillCmd
			// 
			this.btnSkillCmd.Checked = false;
			this.btnSkillCmd.Location = new System.Drawing.Point(164, 29);
			this.btnSkillCmd.Name = "btnSkillCmd";
			this.btnSkillCmd.Size = new System.Drawing.Size(109, 21);
			this.btnSkillCmd.TabIndex = 38;
			this.btnSkillCmd.Text = "Add (cmd)";
			this.btnSkillCmd.Click += new System.EventHandler(this.btnAddSkillCmd_Click);
			// 
			// btnAddSkill
			// 
			this.btnAddSkill.Checked = false;
			this.btnAddSkill.Location = new System.Drawing.Point(210, 5);
			this.btnAddSkill.Name = "btnAddSkill";
			this.btnAddSkill.Size = new System.Drawing.Size(63, 21);
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
			this.numSkill.Location = new System.Drawing.Point(164, 5);
			this.numSkill.LoopValues = false;
			this.numSkill.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numSkill.Name = "numSkill";
			this.numSkill.Size = new System.Drawing.Size(44, 20);
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
			this.chkExitRest.Location = new System.Drawing.Point(244, 254);
			this.chkExitRest.Name = "chkExitRest";
			this.chkExitRest.Size = new System.Drawing.Size(148, 17);
			this.chkExitRest.TabIndex = 36;
			this.chkExitRest.Text = "Exit combat before resting";
			// 
			// chkAllSkillsCD
			// 
			this.chkAllSkillsCD.AutoSize = true;
			this.chkAllSkillsCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.chkAllSkillsCD.Location = new System.Drawing.Point(6, 278);
			this.chkAllSkillsCD.Name = "chkAllSkillsCD";
			this.chkAllSkillsCD.Size = new System.Drawing.Size(165, 17);
			this.chkAllSkillsCD.TabIndex = 35;
			this.chkAllSkillsCD.Text = "Wait for all skills to cool down";
			// 
			// txtKillFQ
			// 
			this.txtKillFQ.Location = new System.Drawing.Point(6, 201);
			this.txtKillFQ.Name = "txtKillFQ";
			this.txtKillFQ.Size = new System.Drawing.Size(141, 20);
			this.txtKillFQ.TabIndex = 34;
			this.txtKillFQ.Text = "Quantity (* = any)";
			this.txtKillFQ.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtKillFQ.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// txtKillFItem
			// 
			this.txtKillFItem.Location = new System.Drawing.Point(6, 177);
			this.txtKillFItem.Name = "txtKillFItem";
			this.txtKillFItem.Size = new System.Drawing.Size(141, 20);
			this.txtKillFItem.TabIndex = 33;
			this.txtKillFItem.Text = "Item name";
			this.txtKillFItem.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtKillFItem.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// txtKillFMon
			// 
			this.txtKillFMon.Location = new System.Drawing.Point(6, 153);
			this.txtKillFMon.Name = "txtKillFMon";
			this.txtKillFMon.Size = new System.Drawing.Size(141, 20);
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
			this.btnKillF.Size = new System.Drawing.Size(143, 22);
			this.btnKillF.TabIndex = 29;
			this.btnKillF.Text = "Kill for [KF]";
			this.btnKillF.Click += new System.EventHandler(this.btnKillF_Click);
			// 
			// txtMonster
			// 
			this.txtMonster.Location = new System.Drawing.Point(4, 5);
			this.txtMonster.Name = "txtMonster";
			this.txtMonster.Size = new System.Drawing.Size(143, 20);
			this.txtMonster.TabIndex = 27;
			this.txtMonster.Text = "Monster (* = random)";
			this.txtMonster.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtMonster.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tabMap
			// 
			this.tabMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabMap.Controls.Add(this.btnCurrBlank);
			this.tabMap.Controls.Add(this.btnSetSpawn);
			this.tabMap.Controls.Add(this.btnWalkRdm);
			this.tabMap.Controls.Add(this.btnWalkCur);
			this.tabMap.Controls.Add(this.btnWalk);
			this.tabMap.Controls.Add(this.numWalkY);
			this.tabMap.Controls.Add(this.numWalkX);
			this.tabMap.Controls.Add(this.button2);
			this.tabMap.Controls.Add(this.btnCellSwap);
			this.tabMap.Controls.Add(this.btnJump);
			this.tabMap.Controls.Add(this.btnCurrCell);
			this.tabMap.Controls.Add(this.txtPad);
			this.tabMap.Controls.Add(this.txtCell);
			this.tabMap.Controls.Add(this.btnJoin);
			this.tabMap.Controls.Add(this.txtJoinPad);
			this.tabMap.Controls.Add(this.txtJoinCell);
			this.tabMap.Controls.Add(this.txtJoin);
			this.tabMap.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabMap.Location = new System.Drawing.Point(4, 23);
			this.tabMap.Margin = new System.Windows.Forms.Padding(0);
			this.tabMap.Name = "tabMap";
			this.tabMap.Padding = new System.Windows.Forms.Padding(3);
			this.tabMap.Size = new System.Drawing.Size(192, 73);
			this.tabMap.TabIndex = 2;
			this.tabMap.Text = "Map";
			// 
			// btnCurrBlank
			// 
			this.btnCurrBlank.Checked = false;
			this.btnCurrBlank.Location = new System.Drawing.Point(192, 79);
			this.btnCurrBlank.Name = "btnCurrBlank";
			this.btnCurrBlank.Size = new System.Drawing.Size(111, 23);
			this.btnCurrBlank.TabIndex = 143;
			this.btnCurrBlank.Text = "Jump Blank";
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
			this.button2.Location = new System.Drawing.Point(133, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(42, 22);
			this.button2.TabIndex = 34;
			this.button2.Text = ">";
			this.button2.Click += new System.EventHandler(this.btnCellSwap_Click);
			// 
			// btnCellSwap
			// 
			this.btnCellSwap.Checked = false;
			this.btnCellSwap.Location = new System.Drawing.Point(133, 29);
			this.btnCellSwap.Name = "btnCellSwap";
			this.btnCellSwap.Size = new System.Drawing.Size(42, 22);
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
			this.txtJoinPad.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtJoinPad.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// txtJoinCell
			// 
			this.txtJoinCell.Location = new System.Drawing.Point(6, 29);
			this.txtJoinCell.Name = "txtJoinCell";
			this.txtJoinCell.Size = new System.Drawing.Size(54, 20);
			this.txtJoinCell.TabIndex = 27;
			this.txtJoinCell.Text = "Enter";
			this.txtJoinCell.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtJoinCell.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// txtJoin
			// 
			this.txtJoin.Location = new System.Drawing.Point(6, 4);
			this.txtJoin.Name = "txtJoin";
			this.txtJoin.Size = new System.Drawing.Size(112, 20);
			this.txtJoin.TabIndex = 26;
			this.txtJoin.Text = "battleon-1e99";
			this.txtJoin.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtJoin.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tabItem
			// 
			this.tabItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabItem.Controls.Add(this.darkGroupBox5);
			this.tabItem.Controls.Add(this.darkGroupBox4);
			this.tabItem.Controls.Add(this.darkGroupBox3);
			this.tabItem.Controls.Add(this.darkGroupBox2);
			this.tabItem.Controls.Add(this.label1);
			this.tabItem.Controls.Add(this.numDropDelay);
			this.tabItem.Controls.Add(this.btnBoost);
			this.tabItem.Controls.Add(this.cbBoosts);
			this.tabItem.Controls.Add(this.btnSwap);
			this.tabItem.Controls.Add(this.txtSwapInv);
			this.tabItem.Controls.Add(this.txtSwapBank);
			this.tabItem.Controls.Add(this.btnWhitelist);
			this.tabItem.Controls.Add(this.btnBoth);
			this.tabItem.Controls.Add(this.txtWhitelist);
			this.tabItem.Controls.Add(this.btnItem);
			this.tabItem.Controls.Add(this.btnUnbanklst);
			this.tabItem.Controls.Add(this.txtItem);
			this.tabItem.Controls.Add(this.cbItemCmds);
			this.tabItem.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabItem.Location = new System.Drawing.Point(4, 23);
			this.tabItem.Margin = new System.Windows.Forms.Padding(0);
			this.tabItem.Name = "tabItem";
			this.tabItem.Padding = new System.Windows.Forms.Padding(3);
			this.tabItem.Size = new System.Drawing.Size(192, 73);
			this.tabItem.TabIndex = 1;
			this.tabItem.Text = "Item";
			// 
			// darkGroupBox5
			// 
			this.darkGroupBox5.Controls.Add(this.btnMapItem);
			this.darkGroupBox5.Controls.Add(this.numMapItem);
			this.darkGroupBox5.Location = new System.Drawing.Point(152, 157);
			this.darkGroupBox5.Name = "darkGroupBox5";
			this.darkGroupBox5.Size = new System.Drawing.Size(132, 76);
			this.darkGroupBox5.TabIndex = 161;
			this.darkGroupBox5.TabStop = false;
			this.darkGroupBox5.Text = "Map Item";
			// 
			// btnMapItem
			// 
			this.btnMapItem.Checked = false;
			this.btnMapItem.Location = new System.Drawing.Point(6, 45);
			this.btnMapItem.Name = "btnMapItem";
			this.btnMapItem.Size = new System.Drawing.Size(120, 22);
			this.btnMapItem.TabIndex = 29;
			this.btnMapItem.Text = "Get map item";
			this.btnMapItem.Click += new System.EventHandler(this.btnMapItem_Click);
			// 
			// numMapItem
			// 
			this.numMapItem.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numMapItem.Location = new System.Drawing.Point(6, 20);
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
			this.numMapItem.Size = new System.Drawing.Size(120, 20);
			this.numMapItem.TabIndex = 30;
			this.numMapItem.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// darkGroupBox4
			// 
			this.darkGroupBox4.Controls.Add(this.chkPickupAll);
			this.darkGroupBox4.Controls.Add(this.chkPickup);
			this.darkGroupBox4.Controls.Add(this.chkReject);
			this.darkGroupBox4.Controls.Add(this.chkPickupAcTag);
			this.darkGroupBox4.Controls.Add(this.chkBankOnStop);
			this.darkGroupBox4.Controls.Add(this.chkRejectAll);
			this.darkGroupBox4.Location = new System.Drawing.Point(287, 5);
			this.darkGroupBox4.Name = "darkGroupBox4";
			this.darkGroupBox4.Size = new System.Drawing.Size(148, 146);
			this.darkGroupBox4.TabIndex = 160;
			this.darkGroupBox4.TabStop = false;
			this.darkGroupBox4.Text = "Options";
			// 
			// chkPickupAll
			// 
			this.chkPickupAll.AutoSize = true;
			this.chkPickupAll.Location = new System.Drawing.Point(11, 19);
			this.chkPickupAll.Name = "chkPickupAll";
			this.chkPickupAll.Size = new System.Drawing.Size(102, 17);
			this.chkPickupAll.TabIndex = 149;
			this.chkPickupAll.Text = "Pick up all items";
			// 
			// chkPickup
			// 
			this.chkPickup.AutoSize = true;
			this.chkPickup.Checked = true;
			this.chkPickup.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPickup.Location = new System.Drawing.Point(11, 40);
			this.chkPickup.Name = "chkPickup";
			this.chkPickup.Size = new System.Drawing.Size(114, 17);
			this.chkPickup.TabIndex = 24;
			this.chkPickup.Text = "Pick up whitelisted";
			// 
			// chkReject
			// 
			this.chkReject.AutoSize = true;
			this.chkReject.Location = new System.Drawing.Point(11, 82);
			this.chkReject.Name = "chkReject";
			this.chkReject.Size = new System.Drawing.Size(130, 17);
			this.chkReject.TabIndex = 25;
			this.chkReject.Text = "Reject non-whitelisted";
			// 
			// chkPickupAcTag
			// 
			this.chkPickupAcTag.AutoSize = true;
			this.chkPickupAcTag.Location = new System.Drawing.Point(11, 61);
			this.chkPickupAcTag.Name = "chkPickupAcTag";
			this.chkPickupAcTag.Size = new System.Drawing.Size(135, 17);
			this.chkPickupAcTag.TabIndex = 157;
			this.chkPickupAcTag.Text = "Pick up ac-tagged only";
			// 
			// chkBankOnStop
			// 
			this.chkBankOnStop.AutoSize = true;
			this.chkBankOnStop.Location = new System.Drawing.Point(11, 103);
			this.chkBankOnStop.Name = "chkBankOnStop";
			this.chkBankOnStop.Size = new System.Drawing.Size(94, 17);
			this.chkBankOnStop.TabIndex = 148;
			this.chkBankOnStop.Text = "Bank on Stop ";
			this.chkBankOnStop.CheckedChanged += new System.EventHandler(this.chkBankOnStop_CheckedChanged);
			// 
			// chkRejectAll
			// 
			this.chkRejectAll.AutoSize = true;
			this.chkRejectAll.Enabled = false;
			this.chkRejectAll.Location = new System.Drawing.Point(11, 124);
			this.chkRejectAll.Name = "chkRejectAll";
			this.chkRejectAll.Size = new System.Drawing.Size(97, 17);
			this.chkRejectAll.TabIndex = 150;
			this.chkRejectAll.Text = "Reject all items";
			this.chkRejectAll.Visible = false;
			// 
			// darkGroupBox3
			// 
			this.darkGroupBox3.Controls.Add(this.numShopId);
			this.darkGroupBox3.Controls.Add(this.btnLoadShop);
			this.darkGroupBox3.Controls.Add(this.txtShopItem);
			this.darkGroupBox3.Controls.Add(this.btnBuy);
			this.darkGroupBox3.Controls.Add(this.btnBuyFast);
			this.darkGroupBox3.Location = new System.Drawing.Point(4, 157);
			this.darkGroupBox3.Name = "darkGroupBox3";
			this.darkGroupBox3.Size = new System.Drawing.Size(144, 100);
			this.darkGroupBox3.TabIndex = 159;
			this.darkGroupBox3.TabStop = false;
			this.darkGroupBox3.Text = "Shop";
			// 
			// numShopId
			// 
			this.numShopId.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numShopId.Location = new System.Drawing.Point(6, 18);
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
			this.numShopId.Size = new System.Drawing.Size(49, 20);
			this.numShopId.TabIndex = 40;
			this.numShopId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btnLoadShop
			// 
			this.btnLoadShop.Checked = false;
			this.btnLoadShop.Location = new System.Drawing.Point(59, 18);
			this.btnLoadShop.Name = "btnLoadShop";
			this.btnLoadShop.Size = new System.Drawing.Size(79, 22);
			this.btnLoadShop.TabIndex = 134;
			this.btnLoadShop.Text = "Load Shop";
			this.btnLoadShop.Click += new System.EventHandler(this.btnLoadShop_Click);
			// 
			// txtShopItem
			// 
			this.txtShopItem.Location = new System.Drawing.Point(6, 42);
			this.txtShopItem.Name = "txtShopItem";
			this.txtShopItem.Size = new System.Drawing.Size(132, 20);
			this.txtShopItem.TabIndex = 41;
			this.txtShopItem.Text = "Shop Item";
			this.txtShopItem.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtShopItem.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnBuy
			// 
			this.btnBuy.Checked = false;
			this.btnBuy.Location = new System.Drawing.Point(6, 64);
			this.btnBuy.Name = "btnBuy";
			this.btnBuy.Size = new System.Drawing.Size(65, 22);
			this.btnBuy.TabIndex = 38;
			this.btnBuy.Text = "Buy item";
			this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
			// 
			// btnBuyFast
			// 
			this.btnBuyFast.Checked = false;
			this.btnBuyFast.Location = new System.Drawing.Point(73, 64);
			this.btnBuyFast.Name = "btnBuyFast";
			this.btnBuyFast.Size = new System.Drawing.Size(65, 22);
			this.btnBuyFast.TabIndex = 133;
			this.btnBuyFast.Text = "Buy fast";
			this.btnBuyFast.Click += new System.EventHandler(this.btnBuyFast_Click);
			// 
			// darkGroupBox2
			// 
			this.darkGroupBox2.Controls.Add(this.btnWhitelistToggle);
			this.darkGroupBox2.Controls.Add(this.btnWhitelistOn);
			this.darkGroupBox2.Controls.Add(this.btnWhitelistOff);
			this.darkGroupBox2.Location = new System.Drawing.Point(287, 157);
			this.darkGroupBox2.Name = "darkGroupBox2";
			this.darkGroupBox2.Size = new System.Drawing.Size(148, 76);
			this.darkGroupBox2.TabIndex = 158;
			this.darkGroupBox2.TabStop = false;
			this.darkGroupBox2.Text = "Whitelist";
			// 
			// btnWhitelistToggle
			// 
			this.btnWhitelistToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnWhitelistToggle.Checked = false;
			this.btnWhitelistToggle.Location = new System.Drawing.Point(9, 20);
			this.btnWhitelistToggle.Name = "btnWhitelistToggle";
			this.btnWhitelistToggle.Size = new System.Drawing.Size(130, 22);
			this.btnWhitelistToggle.TabIndex = 154;
			this.btnWhitelistToggle.Text = "Toggle";
			this.btnWhitelistToggle.Click += new System.EventHandler(this.btnWhitelistToggle_Click);
			// 
			// btnWhitelistOn
			// 
			this.btnWhitelistOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnWhitelistOn.Checked = false;
			this.btnWhitelistOn.Location = new System.Drawing.Point(9, 45);
			this.btnWhitelistOn.Name = "btnWhitelistOn";
			this.btnWhitelistOn.Size = new System.Drawing.Size(64, 22);
			this.btnWhitelistOn.TabIndex = 155;
			this.btnWhitelistOn.Text = "On";
			this.btnWhitelistOn.Click += new System.EventHandler(this.btnWhitelistOn_Click);
			// 
			// btnWhitelistOff
			// 
			this.btnWhitelistOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnWhitelistOff.Checked = false;
			this.btnWhitelistOff.Location = new System.Drawing.Point(75, 45);
			this.btnWhitelistOff.Name = "btnWhitelistOff";
			this.btnWhitelistOff.Size = new System.Drawing.Size(64, 22);
			this.btnWhitelistOff.TabIndex = 156;
			this.btnWhitelistOff.Text = "Off";
			this.btnWhitelistOff.Click += new System.EventHandler(this.btnWhitelistOff_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label1.Location = new System.Drawing.Point(81, 266);
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
			this.numDropDelay.Location = new System.Drawing.Point(10, 263);
			this.numDropDelay.LoopValues = false;
			this.numDropDelay.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
			this.numDropDelay.Name = "numDropDelay";
			this.numDropDelay.Size = new System.Drawing.Size(65, 20);
			this.numDropDelay.TabIndex = 151;
			this.numDropDelay.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.numDropDelay.ValueChanged += new System.EventHandler(this.numDropDelay_ValueChanged);
			// 
			// btnBoost
			// 
			this.btnBoost.Checked = false;
			this.btnBoost.Location = new System.Drawing.Point(148, 129);
			this.btnBoost.Name = "btnBoost";
			this.btnBoost.Size = new System.Drawing.Size(130, 22);
			this.btnBoost.TabIndex = 37;
			this.btnBoost.Text = "Add boost";
			this.btnBoost.Click += new System.EventHandler(this.btnBoost_Click);
			// 
			// cbBoosts
			// 
			this.cbBoosts.FormattingEnabled = true;
			this.cbBoosts.Location = new System.Drawing.Point(149, 105);
			this.cbBoosts.Name = "cbBoosts";
			this.cbBoosts.Size = new System.Drawing.Size(129, 21);
			this.cbBoosts.TabIndex = 36;
			this.cbBoosts.Click += new System.EventHandler(this.cbBoosts_Click);
			// 
			// btnSwap
			// 
			this.btnSwap.Checked = false;
			this.btnSwap.Location = new System.Drawing.Point(6, 129);
			this.btnSwap.Name = "btnSwap";
			this.btnSwap.Size = new System.Drawing.Size(137, 22);
			this.btnSwap.TabIndex = 28;
			this.btnSwap.Text = "Bank swap";
			this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
			// 
			// txtSwapInv
			// 
			this.txtSwapInv.Location = new System.Drawing.Point(6, 105);
			this.txtSwapInv.Name = "txtSwapInv";
			this.txtSwapInv.Size = new System.Drawing.Size(137, 20);
			this.txtSwapInv.TabIndex = 27;
			this.txtSwapInv.Text = "Inventory item name";
			// 
			// txtSwapBank
			// 
			this.txtSwapBank.Location = new System.Drawing.Point(6, 80);
			this.txtSwapBank.Name = "txtSwapBank";
			this.txtSwapBank.Size = new System.Drawing.Size(137, 20);
			this.txtSwapBank.TabIndex = 26;
			this.txtSwapBank.Text = "Bank item name";
			// 
			// btnWhitelist
			// 
			this.btnWhitelist.Checked = false;
			this.btnWhitelist.Location = new System.Drawing.Point(148, 54);
			this.btnWhitelist.Name = "btnWhitelist";
			this.btnWhitelist.Size = new System.Drawing.Size(130, 22);
			this.btnWhitelist.TabIndex = 23;
			this.btnWhitelist.Text = "Add to whitelist";
			this.btnWhitelist.Click += new System.EventHandler(this.btnWhitelist_Click);
			// 
			// btnBoth
			// 
			this.btnBoth.Checked = false;
			this.btnBoth.Location = new System.Drawing.Point(148, 29);
			this.btnBoth.Name = "btnBoth";
			this.btnBoth.Size = new System.Drawing.Size(130, 22);
			this.btnBoth.TabIndex = 23;
			this.btnBoth.Text = "Add to both";
			this.btnBoth.Click += new System.EventHandler(this.btnBoth_Click);
			// 
			// txtWhitelist
			// 
			this.txtWhitelist.Location = new System.Drawing.Point(150, 4);
			this.txtWhitelist.Name = "txtWhitelist";
			this.txtWhitelist.Size = new System.Drawing.Size(128, 20);
			this.txtWhitelist.TabIndex = 22;
			this.txtWhitelist.Text = "Item name";
			this.txtWhitelist.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtWhitelist.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnItem
			// 
			this.btnItem.Checked = false;
			this.btnItem.Location = new System.Drawing.Point(6, 54);
			this.btnItem.Name = "btnItem";
			this.btnItem.Size = new System.Drawing.Size(137, 22);
			this.btnItem.TabIndex = 21;
			this.btnItem.Text = "Add command";
			this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
			// 
			// btnUnbanklst
			// 
			this.btnUnbanklst.Checked = false;
			this.btnUnbanklst.Location = new System.Drawing.Point(148, 79);
			this.btnUnbanklst.Name = "btnUnbanklst";
			this.btnUnbanklst.Size = new System.Drawing.Size(130, 22);
			this.btnUnbanklst.TabIndex = 147;
			this.btnUnbanklst.Text = "Add to Unbank list";
			this.btnUnbanklst.Click += new System.EventHandler(this.btnUnbanklst_Click);
			// 
			// txtItem
			// 
			this.txtItem.Location = new System.Drawing.Point(6, 29);
			this.txtItem.Name = "txtItem";
			this.txtItem.Size = new System.Drawing.Size(137, 20);
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
			this.cbItemCmds.Location = new System.Drawing.Point(6, 4);
			this.cbItemCmds.Name = "cbItemCmds";
			this.cbItemCmds.Size = new System.Drawing.Size(137, 21);
			this.cbItemCmds.TabIndex = 19;
			// 
			// tabQuest
			// 
			this.tabQuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabQuest.Controls.Add(this.chkQRequirements);
			this.tabQuest.Controls.Add(this.chkQRewards);
			this.tabQuest.Controls.Add(this.btnQAddToWhitelist);
			this.tabQuest.Controls.Add(this.numQQuestId);
			this.tabQuest.Controls.Add(this.darkLabel4);
			this.tabQuest.Controls.Add(this.chkReloginCompleteQuest);
			this.tabQuest.Controls.Add(this.chkInBlankCell);
			this.tabQuest.Controls.Add(this.label14);
			this.tabQuest.Controls.Add(this.numEnsureTries);
			this.tabQuest.Controls.Add(this.btnQuestAccept);
			this.tabQuest.Controls.Add(this.btnQuestComplete);
			this.tabQuest.Controls.Add(this.btnQuestAdd);
			this.tabQuest.Controls.Add(this.numQuestItem);
			this.tabQuest.Controls.Add(this.chkQuestItem);
			this.tabQuest.Controls.Add(this.numQuestID);
			this.tabQuest.Controls.Add(this.label4);
			this.tabQuest.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabQuest.Location = new System.Drawing.Point(4, 23);
			this.tabQuest.Margin = new System.Windows.Forms.Padding(0);
			this.tabQuest.Name = "tabQuest";
			this.tabQuest.Padding = new System.Windows.Forms.Padding(3);
			this.tabQuest.Size = new System.Drawing.Size(192, 73);
			this.tabQuest.TabIndex = 3;
			this.tabQuest.Text = "Quest";
			// 
			// chkQRequirements
			// 
			this.chkQRequirements.AutoSize = true;
			this.chkQRequirements.Location = new System.Drawing.Point(80, 207);
			this.chkQRequirements.Name = "chkQRequirements";
			this.chkQRequirements.Size = new System.Drawing.Size(85, 17);
			this.chkQRequirements.TabIndex = 23;
			this.chkQRequirements.Text = "Requirments";
			// 
			// chkQRewards
			// 
			this.chkQRewards.AutoSize = true;
			this.chkQRewards.Location = new System.Drawing.Point(6, 207);
			this.chkQRewards.Name = "chkQRewards";
			this.chkQRewards.Size = new System.Drawing.Size(68, 17);
			this.chkQRewards.TabIndex = 22;
			this.chkQRewards.Text = "Rewards";
			// 
			// btnQAddToWhitelist
			// 
			this.btnQAddToWhitelist.Checked = false;
			this.btnQAddToWhitelist.Location = new System.Drawing.Point(7, 232);
			this.btnQAddToWhitelist.Name = "btnQAddToWhitelist";
			this.btnQAddToWhitelist.Size = new System.Drawing.Size(129, 22);
			this.btnQAddToWhitelist.TabIndex = 21;
			this.btnQAddToWhitelist.Text = "Add item whitelist";
			this.btnQAddToWhitelist.Click += new System.EventHandler(this.btnQAddToWhitelist_Click);
			// 
			// numQQuestId
			// 
			this.numQQuestId.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numQQuestId.Location = new System.Drawing.Point(6, 181);
			this.numQQuestId.LoopValues = false;
			this.numQQuestId.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numQQuestId.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numQQuestId.Name = "numQQuestId";
			this.numQQuestId.Size = new System.Drawing.Size(129, 20);
			this.numQQuestId.TabIndex = 20;
			this.numQQuestId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// darkLabel4
			// 
			this.darkLabel4.AutoSize = true;
			this.darkLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel4.Location = new System.Drawing.Point(3, 165);
			this.darkLabel4.Name = "darkLabel4";
			this.darkLabel4.Size = new System.Drawing.Size(49, 13);
			this.darkLabel4.TabIndex = 19;
			this.darkLabel4.Text = "Quest ID";
			// 
			// chkReloginCompleteQuest
			// 
			this.chkReloginCompleteQuest.AutoSize = true;
			this.chkReloginCompleteQuest.Location = new System.Drawing.Point(232, 50);
			this.chkReloginCompleteQuest.Name = "chkReloginCompleteQuest";
			this.chkReloginCompleteQuest.Size = new System.Drawing.Size(93, 17);
			this.chkReloginCompleteQuest.TabIndex = 18;
			this.chkReloginCompleteQuest.Text = "relogin if failed";
			// 
			// chkInBlankCell
			// 
			this.chkInBlankCell.AutoSize = true;
			this.chkInBlankCell.Location = new System.Drawing.Point(143, 49);
			this.chkInBlankCell.Name = "chkInBlankCell";
			this.chkInBlankCell.Size = new System.Drawing.Size(83, 17);
			this.chkInBlankCell.TabIndex = 17;
			this.chkInBlankCell.Text = "in Blank cell";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label14.Location = new System.Drawing.Point(187, 78);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(26, 13);
			this.label14.TabIndex = 16;
			this.label14.Text = "tries";
			// 
			// numEnsureTries
			// 
			this.numEnsureTries.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numEnsureTries.Location = new System.Drawing.Point(143, 74);
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
            1,
            0,
            0,
            0});
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
			this.tabMisc.Controls.Add(this.darkGroupBox11);
			this.tabMisc.Controls.Add(this.btnAddLabel);
			this.tabMisc.Controls.Add(this.btnGotoLabel);
			this.tabMisc.Controls.Add(this.txtLabel);
			this.tabMisc.Controls.Add(this.darkGroupBox10);
			this.tabMisc.Controls.Add(this.numSpamTimes);
			this.tabMisc.Controls.Add(this.darkGroupBox14);
			this.tabMisc.Controls.Add(this.darkGroupBox12);
			this.tabMisc.Controls.Add(this.btnReturnCmd);
			this.tabMisc.Controls.Add(this.darkGroupBox8);
			this.tabMisc.Controls.Add(this.darkGroupBox7);
			this.tabMisc.Controls.Add(this.btnBlank);
			this.tabMisc.Controls.Add(this.btnLogout);
			this.tabMisc.Controls.Add(this.btnBeep);
			this.tabMisc.Controls.Add(this.numBeepTimes);
			this.tabMisc.Controls.Add(this.btnDelay);
			this.tabMisc.Controls.Add(this.numDelay);
			this.tabMisc.Controls.Add(this.btnStatementAdd);
			this.tabMisc.Controls.Add(this.txtStatement2);
			this.tabMisc.Controls.Add(this.txtStatement1);
			this.tabMisc.Controls.Add(this.cbStatement);
			this.tabMisc.Controls.Add(this.cbCategories);
			this.tabMisc.Controls.Add(this.txtPacket);
			this.tabMisc.Controls.Add(this.btnClientPacket);
			this.tabMisc.Controls.Add(this.btnPacket);
			this.tabMisc.Controls.Add(this.btnLoadCmd);
			this.tabMisc.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabMisc.Location = new System.Drawing.Point(4, 23);
			this.tabMisc.Margin = new System.Windows.Forms.Padding(0);
			this.tabMisc.Name = "tabMisc";
			this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
			this.tabMisc.Size = new System.Drawing.Size(531, 301);
			this.tabMisc.TabIndex = 4;
			this.tabMisc.Text = "Misc";
			// 
			// darkGroupBox11
			// 
			this.darkGroupBox11.Controls.Add(this.lbLabels);
			this.darkGroupBox11.Location = new System.Drawing.Point(318, 5);
			this.darkGroupBox11.Name = "darkGroupBox11";
			this.darkGroupBox11.Size = new System.Drawing.Size(207, 117);
			this.darkGroupBox11.TabIndex = 172;
			this.darkGroupBox11.TabStop = false;
			this.darkGroupBox11.Text = "Labels";
			// 
			// lbLabels
			// 
			this.lbLabels.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.lbLabels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbLabels.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbLabels.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lbLabels.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lbLabels.FormattingEnabled = true;
			this.lbLabels.ItemHeight = 18;
			this.lbLabels.Location = new System.Drawing.Point(3, 16);
			this.lbLabels.Name = "lbLabels";
			this.lbLabels.Size = new System.Drawing.Size(201, 98);
			this.lbLabels.TabIndex = 114;
			// 
			// btnAddLabel
			// 
			this.btnAddLabel.Checked = false;
			this.btnAddLabel.Location = new System.Drawing.Point(422, 150);
			this.btnAddLabel.Name = "btnAddLabel";
			this.btnAddLabel.Size = new System.Drawing.Size(100, 25);
			this.btnAddLabel.TabIndex = 171;
			this.btnAddLabel.Text = "Add";
			this.btnAddLabel.Click += new System.EventHandler(this.btnAddLabel_Click);
			// 
			// btnGotoLabel
			// 
			this.btnGotoLabel.Checked = false;
			this.btnGotoLabel.Location = new System.Drawing.Point(321, 150);
			this.btnGotoLabel.Name = "btnGotoLabel";
			this.btnGotoLabel.Size = new System.Drawing.Size(104, 25);
			this.btnGotoLabel.TabIndex = 170;
			this.btnGotoLabel.Text = "Goto";
			this.btnGotoLabel.Click += new System.EventHandler(this.btnGotoLabel_Click);
			// 
			// txtLabel
			// 
			this.txtLabel.Location = new System.Drawing.Point(321, 129);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(201, 20);
			this.txtLabel.TabIndex = 169;
			this.txtLabel.Text = "Label name";
			// 
			// darkGroupBox10
			// 
			this.darkGroupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.darkGroupBox10.Controls.Add(this.btnStop);
			this.darkGroupBox10.Controls.Add(this.btnRestart);
			this.darkGroupBox10.Location = new System.Drawing.Point(178, 243);
			this.darkGroupBox10.Name = "darkGroupBox10";
			this.darkGroupBox10.Size = new System.Drawing.Size(140, 50);
			this.darkGroupBox10.TabIndex = 168;
			this.darkGroupBox10.TabStop = false;
			this.darkGroupBox10.Text = "Bot Control";
			// 
			// btnStop
			// 
			this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnStop.Checked = false;
			this.btnStop.Location = new System.Drawing.Point(72, 19);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(60, 22);
			this.btnStop.TabIndex = 65;
			this.btnStop.Text = "Stop bot";
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnRestart
			// 
			this.btnRestart.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnRestart.Checked = false;
			this.btnRestart.Location = new System.Drawing.Point(7, 19);
			this.btnRestart.Name = "btnRestart";
			this.btnRestart.Size = new System.Drawing.Size(59, 22);
			this.btnRestart.TabIndex = 66;
			this.btnRestart.Text = "Restart";
			this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
			// 
			// numSpamTimes
			// 
			this.numSpamTimes.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSpamTimes.Location = new System.Drawing.Point(259, 5);
			this.numSpamTimes.LoopValues = false;
			this.numSpamTimes.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.numSpamTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numSpamTimes.Name = "numSpamTimes";
			this.numSpamTimes.Size = new System.Drawing.Size(54, 20);
			this.numSpamTimes.TabIndex = 167;
			this.numSpamTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// darkGroupBox14
			// 
			this.darkGroupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.darkGroupBox14.Controls.Add(this.txtPlayer);
			this.darkGroupBox14.Controls.Add(this.btnGoto);
			this.darkGroupBox14.Location = new System.Drawing.Point(7, 254);
			this.darkGroupBox14.Name = "darkGroupBox14";
			this.darkGroupBox14.Size = new System.Drawing.Size(166, 45);
			this.darkGroupBox14.TabIndex = 166;
			this.darkGroupBox14.TabStop = false;
			this.darkGroupBox14.Text = "Player";
			// 
			// txtPlayer
			// 
			this.txtPlayer.Location = new System.Drawing.Point(6, 19);
			this.txtPlayer.Name = "txtPlayer";
			this.txtPlayer.Size = new System.Drawing.Size(84, 20);
			this.txtPlayer.TabIndex = 69;
			this.txtPlayer.Text = "Player name";
			this.txtPlayer.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtPlayer.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnGoto
			// 
			this.btnGoto.Checked = false;
			this.btnGoto.Location = new System.Drawing.Point(94, 18);
			this.btnGoto.Name = "btnGoto";
			this.btnGoto.Size = new System.Drawing.Size(66, 22);
			this.btnGoto.TabIndex = 68;
			this.btnGoto.Text = "Goto";
			this.btnGoto.Click += new System.EventHandler(this.btnGoto_Click);
			// 
			// darkGroupBox12
			// 
			this.darkGroupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.darkGroupBox12.Controls.Add(this.txtSetInt);
			this.darkGroupBox12.Controls.Add(this.btnSetInt);
			this.darkGroupBox12.Controls.Add(this.numSetInt);
			this.darkGroupBox12.Controls.Add(this.btnIncreaseInt);
			this.darkGroupBox12.Controls.Add(this.btnDecreaseInt);
			this.darkGroupBox12.Location = new System.Drawing.Point(7, 185);
			this.darkGroupBox12.Name = "darkGroupBox12";
			this.darkGroupBox12.Size = new System.Drawing.Size(165, 69);
			this.darkGroupBox12.TabIndex = 164;
			this.darkGroupBox12.TabStop = false;
			this.darkGroupBox12.Text = "Temp Integer";
			// 
			// txtSetInt
			// 
			this.txtSetInt.Location = new System.Drawing.Point(6, 16);
			this.txtSetInt.Name = "txtSetInt";
			this.txtSetInt.Size = new System.Drawing.Size(120, 20);
			this.txtSetInt.TabIndex = 156;
			// 
			// btnSetInt
			// 
			this.btnSetInt.Checked = false;
			this.btnSetInt.Location = new System.Drawing.Point(6, 38);
			this.btnSetInt.Name = "btnSetInt";
			this.btnSetInt.Size = new System.Drawing.Size(52, 23);
			this.btnSetInt.TabIndex = 155;
			this.btnSetInt.Text = "Set";
			this.btnSetInt.Click += new System.EventHandler(this.btnSetInt_Click);
			// 
			// numSetInt
			// 
			this.numSetInt.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSetInt.Location = new System.Drawing.Point(129, 16);
			this.numSetInt.LoopValues = false;
			this.numSetInt.Name = "numSetInt";
			this.numSetInt.Size = new System.Drawing.Size(30, 20);
			this.numSetInt.TabIndex = 157;
			// 
			// btnIncreaseInt
			// 
			this.btnIncreaseInt.Checked = false;
			this.btnIncreaseInt.Location = new System.Drawing.Point(61, 38);
			this.btnIncreaseInt.Name = "btnIncreaseInt";
			this.btnIncreaseInt.Size = new System.Drawing.Size(48, 23);
			this.btnIncreaseInt.TabIndex = 158;
			this.btnIncreaseInt.Text = "++";
			this.btnIncreaseInt.Click += new System.EventHandler(this.btnIncreaseInt_Click);
			// 
			// btnDecreaseInt
			// 
			this.btnDecreaseInt.Checked = false;
			this.btnDecreaseInt.Location = new System.Drawing.Point(111, 38);
			this.btnDecreaseInt.Name = "btnDecreaseInt";
			this.btnDecreaseInt.Size = new System.Drawing.Size(48, 23);
			this.btnDecreaseInt.TabIndex = 158;
			this.btnDecreaseInt.Text = "--";
			this.btnDecreaseInt.Click += new System.EventHandler(this.btnDecreaseInt_Click);
			// 
			// btnReturnCmd
			// 
			this.btnReturnCmd.Checked = false;
			this.btnReturnCmd.Location = new System.Drawing.Point(178, 104);
			this.btnReturnCmd.Name = "btnReturnCmd";
			this.btnReturnCmd.Size = new System.Drawing.Size(135, 22);
			this.btnReturnCmd.TabIndex = 160;
			this.btnReturnCmd.Text = "Return (command)";
			this.btnReturnCmd.Click += new System.EventHandler(this.btnReturnCmd_Click);
			// 
			// darkGroupBox8
			// 
			this.darkGroupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.darkGroupBox8.Controls.Add(this.numIndexCmd);
			this.darkGroupBox8.Controls.Add(this.btnGotoIndex);
			this.darkGroupBox8.Controls.Add(this.btnClearTempVar);
			this.darkGroupBox8.Controls.Add(this.btnGoUpIndex);
			this.darkGroupBox8.Controls.Add(this.btnGoDownIndex);
			this.darkGroupBox8.Location = new System.Drawing.Point(324, 185);
			this.darkGroupBox8.Name = "darkGroupBox8";
			this.darkGroupBox8.Size = new System.Drawing.Size(161, 109);
			this.darkGroupBox8.TabIndex = 162;
			this.darkGroupBox8.TabStop = false;
			this.darkGroupBox8.Text = "Index";
			// 
			// numIndexCmd
			// 
			this.numIndexCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.numIndexCmd.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numIndexCmd.Location = new System.Drawing.Point(6, 19);
			this.numIndexCmd.LoopValues = false;
			this.numIndexCmd.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numIndexCmd.Name = "numIndexCmd";
			this.numIndexCmd.Size = new System.Drawing.Size(47, 20);
			this.numIndexCmd.TabIndex = 152;
			// 
			// btnGotoIndex
			// 
			this.btnGotoIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGotoIndex.Checked = false;
			this.btnGotoIndex.Location = new System.Drawing.Point(61, 19);
			this.btnGotoIndex.Name = "btnGotoIndex";
			this.btnGotoIndex.Size = new System.Drawing.Size(93, 23);
			this.btnGotoIndex.TabIndex = 153;
			this.btnGotoIndex.Text = "Goto Index";
			this.btnGotoIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
			// 
			// btnClearTempVar
			// 
			this.btnClearTempVar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClearTempVar.Checked = false;
			this.btnClearTempVar.Location = new System.Drawing.Point(5, 75);
			this.btnClearTempVar.Name = "btnClearTempVar";
			this.btnClearTempVar.Size = new System.Drawing.Size(150, 22);
			this.btnClearTempVar.TabIndex = 160;
			this.btnClearTempVar.Text = "Clear TempVar cmd";
			this.btnClearTempVar.Click += new System.EventHandler(this.btnClearTempVar_Click);
			// 
			// btnGoUpIndex
			// 
			this.btnGoUpIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGoUpIndex.Checked = false;
			this.btnGoUpIndex.Location = new System.Drawing.Point(5, 46);
			this.btnGoUpIndex.Name = "btnGoUpIndex";
			this.btnGoUpIndex.Size = new System.Drawing.Size(73, 23);
			this.btnGoUpIndex.TabIndex = 153;
			this.btnGoUpIndex.Text = "Up++";
			this.btnGoUpIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
			// 
			// btnGoDownIndex
			// 
			this.btnGoDownIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGoDownIndex.Checked = false;
			this.btnGoDownIndex.Location = new System.Drawing.Point(81, 46);
			this.btnGoDownIndex.Name = "btnGoDownIndex";
			this.btnGoDownIndex.Size = new System.Drawing.Size(73, 23);
			this.btnGoDownIndex.TabIndex = 153;
			this.btnGoDownIndex.Text = "Down--";
			this.btnGoDownIndex.Click += new System.EventHandler(this.btnGotoIndex_Click);
			// 
			// darkGroupBox7
			// 
			this.darkGroupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.darkGroupBox7.Controls.Add(this.btnProvokeOff);
			this.darkGroupBox7.Controls.Add(this.btnProvokeOn);
			this.darkGroupBox7.Location = new System.Drawing.Point(178, 185);
			this.darkGroupBox7.Name = "darkGroupBox7";
			this.darkGroupBox7.Size = new System.Drawing.Size(140, 49);
			this.darkGroupBox7.TabIndex = 161;
			this.darkGroupBox7.TabStop = false;
			this.darkGroupBox7.Text = "Provoke";
			// 
			// btnProvokeOff
			// 
			this.btnProvokeOff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnProvokeOff.Checked = false;
			this.btnProvokeOff.Location = new System.Drawing.Point(70, 19);
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
			this.btnProvokeOn.Location = new System.Drawing.Point(7, 19);
			this.btnProvokeOn.Name = "btnProvokeOn";
			this.btnProvokeOn.Size = new System.Drawing.Size(59, 22);
			this.btnProvokeOn.TabIndex = 149;
			this.btnProvokeOn.Text = "On";
			this.btnProvokeOn.Click += new System.EventHandler(this.btnProvokeOn_Click);
			// 
			// btnBlank
			// 
			this.btnBlank.Checked = false;
			this.btnBlank.Location = new System.Drawing.Point(5, 154);
			this.btnBlank.Name = "btnBlank";
			this.btnBlank.Size = new System.Drawing.Size(167, 22);
			this.btnBlank.TabIndex = 151;
			this.btnBlank.Text = "Blank Command";
			this.btnBlank.Click += new System.EventHandler(this.btnBlank_Click);
			// 
			// btnLogout
			// 
			this.btnLogout.Checked = false;
			this.btnLogout.Location = new System.Drawing.Point(178, 129);
			this.btnLogout.Name = "btnLogout";
			this.btnLogout.Size = new System.Drawing.Size(135, 22);
			this.btnLogout.TabIndex = 114;
			this.btnLogout.Text = "Logout";
			this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
			// 
			// btnBeep
			// 
			this.btnBeep.Checked = false;
			this.btnBeep.Location = new System.Drawing.Point(178, 154);
			this.btnBeep.Name = "btnBeep";
			this.btnBeep.Size = new System.Drawing.Size(75, 22);
			this.btnBeep.TabIndex = 74;
			this.btnBeep.Text = "Play Sound";
			this.btnBeep.Click += new System.EventHandler(this.btnBeep_Click);
			// 
			// numBeepTimes
			// 
			this.numBeepTimes.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numBeepTimes.Location = new System.Drawing.Point(258, 155);
			this.numBeepTimes.LoopValues = false;
			this.numBeepTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numBeepTimes.Name = "numBeepTimes";
			this.numBeepTimes.Size = new System.Drawing.Size(55, 20);
			this.numBeepTimes.TabIndex = 73;
			this.numBeepTimes.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// btnDelay
			// 
			this.btnDelay.Checked = false;
			this.btnDelay.Location = new System.Drawing.Point(178, 53);
			this.btnDelay.Name = "btnDelay";
			this.btnDelay.Size = new System.Drawing.Size(75, 22);
			this.btnDelay.TabIndex = 74;
			this.btnDelay.Text = "Delay";
			this.btnDelay.Click += new System.EventHandler(this.btnDelay_Click);
			// 
			// numDelay
			// 
			this.numDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numDelay.Location = new System.Drawing.Point(259, 53);
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
			this.numDelay.Size = new System.Drawing.Size(54, 20);
			this.numDelay.TabIndex = 73;
			this.numDelay.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
			// 
			// btnStatementAdd
			// 
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
			this.txtStatement2.Location = new System.Drawing.Point(5, 104);
			this.txtStatement2.MaximumSize = new System.Drawing.Size(197, 20);
			this.txtStatement2.MinimumSize = new System.Drawing.Size(167, 20);
			this.txtStatement2.Name = "txtStatement2";
			this.txtStatement2.Size = new System.Drawing.Size(167, 20);
			this.txtStatement2.TabIndex = 60;
			// 
			// txtStatement1
			// 
			this.txtStatement1.Location = new System.Drawing.Point(5, 80);
			this.txtStatement1.MaximumSize = new System.Drawing.Size(197, 20);
			this.txtStatement1.MinimumSize = new System.Drawing.Size(167, 20);
			this.txtStatement1.Name = "txtStatement1";
			this.txtStatement1.Size = new System.Drawing.Size(167, 20);
			this.txtStatement1.TabIndex = 59;
			// 
			// cbStatement
			// 
			this.cbStatement.DisplayMember = "Text";
			this.cbStatement.FormattingEnabled = true;
			this.cbStatement.Location = new System.Drawing.Point(5, 55);
			this.cbStatement.MaxDropDownItems = 15;
			this.cbStatement.MaximumSize = new System.Drawing.Size(197, 0);
			this.cbStatement.MinimumSize = new System.Drawing.Size(167, 0);
			this.cbStatement.Name = "cbStatement";
			this.cbStatement.Size = new System.Drawing.Size(167, 21);
			this.cbStatement.TabIndex = 58;
			this.cbStatement.SelectedIndexChanged += new System.EventHandler(this.cbStatement_SelectedIndexChanged);
			// 
			// cbCategories
			// 
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
			this.txtPacket.Location = new System.Drawing.Point(5, 5);
			this.txtPacket.Name = "txtPacket";
			this.txtPacket.Size = new System.Drawing.Size(248, 20);
			this.txtPacket.TabIndex = 53;
			this.txtPacket.Text = "%xt%zm%.........";
			this.txtPacket.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtPacket.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// btnClientPacket
			// 
			this.btnClientPacket.Checked = false;
			this.btnClientPacket.Location = new System.Drawing.Point(231, 28);
			this.btnClientPacket.Name = "btnClientPacket";
			this.btnClientPacket.Size = new System.Drawing.Size(82, 22);
			this.btnClientPacket.TabIndex = 52;
			this.btnClientPacket.Text = "Client Packet";
			this.btnClientPacket.Click += new System.EventHandler(this.btnClientPacket_Click);
			// 
			// btnPacket
			// 
			this.btnPacket.Checked = false;
			this.btnPacket.Location = new System.Drawing.Point(178, 28);
			this.btnPacket.Name = "btnPacket";
			this.btnPacket.Size = new System.Drawing.Size(52, 22);
			this.btnPacket.TabIndex = 52;
			this.btnPacket.Text = "Packet";
			this.btnPacket.Click += new System.EventHandler(this.btnPacket_Click);
			// 
			// btnLoadCmd
			// 
			this.btnLoadCmd.Checked = false;
			this.btnLoadCmd.Location = new System.Drawing.Point(178, 78);
			this.btnLoadCmd.Name = "btnLoadCmd";
			this.btnLoadCmd.Size = new System.Drawing.Size(135, 22);
			this.btnLoadCmd.TabIndex = 63;
			this.btnLoadCmd.Text = "Load bot (command)";
			this.btnLoadCmd.Click += new System.EventHandler(this.btnLoadCmd_Click);
			// 
			// tabMisc2
			// 
			this.tabMisc2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabMisc2.Controls.Add(this.darkGroupBox15);
			this.tabMisc2.Controls.Add(this.darkGroupBox13);
			this.tabMisc2.Controls.Add(this.darkGroupBox9);
			this.tabMisc2.Controls.Add(this.chkMerge);
			this.tabMisc2.Controls.Add(this.darkGroupBox1);
			this.tabMisc2.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabMisc2.Location = new System.Drawing.Point(4, 23);
			this.tabMisc2.Margin = new System.Windows.Forms.Padding(0);
			this.tabMisc2.Name = "tabMisc2";
			this.tabMisc2.Padding = new System.Windows.Forms.Padding(3);
			this.tabMisc2.Size = new System.Drawing.Size(531, 301);
			this.tabMisc2.TabIndex = 8;
			this.tabMisc2.Text = "Misc 2";
			// 
			// darkGroupBox15
			// 
			this.darkGroupBox15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.darkGroupBox15.Controls.Add(this.btnFollowCmd);
			this.darkGroupBox15.Controls.Add(this.tbFollowPlayer);
			this.darkGroupBox15.Location = new System.Drawing.Point(246, 218);
			this.darkGroupBox15.Name = "darkGroupBox15";
			this.darkGroupBox15.Size = new System.Drawing.Size(109, 77);
			this.darkGroupBox15.TabIndex = 170;
			this.darkGroupBox15.TabStop = false;
			this.darkGroupBox15.Text = "Follow and Kills";
			// 
			// btnFollowCmd
			// 
			this.btnFollowCmd.Checked = false;
			this.btnFollowCmd.Location = new System.Drawing.Point(6, 44);
			this.btnFollowCmd.Name = "btnFollowCmd";
			this.btnFollowCmd.Size = new System.Drawing.Size(93, 23);
			this.btnFollowCmd.TabIndex = 151;
			this.btnFollowCmd.Text = "Follow (cmd)";
			this.btnFollowCmd.Click += new System.EventHandler(this.btnFollowCmd_Click);
			// 
			// tbFollowPlayer
			// 
			this.tbFollowPlayer.Location = new System.Drawing.Point(6, 18);
			this.tbFollowPlayer.Name = "tbFollowPlayer";
			this.tbFollowPlayer.Size = new System.Drawing.Size(93, 20);
			this.tbFollowPlayer.TabIndex = 147;
			this.tbFollowPlayer.Text = "Player";
			this.tbFollowPlayer.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbFollowPlayer.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// darkGroupBox13
			// 
			this.darkGroupBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.darkGroupBox13.Controls.Add(this.label3);
			this.darkGroupBox13.Controls.Add(this.chkSkip);
			this.darkGroupBox13.Controls.Add(this.btnBotDelay);
			this.darkGroupBox13.Controls.Add(this.numBotDelay);
			this.darkGroupBox13.Controls.Add(this.chkRestartDeath);
			this.darkGroupBox13.Location = new System.Drawing.Point(9, 202);
			this.darkGroupBox13.Name = "darkGroupBox13";
			this.darkGroupBox13.Size = new System.Drawing.Size(231, 93);
			this.darkGroupBox13.TabIndex = 161;
			this.darkGroupBox13.TabStop = false;
			this.darkGroupBox13.Text = "Bot Delay";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label3.Location = new System.Drawing.Point(3, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 26);
			this.label3.TabIndex = 72;
			this.label3.Text = "Overall \r\nDelay";
			// 
			// chkSkip
			// 
			this.chkSkip.AutoSize = true;
			this.chkSkip.Checked = true;
			this.chkSkip.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSkip.Location = new System.Drawing.Point(7, 43);
			this.chkSkip.Name = "chkSkip";
			this.chkSkip.Size = new System.Drawing.Size(146, 17);
			this.chkSkip.TabIndex = 62;
			this.chkSkip.Text = "Skip bot delay for index/if";
			// 
			// btnBotDelay
			// 
			this.btnBotDelay.Checked = false;
			this.btnBotDelay.Location = new System.Drawing.Point(99, 19);
			this.btnBotDelay.Name = "btnBotDelay";
			this.btnBotDelay.Size = new System.Drawing.Size(61, 23);
			this.btnBotDelay.TabIndex = 70;
			this.btnBotDelay.Text = "Set delay";
			this.btnBotDelay.Click += new System.EventHandler(this.btnBotDelay_Click);
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
			this.numBotDelay.Location = new System.Drawing.Point(49, 20);
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
			// chkRestartDeath
			// 
			this.chkRestartDeath.AutoSize = true;
			this.chkRestartDeath.Checked = true;
			this.chkRestartDeath.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkRestartDeath.Location = new System.Drawing.Point(7, 62);
			this.chkRestartDeath.Name = "chkRestartDeath";
			this.chkRestartDeath.Size = new System.Drawing.Size(133, 17);
			this.chkRestartDeath.TabIndex = 116;
			this.chkRestartDeath.Text = "Restart bot upon dying";
			// 
			// darkGroupBox9
			// 
			this.darkGroupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.darkGroupBox9.Controls.Add(this.txtAuthor);
			this.darkGroupBox9.Controls.Add(this.splitContainer3);
			this.darkGroupBox9.Controls.Add(this.txtDescription);
			this.darkGroupBox9.Location = new System.Drawing.Point(246, 6);
			this.darkGroupBox9.Name = "darkGroupBox9";
			this.darkGroupBox9.Size = new System.Drawing.Size(279, 195);
			this.darkGroupBox9.TabIndex = 116;
			this.darkGroupBox9.TabStop = false;
			this.darkGroupBox9.Text = "Save/Load";
			// 
			// txtAuthor
			// 
			this.txtAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAuthor.Location = new System.Drawing.Point(7, 45);
			this.txtAuthor.Multiline = true;
			this.txtAuthor.Name = "txtAuthor";
			this.txtAuthor.Size = new System.Drawing.Size(266, 20);
			this.txtAuthor.TabIndex = 119;
			this.txtAuthor.Text = "Author";
			// 
			// splitContainer3
			// 
			this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer3.IsSplitterFixed = true;
			this.splitContainer3.Location = new System.Drawing.Point(7, 19);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.btnSave);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.btnLoad);
			this.splitContainer3.Size = new System.Drawing.Size(266, 22);
			this.splitContainer3.SplitterDistance = 123;
			this.splitContainer3.TabIndex = 118;
			// 
			// btnSave
			// 
			this.btnSave.Checked = false;
			this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnSave.Location = new System.Drawing.Point(0, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(123, 22);
			this.btnSave.TabIndex = 75;
			this.btnSave.Text = "Save bot";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Checked = false;
			this.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLoad.Location = new System.Drawing.Point(0, 0);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(139, 22);
			this.btnLoad.TabIndex = 67;
			this.btnLoad.Text = "Load bot";
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.Location = new System.Drawing.Point(7, 68);
			this.txtDescription.MaxLength = 2147483647;
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(266, 121);
			this.txtDescription.TabIndex = 109;
			this.txtDescription.Text = "Description (Write in RTF)";
			this.txtDescription.Enter += new System.EventHandler(this.TextboxEnter);
			this.txtDescription.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// chkMerge
			// 
			this.chkMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkMerge.AutoSize = true;
			this.chkMerge.Location = new System.Drawing.Point(722, 56);
			this.chkMerge.Name = "chkMerge";
			this.chkMerge.Size = new System.Drawing.Size(56, 17);
			this.chkMerge.TabIndex = 115;
			this.chkMerge.Text = "Merge";
			// 
			// darkGroupBox1
			// 
			this.darkGroupBox1.Controls.Add(this.btnBSStop);
			this.darkGroupBox1.Controls.Add(this.label8);
			this.darkGroupBox1.Controls.Add(this.tbBSLabel);
			this.darkGroupBox1.Controls.Add(this.numBSDelay);
			this.darkGroupBox1.Controls.Add(this.tbBSPacket);
			this.darkGroupBox1.Controls.Add(this.btnBSStart);
			this.darkGroupBox1.Location = new System.Drawing.Point(6, 6);
			this.darkGroupBox1.Name = "darkGroupBox1";
			this.darkGroupBox1.Size = new System.Drawing.Size(234, 125);
			this.darkGroupBox1.TabIndex = 59;
			this.darkGroupBox1.TabStop = false;
			this.darkGroupBox1.Text = "Background Spammer";
			// 
			// btnBSStop
			// 
			this.btnBSStop.Checked = false;
			this.btnBSStop.Enabled = false;
			this.btnBSStop.Location = new System.Drawing.Point(86, 95);
			this.btnBSStop.Name = "btnBSStop";
			this.btnBSStop.Size = new System.Drawing.Size(70, 23);
			this.btnBSStop.TabIndex = 166;
			this.btnBSStop.Text = "Stop";
			this.btnBSStop.Click += new System.EventHandler(this.btnBSStop_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(66, 78);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(20, 13);
			this.label8.TabIndex = 165;
			this.label8.Text = "ms";
			// 
			// tbBSLabel
			// 
			this.tbBSLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbBSLabel.Location = new System.Drawing.Point(6, 21);
			this.tbBSLabel.Name = "tbBSLabel";
			this.tbBSLabel.Size = new System.Drawing.Size(222, 20);
			this.tbBSLabel.TabIndex = 164;
			this.tbBSLabel.Text = "Packet Label";
			// 
			// numBSDelay
			// 
			this.numBSDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numBSDelay.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numBSDelay.Location = new System.Drawing.Point(6, 71);
			this.numBSDelay.LoopValues = false;
			this.numBSDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numBSDelay.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numBSDelay.Name = "numBSDelay";
			this.numBSDelay.Size = new System.Drawing.Size(58, 20);
			this.numBSDelay.TabIndex = 164;
			this.numBSDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// tbBSPacket
			// 
			this.tbBSPacket.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbBSPacket.Location = new System.Drawing.Point(6, 45);
			this.tbBSPacket.Name = "tbBSPacket";
			this.tbBSPacket.Size = new System.Drawing.Size(222, 20);
			this.tbBSPacket.TabIndex = 163;
			this.tbBSPacket.Text = "%xt%zm%.........";
			// 
			// btnBSStart
			// 
			this.btnBSStart.Checked = false;
			this.btnBSStart.Enabled = false;
			this.btnBSStart.Location = new System.Drawing.Point(6, 95);
			this.btnBSStart.Name = "btnBSStart";
			this.btnBSStart.Size = new System.Drawing.Size(70, 23);
			this.btnBSStart.TabIndex = 162;
			this.btnBSStart.Text = "Start";
			this.btnBSStart.Click += new System.EventHandler(this.btnBSStart_Click);
			// 
			// tabOptions
			// 
			this.tabOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabOptions.Controls.Add(this.chkFollowOnly);
			this.tabOptions.Controls.Add(this.tbFollowPlayer2);
			this.tabOptions.Controls.Add(this.chkWalkSpeed);
			this.tabOptions.Controls.Add(this.darkGroupBox6);
			this.tabOptions.Controls.Add(this.label6);
			this.tabOptions.Controls.Add(this.numOptionsTimer);
			this.tabOptions.Controls.Add(this.chkEnableSettings);
			this.tabOptions.Controls.Add(this.chkDisableAnims);
			this.tabOptions.Controls.Add(this.txtSoundItem);
			this.tabOptions.Controls.Add(this.btnSoundAdd);
			this.tabOptions.Controls.Add(this.btnSoundDelete);
			this.tabOptions.Controls.Add(this.btnSoundTest);
			this.tabOptions.Controls.Add(this.lstSoundItems);
			this.tabOptions.Controls.Add(this.label9);
			this.tabOptions.Controls.Add(this.numWalkSpeed);
			this.tabOptions.Controls.Add(this.chkSkipCutscenes);
			this.tabOptions.Controls.Add(this.chkHidePlayers);
			this.tabOptions.Controls.Add(this.chkLag);
			this.tabOptions.Controls.Add(this.chkMagnet);
			this.tabOptions.Controls.Add(this.chkProvoke);
			this.tabOptions.Controls.Add(this.chkInfiniteRange);
			this.tabOptions.Controls.Add(this.grpLogin);
			this.tabOptions.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabOptions.Location = new System.Drawing.Point(4, 23);
			this.tabOptions.Margin = new System.Windows.Forms.Padding(0);
			this.tabOptions.Name = "tabOptions";
			this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
			this.tabOptions.Size = new System.Drawing.Size(531, 301);
			this.tabOptions.TabIndex = 5;
			this.tabOptions.Text = "Options";
			// 
			// chkFollowOnly
			// 
			this.chkFollowOnly.AutoSize = true;
			this.chkFollowOnly.Location = new System.Drawing.Point(150, 174);
			this.chkFollowOnly.Name = "chkFollowOnly";
			this.chkFollowOnly.Size = new System.Drawing.Size(56, 17);
			this.chkFollowOnly.TabIndex = 162;
			this.chkFollowOnly.Text = "Follow";
			// 
			// tbFollowPlayer2
			// 
			this.tbFollowPlayer2.Location = new System.Drawing.Point(207, 173);
			this.tbFollowPlayer2.Name = "tbFollowPlayer2";
			this.tbFollowPlayer2.Size = new System.Drawing.Size(83, 20);
			this.tbFollowPlayer2.TabIndex = 161;
			this.tbFollowPlayer2.Text = "Player";
			this.tbFollowPlayer2.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbFollowPlayer2.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// chkWalkSpeed
			// 
			this.chkWalkSpeed.AutoSize = true;
			this.chkWalkSpeed.Location = new System.Drawing.Point(150, 150);
			this.chkWalkSpeed.Name = "chkWalkSpeed";
			this.chkWalkSpeed.Size = new System.Drawing.Size(83, 17);
			this.chkWalkSpeed.TabIndex = 160;
			this.chkWalkSpeed.Text = "Walk speed";
			this.chkWalkSpeed.CheckedChanged += new System.EventHandler(this.chkWalkSpeed_CheckedChanged);
			// 
			// darkGroupBox6
			// 
			this.darkGroupBox6.Controls.Add(this.splitContainer5);
			this.darkGroupBox6.Controls.Add(this.lstLogText);
			this.darkGroupBox6.Controls.Add(this.txtLog);
			this.darkGroupBox6.Controls.Add(this.label5);
			this.darkGroupBox6.Location = new System.Drawing.Point(310, 5);
			this.darkGroupBox6.Name = "darkGroupBox6";
			this.darkGroupBox6.Size = new System.Drawing.Size(215, 287);
			this.darkGroupBox6.TabIndex = 159;
			this.darkGroupBox6.TabStop = false;
			this.darkGroupBox6.Text = "Logs";
			// 
			// splitContainer5
			// 
			this.splitContainer5.Location = new System.Drawing.Point(6, 96);
			this.splitContainer5.Name = "splitContainer5";
			// 
			// splitContainer5.Panel1
			// 
			this.splitContainer5.Panel1.Controls.Add(this.btnLog);
			// 
			// splitContainer5.Panel2
			// 
			this.splitContainer5.Panel2.Controls.Add(this.btnLogDebug);
			this.splitContainer5.Size = new System.Drawing.Size(202, 23);
			this.splitContainer5.SplitterDistance = 100;
			this.splitContainer5.SplitterWidth = 2;
			this.splitContainer5.TabIndex = 156;
			// 
			// btnLog
			// 
			this.btnLog.Checked = false;
			this.btnLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLog.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnLog.Location = new System.Drawing.Point(0, 0);
			this.btnLog.Name = "btnLog";
			this.btnLog.Size = new System.Drawing.Size(100, 23);
			this.btnLog.TabIndex = 148;
			this.btnLog.Text = "Log Script";
			this.btnLog.Click += new System.EventHandler(this.logScript);
			// 
			// btnLogDebug
			// 
			this.btnLogDebug.Checked = false;
			this.btnLogDebug.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLogDebug.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnLogDebug.Location = new System.Drawing.Point(0, 0);
			this.btnLogDebug.Name = "btnLogDebug";
			this.btnLogDebug.Size = new System.Drawing.Size(100, 23);
			this.btnLogDebug.TabIndex = 152;
			this.btnLogDebug.Text = "Log Debug";
			this.btnLogDebug.Click += new System.EventHandler(this.logDebug);
			// 
			// lstLogText
			// 
			this.lstLogText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstLogText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.lstLogText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            "{INT VALUE: int}",
            "{ROOM_ID}"});
			this.lstLogText.Location = new System.Drawing.Point(6, 137);
			this.lstLogText.Name = "lstLogText";
			this.lstLogText.Size = new System.Drawing.Size(202, 145);
			this.lstLogText.TabIndex = 153;
			this.lstLogText.DoubleClick += new System.EventHandler(this.lstLogText_DoubleClick);
			this.lstLogText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstLogText_KeyDown);
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
			this.txtLog.Location = new System.Drawing.Point(6, 15);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.Size = new System.Drawing.Size(202, 77);
			this.txtLog.TabIndex = 147;
			this.txtLog.Text = "Text";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label5.Location = new System.Drawing.Point(21, 124);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(115, 13);
			this.label5.TabIndex = 155;
			this.label5.Text = "Viable Log References";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label6.Location = new System.Drawing.Point(196, 276);
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
			this.numOptionsTimer.Location = new System.Drawing.Point(149, 272);
			this.numOptionsTimer.LoopValues = false;
			this.numOptionsTimer.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numOptionsTimer.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.numOptionsTimer.Name = "numOptionsTimer";
			this.numOptionsTimer.Size = new System.Drawing.Size(46, 20);
			this.numOptionsTimer.TabIndex = 156;
			this.numOptionsTimer.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numOptionsTimer.ValueChanged += new System.EventHandler(this.numOptionsTimer_ValueChanged);
			// 
			// chkEnableSettings
			// 
			this.chkEnableSettings.AutoSize = true;
			this.chkEnableSettings.Location = new System.Drawing.Point(150, 242);
			this.chkEnableSettings.Name = "chkEnableSettings";
			this.chkEnableSettings.Size = new System.Drawing.Size(97, 30);
			this.chkEnableSettings.TabIndex = 132;
			this.chkEnableSettings.Text = "Enable options\r\nwithout starting";
			this.chkEnableSettings.CheckedChanged += new System.EventHandler(this.chkEnableSettings_CheckedChanged);
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
			this.txtSoundItem.Location = new System.Drawing.Point(3, 249);
			this.txtSoundItem.Name = "txtSoundItem";
			this.txtSoundItem.Size = new System.Drawing.Size(139, 20);
			this.txtSoundItem.TabIndex = 130;
			// 
			// btnSoundAdd
			// 
			this.btnSoundAdd.Checked = false;
			this.btnSoundAdd.Location = new System.Drawing.Point(48, 271);
			this.btnSoundAdd.Name = "btnSoundAdd";
			this.btnSoundAdd.Size = new System.Drawing.Size(43, 22);
			this.btnSoundAdd.TabIndex = 129;
			this.btnSoundAdd.Text = "Add";
			this.btnSoundAdd.Click += new System.EventHandler(this.btnSoundAdd_Click);
			// 
			// btnSoundDelete
			// 
			this.btnSoundDelete.Checked = false;
			this.btnSoundDelete.Location = new System.Drawing.Point(92, 271);
			this.btnSoundDelete.Name = "btnSoundDelete";
			this.btnSoundDelete.Size = new System.Drawing.Size(50, 22);
			this.btnSoundDelete.TabIndex = 128;
			this.btnSoundDelete.Text = "Delete";
			this.btnSoundDelete.Click += new System.EventHandler(this.btnSoundDelete_Click);
			// 
			// btnSoundTest
			// 
			this.btnSoundTest.Checked = false;
			this.btnSoundTest.Location = new System.Drawing.Point(3, 271);
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
			this.lstSoundItems.Location = new System.Drawing.Point(3, 191);
			this.lstSoundItems.Name = "lstSoundItems";
			this.lstSoundItems.Size = new System.Drawing.Size(139, 54);
			this.lstSoundItems.TabIndex = 125;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.label9.Location = new System.Drawing.Point(6, 162);
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
			this.numWalkSpeed.Location = new System.Drawing.Point(233, 149);
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
			this.numWalkSpeed.Size = new System.Drawing.Size(39, 20);
			this.numWalkSpeed.TabIndex = 123;
			this.numWalkSpeed.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.numWalkSpeed.ValueChanged += new System.EventHandler(this.numWalkSpeed_ValueChanged);
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
			this.grpLogin.Size = new System.Drawing.Size(141, 141);
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
			this.cbServers.DisplayMember = "Name";
			this.cbServers.FormattingEnabled = true;
			this.cbServers.Location = new System.Drawing.Point(4, 17);
			this.cbServers.Name = "cbServers";
			this.cbServers.Size = new System.Drawing.Size(123, 21);
			this.cbServers.TabIndex = 0;
			this.cbServers.ValueMember = "Name";
			// 
			// chkRelogRetry
			// 
			this.chkRelogRetry.AutoSize = true;
			this.chkRelogRetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.chkRelogRetry.Location = new System.Drawing.Point(1, 120);
			this.chkRelogRetry.Name = "chkRelogRetry";
			this.chkRelogRetry.Size = new System.Drawing.Size(143, 17);
			this.chkRelogRetry.TabIndex = 88;
			this.chkRelogRetry.Text = "Relog again if in battleon";
			// 
			// chkRelog
			// 
			this.chkRelog.AutoSize = true;
			this.chkRelog.Location = new System.Drawing.Point(4, 42);
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
			// tabOptions2
			// 
			this.tabOptions2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabOptions2.Controls.Add(this.darkGroupBox17);
			this.tabOptions2.Controls.Add(this.colorfulCommands);
			this.tabOptions2.Controls.Add(this.darkGroupBox16);
			this.tabOptions2.Controls.Add(this.btnSetSpawn2);
			this.tabOptions2.Controls.Add(this.darkLabel6);
			this.tabOptions2.Controls.Add(this.numSaveProgress);
			this.tabOptions2.Controls.Add(this.chkSaveProgress);
			this.tabOptions2.Controls.Add(this.chkAntiCounter);
			this.tabOptions2.Controls.Add(this.numPrivateRoom);
			this.tabOptions2.Controls.Add(this.chkPrivateRoom);
			this.tabOptions2.Controls.Add(this.chkGender);
			this.tabOptions2.Controls.Add(this.numSetFPS);
			this.tabOptions2.Controls.Add(this.btnSetFPS);
			this.tabOptions2.Controls.Add(this.lblUP);
			this.tabOptions2.Controls.Add(this.btnSetLevelCmd);
			this.tabOptions2.Controls.Add(this.btnSetLevel);
			this.tabOptions2.Controls.Add(this.tbLevel);
			this.tabOptions2.Controls.Add(this.groupBox1);
			this.tabOptions2.Controls.Add(this.btnSearchCmd);
			this.tabOptions2.Controls.Add(this.txtSearchCmd);
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
			this.tabOptions2.Size = new System.Drawing.Size(531, 301);
			this.tabOptions2.TabIndex = 7;
			this.tabOptions2.Text = "Client";
			// 
			// darkGroupBox17
			// 
			this.darkGroupBox17.Controls.Add(this.chkLoginLock);
			this.darkGroupBox17.Controls.Add(this.tbLoginUsername);
			this.darkGroupBox17.Controls.Add(this.tbLoginPassword);
			this.darkGroupBox17.Location = new System.Drawing.Point(336, 87);
			this.darkGroupBox17.Name = "darkGroupBox17";
			this.darkGroupBox17.Size = new System.Drawing.Size(189, 69);
			this.darkGroupBox17.TabIndex = 172;
			this.darkGroupBox17.TabStop = false;
			this.darkGroupBox17.Text = "Fixed Auto Relogin";
			// 
			// chkLoginLock
			// 
			this.chkLoginLock.AutoSize = true;
			this.chkLoginLock.Location = new System.Drawing.Point(133, 33);
			this.chkLoginLock.Name = "chkLoginLock";
			this.chkLoginLock.Size = new System.Drawing.Size(50, 17);
			this.chkLoginLock.TabIndex = 172;
			this.chkLoginLock.Text = "Lock";
			this.chkLoginLock.CheckedChanged += new System.EventHandler(this.chkLoginLock_CheckedChanged);
			// 
			// tbLoginUsername
			// 
			this.tbLoginUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbLoginUsername.Location = new System.Drawing.Point(6, 19);
			this.tbLoginUsername.Name = "tbLoginUsername";
			this.tbLoginUsername.Size = new System.Drawing.Size(124, 20);
			this.tbLoginUsername.TabIndex = 170;
			this.tbLoginUsername.Text = "Username";
			this.tbLoginUsername.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbLoginUsername.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tbLoginPassword
			// 
			this.tbLoginPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbLoginPassword.Location = new System.Drawing.Point(6, 43);
			this.tbLoginPassword.Name = "tbLoginPassword";
			this.tbLoginPassword.Size = new System.Drawing.Size(124, 20);
			this.tbLoginPassword.TabIndex = 171;
			this.tbLoginPassword.Text = "Password";
			this.tbLoginPassword.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbLoginPassword.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// colorfulCommands
			// 
			this.colorfulCommands.Checked = true;
			this.colorfulCommands.CheckState = System.Windows.Forms.CheckState.Checked;
			this.colorfulCommands.Location = new System.Drawing.Point(12, 268);
			this.colorfulCommands.Name = "colorfulCommands";
			this.colorfulCommands.Size = new System.Drawing.Size(240, 30);
			this.colorfulCommands.TabIndex = 169;
			this.colorfulCommands.Text = "Enable custom commands (CPU intensive)\r\n";
			// 
			// darkGroupBox16
			// 
			this.darkGroupBox16.Controls.Add(this.btnAMTest);
			this.darkGroupBox16.Controls.Add(this.chkAMStopBot);
			this.darkGroupBox16.Controls.Add(this.chkAMLogout);
			this.darkGroupBox16.Controls.Add(this.chkAntiMod);
			this.darkGroupBox16.Location = new System.Drawing.Point(336, 6);
			this.darkGroupBox16.Name = "darkGroupBox16";
			this.darkGroupBox16.Size = new System.Drawing.Size(189, 76);
			this.darkGroupBox16.TabIndex = 151;
			this.darkGroupBox16.TabStop = false;
			this.darkGroupBox16.Text = "Anti Mod";
			// 
			// btnAMTest
			// 
			this.btnAMTest.Checked = false;
			this.btnAMTest.Location = new System.Drawing.Point(6, 41);
			this.btnAMTest.Name = "btnAMTest";
			this.btnAMTest.Size = new System.Drawing.Size(75, 23);
			this.btnAMTest.TabIndex = 1;
			this.btnAMTest.Text = "Test";
			this.btnAMTest.Visible = false;
			this.btnAMTest.Click += new System.EventHandler(this.btnAMTest_Click);
			// 
			// chkAMStopBot
			// 
			this.chkAMStopBot.AutoSize = true;
			this.chkAMStopBot.Checked = true;
			this.chkAMStopBot.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAMStopBot.Location = new System.Drawing.Point(104, 45);
			this.chkAMStopBot.Name = "chkAMStopBot";
			this.chkAMStopBot.Size = new System.Drawing.Size(67, 17);
			this.chkAMStopBot.TabIndex = 171;
			this.chkAMStopBot.Text = "Stop Bot";
			// 
			// chkAMLogout
			// 
			this.chkAMLogout.AutoSize = true;
			this.chkAMLogout.Checked = true;
			this.chkAMLogout.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAMLogout.Location = new System.Drawing.Point(104, 21);
			this.chkAMLogout.Name = "chkAMLogout";
			this.chkAMLogout.Size = new System.Drawing.Size(59, 17);
			this.chkAMLogout.TabIndex = 170;
			this.chkAMLogout.Text = "Logout";
			// 
			// chkAntiMod
			// 
			this.chkAntiMod.AutoSize = true;
			this.chkAntiMod.Checked = true;
			this.chkAntiMod.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAntiMod.Location = new System.Drawing.Point(6, 19);
			this.chkAntiMod.Name = "chkAntiMod";
			this.chkAntiMod.Size = new System.Drawing.Size(59, 17);
			this.chkAntiMod.TabIndex = 169;
			this.chkAntiMod.Text = "Enable";
			this.chkAntiMod.MouseHover += new System.EventHandler(this.chkAntiMod_MouseHover);
			// 
			// btnSetSpawn2
			// 
			this.btnSetSpawn2.Checked = false;
			this.btnSetSpawn2.Location = new System.Drawing.Point(196, 189);
			this.btnSetSpawn2.Name = "btnSetSpawn2";
			this.btnSetSpawn2.Size = new System.Drawing.Size(125, 23);
			this.btnSetSpawn2.TabIndex = 168;
			this.btnSetSpawn2.Text = "Set Spawnpoint";
			this.btnSetSpawn2.Click += new System.EventHandler(this.btnSetSpawn2_Click);
			// 
			// darkLabel6
			// 
			this.darkLabel6.AutoSize = true;
			this.darkLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel6.Location = new System.Drawing.Point(416, 241);
			this.darkLabel6.Name = "darkLabel6";
			this.darkLabel6.Size = new System.Drawing.Size(43, 13);
			this.darkLabel6.TabIndex = 167;
			this.darkLabel6.Text = "minutes";
			// 
			// numSaveProgress
			// 
			this.numSaveProgress.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSaveProgress.Location = new System.Drawing.Point(362, 234);
			this.numSaveProgress.LoopValues = false;
			this.numSaveProgress.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
			this.numSaveProgress.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numSaveProgress.Name = "numSaveProgress";
			this.numSaveProgress.Size = new System.Drawing.Size(52, 20);
			this.numSaveProgress.TabIndex = 166;
			this.numSaveProgress.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// chkSaveProgress
			// 
			this.chkSaveProgress.AutoSize = true;
			this.chkSaveProgress.Location = new System.Drawing.Point(342, 215);
			this.chkSaveProgress.Name = "chkSaveProgress";
			this.chkSaveProgress.Size = new System.Drawing.Size(91, 17);
			this.chkSaveProgress.TabIndex = 165;
			this.chkSaveProgress.Text = "Relogin every";
			this.chkSaveProgress.CheckedChanged += new System.EventHandler(this.chkSaveProgress_CheckedChanged);
			this.chkSaveProgress.MouseHover += new System.EventHandler(this.chkSaveProgress_MouseHover);
			// 
			// chkAntiCounter
			// 
			this.chkAntiCounter.AutoSize = true;
			this.chkAntiCounter.Location = new System.Drawing.Point(342, 164);
			this.chkAntiCounter.Name = "chkAntiCounter";
			this.chkAntiCounter.Size = new System.Drawing.Size(116, 17);
			this.chkAntiCounter.TabIndex = 162;
			this.chkAntiCounter.Text = "Anti counter attack";
			this.chkAntiCounter.CheckedChanged += new System.EventHandler(this.chkAntiCounter_CheckedChanged);
			this.chkAntiCounter.MouseHover += new System.EventHandler(this.chkAntiCounter_MouseHover);
			// 
			// numPrivateRoom
			// 
			this.numPrivateRoom.Location = new System.Drawing.Point(430, 187);
			this.numPrivateRoom.Name = "numPrivateRoom";
			this.numPrivateRoom.Size = new System.Drawing.Size(54, 20);
			this.numPrivateRoom.TabIndex = 158;
			this.numPrivateRoom.Text = "rand";
			// 
			// chkPrivateRoom
			// 
			this.chkPrivateRoom.AutoSize = true;
			this.chkPrivateRoom.Location = new System.Drawing.Point(342, 189);
			this.chkPrivateRoom.Name = "chkPrivateRoom";
			this.chkPrivateRoom.Size = new System.Drawing.Size(85, 17);
			this.chkPrivateRoom.TabIndex = 157;
			this.chkPrivateRoom.Text = "Private room";
			this.chkPrivateRoom.CheckedChanged += new System.EventHandler(this.chkPrivateRoom_CheckedChanged);
			this.chkPrivateRoom.MouseHover += new System.EventHandler(this.chkPrivateRoom_MouseHover);
			// 
			// chkGender
			// 
			this.chkGender.AutoSize = true;
			this.chkGender.Location = new System.Drawing.Point(12, 218);
			this.chkGender.Name = "chkGender";
			this.chkGender.Size = new System.Drawing.Size(89, 17);
			this.chkGender.TabIndex = 155;
			this.chkGender.Text = "Gender swap";
			// 
			// numSetFPS
			// 
			this.numSetFPS.IncrementAlternate = new decimal(new int[] {
            10,
            0,
            0,
            65536});
			this.numSetFPS.Location = new System.Drawing.Point(196, 163);
			this.numSetFPS.LoopValues = false;
			this.numSetFPS.Name = "numSetFPS";
			this.numSetFPS.Size = new System.Drawing.Size(51, 20);
			this.numSetFPS.TabIndex = 154;
			this.numSetFPS.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			// 
			// btnSetFPS
			// 
			this.btnSetFPS.Checked = false;
			this.btnSetFPS.Location = new System.Drawing.Point(254, 162);
			this.btnSetFPS.Name = "btnSetFPS";
			this.btnSetFPS.Size = new System.Drawing.Size(66, 23);
			this.btnSetFPS.TabIndex = 153;
			this.btnSetFPS.Text = "Set FPS";
			this.btnSetFPS.Click += new System.EventHandler(this.btnSetFPS_Click);
			// 
			// lblUP
			// 
			this.lblUP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblUP.AutoSize = true;
			this.lblUP.Location = new System.Drawing.Point(420, 280);
			this.lblUP.Name = "lblUP";
			this.lblUP.Size = new System.Drawing.Size(106, 13);
			this.lblUP.TabIndex = 152;
			this.lblUP.Text = "username | password";
			this.lblUP.Visible = false;
			this.lblUP.DoubleClick += new System.EventHandler(this.lblUP_Click_1);
			// 
			// btnSetLevelCmd
			// 
			this.btnSetLevelCmd.Checked = false;
			this.btnSetLevelCmd.Location = new System.Drawing.Point(136, 192);
			this.btnSetLevelCmd.Name = "btnSetLevelCmd";
			this.btnSetLevelCmd.Size = new System.Drawing.Size(53, 20);
			this.btnSetLevelCmd.TabIndex = 151;
			this.btnSetLevelCmd.Text = "(cmd)";
			this.btnSetLevelCmd.Click += new System.EventHandler(this.btnSetLevelCmd_Click);
			// 
			// btnSetLevel
			// 
			this.btnSetLevel.Checked = false;
			this.btnSetLevel.Location = new System.Drawing.Point(90, 192);
			this.btnSetLevel.Name = "btnSetLevel";
			this.btnSetLevel.Size = new System.Drawing.Size(45, 20);
			this.btnSetLevel.TabIndex = 150;
			this.btnSetLevel.Text = "Set";
			this.btnSetLevel.Click += new System.EventHandler(this.btnSetLevel_Click);
			// 
			// tbLevel
			// 
			this.tbLevel.Location = new System.Drawing.Point(10, 192);
			this.tbLevel.Name = "tbLevel";
			this.tbLevel.Size = new System.Drawing.Size(78, 20);
			this.tbLevel.TabIndex = 149;
			this.tbLevel.Text = "Level";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnAddInfoMsg);
			this.groupBox1.Controls.Add(this.btnAddWarnMsg);
			this.groupBox1.Controls.Add(this.inputMsgClient);
			this.groupBox1.Location = new System.Drawing.Point(195, 56);
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
			this.btnAddInfoMsg.Click += new System.EventHandler(this.btnAddInfoMsg_Click);
			// 
			// btnAddWarnMsg
			// 
			this.btnAddWarnMsg.Checked = false;
			this.btnAddWarnMsg.Location = new System.Drawing.Point(6, 43);
			this.btnAddWarnMsg.Name = "btnAddWarnMsg";
			this.btnAddWarnMsg.Size = new System.Drawing.Size(112, 23);
			this.btnAddWarnMsg.TabIndex = 149;
			this.btnAddWarnMsg.Text = "Add Warning";
			this.btnAddWarnMsg.Click += new System.EventHandler(this.btnAddWarnMsg_Click);
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
			this.btnSearchCmd.Location = new System.Drawing.Point(194, 29);
			this.btnSearchCmd.Name = "btnSearchCmd";
			this.btnSearchCmd.Size = new System.Drawing.Size(125, 23);
			this.btnSearchCmd.TabIndex = 147;
			this.btnSearchCmd.Text = "Search";
			this.btnSearchCmd.Click += new System.EventHandler(this.btnSearchCmd_Click);
			// 
			// txtSearchCmd
			// 
			this.txtSearchCmd.Location = new System.Drawing.Point(195, 6);
			this.txtSearchCmd.Name = "txtSearchCmd";
			this.txtSearchCmd.Size = new System.Drawing.Size(124, 20);
			this.txtSearchCmd.TabIndex = 146;
			this.txtSearchCmd.Text = "Search Commands";
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
			this.btnChangeNameCmd.Checked = false;
			this.btnChangeNameCmd.Location = new System.Drawing.Point(136, 145);
			this.btnChangeNameCmd.Name = "btnChangeNameCmd";
			this.btnChangeNameCmd.Size = new System.Drawing.Size(53, 21);
			this.btnChangeNameCmd.TabIndex = 142;
			this.btnChangeNameCmd.Text = "(cmd)";
			this.btnChangeNameCmd.Click += new System.EventHandler(this.btnChangeCmd_Click);
			// 
			// btnchangeName
			// 
			this.btnchangeName.Checked = false;
			this.btnchangeName.Location = new System.Drawing.Point(90, 145);
			this.btnchangeName.Name = "btnchangeName";
			this.btnchangeName.Size = new System.Drawing.Size(45, 21);
			this.btnchangeName.TabIndex = 142;
			this.btnchangeName.Text = "Set";
			this.btnchangeName.Click += new System.EventHandler(this.btnchangeName_Click);
			// 
			// btnChangeGuildCmd
			// 
			this.btnChangeGuildCmd.Checked = false;
			this.btnChangeGuildCmd.Location = new System.Drawing.Point(136, 169);
			this.btnChangeGuildCmd.Name = "btnChangeGuildCmd";
			this.btnChangeGuildCmd.Size = new System.Drawing.Size(53, 20);
			this.btnChangeGuildCmd.TabIndex = 143;
			this.btnChangeGuildCmd.Text = "(cmd)";
			this.btnChangeGuildCmd.Click += new System.EventHandler(this.btnChangeCmd_Click);
			// 
			// btnchangeGuild
			// 
			this.btnchangeGuild.Checked = false;
			this.btnchangeGuild.Location = new System.Drawing.Point(90, 169);
			this.btnchangeGuild.Name = "btnchangeGuild";
			this.btnchangeGuild.Size = new System.Drawing.Size(45, 20);
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
			// tabHunt
			// 
			this.tabHunt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabHunt.Controls.Add(this.btnAddCmdHunt);
			this.tabHunt.Controls.Add(this.chkIsTempF);
			this.tabHunt.Controls.Add(this.darkLabel3);
			this.tabHunt.Controls.Add(this.tbGetAfterF);
			this.tabHunt.Controls.Add(this.chkGetAfterF);
			this.tabHunt.Controls.Add(this.chkAddToWhitelistF);
			this.tabHunt.Controls.Add(this.tbItemQtyF);
			this.tabHunt.Controls.Add(this.tbItemNameF);
			this.tabHunt.Controls.Add(this.tbMonNameF);
			this.tabHunt.Controls.Add(this.cbBlankFirstF);
			this.tabHunt.Controls.Add(this.btnGetMapF);
			this.tabHunt.Controls.Add(this.tbPadF);
			this.tabHunt.Controls.Add(this.tbCellF);
			this.tabHunt.Controls.Add(this.tbMapF);
			this.tabHunt.Controls.Add(this.darkLabel2);
			this.tabHunt.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabHunt.Location = new System.Drawing.Point(4, 23);
			this.tabHunt.Margin = new System.Windows.Forms.Padding(0);
			this.tabHunt.Name = "tabHunt";
			this.tabHunt.Padding = new System.Windows.Forms.Padding(3);
			this.tabHunt.Size = new System.Drawing.Size(531, 301);
			this.tabHunt.TabIndex = 3;
			this.tabHunt.Text = "Hunt";
			// 
			// btnAddCmdHunt
			// 
			this.btnAddCmdHunt.Checked = false;
			this.btnAddCmdHunt.Location = new System.Drawing.Point(9, 196);
			this.btnAddCmdHunt.Name = "btnAddCmdHunt";
			this.btnAddCmdHunt.Size = new System.Drawing.Size(141, 23);
			this.btnAddCmdHunt.TabIndex = 170;
			this.btnAddCmdHunt.Text = "Add Command";
			this.btnAddCmdHunt.Click += new System.EventHandler(this.btnAddCmdHunt_Click);
			// 
			// chkIsTempF
			// 
			this.chkIsTempF.AutoSize = true;
			this.chkIsTempF.Location = new System.Drawing.Point(156, 97);
			this.chkIsTempF.Name = "chkIsTempF";
			this.chkIsTempF.Size = new System.Drawing.Size(63, 17);
			this.chkIsTempF.TabIndex = 169;
			this.chkIsTempF.Text = "is Temp";
			// 
			// darkLabel3
			// 
			this.darkLabel3.AutoSize = true;
			this.darkLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel3.Location = new System.Drawing.Point(134, 166);
			this.darkLabel3.Name = "darkLabel3";
			this.darkLabel3.Size = new System.Drawing.Size(24, 13);
			this.darkLabel3.TabIndex = 168;
			this.darkLabel3.Text = "kills";
			// 
			// tbGetAfterF
			// 
			this.tbGetAfterF.Location = new System.Drawing.Point(98, 161);
			this.tbGetAfterF.Name = "tbGetAfterF";
			this.tbGetAfterF.Size = new System.Drawing.Size(35, 20);
			this.tbGetAfterF.TabIndex = 167;
			this.tbGetAfterF.Text = "5";
			// 
			// chkGetAfterF
			// 
			this.chkGetAfterF.AutoSize = true;
			this.chkGetAfterF.Location = new System.Drawing.Point(9, 162);
			this.chkGetAfterF.Name = "chkGetAfterF";
			this.chkGetAfterF.Size = new System.Drawing.Size(90, 17);
			this.chkGetAfterF.TabIndex = 166;
			this.chkGetAfterF.Text = "Get drops per";
			// 
			// chkAddToWhitelistF
			// 
			this.chkAddToWhitelistF.AutoSize = true;
			this.chkAddToWhitelistF.Checked = true;
			this.chkAddToWhitelistF.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAddToWhitelistF.Location = new System.Drawing.Point(9, 141);
			this.chkAddToWhitelistF.Name = "chkAddToWhitelistF";
			this.chkAddToWhitelistF.Size = new System.Drawing.Size(99, 17);
			this.chkAddToWhitelistF.TabIndex = 165;
			this.chkAddToWhitelistF.Text = "add to Whitelist";
			// 
			// tbItemQtyF
			// 
			this.tbItemQtyF.Location = new System.Drawing.Point(9, 119);
			this.tbItemQtyF.Name = "tbItemQtyF";
			this.tbItemQtyF.Size = new System.Drawing.Size(141, 20);
			this.tbItemQtyF.TabIndex = 164;
			this.tbItemQtyF.Text = "Quantity (* = any)";
			this.tbItemQtyF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbItemQtyF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tbItemNameF
			// 
			this.tbItemNameF.Location = new System.Drawing.Point(9, 95);
			this.tbItemNameF.Name = "tbItemNameF";
			this.tbItemNameF.Size = new System.Drawing.Size(141, 20);
			this.tbItemNameF.TabIndex = 163;
			this.tbItemNameF.Text = "Item name";
			this.tbItemNameF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbItemNameF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tbMonNameF
			// 
			this.tbMonNameF.Location = new System.Drawing.Point(9, 71);
			this.tbMonNameF.Name = "tbMonNameF";
			this.tbMonNameF.Size = new System.Drawing.Size(141, 20);
			this.tbMonNameF.TabIndex = 162;
			this.tbMonNameF.Text = "Monster (* = random)";
			this.tbMonNameF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbMonNameF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// cbBlankFirstF
			// 
			this.cbBlankFirstF.AutoSize = true;
			this.cbBlankFirstF.Checked = true;
			this.cbBlankFirstF.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbBlankFirstF.Location = new System.Drawing.Point(10, 46);
			this.cbBlankFirstF.Name = "cbBlankFirstF";
			this.cbBlankFirstF.Size = new System.Drawing.Size(124, 17);
			this.cbBlankFirstF.TabIndex = 161;
			this.cbBlankFirstF.Text = "Blank firts before join";
			// 
			// btnGetMapF
			// 
			this.btnGetMapF.Checked = false;
			this.btnGetMapF.Location = new System.Drawing.Point(312, 20);
			this.btnGetMapF.Name = "btnGetMapF";
			this.btnGetMapF.Size = new System.Drawing.Size(83, 23);
			this.btnGetMapF.TabIndex = 160;
			this.btnGetMapF.Text = "Get";
			this.btnGetMapF.Click += new System.EventHandler(this.btnGetMapF_Click);
			// 
			// tbPadF
			// 
			this.tbPadF.Location = new System.Drawing.Point(235, 22);
			this.tbPadF.Name = "tbPadF";
			this.tbPadF.Size = new System.Drawing.Size(71, 20);
			this.tbPadF.TabIndex = 159;
			this.tbPadF.Text = "Spawn";
			this.tbPadF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbPadF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tbCellF
			// 
			this.tbCellF.Location = new System.Drawing.Point(153, 22);
			this.tbCellF.Name = "tbCellF";
			this.tbCellF.Size = new System.Drawing.Size(76, 20);
			this.tbCellF.TabIndex = 158;
			this.tbCellF.Text = "Enter";
			this.tbCellF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbCellF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// tbMapF
			// 
			this.tbMapF.Location = new System.Drawing.Point(9, 22);
			this.tbMapF.Name = "tbMapF";
			this.tbMapF.Size = new System.Drawing.Size(138, 20);
			this.tbMapF.TabIndex = 157;
			this.tbMapF.Text = "battleon-1e99";
			this.tbMapF.Enter += new System.EventHandler(this.TextboxEnter);
			this.tbMapF.Leave += new System.EventHandler(this.TextboxLeave);
			// 
			// darkLabel2
			// 
			this.darkLabel2.AutoSize = true;
			this.darkLabel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel2.Location = new System.Drawing.Point(3, 3);
			this.darkLabel2.Name = "darkLabel2";
			this.darkLabel2.Size = new System.Drawing.Size(72, 13);
			this.darkLabel2.TabIndex = 19;
			this.darkLabel2.Text = "Short Hunting";
			// 
			// tabBots
			// 
			this.tabBots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabBots.Controls.Add(this.txtSavedDesc);
			this.tabBots.Controls.Add(this.darkPanel1);
			this.tabBots.Controls.Add(this.lblBoosts);
			this.tabBots.Controls.Add(this.lblDrops);
			this.tabBots.Controls.Add(this.lblQuests);
			this.tabBots.Controls.Add(this.lblSkills);
			this.tabBots.Controls.Add(this.lblCommands);
			this.tabBots.Controls.Add(this.lblItems);
			this.tabBots.Controls.Add(this.txtSavedAuthor);
			this.tabBots.Controls.Add(this.lblBots);
			this.tabBots.Controls.Add(this.txtSavedAdd);
			this.tabBots.Controls.Add(this.btnSavedAdd);
			this.tabBots.Controls.Add(this.txtSaved);
			this.tabBots.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabBots.Location = new System.Drawing.Point(4, 23);
			this.tabBots.Margin = new System.Windows.Forms.Padding(0);
			this.tabBots.Name = "tabBots";
			this.tabBots.Padding = new System.Windows.Forms.Padding(3);
			this.tabBots.Size = new System.Drawing.Size(531, 301);
			this.tabBots.TabIndex = 6;
			this.tabBots.Text = "Bots";
			// 
			// txtSavedDesc
			// 
			this.txtSavedDesc.Location = new System.Drawing.Point(321, 90);
			this.txtSavedDesc.MaxLength = 2147483647;
			this.txtSavedDesc.Multiline = true;
			this.txtSavedDesc.Name = "txtSavedDesc";
			this.txtSavedDesc.ReadOnly = true;
			this.txtSavedDesc.Size = new System.Drawing.Size(193, 169);
			this.txtSavedDesc.TabIndex = 21;
			this.txtSavedDesc.Text = "Description";
			// 
			// darkPanel1
			// 
			this.darkPanel1.Controls.Add(this.treeBots);
			this.darkPanel1.Location = new System.Drawing.Point(4, 27);
			this.darkPanel1.Name = "darkPanel1";
			this.darkPanel1.Size = new System.Drawing.Size(307, 232);
			this.darkPanel1.TabIndex = 148;
			// 
			// treeBots
			// 
			this.treeBots.AllowDrop = true;
			this.treeBots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.treeBots.ForeColor = System.Drawing.Color.Gainsboro;
			this.treeBots.LineColor = System.Drawing.Color.DarkGray;
			this.treeBots.Location = new System.Drawing.Point(0, 0);
			this.treeBots.Name = "treeBots";
			this.treeBots.Size = new System.Drawing.Size(307, 232);
			this.treeBots.TabIndex = 17;
			this.treeBots.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeBots_AfterExpand);
			this.treeBots.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeBots_AfterSelect);
			// 
			// lblBoosts
			// 
			this.lblBoosts.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblBoosts.AutoSize = true;
			this.lblBoosts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblBoosts.Location = new System.Drawing.Point(286, 275);
			this.lblBoosts.Name = "lblBoosts";
			this.lblBoosts.Size = new System.Drawing.Size(42, 13);
			this.lblBoosts.TabIndex = 25;
			this.lblBoosts.Text = "Boosts:";
			this.lblBoosts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblDrops
			// 
			this.lblDrops.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblDrops.AutoSize = true;
			this.lblDrops.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblDrops.Location = new System.Drawing.Point(230, 275);
			this.lblDrops.Name = "lblDrops";
			this.lblDrops.Size = new System.Drawing.Size(38, 13);
			this.lblDrops.TabIndex = 24;
			this.lblDrops.Text = "Drops:";
			this.lblDrops.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblQuests
			// 
			this.lblQuests.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblQuests.AutoSize = true;
			this.lblQuests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblQuests.Location = new System.Drawing.Point(172, 275);
			this.lblQuests.Name = "lblQuests";
			this.lblQuests.Size = new System.Drawing.Size(43, 13);
			this.lblQuests.TabIndex = 23;
			this.lblQuests.Text = "Quests:";
			this.lblQuests.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblSkills
			// 
			this.lblSkills.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblSkills.AutoSize = true;
			this.lblSkills.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblSkills.Location = new System.Drawing.Point(119, 275);
			this.lblSkills.Name = "lblSkills";
			this.lblSkills.Size = new System.Drawing.Size(34, 13);
			this.lblSkills.TabIndex = 22;
			this.lblSkills.Text = "Skills:";
			this.lblSkills.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblCommands
			// 
			this.lblCommands.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblCommands.AutoSize = true;
			this.lblCommands.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblCommands.Location = new System.Drawing.Point(38, 262);
			this.lblCommands.Name = "lblCommands";
			this.lblCommands.Size = new System.Drawing.Size(62, 26);
			this.lblCommands.TabIndex = 21;
			this.lblCommands.Text = "Number of\r\nCommands:";
			this.lblCommands.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// lblItems
			// 
			this.lblItems.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblItems.AutoSize = true;
			this.lblItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblItems.Location = new System.Drawing.Point(348, 275);
			this.lblItems.Name = "lblItems";
			this.lblItems.Size = new System.Drawing.Size(35, 13);
			this.lblItems.TabIndex = 146;
			this.lblItems.Text = "Items:";
			this.lblItems.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// txtSavedAuthor
			// 
			this.txtSavedAuthor.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.txtSavedAuthor.Location = new System.Drawing.Point(321, 64);
			this.txtSavedAuthor.Name = "txtSavedAuthor";
			this.txtSavedAuthor.ReadOnly = true;
			this.txtSavedAuthor.Size = new System.Drawing.Size(193, 20);
			this.txtSavedAuthor.TabIndex = 19;
			this.txtSavedAuthor.Text = "Author";
			// 
			// lblBots
			// 
			this.lblBots.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblBots.AutoSize = true;
			this.lblBots.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblBots.Location = new System.Drawing.Point(317, 50);
			this.lblBots.Name = "lblBots";
			this.lblBots.Size = new System.Drawing.Size(83, 13);
			this.lblBots.TabIndex = 18;
			this.lblBots.Text = "Number of Bots:";
			// 
			// txtSavedAdd
			// 
			this.txtSavedAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.txtSavedAdd.Location = new System.Drawing.Point(321, 27);
			this.txtSavedAdd.Name = "txtSavedAdd";
			this.txtSavedAdd.Size = new System.Drawing.Size(118, 20);
			this.txtSavedAdd.TabIndex = 16;
			// 
			// btnSavedAdd
			// 
			this.btnSavedAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnSavedAdd.Checked = false;
			this.btnSavedAdd.Location = new System.Drawing.Point(445, 27);
			this.btnSavedAdd.Name = "btnSavedAdd";
			this.btnSavedAdd.Size = new System.Drawing.Size(69, 22);
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
			this.txtSaved.Size = new System.Drawing.Size(510, 20);
			this.txtSaved.TabIndex = 13;
			this.txtSaved.TextChanged += new System.EventHandler(this.txtSaved_TextChanged);
			// 
			// tabInfo
			// 
			this.tabInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.tabInfo.Controls.Add(this.panel5);
			this.tabInfo.ForeColor = System.Drawing.Color.Gainsboro;
			this.tabInfo.Location = new System.Drawing.Point(4, 23);
			this.tabInfo.Name = "tabInfo";
			this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
			this.tabInfo.Size = new System.Drawing.Size(192, 73);
			this.tabInfo.TabIndex = 9;
			this.tabInfo.Text = "Info";
			this.tabInfo.ToolTipText = "The Info about the bot you\'ve loaded";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.richTextBox2);
			this.panel5.Controls.Add(this.rtbInfo);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(3, 3);
			this.panel5.Margin = new System.Windows.Forms.Padding(5);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(186, 67);
			this.panel5.TabIndex = 0;
			// 
			// richTextBox2
			// 
			this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.richTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox2.Location = new System.Drawing.Point(0, 48);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.Size = new System.Drawing.Size(54, 22);
			this.richTextBox2.TabIndex = 1;
			this.richTextBox2.Text = "Test here";
			this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
			// 
			// rtbInfo
			// 
			this.rtbInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.rtbInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbInfo.ForeColor = System.Drawing.Color.Gainsboro;
			this.rtbInfo.Location = new System.Drawing.Point(0, 0);
			this.rtbInfo.Name = "rtbInfo";
			this.rtbInfo.Size = new System.Drawing.Size(186, 67);
			this.rtbInfo.TabIndex = 0;
			this.rtbInfo.Text = "This is where information about a bot will be shown in RichTextFormat";
			this.rtbInfo.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbInfo_LinkClicked);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 255);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.splitContainer1.Panel1.Controls.Add(this.cbLists);
			this.splitContainer1.Panel1.Controls.Add(this.panel3);
			this.splitContainer1.Panel1.Controls.Add(this.chkAll);
			this.splitContainer1.Panel1.Controls.Add(this.btnClear);
			this.splitContainer1.Panel1MinSize = 0;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.panel4);
			this.splitContainer1.Panel2.Controls.Add(this.panel2);
			this.splitContainer1.Panel2MinSize = 0;
			this.splitContainer1.Size = new System.Drawing.Size(258, 75);
			this.splitContainer1.SplitterDistance = 124;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 149;
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
			this.cbLists.Size = new System.Drawing.Size(119, 21);
			this.cbLists.TabIndex = 169;
			this.cbLists.SelectedIndexChanged += new System.EventHandler(this.cbLists_SelectedIndexChanged);
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.btnDown);
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(120, 22);
			this.panel3.TabIndex = 148;
			// 
			// btnDown
			// 
			this.btnDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnDown.Checked = false;
			this.btnDown.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnDown.Location = new System.Drawing.Point(0, 0);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(120, 22);
			this.btnDown.TabIndex = 166;
			this.btnDown.Text = "▼";
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// chkAll
			// 
			this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkAll.AutoSize = true;
			this.chkAll.Location = new System.Drawing.Point(85, 28);
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
			this.btnClear.Location = new System.Drawing.Point(1, 25);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(78, 22);
			this.btnClear.TabIndex = 167;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.panel4.Controls.Add(this.chkEnable);
			this.panel4.Controls.Add(this.btnRemove);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 22);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(133, 51);
			this.panel4.TabIndex = 148;
			// 
			// chkEnable
			// 
			this.chkEnable.AutoSize = true;
			this.chkEnable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.chkEnable.Location = new System.Drawing.Point(23, 30);
			this.chkEnable.Name = "chkEnable";
			this.chkEnable.Size = new System.Drawing.Size(78, 17);
			this.chkEnable.TabIndex = 169;
			this.chkEnable.Text = "Enable Bot";
			this.chkEnable.UseVisualStyleBackColor = true;
			this.chkEnable.CheckedChanged += new System.EventHandler(this.chkEnableBot_CheckedChanged);
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnRemove.Checked = false;
			this.btnRemove.Location = new System.Drawing.Point(0, 3);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(132, 22);
			this.btnRemove.TabIndex = 166;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// panel2
			// 
			this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel2.Controls.Add(this.btnUp);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(133, 22);
			this.panel2.TabIndex = 147;
			// 
			// btnUp
			// 
			this.btnUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnUp.Checked = false;
			this.btnUp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnUp.Location = new System.Drawing.Point(0, 0);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(133, 22);
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
			this.panel1.Size = new System.Drawing.Size(267, 328);
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
			this.splitContainer2.Panel2.Controls.Add(this.mainTabControl);
			this.splitContainer2.Size = new System.Drawing.Size(810, 328);
			this.splitContainer2.SplitterDistance = 267;
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
			// BotManager
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.ClientSize = new System.Drawing.Size(824, 342);
			this.ContextMenuStrip = this.BotManagerMenuStrip;
			this.Controls.Add(this.splitContainer2);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Icon = global::Properties.Resources.GrimoireIcon;
			this.Name = "BotManager";
			this.Padding = new System.Windows.Forms.Padding(7);
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = " Bot";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BotManager_FormClosing);
			this.Load += new System.EventHandler(this.BotManager_Load);
			this.mainTabControl.ResumeLayout(false);
			this.tabCombat.ResumeLayout(false);
			this.tabCombat.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRestMP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numRest)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSkillD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSafe)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSkill)).EndInit();
			this.tabMap.ResumeLayout(false);
			this.tabMap.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numWalkY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numWalkX)).EndInit();
			this.tabItem.ResumeLayout(false);
			this.tabItem.PerformLayout();
			this.darkGroupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numMapItem)).EndInit();
			this.darkGroupBox4.ResumeLayout(false);
			this.darkGroupBox4.PerformLayout();
			this.darkGroupBox3.ResumeLayout(false);
			this.darkGroupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numShopId)).EndInit();
			this.darkGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numDropDelay)).EndInit();
			this.tabQuest.ResumeLayout(false);
			this.tabQuest.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numQQuestId)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numEnsureTries)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuestItem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numQuestID)).EndInit();
			this.tabMisc.ResumeLayout(false);
			this.tabMisc.PerformLayout();
			this.darkGroupBox11.ResumeLayout(false);
			this.darkGroupBox10.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numSpamTimes)).EndInit();
			this.darkGroupBox14.ResumeLayout(false);
			this.darkGroupBox14.PerformLayout();
			this.darkGroupBox12.ResumeLayout(false);
			this.darkGroupBox12.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSetInt)).EndInit();
			this.darkGroupBox8.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numIndexCmd)).EndInit();
			this.darkGroupBox7.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numBeepTimes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
			this.tabMisc2.ResumeLayout(false);
			this.tabMisc2.PerformLayout();
			this.darkGroupBox15.ResumeLayout(false);
			this.darkGroupBox15.PerformLayout();
			this.darkGroupBox13.ResumeLayout(false);
			this.darkGroupBox13.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBotDelay)).EndInit();
			this.darkGroupBox9.ResumeLayout(false);
			this.darkGroupBox9.PerformLayout();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.darkGroupBox1.ResumeLayout(false);
			this.darkGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBSDelay)).EndInit();
			this.tabOptions.ResumeLayout(false);
			this.tabOptions.PerformLayout();
			this.darkGroupBox6.ResumeLayout(false);
			this.darkGroupBox6.PerformLayout();
			this.splitContainer5.Panel1.ResumeLayout(false);
			this.splitContainer5.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
			this.splitContainer5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numOptionsTimer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numWalkSpeed)).EndInit();
			this.grpLogin.ResumeLayout(false);
			this.grpLogin.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numRelogDelay)).EndInit();
			this.tabOptions2.ResumeLayout(false);
			this.tabOptions2.PerformLayout();
			this.darkGroupBox17.ResumeLayout(false);
			this.darkGroupBox17.PerformLayout();
			this.darkGroupBox16.ResumeLayout(false);
			this.darkGroupBox16.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSaveProgress)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSetFPS)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.grpAccessLevel.ResumeLayout(false);
			this.grpAccessLevel.PerformLayout();
			this.grpAlignment.ResumeLayout(false);
			this.tabHunt.ResumeLayout(false);
			this.tabHunt.PerformLayout();
			this.tabBots.ResumeLayout(false);
			this.tabBots.PerformLayout();
			this.darkPanel1.ResumeLayout(false);
			this.tabInfo.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
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
			_ = Proxy.Instance.SendToClient(data);
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

		private void btnProvokeOn_Click(object sender, EventArgs e)
		{
			/*AddCommand(new CmdToggleProvoke
			{
				Type = 1
			}, (ModifierKeys & Keys.Control) == Keys.Control);*/

			AddCommand(new CmdProvoke
			{
				Set = true
			}, (ModifierKeys & Keys.Control) == Keys.Control) ;
		}

		private void btnProvokeOff_Click(object sender, EventArgs e)
		{
			/*AddCommand(new CmdToggleProvoke
			{
				Type = 0
			}, (ModifierKeys & Keys.Control) == Keys.Control);*/

			AddCommand(new CmdProvoke
			{
				Set = false
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
			else if (e.TabPage == tabMisc)
			{
				GetAllCommands<CmdLabel>(lbLabels);
			}
		}

		private void txtSaved_TextChanged(object sender, EventArgs e)
		{
			UpdateTree();
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

		private void lstCommands_DragDrop(object sender, DragEventArgs e)
		{
			Configuration config;
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files.Length > 1) return;
			if (TryDeserialize(File.ReadAllText(files[0]), out config))
			{
				ApplyConfiguration(config);
				GetAllCommands<CmdLabel>(lbLabels);
			}
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
			AddCommand(new CmdBlank3 { Text = "...", Alpha = 1, R = 220, G = 220, B = 220 }, (ModifierKeys & Keys.Control) == Keys.Control);
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
			AddCommand(new CmdMoveToCell
			{
				Cell = "Blank",
				Pad = "Spawn"
			}, (ModifierKeys & Keys.Control) == Keys.Control);
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
			string index = numSkill.Text;
			Skill skill = new Skill
			{
				Text = Skill.GetSkillName(index),
				Index = index,
				Type = Skill.SkillType.Normal,
			};

			AddCommand(new CmdUseSkill
			{
				Skill = skill,
				Wait = this.chkSkillCD.Checked,
				Targeted = chkUseSkillTargeted.Checked
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
			Player.ToggleMute(!chkToggleMute.Checked);
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
				Monster = txtMonster.Text == "Monster (* = random)" ? "*" : txtMonster.Text
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
			if (mainTabControl.Visible)
			{
				this.ClientSize = new Size(p1w, ClientSize.Height);
				mainTabControl.Visible = false;
				splitContainer2.Panel2Collapsed = true;
			}
			else
			{
				this.ClientSize = new Size(p1w + 500, ClientSize.Height);
				splitContainer2.Panel2Collapsed = false;
				mainTabControl.Visible = true;
			}
		}

		public Dictionary<string, Color> CurrentColors = new Dictionary<string, Color>();

		public Color GetCurrentColor(string cmd)
		{
			if (!CurrentColors.ContainsKey(cmd))
				CurrentColors[cmd] = GetColor(cmd);
			return CurrentColors[cmd];
		}

		public Color GetColor(string name)
		{
			Config c = Config.Load(Application.StartupPath + "\\config.cfg");
			return Color.FromArgb(int.Parse(c.Get(name + "Color") ?? "FFDCDCDC", System.Globalization.NumberStyles.HexNumber));
		}

		public Dictionary<string, bool> CurrentCentered = new Dictionary<string, bool>();

		private bool GetCurrentBoolCentered(string cmd)
		{
			if (!CurrentCentered.ContainsKey(cmd))
				CurrentCentered[cmd] = GetBoolCentered(cmd);
			return CurrentCentered[cmd];
		}

		private bool GetBoolCentered(string name)
		{
			Config c = Config.Load(Application.StartupPath + "\\config.cfg");
			return bool.Parse(c.Get(name + "Centered") ?? "false");
		}

		private void lstCommands_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (!(e.Index > -1))
				return;
			e.DrawBackground();
			if (!colorfulCommands.Checked)
			{
				// Define the default color of the brush as black.
				Brush myBrush = Brushes.Gainsboro;
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
			//string WindowText = SystemColors.WindowText.ToArgb().ToString();
			SolidBrush color = new SolidBrush(GetCurrentColor(scmd));
			SolidBrush varColor = new SolidBrush(GetCurrentColor("Variable"));
			SolidBrush eVarColor = new SolidBrush(GetCurrentColor("ExtendedVariable"));
			SolidBrush indexcolor = new SolidBrush(GetCurrentColor("Index"));
			RectangleF region = new RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height);
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

			if (cmd is CmdBlank || cmd is CmdBlank2 || cmd is CmdBlank3)
			{
				string txt = lstCommands.Items[e.Index].ToString();
				Font cmdFont = new Font("Arial", e.Font.Size + (float)6.5, FontStyle.Bold, GraphicsUnit.Pixel);
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
							e.Graphics.DrawString(txt, cmdFont, b2b, e.Bounds, centered);
					}catch{}
				}
				else if(cmd is CmdBlank3)
				{
					var jsonObj = JsonConvert.DeserializeObject<CmdBlank3>(JsonConvert.SerializeObject(cmd));
					try
					{
						SolidBrush colorBrush = new SolidBrush(jsonObj.Argb());
						e.Graphics.DrawString(txt, cmdFont, colorBrush, e.Bounds, centered);
					}
					catch { }
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

			// Draw the second string (rest of the string, in this case Command type).
			string cmdText = lstCommands.Items[e.Index].ToString();
			string[] toDraw;
			if (cmdText.Contains(':')) {
				toDraw = lstCommands.Items[e.Index].ToString().Split(':');
				Region second = DrawString(e.Graphics, toDraw[0], font, color, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
				region = new RectangleF(region.X + second.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
				if(toDraw[1].Contains(","))
				{
					toDraw = toDraw[1].Split(',');
					Region third = DrawString(e.Graphics, toDraw[0], font, varColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
					for (int i = 1; i < toDraw.Length; i++)
					{
						region = new RectangleF(region.X + third.GetBounds(e.Graphics).Width + 3, region.Y, region.Width, region.Height);
						third = DrawString(e.Graphics, toDraw[i], font, eVarColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
					}
				}
				else
					DrawString(e.Graphics, toDraw[1], font, varColor, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);
			}
			else
				DrawString(e.Graphics, cmdText, font, color, region, GetCurrentBoolCentered(scmd) ? centered : StringFormat.GenericDefault);

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

		private void treeBots_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}

		private void btnClientPacket_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdPacket
			{
				Packet = txtPacket.Text,
				SpamTimes = Decimal.ToInt32(numSpamTimes.Value),
				ForClient = true
			}, (ModifierKeys & Keys.Control) == Keys.Control);
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

		private void btnReturnCmd_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdReturn(), (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnClearTempVar_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdClearTemp(), (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnSaveAllCommands_Click(object sender, EventArgs e)
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
					Configuration oldConfig = GenerateConfiguration();
					chkAll.Checked = true;
					btnClear.PerformClick();
					chkAll.Checked = false;

					var type = typeof(IBotCommand); // Get the type of our interface
					var types = AppDomain.CurrentDomain.GetAssemblies() // Get the assemblies associated with our project
						.SelectMany(s => s.GetTypes()) // Get all the types
						.Where(p => type.IsAssignableFrom(p) && !p.IsInterface); // Filter to find any type that can be assigned to an IModule

					var typeList = types as Type[] ?? types.ToArray(); // Convert to an array
					for (int i = 0; i < typeList.Count(); i++)
					{
						//AddCommand(new typeList[i]);
						//someone figure out how to do this thx
					}

					Configuration value = GenerateConfiguration();
					try
					{
						File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(value, Formatting.Indented, _saveSerializerSettings));
					}
					catch (Exception ex)
					{
						MessageBox.Show("Unable to save bot: " + ex.Message);
					}
					finally
					{
						ApplyConfiguration(oldConfig);
					}
				}
			}
			
		}

		private void btnSetFPSCmd_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdSetFPS {
				FPS = (int)numSetFPS.Value
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void lbLabels_DoubleClick(object sender, EventArgs e)
		{
			object selected = lbLabels.SelectedItem;
			if (selected != null)
				txtLabel.Text = selected.ToString().Substring(1, selected.ToString().Length - 2);
		}

		private void richTextBox2_TextChanged(object sender, EventArgs e)
		{
			try {
				rtbInfo.Rtf = richTextBox2.Text;
				return;

				var currDir = System.AppDomain.CurrentDomain.BaseDirectory;
				var tempDoc = $@"{currDir}/tempdoc";
				var target = $@"{currDir}/temprtf";
				if (File.Exists(tempDoc))
				{
					File.Delete(tempDoc);
				}
				using (FileStream newFile = File.Create(tempDoc))
				{
					byte[] toWrite = new UTF8Encoding(true).GetBytes(richTextBox2.Text);
					newFile.Write(toWrite, 0, toWrite.Length);
				}
				var tempRtf = "";
				Task.WaitAll(Task.Run(() => DocConvert.toRtf(tempDoc, target, out tempRtf)));
				rtbInfo.Rtf = File.ReadAllText(tempRtf);
			} catch { };
		}

		private void rtbInfo_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			Process.Start(e.LinkText);
		}

		private async void btnSetLevel_Click(object sender, EventArgs e)
		{
			if (!int.TryParse(tbLevel.Text, out _)) return;

			string packet =
				"{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"levelUp\",\"intExpToLevel\":\"2000\",\"intLevel\":" + tbLevel.Text + "}}}";
			await Proxy.Instance.SendToClient(packet);
		}

		private void btnSetLevelCmd_Click(object sender, EventArgs e)
		{
			if (!int.TryParse(tbLevel.Text, out _)) return;

			string packet = "Level " + tbLevel.Text;
			this.AddCommand(new CmdPacket
			{
				Packet = packet,
				SpamTimes = 1,
				ForClient = true
			}, (Control.ModifierKeys & Keys.Control) == Keys.Control);
		}

		private async void chkWalkSpeed_CheckedChanged(object sender, EventArgs e)
		{
			if (chkWalkSpeed.Checked)
			{
				Flash.Call("SetWalkSpeed", new string[] { numWalkSpeed.Value.ToString() });

				//string cell = Player.Cell;
				while (chkWalkSpeed.Checked && (int)numWalkSpeed.Value != 8)
				{
					//if (Player.Cell != cell)
					await Task.Delay(200);
					Flash.Call("SetWalkSpeed", new string[] { numWalkSpeed.Value.ToString() });
				}
			}
			else
			{
				Flash.Call("SetWalkSpeed", new string[] { "8" });
			}
		}

		private void btnAddCmdHunt_Click(object sender, EventArgs e)
		{
			if (tbItemNameF.Text.Length > 0 && tbItemQtyF.Text.Length > 0)
			{
				string monster = string.IsNullOrEmpty(this.tbMonNameF.Text) ? "*" : this.tbMonNameF.Text;
				string monId = "";
				if (monster.Split(' ')[0].Equals("/id"))
				{
					monId = monster.Split(' ')[1];
				}
				bool flag = this.tbMonNameF.Text == "Monster (* = random)" || string.IsNullOrEmpty(tbMonNameF.Text);
				if (flag) monster = "*";
				string text = this.tbItemNameF.Text;
				string text2 = this.tbItemQtyF.Text;

				if (chkAddToWhitelistF.Checked)
				{
					if (tbItemNameF.Text.Length <= 0) return;
					string[] items = tbItemNameF.Text.Split(new char[] {
						','
					});

					foreach (string item in items)
					{
						if (!lstDrops.Items.Cast<string>().ToList().Any((string d) => d.Equals(item, StringComparison.OrdinalIgnoreCase)))
							lstDrops.Items.Add(item);
					}
				}

				int times = 0;

				CmdShortHunt cmd = new CmdShortHunt
				{
					Map = tbMapF.Text,
					Cell = tbCellF.Text,
					Pad = tbPadF.Text,
					ItemType = (chkIsTempF.Checked ? ItemType.TempItems : ItemType.Items),
					Monster = monster,
					ItemName = text,
					Quantity = text2,
					IsGetDrops = chkGetAfterF.Checked,
					AfterKills = int.TryParse(this.tbGetAfterF.Text, out times) ? times : 1,
					BlankFirst = cbBlankFirstF.Checked
				};

				this.AddCommand(cmd, (Control.ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void lblUP_Click_1(object sender, EventArgs e)
		{
			lblUP.Text = $"{Player.Username} | {Player.Password}";
		}

		private void btnGetMapF_Click(object sender, EventArgs e)
		{
			tbMapF.Text = Player.Map;
			tbCellF.Text = Player.Cell;
			tbPadF.Text = Player.Pad;
		}

		private void btnAllSkill_Click(object sender, EventArgs e)
		{
			for (int i = 1; i <= 4; i++)
			{
				AddSkill(new Skill
				{
					Text = Skill.GetSkillName(i.ToString()),
					Index = i.ToString(),
					Type = Skill.SkillType.Normal
				}, (ModifierKeys & Keys.Control) == Keys.Control);
			}
		}

		private void btnQAddToWhitelist_Click(object sender, EventArgs e)
		{
			int idQuest = Decimal.ToInt32(numQQuestId.Value);
			Player.Quests.Load(idQuest);

			if (chkQRewards.Checked)
			{
				List<String> items = Grabber.GetQuestRewards(idQuest);
				foreach (String item in items)
				{
					if (item.Length > 0)
						lstDrops.Items.Add(item);
				}
			}

			if (chkQRequirements.Checked)
			{
				List<String> items = Grabber.GetQuestRequirment(idQuest);
				foreach (String item in items)
				{
					if (item.Length > 0)
						lstDrops.Items.Add(item);
				}
			}
		}

		private void btnAddWarnMsg_Click(object sender, EventArgs e)
		{
			this.AddCommand(new CmdClientMessage
			{
				Messages = inputMsgClient.Text,
				IsWarning = true
			}, (Control.ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnAddInfoMsg_Click(object sender, EventArgs e)
		{
			this.AddCommand(new CmdClientMessage
			{
				Messages = inputMsgClient.Text,
				IsWarning = false
			}, (Control.ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void btnSetFPS_Click(object sender, EventArgs e)
		{
			Flash.Call("SetFPS", (int)numSetFPS.Value);
		}

		private void btnBSStart_Click(object sender, EventArgs e)
		{

		}

		private void btnBSStop_Click(object sender, EventArgs e)
		{

		}

		private HandlerPrivateJoin handlerPrivateJoin = new HandlerPrivateJoin();

		private void chkPrivateRoom_CheckedChanged(object sender, EventArgs e)
		{
			numPrivateRoom.Enabled = !chkPrivateRoom.Checked;
			if (chkPrivateRoom.Checked)
			{
				handlerPrivateJoin.Room = numPrivateRoom.Text;
				Proxy.Instance.RegisterHandler(handlerPrivateJoin);
			}
			else
			{
				Proxy.Instance.UnregisterHandler(handlerPrivateJoin);
			}
		}

		private void chkPrivateRoom_MouseHover(object sender, EventArgs e)
		{
			ToolTip tooltip = new ToolTip();
			tooltip.SetToolTip(this.chkPrivateRoom, "Auto join to private room.");
		}


		private void chkSaveProgress_CheckedChanged(object sender, EventArgs e)
		{
			numSaveProgress.Enabled = !chkSaveProgress.Checked;
			if (chkSaveProgress.Checked)
			{
				SaveProgressTimer.Interval =  1000 * 60 * (int)numSaveProgress.Value;
				SaveProgressTimer.Elapsed += OnTimedEvent;
				SaveProgressTimer.Start();
				Console.WriteLine($"SaveProgress: Timer start");
			} 
			else
			{
				SaveProgressTimer.Elapsed -= OnTimedEvent;
				SaveProgressTimer.Stop();
				Console.WriteLine("SaveProgress: Stoped");
			}
		}

		private void chkSaveProgress_MouseHover(object sender, EventArgs e) 
		{
			ToolTip tooltip = new ToolTip();
			tooltip.SetToolTip(this.chkSaveProgress, "Just logout every...");
		}


		private static async void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
		{
			if (Player.IsLoggedIn)
			{
                /*Player.MoveToCell("Blank", "Spawn");
				await Task.Delay(3000);
				string username = OptionsManager.LoginUsername != null ? OptionsManager.LoginUsername : Player.Username;
				await Proxy.Instance.SendToServer($"%xt%zm%house%1%{username}%");*/
                Player.Logout();
				LogForm.Instance.AppendDebug($"[{DateTime.Now:HH:mm:ss}] Progress saved.\r\n");
			}
		}

		private async void btnSetSpawn2_Click(object sender, EventArgs e)
		{
			btnSetSpawn2.Enabled = false;
			Player.SetSpawnPoint();
			string Messages = $"Spawnpoint set: {Player.Cell}, {Player.Pad}";
			await Proxy.Instance.SendToClient($"%xt%server%-1%{Messages}%");
			await Task.Delay(500);
			btnSetSpawn2.Enabled = true;
		}

		private void btnFollowCmd_Click(object sender, EventArgs e)
		{
			AddCommand(new CmdFollow
			{
				PlayerName = tbFollowPlayer.Text
			}, (ModifierKeys & Keys.Control) == Keys.Control);
		}

		private void toggleAntiMod(bool enable)
		{
			if (enable)
			{
				Proxy.Instance.ReceivedFromServer += CapturePlayerData;
				chkHidePlayers.Checked = false;
			}
			else
			{
				Proxy.Instance.ReceivedFromServer -= CapturePlayerData;
			}

			btnAMTest.Enabled = !chkAntiMod.Checked;
			chkAMLogout.Enabled = !chkAntiMod.Checked;
			chkAMStopBot.Enabled = !chkAntiMod.Checked;
			chkHidePlayers.Enabled = !chkAntiMod.Checked;
		}

		private void chkAntiMod_MouseHover(object sender, EventArgs e)
		{
			ToolTip tooltip = new ToolTip();
			tooltip.SetToolTip(this.chkAntiMod, "Auto stop and logout when you see any staff/mod.");
		}

		private void btnAMTest_Click(object sender, EventArgs e)
		{
			showLog("Username", "60");
			modAction();
		}

		private string username = "";
		private string accessLevel = "";

		private void CapturePlayerData(Grimoire.Networking.Message message)
		{
			string msg = message.ToString();

			try
			{
				//"cmd": "initUserDatas"
				if (msg.Contains("\"cmd\":\"initUserDatas\""))
				{
					JObject packet = (JObject)JObject.Parse(msg)["b"]["o"];
					JArray datas = (JArray)packet["a"];

					foreach (JObject data in datas)
					{
						JObject _data = (JObject)data["data"];
						string accessLevel = _data.GetValue("intAccessLevel")?.ToString();
						string username = _data.GetValue("strUsername")?.ToString();

						this.accessLevel = accessLevel;
						this.username = username;
						showLog(this.username, this.accessLevel);
                    }
				}

				//"cmd": "initUserData"
				else if (msg.Contains("\"cmd\":\"initUserData\""))
				{
					JObject packet = (JObject)JObject.Parse(msg)["b"]["o"];
					JObject data = (JObject)packet["data"];
					JValue accessLevel = (JValue)data["intAccessLevel"];
					JValue username = (JValue)data["strUsername"];

					this.accessLevel = accessLevel?.ToString();
					this.username = username?.ToString();
					showLog(this.username, this.accessLevel);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("MyMsg: " + msg);
				Console.WriteLine("MyError: " + e.Message);
			}
		}

		private void showLog(string username, string accessLevel)
		{
			int _accessLevel = int.Parse(accessLevel);
			string color, log = "";

			switch (_accessLevel)
			{
				case 100:
				case 30:
					color = "Dark Green";
					break;
				case 40:
					color = "Light Green";
					break;
				case 50:
					color = "Purple";
					break;
				case 60:
					color = "Yellow";
					break;
				default:
					color = "Unknown";
					break;
			}

			if (_accessLevel >= 30)
			{
				string text = $"==> [STAFF  | {_accessLevel}/{color} | {username}] <==";
				string strip = "";
				for (int i = 0; i < text.Length; i++)
					strip += "-";
				log += $"{strip} {Environment.NewLine} {text} {Environment.NewLine} {strip} {Environment.NewLine}";
				modAction();
                LogForm.Instance.AppendDebug(log);
            }

            Console.WriteLine($"AntiMod: {this.username} | {this.accessLevel}");
        }

		private async void modAction()
		{
			if (chkAMStopBot.Checked)
				chkEnable.Checked = false;

			await Task.Delay(500);

			if (chkAMLogout.Checked)
				Player.Logout();
		}

		private void chkAntiCounter_MouseHover(object sender, EventArgs e)
		{
			ToolTip tooltip = new ToolTip();
			tooltip.SetToolTip(this.chkAntiCounter, "Auto cancel target when monster countering attack.");
		}

		private void chkAntiCounter_CheckedChanged(object sender, EventArgs e)
		{
			if (chkDisableAnims.Checked && chkAntiCounter.Checked)
				chkDisableAnims.Checked = false;
			if (chkAntiCounter.Checked)
			{
				Proxy.Instance.ReceivedFromServer += AntiCounterHandler;
			} 
			else
			{
				Proxy.Instance.ReceivedFromServer -= AntiCounterHandler;
			}
		}

		private void AntiCounterHandler(Grimoire.Networking.Message message)
		{
			string msg = message.ToString();

			try
			{
				//"cmd":"aura++","auras":[{"nam":"Counter Attack"
				//prepares a counter attack!!
				string c1 = "\"cmd\":\"aura++\",\"auras\":[{\"nam\":\"Counter Attack\"";
				string c2 = "prepares a counter attack";
				if (msg.Contains(c2))
				{
					Console.WriteLine("Counter Attack: active");
					Player.CancelTarget();
					Player.CancelAutoAttack();
					if (chkEnable.Checked)
					{
						ActiveBotEngine.Configuration.SkipAttack = true;
					}
				}

				//"cmd":"aura--","aura":{"nam":"Counter Attack"
				if (msg.Contains("\"cmd\":\"aura--\",\"aura\":{\"nam\":\"Counter Attack\""))
				{
					Console.WriteLine("Counter Attack: fades");
					ActiveBotEngine.Configuration.SkipAttack = false;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("MyMsg: " + msg);
				Console.WriteLine("MyError: " + e.Message);
			}
		}

		private void chkLoginLock_CheckedChanged(object sender, EventArgs e)
		{
			if (chkLoginLock.Checked) {
				OptionsManager.LoginUsername = tbLoginUsername.Text;
				OptionsManager.LoginPassword = tbLoginPassword.Text;
			}
			else
			{
				OptionsManager.LoginUsername = null;
				OptionsManager.LoginPassword = null;
			}

			tbLoginUsername.Enabled = !chkLoginLock.Checked;
			tbLoginPassword.Enabled = !chkLoginLock.Checked;
		}

		private void FollowHandler(Grimoire.Networking.Message message)
		{
			if (!Player.IsLoggedIn) return;
			string msg = message.ToString();

			//%xt%uotls%-1%surga%strPad:Right,tx:0,strFrame:r2,ty:0%
			if (msg.Contains("%xt%uotls%-1%" + PlayerName + "%strPad"))
			{
				string cell = CmdFollow.getBetweenString(msg, "strFrame:", ",");
				string pad = CmdFollow.getBetweenString(msg, "strPad:", ",");
				Player.MoveToCell(cell, pad);
				Player.SetSpawnPoint();
			}

			//%xt%exitArea%-1%21959%surga%
			if (msg.Contains("%xt%exitArea%") && msg.Contains(PlayerName))
			{
				Player.CancelTarget();
			}
		}

		private void chkUseSkillTargeted_MouseHover(object sender, EventArgs e)
		{
			ToolTip tooltip = new ToolTip();
			tooltip.SetToolTip(this.chkAntiMod, "Use skill when player has target only.");
		}

		private string PlayerName;
        private bool isFollowing = false;
		private async void chkEnableSettings_CheckedChanged(object sender, EventArgs e)
        {
            tbFollowPlayer2.Enabled = !chkEnableSettings.Checked;
            chkFollowOnly.Enabled = !chkEnableSettings.Checked;
            if (chkFollowOnly.Checked && chkEnableSettings.Checked)
			{
                Console.WriteLine("Following.");
                isFollowing = true;
                PlayerName = tbFollowPlayer2.Text;

				Proxy.Instance.ReceivedFromServer += FollowHandler;

				while (chkFollowOnly.Checked && chkEnableSettings.Checked)
				{
					if (!Player.IsLoggedIn)
					{
						await Task.Delay(2000);
						return;
					}
					List<string> mapPlayers = World.PlayersInMap;
					mapPlayers.ConvertAll<string>(a => a.ToLower());
					if (!mapPlayers.Contains(PlayerName))
					{
						Player.MoveToCell("Blank");
						await Task.Delay(3000);
						Player.GoToPlayer(PlayerName);
					}
					await Task.Delay(2000);
				}
			}
			else
            {
                if (isFollowing)
                {
                    isFollowing = false;
                    Console.WriteLine("Stop Following.");
                    Proxy.Instance.ReceivedFromServer -= FollowHandler;
                }
			}
		}
	}
}