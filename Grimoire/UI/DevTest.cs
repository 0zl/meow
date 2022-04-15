using DarkUI.Forms;
using Grimoire.Tools;
using Newtonsoft.Json.Linq;
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

		private void radGetGame_CheckedChanged(object sender, EventArgs e)
		{
			/*if (radGetGame.Checked)
			{
				chkArgs.Checked = false;
				chkArgs.Enabled = false;
			}
			else
			{
				chkArgs.Enabled = true;
			}*/
		}

		private async void btnCall_Click_1(object sender, EventArgs e)
		{
			string[] args = chkArgs.Checked ? (tbArgs.Text.Contains(',') ? tbArgs.Text.Split(',') : new string[] { tbArgs.Text }) : new string[0];
			if (radGrimoire.Checked)
			{
				string res = Flash.Call2(tbGameFunction.Text, args);
				tbLogs.Text = res;
			}
			if (radGame.Checked)
			{
				string res = Flash.CallGameFunction2(tbGameFunction.Text, args);
				tbLogs.Text = res;
			}
			if (radGetGame.Checked)
			{
				if (tbArgs.Text.Length > 0 && chkArgs.Checked)
				{
					object value = tbArgs.Text;
					switch (tbArgs.Text)
					{
						case "True":
						case "true":
							value = true;
							break;
						case "False":
						case "false":
							value = false;
							break;
					}
					btnCall.Enabled = false;
					Flash.SetGameObject(tbGameFunction.Text, value);
					await Task.Delay(1000);
					btnCall.Enabled = true;
				}
				string res = Flash.GetGameObject(tbGameFunction.Text);
				tbLogs.Text = res == null ? "Can't call the function." : res.Length < 1 ? "Function called, but can't process its return." : res;
			}
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			if (tbLogs.Text.Length > 0)
			{
				Clipboard.SetText(tbLogs.Text);
			}
		}

		private void btnFormat_Click(object sender, EventArgs e)
		{
			string text = tbLogs.Text;
			try
			{
				JObject format = (JObject)JObject.Parse(text);
				tbLogs.Text = format.ToString();
			} 
			catch
			{
				//not vaild json
			}
		}

		private void DevTest_Load(object sender, EventArgs e)
		{

		}
	}
}
