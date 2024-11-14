using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawnbarrow
{
    internal class Game
    {
        private int currentRoomIndex;
        private (int x, int y) currRoomCoordinates;
        string[] roomDescriptions =
        {
            //1 INDICES 0
            "Having just awoken, you find yourself in a strange meadow nestled in what seems to be a vibrant forest, with mild amounts of decaying architecture, certainly thousands of years old. There exists an exorbitant amount of small pests in the area. The smell of wildlife is all around you, but it's oddly comforting to be in such an area. There is a man standing near a sign, and a forest north of him. There seems to be nothing to the east, nothing to the west, and only a small rat in the southern direction. Your options are clear, you can head North, and engage with this stranger or you can head to the South, to investigate this rat.",
            //2 INDICES 1
            "The man before you seems happy to see you",
            //3 INDICES 2
            "You return to the strange meadow, and the man that was once here is gone, and the decaying architecture seems to want to be put out of it's misery",
            //4 INDICES 3
            "Around you is a vast clearing, the open paths offer an expansive array of options, you can go in any cardinal direction, and find yourself in a new unknown enviornment",
            //5 INDICES 4
            "nice",
            //6 INDICES 5
            "what is up gangster",
            //7 INDICES 6
            "wile",
            //8 INDICES 7
            "already",
            //9 INDICES 8
            "what the heck",
            //10 INDICES 9
            ":)",
            //11 INDICES 10
            "show me",
            //12 INDICES 11
            "niceu",
            //13 INDICES 12
            "Having recently woken up, you have no idea about your surroundings, but you see this silly little man in the corner, eager to speak to you",
            //14 INDICES 13
            "this ought to be interesting",
            //15 INDICES 14
            "friends",
            //16 INDICES 15
            "what's up gangster",
            //17 INDICES 16
            "nice dude",
            //18 INDICES 17
            "what is this room",
            //19 INDICES 18
            "alrighty",
            //20 INDICES 19
            "friends,",
            //21 INDICES 20
            "gangganggang",
            //22 INDICES 21
            "howdy",
            //23 INDICES 22
            "boyohboy",
            //24 INDICES 23
            "friends",
            //25 INDICES 24
            "help me jafa"

        };
        string idmeadow1 = "Having just awoken, you find yourself in a strange meadow nestled in what seems to be a vibrant forest, with mild amounts of decaying architecture, certainly thousands of years old. There exists an exorbitant amount of small pests in the area. The smell of wildlife is all around you, but it's oddly comforting to be in such an area. There is a man standing near a sign, and a forest north of him. There seems to be nothing to the east, nothing to the west, and only a small rat in the southern direction. Your options are clear, you can head North, and engage with this stranger or you can head to the South, to investigate this rat.";
        string idmeadow2 = "The man before you seems happy to see you";
        string idmeadow3 = "You return to the strange meadow, and the man that was once here is gone, and the decaying architecture seems to want to be put out of it's misery";

        string idratfight = "You have encountered a rat! He look's kind of small though, I'm sure you can take him";
        string idghoulfight = "You have encountered a Ghoul! Something tells me touching him will never get the smell out of your clothes.";
        string idskeletonfight = "You have encountered a skeleton! There is legitimately nothing to be afraid of";
        string iddragonfight = "You have encountered a ferocious dragon, his teeth are the size of your arms! It would take almost no effort for him to devour you whole";
        public void setCurrentRoom(int x, int y)
        {
            currRoomCoordinates = (x, y);
        }

        public void setcurrentRoomIndex(int x, int y)
        {
            //  currentRoomIndex = (x * y) - 1;
            // currentRoomIndex = (x - 1) + (y - 1) * 5;

        }
        public (int x, int y) getCurrentRoom()
        {
            return (currRoomCoordinates);
        }
        public int getcurrentRoomIndex()
        {
            return currentRoomIndex;
        }
        public bool checkRoom(bool Direction)
        {
            return Direction;
        }
        public string getMap()
        {
            int x = currRoomCoordinates.x;
            int y = currRoomCoordinates.y;
            string output = "";
            string dot = "**";
            string dash = "||";

            for (int i = 0; i < currRoomCoordinates.x; i++)
            {
                output += dot;
            }
            for (int i = 0; i < currRoomCoordinates.y; i++)
            {
                output += dash;
            }
            return output;
        }
        public string Output()
        {
            int x = currRoomCoordinates.x;
            int y = currRoomCoordinates.y;

            int numColumns = 5;
            currentRoomIndex = (x - 1) + (y - 1) * numColumns;

            if ((currentRoomIndex >= 0) && (currentRoomIndex < roomDescriptions.Length))
            {
                return roomDescriptions[currentRoomIndex];
            }
            else return "You are in a strange, unmapped location, how did you get here?";
            
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

            return response;
        }

    }
}
