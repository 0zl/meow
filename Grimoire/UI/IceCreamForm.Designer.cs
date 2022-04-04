
namespace Grimoire.UI
{
	partial class IceCreamForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IceCreamForm));
			this.imgAstolfo = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.imgAstolfo)).BeginInit();
			this.SuspendLayout();
			// 
			// imgAstolfo
			// 
			this.imgAstolfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imgAstolfo.Image = ((System.Drawing.Image)(resources.GetObject("imgAstolfo.Image")));
			this.imgAstolfo.InitialImage = null;
			this.imgAstolfo.Location = new System.Drawing.Point(0, 0);
			this.imgAstolfo.Name = "imgAstolfo";
			this.imgAstolfo.Size = new System.Drawing.Size(1284, 701);
			this.imgAstolfo.TabIndex = 172;
			this.imgAstolfo.TabStop = false;
			this.imgAstolfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imgAstolfo_MouseClick);
			// 
			// IceCreamForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1284, 701);
			this.ControlBox = false;
			this.Controls.Add(this.imgAstolfo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "IceCreamForm";
			this.Text = "Dig your treasure here !!";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.imgAstolfo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox imgAstolfo;
	}
}