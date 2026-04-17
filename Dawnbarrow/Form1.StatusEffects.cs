using System;

namespace Dawnbarrow
{
    public partial class Dawnbarrow
    {
        private int enemyBurnTurns;
        private int enemyBurnDamage;
        private int enemyBleedTurns;
        private int enemyBleedDamage;
        private int enemyArmorBreakTurns;
        private int enemyArmorBreakAmount;
        private int enemyFrozenTurns;

        private int playerBurnTurns;
        private int playerBurnDamage;
        private int playerBleedTurns;
        private int playerBleedDamage;
        private int playerRegenTurns;
        private int playerRegenAmount;

        private void ClearEnemyStatusEffects()
        {
            if (enemyArmorBreakTurns > 0 && enemyArmorBreakAmount > 0)
            {
                enemy.enemyArmor += enemyArmorBreakAmount;
            }

            enemyBurnTurns = 0;
            enemyBurnDamage = 0;
            enemyBleedTurns = 0;
            enemyBleedDamage = 0;
            enemyArmorBreakTurns = 0;
            enemyArmorBreakAmount = 0;
            enemyFrozenTurns = 0;
        }

        private void ClearPlayerStatusEffects()
        {
            playerBurnTurns = 0;
            playerBurnDamage = 0;
            playerBleedTurns = 0;
            playerBleedDamage = 0;
            playerRegenTurns = 0;
            playerRegenAmount = 0;
        }

        private void ApplyEnemyBurn(int turns, int damage)
        {
            if (turns <= 0 || damage <= 0)
            {
                return;
            }

            enemyBurnTurns = Math.Max(enemyBurnTurns, turns);
            enemyBurnDamage = Math.Max(enemyBurnDamage, damage);
        }

        private void ApplyEnemyBleed(int turns, int damage)
        {
            if (turns <= 0 || damage <= 0)
            {
                return;
            }

            enemyBleedTurns = Math.Max(enemyBleedTurns, turns);
            enemyBleedDamage = Math.Max(enemyBleedDamage, damage);
        }

        private void ApplyEnemyArmorBreak(int turns, int amount)
        {
            if (turns <= 0 || amount <= 0)
            {
                return;
            }

            if (enemyArmorBreakTurns <= 0)
            {
                enemyArmorBreakAmount = amount;
                enemy.enemyArmor = Math.Max(0, enemy.enemyArmor - amount);
            }
            else if (amount > enemyArmorBreakAmount)
            {
                int bonusReduction = amount - enemyArmorBreakAmount;
                enemyArmorBreakAmount = amount;
                enemy.enemyArmor = Math.Max(0, enemy.enemyArmor - bonusReduction);
            }

            enemyArmorBreakTurns = Math.Max(enemyArmorBreakTurns, turns);
        }

        private void ApplyEnemyFreeze(int turns)
        {
            if (turns <= 0)
            {
                return;
            }

            enemyFrozenTurns = Math.Max(enemyFrozenTurns, turns);
        }

        private bool ConsumeEnemyFrozenTurn(out string output)
        {
            output = "";

            if (enemyFrozenTurns <= 0)
            {
                return false;
            }

            enemyFrozenTurns--;
            output = $"{enemy.currentEnemy} is frozen and cannot attack this turn.\n";
            return true;
        }

        private void ApplyPlayerBurn(int turns, int damage)
        {
            if (turns <= 0 || damage <= 0)
            {
                return;
            }

            playerBurnTurns = Math.Max(playerBurnTurns, turns);
            playerBurnDamage = Math.Max(playerBurnDamage, damage);
        }

        private void ApplyPlayerBleed(int turns, int damage)
        {
            if (turns <= 0 || damage <= 0)
            {
                return;
            }

            playerBleedTurns = Math.Max(playerBleedTurns, turns);
            playerBleedDamage = Math.Max(playerBleedDamage, damage);
        }

        private void ApplyPlayerRegen(int turns, int healAmount)
        {
            if (turns <= 0 || healAmount <= 0)
            {
                return;
            }

            playerRegenTurns = Math.Max(playerRegenTurns, turns);
            playerRegenAmount = Math.Max(playerRegenAmount, healAmount);
        }

        private string ProcessEnemyStatusEffectsAtTurnStart()
        {
            if (Player.isFighting == false || enemy.enemyCHP <= 0)
            {
                return "";
            }

            string output = "";

            if (enemyBurnTurns > 0)
            {
                enemy.enemyCHP -= enemyBurnDamage;
                output += $"Burn deals {enemyBurnDamage} damage to {enemy.currentEnemy}.\n";
                enemyBurnTurns--;
            }

            if (enemyBleedTurns > 0)
            {
                enemy.enemyCHP -= enemyBleedDamage;
                output += $"Bleed deals {enemyBleedDamage} damage to {enemy.currentEnemy}.\n";
                enemyBleedTurns--;
            }

            if (enemyArmorBreakTurns > 0)
            {
                enemyArmorBreakTurns--;
                if (enemyArmorBreakTurns == 0 && enemyArmorBreakAmount > 0)
                {
                    enemy.enemyArmor += enemyArmorBreakAmount;
                    output += $"{enemy.currentEnemy} recovers from armor break.\n";
                    enemyArmorBreakAmount = 0;
                }
            }

            if (enemy.enemyCHP <= 0)
            {
                enemy.enemyCHP = 0;
            }

            return output;
        }

        private string ProcessPlayerStatusEffectsAtTurnStart()
        {
            string output = "";

            if (playerBurnTurns > 0)
            {
                Player.currentHealth -= playerBurnDamage;
                output += $"You take {playerBurnDamage} burn damage.\n";
                playerBurnTurns--;
            }

            if (playerBleedTurns > 0)
            {
                Player.currentHealth -= playerBleedDamage;
                output += $"You take {playerBleedDamage} bleed damage.\n";
                playerBleedTurns--;
            }

            if (playerRegenTurns > 0)
            {
                double previousHealth = Player.currentHealth;
                Player.currentHealth = Math.Min(Player.maxhealth, Player.currentHealth + playerRegenAmount);
                double healed = Player.currentHealth - previousHealth;
                if (healed > 0)
                {
                    output += $"Regen restores {healed} health.\n";
                }
                playerRegenTurns--;
            }

            if (Player.currentHealth <= 0)
            {
                Player.currentHealth = 0;
                Player.isFighting = false;
                Player.isGameOver = true;
                output += "You have 0 health remaining. Game Over. Type restart to begin again, or load to restore a save.";
            }

            return output;
        }

        private bool TryResolveEnemyUniqueSkill(out string output)
        {
            output = "";

            if (Player.isFighting == false || enemy.enemyCHP <= 0)
            {
                return false;
            }

            int roll = exploreRandom.Next(0, 100);
            if (roll >= 28)
            {
                return false;
            }

            if (enemy.currentEnemy.Contains("DRAGON", StringComparison.OrdinalIgnoreCase))
            {
                int damage = Math.Max(exploreRandom.Next(8, 15) - Player.armor, 1);
                Player.currentHealth -= damage;
                ApplyPlayerBurn(2, 5);
                output = $"{enemy.currentEnemy} exhales Flame Breath for {damage} damage and sets you ablaze!\n";
                return true;
            }

            if (enemy.currentEnemy.Contains("Chimera", StringComparison.OrdinalIgnoreCase))
            {
                int damage = Math.Max(exploreRandom.Next(6, 13) - Player.armor, 1);
                Player.currentHealth -= damage;
                ApplyPlayerBleed(2, 4);
                output = $"{enemy.currentEnemy} uses Rending Claw for {damage} damage and inflicts bleed!\n";
                return true;
            }

            if (enemy.currentEnemy.Contains("Hydra", StringComparison.OrdinalIgnoreCase))
            {
                int damage = Math.Max(exploreRandom.Next(7, 14) - Player.armor, 1);
                Player.currentHealth -= damage;
                ApplyPlayerBleed(3, 3);
                output = $"{enemy.currentEnemy} spits venom for {damage} damage and worsening bleed!\n";
                return true;
            }

            if (enemy.currentEnemy.Contains("Cerberus", StringComparison.OrdinalIgnoreCase))
            {
                int damage = Math.Max(exploreRandom.Next(6, 12) - Player.armor, 1);
                Player.currentHealth -= damage;
                ApplyPlayerBurn(2, 4);
                output = $"{enemy.currentEnemy} lashes out with Hellfire for {damage} damage.\n";
                return true;
            }

            int defaultDamage = Math.Max(exploreRandom.Next(5, 11) - Player.armor, 1);
            Player.currentHealth -= defaultDamage;
            output = $"{enemy.currentEnemy} uses Savage Swipe for {defaultDamage} damage!\n";
            return true;
        }
    }
}
