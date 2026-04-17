using System;
using System.Text;

namespace Dawnbarrow
{
    internal class Game
    {
        public int currentRoomIndex;
        public (int x, int y) currRoomCoordinates;
        public string[] roomDescriptions = new string[25];
        public string[] roomsubtext = new string[25];

        public Game()
        {
            currRoomCoordinates = (1, 1);
            RefreshRoomTextCache();
        }

        private static int IndexFor(int x, int y)
        {
            return (x - 1) + (y - 1) * 5;
        }

        private void RefreshRoomTextCache()
        {
            for (int index = 0; index < roomDescriptions.Length; index++)
            {
                roomDescriptions[index] = "You are in a strange, unmapped location, how did you get here?";
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
            currRoomCoordinates = (x, y);
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

            for (int row = 5; row >= 1; row--)
            {
                for (int col = 1; col <= 5; col++)
                {
                    output.Append(row == y && col == x ? 'X' : 'O');
                }

                output.AppendLine();
            }

            return output.ToString();
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
                response = roomDescriptions[currentRoomIndex];
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
