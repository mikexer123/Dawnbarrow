using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
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


        public Dawnbarrow()
        {
            InitializeComponent();
            typingTimer = new System.Windows.Forms.Timer();
            typingTimer.Interval = 2; //typing speed
            typingTimer.Tick += TypingTimerTick;

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
            (int x, int y) nextroom = room.GetNextRoomIndex(PlayerInput);
            (int x, int y) currentRoom = (room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y);
            game.setcurrentRoomIndex(currentRoom.x,currentRoom.y);
            //empty command
            if (string.IsNullOrEmpty(PlayerInput) )
            {
                StartTyping(" \n Please enter a command", false);
                return;
            }

            string PlayerIO = $"\n >You typed  {PlayerInput}  \n";
            string gameResponse = game.checkInput(PlayerInput);
            string friend = game.getcurrentRoomIndex().ToString() + " " + nextroom.ToString();
            label1.Text = room.getCurrentRoomCoordinates().ToString();
            string newOutput = PlayerIO + gameResponse;
            
            if (string.IsNullOrEmpty(gameResponse)) //unrecognized command
            {
                newOutput += "I didn't understand your command";
            }

            StartTyping(newOutput + " " + friend, false);
            room.setCurrentRoom(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y);
            InputBox.Clear();
            

            

            //if (room.CanGo(PlayerInput))
            //{
            //    room.setCurrentRoom(nextroom.x, nextroom.y);
            //    game.setCurrentRoom(nextroom.x, nextroom.y);
            //    string roomDescription = game.Output();
            //    StartTyping($"{roomDescription} \n You move {PlayerInput} to coordinates {nextroom}", false);
            //}
            //else
            //{
            //    StartTyping($"The path to the coords {nextroom} is blocked off", false);
            //    room.getCurrentRoomCoordinates();
            //    game.getCurrentRoom();
            //}
            
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
    }
}