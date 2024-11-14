using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawnbarrow
{
    internal class Item
    {
        bool isQuestItem;
        bool isWeapon;
        bool isArmor;
        bool isConsumable;
        int itemid;
        List<string> headArmor = new List<string>() {"Leather Helmet +1", "Iron Helmet +2", "Topaz Helmet +3", "Saviors Helmet +4"};
        List<string> chestArmor = new List<string>() {"Leather Chestplate +1", "Iron Chestplate +2", "Topaz Chestplate +3","Saviors Chestplate +4"};
        List<string> legArmor = new List<string>() {"Leather Leggings +1", "Iron Leggings +2", "Topaz Leggings +3", "Saviors Leggings +4"};
        List<string> Weapon = new List<string>() {"Iron Sword +1", "Fire Sword +2", "Topaz Sword +3", "Saviors Sword +4"};
        List<string> questItem = new List<string>() { "Ladder", "Pickaxe", "Boss Key", "Talking Cat", "Friendship Bracelet"};


        public Item (int itemid)
        {
            this.itemid = itemid;
        }

        
    }
}
