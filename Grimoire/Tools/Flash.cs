using AxShockwaveFlashObjects;
using Grimoire.Botting;
using Grimoire.Game;
using Grimoire.Game.Data;
using Grimoire.Networking;
using Grimoire.UI;
using Grimoire.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Grimoire.Tools
{
	public delegate void FlashCallHandler(AxShockwaveFlash flash, string function, params object[] args);

	public delegate void FlashErrorHandler(AxShockwaveFlash flash, Exception e, string function, params object[] args);

	public class Flash
	{
		private static Flash _instance;
		public static Flash Instance => _instance ?? (_instance = new Flash());

		public static AxShockwaveFlash flash;

		public static event FlashCallHandler FlashCall;

		public static event FlashErrorHandler FlashError;

		public static event Action<int> SwfLoadProgress;

		/// <summary>
		/// Sends the specified packet to the server.
		/// </summary>
		/// <param name="packet">The packet to be sent.</param>
		/// <param name="type">The type of the packet being sent (String, Json). The default is string.</param>
		/// <remarks>Be careful when using this method. Incorrect use of this method may cause you to be kicked (or banned, although very unlikely).</remarks>
		public static void SendPacket(string packet, string type = "String")
		{
			CallGameFunction2("sfc.send" + type, packet);
		}

		/// <summary>
		/// Sends the specified packet to the client (simulates a response as if the server sent the packet).
		/// </summary>
		/// <param name="packet">The packet to send.</param>
		/// <param name="type">The type of the packet. This can be xml, json or str.</param>
		public static void SendClientPacket(string packet, string type = "str")
		{
			Flash.Call("sendClientPacket", packet, type);
		}

		/// <summary>
		/// Calls the actionscript object with the given name at the given location.
		/// </summary>
		/// <param name="path">The path to the object and its function name.</param>
		/// <param name="args">The arguments to pass to the function.</param>
		/// <returns>The value of the object returned by calling the function as a serialzied JSON string.</returns>
		public static string CallGameFunction2(string path, params object[] args)
		{
			return args.Length > 0 ? Flash.Call("callGameFunction", new object[] { path }.Concat(args).ToArray()) : Flash.Call2("callGameFunction0", path);
		}

		public static void ProcessFlashCall(object sender, _IShockwaveFlashEvents_FlashCallEvent e)
		{
			XElement xElement = XElement.Parse(e.request);
			string text = xElement.Attribute("name")?.Value;
			string text2 = xElement.Element("arguments")?.Value;
			if (text == null)
			{
				return;
			}
			if (!(text == "progress"))
			{
				if (text == "modifyServers")
				{
					Root.Instance.flashPlayer.SetReturnValue("<string>" + ModifyServerList(text2.Trim()) + "</string>");
				}
			}
			else
			{
				SwfLoadProgress?.Invoke(int.Parse(text2));
				if (text2 == "100") Flash.Call("SetTitle", $"Grimlite Li {AboutForm.Instance.getVersion()}");
			}
		}

		public static string GetGameObject(string path)
		{
			return Call2("getGameObject", path);
		}

		public static string GetGameObjectStatic(string path)
		{
			return Call2("getGameObjectS", path);
		}

		public T GetGameObject<T>(string path, T def = default)
		{
			try
			{
				return JsonConvert.DeserializeObject<T>(GetGameObject(path));
			}
			catch
			{
				return def;
			}
		}

		public static string CallGameFunction(string path, params object[] args)
		{
			return args.Length > 0 ? Call("callGameFunction", new object[] { path }.Concat(args).ToArray()) : Call<string>("callGameFunction0", path);
		}

		public void SetGameObject(string path, object value)
		{
			Call("setGameObject", path, value);
		}

		public static string Call2(string function, params object[] args)
		{
			return Call<string>(function, args);
		}

		public static string Call(string function, params object[] args)
		{
			return Call<string>(function, args);
		}

		public static T Call<T>(string function, params object[] args)
		{
			try
			{
				object o = Call(function, typeof(T), args);
				if (o != null)
					return (T)o;
				return default;
			}
			catch
			{
				return default;
			}
		}

		public static object Call(string function, Type type, params object[] args)
		{
			try
			{
				StringBuilder req = new StringBuilder().Append($"<invoke name=\"{function}\" returntype=\"xml\">");
				if (args.Length > 0)
				{
					req.Append("<arguments>");
					args.ForEach(o => req.Append(ToFlashXml(o)));
					req.Append("</arguments>");
				}
				req.Append("</invoke>");
				string result = flash.CallFunction(req.ToString());
				XElement el = XElement.Parse(result);
				return el == null || el.FirstNode == null ? default : Convert.ChangeType(el.FirstNode.ToString(), type);
			}
			catch (Exception e)
			{
				FlashError?.Invoke(flash, e, function, args);
				return default;
			}
		}

		public static T Call<T>(string function, params string[] args)
		{
			return TryDeserialize<T>(GetResponse(BuildRequest(function, args)));
		}

		public static void Call(string function, params string[] args)
		{
			Call<string>(function, args);
		}

		private static string BuildRequest(string method, params string[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<invoke name=\"" + method + "\" returntype=\"xml\">");
			if (args != null && args.Length != 0)
			{
				stringBuilder.Append("<arguments>");
				foreach (string str in args)
				{
					stringBuilder.Append("<string>" + str + "</string>");
				}
				stringBuilder.Append("</arguments>");
			}
			stringBuilder.Append("</invoke>");
			return stringBuilder.ToString();
		}

		private static string GetResponse(string request)
		{
			try
			{
				return HttpUtility.HtmlDecode(XElement.Parse(Root.Instance.flashPlayer.CallFunction(request)).FirstNode?.ToString() ?? string.Empty);
			}
			catch
			{
				return string.Empty;
			}
		}

		private static T TryDeserialize<T>(string str)
		{
			try
			{
				return JsonConvert.DeserializeObject<T>(str);
			}
			catch
			{
				return default;
			}
		}

		public static string ToFlashXml(object o)
		{
			switch (o)
			{
				case null:
					return "<null/>";

				case bool _:
					return $"<{o.ToString().ToLower()}/>";

				case double _:
				case float _:
				case long _:
				case int _:
					return $"<number>{o}</number>";

				case ExpandoObject _:
					StringBuilder sb = new StringBuilder().Append("<object>");
					foreach (KeyValuePair<string, object> kvp in o as IDictionary<string, object>)
						sb.Append($"<property id=\"{kvp.Key}\">{ToFlashXml(kvp.Value)}</property>");
					return sb.Append("</object>").ToString();

				default:
					if (o is Array)
					{
						StringBuilder _sb = new StringBuilder().Append("<array>");
						int k = 0;
						foreach (object el in o as Array)
							_sb.Append($"<property id=\"{k++}\">{ToFlashXml(el)}</property>");
						return _sb.Append("</array>").ToString();
					}
					return $"<string>{SecurityElement.Escape(o.ToString())}</string>";
			}
		}

		private static string ModifyServerList(string response)
		{
			if (response.StartsWith("{\"login\"") && response.EndsWith("]}"))
			{
				return ServersFromJson(response);
			}
			if (response.StartsWith("<login") && response.EndsWith("</login>"))
			{
				return ServersFromXml(response);
			}
			return response;
		}

		private static string ServersFromJson(String json)
		{
			JObject packet = (JObject)JObject.Parse(json);
			JObject login = (JObject)packet["login"];
			JArray servers = (JArray)packet["servers"];
			Server[] array = new Server[servers.Count];

			login["iUpg"] = 10;
			login["iUpgDays"] = 999;

			for (int i = 0; i < servers.Count; i++)
			{
				JObject server = (JObject)servers[i];
				array[i] = new Server
				{
					IsChatRestricted = server.GetValue("iChat")?.ToString() == "0",
					PlayerCount = int.Parse(server.GetValue("iCount")?.ToString()),
					IsMemberOnly = server.GetValue("bUpg")?.ToString() == "1",
					IsOnline = server.GetValue("bOnline")?.ToString() == "1",
					Name = server.GetValue("sName")?.ToString(),
					Port = int.Parse(server.GetValue("iPort")?.ToString())
				};
				server["RealAddress"] = server["sIP"];
				server["RealPort"] = server["iPort"].ToString();
				server["sIP"] = "127.0.0.1";
				server["iPort"] = Proxy.Instance.ListenerPort;
			}
			BotManager.Instance.OnServersLoaded(array);
			return packet.ToString(Newtonsoft.Json.Formatting.None);
		}

		private static string ServersFromXml(String xml)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			XmlElement xmlElement = xmlDocument["login"];

			xmlElement.Attributes["iUpg"].Value = "1";
			xmlElement.Attributes["iUpgDays"].Value = "999";

			Server[] array = new Server[xmlElement.ChildNodes.Count];
			for (int i = 0; i < xmlElement.ChildNodes.Count; i++)
			{
				XmlElement xmlElement2 = (XmlElement)xmlElement.ChildNodes[i];
				XmlAttribute xmlAttribute = xmlElement2.Attributes["sIP"];
				if (xmlAttribute == null)
				{
					return xml;
				}
				XmlAttribute xmlAttribute2 = xmlElement2.Attributes["iPort"];
				xmlElement2.Attributes.Append(xmlDocument.CreateAttribute("RealAddress")).Value = xmlAttribute.Value;
				xmlElement2.Attributes.Append(xmlDocument.CreateAttribute("RealPort")).Value = xmlAttribute2.Value;
				xmlElement2.Attributes["iPort"].Value = Proxy.Instance.ListenerPort.ToString();
				xmlAttribute.Value = "127.0.0.1";
				array[i] = new Server
				{
					IsChatRestricted = xmlElement2.Attributes["iChat"].Value == "0",
					PlayerCount = int.Parse(xmlElement2.Attributes["iCount"].Value),
					IsMemberOnly = xmlElement2.Attributes["bUpg"].Value == "1",
					IsOnline = xmlElement2.Attributes["bOnline"].Value == "1",
					Name = xmlElement2.Attributes["sName"].Value,
					Port = int.Parse(xmlElement2.Attributes["iPort"].Value)
				};
			}
			BotManager.Instance.OnServersLoaded(array);
			return xmlDocument.OuterXml;
		}

		public static object FromFlashXml(XElement el)
		{
			switch (el.Name.ToString())
			{
				case "number":
					return int.TryParse(el.Value, out int i) ? i : float.TryParse(el.Value, out float f) ? f : 0;
				case "true":
					return true;
				case "false":
					return false;
				case "null":
					return null;
				case "array":
					return el.Elements().Select(e => FromFlashXml(e)).ToArray();
				case "object":
					dynamic d = new ExpandoObject();
					el.Elements().ForEach(e => d[e.Attribute("id").Value] = FromFlashXml(e.Elements().First()));
					return d;
				default:
					return el.Value;
			}
		}

		public static void CallHandler(object sender, _IShockwaveFlashEvents_FlashCallEvent e)
		{
			XElement el = XElement.Parse(e.request);
			string name = el.Attribute("name").Value;

			if (name == null)
				return;

			object[] args = el.Elements().Select(x => FromFlashXml(x)).ToArray();

			switch (name)
			{
				case "debug":
					Console.WriteLine("SWFDebug: " + args[0]);
					break;

				case "progress":
					SwfLoadProgress?.Invoke(int.Parse(args[0].ToString()));
					break;

				case "modifyServers":
					Root.Instance.flashPlayer.SetReturnValue("<string>" + ModifyServerList(args[0].ToString().Trim()) + "</string>");
					break;

				case "getServers":
					Console.WriteLine("servers: " + args[0].ToString());
					JObject packet = (JObject)JObject.Parse(args[0].ToString());
					JObject login = (JObject)packet["login"];
					JArray servers = (JArray)packet["servers"];
					Server[] array = new Server[servers.Count];
					for (int i = 0; i < servers.Count; i++)
					{
						JObject server = (JObject)servers[i];
						array[i] = new Server
						{
							IsChatRestricted = server.GetValue("iChat")?.ToString() == "0",
							PlayerCount = int.Parse(server.GetValue("iCount")?.ToString()),
							IsMemberOnly = server.GetValue("bUpg")?.ToString() == "1",
							IsOnline = server.GetValue("bOnline")?.ToString() == "1",
							Name = server.GetValue("sName")?.ToString(),
							Port = int.Parse(server.GetValue("iPort")?.ToString()),
							Ip = server.GetValue("sIP")?.ToString()
						};
						server["sIP"] = server["sIP"];
						server["iPort"] = server["iPort"].ToString();
					}
					BotManager.Instance.OnServersLoaded(array);
					PacketInterceptor.Instance.OnServersLoaded(array);
					break;

				case "getServers2":
					Console.WriteLine("servers2: " + args[0].ToString());
					JArray servers2 = (JArray)JArray.Parse(args[0].ToString());
					Server[] array2 = new Server[servers2.Count];
					for (int i = 0; i < servers2.Count; i++)
					{
						JObject server = (JObject)servers2[i];
						array2[i] = new Server
						{
							IsChatRestricted = server.GetValue("iChat")?.ToString() == "0",
							PlayerCount = int.Parse(server.GetValue("iCount")?.ToString()),
							IsMemberOnly = server.GetValue("bUpg")?.ToString() == "1",
							IsOnline = server.GetValue("bOnline")?.ToString() == "1",
							Name = server.GetValue("sName")?.ToString(),
							Port = int.Parse(server.GetValue("iPort")?.ToString()),
							Ip = server.GetValue("sIP")?.ToString()
						};
						server["sIP"] = server["sIP"];
						server["iPort"] = server["iPort"].ToString();
					}
					BotManager.Instance.OnServersLoaded(array2);
					PacketInterceptor.Instance.OnServersLoaded(array2);
					break;

				case "packetFromClient":
					args[0] = ProcessPacketFromClient((string)args[0]);
					FlashCall?.Invoke(flash, name, args[0]);
					break;

				case "packetFromServer":
					args[0] = ProcessPacketFromServer((string)args[0]);
					FlashCall?.Invoke(flash, name, args[0]);
					break;

				case "pext":
					args[0] = ProcessPext((string)args[0]);
					FlashCall?.Invoke(flash, name, args[0]);
					break;
			}

			//FlashCall?.Invoke(flash, name, args[0]);
		}

		private static string ProcessPacketFromClient(string packet)
		{
			if (packet.Contains(":"))
				packet = packet.Remove(0, packet.IndexOf(':') + 1);
			packet = packet.Trim(new char[] { ' ', '[', ']' });

			switch (packet.Split('%')[3])
			{
				case "moveToCell":
					if (OptionsManager.WalkSpeed != 8)
						OptionsManager.SetWalkSpeed();
					break;

				case "afk":
					if (OptionsManager.AFK && packet.Split('%')[5] == "true" && Root.Instance.chkStartBot.Checked)
					{
						LogForm.Instance.AppendDebug($"[{DateTime.Now:HH:mm:ss}] Logout on AFK.");
						Player.Logout();
					}
					break;
			}
			return packet;
		}

		private static string ProcessPacketFromServer(string packet)
		{
			return packet;
		}

		public static string ProcessPext(string text)
		{
			dynamic packet = JsonConvert.DeserializeObject<dynamic>(text);
			string type = packet["params"].type;
			dynamic data = packet["params"].dataObj;
			if (type == "json")
			{
				switch ((string)data.cmd)
				{
					case "dropItem":
						JObject items = (JObject)data["items"];
						if (items != null)
						{
							InventoryItem item = items.ToObject<Dictionary<int, InventoryItem>>().First().Value;
							World.OnItemDropped(item);
							if (BotManager.Instance.ActiveBotEngine.IsRunning)
							{
								Configuration configuration = BotManager.Instance.ActiveBotEngine.Configuration;
								List<string> whiteList = new List<string>();
								if (configuration.Drops != null)
									whiteList = configuration.Drops;
								if (configuration.EnableRejection && !whiteList.ConvertAll<string>(a => a.ToLower()).Contains(item.Name.ToLower()))
								{
									Call("RejectDrop", new string[2] { item.Name.ToLower(), item.Id.ToString() });
								}
							}
						}
						break;

					case "getQuests":
						JObject quests = (JObject)data["quests"];
						Dictionary<int, Quest> dictionary = quests?.ToObject<Dictionary<int, Quest>>();
						if (dictionary != null && dictionary.Count > 0)
						{
							Player.Quests.OnQuestsLoaded(dictionary.Select((KeyValuePair<int, Quest> q) => q.Value).ToList());
						}
						break;

					case "ccqr":
						var comp = data.ToObject<CompletedQuest>();
						Player.Quests.OnQuestCompleted(comp);
						break;

					case "loadShop":
						JObject shopinfo = (JObject)data["shopinfo"];
						if (shopinfo != null)
						{
							World.OnShopLoaded(shopinfo.ToObject<ShopInfo>());
						}
						break;

					case "sAct":
						if (OptionsManager.InfiniteRange)
							OptionsManager.SetInfiniteRange();
						break;

					case "retrieveUserData":
					case "retrieveUserDatas":
						if (OptionsManager.HidePlayers)
							OptionsManager.DestroyPlayers();
						break;

					default:
						return text;
				}
			}
			if (type == "str")
			{

			}
			return text;
		}
	}
}