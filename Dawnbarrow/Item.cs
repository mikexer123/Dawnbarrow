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
       
       
     
        public struct Tool
        {
            public int itemid = 0;
            public string toolName = "";
            public string toolType = "";
            public (int x, int y) roomLocation;

            public Tool(int x, int y, string name, int type)
            {
                int tvalue = type;
                toolName = name;


                roomLocation = (x, y);
                if (tvalue == 0) 
                {
                    bool isQuestItem = true;
                    toolType = "Quest Item";
                }
                if (tvalue == 1)
                {
                    bool isWeapon = true;
                    toolType = "Weapon";
                }
                if (tvalue == 2)
                {
                    bool isArmor = true;
                    toolType = "Armor";
                }
                if (tvalue == 3)
                {
                    bool isConsumable = true;
                    toolType = "Consumable";
                }

            }
                

        }
        public Item()
        {
            
        }





    } }
        
 

