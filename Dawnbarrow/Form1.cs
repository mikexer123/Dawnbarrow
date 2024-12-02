using System.DirectoryServices.ActiveDirectory;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Windows.Forms.VisualStyles;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic.FileIO;
namespace Dawnbarrow
{
    public partial class Dawnbarrow : Form
    {
        private System.Windows.Forms.Timer typingTimer;
        private int currentCharIndex = 0;
        private string currentOutput = "";
        Game game = new Game();
        Room room = new Room();
        Player Player = new Player();
        Item Item = new Item();
        Enemy enemy = new Enemy();
        public List<string> titemlist = new List<string>() { "Leather Helmet +1", "Iron Helmet +2", "Topaz Helmet +3", "Saviors Helmet +4", "Leather Chestplate +1", "Iron Chestplate +2", "Topaz Chestplate +3", "Saviors Chestplate +4", "Leather Leggings +1", "Iron Leggings +2", "Topaz Leggings +3", "Saviors Leggings +4", "Iron Sword +1", "Fire Sword +2", "Topaz Sword +3", "Saviors Sword +4", "Ladder", "Pickaxe", "Boss Key", "Talking Cat", "Friendship Bracelet" };
        public string[] itemname = { "Leather Helmet +1", "Iron Helmet +2", "Topaz Helmet +3", "Savior Helmet +4", "Leather Chestplate +1", "Iron Chestplate +2", "Topaz Chestplate +3", "Savior Chestplate +4", "Leather Leggings +1", "Iron Leggings +2", "Topaz Leggings +3", "Savior Leggings +4", "Iron Sword +1", "Fire Sword +2", "Topaz Sword +3", "Savior Sword +4", "Ladder", "Pickaxe", "Boss Key", "Talking Cat", "Friendship Bracelet", "Fire Bomb" };
        int[] itemtype = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 19, 20, 21 };

        public Dawnbarrow()
        {
            InitializeComponent();
            typingTimer = new System.Windows.Forms.Timer();
            typingTimer.Interval = 1; //typing speed
            typingTimer.Tick += TypingTimerTick;
            for (int i = 0; i < titemlist.Count; i++)
            {

                int itemid = titemlist.IndexOf(titemlist[i]) + 1;

            }

        }
        private void StartTyping(string text, bool append = false)
        {


            if (append == true)
            {

                currentOutput = ConsoleOut.Text + "\n" + text;
            }
            else
            {
                currentOutput = text;
            }
            currentCharIndex = 0;

            if (typingTimer.Enabled)
            {
                typingTimer.Stop();
            }

            typingTimer.Start();
        }
        private void TypingTimerTick(object sender, EventArgs e)
        {
            if (currentCharIndex < currentOutput.Length)
            {
                ConsoleOut.AppendText(currentOutput[currentCharIndex].ToString());
                currentCharIndex++;
            }
            else
            {
                typingTimer.Stop();
            }
        }
        private void submit_button_Click(object sender, EventArgs e)
        {


            string PlayerInput = InputBox.Text.Trim();
            string outputText = "";

            if (string.IsNullOrEmpty(PlayerInput))
            {
                outputText = "\nPlease enter a command";
                StartTyping(outputText, false);
                return;
            }

            string playerAction = $"\nYou typed: {PlayerInput}";
            string gameResponse = checkInput(PlayerInput);

            (int x, int y) nextroomCoordinates = room.GetNextRoomIndex(PlayerInput);


            {
                if ((room.CanGo(PlayerInput.ToLower()) && (PlayerInput == "north" || PlayerInput == "south" || PlayerInput == "east" || PlayerInput == "west" || PlayerInput == "North" || PlayerInput == "South" || PlayerInput == "East" || PlayerInput == "West")))
                {

                    room.setCurrentRoom(nextroomCoordinates.x, nextroomCoordinates.y);
                    game.setCurrentRoom(nextroomCoordinates.x, nextroomCoordinates.y);
                    string roomDescription = game.Output();
                    enemy.enemySpawn(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y);
                    outputText = $"{playerAction} \n {gameResponse}";
                    outputText += $"\n {roomDescription}";

                }
                else if ((room.CanGo(PlayerInput.ToLower()) == false) && (PlayerInput == "north" || PlayerInput == "south" || PlayerInput == "east" || PlayerInput == "west" || PlayerInput == "North" || PlayerInput == "South" || PlayerInput == "East" || PlayerInput == "West"))
                {
                    outputText = $"The path to the coords {nextroomCoordinates} is blocked off";
                    StartTyping(outputText, false);

                }
                else
                {
                    outputText = $"{playerAction} \n {gameResponse}";
                }

            }


            StartTyping(outputText, false);
            string whereami = room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y);
            label1.Text = whereami + room.getCurrentRoomCoordinates().ToString();
            

            updatelabels();
            InputBox.Clear();



        }
        public string checkInput(string input) // where the magic happens
        {
            string response = "";
            if ((input == "look around") || (input == "Look around") || (input == "see around") || (input == "search") || (input == "inspect surroundings"))
            {
                response = game.roomsubtext[game.currentRoomIndex]; //This is not the same as roomdescriptions! (this is for finding quest items and consumables)
            }
            else
            if (input.Contains("gender") || input.Contains("Gender")) //This is actually an entirely optional step in the game
            {
                input = input.Remove(0, 7);
                Player.storeGender(input);

                if ((Player.Gender == "Female") || (Player.Gender == "Male") || (Player.Gender == "male") || (Player.Gender == " female") || (Player.Gender == " Female") || (Player.Gender == " Male") || (Player.Gender == " male") || (Player.Gender == "female"))
                {
                    response = $"Your gender is {Player.Gender}\n";
                }
                else
                {
                    response = $"\"{Player.Gender}\" is not a real gender but okay, since this is a pretend game, you can be whatever you want";
                }
            }
            else
            if (input.Contains("name") || input.Contains("Name")) // This is ALSO an entirely optional step in the game.
            {
                input = input.Remove(0, 5);
                Player.storeName(input);
                response = $"Your name is {Player.playerName}\n";
                if (Player.playerName == "Mike" || Player.playerName == "Prasiddha" || Player.playerName == "Mike Lanier" || Player.playerName == "prasiddha" || Player.playerName == "Prasiddha Pokhrel" || Player.playerName == "prasiddha pokhrel")
                {
                    response += "You share a name with one of the developers! Isn't that exciting? \n";
                }
                if ((Player.playerName == "Emmy") || (Player.playerName == "Emilia") || (Player.playerName == "emmy"))
                {
                    response += "You share a name with one of the developers girlfriend! If you're her, Mike says \"I love you Emmy, I hope you're not procrastinating!\" \n";
                }
            }
            else
            if ((input == "south") || (input == "South") || (input == "SOUTH") || (input == "s") || (input == "S"))
            {
                response = "You start heading South";
            }
            else
            if ((input == "north") || (input == "North") || (input == "NORTH") || (input == "n") || (input == "N"))
            {
                response = "You start heading North!";
            }
            else
            if ((input == "east") || (input == "East") || (input == "EAST") || (input == "e") || (input == "E"))
            {
                response = "You start heading East";
            }
            else
            if ((input == "west") || (input == "West") || (input == "WEST") || (input == "w") || (input == "W")) // because i'm silly, some of these inputs don't work :) just upper and lower case.
            {
                response = "You start heading West";
            }
            else
            if ((input == "fight") || (input == "kill" || (input == "murder") || (input == "mordor") || input.Contains("fight"))) //Begin Fighting
            {
                response = "\n" + enemy.desc + "\n" + "You begin fighting\n" + enemy.MonsterInfo();
                Player.isFighting = true;
            }
            else
            if ((input == "check self" || input == "inspect self" || input == "stats" || input == "whoami")) //Player Card
            {
                response = Player.playerInfo();
            }
            else
            if (((input == "hit") || input == "slash" || input == "bap" || input == "fuckemup") && Player.isFighting == true) //typing hit while in combat
            {
                response = Combat();
            }
            else
            if (((input == "hit") || input == "slash" || input == "bap" || input == "fuckemup") && Player.isFighting == false) //typing hit while NOT in combat
            {
                response = $"There is nothing to {input}";
            }
            else
            if (input.Contains("Who is the cutest cat on the planet?"))
            {
                response = "Fun fact, there is a tie between the two cutest cat's on the planet! One is named Jojo, the other is named Toulouse.";
            }
            else
            if (input.Contains("Emmy"))
            {
                response = "What are you doing saying the game-creators girlfriend's name in this console?";
            }
            else
            if ((input == "") || (input == " "))
            {
                response = "You have to write something!";
            }
            else
            if (input == "die")
            {
                response = "I bet you thought I'd say you can't die huh?";
            }
            else
            if (input == "map")
            {
                response = game.getMap();
            }
            else
            if (input.Contains("equip")) // Main equip command
            {
                if ((input.Contains("iron sword")) || input.Contains("Iron Sword")) // Iron Sword
                {
                    response = "You equip iron sword";
                    Player.WeaponEquipped = "Iron Sword +1";
                }
                else
                if ((input.Contains("fire sword")) || input.Contains("Fire Sword")) // Fire Sword
                {
                    response = "You equip fire sword";
                    Player.WeaponEquipped = "Fire Sword +2";
                }
                else
                if ((input.Contains("topaz sword")) || input.Contains("Topaz Sword")) // Topaz Sword
                {
                    response = "You equip topaz sword";
                    Player.WeaponEquipped = "Topaz Sword +3";
                }
                else
                if ((input.Contains("savior sword")) || input.Contains("Savior Sword")) // Savior Sword
                {
                    response = "You equip savior sword";
                    Player.WeaponEquipped = "Savior Sword +4";
                }
                else
                if ((input.Contains("Leather Leggings")) || input.Contains("leather leggings")) // Leather leggings
                {
                    response = "You equip Leather Leggings";
                    Player.LegsEquipped = "Leather Leggings +1";
                }
                else
                if ((input.Contains("Iron Leggings")) || input.Contains("iron leggings")) //Iron Leggings
                {
                    response = "You equip Iron Leggings";
                    Player.LegsEquipped = "Iron Leggings +2";
                }
                else
                if ((input.Contains("Topaz Leggings")) || input.Contains("topaz leggings")) // Topaz Leggings
                {
                    response = "You equip Topaz Leggings";
                    Player.LegsEquipped = "Topaz Leggings +3";
                }
                else
                if ((input.Contains("Savior Leggings")) || input.Contains("savior leggings")) // Savior Leggings
                {
                    response = "You equip Savior Leggings +4";
                    Player.LegsEquipped = "Savior Leggings +4";
                }
                else
                if ((input.Contains("Leather Chestplate")) || input.Contains("leather chestplate")) // Leather Chestplate
                {
                    response = "You equip Leather Chestplate";
                    Player.ChestEquipped = "Leather Chestplate +1";
                }
                else
                if ((input.Contains("Iron Chestplate")) || input.Contains("iron chestplate")) //Iron Chestplate
                {
                    response = "You equip Iron Chestplate";
                    Player.ChestEquipped = "Iron Chestplate +2";
                }
                else
                if ((input.Contains("topaz chestplate")) || input.Contains("Topaz Chestplate")) //Topaz Chestplate
                {
                    response = "You equip Topaz Chestplate";
                    Player.ChestEquipped = "Topaz Chestplate +3";
                }
                else
                if ((input.Contains("savior chestplate")) || input.Contains("Savior Chestplate")) //Savior Chestplate
                {
                    response = "You equip Savior Chestplate";
                    Player.ChestEquipped = "Savior Chestplate +4";
                }
                else
                if ((input.Contains("Leather Helmet")) || input.Contains("leather helmet")) //Leather Chestplate
                {
                    response = "You equip Leather Helmet +1";
                    Player.HeadEquipped = "Leather Helmet +1";
                }
                else
                if ((input.Contains("Iron Helmet")) || input.Contains("iron helmet")) //Iron Helmet
                {
                    response = "You equip Iron Helmet +2";
                    Player.HeadEquipped = "Iron Helmet +2";
                }
                else
                if ((input.Contains("topaz helmet")) || input.Contains("Topaz Helmet")) //Topaz Helmet
                {
                    response = "You equip topaz sword";
                    Player.HeadEquipped = "Topaz Sword +3";
                }
                else
                if ((input.Contains("savior helmet")) || input.Contains("Savior Helmet")) // Savior Helmet
                {
                    response = "You equip savior sword";
                    Player.HeadEquipped = "Savior Sword +4";
                }
                else
                {
                    input = input.Remove(0, 6);
                    response = "You don't have a " + input + " to equip";
                }
            } // MAIN EQUIP COMMAND!
            else
            {
                response = $"You can't {input}";
            }
            return response;
        }



        public string Combat()
        {
            string output = "";

            output += Player.playerTurn(enemy.currentEnemy, enemy.enemyArmor) + "\n";
            enemy.enemyCHP -= Player.playerDmg(enemy.enemyArmor);
            output += "The enemy has" + enemy.enemyCHP + "/" + enemy.enemyHP + "Hit Points Remaining \n";

            if (enemy.enemyCHP <= 0)
            {
                enemy.enemyCHP = 0;
                output += "You have sucessfully defeated the enemy!\n";
                output += Player.experience(enemy.enemyxpgiven);
                Player.isFighting = false;
            }

            if (enemy.enemyCHP != 0)
            {
                output += enemy.MonsterTurn(Player.armor) + "\n";
                Player.currentHealth -= enemy.MonsterDmg(Player.armor);
                output += "The player has" + Player.currentHealth + "/" + Player.maxhealth + "Hit Points Remaining \n";
            }

            if (Player.currentHealth <= 0)
            {
                output += "You have 0 health remaining, you cannot continue, the evil forces of Dawnbarrow grow stronger. Please exit the game and start again.";
            }


            return output;
        }
        private void ConsoleOut_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Dawnbarrow_Load(object sender, EventArgs e)
        {

        }

        private void InputBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void InputBox_MouseClick(object sender, MouseEventArgs e)
        {
            { InputBox.Text = ""; }
        }
        public void updatelabels()
        { //every label in the game except currcoordinates lol
            PlayerHP.Text = $"CurrHP: {Player.currentHealth} / TotHP: {Player.maxhealth}";
            XP.Text = $"CurrXP: {Player.currentxp} / Xp2nex: {Player.xptonextlevel}";
            //ArAt.Text = $"Armor: {Player.armor} /  Attack: {Player.dmg}";
            NGL.Text = $"{Player.playerName} / {Player.Gender} / Lvl: {Player.lvl}";
            Equip.Text = $"Currently Equipped:\n {Player.HeadEquipped},\n {Player.ChestEquipped},\n {Player.LegsEquipped},\n {Player.WeaponEquipped}";
            bryant.Text = $"Item: {Item.itemvariable(itemname, 2)}";
        }

        private void ArAt_Click(object sender, EventArgs e)
        {

        }


    }
}