# meow

`meow` (internally referred to as **Grimoire**) is a sophisticated automation bot designed for a Flash-based game. It provides a comprehensive engine for executing complex sequences of game actions, ranging from automated combat and questing to inventory and bank management.

## Features

### 🤖 Botting Engine
- **Sequence Execution**: Run a list of commands in order with support for delays and conditional jumps.
- **Auto-Relogin**: Automatic reconnection and state restoration upon disconnection.
- **Stat Management**: Automated resting when Health (HP) or Mana (MP) falls below configured thresholds.
- **Boost Management**: Automatically applies active boosts from the inventory.
- **Death Handling**: Configurable behavior upon character death (restart sequence or resume).

### ⚔️ Combat Automation
- **Targeting**: Attack specific monsters or use wildcards.
- **Skill Rotation**: Support for defined skill sets with customizable delays and "wait for cooldown" logic.
- **Kill-For**: Automated hunting until a specific quantity of items (inventory or temporary) is collected.
- **Follow Mode**: Ability to follow a specific player and kill monsters in their vicinity.
- **Anti-Counter**: Advanced logic to detect "Counter Attack" animations and pause combat to avoid damage.

### 📦 Item & Bank Management
- **Bank Operations**: Transfer items between inventory and bank, swap items, and bulk-banking tools.
- **Shop Integration**: Automated loading of shops, buying items, and "Buy Back" functionality.
- **Equipment**: Safe/Unsafe equipping of items and support for loading equipment sets from JSON files.
- **Looting**: Automated pickup of items based on name, AC status, or a global "pickup all" toggle.

### 📜 Questing
- **Auto-Accept/Complete**: Automatically accepts and completes quests defined in the configuration.
- **Quest Tracking**: Logic to handle quest failures and trigger relogins if a quest becomes stuck.

## Command Reference

The bot uses an `IBotCommand` architecture. Some of the implemented commands include:

| Command | Description |
| :--- | :--- |
| `CmdAttack` | Attacks a specific monster and optionally uses a skill set. |
| `CmdKill` | Kills a specific monster with priority targets and anti-counter support. |
| `CmdKillFor` | Hunts a monster until a specific item count or quest requirement is met. |
| `CmdFollow` | Follows a player and kills monsters. |
| `CmdBankTransfer` | Moves items between the inventory and the bank. |
| `CmdEquip` | Equips a specific item (supports safe/unsafe mode). |
| `CmdBuy` | Opens a shop and purchases a specific item. |
| `CmdRest` | Rests the character to recover HP/MP. |

## Installation & Configuration

### Dependencies
- **.NET Framework** (based on C# source)
- **Fody/Costura** (used for embedding DLLs into a single executable)
- **DarkUI** (used for the user interface)
- **Newtonsoft.Json** (used for configuration and set files)

### Hotkeys
The bot supports customizable hotkeys defined in `hotkeys.json`. Default mappings include:
- `F10`: Show Bot
- `F9`: Show Packet Logger
- `F8`: Show Loaders
- `F7`: Show Fast Travels
- `F6`: Show Logs
- `F1`: Change Launcher Skin
- `F2`: Load Hair shop
- `F3`: Load Armor Customizer
