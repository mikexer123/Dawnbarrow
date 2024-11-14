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
            string outputText = "";

            if (string.IsNullOrEmpty(PlayerInput))
            {
                outputText = "\nPlease enter a command";
                StartTyping(outputText, false);
                return;
            }

            string playerAction = $"\n You typed :{PlayerInput}";
            string gameResponse = game.checkInput(PlayerInput);

            (int x, int y) nextroomCoordinates = room.GetNextRoomIndex(PlayerInput);
             

            {
                if (room.CanGo(PlayerInput.ToLower()))
                {

                    room.setCurrentRoom(nextroomCoordinates.x, nextroomCoordinates.y);
                    game.setCurrentRoom(nextroomCoordinates.x, nextroomCoordinates.y);
                    string roomDescription = game.Output();

                    outputText = $"{playerAction} \n {gameResponse}";
                    outputText += $"\n {roomDescription}";

                }
                else if (room.CanGo(PlayerInput.ToLower()) == false)
                { outputText = $"The path to the coords {nextroomCoordinates} is blocked off";
                  StartTyping(outputText, false);
                        
                }
               
                }
               

            StartTyping(outputText, false);
            label1.Text = room.getCurrentRoomCoordinates().ToString();
            
            InputBox.Clear();
            
         

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