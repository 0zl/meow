using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Networking
{
	public class GrimoireClient
	{
		public event Action Disconnected;

		public event Action<string> MessageReceived;

		private const int BufferSize = 1024;

		private readonly TcpClient _client;

		private readonly byte[] _readBuffer;

		private List<byte> _spillBuffer;

		private bool _connected;

		private readonly object _disconnectLock;

		public GrimoireClient(TcpClient client)
		{
			this._readBuffer = new byte[1024];
			this._spillBuffer = new List<byte>();
			this._disconnectLock = new object();
			this._client = client;
			this._connected = true;
		}

		public GrimoireClient(string address, int port)
		{
			this._readBuffer = new byte[1024];
			this._spillBuffer = new List<byte>();
			this._disconnectLock = new object();
			this._client = new TcpClient();
			try
			{
				this._client.Connect(IPAddress.Parse(address), port);
			}
			catch (Exception)
			{
				this._client.Connect(Dns.GetHostAddresses(address)[0], port);
			}
			this._connected = true;
		}

		public void Start()
		{
			this.Read();
		}

		private void Read()
		{
			try
			{
				this._client.GetStream().BeginRead(this._readBuffer, 0, 1024, new AsyncCallback(this.OnRead), null);
			}
			catch
			{
				this.Disconnect();
			}
		}

		public void Write(byte[] message)
		{
			try
			{
				this._client.GetStream().BeginWrite(message, 0, message.Length, new AsyncCallback(this.OnWrite), null);
			}
			catch
			{
				this.Disconnect();
			}
		}

		public void Write(string message)
		{
			if (message != null && message.Length > 0)
			{
				if (message[message.Length - 1] != '\0')
				{
					message += "\0";
				}
				this.Write(Encoding.UTF8.GetBytes(message));
			}
		}

		public async Task WriteTask(byte[] message)
		{
			try
			{
				await this._client.GetStream().WriteAsync(message, 0, message.Length);
			}
			catch
			{
				this.Disconnect();
			}
		}

		public async Task WriteTask(string message)
		{
			string text = message;
			if (text != null && text.Length > 0)
			{
				if (message[message.Length - 1] != '\0')
				{
					message += "\0";
				}
				await this.WriteTask(Encoding.UTF8.GetBytes(message));
			}
		}

		public void Disconnect()
		{
			object disconnectLock = this._disconnectLock;
			lock (disconnectLock)
			{
				if (this._connected)
				{
					try
					{
						this._client.Close();
					}
					finally
					{
						this._connected = false;
						Action disconnected = this.Disconnected;
						if (disconnected != null)
						{
							disconnected();
						}
					}
				}
			}
		}

		private void OnRead(IAsyncResult result)
		{
			try
			{
				int num = this._client.GetStream().EndRead(result);
				if (num == 0)
				{
					this.Disconnect();
				}
				else
				{
					int num2 = 0;
					while (--num >= 0)
					{
						byte b = this._readBuffer[num2++];
						if (b != 0)
						{
							this._spillBuffer.Add(b);
						}
						else
						{
							string @string = Encoding.UTF8.GetString(this._spillBuffer.ToArray());
							this._spillBuffer = new List<byte>();
							Action<string> messageReceived = this.MessageReceived;
							if (messageReceived != null)
							{
								messageReceived(@string);
							}
						}
					}
					this.Read();
				}
			}
			catch
			{
				this.Disconnect();
			}
		}

		private void OnWrite(IAsyncResult result)
		{
			try
			{
				this._client.GetStream().EndWrite(result);
			}
			catch
			{
				this.Disconnect();
			}
		}
	}
}
