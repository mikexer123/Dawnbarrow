using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Dawnbarrow
{
    internal class Room
    {
        private const int gridWidth = 5;
        private const int gridHeight = 5;
        private (int x, int y) currRoomCoordinates;
        private int[] roomid = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
        private List<Roomdata> rooms;





        private struct Roomdata //so this is a struct, it basically sets parameters like a function,
                                //but it's really a datatype that holds datatypes
        {
            public (int x, int y) Coordinates;
            public bool CanGoNorth;
            public bool CanGoSouth;
            public bool CanGoWest;
            public bool CanGoEast;

            public Roomdata(int x, int y, bool north, bool south, bool east, bool west)
            {
                Coordinates = (x, y);
                CanGoNorth = north;
                CanGoSouth = south;
                CanGoWest = west;
                CanGoEast = east;
            }
        }
        public Room()
        {
            rooms = new List<Roomdata>
            {   // coords, North, South, East, West (cango)
                new Roomdata(1,5, false, true, true, false),
                new Roomdata(2,5, false, true, true, true),
                new Roomdata(3,5, false, true, true, true),
                new Roomdata(4,5, false, true, true, true),
                new Roomdata(5,5, false, true, false, true),
                new Roomdata(1,4, true, true, true, false),
                new Roomdata(2,4, true, true, true, true),
                new Roomdata(3,4, true, true, true, true),
                new Roomdata(4,4, true, true, true, false),
                new Roomdata(5,4, false, true, false, false),
                new Roomdata(1,3, true, true, true, false),
                new Roomdata(2,3, true, true, true, true),
                new Roomdata(3,3, true, true, true, true), //starting room :)
                new Roomdata(4,3, true, true, true, true),
                new Roomdata(5,3, true, true, false, true),
                new Roomdata(1,2, true, true, true, false),
                new Roomdata(2,2, true, true, true, true),
                new Roomdata(3,2, true, true, true, true),
                new Roomdata(4,2, true, true, true, true),
                new Roomdata(5,2, true, false, false, false),
                new Roomdata(1,1, true, false, true, false),
                new Roomdata(2,1, true, false, true, true),
                new Roomdata(3,1, true, false, true, true),
                new Roomdata(4,1, true, false, true, true),
                new Roomdata(5,1, true, false, false, true),
             };
            currRoomCoordinates = (1, 1);
        }
        public (int x, int y) GetCurrentRoom() => currRoomCoordinates;
        public (int x, int y) GetNextRoomIndex(int currentIndex, string direction)
        {
            switch (direction)
            {
                case "north":
                case "N":
                case "n":
                case "North":
                case "NORTH":
                    return (currRoomCoordinates.x, currRoomCoordinates.y + 1);
                case "West":
                case "west":
                case "WEST":
                case "W":
                case "w":
                    return (currRoomCoordinates.x - 1, currRoomCoordinates.y);
                case "South":
                case "SOUTH":
                case "south":
                case "s":
                case "S":
                    return (currRoomCoordinates.x, currRoomCoordinates.y - 1);
                case "east":
                case "EAST":
                case "East":
                case "e":
                case "E":
                    return (currRoomCoordinates.x + 1, currRoomCoordinates.y);
                default:
                    return (currRoomCoordinates.x, currRoomCoordinates.y);
            }
        }
        public (int x, int y) getCurrentRoomCoordinates()
        {
            return currRoomCoordinates;
        }
        public void setCurrentRoom(int x, int y)
        {
            currRoomCoordinates.x = x;
            currRoomCoordinates.y = y;
        }
        public string direction(string dir) {
            string NEWS = dir;
            switch (NEWS)
            {

                case "north":
                case "N":
                case "n":
                case "North":
                case "NORTH":
                    NEWS = "north";
                    break;
                case "West":
                case "west":
                case "WEST":
                case "W":
                case "w":
                    NEWS = "west";
                    break;
                case "South":
                case "SOUTH":
                case "south":
                case "s":
                case "S":
                    NEWS = "south";
                    break;
                case "east":
                case "EAST":
                case "East":
                case "e":
                case "E":
                    NEWS = "east";
                    break;
            }
                    return NEWS;
            }
        public (int x, int y) GetNextRoomIndex(string direction)
        {
            int newX = currRoomCoordinates.x;
            int newY = currRoomCoordinates.y;

            switch (direction)
            {

                case "north":
                case "N":
                case "n":
                case "North":
                case "NORTH":
                    newY++;
                    break;
                case "West":
                case "west":
                case "WEST":
                case "W":
                case "w":
                    newX--;
                    break;
                case "South":
                case "SOUTH":
                case "south":
                case "s":
                case "S":
                    newY--;
                    break;
                case "east":
                case "EAST":
                case "East":
                case "e":
                case "E":
                    newX++;
                    break;
                default:
                    return (currRoomCoordinates.x, currRoomCoordinates.y);
            }
            if ((newX < 1) || (newY < 1) || (newX > 5) || (newY > 5))
            {
                return (currRoomCoordinates.x, currRoomCoordinates.y);
            }

            return (newX, newY);

        }

        int[] getroomid() { return roomid; }

        public bool CanGo(string direction)
        {
            var currentRoom = rooms.Find(room => room.Coordinates == currRoomCoordinates);
            switch (direction)
            {
                case "north":
                case "N":
                case "n":
                case "North":
                case "NORTH":
                    return currentRoom.CanGoNorth;
                case "West":
                case "west":
                case "WEST":
                case "W":
                case "w":
                    return currentRoom.CanGoWest;
                case "South":
                case "SOUTH":
                case "south":
                case "s":
                case "S":
                    return currentRoom.CanGoSouth;
                case "east":
                case "EAST":
                case "East":
                case "e":
                case "E":
                    return currentRoom.CanGoEast;

                default:
                    return false;
            }
        }
    }
}
