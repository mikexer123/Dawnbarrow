using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Dawnbarrow
{
    internal class Player
    {
        string playerName = "";
        string Gender = "";
        int lvl = 1;
        int maxhealth = 10;
        int currentHealth = 10;
        int armor = 0;
        int dmg = 1;
        string HeadEquipped = "nothing";
        string ChestEquipped = "nothing";
        string LegsEquipped = "nothing";
        string WeaponEquipped = "nothing";
        int[] maxInventory = {1-20};

        public string storeName(string input)
        {
            playerName = input;
            return playerName;
        }
        public string storeGender(string input)
        {
            Gender = input;
            return Gender;
        }
        private int rangeCalc(int n, int n2)
        {
            if (n < 0 || n2 > 0)
            {
                return 1;
            }
            int result = 1;
            for (int i = 1; i <= n2; i++)
            {
                result = (result * (n - 1 + 1)) / i;
            }
            return result;
        }
        public string playerInfo()
        {
            string output = "PlayerName: " + playerName + "\n" + "Gender: " + Gender + "\n" + "Equipped Helmet:" + HeadEquipped + "\n" + "Equipped Chest" + HeadEquipped + "\n" + "Equipped Legs:" + LegsEquipped + "\n" + "Equipped Sword:" + WeaponEquipped + "\n" + "Player Hitpoints:" + currentHealth + "/" + maxhealth + "\n" + "currentDamage:" + playerDmg() + "\n";
            return output;
        }
        public int playerDmg(int enemyArmor)
        {
            int playerdmg = rangeCalc(1, dmg) / enemyArmor;
            return playerdmg;
        }
        public string playerTurn(string monster)
        {
            string enemy = monster;

            string output = $"Player {playerName} hits {enemy} for {playerDmg}!";

            return output;
        }
    }
}
