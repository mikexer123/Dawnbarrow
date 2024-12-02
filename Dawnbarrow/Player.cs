using System;
using System.Collections.Generic;
using System.Drawing.Text;
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
       private double signif = 1;
       public string[] inventory = new string[20];
        //questitems
       public bool hasLadder = false;
       public bool hasPickaxe = false;
       public bool hasBossKey = false;
       public bool hasTalkingCat = false;
       public bool hasFriendshipBracelet = false;
        //swords
        public bool hasIronSword = false;
        public bool hasFireSword = false;
        public bool hasTopazSword = false;
        public bool hasSaviorSword = false;
        //helmets
        public bool hasLeatherHelmet = false;
        public bool hasIronHelmet = false;
        public bool hasTopazHelmet = false;
        public bool hasSaviorHelmet = false;
        //chestplate
        public bool hasLeatherChestplate = false;
        public bool hasIronChestplate = false;
        public bool hasTopazChestplate = false;
        public bool hasSaviorChestplate = false;
        //leggings
        public bool hasLeatherLeggings = false;
        public bool hasIronLeggings = false;
        public bool hasTopazLeggings = false;
        public bool hasSaviorLeggings = false;
        

        public string experience(int enemyxpgiven) //this method is for calculating experience and HOPEFULLY solving leveling completely.
        {
            string output;

            output = $"You gained {enemyxpgiven} experience points \n";
            currentxp += enemyxpgiven;
            output += $"current xp:{currentxp} //// total xp to next level{xptonextlevel} \n";
            while ( currentxp >= xptonextlevel )
            {
                lvl++;
                output += $"You leveled up! \n Your new level is {lvl} \n";
                signif += lvl;
                xptonextlevel = xptonextlevel * 1.4 + (signif * 3);
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
            string output = "PlayerName: " + playerName + "\n" + "Gender: " + Gender + "\n" + "Equipped Helmet:" + HeadEquipped + "\n" + "Equipped Chest" + ChestEquipped + "\n" + "Equipped Legs:" + LegsEquipped + "\n" + "Equipped Sword:" + WeaponEquipped + "\n" + "Player Hitpoints:" + currentHealth + "/" + maxhealth + "\n" + "currentDamage:" + playerDmg(2) + "\n";
            return output;
        }
        public int playerDmg(int enemyArmor)
        {
           
            int playerdmg = rangeCalc (1, 7);
            if (WeaponEquipped == "nothing")
            {
                playerdmg = Math.Max(rangeCalc(1, 7) - enemyArmor, 0);
            }   
            else
            if (WeaponEquipped == "Iron Sword +1")
            {
                playerdmg = rangeCalc(3, 9) - enemyArmor;
            }
            else
            if (WeaponEquipped == "Fire Sword +2")
            {
                playerdmg = rangeCalc(4, 11) - enemyArmor;
            }
            else
            if (WeaponEquipped == "Topaz Sword +3")
            {
                playerdmg = rangeCalc(5, 13) - enemyArmor;
            }
            else
            if (WeaponEquipped == "Savior Sword +4")
            {
                playerdmg = rangeCalc(6, 15) - enemyArmor;
            }
            return playerdmg;
        }
        public string playerTurn(string monster, int armorval)
        {
            string enemy = monster;

            string output = $"Player {playerName} hits {enemy} for {playerDmg(armorval)}!";

            return output;
        }
        public void giveItem(string item)
        {
            //helmets
            if (item == "Leather Helmet +1")
            {
                hasLeatherHelmet = true;
            }
            if (item == "Iron Helmet +2")
            {
                hasLeatherHelmet = true;
            }
            if (item == "Topaz Helmet +3")
            {
                hasTopazHelmet = true;
            }
            if (item == "Savior Helmet +4")
            {
                hasSaviorHelmet = true;
            }
            //chestplate
            if (item == "Leather Chestplate +1")
            {
                hasLeatherChestplate = true;
            }
            if (item == "Iron Chestplate +2")
            {
                hasIronChestplate = true;
            }
            if (item == "Topaz Chestplate +3")
            {
                hasTopazChestplate = true;
            }
            if (item == "Savior Chestplate +4")
            {
                hasSaviorChestplate = true;
            }
            //leggings
            if (item == "Leather Leggings +1")
            {
                hasLeatherLeggings = true;
            }
            if (item == "Iron Leggings +2")
            {
                hasIronLeggings = true;
            }
            if (item == "Topaz Leggings +3")
            {
                hasTopazLeggings = true;
            }
            if (item == "Savior Leggings +4")
            {
                hasSaviorLeggings = true;
            }
            //Sword
            if (item == "Iron Sword +1")
            {
                hasIronSword = true;
            }
            if (item == "Fire Sword +2")
            {
                hasFireSword = true;
            }
            if (item == "Topaz Sword +3")
            {
                hasTopazSword = true;
            }
            if (item == "Savior Sword +4")
            {
                hasTopazSword = true;
            }
            //Quest Item
            if (item == "Ladder")
            {
                hasLadder = true;
            }
            if (item == "Boss Key")
            {
                hasBossKey = true;
            }
            if (item == "Talking Cat")
            {
                hasTalkingCat = true;
            }
            if (item == "Pickaxe")
            {
                hasPickaxe = true;
            }
            if (item == "Friendship Bracelet")
            {
                hasFriendshipBracelet = true;
            }    
        }
        public string displayInventory()
        {
            string inv = "";
            string questItems = "";
            string helmets = "";
            string chestplates = "";
            string leggings = "";
            string swords = "";
            string consumables = "";
            //QuestItems
            if (hasLadder)
            {
                questItems += " Ladder";
            }
            if (hasBossKey)
            {
                questItems += " Boss Key";
            }
            if (hasTalkingCat)
            {
                questItems += " Talking Cat";
            }
            if (hasPickaxe)
            {
                questItems += " Pickaxe";
            }
            if (hasFriendshipBracelet)
            {
                questItems += " Friendship Bracelet";
            }
            //Helmets
            if (hasLeatherHelmet)
            {
                helmets += " Leather Helmet +1";
            }
            if (hasIronHelmet)
            {
                helmets += " Iron Helmet +2";
            }
            if (hasTopazHelmet)
            {
                helmets += " Topaz Helmet +3";
            }
            if (hasSaviorHelmet)
            {
                helmets += " Savior Helmet +4";
            }
            //Chestplates
            if (hasLeatherChestplate)
            {
                chestplates += " Leather Chestplate +1";
            }
            if (hasIronChestplate)
            {
                chestplates += " Iron Chestplate +2";
            }
            if (hasTopazChestplate)
            {
                chestplates += " Topaz Chestplate +3";
            }
            if (hasSaviorChestplate)
            {
                chestplates += " Savior Chestplate +4";
            }
            //Leggings
            if (hasLeatherLeggings)
            {
                leggings += " Leather Leggings +1";
            }
            if (hasIronLeggings)
            {
                leggings += " Iron Leggings +2";
            }
            if (hasTopazLeggings)
            {
                leggings += " Topaz Leggings +3";
            }
            if (hasSaviorLeggings)
            {
                leggings += " Savior Leggings +4";
            }
            //Swords
            if (hasIronSword)
            {
                swords += " Iron Sword +1";
            }
            if (hasFireSword)
            {
                swords += " Fire Sword +2";
            }
            if (hasTopazSword)
            {
                swords += " Topaz Sword +3";
            }
            if (hasSaviorSword)
            {
                swords += " Savior Sword +4";
            }
            inv = $"QuestItems: {questItems} \n Helmets: {helmets} \n Chestplates: {chestplates} \n Leggings: {leggings} \n Swords: {swords} \n Consumables: {consumables} ";
            return inv;
        }
        public void cheat()
        {
            hasSaviorSword = true;
            hasSaviorChestplate = true;
            hasSaviorLeggings = true;
            hasSaviorHelmet = true;
            hasTalkingCat = true;
            hasFriendshipBracelet = true;
            hasLadder = true;
            hasBossKey = true;
            hasPickaxe = true;
        }
        public void calculateArmor()
        {
           
            int headArmor = 0;
            int chestArmor = 0;
            int legArmor = 0;
         if (HeadEquipped == "Leather Helmet +1")
            {
                headArmor = 1;
            }
         if (HeadEquipped == "Iron Helmet +2")
            {
                headArmor = 2;
            }
         if (HeadEquipped == "Topaz Helmet +3")
            {
                headArmor = 3;
            }
         if (HeadEquipped == "Savior Helmet +4")
            {
                headArmor = 4;
            }    
         if (ChestEquipped == "Leather Helmet +1")
            {
                chestArmor = 1;
            }
         if (ChestEquipped == "Iron Chestplate +2")
            {
                chestArmor = 2;
            }
         if (ChestEquipped == "Topaz Chestplate +3")
            {
                chestArmor = 3;
            }
         if (ChestEquipped == "Savior Chestplate +4")
            {
                chestArmor = 4;
            }
         if (LegsEquipped == "Leather Leggings +1")
            {
                legArmor = 1;
            }
         if (LegsEquipped == "Iron Leggings +2")
            {
                legArmor = 2;
            }
         if (LegsEquipped == "Topaz Leggings +3")
            {
                legArmor = 3;
            }
         if (LegsEquipped == "Savior Leggings +4")
            {
                legArmor = 4;
            }

                armor = headArmor + chestArmor + legArmor;





        }
    }
}
