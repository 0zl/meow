using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grimoire.Game.Data
{
    public class Bank
    {
        public List<InventoryItem> Items => Flash.Call<List<InventoryItem>>("GetBankItems", new string[0]);

        public InventoryItem GetItemByName(string name)
        {
            return Flash.Call<InventoryItem>("GetBankItemByName", name);
        }

        public int AvailableSlots => TotalSlots - UsedSlots;

        public int UsedSlots => Flash.Call<int>("UsedBankSlots", new string[0]);

        public int TotalSlots => Flash.Call<int>("BankSlots", new string[0]);

        public void TransferToBank(string itemName) => Flash.Call("TransferToBank", itemName);

        public void TransferFromBank(string itemName) => Flash.Call("TransferToInventory", itemName);

        public void Swap(string invItemName, string bankItemName) => Flash.Call("BankSwap", invItemName, bankItemName);

        public bool ContainsItemX(string itemName, string quantity = "*")
        {
            InventoryItem inventoryItem = Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (inventoryItem != null)
            {
                if (!(quantity == "*"))
                {
                    return inventoryItem.Quantity >= int.Parse(quantity);
                }
                return true;
            }
            return false;
        }

        public bool ContainsItem(string itemName, string quantity = "*")
        {
            InventoryItem item = Player.Bank.GetItemByName(itemName);
            if (item == null)
            {
                return false;
            }
            else
            {
                if (Int32.TryParse(quantity, out int qty))
                    if (item.Quantity < qty)
                        return false;
            }
            return true;
        }

        public void Show()
        {
            Flash.Call("ShowBank", new string[0]);
        }

        public void LoadItems()
        {
            Flash.Call("LoadBankItems", new string[0]);
        }

        public void GetBank()
        {
            Flash.Call("GetBank", new string[0]);
        }
    }
}