using System;
using System.Collections.Generic;
using System.Linq;

namespace Dawnbarrow
{
    public partial class Dawnbarrow
    {
        private readonly Dictionary<string, string> commandUsage = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["north"] = "Usage: north | n\nWhen: Move one tile north when not in combat.",
            ["south"] = "Usage: south | s\nWhen: Move one tile south when not in combat.",
            ["east"] = "Usage: east | e\nWhen: Move one tile east when not in combat.",
            ["west"] = "Usage: west | w\nWhen: Move one tile west when not in combat.",
            ["fight"] = "Usage: fight\nWhen: Start combat with the current room enemy.",
            ["hit"] = "Usage: hit\nWhen: Basic attack after combat has started.",
            ["fireball"] = "Usage: fireball\nWhen: Combat spell for direct damage + burn.",
            ["ice ball"] = "Usage: ice ball\nWhen: Combat spell for damage and enemy pressure reduction.",
            ["heal"] = "Usage: heal\nWhen: Combat spell to recover HP and apply regen.",
            ["bash"] = "Usage: bash\nWhen: Combat skill to damage and apply armor break.",
            ["slice"] = "Usage: slice\nWhen: Combat skill to deal damage + bleed.",
            ["ultra instinct"] = "Usage: ultra instinct\nWhen: High-mana burst with no enemy counterattack.",
            ["rupture"] = "Usage: rupture\nWhen: Heavy bleed skill for longer fights.",
            ["battle trance"] = "Usage: battle trance\nWhen: Apply strong regen in combat.",
            ["shop"] = "Usage: shop\nWhen: View merchant stock in the safe jungle room (3,1).",
            ["buy"] = "Usage: buy <item>\nExamples: buy fire bomb, buy scroll of freezing, buy mana.",
            ["use"] = "Usage: use <item>\nExamples: use fire bomb, use scroll of freezing, use scroll of fire.",
            ["use scroll of flight"] = "Usage: use scroll of flight x y\nExample: use scroll of flight 5 2\nWhen: Fast travel while out of combat.",
            ["inventory"] = "Usage: inventory\nWhen: Show all items and consumable counts.",
            ["equip"] = "Usage: equip <item name>\nExample: equip savior sword",
            ["take off"] = "Usage: take off <item name>\nExample: take off savior sword",
            ["explore"] = "Usage: explore\nWhen: In cleared repeatable rooms to trigger new encounters or loot.",
            ["search ground"] = "Usage: search ground\nWhen: After defeating an enemy with dropped loot.",
            ["save"] = "Usage: save OR save <filename>\nExamples: save, save run42",
            ["load"] = "Usage: load OR load <filename>\nExamples: load, load run42",
            ["map"] = "Usage: map\nWhen: Display the simple coordinate map.",
            ["run"] = "Usage: run\nWhen: Attempt to leave combat immediately.",
            ["spawn"] = "Usage: spawn OR spawn here\nWhen: Testing command to respawn the current room encounter.",
            ["restart"] = "Usage: restart\nWhen: Start a new run.",
            ["suicide"] = "Usage: suicide\nWhen: Immediate reset for fast testing.",
            ["name"] = "Usage: name <your name>\nExample: name Rowan",
            ["gender"] = "Usage: gender <value>\nExample: gender nonbinary",
            ["stats"] = "Usage: stats | check self | whoami\nWhen: Show current player information.",
            ["cheat"] = "Usage: cheat\nWhen: Testing only. Grants an extreme overpowered loadout."
        };

        private static string NormalizeUsageTopic(string topic)
        {
            return topic.Trim().ToLowerInvariant();
        }

        private string ResolveUsageTopic(string topic)
        {
            string normalized = NormalizeUsageTopic(topic);

            if (string.IsNullOrWhiteSpace(normalized))
            {
                return "Usage lookup: type ? <command>\nExamples: ? north, ? use, ? use scroll of flight, ? save";
            }

            if (normalized == "n") return commandUsage["north"];
            if (normalized == "s") return commandUsage["south"];
            if (normalized == "e") return commandUsage["east"];
            if (normalized == "w") return commandUsage["west"];

            if (commandUsage.TryGetValue(normalized, out string? value))
            {
                return value;
            }

            string? partialMatch = commandUsage.Keys.FirstOrDefault(key => key.Contains(normalized, StringComparison.OrdinalIgnoreCase));
            if (partialMatch != null)
            {
                return commandUsage[partialMatch];
            }

            return $"No usage entry found for '{topic}'. Try '? use' or '? buy'.";
        }

        private bool TryHandleUsageLookup(string trimmedInput, string lowerInput, out string response)
        {
            response = "";

            if (lowerInput == "?" || lowerInput == "usage")
            {
                response = ResolveUsageTopic("");
                return true;
            }

            if (lowerInput.StartsWith("?"))
            {
                response = ResolveUsageTopic(trimmedInput.Substring(1));
                return true;
            }

            if (lowerInput.StartsWith("usage "))
            {
                response = ResolveUsageTopic(trimmedInput.Substring(6));
                return true;
            }

            return false;
        }
    }
}
