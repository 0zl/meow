using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;
using Grimoire.Game;

namespace Grimoire.UI
{
	public partial class AboutForm : DarkForm
	{
		private readonly string version = "2.3.4";
		private readonly string releaseDate = "24-03-2022";

		public AboutForm()
		{
			InitializeComponent();
			this.lblVersion.Text = $"Version {version} ({releaseDate})";
		}

		private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}

		private void pbFrozttGithub_Click(object sender, EventArgs e)
		{
			Process.Start("https://github.com/dwiki08/meow");
		}

		private void pbCatGithub_Click(object sender, EventArgs e)
		{
			Process.Start("https://github.com/0zl");
		}

		private void pbsatanGithub_Click(object sender, EventArgs e)
		{
			Process.Start("https://github.com/wispie");
		}

		private void pbBineyMPGH_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.mpgh.net/forum/member.php?u=4062680");
		}

		private void pbEmperorMPGH_Click(object sender, EventArgs e)
		{
			Process.Start("https://www.mpgh.net/forum/member.php?u=2374072");
		}

		private void AboutForm_Load(object sender, EventArgs e)
		{
			bool latest = false;
			//int version = version whatever  
			if (latest)
			{
				//lblUpdate = $"Grimlite is up to date ({version})"
			}
			else
			{
				//int latestVersion = latest version or whatever
				//bool latestStatus
				//lblUpdate = $"Latest {latestVersion}, status: {latestStatus ? "Released" : "Unreleased"}"
				
				// I'll handle the Released or Unreleased with color (green = released, red = unreleased)
			}

		}

		private async void btnLoadSWF_Click(object sender, EventArgs e)
		{
			bool fileExist = File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), tbGameSWF.Text));
			if (!fileExist)
			{
				lblSwfInfo.Text = "File not found.";
				return;
			}

			btnLoadSWF.Enabled = false;
			BotManager.Instance.chkEnable.Checked = false;
			if (Player.IsLoggedIn) Player.Logout();
			Root.Instance.InitFlashMovie(tbGameSWF.Text);
			await Task.Delay(2000);
			btnLoadSWF.Enabled = true;
			lblSwfInfo.Text = "";
		}

		//required to checking version in some existing plugins
		public string getVersion()
		{
			return $"Version {version} ({releaseDate.Replace("-", "")})";
		}

		public string getVersionCode()
		{
			return version;
		}

		public string getReleaseDate()
		{
			return releaseDate;
		}

	}
}
