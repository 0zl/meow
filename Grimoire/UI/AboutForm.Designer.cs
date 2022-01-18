namespace Grimoire.UI
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public static AboutForm Instance = new AboutForm();
      
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblSwfInfo = new DarkUI.Controls.DarkLabel();
			this.btnLoadSWF = new DarkUI.Controls.DarkButton();
			this.tbGameSWF = new DarkUI.Controls.DarkTextBox();
			this.pbFrozttGithub = new System.Windows.Forms.PictureBox();
			this.darkLabel6 = new DarkUI.Controls.DarkLabel();
			this.darkLabel3 = new DarkUI.Controls.DarkLabel();
			this.lblVersion = new DarkUI.Controls.DarkLabel();
			this.rtbCredits = new System.Windows.Forms.RichTextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.pbGrimoireBig = new System.Windows.Forms.PictureBox();
			this.darkLabel5 = new DarkUI.Controls.DarkLabel();
			this.darkLabel4 = new DarkUI.Controls.DarkLabel();
			this.pbCatGithub = new System.Windows.Forms.PictureBox();
			this.pbEmperorMPGH = new System.Windows.Forms.PictureBox();
			this.pbBineyMPGH = new System.Windows.Forms.PictureBox();
			this.pbsatanGithub = new System.Windows.Forms.PictureBox();
			this.lblCat = new DarkUI.Controls.DarkLabel();
			this.lblEmperorAnts = new DarkUI.Controls.DarkLabel();
			this.lblBiney = new DarkUI.Controls.DarkLabel();
			this.lblWisp = new DarkUI.Controls.DarkLabel();
			this.darkLabel1 = new DarkUI.Controls.DarkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbFrozttGithub)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbGrimoireBig)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCatGithub)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbEmperorMPGH)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbBineyMPGH)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbsatanGithub)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblSwfInfo);
			this.panel1.Controls.Add(this.btnLoadSWF);
			this.panel1.Controls.Add(this.tbGameSWF);
			this.panel1.Controls.Add(this.pbFrozttGithub);
			this.panel1.Controls.Add(this.darkLabel6);
			this.panel1.Controls.Add(this.darkLabel3);
			this.panel1.Controls.Add(this.lblVersion);
			this.panel1.Controls.Add(this.rtbCredits);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.darkLabel5);
			this.panel1.Controls.Add(this.darkLabel4);
			this.panel1.Controls.Add(this.pbCatGithub);
			this.panel1.Controls.Add(this.pbEmperorMPGH);
			this.panel1.Controls.Add(this.pbBineyMPGH);
			this.panel1.Controls.Add(this.pbsatanGithub);
			this.panel1.Controls.Add(this.lblCat);
			this.panel1.Controls.Add(this.lblEmperorAnts);
			this.panel1.Controls.Add(this.lblBiney);
			this.panel1.Controls.Add(this.lblWisp);
			this.panel1.Controls.Add(this.darkLabel1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(551, 440);
			this.panel1.TabIndex = 3;
			// 
			// lblSwfInfo
			// 
			this.lblSwfInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSwfInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblSwfInfo.Location = new System.Drawing.Point(421, 369);
			this.lblSwfInfo.Name = "lblSwfInfo";
			this.lblSwfInfo.Size = new System.Drawing.Size(118, 13);
			this.lblSwfInfo.TabIndex = 21;
			this.lblSwfInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnLoadSWF
			// 
			this.btnLoadSWF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadSWF.Checked = false;
			this.btnLoadSWF.Location = new System.Drawing.Point(421, 411);
			this.btnLoadSWF.Name = "btnLoadSWF";
			this.btnLoadSWF.Size = new System.Drawing.Size(118, 22);
			this.btnLoadSWF.TabIndex = 19;
			this.btnLoadSWF.Text = "Load Game SWF";
			this.btnLoadSWF.Click += new System.EventHandler(this.btnLoadSWF_Click);
			// 
			// tbGameSWF
			// 
			this.tbGameSWF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tbGameSWF.Location = new System.Drawing.Point(421, 385);
			this.tbGameSWF.Name = "tbGameSWF";
			this.tbGameSWF.Size = new System.Drawing.Size(118, 20);
			this.tbGameSWF.TabIndex = 18;
			this.tbGameSWF.Text = "Loader/grimoire.swf";
			// 
			// pbFrozttGithub
			// 
			this.pbFrozttGithub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pbFrozttGithub.Image = ((System.Drawing.Image)(resources.GetObject("pbFrozttGithub.Image")));
			this.pbFrozttGithub.InitialImage = null;
			this.pbFrozttGithub.Location = new System.Drawing.Point(21, 115);
			this.pbFrozttGithub.Name = "pbFrozttGithub";
			this.pbFrozttGithub.Size = new System.Drawing.Size(16, 16);
			this.pbFrozttGithub.TabIndex = 17;
			this.pbFrozttGithub.TabStop = false;
			this.pbFrozttGithub.Click += new System.EventHandler(this.pbFrozttGithub_Click);
			// 
			// darkLabel6
			// 
			this.darkLabel6.AutoSize = true;
			this.darkLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel6.Location = new System.Drawing.Point(40, 117);
			this.darkLabel6.Name = "darkLabel6";
			this.darkLabel6.Size = new System.Drawing.Size(45, 13);
			this.darkLabel6.TabIndex = 16;
			this.darkLabel6.Text = "Froztt13";
			// 
			// darkLabel3
			// 
			this.darkLabel3.AutoSize = true;
			this.darkLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel3.Location = new System.Drawing.Point(14, 97);
			this.darkLabel3.Name = "darkLabel3";
			this.darkLabel3.Size = new System.Drawing.Size(104, 13);
			this.darkLabel3.TabIndex = 15;
			this.darkLabel3.Text = "The Grimlite Li Team";
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblVersion.Location = new System.Drawing.Point(16, 36);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(126, 13);
			this.lblVersion.TabIndex = 14;
			this.lblVersion.Text = "version";
			// 
			// rtbCredits
			// 
			this.rtbCredits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtbCredits.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
			this.rtbCredits.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbCredits.ForeColor = System.Drawing.Color.Gainsboro;
			this.rtbCredits.Location = new System.Drawing.Point(16, 307);
			this.rtbCredits.Name = "rtbCredits";
			this.rtbCredits.Size = new System.Drawing.Size(269, 121);
			this.rtbCredits.TabIndex = 13;
			this.rtbCredits.Text = resources.GetString("rtbCredits.Text");
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.pbGrimoireBig);
			this.panel2.Location = new System.Drawing.Point(283, 11);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(256, 256);
			this.panel2.TabIndex = 12;
			// 
			// pbGrimoireBig
			// 
			this.pbGrimoireBig.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbGrimoireBig.Image = global::Properties.Resources.GrimoireIconBig;
			this.pbGrimoireBig.Location = new System.Drawing.Point(0, 0);
			this.pbGrimoireBig.Name = "pbGrimoireBig";
			this.pbGrimoireBig.Size = new System.Drawing.Size(256, 256);
			this.pbGrimoireBig.TabIndex = 0;
			this.pbGrimoireBig.TabStop = false;
			// 
			// darkLabel5
			// 
			this.darkLabel5.AutoSize = true;
			this.darkLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel5.Location = new System.Drawing.Point(14, 215);
			this.darkLabel5.Name = "darkLabel5";
			this.darkLabel5.Size = new System.Drawing.Size(141, 13);
			this.darkLabel5.TabIndex = 11;
			this.darkLabel5.Text = "The Previous Grimoire Team";
			// 
			// darkLabel4
			// 
			this.darkLabel4.AutoSize = true;
			this.darkLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.darkLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel4.Location = new System.Drawing.Point(12, 12);
			this.darkLabel4.Name = "darkLabel4";
			this.darkLabel4.Size = new System.Drawing.Size(92, 24);
			this.darkLabel4.TabIndex = 9;
			this.darkLabel4.Text = "Grimlite Li";
			// 
			// pbCatGithub
			// 
			this.pbCatGithub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pbCatGithub.Image = global::Properties.Resources.GitHub;
			this.pbCatGithub.Location = new System.Drawing.Point(22, 189);
			this.pbCatGithub.Name = "pbCatGithub";
			this.pbCatGithub.Size = new System.Drawing.Size(16, 16);
			this.pbCatGithub.TabIndex = 7;
			this.pbCatGithub.TabStop = false;
			this.pbCatGithub.Click += new System.EventHandler(this.pbCatGithub_Click);
			// 
			// pbEmperorMPGH
			// 
			this.pbEmperorMPGH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pbEmperorMPGH.Image = global::Properties.Resources.MPGHfavicon16;
			this.pbEmperorMPGH.Location = new System.Drawing.Point(22, 256);
			this.pbEmperorMPGH.Name = "pbEmperorMPGH";
			this.pbEmperorMPGH.Size = new System.Drawing.Size(16, 16);
			this.pbEmperorMPGH.TabIndex = 7;
			this.pbEmperorMPGH.TabStop = false;
			this.pbEmperorMPGH.Click += new System.EventHandler(this.pbEmperorMPGH_Click);
			// 
			// pbBineyMPGH
			// 
			this.pbBineyMPGH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pbBineyMPGH.Image = global::Properties.Resources.MPGHfavicon16;
			this.pbBineyMPGH.Location = new System.Drawing.Point(22, 235);
			this.pbBineyMPGH.Name = "pbBineyMPGH";
			this.pbBineyMPGH.Size = new System.Drawing.Size(16, 16);
			this.pbBineyMPGH.TabIndex = 7;
			this.pbBineyMPGH.TabStop = false;
			this.pbBineyMPGH.Click += new System.EventHandler(this.pbBineyMPGH_Click);
			// 
			// pbsatanGithub
			// 
			this.pbsatanGithub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pbsatanGithub.Image = global::Properties.Resources.GitHub;
			this.pbsatanGithub.Location = new System.Drawing.Point(22, 167);
			this.pbsatanGithub.Name = "pbsatanGithub";
			this.pbsatanGithub.Size = new System.Drawing.Size(16, 16);
			this.pbsatanGithub.TabIndex = 7;
			this.pbsatanGithub.TabStop = false;
			this.pbsatanGithub.Click += new System.EventHandler(this.pbsatanGithub_Click);
			// 
			// lblCat
			// 
			this.lblCat.AutoSize = true;
			this.lblCat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblCat.Location = new System.Drawing.Point(40, 190);
			this.lblCat.Name = "lblCat";
			this.lblCat.Size = new System.Drawing.Size(22, 13);
			this.lblCat.TabIndex = 6;
			this.lblCat.Text = "cat";
			// 
			// lblEmperorAnts
			// 
			this.lblEmperorAnts.AutoSize = true;
			this.lblEmperorAnts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblEmperorAnts.Location = new System.Drawing.Point(40, 258);
			this.lblEmperorAnts.Name = "lblEmperorAnts";
			this.lblEmperorAnts.Size = new System.Drawing.Size(64, 13);
			this.lblEmperorAnts.TabIndex = 6;
			this.lblEmperorAnts.Text = "EmperorArts";
			// 
			// lblBiney
			// 
			this.lblBiney.AutoSize = true;
			this.lblBiney.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblBiney.Location = new System.Drawing.Point(40, 237);
			this.lblBiney.Name = "lblBiney";
			this.lblBiney.Size = new System.Drawing.Size(33, 13);
			this.lblBiney.TabIndex = 6;
			this.lblBiney.Text = "Biney";
			// 
			// lblWisp
			// 
			this.lblWisp.AutoSize = true;
			this.lblWisp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lblWisp.Location = new System.Drawing.Point(40, 168);
			this.lblWisp.Name = "lblWisp";
			this.lblWisp.Size = new System.Drawing.Size(59, 13);
			this.lblWisp.TabIndex = 6;
			this.lblWisp.Text = "wisp/satan";
			// 
			// darkLabel1
			// 
			this.darkLabel1.AutoSize = true;
			this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel1.Location = new System.Drawing.Point(14, 146);
			this.darkLabel1.Name = "darkLabel1";
			this.darkLabel1.Size = new System.Drawing.Size(93, 13);
			this.darkLabel1.TabIndex = 5;
			this.darkLabel1.Text = "The Grimlite Team";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.label1.ForeColor = System.Drawing.Color.Gainsboro;
			this.label1.Location = new System.Drawing.Point(34, 146);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 13);
			this.label1.TabIndex = 4;
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(551, 440);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(449, 342);
			this.Name = "AboutForm";
			this.Text = "About";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AboutForm_FormClosing);
			this.Load += new System.EventHandler(this.AboutForm_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbFrozttGithub)).EndInit();
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbGrimoireBig)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCatGithub)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbEmperorMPGH)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbBineyMPGH)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbsatanGithub)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbCatGithub;
        private System.Windows.Forms.PictureBox pbsatanGithub;
        private DarkUI.Controls.DarkLabel lblCat;
        private DarkUI.Controls.DarkLabel lblWisp;
        private DarkUI.Controls.DarkLabel darkLabel1;
        private DarkUI.Controls.DarkLabel darkLabel4;
        private DarkUI.Controls.DarkLabel darkLabel5;
        private DarkUI.Controls.DarkLabel lblEmperorAnts;
        private DarkUI.Controls.DarkLabel lblBiney;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pbGrimoireBig;
        private System.Windows.Forms.RichTextBox rtbCredits;
        private System.Windows.Forms.PictureBox pbEmperorMPGH;
        private System.Windows.Forms.PictureBox pbBineyMPGH;
        private DarkUI.Controls.DarkLabel darkLabel3;
        private DarkUI.Controls.DarkLabel lblVersion;
        private DarkUI.Controls.DarkLabel darkLabel6;
        private System.Windows.Forms.PictureBox pbFrozttGithub;
        private DarkUI.Controls.DarkTextBox tbGameSWF;
        private DarkUI.Controls.DarkButton btnLoadSWF;
        private DarkUI.Controls.DarkLabel lblSwfInfo;
    }
}