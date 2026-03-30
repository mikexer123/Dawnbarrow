using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Dawnbarrow
{
    public partial class Dawnbarrow
    {
        private sealed class InventoryEntry
        {
            public InventoryEntry(string itemName, string category, string? displayName = null)
            {
                ItemName = itemName;
                Category = category;
                DisplayName = string.IsNullOrWhiteSpace(displayName) ? itemName : displayName;
            }

            public string ItemName { get; }
            public string Category { get; }
            public string DisplayName { get; }

            public override string ToString()
            {
                return $"[{Category}] {DisplayName}";
            }
        }

        private readonly List<InventoryEntry> inventoryEntries = new List<InventoryEntry>();

        private List<InventoryEntry> BuildInventoryEntries()
        {
            List<InventoryEntry> entries = new List<InventoryEntry>();

            void AddIfOwned(bool hasItem, string itemName, string category)
            {
                if (hasItem)
                {
                    entries.Add(new InventoryEntry(itemName, category));
                }
            }

            AddIfOwned(Player.hasLadder, "Ladder", "Quest");
            AddIfOwned(Player.hasBossKey, "Boss Key", "Quest");
            AddIfOwned(Player.hasTalkingCat, "Talking Cat", "Quest");
            AddIfOwned(Player.hasPickaxe, "Pickaxe", "Quest");
            AddIfOwned(Player.hasFriendshipBracelet, "Friendship Bracelet", "Quest");

            if (Player.fireBombCount > 0)
            {
                entries.Add(new InventoryEntry("Fire Bomb", "Consumable", $"Fire Bomb x{Player.fireBombCount}"));
            }

            AddIfOwned(Player.hasLeatherHelmet, "Leather Helmet +1", "Helmet");
            AddIfOwned(Player.hasIronHelmet, "Iron Helmet +2", "Helmet");
            AddIfOwned(Player.hasTopazHelmet, "Topaz Helmet +3", "Helmet");
            AddIfOwned(Player.hasSaviorHelmet, "Savior Helmet +4", "Helmet");

            AddIfOwned(Player.hasLeatherChestplate, "Leather Chestplate +1", "Chest");
            AddIfOwned(Player.hasIronChestplate, "Iron Chestplate +2", "Chest");
            AddIfOwned(Player.hasTopazChestplate, "Topaz Chestplate +3", "Chest");
            AddIfOwned(Player.hasSaviorChestplate, "Savior Chestplate +4", "Chest");

            AddIfOwned(Player.hasLeatherLeggings, "Leather Leggings +1", "Legs");
            AddIfOwned(Player.hasIronLeggings, "Iron Leggings +2", "Legs");
            AddIfOwned(Player.hasTopazLeggings, "Topaz Leggings +3", "Legs");
            AddIfOwned(Player.hasSaviorLeggings, "Savior Leggings +4", "Legs");

            AddIfOwned(Player.hasIronSword, "Iron Sword +1", "Weapon");
            AddIfOwned(Player.hasFireSword, "Fire Sword +2", "Weapon");
            AddIfOwned(Player.hasTopazSword, "Topaz Sword +3", "Weapon");
            AddIfOwned(Player.hasSaviorSword, "Savior Sword +4", "Weapon");

            return entries;
        }

        private bool IsEquipped(string itemName)
        {
            return Player.HeadEquipped == itemName
                || Player.ChestEquipped == itemName
                || Player.LegsEquipped == itemName
                || Player.WeaponEquipped == itemName;
        }

        private string GetInventoryActionLabel(InventoryEntry? entry)
        {
            if (entry == null)
            {
                return "Equip / Use";
            }

            if (entry.Category == "Quest")
            {
                return "Inspect";
            }

            if (entry.Category == "Consumable")
            {
                return "Use";
            }

            return IsEquipped(entry.ItemName) ? "Unequip" : "Equip";
        }

        private string HandleInventorySelectionAction()
        {
            InventoryEntry? selectedEntry = InventoryList.SelectedItem as InventoryEntry;

            if (selectedEntry == null)
            {
                return "Select an item from the inventory panel first.";
            }

            if (selectedEntry.Category == "Quest")
            {
                return $"{selectedEntry.ItemName} is a quest item. It works automatically when the room or event needs it.";
            }

            if (selectedEntry.Category == "Consumable")
            {
                string consumableResponse = UseConsumable(selectedEntry.ItemName);
                Player.calculateArmor();
                updatelabels();
                return consumableResponse;
            }

            string command = IsEquipped(selectedEntry.ItemName)
                ? $"take off {selectedEntry.ItemName.ToLower()}"
                : $"equip {selectedEntry.ItemName.ToLower()}";

            string response = checkInput(command);
            Player.calculateArmor();
            updatelabels();
            return response;
        }

        private void RefreshInventoryPanel()
        {
            if (InventoryList == null)
            {
                return;
            }

            InventoryEntry? currentSelection = InventoryList.SelectedItem as InventoryEntry;
            string? selectedItemName = currentSelection?.ItemName;

            inventoryEntries.Clear();
            inventoryEntries.AddRange(BuildInventoryEntries());

            InventoryList.BeginUpdate();
            InventoryList.Items.Clear();

            foreach (InventoryEntry entry in inventoryEntries)
            {
                InventoryList.Items.Add(entry);
            }

            if (selectedItemName != null)
            {
                InventoryEntry? replacement = inventoryEntries.FirstOrDefault(entry => entry.ItemName == selectedItemName);
                if (replacement != null)
                {
                    InventoryList.SelectedItem = replacement;
                }
            }

            if (InventoryList.SelectedIndex < 0 && InventoryList.Items.Count > 0)
            {
                InventoryList.SelectedIndex = 0;
            }

            InventoryList.EndUpdate();
            InventoryHeader.Text = $"Inventory ({inventoryEntries.Count})";
            InventoryHint.Text = inventoryEntries.Count == 0 ? "No items yet" : "Double-click item";
            InventoryActionButton.Text = GetInventoryActionLabel(InventoryList.SelectedItem as InventoryEntry);
            InventoryActionButton.Enabled = inventoryEntries.Count > 0;
        }

        private void InventoryActionButton_Click(object sender, EventArgs e)
        {
            StartTyping($"\n{HandleInventorySelectionAction()}", true);
        }

        private void InventoryList_DoubleClick(object sender, EventArgs e)
        {
            StartTyping($"\n{HandleInventorySelectionAction()}", true);
        }

        private void InventoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            InventoryActionButton.Text = GetInventoryActionLabel(InventoryList.SelectedItem as InventoryEntry);
        }
    }
}
