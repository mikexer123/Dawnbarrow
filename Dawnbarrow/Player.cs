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
       public string playerName = "";
       public string Gender = "";
       public int lvl = 1;
       public int maxhealth = 10;
       public int currentHealth = 10;
       public int armor = 1;
       public int dmg = 1;
       public string HeadEquipped = "nothing";
       public string ChestEquipped = "nothing";
       public string LegsEquipped = "nothing";
       public string WeaponEquipped = "nothing";
       public bool isFighting = false;
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
            {

                if (n > n2)
                {
                    throw new ArgumentException("The first parameter (min) must be less than or equal to the second parameter (max).");
                }

                Random random = new Random();
                return random.Next(n, n2 + 1);
            }
        }
        public string playerInfo()
        {
            string output = "PlayerName: " + playerName + "\n" + "Gender: " + Gender + "\n" + "Equipped Helmet:" + HeadEquipped + "\n" + "Equipped Chest" + HeadEquipped + "\n" + "Equipped Legs:" + LegsEquipped + "\n" + "Equipped Sword:" + WeaponEquipped + "\n" + "Player Hitpoints:" + currentHealth + "/" + maxhealth + "\n" + "currentDamage:" + playerDmg(2) + "\n";
            return output;
        }
        public int playerDmg(int enemyArmor)
        {
           
            int playerdmg = rangeCalc(1, dmg) / enemyArmor;

            if (WeaponEquipped == "iron sword +1")
            {
                playerdmg += 1;
            }
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
