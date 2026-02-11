using Grimoire.Game.Data;
using Grimoire.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Grimoire.Game
{
    public static class World
    {
        public static void RefreshDictionary() => _players = JsonConvert.DeserializeObject<Dictionary<string, PlayerInfo>>(Flash.Call("Players", new object[0]));

        private static Dictionary<string, PlayerInfo> _players;
        /// <summary>
        /// Gets a list of all players in the current map.
        /// </summary>
        public static List<PlayerInfo> Players => _players.Values.ToList();

        public static List<ShopInfo> LoadedShops;

        public static DropStack DropStack;

        private static readonly Dictionary<LockActions, string> LockedActions;

        public static List<Monster> VisibleMonsters => Flash.Call<List<Monster>>("GetVisibleMonstersInCell", new string[0]);

        public static List<Monster> AvailableMonsters => Flash.Call<List<Monster>>("GetMonstersInCell", Player.Cell);

        public static List<Monster> GetAllMonsters()
        {
            List<Monster> allMonsters = new List<Monster>();
            try
            {
                foreach (string cell in Cells)
                {
                    // Ambil string JSON dari SWF
                    string json = Flash.Call("GetMonstersInCell", new object[] { cell });

                    if (!string.IsNullOrEmpty(json) && json.StartsWith("["))
                    {
                        var monstersInCell = JsonConvert.DeserializeObject<List<Monster>>(json);
                        if (monstersInCell != null && monstersInCell.Count > 0)
                        {
                            //LogForm.Instance.AppendDebug($"Getting monsters on : {cell}");
                            allMonsters.AddRange(monstersInCell);
                        } //else LogForm.Instance.AppendDebug($"[FAILED] Getting monsters on : {cell}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Please wait for map loaded or Login first before grabbing\n\n{ex.Message}",
                    "GrabAllMonsters Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            return allMonsters;
        }

        public static void ReloadMap()
        {
            Flash.Call("ReloadMap", new string[0]);
        }
        public static void LoadMap(string mapSwf)
        {
            Flash.Call("LoadMap", mapSwf);
        }

        public static bool IsMapLoading => !Flash.Call<bool>("MapLoadComplete", new string[0]);

        public static List<string> PlayersInMap => Flash.Call<List<string>>("PlayersInMap", new string[0]);

        public static List<InventoryItem> ItemTree => Flash.Call<List<InventoryItem>>("GetItemTree", new string[0]);

        public static string[] Cells => Flash.Call<string[]>("GetCells", new string[0]);

        /*public static string[] Cells { get {
                string cells = Flash.Call<string>("GetCells", new string[0]);
                return cells.Trim(new Char[] { ' ', '\"', '[', ']' }).Split(','); 
            } }*/

        public static int RoomId => Flash.Call<int>("RoomId", new string[0]);

        public static int RoomNumber => Flash.Call<int>("RoomNumber", new string[0]);

        public static int GetPlayerHealthPercentage(string username) => Flash.Call<int>("GetPlayerHealthPercentage", username);

        public static int GetPlayerHealth(string username) => Flash.Call<int>("GetPlayerHealth", username);

        public static event Action<InventoryItem> ItemDropped;

        public static event Action<ShopInfo> ShopLoaded;

        public static void OnItemDropped(InventoryItem drop)
        {
            Action<InventoryItem> itemDropped = ItemDropped;
            if (itemDropped != null)
            {
                string text = $"{(Player.Inventory.GetItemByName(drop.Name) ?? new InventoryItem()).Quantity}";
                //LogForm.Instance.AppendDrops($"[Item Drop] {drop.Quantity} {drop.Name} at {DateTime.Now:hh:mm:ss tt} [{text}] \r\n");
                itemDropped(drop);
            }

            //ItemDropped?.Invoke(drop);
        }

        public static void OnShopLoaded(ShopInfo shopInfo)
        {
            ShopLoaded?.Invoke(shopInfo);
            if (!LoadedShops.Exists(s => s.Id == shopInfo.Id))
            {
                LoadedShops.Add(shopInfo);
            }
        }

        public static bool IsActionAvailable(LockActions action) => Flash.Call<bool>("IsActionAvailable", LockedActions[action]);

        public static void SetSpawnPoint() => Flash.Call("SetSpawnPoint", new string[0]);

        public static int GetMonsterHealth(string monster) => Flash.Call<int>("GetMonsterHealth", new string[] { monster });

        public static bool IsMonsterAvailable(string name)
        {
            if (name.StartsWith("id'"))
            {
                return Flash.Call<bool>("IsMonsterAvailableByMonMapID", new string[1] { name.Split('\'')[1] });
            }
            else if (name.StartsWith("id."))
            {
                return Flash.Call<bool>("IsMonsterAvailableByMonMapID", new string[1] { name.Split('.')[1] });
            }
            else if (name.StartsWith("id:"))
            {
                return Flash.Call<bool>("IsMonsterAvailableByMonMapID", new string[1] { name.Split(':')[1] });
            }
            else if (name.StartsWith("id-"))
            {
                return Flash.Call<bool>("IsMonsterAvailableByMonMapID", new string[1] { name.Split('-')[1] });
            }
            else
            {
                return Flash.Call<bool>("IsMonsterAvailable", new string[1] { name });
            }
        }

        static World()
        {
            LoadedShops = new List<ShopInfo>();
            DropStack = new DropStack();
            LockedActions = new Dictionary<LockActions, string>(14)
            {
                {
                    LockActions.LoadShop,
                    "loadShop"
                },
                {
                    LockActions.LoadEnhShop,
                    "loadEnhShop"
                },
                {
                    LockActions.LoadHairShop,
                    "loadHairShop"
                },
                {
                    LockActions.EquipItem,
                    "equipItem"
                },
                {
                    LockActions.UnequipItem,
                    "unequipItem"
                },
                {
                    LockActions.BuyItem,
                    "buyItem"
                },
                {
                    LockActions.SellItem,
                    "sellItem"
                },
                {
                    LockActions.GetMapItem,
                    "getMapItem"
                },
                {
                    LockActions.TryQuestComplete,
                    "tryQuestComplete"
                },
                {
                    LockActions.AcceptQuest,
                    "acceptQuest"
                },
                {
                    LockActions.DoIA,
                    "doIA"
                },
                {
                    LockActions.Rest,
                    "rest"
                },
                {
                    LockActions.Who,
                    "who"
                },
                {
                    LockActions.Transfer,
                    "tfer"
                }
            };
        }
    }
}