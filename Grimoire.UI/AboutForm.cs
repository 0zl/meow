using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;

namespace Grimoire.UI
{
    public partial class AboutForm : DarkForm
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://paypal.me/wispsatan");
        }

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void pbCatGithub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/0zl");
        }

        private void pbsatanGithub_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/wispie");
        }
    }
}
