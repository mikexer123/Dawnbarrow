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

        public Item (int itemid)
        {
            this.itemid = itemid;
        }
        
    }
}
