using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Dawnbarrow
{
    public partial class Dawnbarrow
    {
        private sealed class ShopEntry
        {
            public ShopEntry(string command, string displayName, int price, string description)
            {
                Command = command;
                DisplayName = displayName;
                Price = price;
                Description = description;
            }

            public string Command { get; }
            public string DisplayName { get; }
            public int Price { get; }
            public string Description { get; }

            public override string ToString()
            {
                return $"{DisplayName} ({Price}g)";
            }
        }

        private readonly List<ShopEntry> shopEntries = new List<ShopEntry>
        {
            new ShopEntry("buy fire bomb", "Fire Bomb", 18, "Strong consumable damage in combat."),
            new ShopEntry("buy heal", "Heal Service", 12, "Restore 20 health instantly."),
            new ShopEntry("buy mana", "Mana Tonic", 10, "Restore 10 mana instantly."),
            new ShopEntry("buy scroll of freezing", "Scroll of Freezing", 20, "Freeze enemies for 3 turns."),
            new ShopEntry("buy scroll of fire", "Scroll of Fire", 20, "Apply strong burn over time."),
            new ShopEntry("buy scroll of flight", "Scroll of Flight", 28, "Fast travel to any map coordinate.")
        };

        private void RefreshShopPanel()
        {
            if (ShopList == null)
            {
                return;
            }

            ShopEntry? selected = ShopList.SelectedItem as ShopEntry;
            string? selectedCommand = selected?.Command;

            ShopList.BeginUpdate();
            ShopList.Items.Clear();
            foreach (ShopEntry entry in shopEntries)
            {
                ShopList.Items.Add(entry);
            }

            if (selectedCommand != null)
            {
                ShopEntry? replacement = shopEntries.FirstOrDefault(entry => entry.Command == selectedCommand);
                if (replacement != null)
                {
                    ShopList.SelectedItem = replacement;
                }
            }

            if (ShopList.SelectedIndex < 0 && ShopList.Items.Count > 0)
            {
                ShopList.SelectedIndex = 0;
            }
            ShopList.EndUpdate();

            bool inShopRoom = IsShopRoom();
            ShopHeader.Text = inShopRoom ? "Shop (Merchant Nearby)" : "Shop";
            ShopHint.Text = inShopRoom ? "Select item and buy" : "Travel to 3,1 to shop";
            ShopBuyButton.Enabled = inShopRoom && Player.isFighting == false && ShopList.SelectedItem is ShopEntry;
        }

        private void ShopList_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool inShopRoom = IsShopRoom();
            ShopBuyButton.Enabled = inShopRoom && Player.isFighting == false && ShopList.SelectedItem is ShopEntry;
        }

        private void ShopList_DoubleClick(object sender, EventArgs e)
        {
            HandleShopBuyAction();
        }

        private void ShopBuyButton_Click(object sender, EventArgs e)
        {
            HandleShopBuyAction();
        }

        private void HandleShopBuyAction()
        {
            ShopEntry? selected = ShopList.SelectedItem as ShopEntry;

            if (selected == null)
            {
                StartTyping("\nSelect an item to buy.", true);
                return;
            }

            string output = HandleBuyCommand(selected.Command);
            Player.calculateArmor();
            updatelabels();
            StartTyping($"\n{output}", true);
        }
    }
}
