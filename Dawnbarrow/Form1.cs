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
            Player.calculateArmor();
            updateBackground();



        }
        public string checkInput(string input) // where the magic happens
        {
            string response = "";
            if ((input == "look around") || (input == "Look around") || (input == "see around") || (input == "search") || (input == "inspect surroundings") || (input == "Look Around"))
            {
                response = game.roomsubtext[game.currentRoomIndex]; //This is not the same as roomdescriptions! (this is for finding quest items and consumables)
            }
            else
            if (input.Contains("gender") || input.Contains("Gender")) //This is actually an entirely optional step in the game
            {
                input = input.Remove(0, 7);
                Player.storeGender(input);
                response = $"Your gender is {Player.Gender}\n";
            }
            else
            if (input.Contains("name") || input.Contains("Name")) // This is ALSO an entirely optional step in the game.
            {
                input = input.Remove(0, 5);
                Player.storeName(input);
                response = $"Your name is {Player.playerName}\n";
                if (Player.playerName == "Jon Scott")
                {
                    response = $"Your name is {Player.playerName}\n";
                    response = "You're a little gay boy aren't you ? :)";
                }
                if (Player.playerName == "Mike" || Player.playerName == "Prasiddha" || Player.playerName == "Mike Lanier" || Player.playerName == "prasiddha" || Player.playerName == "Prasiddha Pokhrel" || Player.playerName == "prasiddha pokhrel")
                {
                    response = $"Your name is {Player.playerName}\n";
                    response += "You share a name with one of the developers! Isn't that exciting? \n";
                }
                if (Player.playerName == "Barbara Bancroft")
                {
                    response = $"Your name is {Player.playerName}\n";
                    response = $"You're our app development professor!";
                }
                if ( (Player.playerName == "Alexander Juxley") || (Player.playerName == "Jonathan Lanteigne") || (Player.playerName == "Tony Allam") || (Player.playerName == "Nicholas Harrington") || (Player.playerName == "Jonathan Rowe") || (Player.playerName == "Graham Zambrowicz") || (Player.playerName == "Gabriel Bashaw") || (Player.playerName == "Alexander Pessinis") || (Player.playerName == "Tony Allam") || (Player.playerName == "Sam Do") || (Player.playerName == "Reginald Hardwick"))
                {
                    response = $"Your name is {Player.playerName}\n";
                    response = "You are in our app development class! You saw our developmental process!";
                }
                if ((Player.playerName == "Emmy") || (Player.playerName == "Emilia") || (Player.playerName == "emmy"))
                {
                    response = $"Your name is {Player.playerName}\n";
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
            if ((input == "fight") || (input == "kill" || (input == "murder") || (input == "mordor") || input.Contains("fight") || input.Contains("Fight"))) //Begin Fighting
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
            if ((input.Contains("equip")) || (input.Contains("Equip"))) // Main equip command
            {
                if (((input.Contains("iron sword")) || input.Contains("Iron Sword")) && (Player.hasIronSword == true)) // Iron Sword
                {
                    response = "You equip Iron sword";
                    Player.WeaponEquipped = "Iron Sword +1";
                }
                else
                if (((input.Contains("fire sword")) || input.Contains("Fire Sword")) && (Player.hasFireSword == true)) // Fire Sword
                {
                    response = "You equip Fire sword";
                    Player.WeaponEquipped = "Fire Sword +2";
                }
                else
                if (((input.Contains("topaz sword")) || input.Contains("Topaz Sword")) && (Player.hasTopazSword == true)) // Topaz Sword
                {
                    response = "You equip Topaz sword";
                    Player.WeaponEquipped = "Topaz Sword +3";
                }
                else
                if (((input.Contains("savior sword")) || input.Contains("Savior Sword")) && (Player.hasSaviorSword == true)) // Savior Sword
                {
                    response = "You equip Savior sword";
                    Player.WeaponEquipped = "Savior Sword +4";
                }
                else
                if (((input.Contains("Leather Leggings")) || input.Contains("leather leggings")) && (Player.hasLeatherLeggings == true))// Leather leggings
                {
                    response = "You equip Leather Leggings";
                    Player.LegsEquipped = "Leather Leggings +1";
                }
                else
                if (((input.Contains("Iron Leggings")) || input.Contains("iron leggings")) && (Player.hasIronLeggings == true))//Iron Leggings
                {
                    response = "You equip Iron Leggings";
                    Player.LegsEquipped = "Iron Leggings +2";
                }
                else
                if (((input.Contains("Topaz Leggings")) || input.Contains("topaz leggings")) && (Player.hasTopazLeggings == true))// Topaz Leggings
                {
                    response = "You equip Topaz Leggings";
                    Player.LegsEquipped = "Topaz Leggings +3";
                }
                else
                if (((input.Contains("Savior Leggings")) || input.Contains("savior leggings")) && (Player.hasSaviorLeggings == true)) // Savior Leggings
                {
                    response = "You equip Savior Leggings +4";
                    Player.LegsEquipped = "Savior Leggings +4";
                }
                else
                if (((input.Contains("Leather Chestplate")) || input.Contains("leather chestplate")) && (Player.hasLeatherChestplate == true)) // Leather Chestplate
                {
                    response = "You equip Leather Chestplate";
                    Player.ChestEquipped = "Leather Chestplate +1";
                }
                else
                if (((input.Contains("Iron Chestplate")) || input.Contains("iron chestplate")) && (Player.hasIronChestplate == true))//Iron Chestplate
                {
                    response = "You equip Iron Chestplate";
                    Player.ChestEquipped = "Iron Chestplate +2";
                }
                else
                if (((input.Contains("topaz chestplate")) || input.Contains("Topaz Chestplate")) && (Player.hasTopazChestplate == true)) //Topaz Chestplate
                {
                    response = "You equip Topaz Chestplate";
                    Player.ChestEquipped = "Topaz Chestplate +3";
                }
                else
                if (((input.Contains("savior chestplate")) || input.Contains("Savior Chestplate")) && (Player.hasSaviorChestplate == true)) //Savior Chestplate
                {
                    response = "You equip Savior Chestplate";
                    Player.ChestEquipped = "Savior Chestplate +4";
                }
                else
                if (((input.Contains("Leather Helmet")) || input.Contains("leather helmet")) && (Player.hasLeatherHelmet == true)) //Leather Helmet
                {
                    response = "You equip Leather Helmet +1";
                    Player.HeadEquipped = "Leather Helmet +1";
                }
                else
                if (((input.Contains("Iron Helmet")) || input.Contains("iron helmet")) && (Player.hasIronHelmet == true)) //Iron Helmet
                {
                    response = "You equip Iron Helmet +2";
                    Player.HeadEquipped = "Iron Helmet +2";
                }
                else
                if (((input.Contains("topaz helmet")) || input.Contains("Topaz Helmet")) && (Player.hasTopazHelmet == true)) //Topaz Helmet
                {
                    response = "You equip topaz sword";
                    Player.HeadEquipped = "Topaz Helmet +3";
                }
                else
                if (((input.Contains("savior helmet")) || input.Contains("Savior Helmet")) && (Player.hasSaviorHelmet == true)) // Savior Helmet
                {
                    response = "You equip savior Helmet";
                    Player.HeadEquipped = "Savior Helmet +4";
                }
                else
                {
                    input = input.Remove(0, 6);
                    response = "You don't have a " + input + " to equip";
                }
            } 
            else
            if ((input.Contains("pick up") || input.Contains("Pick up") || input.Contains("Search Ground") || input.Contains("search ground")) && enemy.isdefeated == true)
            {
                response = $"You pick up the {enemy.placedObject}";
                Player.giveItem(enemy.placedObject);
            }
            else
            if (input.Contains("Display Commands") || input.Contains("display commands") || input.Contains("show commands")) //display commands
            {
                MessageBox.Show(" Look around ---> gain more information about your surroundings \n Gender (gender) ---> input your gender \n name (name) ---> Input your name \n " +
                    "Fight ---> Fight the current monster in the room \n Hit ---> Hit the current monster (must first be fighting) \n check self ---> learn more information about yourself \n" +
                    " Inventory ---> look at your inventory \n equip (item) ---> toggle current equipment \n " +
                    "Search Ground ---> Pick up items on the ground \n North, South, East, West ---> Walk in direction written", "Dawnbarrow Commands");

            }
            else
            if (input.Contains("Display Hidden Commands") || input.Contains("display hidden commands") || input.Contains("show hidden commands"))
            {
                MessageBox.Show("Who is the cutest cat on the planet? ---> Find out \n die ---> Find out if you can die! \n Cheat ---> Gives full savior kit (ur a terrible person)", "Dawnbarrow Hidden Commands (ur sneaky)");
            }
            else
            if ((input.Contains("pick up") || input.Contains("Pick up") || input.Contains("Search Ground") || input.Contains("search ground")) && enemy.isdefeated == false)
            {
                response = "It's not safe enough to pick up the item in this room!";
            }
            else
            if (input.Contains("Inventory") || input.Contains("inventory"))
            {
                response = Player.displayInventory();
            }
            else
            if (input.Contains("give friendship bracelet") && Player.hasFriendshipBracelet == true)
            {
                enemy.enemyCHP = 0;
                response = "You made the Lonely Giant happy, and thus he became your friend, feel free to pick up the saviors helmet before you go!";
            }
            else
            if (input.Contains("Cheat") || input.Contains("cheat"))
            {
                response = $"You have cheated!!! You have all quest items and all four pieces of the savior set";
                Player.cheat();
            }
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
                enemy.isdefeated = true;
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
            //bryant.Text = $"Item: {Item.itemvariable(itemname, 2)}";
        }

        private void ArAt_Click(object sender, EventArgs e)
        {

        }

        private void Background_Click(object sender, EventArgs e)
        {

        }
        public void updateBackground()
        {
            if (room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y) == "Forest")
            {
                Background.Image = Properties.Resources.Forest1;
            }
            else
            if (room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y) == "Jungle")
            {
                Background.Image = Properties.Resources._51;
            }
            else
            if (room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y) == "Grassland")
            {
                Background.Image = Properties.Resources.grassland;
            }
            else
            if (room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y) == "River")
            {
                Background.Image = Properties.Resources.River;
            }
            else
            if (room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y) == "Mountain Pass")
            {
                Background.Image = Properties.Resources.Mountain_Pass___Ending;
            }
            
            if (Player.hasFriendshipBracelet == true)
            {
                FriendshipBracelet.Image = Properties.Resources.FriendshipBraceletOn;
            }
            if (Player.hasPickaxe == true)
            {
                Pickaxe.Image = Properties.Resources.PickaxeOn;
            }
            if (Player.hasTalkingCat == true)
            {
                TalkingCat.Image = Properties.Resources.TalkingCatOn;
            }
            if (Player.hasLadder == true)
            {
                Ladder.Image = Properties.Resources.LadderOn;
            }
            if (Player.hasBossKey == true)
            {
                BossKey.Image = Properties.Resources.BosskeyON;
            }
        }
    }
}