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
            public List<RoomEncounterData> RoomEncounters { get; set; } = new List<RoomEncounterData>();
            public List<ExploreEncounterProfileData> ExploreEncounterProfiles { get; set; } = new List<ExploreEncounterProfileData>();
            public List<EnemySkillData> EnemySkills { get; set; } = new List<EnemySkillData>();
            public List<CommandUsageData> CommandUsage { get; set; } = new List<CommandUsageData>();
        }

        private sealed class ShopItemData
        {
            public string Command { get; set; } = "";
            public string DisplayName { get; set; } = "";
            public int Price { get; set; }
            public string Description { get; set; } = "";
        }

        private sealed class RoomEncounterData
        {
            public int X { get; set; }
            public int Y { get; set; }
            public string EnemyName { get; set; } = "";
            public int MinHP { get; set; }
            public int MaxHP { get; set; }
            public int MinArmor { get; set; }
            public int MaxArmor { get; set; }
            public int MinDamage { get; set; }
            public int MaxDamage { get; set; }
            public int MinXp { get; set; }
            public int MaxXp { get; set; }
            public string Description { get; set; } = "";
            public string PlacedObject { get; set; } = "";
            public bool NeedsTalkingCat { get; set; }
            public bool NeedsLadder { get; set; }
            public bool NeedsPickaxe { get; set; }
            public bool NeedsBossKey { get; set; }
            public bool NeedsFriendshipBracelet { get; set; }
        }

        private sealed class ExploreEncounterProfileData
        {
            public List<string> BiomeContains { get; set; } = new List<string>();
            public List<ExploreEnemyOptionData> EnemyOptions { get; set; } = new List<ExploreEnemyOptionData>();
            public int MinHP { get; set; }
            public int MaxHP { get; set; }
            public int MinArmor { get; set; }
            public int MaxArmor { get; set; }
            public int MinDamage { get; set; }
            public int MaxDamage { get; set; }
            public int MinXp { get; set; }
            public int MaxXp { get; set; }
            public string Description { get; set; } = "";
        }

        private sealed class ExploreEnemyOptionData
        {
            public string Name { get; set; } = "";
            public int Weight { get; set; } = 1;
        }

        private sealed class EnemySkillData
        {
            public string EnemyNameContains { get; set; } = "";
            public string SkillName { get; set; } = "";
            public int ChancePercent { get; set; } = 25;
            public int MinDamage { get; set; }
            public int MaxDamage { get; set; }
            public string StatusEffect { get; set; } = "";
            public int StatusTurns { get; set; }
            public int StatusAmount { get; set; }
            public string MessageTemplate { get; set; } = "";
        }

        private sealed class CommandUsageData
        {
            public string Topic { get; set; } = "";
            public string Usage { get; set; } = "";
        }

        private readonly Dictionary<string, RoomEncounterData> roomEncounterData = new Dictionary<string, RoomEncounterData>();
        private readonly List<ExploreEncounterProfileData> exploreEncounterProfiles = new List<ExploreEncounterProfileData>();
        private readonly List<EnemySkillData> enemySkillProfiles = new List<EnemySkillData>();

        private static string BuildEncounterKey(int x, int y)
        {
            return $"{x},{y}";
        }

        private static int ClampMin(int value)
        {
            return Math.Max(0, value);
        }

        private int RollDataRange(int min, int max)
        {
            min = ClampMin(min);
            max = ClampMin(max);

            if (max < min)
            {
                (min, max) = (max, min);
            }

            return exploreRandom.Next(min, max + 1);
        }

        private string SelectWeightedExploreEnemy(List<ExploreEnemyOptionData> options)
        {
            List<ExploreEnemyOptionData> validOptions = options
                .Where(option => string.IsNullOrWhiteSpace(option.Name) == false)
                .ToList();

            if (validOptions.Count == 0)
            {
                return "Wandering Monster";
            }

            int totalWeight = validOptions.Sum(option => Math.Max(1, option.Weight));
            int roll = exploreRandom.Next(1, totalWeight + 1);
            int running = 0;
            foreach (ExploreEnemyOptionData option in validOptions)
            {
                running += Math.Max(1, option.Weight);
                if (roll <= running)
                {
                    return option.Name;
                }
            }

            return validOptions[0].Name;
        }

        private int CalculateEncounterGoldDrop(int hp, int armor, int damage, int xp)
        {
            if (hp <= 0)
            {
                return 0;
            }

            int minimumGold = Math.Max(1, (hp / 5) + armor);
            int maximumGold = Math.Max(minimumGold, minimumGold + Math.Max(1, damage) + (xp / 20));
            return exploreRandom.Next(minimumGold, maximumGold + 1);
        }

        private bool TrySpawnRoomEncounterFromData(int x, int y)
        {
            if (roomEncounterData.TryGetValue(BuildEncounterKey(x, y), out RoomEncounterData? encounter) == false)
            {
                return false;
            }

            enemy.ClearEncounter();
            enemy.isdefeated = false;
            enemy.currentEnemy = encounter.EnemyName;
            enemy.enemyHP = RollDataRange(encounter.MinHP, encounter.MaxHP);
            enemy.enemyCHP = enemy.enemyHP;
            enemy.enemyArmor = RollDataRange(encounter.MinArmor, encounter.MaxArmor);
            enemy.enemyDamage = RollDataRange(encounter.MinDamage, encounter.MaxDamage);
            enemy.enemyxpgiven = RollDataRange(encounter.MinXp, encounter.MaxXp);
            enemy.desc = encounter.Description;
            enemy.placedObject = encounter.PlacedObject;
            enemy.needsTalkingCat = encounter.NeedsTalkingCat;
            enemy.needsLadder = encounter.NeedsLadder;
            enemy.needsPickaxe = encounter.NeedsPickaxe;
            enemy.needsBossKey = encounter.NeedsBossKey;
            enemy.needsFriendshipBracelet = encounter.NeedsFriendshipBracelet;
            enemy.goldDrop = CalculateEncounterGoldDrop(enemy.enemyHP, enemy.enemyArmor, enemy.enemyDamage, enemy.enemyxpgiven);
            return true;
        }

        private void SpawnRoomEncounterWithFallback(int x, int y)
        {
            if (TrySpawnRoomEncounterFromData(x, y))
            {
                return;
            }

            enemy.ClearEncounter();
            enemy.isdefeated = false;
            enemy.currentEnemy = "Wandering Monster";
            enemy.enemyHP = RollDataRange(6, 14);
            enemy.enemyCHP = enemy.enemyHP;
            enemy.enemyArmor = RollDataRange(0, 2);
            enemy.enemyDamage = RollDataRange(1, 4);
            enemy.enemyxpgiven = RollDataRange(5, 16);
            enemy.desc = "You step into an unmapped encounter and draw the attention of a roaming threat.";
            enemy.placedObject = "";
            enemy.goldDrop = CalculateEncounterGoldDrop(enemy.enemyHP, enemy.enemyArmor, enemy.enemyDamage, enemy.enemyxpgiven);
        }

        private bool TrySpawnExploreEncounterFromData(string biome)
        {
            if (exploreEncounterProfiles.Count == 0)
            {
                return false;
            }

            ExploreEncounterProfileData? profile = exploreEncounterProfiles.FirstOrDefault(candidate =>
                candidate.BiomeContains.Any(key => biome.Contains(key, StringComparison.OrdinalIgnoreCase)));

            if (profile == null)
            {
                return false;
            }

            enemy.ClearEncounter();
            enemy.currentEnemy = SelectWeightedExploreEnemy(profile.EnemyOptions);
            enemy.enemyHP = RollDataRange(profile.MinHP, profile.MaxHP);
            enemy.enemyCHP = enemy.enemyHP;
            enemy.enemyArmor = RollDataRange(profile.MinArmor, profile.MaxArmor);
            enemy.enemyDamage = RollDataRange(profile.MinDamage, profile.MaxDamage);
            enemy.enemyxpgiven = RollDataRange(profile.MinXp, profile.MaxXp);
            enemy.desc = profile.Description;
            enemy.isdefeated = false;
            enemy.goldDrop = CalculateEncounterGoldDrop(enemy.enemyHP, enemy.enemyArmor, enemy.enemyDamage, enemy.enemyxpgiven);
            return true;
        }

        private bool TryResolveEnemyUniqueSkillFromData(out string output)
        {
            output = "";

            if (enemySkillProfiles.Count == 0)
            {
                return false;
            }

            EnemySkillData? profile = enemySkillProfiles.FirstOrDefault(skill =>
                string.IsNullOrWhiteSpace(skill.EnemyNameContains) == false
                && enemy.currentEnemy.Contains(skill.EnemyNameContains, StringComparison.OrdinalIgnoreCase));

            if (profile == null)
            {
                return false;
            }

            int chance = Math.Clamp(profile.ChancePercent, 0, 100);
            if (exploreRandom.Next(0, 100) >= chance)
            {
                return false;
            }

            int damage = Math.Max(RollDataRange(profile.MinDamage, profile.MaxDamage) - Player.armor, 1);
            Player.currentHealth -= damage;

            string effect = profile.StatusEffect.Trim().ToLowerInvariant();
            if (effect == "burn")
            {
                ApplyPlayerBurn(profile.StatusTurns, profile.StatusAmount);
            }
            else if (effect == "bleed")
            {
                ApplyPlayerBleed(profile.StatusTurns, profile.StatusAmount);
            }

            if (string.IsNullOrWhiteSpace(profile.MessageTemplate) == false)
            {
                output = profile.MessageTemplate
                    .Replace("{enemy}", enemy.currentEnemy)
                    .Replace("{skill}", profile.SkillName)
                    .Replace("{damage}", damage.ToString());
            }
            else
            {
                output = $"{enemy.currentEnemy} uses {profile.SkillName} for {damage} damage!\n";
            }

            if (output.EndsWith("\n") == false)
            {
                output += "\n";
            }

            return true;
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

                if (data.RoomEncounters.Count > 0)
                {
                    roomEncounterData.Clear();
                    foreach (RoomEncounterData encounter in data.RoomEncounters)
                    {
                        if (encounter.X < 1 || encounter.X > 5 || encounter.Y < 1 || encounter.Y > 5)
                        {
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(encounter.EnemyName))
                        {
                            continue;
                        }

                        roomEncounterData[BuildEncounterKey(encounter.X, encounter.Y)] = encounter;
                    }
                }

                if (data.ExploreEncounterProfiles.Count > 0)
                {
                    exploreEncounterProfiles.Clear();
                    foreach (ExploreEncounterProfileData profile in data.ExploreEncounterProfiles)
                    {
                        if (profile.BiomeContains.Count == 0 || profile.EnemyOptions.Count == 0)
                        {
                            continue;
                        }

                        exploreEncounterProfiles.Add(profile);
                    }
                }

                if (data.EnemySkills.Count > 0)
                {
                    enemySkillProfiles.Clear();
                    foreach (EnemySkillData skill in data.EnemySkills)
                    {
                        if (string.IsNullOrWhiteSpace(skill.EnemyNameContains) || string.IsNullOrWhiteSpace(skill.SkillName))
                        {
                            continue;
                        }

                        enemySkillProfiles.Add(skill);
                    }
                }

                if (data.CommandUsage.Count > 0)
                {
                    commandUsage.Clear();
                    foreach (CommandUsageData usage in data.CommandUsage)
                    {
                        if (string.IsNullOrWhiteSpace(usage.Topic) || string.IsNullOrWhiteSpace(usage.Usage))
                        {
                            continue;
                        }

                        commandUsage[usage.Topic.Trim()] = usage.Usage.Trim();
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
