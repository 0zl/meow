using DarkUI.Forms;
using Grimoire.Game;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Grimoire.UI
{
	public partial class PvP : DarkForm
	{
		public PvP()
		{
			InitializeComponent();
		}

		public static PvP Instance
		{
			get;
		} = new PvP();

		private void PvP_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}

		private void btnGetCD_Click(object sender, EventArgs e)
		{
			int index = (int)numCDIndex.Value;
			string cooldown = Player.GetSkillCooldown(index.ToString());
			numCDValue.Value = Convert.ToInt32(cooldown);
		}

		private void btnSetCD_Click(object sender, EventArgs e)
		{
			if (!Player.IsLoggedIn) return;
			int index = (int)numCDIndex.Value;
			int value = (int)numCDValue.Value;
			Player.SetSkillCooldown(index.ToString(), value.ToString());
		}

		private void btnGetRange_Click(object sender, EventArgs e)
		{

		}

		private void btnSetRange_Click(object sender, EventArgs e)
		{
			if (!Player.IsLoggedIn) return;
			int index = (int)numRangeIndex.Value;
			int value = (int)numRangeValue.Value;
			Player.SetSkillRange(index.ToString(), value.ToString());
		}

		private void chkEnableAutoTarget_CheckedChanged(object sender, EventArgs e)
		{
			dropPlayers.Enabled = !chkEnableAutoTarget.Checked;
		}

		private void btnGetPlayers_Click(object sender, EventArgs e)
		{
		}

		private void dropPlayers_Click(object sender, EventArgs e)
		{
			if (!Player.IsLoggedIn) return;
			dropPlayers.Items.Clear();
			foreach (string p in World.PlayersInMap)
			{
				if (p.Length > 0)
					dropPlayers.Items.Add(p);
			}
		}

		public void SetTargetPlayer()
		{
			if (!Player.IsLoggedIn) return;
			Player.SetTargetPvP(chkEnableAutoTarget.Checked ? dropPlayers.SelectedItem.ToString() : null);
		}
	}
}
