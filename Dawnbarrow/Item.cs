using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms.VisualStyles;
namespace Dawnbarrow
{//Probably going to trash this whole file because I cannot figure out how to make an itemclass, and making a bool for EVERY item has worked so much better :)
    public enum ItemType
    {
        Weapon,
        Armor,
        QuestItem,
        Consumable,
        Misc
    }
    internal class Item
    {

        public string[] itemname = { "Leather Helmet +1", "Iron Helmet +2", "Topaz Helmet +3", "Savior Helmet +4", "Leather Chestplate +1", "Iron Chestplate +2", "Topaz Chestplate +3", "Savior Chestplate +4", "Leather Leggings +1", "Iron Leggings +2", "Topaz Leggings +3", "Savior Leggings +4", "Iron Sword +1", "Fire Sword +2", "Topaz Sword +3", "Savior Sword +4", "Ladder", "Pickaxe", "Boss Key", "Talking Cat", "Friendship Bracelet", "Fire Bomb" };
      
        public string Name {  get; set; }
        public ItemType Type { get; set; }
        public bool IsWeapon { get; set; }
        public bool IsArmor { get; set; }
        public bool IsQuestItem { get; set; }
        public bool IsConsumable { get; set; }
        public bool IsMisc { get; set; }

        public Item(string name, ItemType type)
        {
            Name = name;
            Type = type;
            IsWeapon = (type == ItemType.Weapon);
            IsArmor = (type == ItemType.Armor);
            IsQuestItem = (type == ItemType.QuestItem);
            IsConsumable = (type == ItemType.Consumable);
            IsMisc = (type == ItemType.Misc);
        }

        public static ItemType determiner(string itemname)
        {
            if (itemname.Contains("Helmet") || itemname.Contains("Chestplate") || itemname.Contains("Leggings"))
            {
                return ItemType.Armor;
            }
            if (itemname.Contains("Sword"))
            {
                return ItemType.Weapon;
            }
            if (itemname == "Ladder" || itemname == "Pickaxe" || itemname == "Boss Key" || itemname == "Talking Cat" || itemname == "Friendship Bracelet")
            { 
                return ItemType.QuestItem; 
            }
            if (itemname == "potion" || itemname == "firebomb")
            {
                return ItemType.Consumable;
            }
            else
            return ItemType.Misc;
        }

        //public string itemvariable(string[] itemname, int itemtype)
        //{
        //    string bryant = itemname[itemtype];
        //    return bryant;
        //}
        //public string checkitem(int itemtype)
        //{
        //    if (itemtype == 0)
        //    { }
        //    return "";
        //}
        //public int itemType(string[] itemname)
        //{
          
        //    for (int i = 0; i < itemname.Length; i++)
        //    {
        //        if (itemname[i].Contains("Leather"))
        //        {
        //            return 0;
        //        }
        //    }
        //    return 0;
        //}
        //public string currentRoomItem(int x, int y)
        //{
        //    int numColumns = 5;
        //    int currentRoomIndex = (x - 1) + (y - 1) * numColumns;
        //    if ((currentRoomIndex >= 0) && (currentRoomIndex < itemname.Length))
        //    {
        //        return itemname[currentRoomIndex];
        //    }
        //    else return "This item does not exist";
        //}
        
        //public struct Tools
        //{  // Tools refer to "Placed Tools"
        //    public int itemid = 0;
        //    public string toolName = "";
        //    public string toolType = "";
        //    public (int x, int y) roomLocation;
        //    bool isQuestItem;
        //    bool isWeapon;
        //    bool isArmor;
        //    bool isConsumable;



           
        //    public Tools(int x, int y, string name, int type)
        //    {
        //        int tvalue = type;
        //        toolName = name;
                

        //        roomLocation = (x, y);
        //        if (tvalue == 0)
        //        {
        //            isQuestItem = true;
        //            toolType = "Quest Item";
        //        }
        //        else
        //        if (tvalue == 1)
        //        {
        //            isWeapon = true;
        //            toolType = "Weapon";
        //        }
        //        else
        //        if (tvalue == 2)
        //        {
        //            isArmor = true;
        //            toolType = "Armor";
        //        }
        //        else
        //        if (tvalue == 3)
        //        {
        //            isConsumable = true;
        //            toolType = "Consumable";
        //        }
        //        else
        //        {
        //            isArmor = false;
        //            isConsumable = false;
        //            isQuestItem = false;
        //            isWeapon = false;
        //            toolType = "This item doesn't exist";
        //        }
        //    }
        //    } // End of item struct

        //public Item(int x, int y) // Constructor for the Item Object that populates each room with an item
        //{


        //    for (int i = 0; i < itemname.Length; i++)
        //    {
        //        List<Tools> items = new List<Tools> {
        //        new Tools(x, y, itemname[i], tvalue(itemname))
        //        };

        //    }

        //}
        //public int tvalue(string[] itemname)
        //{

        //    int value = 0;

        //    for (int i = 0; i < itemname.Length; i++)
        //    {
        //        if (itemname[i].Contains("Helmet"))
        //        {
        //            value = 2;
        //        }
        //        else
        //        if (itemname[i].Contains("Chestplate"))
        //        {
        //            value = 2;
        //        }
        //        else
        //        if (itemname[i].Contains("Legging"))
        //        {
        //            value = 2;
        //        }
        //        else
        //        if (itemname[i].Contains("Sword"))
        //        {
        //            value = 1;
        //        }
        //        else
        //        if (itemname[i] == "Ladder" || itemname[i] == "Pickaxe" || itemname[i] == "Boss Key" || itemname[i] == "Talking Cat" || itemname[i] == "Friendship Bracelet")
        //        {
        //            value = 0;
        //        }
        //    }
        //    return value;
        //}




    } }
        
 

