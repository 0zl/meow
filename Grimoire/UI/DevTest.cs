using DarkUI.Forms;
using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.UI
{
	public partial class DevTest : DarkForm
	{
		public DevTest()
		{
			InitializeComponent();
		}
		public static DevTest Instance
		{
			get;
		} = new DevTest();

		private void DevTest_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}

		private void chkArgs_CheckedChanged(object sender, EventArgs e)
		{
			tbArgs.Enabled = chkArgs.Checked;
		}

		private void btnCall_Click_1(object sender, EventArgs e)
		{
			string[] args = chkArgs.Checked ? (tbArgs.Text.Contains(',') ? tbArgs.Text.Split(',') : new string[] { tbArgs.Text }) : new string[0];
			if (radGrimoire.Checked)
			{
				string res = Flash.Call2(tbGameFunction.Text, args);
				tbLogs.Text = res;
			}
			if (radGame.Checked)
			{
				string res = Flash.CallGameFunction(tbGameFunction.Text, tbArgs.Text.Split(','));
				tbLogs.Text = res;
			}
		}
	}
}
