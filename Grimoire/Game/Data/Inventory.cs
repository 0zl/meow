using Grimoire.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grimoire.Game.Data
{
    public class Inventory
    {
        public List<InventoryItem> Items => Flash.Call<List<InventoryItem>>("GetInventoryItems", new string[0]);

        public InventoryItem GetItemByName(string name)
        {
            return Flash.Call<InventoryItem>("GetInventoryItemByName", name);
        }

        public InventoryItem GetItem(string name)=> Flash.Call<InventoryItem>("GetInventoryItemByName", name);

        public int MaxSlots => Flash.Call<int>("InventorySlots", new string[0]);

        public int UsedSlots => Flash.Call<int>("UsedInventorySlots", new string[0]);

        public int AvailableSlots => MaxSlots - UsedSlots;

        public bool ContainsItemX(string name, string quantity)
        {
            InventoryItem inventoryItem = Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
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
            InventoryItem item = Player.Inventory.GetItemByName(itemName);
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

        public bool ContainsMaxItem(string name)
        {
            InventoryItem inventoryItem = Items.FirstOrDefault((InventoryItem i) => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (inventoryItem != null)
            {
                return inventoryItem.Quantity >= inventoryItem.MaxStack;
            }
            return false;
        }
    }
}
