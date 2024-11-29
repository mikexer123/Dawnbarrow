using System.DirectoryServices.ActiveDirectory;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Windows.Forms.VisualStyles;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
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

            string playerAction = $"\n You typed: {PlayerInput}";
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
        public string checkInput(string input)
        {
            string response = "";
            if ((input == "look around") || (input == "Look around") || (input == "see around") || (input == "search") || (input == "inspect surroundings"))
            {
                response = game.roomsubtext[game.currentRoomIndex];
            }
            else
            if (input.Contains("gender") || input.Contains("Gender"))
            {
                input = input.Remove(0, 7);
                Player.storeGender(input);
                response = $"Your gender is {Player.Gender}\n";

            }
            else
            if ( input.Contains("name") || input.Contains("Name") )
            {
                input = input.Remove(0, 5);
                Player.storeName(input);
                response = $"Your name is {Player.playerName}\n";
                if (Player.playerName == "Mike" || Player.playerName == "Prasiddha" || Player.playerName == "Mike Lanier" || Player.playerName == "prasiddha" || Player.playerName == "Prasiddha Pokhrel" || Player.playerName == "prasiddha pokhrel")
                {
                    response += "You share a name with one of the developers! Isn't that exciting?";
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
            if ((input == "west") || (input == "West") || (input == "WEST") || (input == "w") || (input == "W"))
            {
                response = "You start heading West";
            }
            else
            if ((input == "fight") || (input == "kill" || (input == "murder") || (input == "mordor") || input.Contains("fight"))) //Begin Fighting
            {
                response = "You begin fighting\n" + enemy.MonsterInfo();
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
                response = "What are you doing saying the game-creators name in this console?";
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
            ArAt.Text = $"Armor: {Player.armor} /  Attack: {Player.dmg}";
            NGL.Text = $"{Player.playerName} / {Player.Gender} / Lvl: {Player.lvl}";
        }

        private void ArAt_Click(object sender, EventArgs e)
        {

        }
    }
}