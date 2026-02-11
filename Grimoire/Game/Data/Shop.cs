using Grimoire.Networking;
using Grimoire.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Grimoire.Game.Data
{
    public class Shop
    {
        public static Shop Instance = new Shop();

        [JsonProperty("sName")]
        public string Name
        {
            get;
            set;
        }

        [JsonProperty("ShopID")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("items")]
        public List<InventoryItem> Items
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public static bool IsShopLoaded => Flash.Call<bool>("IsShopLoaded", new string[0]);

        public static void BuyItem(string name)
        {
            Flash.Call("BuyItem", name);
        }

        public static void BuyItemQty(string name, int qty)
        {
            Flash.Call("BuyItemQty", new string[] { name, qty.ToString() });
        }

        public static void BuyItemQty(int itemId, int shopItemId, int qty)
        {
            Flash.Call("BuyItemQtyById", new string[] { qty.ToString(), itemId.ToString(), shopItemId.ToString() });
        }


        public static void ResetShopInfo()
        {
            Flash.Call("ResetShopInfo", new string[0]);
        }
        private static int previousShop = 0;
        public static void Load(int id)
        {
            if (previousShop != id)
            {
                ResetShopInfo();
                previousShop = id;
            }
            Flash.Call("LoadShop", id.ToString());
        }

        public static void SellItem(string name)
        {
            Flash.Call("SellItem", name);
        }
        public static void SellItem(string name, int qty = 1)
        {
            var item = Player.Inventory.Items.Find(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (item == null)
                return;

            if (qty <= 0)
                qty += item.Quantity; // 0 = sell whole stack, -1 = Leave 1 stack
            if (qty > 0)
            {
                Proxy.Instance.SendToServer($"%xt%zm%sellItem%{World.RoomId}%{item.Id}%{qty}%");
            }
        }

        public static void LoadHairShop(string id)
        {
            Flash.Call("LoadHairShop", id);
        }

        public static void LoadHairShop(int id)
        {
            Flash.Call("LoadHairShop", id.ToString());
        }

        public static void LoadArmorCustomizer()
        {
            Flash.Call("LoadArmorCustomizer", new string[0]);
        }
    }
}