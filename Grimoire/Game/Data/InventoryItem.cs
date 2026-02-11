using Grimoire.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;

namespace Grimoire.Game.Data
{
    public class InventoryItem
    {
        private string _name;

        public static readonly string[] EquippableCategories = new string[14]
        {
            "Sword",
            "Axe",
            "Dagger",
            "Gun",
            "Bow",
            "Mace",
            "Polearm",
            "Staff",
            "Wand",
            "Class",
            "Armor",
            "Helm",
            "Cape",
            "Item"
        };

        public static readonly string[] Weapons = new string[9]
        {
            "Sword",
            "Axe",
            "Dagger",
            "Gun",
            "Bow",
            "Mace",
            "Polearm",
            "Staff",
            "Wand",
        };

        public static readonly string[] EquippableNonWeapon = new string[5]
        {
            "Class",
            "Armor",
            "Helm",
            "Cape",
            "Pet"
        };

        public enum forgeID
        {
            ForgeCape = 71406,
            Lament = 71543,
            Penitence = 71542,
            Vainglory = 70968,
            Absolution = 70969,
            Avarice = 70970,

            ForgeHelm = 71537,
            Hearty = 81467,
            Examen = 71539,
            Anima = 71541,
            Pneuma = 71540,
            Vim = 71538,

            ForgeWeapon = 70753,
            Praxis = 76979,
            Dauntless = 76980,
            Ravenous = 83500,
            Acheron = 71628,
            Elysium = 71629,
            Smite = 70751,
            Valiance = 70752,
            Arcana = 70991,
            Lacerate = 70750
        }
        [JsonProperty("iEnh")]
        public int Enhancement
        {
            get;
            set;
        }

        [JsonIgnore]
        public forgeID? ForgeEnhancement
        {
            get => Enum.IsDefined(typeof(forgeID), Enhancement) ? (forgeID?)Enhancement : null;
            set => Enhancement = value.HasValue ? (int)value.Value : 0;
        }

        [JsonProperty("iQty")]
        public int Quantity
        {
            get;
            set;
        }

        [JsonProperty("sDesc")]
        public string Description
        {
            get;
            set;
        }

        [JsonProperty("iStk")]
        public int MaxStack
        {
            get;
            set;
        }

        [JsonProperty("iLvl")]
        public int Level
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bCoins")]
        public bool IsAcItem
        {
            get;
            set;
        }

        public int CharItemId
        {
            get;
            set;
        }

        [JsonProperty("sLink")]
        public string Link
        {
            get;
            set;
        }

        [JsonProperty("sFile")]
        public string File
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bEquip")]
        public bool IsEquipped
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bUpg")]
        public bool IsMemberOnly
        {
            get;
            set;
        }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("bTemp")]
        public bool IsTemporary
        {
            get;
            set;
        }

        [JsonProperty("iCost")]
        public int Cost
        {
            get;
            set;
        }

        [JsonProperty("sType")]
        public string Category
        {
            get;
            set;
        }

        [JsonProperty("ItemID")]
        public int Id
        {
            get;
            set;
        }

        [JsonProperty("sName")]
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    _name = World.ItemTree.FirstOrDefault((InventoryItem i) => i.Id == Id)?.Name;
                }
                return _name;
            }
            set
            {
                _name = value?.Trim();
            }
        }

        [JsonProperty("ShopItemID")]
        public int ShopItemId
        {
            get;
            set;
        }

        [JsonProperty("iRate")]
        public string DropChance
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Name;
        }

        #region bools
        public bool IsEquippable => EquippableCategories.Contains(Category);

        public bool IsWeapon => Weapons.Contains(Category);

        public bool IsEquippableNonItem => EquippableNonWeapon.Contains(Category);

        public bool ShouldSerializeDescription => false;

        public bool ShouldSerializeMaxStack => false;

        public bool ShouldSerializeLevel => false;

        public bool ShouldSerializeIsAcItem => false;

        public bool ShouldSerializeLink => false;

        public bool ShouldSerializeFile => false;

        public bool ShouldSerializeIsEquipped => false;

        public bool ShouldSerializeIsMemberOnly => false;

        public bool ShouldSerializeIsTemporary() => false;

        public bool ShouldSerializeCost() => false;

        public bool ShouldSerializeCategory() => false;

        public bool ShouldSerializeShopItemId() => false;

        public bool ShouldSerializeDropChance() => false;
        #endregion
    }
}
