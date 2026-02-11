using Grimoire.Botting.Commands.Map;
using Grimoire.Game.Data;
using Grimoire.Game;
using Grimoire.Tools;
using System.Threading.Tasks;
using System.Collections.Generic;
using Grimoire.UI;
using System.Web.UI;
using System;
using System.Linq;
using Grimoire.Networking;

namespace Grimoire.Botting.Commands.Combat
{
    class CmdQuestHunt : IBotCommand
    {
        public string Map { get; set; }
        public string Cell { get; set; }
        public string Pad { get; set; }
        public string Monster { get; set; }
        public string ItemName { get; set; }
        public string Quantity { get; set; }
        public int QID { get; set; } = 0;
        public int maxcell { get; set; } = 10;
        public bool BlankFirst { get; set; } = false;

        public async Task Execute(IBotEngine instance)
        {
            string[] items = ItemName.Split(',');
            string[] monsters = Monster.Split(',');
            string[] quantities = Quantity.Split(',');
            bool doQuest = QID != 0;
            if (doQuest)
            {
                if (!Player.Quests.QuestTree.Exists(q => q.Id == QID))
                {
                    Player.Quests.Load(QID);
                    await instance.WaitUntil(() => Player.Quests.QuestTree.Any((Game.Data.Quest q) => q.Id == QID));
                }

                Game.Data.Quest quest = Player.Quests.Quest(QID);
                int progress = Player.Quests.progress(quest.Id);
                //int.Parse(Flash.CallGameFunction2("world.getQuestValue", quest.ISlot));
                if (progress >= quest.IValue)
                    return;
                if (!quest.IsInProgress)
                {
                    quest.Accept();
                    await instance.WaitUntil(() => !Player.Quests.IsInProgress(QID), timeout: 1);
                }
                var reqs = quest.RequiredItems;
                if (items.Length < reqs.Count)
                    Array.Resize(ref items, reqs.Count);
                if (monsters.Length < reqs.Count)
                    Array.Resize(ref monsters, reqs.Count);
                for (int i = 0; i < reqs.Count && instance.IsRunning; i++)
                {
                    string name = reqs[i].Name;
                    string qty = reqs[i].Quantity.ToString();

                    if (items[i] == null)
                        items[i] = "";

                    LogForm.Instance.devDebug($"Name = {name} | Qty = {qty}");
                    if (!Player.Map.Equals(Map.Split('-')[0].ToLower()))
                        await joinmap(Map, instance);
                    if (int.TryParse(items[i], out int mapitemid))
                        await getMap(mapitemid, qty);
                    else
                        await hunt(name, qty, monsters[i] ?? "*", instance);
                }
                if (quest.CanComplete)
                    quest.Complete();
                await Task.Delay(600);
                return;
            }
            for (int i = 0; i < items.Length && instance.IsRunning; i++)
            {
                string qty = i < quantities.Length ? quantities[i] : "1";
                string monster = i < monsters.Length ? monsters[i] : "*";

                if (itemCollected(items[i], quantities[i]))
                    continue;

                if (!Player.Map.Equals(Map.Split('-')[0].ToLower()))
                    await joinmap(Map, instance);

                if (int.TryParse(items[i], out int mapitemid))
                    await getMap(mapitemid, qty);
                else
                    await hunt(items[i], qty, monster, instance);

                await Task.Delay(600);
            }
        }
        async Task hunt(string item, string qty, string monster, IBotEngine instance)
        {
            List<string> targetCell = GetMonsterCells(monster);
            int _maxcell;
            if (targetCell.Count >= maxcell)
                _maxcell = maxcell;
            else
                _maxcell = targetCell.Count;
            CmdKill kill = new CmdKill
            {
                Monster = monster,
            };
            if (targetCell.Count > 0)
            {
                int i = 0;
                while (!itemCollected(item, qty) && instance.IsRunning)
                {
                    if (World.IsMonsterAvailable(monster))
                    {
                        Player.AttackMonster(monster);
                        await kill.Execute(instance);
                        continue;
                    }
                    if (Player.Cell != targetCell[i])
                    {
                        Player.MoveToCell(targetCell[i], "Left");
                        LogForm.Instance.devDebug($"Cell : {targetCell[i]} [{i + 1}/{_maxcell}]");
                    }
                    await instance.WaitUntil(() => World.IsMonsterAvailable(monster), interval: 50);
                    if (++i >= _maxcell)
                        i = 0;
                }
            }
        }

        async Task getMap(int mapitemid, string sqty)
        {
            Player.MoveToCell("Cut1", "Left");
            int qty = int.Parse(sqty);

            for (int i = -1; i < qty; i++) //extra 1 attempt for getting map anticipating failure
            {
                await Proxy.Instance.SendToServer($"%xt%zm%getMapItem%1%{mapitemid}%");
                await Task.Delay(600);

                if (itemCollected(Player.recentMapItem[mapitemid], sqty))
                    break;
                if (!Player.recentMapItem[mapitemid].Equals("blank"))
                    continue;
            }
        }

        bool itemCollected(string id, string qty)
        {
            return Player.TempInventory.ContainsItem(id, qty) || Player.Inventory.ContainsItem(id, qty);
        }

        List<string> GetMonsterCells(string monsterName)
        {
            List<Monster> monMap = World.GetAllMonsters();

            if (monsterName == "*")
            {
                //Collect all monster, then filtered most within a cell
                return monMap
                    .Where(m => !string.IsNullOrEmpty(m.cell))
                    .GroupBy(m => m.cell, StringComparer.OrdinalIgnoreCase)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .ToList();
            }

            //Collect all monster with that has the name
            List<string> targetCells = monMap
                .Where(m => m.Name.IndexOf(monsterName, StringComparison.OrdinalIgnoreCase) >= 0) //&& m.IsAlive)
                .GroupBy(m => m.cell, StringComparer.OrdinalIgnoreCase)
                .OrderByDescending(g => g.Count()) // cell with most monster 
                .Select(g => g.Key)
                .ToList();
            return targetCells;
        }

        async Task joinmap(string map, IBotEngine instance)
        {
            CmdJoin join = new CmdJoin
            {
                Map = map,
                Cell = Cell,
                Pad = Pad
            };
            while (!Player.Map.Equals(map.Split('-')[0]) && instance.IsRunning)
            {
                if (BlankFirst)
                {
                    string[] safeCell = ClientConfig.GetValue(ClientConfig.C_SAFE_CELL).Split(',');
                    Player.MoveToCell(safeCell[0], safeCell[1]);
                    await instance.WaitUntil(() => Player.CurrentState != Player.State.InCombat, timeout: 3);
                    await Task.Delay(1000);
                }
                await join.Execute(instance);
                await Task.Delay(1500);
            }
            if (!Player.Cell.Equals(Cell)) Player.MoveToCell(Cell, Pad);
            //await Task.Delay(1000);
        }

        public override string ToString()
        {
            string[] items = ItemName.Split(',');
            string[] quantities = Quantity.Split(',');
            string[] monsters = Monster.Split(',');
            //string parts = "";
            List<string> _parts = new List<string>();
            if (QID != 0)
            {
                int _maxlen = Math.Max(items.Length, monsters.Length);
                for (int i = 0; i < _maxlen; i++)
                {
                    string item = (i < items.Length) ? items[i].Trim() : "";
                    string mob = (i < monsters.Length) ? monsters[i].Trim() : "";

                    if (string.IsNullOrEmpty(item) && string.IsNullOrEmpty(mob))
                    {
                        _parts.Add("Blank");
                        continue;
                    }

                    if (!string.IsNullOrEmpty(item))
                        _parts.Add(item);
                    else if (!string.IsNullOrEmpty(mob))
                        _parts.Add(mob);
                }
                return $"Quest-{QID} items:{string.Join(" | ", _parts)}";
            }

            for (int i = 0; i < items.Length; i++)
            {
                //parts += $"{i + 1}-{items[i]} x{quantities[i]} [{monsters[i]}]";
                _parts.Add($"{i + 1}-{items[i]} x{quantities[i]} [{monsters[i]}]");
            }
            return $"Hunt : {string.Join(" | ", _parts)}";
        }

    }
}
