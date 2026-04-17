using System;
using System.Text;

namespace Dawnbarrow
{
    internal class Game
    {
        private const string DefaultRoomText = "You are in a strange, unmapped location, how did you get here?";
        public int currentRoomIndex;
        public (int x, int y) currRoomCoordinates;
        public string[] roomDescriptions;
        public string[] roomsubtext;
        private readonly int minX;
        private readonly int maxX;
        private readonly int minY;
        private readonly int maxY;
        private readonly int mapWidth;

        public Game()
        {
            (minX, maxX, minY, maxY) = WorldData.GetBounds();
            mapWidth = (maxX - minX) + 1;
            int mapHeight = (maxY - minY) + 1;
            int roomCount = Math.Max(1, mapWidth * mapHeight);
            roomDescriptions = new string[roomCount];
            roomsubtext = new string[roomCount];
            currRoomCoordinates = WorldData.GetStartCoordinates();
            RefreshRoomTextCache();
            currentRoomIndex = IndexFor(currRoomCoordinates.x, currRoomCoordinates.y);
        }

        private int IndexFor(int x, int y)
        {
            return (x - minX) + (y - minY) * mapWidth;
        }

        private void RefreshRoomTextCache()
        {
            for (int index = 0; index < roomDescriptions.Length; index++)
            {
                roomDescriptions[index] = DefaultRoomText;
                roomsubtext[index] = roomDescriptions[index];
            }

            foreach (WorldData.RoomNode room in WorldData.GetRooms().Values)
            {
                int index = IndexFor(room.X, room.Y);
                if (index < 0 || index >= roomDescriptions.Length)
                {
                    continue;
                }

                roomDescriptions[index] = string.IsNullOrWhiteSpace(room.Description)
                    ? roomDescriptions[index]
                    : room.Description;

                roomsubtext[index] = string.IsNullOrWhiteSpace(room.Subtext)
                    ? roomDescriptions[index]
                    : room.Subtext;
            }
        }

        public void setCurrentRoom(int x, int y)
        {
            if (WorldData.RoomExists(x, y))
            {
                currRoomCoordinates = (x, y);
            }
            else
            {
                currRoomCoordinates = WorldData.GetStartCoordinates();
            }

            currentRoomIndex = IndexFor(currRoomCoordinates.x, currRoomCoordinates.y);
        }

        public (int x, int y) getCurrentRoom()
        {
            return currRoomCoordinates;
        }

        public int getcurrentRoomIndex()
        {
            return currentRoomIndex;
        }

        public bool checkRoom(bool direction)
        {
            return direction;
        }

        public string getMap()
        {
            int x = currRoomCoordinates.x;
            int y = currRoomCoordinates.y;
            StringBuilder output = new StringBuilder();

            for (int row = maxY; row >= minY; row--)
            {
                for (int col = minX; col <= maxX; col++)
                {
                    if (WorldData.RoomExists(col, row) == false)
                    {
                        output.Append(' ');
                        continue;
                    }

                    output.Append(row == y && col == x ? 'X' : 'O');
                }

                output.AppendLine();
            }

            return output.ToString();
        }

        public string GetCurrentRoomSubtext()
        {
            int index = IndexFor(currRoomCoordinates.x, currRoomCoordinates.y);
            if (index >= 0 && index < roomsubtext.Length)
            {
                return roomsubtext[index];
            }

            return DefaultRoomText;
        }

        public string Output()
        {
            int x = currRoomCoordinates.x;
            int y = currRoomCoordinates.y;
            currentRoomIndex = IndexFor(x, y);

            if ((currentRoomIndex >= 0) && (currentRoomIndex < roomDescriptions.Length))
            {
                return roomDescriptions[currentRoomIndex];
            }

            return "You are in a strange, unmapped location, how did you get here?";
        }

        public string checkInput(string input, int currRoomx, int currRoomy)
        {
            string response = "";
            if ((input == "look around") || (input == "Look around") || (input == "see around") || (input == "search") || (input == "inspect surroundings"))
            {
                response = Output();
            }
            if ((input == "south") || (input == "South") || (input == "SOUTH") || (input == "s") || (input == "S"))
            {
                response = "You start heading South";
            }
            if ((input == "north") || (input == "North") || (input == "NORTH") || (input == "n") || (input == "N"))
            {
                response = "You start heading North!";
            }
            if ((input == "east") || (input == "East") || (input == "EAST") || (input == "e") || (input == "E"))
            {
                response = "You start heading East";
            }
            if ((input == "west") || (input == "West") || (input == "WEST") || (input == "w") || (input == "W"))
            {
                response = "You start heading West";
            }

            return response;
        }

        public string checkInput(string input)
        {
            string response = "";
            if ((input == "look around") || (input == "Look around") || (input == "see around") || (input == "search") || (input == "inspect surroundings"))
            {
                response = GetCurrentRoomSubtext();
            }
            if ((input == "south") || (input == "South") || (input == "SOUTH") || (input == "s") || (input == "S"))
            {
                response = "You start heading South";
            }
            if ((input == "north") || (input == "North") || (input == "NORTH") || (input == "n") || (input == "N"))
            {
                response = "You start heading North!";
            }
            if ((input == "east") || (input == "East") || (input == "EAST") || (input == "e") || (input == "E"))
            {
                response = "You start heading East";
            }
            if ((input == "west") || (input == "West") || (input == "WEST") || (input == "w") || (input == "W"))
            {
                response = "You start heading West";
            }
            if ((input == "fight") || (input == "kill" || (input == "murder") || (input == "")))
            {
                response = "You begin fighting";
            }

            return response;
        }
    }
}
