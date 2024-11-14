using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawnbarrow
{
    internal class Player
    {
        string playerName = "";
        string Gender = "";
        int lvl = 1;
        int health = 10;
        int magic = 10;
        int armor = 0;
        string HeadEquipped = "nothing";
        string ChestEquipped = "nothing";
        string LegsEquipped = "nothing";
        string WeaponEquipped = "nothing";
        int[] maxInventory = {1-20};
    }
}
