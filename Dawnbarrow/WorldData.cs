using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Dawnbarrow
{
    internal static class WorldData
    {
        internal sealed class RoomNode
        {
            public int X { get; set; }
            public int Y { get; set; }
            public string Biome { get; set; } = "Unknown location";
            public bool CanGoNorth { get; set; }
            public bool CanGoSouth { get; set; }
            public bool CanGoEast { get; set; }
            public bool CanGoWest { get; set; }
            public bool RepeatableEncounter { get; set; } = true;
            public string Description { get; set; } = "";
            public string Subtext { get; set; } = "";
        }

        private sealed class WorldFile
        {
            public List<RoomNode> Rooms { get; set; } = new List<RoomNode>();
        }

        private static IReadOnlyDictionary<string, RoomNode>? roomCache;

        private static string BuildKey(int x, int y)
        {
            return $"{x},{y}";
        }

        private static string GetWorldDataPath()
        {
            return Path.Combine(AppContext.BaseDirectory, "Data", "world-data.json");
        }

        public static IReadOnlyDictionary<string, RoomNode> GetRooms()
        {
            if (roomCache != null)
            {
                return roomCache;
            }

            Dictionary<string, RoomNode> result = new Dictionary<string, RoomNode>();
            string path = GetWorldDataPath();

            if (File.Exists(path))
            {
                try
                {
                    string json = File.ReadAllText(path);
                    WorldFile? data = JsonSerializer.Deserialize<WorldFile>(json);
                    if (data != null)
                    {
                        foreach (RoomNode room in data.Rooms)
                        {
                            if (room.X < 1 || room.X > 5 || room.Y < 1 || room.Y > 5)
                            {
                                continue;
                            }

                            result[BuildKey(room.X, room.Y)] = room;
                        }
                    }
                }
                catch
                {
                    // Fallback below.
                }
            }

            if (result.Count == 0)
            {
                for (int y = 1; y <= 5; y++)
                {
                    for (int x = 1; x <= 5; x++)
                    {
                        result[BuildKey(x, y)] = new RoomNode
                        {
                            X = x,
                            Y = y,
                            Biome = "Unknown location",
                            CanGoNorth = y < 5,
                            CanGoSouth = y > 1,
                            CanGoEast = x < 5,
                            CanGoWest = x > 1,
                            RepeatableEncounter = true,
                            Description = "You are in a strange, unmapped location, how did you get here?",
                            Subtext = "You are in a strange, unmapped location, how did you get here?"
                        };
                    }
                }
            }

            roomCache = result;
            return roomCache;
        }
    }
}
