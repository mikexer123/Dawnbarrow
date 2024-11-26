using System.DirectoryServices.ActiveDirectory;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Windows.Forms.VisualStyles;
using System.ComponentModel.Design;
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
            typingTimer.Interval = 20; //typing speed
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

            string playerAction = $"\n You typed :{PlayerInput}";
            string gameResponse = checkInput(PlayerInput);

            (int x, int y) nextroomCoordinates = room.GetNextRoomIndex(PlayerInput);


            {
                if ((room.CanGo(PlayerInput.ToLower()) && (PlayerInput == "north" || PlayerInput == "south" || PlayerInput == "east" || PlayerInput == "west")))
                {

                    room.setCurrentRoom(nextroomCoordinates.x, nextroomCoordinates.y);
                    game.setCurrentRoom(nextroomCoordinates.x, nextroomCoordinates.y);
                    string roomDescription = game.Output();

                    outputText = $"{playerAction} \n {gameResponse}";
                    outputText += $"\n {roomDescription}";

                }
                else if ( (room.CanGo(PlayerInput.ToLower()) == false) && (PlayerInput == "north" || PlayerInput == "south" || PlayerInput == "east" || PlayerInput == "west") )
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
            enemy.enemySpawn(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y);

            InputBox.Clear();



        }
        public string checkInput(string input)
        {
            string response = "";
            if ((input == "look around") || (input == "Look around") || (input == "see around") || (input == "search") || (input == "inspect surroundings"))
            {
                response = game.roomDescriptions[game.currentRoomIndex];
            }
            if ((input == "south") || (input == "South") || (input == "SOUTH") || (input == "s") || (input == "S"))
            {
                response = "You start heading South";
            }
            if ((input == "north") || (input == "North") || (input == "NORTH") || (input == "n") || (input == "N"))
            {
                response = "You start heading North!";
            }
            if ((input == "east") || (input == "East") || (input == "EAST") || (input == "e") || (input == "E"))
            {
                response = "You start heading East";
            }
            if ((input == "west") || (input == "West") || (input == "WEST") || (input == "w") || (input == "W"))
            {
                response = "You start heading West";
            }
            if ((input == "fight") || (input == "kill" || (input == "murder") || (input == "mordor")))
            {
                response = "You begin fighting\n" + enemy.MonsterInfo();
            }

            return response;
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
    }
}