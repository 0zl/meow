using DarkUI.Controls;
using DarkUI.Forms;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class RawCommandEditor : DarkForm
    {
        private IContainer components;
        private DarkButton btnOK;
        private DarkButton btnCancel;
        private DarkTextBox txtCmd;

        public string Input => txtCmd.Text;

        public string Content
        {
            set
            {
                txtCmd.Text = value;
            }
        }

        public RawCommandEditor()
        {
            InitializeComponent();
        }

        private void RawCommandEditor_Load(object sender, EventArgs e)
        {
            txtCmd.Select();
        }

        private void txtCmd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                /*case Keys.Return:
                    btnOK.PerformClick();
                    break;*/

                case Keys.Escape:
                    btnCancel.PerformClick();
                    break;
            }
        }

        public static string Show(string content)
        {
            using (RawCommandEditor rawCommandEditor = new RawCommandEditor
            {
                Content = content
            })
            {
                return (rawCommandEditor.ShowDialog() == DialogResult.OK) ? rawCommandEditor.Input : null;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RawCommandEditor));
			this.btnOK = new DarkUI.Controls.DarkButton();
			this.btnCancel = new DarkUI.Controls.DarkButton();
			this.txtCmd = new DarkUI.Controls.DarkTextBox();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Checked = false;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(197, 226);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			// 
			// btnCancel
			// 
			this.btnCancel.Checked = false;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(116, 226);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtCmd
			// 
			this.txtCmd.Location = new System.Drawing.Point(12, 12);
			this.txtCmd.Multiline = true;
			this.txtCmd.Name = "txtCmd";
			this.txtCmd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtCmd.Size = new System.Drawing.Size(260, 208);
			this.txtCmd.TabIndex = 2;
			this.txtCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCmd_KeyDown);
			// 
			// RawCommandEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.txtCmd);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "RawCommandEditor";
			this.Text = "Raw Command Editor";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.RawCommandEditor_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		private void btnCancel_Click(object sender, EventArgs e)
		{

		}
	}
}