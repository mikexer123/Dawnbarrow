using System;
using System.Collections.Generic;

namespace Dawnbarrow
{
    internal class Room
    {
        private readonly Random random = new Random();
        private readonly IReadOnlyDictionary<string, WorldData.RoomNode> rooms;
        private (int x, int y) currRoomCoordinates;

        public Room()
        {
            rooms = WorldData.GetRooms();
            currRoomCoordinates = (1, 1);
        }

        private static string Key(int x, int y)
        {
            return $"{x},{y}";
        }

        public string Biome(int x, int y)
        {
            return rooms.TryGetValue(Key(x, y), out WorldData.RoomNode? room) ? room.Biome : "Unknown location";
        }

        public (int x, int y) GetCurrentRoom() => currRoomCoordinates;

        public (int x, int y) GetNextRoomIndex(int currentIndex, string direction)
        {
            return GetNextRoomIndex(direction);
        }

        public (int x, int y) getCurrentRoomCoordinates()
        {
            return currRoomCoordinates;
        }

        public bool randomEncounter()
        {
            return random.Next(100) < 55;
        }

        public bool IsRepeatableEncounterRoom(int x, int y)
        {
            if (rooms.TryGetValue(Key(x, y), out WorldData.RoomNode? room) == false)
            {
                return false;
            }

            return room.RepeatableEncounter;
        }

        public void setCurrentRoom(int x, int y)
        {
            currRoomCoordinates = (x, y);
        }

        public string direction(string dir)
        {
            switch (dir)
            {
                case "north":
                case "N":
                case "n":
                case "North":
                case "NORTH":
                    return "north";
                case "West":
                case "west":
                case "WEST":
                case "W":
                case "w":
                    return "west";
                case "South":
                case "SOUTH":
                case "south":
                case "s":
                case "S":
                    return "south";
                case "east":
                case "EAST":
                case "East":
                case "e":
                case "E":
                    return "east";
                default:
                    return dir;
            }
        }

        public (int x, int y) GetNextRoomIndex(string direction)
        {
            int newX = currRoomCoordinates.x;
            int newY = currRoomCoordinates.y;

            switch (direction)
            {
                case "north":
                    newY++;
                    break;
                case "west":
                    newX--;
                    break;
                case "south":
                    newY--;
                    break;
                case "east":
                    newX++;
                    break;
                default:
                    return currRoomCoordinates;
            }

            if ((newX < 1) || (newY < 1) || (newX > 5) || (newY > 5))
            {
                return currRoomCoordinates;
            }

            return (newX, newY);
        }

        public bool CanGo(string direction)
        {
            if (rooms.TryGetValue(Key(currRoomCoordinates.x, currRoomCoordinates.y), out WorldData.RoomNode? room) == false)
            {
                return false;
            }

            switch (direction)
            {
                case "north":
                    return room.CanGoNorth;
                case "west":
                    return room.CanGoWest;
                case "south":
                    return room.CanGoSouth;
                case "east":
                    return room.CanGoEast;
                default:
                    return false;
            }
        }
    }
}
