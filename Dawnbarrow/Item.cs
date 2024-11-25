using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
namespace Dawnbarrow
{
    internal class Item
    {
        



        List<string> headArmor = new List<string>() { "Leather Helmet +1", "Iron Helmet +2", "Topaz Helmet +3", "Saviors Helmet +4" };
        List<string> chestArmor = new List<string>() { "Leather Chestplate +1", "Iron Chestplate +2", "Topaz Chestplate +3", "Saviors Chestplate +4" };
        List<string> legArmor = new List<string>() { "Leather Leggings +1", "Iron Leggings +2", "Topaz Leggings +3", "Saviors Leggings +4" };
        List<string> Weapon = new List<string>() { "Iron Sword +1", "Fire Sword +2", "Topaz Sword +3", "Saviors Sword +4" };
        List<string> questItem = new List<string>() { "Ladder", "Pickaxe", "Boss Key", "Talking Cat", "Friendship Bracelet" };
        private List<Tools> items;


        public struct Tools
        {  // Tools refer to "Placed Tools"
            public int itemid = 0;
            public string toolName = "";
            public string toolType = "";
            public (int x, int y) roomLocation;
            bool isQuestItem;
            bool isWeapon;
            bool isArmor;
            bool isConsumable;

            public Tools(int x, int y, string name, int type)
            {
                int tvalue = type;
                toolName = name;
                

                roomLocation = (x, y);
                if (tvalue == 0)
                {
                    isQuestItem = true;
                    toolType = "Quest Item";
                }
                else
                if (tvalue == 1)
                {
                    isWeapon = true;
                    toolType = "Weapon";
                }
                else
                if (tvalue == 2)
                {
                    isArmor = true;
                    toolType = "Armor";
                }
                else
                if (tvalue == 3)
                {
                    isConsumable = true;
                    toolType = "Consumable";
                }
                else
                {
                    isArmor = false;
                    isConsumable = false;
                    isQuestItem = false;
                    isWeapon = false;
                    toolType = "This item doesn't exist";
                }
            }
            } // End of item struct
        public Item() // Constructor for the Item Object that populates each room with an item
        {
            List<Tools> items = new List<Tools> {
                new Tools(1, 2, "Leather Helmet +1", 2),
                new Tools(1, 3, "Leather Chestplate +1", 2),
                new Tools(1, 4, "Leather Leggings +1", 2),
                new Tools(1, 5, "Iron Sword +1", 1),
                new Tools(2, 1, "Iron Leggings +2", 2),
                new Tools(2, 2, "Iron Leggings +2", 2),
                new Tools(2, 3, "Iron Leggings +2", 2),
                new Tools(2, 4, "Iron Leggings +2", 2),
                new Tools(2, 5, "Iron Leggings +2", 2),
                new Tools(3, 1, "Iron Leggings +2", 2),
                new Tools(3, 2, "Iron Leggings +2", 2),
                new Tools(3, 3, "Iron Leggings +2", 2),
                new Tools(3, 4, "Iron Leggings +2", 2),
                new Tools(3, 5, "Iron Leggings +2", 2),
                new Tools(4, 1, "Iron Leggings +2", 2),
                new Tools(4, 2, "Iron Leggings +2", 2),
                new Tools(4, 3, "Iron Leggings +2", 2),
                new Tools(4, 4, "Iron Leggings +2", 2),
                new Tools(4, 5, "Iron Leggings +2", 2),
                new Tools(5, 1, "Iron Leggings +2", 2),
                new Tools(5, 2, "Iron Leggings +2", 2),
                new Tools(5, 3, "Iron Leggings +2", 2),
                new Tools(5, 4, "Iron Leggings +2", 2),
                new Tools(5, 5, "Iron Leggings +2", 2),

                };


        }





    } }
        
 

