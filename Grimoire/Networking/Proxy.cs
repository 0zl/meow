using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Grimoire.Game;
using Grimoire.Networking.Handlers;
using Grimoire.Tools;

namespace Grimoire.Networking
{
	public class Proxy
	{
		public delegate void Receive(Message message);

		public static Proxy Instance { get; } = new Proxy();

		public event Receive ReceivedFromClient;

		public event Receive ReceivedFromServer;

		public int ListenerPort { get; set; }

		private static readonly CancellationTokenSource AppClosingToken = new CancellationTokenSource();

		private Proxy()
		{
			this.ReceivedFromServer += this.ProcessMessage;
			this.ReceivedFromClient += this.ProcessMessage;
		}

		public void RegisterHandler(IJsonMessageHandler handler)
		{
			this._handlersJson.Add(handler);
		}

		public void RegisterHandler(IXmlMessageHandler handler)
		{
			this._handlersXml.Add(handler);
		}

		public void RegisterHandler(IXtMessageHandler handler)
		{
			this._handlersXt.Add(handler);
		}

		public void UnregisterHandler(IJsonMessageHandler handler)
		{
			this._handlersJson.Remove(handler);
		}

		public void UnregisterHandler(IXmlMessageHandler handler)
		{
			this._handlersXml.Remove(handler);
		}

		public void UnregisterHandler(IXtMessageHandler handler)
		{
			this._handlersXt.Remove(handler);
		}

		public void Start()
		{
			this._listener = new TcpListener(IPAddress.Loopback, this.ListenerPort);
			this._listener.Start();
			this._listener.BeginAcceptTcpClient(new AsyncCallback(this.OnClientAccept), null);
		}

		public void Stop(bool appClosing)
		{
			if (this._listener == null) return;
			if (appClosing)
				AppClosingToken.Cancel();
			this._listener.Stop();
			GrimoireClient server = this._server;
			if (server != null)
			{
				server.Disconnect();
			}
			GrimoireClient client = this._client;
			if (client == null)
			{
				return;
			}
			client.Disconnect();
		}

		private void OnClientAccept(IAsyncResult result)
		{
			Console.WriteLine("connecting...");
			if (AppClosingToken.IsCancellationRequested) return;
			if (this._client != null)
			{
				this._client.Disconnected -= this.OnClientDisconnect;
				this._client.MessageReceived -= this.OnClientMessage;
				this._client.Disconnect();
			}
			if (this._server != null)
			{
				this._server.Disconnected -= this.OnServerDisconnect;
				this._server.MessageReceived -= this.OnServerMessage;
				this._server.Disconnect();
			}
			try
			{
				this._client = new GrimoireClient(this._listener.EndAcceptTcpClient(result));
				string address = Flash.Call<string>("RealAddress", new string[0]);
				int port = int.Parse(Flash.Call<string>("RealPort", new string[0]));
				this._server = new GrimoireClient(address, port);
				this._client.Disconnected += this.OnClientDisconnect;
				this._server.Disconnected += this.OnServerDisconnect;
				this._client.MessageReceived += this.OnClientMessage;
				this._server.MessageReceived += this.OnServerMessage;
				this._client.Start();
				this._server.Start();
			}
			finally
			{
				this._listener.BeginAcceptTcpClient(new AsyncCallback(this.OnClientAccept), null);
			}
		}

		private void OnClientDisconnect()
		{
			GrimoireClient server = this._server;
			if (server == null)
			{
				return;
			}
			server.Disconnect();
		}

		private void OnServerDisconnect()
		{
			GrimoireClient client = this._client;
			if (client == null)
			{
				return;
			}
			client.Disconnect();
		}

		private void OnClientMessage(string message)
		{
			if (AppClosingToken.IsCancellationRequested) return;
			Message message2 = this.CreateMessage(message);
			Receive receivedFromClient = this.ReceivedFromClient;
			if (receivedFromClient != null)
			{
				receivedFromClient(message2);
			}
			if (message2.Send)
			{
				this.SendToServer(message2.ToString());
			}
		}

		private void OnServerMessage(string message)
		{
			if (AppClosingToken.IsCancellationRequested) return;
			Message message2 = this.CreateMessage(message);
			Receive receivedFromServer = this.ReceivedFromServer;
			if (receivedFromServer != null)
			{
				receivedFromServer(message2);
			}
			if (message2.Send)
			{
				this.SendToClient(message2.ToString());
			}
		}

		private void ProcessMessage(Message message)
		{
			try
			{
				switch (message)
				{
					case XtMessage xtMessage:
						foreach (IXtMessageHandler item in _handlersXt.Where((IXtMessageHandler h) => h.HandledCommands.Contains(xtMessage.Command)))
						{
							item.Handle(xtMessage);
						}
						break;

					case JsonMessage jsonMessage:
						foreach (IJsonMessageHandler item in _handlersJson.Where((IJsonMessageHandler h) => h.HandledCommands.Contains(jsonMessage.Command)))
						{
							item.Handle(jsonMessage);
						}
						break;

					case XmlMessage xmlMessage:
						foreach (IXmlMessageHandler item in _handlersXml.Where((IXmlMessageHandler h) => h.HandledCommands.Contains(xmlMessage.Command)))
						{
							item.Handle(xmlMessage);
						}
						break;
				}
			}
			catch
			{

			}
		}

		private Message CreateMessage(string raw)
		{
			if (raw != null && raw.Length > 0)
			{
				char c = raw[0];
				if (c == '%')
				{
					return new XtMessage(raw);
				}
				if (c == '<')
				{
					return new XmlMessage(raw);
				}
				if (c == '{')
				{
					return new JsonMessage(raw);
				}
			}
			return null;
		}

		public async Task SendToServer(string data)
		{
			/*string text = data.Replace("{ROOM_ID}", World.RoomId.ToString());
			await this._server.WriteTask(text);*/

			bool json = data.StartsWith("{");
			Flash.SendPacket(data, json ? "Json" : "String");
		}

		public async Task SendToServer(byte[] data)
		{
			await this._server.WriteTask(data);
		}

		public async Task SendToClient(string data)
		{
			//await this._client.WriteTask(data);

			bool json = data.StartsWith("{");
			Flash.SendClientPacket(data, json ? "json" : "str");
		}

		public async Task SendToClient(byte[] data)
		{
			await this._client.WriteTask(data);
		}

		private GrimoireClient _client;

		private GrimoireClient _server;

		private TcpListener _listener;

		private readonly List<IJsonMessageHandler> _handlersJson = new List<IJsonMessageHandler>
		{			
			//new HandlerSkills(),
			//new HandlerDPS(),
			new HandlerDropItem(),
			new HandlerGetQuests(),
			new HandlerQuestComplete(),
			//new HandlerMapJoin(),
			//new HandlerLoadBank(),
			new HandlerLoadShop()
		};

		private readonly List<IXtMessageHandler> _handlersXt = new List<IXtMessageHandler>
		{
			//new HandlerWarningsXt(),
			//new HandlerLogin(),
			//new HandlerAFK(),
			//new HandlerChat(),
			//new HandlerXtJoin(),
			//new HandlerXtCellJoin()
			new HandlerWarnings()
		};

		private readonly List<IXmlMessageHandler> _handlersXml = new List<IXmlMessageHandler>
		{
			new HandlerPolicy()
		};
	}
}
