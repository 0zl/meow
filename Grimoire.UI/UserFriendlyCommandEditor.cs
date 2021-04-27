using DarkUI.Controls;
using DarkUI.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Grimoire.UI
{
    public class UserFriendlyCommandEditor : DarkForm
    {
        private IContainer components;
        private DarkButton btnOK;
        private ToolStripContainer toolStripContainer1;
        private SplitContainer splitContainer1;
        private DarkButton btnRawCommand;
        private DarkButton btnCancel;

        private static object cmdObj
        {
            get;
            set;
        }
        
        private static UserFriendlyCommandEditor commandEditor
        {
            get;
            set;
        }

        public static string cmd
        {
            get;
            set;
        }

        private UserFriendlyCommandEditor()
        {
            InitializeComponent();
        }

        private void RawCommandEditor_Load(object sender, EventArgs e)
        {
           
        }

        private void txtCmd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    btnOK.PerformClick();
                    break;

                case Keys.Escape:
                    btnCancel.PerformClick();
                    break;
            }
        }

        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        };

        public static string Show(object obj)
        {
            cmdObj = obj;
            JObject content = JObject.Parse(JsonConvert.SerializeObject(obj));
            using (commandEditor = new UserFriendlyCommandEditor())
            {
                int currentY = 13;
                int count = 0;
                string[] skip = { "Tag", "Text", "Description1", "Description2" };
                Dictionary<string, KeyValuePair<DarkLabel, DarkTextBox>> currentVars = new Dictionary<string, KeyValuePair<DarkLabel, DarkTextBox>>();
                foreach (KeyValuePair<string, JToken> item in content)
                {
                    if (Array.IndexOf(skip, item.Key) == -1 && !string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        currentVars.Add(item.Key, new KeyValuePair<DarkLabel, DarkTextBox>(
                            new DarkLabel()
                            {
                                Name = $"lbl{item.Key}{count}",
                                Text = item.Key,
                                Size = new System.Drawing.Size(70, 20),
                                Location = new System.Drawing.Point(13, currentY),
                                Anchor = AnchorStyles.Left | AnchorStyles.Top
                            },
                            new DarkTextBox()
                            {
                                Name = $"tb{item.Key}{count}",
                                Text = item.Value.ToString(),
                                Size = new System.Drawing.Size(180, 20),
                                Location = new System.Drawing.Point(105, currentY),
                                Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left
                            }));
                        commandEditor.Controls.Add(currentVars[item.Key].Key);
                        commandEditor.Controls.Add(currentVars[item.Key].Value);
                        count++;
                        commandEditor.Size = new Size(commandEditor.Size.Width, commandEditor.Size.Height + currentY);
                        currentY += 30;
                    }
                    //honestly i have no fucking idea how to implement this properly
                }
                DialogResult results = commandEditor.ShowDialog();
                bool dialog = results == DialogResult.OK;
                bool dialog2 = results == DialogResult.Abort;
                if (dialog)
                {
                    foreach (KeyValuePair<string, JToken> item in content)
                    {
                        if (currentVars.ContainsKey(item.Key))
                        {
                            content[item.Key] = currentVars[item.Key].Value.Text;
                        }
                    }
                    var serialized = JsonConvert.SerializeObject(content, Formatting.Indented, _serializerSettings);
                    return serialized;
                }
                else if (dialog2)
                    return RawCommandEditor.Show(JsonConvert.SerializeObject(cmdObj, Formatting.Indented, _serializerSettings));
                else return null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserFriendlyCommandEditor));
            this.btnOK = new DarkUI.Controls.DarkButton();
            this.btnCancel = new DarkUI.Controls.DarkButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnRawCommand = new DarkUI.Controls.DarkButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Checked = false;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(0, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(137, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.Checked = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 46);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnCancel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Size = new System.Drawing.Size(282, 23);
            this.splitContainer1.SplitterDistance = 141;
            this.splitContainer1.TabIndex = 2;
            // 
            // btnRawCommand
            // 
            this.btnRawCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRawCommand.Checked = false;
            this.btnRawCommand.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnRawCommand.Location = new System.Drawing.Point(12, 17);
            this.btnRawCommand.Name = "btnRawCommand";
            this.btnRawCommand.Size = new System.Drawing.Size(282, 23);
            this.btnRawCommand.TabIndex = 3;
            this.btnRawCommand.Text = "Raw Command Editor";
            // 
            // UserFriendlyCommandEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 81);
            this.Controls.Add(this.btnRawCommand);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserFriendlyCommandEditor";
            this.Text = "Command Editor";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.RawCommandEditor_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}