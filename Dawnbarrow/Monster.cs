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

        private int enemyHP; //EnemyMAX Hit Points (their total life)
        private int enemyCHP; //Enemy Current Hit Points
        private int enemyArmor; // Enemy Armor
        private int enemyDamage; // Enemy Damage to player on every hit
        private string currentEnemy = ""; //The enemy string value tied to the currentRoom


        public void enemySpawn(int x, int y)
        {

            //1,1

            if ((x == 1) && (y == 1))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = rangeCalc(1, 3); //method that chooses 1,3 (I learned that isn't built inherently in C# so I had to make one :)
                enemyDamage = rangeCalc(1, 3);
                currentEnemy = "rat";
            }
            else
            //1,2
            if ((x == 1) && (y == 2))
            {
                enemyHP = 20;
                enemyCHP = 20;
                enemyArmor = rangeCalc(1, 3); 
                enemyDamage = rangeCalc(1, 3); 
                currentEnemy = "rat";
            }
            else
            //1,3
            if ((x == 1) && (y == 3))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = rangeCalc(1, 3);
                enemyDamage = rangeCalc(1, 3);
                currentEnemy = "rat";
            }
            else
            //1,4
            if ((x == 1) && (y == 4))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //1,5
            if ((x == 1) && (y == 5))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //2,1
            if ((x == 2) && (y == 1))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //2,2
            if ((x == 2) && (y == 2))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //2,3
            if ((x == 2) && (y == 3))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //2,4
            if ((x == 2) && (y == 4))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //2,5
            if ((x == 2) && (y == 5))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //3,1
            if ((x == 3) && (y == 1))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //3,2
            if ((x == 3) && (y == 2))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //3,3
            if ((x == 3) && (y == 3))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //3,4
            if ((x == 3) && (y == 4))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //3,5
                if ((x == 3) && (y == 5))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //4,1
                if ((x == 4) && (y == 1))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //4,2
                if ((x == 4) && (y == 2))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //4,3
                if ((x == 4) && (y == 3))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //4,4
                if ((x == 4) && (y == 4))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //4,5
                if ((x == 4) && (y == 5))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //5,1
            if ((x == 5) && (y == 1))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //5,2
            if ((x == 5) && (y == 2))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            //5,3
            else
                if ((x == 5) && (y == 3))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //5,4
                if ((x == 5) && (y == 4))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            //5,5
            if ((x == 5) && (y == 5))
            {
                enemyHP = 10;
                enemyCHP = 10;
                enemyArmor = 1;
                enemyDamage = 1;
                currentEnemy = "rat";
            }
            else
            // if the player somehow got out of bounds and fought an enemy, they will see a debug enemy listed below :)
            {
                enemyHP = 2;
                enemyCHP = 2;
                enemyArmor = 2;
                enemyDamage = 2;
                currentEnemy = "YOU SHOULD NOT BE HERE";
            }

        }
        private int rangeCalc(int n, int n2)
        {
            if (n < 0 || n2>0)
            {
                return 0;
            }
            int result = 1;
            for (int i = 1; i <= n2; i++) {
                result = (result * (n-1 + 1)) / i;
            }
            return result;
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
                return damage;
            }
            else
                return 111;
        }
        public string MonsterInfo()
        {
            string output = $"Monster Name: {currentEnemy} \n Monster HitPoints: {enemyCHP}/{enemyHP} \n Monster Armor: {enemyArmor} ";
            return output;
        }
        public int MonsterDmg()
        {
           int monsterdmg = rangeCalc(1, enemyDamage);
           return monsterdmg;
        }
        public string MonsterTurn()
        {
            string output = $"Enemy {currentEnemy} hits you for {MonsterDmg()}";
            return output;
        }
    }
}
