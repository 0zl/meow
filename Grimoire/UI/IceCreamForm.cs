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
	public partial class IceCreamForm : Form
	{
		public IceCreamForm()
		{
			InitializeComponent();
			this.WindowState = FormWindowState.Maximized;
			this.MinimumSize = this.Size;
			this.MaximumSize = this.Size;
		}

		private void imgAstolfo_MouseClick(object sender, MouseEventArgs e)
		{
			ShowForm(new IceCreamForm());
		}

		public void ShowForm(Form form)
		{
			form.Show();
			form.BringToFront();
			form.Focus();
		}
	}
}
