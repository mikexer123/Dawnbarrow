using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Dawnbarrow
{
    public partial class Dawnbarrow
    {
        private sealed class GameData
        {
            public List<string> ExploreLoot { get; set; } = new List<string>();
            public List<ShopItemData> ShopItems { get; set; } = new List<ShopItemData>();
        }

        private sealed class ShopItemData
        {
            public string Command { get; set; } = "";
            public string DisplayName { get; set; } = "";
            public int Price { get; set; }
            public string Description { get; set; } = "";
        }

        private string GetGameDataPath()
        {
            string exeDirectory = AppContext.BaseDirectory;
            return Path.Combine(exeDirectory, "Data", "game-data.json");
        }

        private void LoadGameData()
        {
            string dataPath = GetGameDataPath();

            if (File.Exists(dataPath) == false)
            {
                return;
            }

            try
            {
                string json = File.ReadAllText(dataPath);
                GameData? data = JsonSerializer.Deserialize<GameData>(json);

                if (data == null)
                {
                    return;
                }

                if (data.ExploreLoot.Count > 0)
                {
                    exploreItems.Clear();
                    exploreItems.AddRange(data.ExploreLoot.Where(item => string.IsNullOrWhiteSpace(item) == false));
                }

                if (data.ShopItems.Count > 0)
                {
                    shopEntries.Clear();
                    foreach (ShopItemData item in data.ShopItems)
                    {
                        if (string.IsNullOrWhiteSpace(item.Command) || string.IsNullOrWhiteSpace(item.DisplayName) || item.Price <= 0)
                        {
                            continue;
                        }

                        shopEntries.Add(new ShopEntry(item.Command.Trim().ToLowerInvariant(), item.DisplayName.Trim(), item.Price, item.Description.Trim()));
                    }
                }
            }
            catch
            {
                // Keep defaults when data file is missing or malformed.
            }
        }
    }
}
