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
       public double lvl = 1;
       public double maxhealth = 10;
       public double currentHealth = 10;
       public int armor = 1;
       public int dmg = 1;
       public string HeadEquipped = "nothing";
       public string ChestEquipped = "nothing";
       public string LegsEquipped = "nothing";
       public string WeaponEquipped = "nothing";
       public bool isFighting = false;
       public double xptonextlevel = 5;
       public double currentxp = 0;
        public string experience(int enemyxpgiven) //this method is for calculating experience and HOPEFULLY solving leveling completely.
        {
            string output;

            output = $"You gained {enemyxpgiven} experience points \n";
            currentxp += enemyxpgiven;
            output += $"current xp:{currentxp} //// total xp to next level{xptonextlevel} \n";
            if ( currentxp >= xptonextlevel )
            {
                lvl++;
                output += $"You leveled up! \n Your new level is {lvl} \n";
                xptonextlevel = xptonextlevel * 1.25;
                maxhealth = maxhealth * 1.25;
                currentHealth = maxhealth;
            }
            
            return output;
        }
       
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
           
            int playerdmg = rangeCalc(1, 3) / enemyArmor;
            if (WeaponEquipped == "Nothing")
            {
                playerdmg = rangeCalc(1, 5);
            }   
            else
            if (WeaponEquipped == "Iron Sword +1")
            {
                playerdmg = rangeCalc(3, 6);
            }
            else
            if (WeaponEquipped == "Fire Sword +2")
            {
                playerdmg = rangeCalc(4, 8);
            }
            else
            if (WeaponEquipped == "Topaz Sword +3")
            {
                playerdmg = rangeCalc(5, 10);
            }
            else
            if (WeaponEquipped == "Savior Sword +4")
            {
                playerdmg = rangeCalc(6, 15);
            }
            return playerdmg;
        }
        public string playerTurn(string monster, int armorval)
        {
            string enemy = monster;

            string output = $"Player {playerName} hits {enemy} for {playerDmg(armorval)}!";

            return output;
        }
    }
}
