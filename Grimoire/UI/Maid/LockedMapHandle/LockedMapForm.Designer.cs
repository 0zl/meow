
namespace MaidRemake.LockedMapHandle
{
    partial class LockedMapForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockedMapForm));
			this.tbLockedMapAlternative = new DarkUI.Controls.DarkTextBox();
			this.darkLabel1 = new DarkUI.Controls.DarkLabel();
			this.tbGrabMapResult = new DarkUI.Controls.DarkTextBox();
			this.btnGrabMap = new DarkUI.Controls.DarkButton();
			this.btnAddToList = new DarkUI.Controls.DarkButton();
			this.lbCounter = new DarkUI.Controls.DarkLabel();
			this.lblCheck = new System.Windows.Forms.LinkLabel();
			this.lblClear = new System.Windows.Forms.LinkLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.darkLabel2 = new DarkUI.Controls.DarkLabel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbLockedMapAlternative
			// 
			this.tbLockedMapAlternative.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbLockedMapAlternative.Font = new System.Drawing.Font("Leelawadee UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbLockedMapAlternative.Location = new System.Drawing.Point(12, 48);
			this.tbLockedMapAlternative.Multiline = true;
			this.tbLockedMapAlternative.Name = "tbLockedMapAlternative";
			this.tbLockedMapAlternative.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbLockedMapAlternative.Size = new System.Drawing.Size(210, 139);
			this.tbLockedMapAlternative.TabIndex = 0;
			this.tbLockedMapAlternative.Text = "citadel-6666;m22;Left\r\ntercessuinotlim-6666";
			this.tbLockedMapAlternative.TextChanged += new System.EventHandler(this.tbLockedMapAlternative_TextChanged);
			// 
			// darkLabel1
			// 
			this.darkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel1.Location = new System.Drawing.Point(12, 9);
			this.darkLabel1.Name = "darkLabel1";
			this.darkLabel1.Size = new System.Drawing.Size(210, 17);
			this.darkLabel1.TabIndex = 1;
			this.darkLabel1.Text = "Here\'s the map(s) you\'ll try to join";
			this.darkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbGrabMapResult
			// 
			this.tbGrabMapResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbGrabMapResult.Font = new System.Drawing.Font("Leelawadee UI", 8.25F);
			this.tbGrabMapResult.Location = new System.Drawing.Point(12, 212);
			this.tbGrabMapResult.Name = "tbGrabMapResult";
			this.tbGrabMapResult.Size = new System.Drawing.Size(210, 22);
			this.tbGrabMapResult.TabIndex = 3;
			this.tbGrabMapResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// btnGrabMap
			// 
			this.btnGrabMap.Checked = false;
			this.btnGrabMap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnGrabMap.Location = new System.Drawing.Point(0, 0);
			this.btnGrabMap.Name = "btnGrabMap";
			this.btnGrabMap.Size = new System.Drawing.Size(100, 25);
			this.btnGrabMap.TabIndex = 4;
			this.btnGrabMap.Text = "Get Current Map";
			this.btnGrabMap.Click += new System.EventHandler(this.btnGrabMap_Click);
			// 
			// btnAddToList
			// 
			this.btnAddToList.Checked = false;
			this.btnAddToList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnAddToList.Location = new System.Drawing.Point(0, 0);
			this.btnAddToList.Name = "btnAddToList";
			this.btnAddToList.Size = new System.Drawing.Size(106, 25);
			this.btnAddToList.TabIndex = 5;
			this.btnAddToList.Text = "Add To List";
			this.btnAddToList.Click += new System.EventHandler(this.btnAddToList_Click);
			// 
			// lbCounter
			// 
			this.lbCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbCounter.AutoSize = true;
			this.lbCounter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.lbCounter.Location = new System.Drawing.Point(12, 190);
			this.lbCounter.Name = "lbCounter";
			this.lbCounter.Size = new System.Drawing.Size(102, 13);
			this.lbCounter.TabIndex = 6;
			this.lbCounter.Text = "Number of map(s): 2";
			// 
			// lblCheck
			// 
			this.lblCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCheck.AutoSize = true;
			this.lblCheck.LinkColor = System.Drawing.Color.DeepSkyBlue;
			this.lblCheck.Location = new System.Drawing.Point(144, 190);
			this.lblCheck.Name = "lblCheck";
			this.lblCheck.Size = new System.Drawing.Size(44, 13);
			this.lblCheck.TabIndex = 7;
			this.lblCheck.TabStop = true;
			this.lblCheck.Text = "[Check]";
			this.lblCheck.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCheck_LinkClicked);
			// 
			// lblClear
			// 
			this.lblClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblClear.AutoSize = true;
			this.lblClear.LinkColor = System.Drawing.Color.DeepSkyBlue;
			this.lblClear.Location = new System.Drawing.Point(185, 190);
			this.lblClear.Name = "lblClear";
			this.lblClear.Size = new System.Drawing.Size(37, 13);
			this.lblClear.TabIndex = 8;
			this.lblClear.TabStop = true;
			this.lblClear.Text = "[Clear]";
			this.lblClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblClear_LinkClicked);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(12, 238);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.btnGrabMap);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btnAddToList);
			this.splitContainer1.Size = new System.Drawing.Size(210, 25);
			this.splitContainer1.SplitterDistance = 100;
			this.splitContainer1.TabIndex = 10;
			// 
			// darkLabel2
			// 
			this.darkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.darkLabel2.Location = new System.Drawing.Point(9, 26);
			this.darkLabel2.Name = "darkLabel2";
			this.darkLabel2.Size = new System.Drawing.Size(213, 15);
			this.darkLabel2.TabIndex = 2;
			this.darkLabel2.Text = " when your target is in locked zone";
			this.darkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LockedMapForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(234, 274);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.lblClear);
			this.Controls.Add(this.lblCheck);
			this.Controls.Add(this.lbCounter);
			this.Controls.Add(this.tbGrabMapResult);
			this.Controls.Add(this.darkLabel2);
			this.Controls.Add(this.darkLabel1);
			this.Controls.Add(this.tbLockedMapAlternative);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LockedMapForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Alternative Map(s)";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        public DarkUI.Controls.DarkTextBox tbLockedMapAlternative;
        private DarkUI.Controls.DarkLabel darkLabel1;
        private DarkUI.Controls.DarkTextBox tbGrabMapResult;
        private DarkUI.Controls.DarkButton btnGrabMap;
        private DarkUI.Controls.DarkButton btnAddToList;
        private DarkUI.Controls.DarkLabel lbCounter;
        private System.Windows.Forms.LinkLabel lblCheck;
        private System.Windows.Forms.LinkLabel lblClear;
        private System.Windows.Forms.SplitContainer splitContainer1;
		private DarkUI.Controls.DarkLabel darkLabel2;
	}
}