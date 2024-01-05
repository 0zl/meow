using DarkUI.Forms;
using Grimoire.Game;
using Grimoire.Tools;
using Grimoire.Tools.Maid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MaidRemake.WhitelistMap
{
	public partial class WhitelistMapForm : DarkForm
	{
		public static WhitelistMapForm Instance { get; } = new WhitelistMapForm();

		public List<string> getAlternativeMap => getMaps();

		public WhitelistMapForm()
		{
			InitializeComponent();
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}

		private List<string> getMaps()
		{
			Regex rgx = new Regex("[^a-zA-Z0-9\\-\\;\\r\\n]");
			string clearText = rgx.Replace(tbWhitelistMap.Text, String.Empty);
			string[] lines = clearText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			List<string> maps = new List<string>();
			foreach (string line in lines)
			{
				string[] mapInfo = line.Split(';');

				string mapName = mapInfo[0];
				string cellName = "Enter";
				string padName = "Spawn";

				if (mapInfo[0].Contains('-'))
				{
					mapName = mapInfo[0].Split('-')[0];
				}
				try { cellName = mapInfo[1] == String.Empty ? "all" : mapInfo[1]; }
				catch { }
				try { padName = mapInfo[2] == String.Empty ? "all" : mapInfo[2]; }
				catch { }
				maps.Add($"{mapName.ToLower()};{cellName};{padName}");
			}
			return maps;
		}

		private void tbWhitelistMap_TextChanged(object sender, EventArgs e)
		{
			lbCounter.Text = "Number of map(s): " + getMaps().Count.ToString();
		}

		private void lblCheck_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (getMaps().Count <= 0)
				return;

			List<string> maps = getMaps();
			string formattedMap =
				"Unspecified cell/pad will set to Spawn/Enter,\r\n" +
				"The empty line & space will be ignored, etc.\r\n" +
				"(It will goto target when you're in the same map)\r\n\r\n";
			int counter = 1;
			foreach (string map in maps)
			{
				string[] mapInfo = map.Split(';');
				if (mapInfo[1].ToLower() == "all")
				{
					formattedMap += $"({counter}) map:{mapInfo[0]} in all cells\r\n";
				} 
				else
				{
					formattedMap += $"({counter}) map:{mapInfo[0]}, cell:{mapInfo[1]}, pad:{mapInfo[2]}\r\n";
				}
				counter++;
			}
			MessageBoxEx.Show(this, formattedMap, "Parsing Result");
		}

		private void lblClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			tbWhitelistMap.Text = String.Empty;
		}

		private void btnGrabMap_Click(object sender, EventArgs e)
		{
			if (Player.IsLoggedIn && !World.IsMapLoading)
			{
				string getMapInfo = $"{Player.Map}-{Flash.Call<int>("RoomNumber", new string[0])}";
				tbGrabMapResult.Text = $"{getMapInfo};{Player.Cell};{Player.Pad}";
			}
			else
			{
				tbGrabMapResult.Text = String.Empty;
			}
		}

		private void btnAddToList_Click(object sender, EventArgs e)
		{
			if (tbGrabMapResult.Text != String.Empty)
			{
				tbWhitelistMap.Text += tbWhitelistMap.Text == String.Empty ? String.Empty : "\r\n";
				tbWhitelistMap.Text += tbGrabMapResult.Text;
			}
		}
	}
}
