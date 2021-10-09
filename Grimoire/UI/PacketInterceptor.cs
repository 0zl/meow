using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.GameProxy;
using Grimoire.Networking;
using Grimoire.Tools;

namespace Grimoire.UI
{
	public partial class PacketInterceptor : DarkForm
	{
		public PacketInterceptor()
		{
			InitializeComponent();

            listPackets.KeyUp += ListPackets_KeyUp;
            listPackets.View = View.Details;
            listPackets.Scrollable = true;
            _logger = new LoggerInterceptor(listPackets);
            chkLogPackets.Checked = true;
        }

        public static PacketInterceptor Instance
        {
            get;
        } = new PacketInterceptor();

        private void PacketInterceptor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private LoggerInterceptor _logger;

        public void OnServersLoaded(Server[] servers)
        {
            if (servers != null && servers.Length != 0 && cbServers.Items.Count <= 1)
            {
                cbServers.Items.AddRange(servers);
                cbServers.SelectedIndex = 0;
                Root.Instance.changeServerList.Items.AddRange(servers);
                Root.Instance.changeServerList.SelectedIndex = 0;
            }
        }

        private void ListPackets_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C && listPackets.SelectedItems.Count == 1)
                Clipboard.SetText(((MessageInfo)listPackets.SelectedItems[0].Tag).Content);
        }

        CaptureProxy captureProxy = new CaptureProxy();

        private void btnConnect_Click(object sender, EventArgs e)
		{
			if (captureProxy.Running)
            {
                captureProxy.Stop();
                btnConnect.Text = "Connect";
            }
            else
            {
                Server server = cbServers.SelectedItem as Server;
                if (server != null)
                {
                    IPAddress ip = IPAddress.TryParse(server.Ip, out IPAddress addr) ? addr : Dns.GetHostEntry(server.Ip).AddressList[0];
                    captureProxy.Destination = new IPEndPoint(ip, 5588);
                    captureProxy.Start();
                    Player.Logout();
                    AutoRelogin.Login(Flash.Call<string>("GetUsername", new string[0]), Flash.Call<string>("GetPassword", new string[0]));
                    Flash.Call("ConnectTo", new string[] { "127.0.0.1" });
                    btnConnect.Text = "Disconnect";
                }
            }
        }

		private void lnkClearLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
            listPackets.Items.Clear();
        }

		private void chkLogPackets_CheckedChanged(object sender, EventArgs e)
		{
            if (chkLogPackets.Checked)
                captureProxy.Interceptors.Add(_logger);
            else
                captureProxy.Interceptors.Remove(_logger);
        }

        public class LoggerInterceptor : Interceptor
        {
            public int Priority => int.MaxValue;

            private ListView _host;

            public LoggerInterceptor(ListView host)
            {
                _host = host;
            }

            public void Intercept(MessageInfo info, bool outbound)
            {
                _host.Invoke(new Action(delegate ()
                {
                    ListViewItem item = _host.Items.Add(info.Content);
                    item.Tag = info;
                    item.BackColor = info.Send ? (outbound ? Color.Yellow : Color.CornflowerBlue) : Color.Red;
                    _host.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    item.EnsureVisible();
                }));
            }
        }

		private async void btnSendToClient_Click(object sender, EventArgs e)
		{
            await Proxy.Instance.SendToClient(tbPacketToSend.Text);
        }

		private async void btnSendToServer_Click(object sender, EventArgs e)
		{
            await Proxy.Instance.SendToServer(tbPacketToSend.Text);
        }
	}
}
