using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawnbarrow
{
    internal class Enemy
    {
        List<string> Boss = new List<string>() { "dragon", "chimera", "hydra", "cerberus", "giant" };
        List<string> Monster = new List<string>() { "rat", "ghoul", "skeleton", "zombie", "spider", "snake", "red ant", "liger", "kappa", "goblin"  };

        public int enemyHP = 1; //EnemyMAX Hit Points (their total life)
        public int enemyCHP = 1; //Enemy Current Hit Points
        public int enemyArmor = 1; // Enemy Armor
        public int enemyDamage = 1; // Enemy Damage to player on every hit
        public string currentEnemy = ""; //The enemy string value tied to the currentRoom
        public int enemyxpgiven = 1;
        public string desc = "";
        public string placedObject = "";
        public bool isdefeated = false;
        public bool needsTalkingCat = false;
        public bool needsLadder = false;
        public bool needsPickaxe = false;
        public bool needsBossKey = false;
        public bool needsFriendshipBracelet = false;
        public void enemySpawn(int x, int y)
        
        {

            //1,1 Forest

            if ((x == 1) && (y == 1))
            {
                isdefeated = false;
                enemyHP = rangeCalc(1, 3);
                enemyCHP = enemyHP;
                enemyArmor = rangeCalc(1, 2); //method that chooses 1,2 (I learned that isn't built inherently in C# so I had to make one :)
                enemyDamage = rangeCalc(1, 3);
                currentEnemy = "Rat";
                enemyxpgiven = rangeCalc(5, 5);
                desc = "\"You have encountered a rat! He look's kind of small though, I'm sure you can take him, realistically if you die here, you were never meant to play this game\"";
                placedObject = "Iron Sword +1";
            }
            else
            //1,2 Forest
            if ((x == 1) && (y == 2))
            {
                isdefeated = false;
                enemyHP = rangeCalc(1, 3);
                enemyCHP = enemyHP;
                enemyArmor = rangeCalc(1, 2); 
                enemyDamage = rangeCalc(1, 2); 
                currentEnemy = "Skeleton";
                enemyxpgiven = rangeCalc(5, 10);
                desc = "\"You have encountered a skeleton! He is a silly bag of bones, I think he's even falling apart. There isn't much he can do against a good old cardiovascular system\"";
                placedObject = "Leather Helmet +1";
            }
            else
            //1,3 Forest
            if ((x == 1) && (y == 3))
            {
                isdefeated = false;
                enemyHP = rangeCalc(1, 5);
                enemyCHP = enemyHP;
                enemyArmor = rangeCalc(1, 3);
                enemyDamage = rangeCalc(1, 3);
                currentEnemy = "Rat";
                enemyxpgiven = rangeCalc(3, 20);
                desc = "\"You have encountered a rat! He look's kind of small though, I'm sure you can take him, realistically if you die here, you were never meant to play this game\"";
                placedObject = "Leather Chestplate +1";
            }
            else
            //1,4 Forest
            if ((x == 1) && (y == 4))
            {
                isdefeated = false;
                enemyHP = rangeCalc(1,8);
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "Large Spider";
                enemyxpgiven = rangeCalc(1, 5);
                desc = "\" This spider is actually terrifying, but he's still just a spider right? RIGHT? There's no way you'll lose to a spider? You're meant to be the savior of Dawnbarrow not the sissy who ran from danger.\"";
                placedObject = "Leather Leggings +1";
            }
            else
            //1,5 Forest
            if ((x == 1) && (y == 5))
            {
                isdefeated = false;
                enemyHP = 25;
                enemyCHP = 25;
                enemyArmor = 1;
                enemyDamage = rangeCalc(1,2);
                currentEnemy = "Zombie";
                enemyxpgiven = rangeCalc(1, 5);
                desc = "I hope it's clear now that you should only be using this part of the game as an exploration tool, and that by NO means should you search on the ground after defeating this zombie";
                placedObject = "Iron Sword +1";
            }
            else
            //2,1 Jungle LADDER LOCATION 
            if ((x == 2) && (y == 1))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "Ghoul";
                enemyxpgiven = rangeCalc(12, 25);
                placedObject = "Ladder";
                desc = "There is a ghoul here who has 4 peg legs for appendages, he doesn't seem threatening in the slightest but where did these pegs come from";
            }
            else
            //2,2 Jungle
            if ((x == 2) && (y == 2))
            {
                isdefeated = false;
                enemyHP = 20;
                enemyCHP = 20;
                enemyArmor = 2;
                enemyDamage = 5;
                currentEnemy = "Snake";
                desc = "This snake is no ordinary snake, he seems slimier than usual and has extra rows of fangs? I'm not sure the species, but I can tell you one thing, this creature looks like the love child of a snake and a florida man!";
                enemyxpgiven = rangeCalc(10, 25);
                placedObject = "Fire Sword +2";
            }
            else
            //2,3 River
            if ((x == 2) && (y == 3))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
                

            }
            else
            //2,4 Grassland
            if ((x == 2) && (y == 4))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //2,5 Grassland TALKING CAT
            if ((x == 2) && (y == 5))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "Ghoul";
                enemyxpgiven = rangeCalc(1, 5);
                desc = "This ghoul looks hungry for cats, you can hear him purring as cats do, I think there is only ONE thing to do here, put this sick monster out of it's misery.";
                placedObject = "Talking Cat";
            }
            else
            //3,1
            if ((x == 3) && (y == 1))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //3,2
            if ((x == 3) && (y == 2))
            {
                isdefeated = false;
                enemyHP = 50;
                enemyCHP = 50;
                enemyArmor = 3;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //3,3
            if ((x == 3) && (y == 3))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //3,4 Cerberus Savior Sword Location
            if ((x == 3) && (y == 4))
            {
                isdefeated = false;
                enemyHP = 50;
                enemyCHP = 50;
                enemyArmor = 2;
                enemyDamage = rangeCalc(1,12);
                currentEnemy = "Cerberus";
                enemyxpgiven = rangeCalc(1, 5);
                placedObject = "Savior Sword +4";
            }
            else
            //3,5 FRIENDSHIP BRACELET Location
                if ((x == 3) && (y == 5))
            {
                isdefeated = false;
                enemyHP = 30;
                enemyCHP = 30;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "Giant Prarie Dog";
                desc = "The menacing Prarie Dog has an irrational anger towards you for laughing at his brethren. There's something caught around his neck! His razor sharp fangs look deadly and are best dealt with using a blade of your own!";
                enemyxpgiven = rangeCalc(1, 10);
                placedObject = "Friendship Bracelet";
            }
            else
            //4,1
                if ((x == 4) && (y == 1))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //4,2
                if ((x == 4) && (y == 2))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //4,3
                if ((x == 4) && (y == 3))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //4,4
                if ((x == 4) && (y == 4))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //4,5
                if ((x == 4) && (y == 5))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //5,1
            if ((x == 5) && (y == 1))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //5,2
            if ((x == 5) && (y == 2))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            //5,3
            else
                if ((x == 5) && (y == 3))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //5,4
                if ((x == 5) && (y == 4))
            {
                isdefeated = false;
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
                enemyxpgiven = rangeCalc(1, 5);
            }
            else
            //5,5
            if ((x == 5) && (y == 5))
            {
                isdefeated = false;
                needsFriendshipBracelet = true;
                enemyHP = 1000;
                enemyCHP = 1000;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "Lonely Giant";
                enemyxpgiven = rangeCalc(1, 2000);
                desc = "This humongous creature could not look any more sad. He seems to be guarding a piece of the saviors armor, you're more than welcome to try and fight him, but people this sad need friends, and even this monster doesn't deserve this fate";
            }
            else
            // if the player somehow got out of bounds and fought an enemy, they will see a debug enemy listed below :)
            {
                isdefeated = false;
                enemyHP = 2;
                enemyCHP = 2;
                enemyArmor = 2;
                enemyDamage = 2;
                currentEnemy = "YOU SHOULD NOT BE HERE";
                enemyxpgiven = rangeCalc(1, 5);
            }

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
        private int CalculateEDamage(int armor, int damage) //method to calculate Enemy damage, needs revision. 
        {
            if (armor > 0)
            {
                damage -= armor; //As it stands the enemy armor is subtracted from your current damage
                return damage;
            }
            else
            if (armor == 0)
            {
                return damage + 1;
            }
            else
                return 111;
        }
        public string MonsterInfo()
        {
            string output = $"Monster Name: {currentEnemy} \n Monster HitPoints: {enemyCHP}/{enemyHP} \n Monster Armor: {enemyArmor} ";
            return output;
        }
        public int MonsterDmg(int PlayerArmor)
        {
           int monsterdmg = rangeCalc(1, enemyDamage) - PlayerArmor;
           return monsterdmg;
        }
        public string MonsterTurn(int PlayerArmor)
        {
            string output = $"Enemy {currentEnemy} hits you for {MonsterDmg(PlayerArmor)}";
            return output;
        }
    }
}
