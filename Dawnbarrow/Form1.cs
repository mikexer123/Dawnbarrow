using System.DirectoryServices.ActiveDirectory;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Text.Json;
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
        private const int charactersPerTick = 4;
        private System.Windows.Forms.Timer typingTimer;
        private readonly Queue<string> typingQueue = new Queue<string>();
        private readonly HashSet<string> defeatedRooms = new HashSet<string>();
        private readonly Random exploreRandom = new Random();
        private readonly List<string> exploreItems = new List<string>() { "Leather Helmet +1", "Leather Chestplate +1", "Leather Leggings +1", "Iron Sword +1", "Iron Helmet +2", "Iron Chestplate +2", "Iron Leggings +2", "Fire Sword +2", "Topaz Helmet +3", "Topaz Chestplate +3", "Topaz Leggings +3", "Topaz Sword +3" };
        private int currentCharIndex = 0;
        private string currentOutput = "";
        Game game = new Game();
        Room room = new Room();
        Player Player = new Player();
        Enemy enemy = new Enemy();
        public List<string> titemlist = new List<string>() { "Leather Helmet +1", "Iron Helmet +2", "Topaz Helmet +3", "Saviors Helmet +4", "Leather Chestplate +1", "Iron Chestplate +2", "Topaz Chestplate +3", "Saviors Chestplate +4", "Leather Leggings +1", "Iron Leggings +2", "Topaz Leggings +3", "Saviors Leggings +4", "Iron Sword +1", "Fire Sword +2", "Topaz Sword +3", "Saviors Sword +4", "Ladder", "Pickaxe", "Boss Key", "Talking Cat", "Friendship Bracelet" };
        public string[] itemname = { "Leather Helmet +1", "Iron Helmet +2", "Topaz Helmet +3", "Savior Helmet +4", "Leather Chestplate +1", "Iron Chestplate +2", "Topaz Chestplate +3", "Savior Chestplate +4", "Leather Leggings +1", "Iron Leggings +2", "Topaz Leggings +3", "Savior Leggings +4", "Iron Sword +1", "Fire Sword +2", "Topaz Sword +3", "Savior Sword +4", "Ladder", "Pickaxe", "Boss Key", "Talking Cat", "Friendship Bracelet", "Fire Bomb" };
        int[] itemtype = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 19, 20, 21 };

        private sealed class SaveData
        {
            public int RoomX { get; set; }
            public int RoomY { get; set; }
            public int CurrentRoomIndex { get; set; }
            public string ConsoleText { get; set; } = "";
            public List<string> DefeatedRooms { get; set; } = new List<string>();
            public PlayerSaveData Player { get; set; } = new PlayerSaveData();
            public EnemySaveData Enemy { get; set; } = new EnemySaveData();
        }

        private sealed class PlayerSaveData
        {
            public string PlayerName { get; set; } = "";
            public string Gender { get; set; } = "";
            public int Level { get; set; }
            public double MaxHealth { get; set; }
            public double CurrentHealth { get; set; }
            public int Armor { get; set; }
            public int Damage { get; set; }
            public string HeadEquipped { get; set; } = "nothing";
            public string ChestEquipped { get; set; } = "nothing";
            public string LegsEquipped { get; set; } = "nothing";
            public string WeaponEquipped { get; set; } = "nothing";
            public bool IsFighting { get; set; }
            public double XpToNextLevel { get; set; }
            public double CurrentXp { get; set; }
            public bool HasLadder { get; set; }
            public bool HasPickaxe { get; set; }
            public bool HasBossKey { get; set; }
            public bool HasTalkingCat { get; set; }
            public bool HasFriendshipBracelet { get; set; }
            public bool HasIronSword { get; set; }
            public bool HasFireSword { get; set; }
            public bool HasTopazSword { get; set; }
            public bool HasSaviorSword { get; set; }
            public bool HasLeatherHelmet { get; set; }
            public bool HasIronHelmet { get; set; }
            public bool HasTopazHelmet { get; set; }
            public bool HasSaviorHelmet { get; set; }
            public bool HasLeatherChestplate { get; set; }
            public bool HasIronChestplate { get; set; }
            public bool HasTopazChestplate { get; set; }
            public bool HasSaviorChestplate { get; set; }
            public bool HasLeatherLeggings { get; set; }
            public bool HasIronLeggings { get; set; }
            public bool HasTopazLeggings { get; set; }
            public bool HasSaviorLeggings { get; set; }
        }

        private sealed class EnemySaveData
        {
            public int EnemyHP { get; set; }
            public int EnemyCHP { get; set; }
            public int EnemyArmor { get; set; }
            public int EnemyDamage { get; set; }
            public string CurrentEnemy { get; set; } = "";
            public int EnemyXpGiven { get; set; }
            public string Description { get; set; } = "";
            public string PlacedObject { get; set; } = "";
            public bool IsDefeated { get; set; }
            public bool NeedsTalkingCat { get; set; }
            public bool NeedsLadder { get; set; }
            public bool NeedsPickaxe { get; set; }
            public bool NeedsBossKey { get; set; }
            public bool NeedsFriendshipBracelet { get; set; }
        }

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

            room.setCurrentRoom(1, 1);
            game.setCurrentRoom(1, 1);
            enemy.enemySpawn(1, 1);
            label1.Text = room.Biome(1, 1) + room.getCurrentRoomCoordinates().ToString();
            updateBackground();
            updatelabels();

        }
        private void StartTyping(string text, bool append = false)
        {


            string textToQueue = append == true ? "\n" + text : text;

            if (typingTimer.Enabled || currentCharIndex < currentOutput.Length)
            {
                typingQueue.Enqueue(textToQueue);
                return;
            }

            currentOutput = textToQueue;
            currentCharIndex = 0;
            typingTimer.Start();
        }
        private void TypingTimerTick(object? sender, EventArgs e)
        {
            if (currentCharIndex < currentOutput.Length)
            {
                int charsToWrite = Math.Min(charactersPerTick, currentOutput.Length - currentCharIndex);
                ConsoleOut.AppendText(currentOutput.Substring(currentCharIndex, charsToWrite));
                currentCharIndex += charsToWrite;
            }
            else
            {
                if (typingQueue.Count > 0)
                {
                    currentOutput = typingQueue.Dequeue();
                    currentCharIndex = 0;
                }
                else
                {
                    typingTimer.Stop();
                }
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
            string normalizedDirection = room.direction(PlayerInput);
            bool isMovementCommand = normalizedDirection == "north" || normalizedDirection == "south" || normalizedDirection == "east" || normalizedDirection == "west";

            (int x, int y) nextroomCoordinates = room.GetNextRoomIndex(normalizedDirection);


            {
                if (isMovementCommand && Player.isFighting == true)
                {
                    outputText = $"{playerAction} \n You legit can't go that way right now you're in a fight, so stop trying to be sneaky, use the run command if you want to leave combat, you sissy!";
                }
                else if (isMovementCommand && room.CanGo(normalizedDirection))
                {

                    room.setCurrentRoom(nextroomCoordinates.x, nextroomCoordinates.y);
                    game.setCurrentRoom(nextroomCoordinates.x, nextroomCoordinates.y);
                    string roomDescription = game.Output();
                    string roomEncounterText = PrepareRoomEncounter();
                    outputText = $"{playerAction} \n {gameResponse}";
                    outputText += $"\n {roomDescription}";
                    outputText += roomEncounterText;

                }
                else if (isMovementCommand && room.CanGo(normalizedDirection) == false)
                {
                    outputText = $"{playerAction} \n The path to the coords {nextroomCoordinates} is blocked off";
                }
                else
                {
                    outputText = $"{playerAction} \n {gameResponse}";
                }

            }


            StartTyping(outputText, false);
            string whereami = room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y);
            label1.Text = whereami + room.getCurrentRoomCoordinates().ToString();


            
            InputBox.Clear();
            Player.calculateArmor();
            updateBackground();
            updatelabels();



        }
        private static bool MatchesAny(string value, params string[] options)
        {
            return options.Contains(value);
        }

        private static bool ContainsAny(string value, params string[] options)
        {
            return options.Any(value.Contains);
        }

        private bool TryGetNamedValue(string input, string keyword, out string value)
        {
            int keywordIndex = input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase);

            if (keywordIndex < 0)
            {
                value = "";
                return false;
            }

            value = input.Substring(keywordIndex + keyword.Length).Trim();
            return true;
        }

        private string BuildNameResponse()
        {
            string response = $"Your name is {Player.playerName}\n";

            if (Player.playerName == "Jon Scott")
            {
                return "You're a little gay boy aren't you ? :)";
            }

            if (Player.playerName == "Mike" || Player.playerName == "Prasiddha" || Player.playerName == "Mike Lanier" || Player.playerName == "prasiddha" || Player.playerName == "Prasiddha Pokhrel" || Player.playerName == "prasiddha pokhrel")
            {
                return response + "You share a name with one of the developers! Isn't that exciting? \n";
            }

            if (Player.playerName == "Barbara Bancroft")
            {
                return "You're our app development professor!";
            }

            if ((Player.playerName == "Alexander Juxley") || (Player.playerName == "Jonathan Lanteigne") || (Player.playerName == "Tony Allam") || (Player.playerName == "Nicholas Harrington") || (Player.playerName == "Jonathan Rowe") || (Player.playerName == "Graham Zambrowicz") || (Player.playerName == "Gabriel Bashaw") || (Player.playerName == "Alexander Pessinis") || (Player.playerName == "Tony Allam") || (Player.playerName == "Sam Do") || (Player.playerName == "Reginald Hardwick"))
            {
                return "You are in our app development class! You saw our developmental process!";
            }

            if ((Player.playerName == "Emmy") || (Player.playerName == "Emilia") || (Player.playerName == "emmy"))
            {
                return response + "You share a name with one of the developers girlfriend! If you're her, Mike says \"I love you Emmy, I hope you're not procrastinating!\" \n";
            }

            return response;
        }

        private bool TryGetMovementResponse(string lowerInput, out string response)
        {
            response = "";

            if (MatchesAny(lowerInput, "south", "s"))
            {
                response = "You start heading South";
                return true;
            }

            if (MatchesAny(lowerInput, "north", "n"))
            {
                response = "You start heading North!";
                return true;
            }

            if (MatchesAny(lowerInput, "east", "e"))
            {
                response = "You start heading East";
                return true;
            }

            if (MatchesAny(lowerInput, "west", "w"))
            {
                response = "You start heading West";
                return true;
            }

            return false;
        }

        private string HandleEquipCommand(string input, string lowerInput)
        {
            if (lowerInput.Contains("iron sword") && Player.hasIronSword == true)
            {
                Player.WeaponEquipped = "Iron Sword +1";
                return "You equip Iron sword";
            }
            if (lowerInput.Contains("fire sword") && Player.hasFireSword == true)
            {
                Player.WeaponEquipped = "Fire Sword +2";
                return "You equip Fire sword";
            }
            if (lowerInput.Contains("topaz sword") && Player.hasTopazSword == true)
            {
                Player.WeaponEquipped = "Topaz Sword +3";
                return "You equip Topaz sword";
            }
            if (lowerInput.Contains("savior sword") && Player.hasSaviorSword == true)
            {
                Player.WeaponEquipped = "Savior Sword +4";
                return "You equip Savior sword";
            }
            if (lowerInput.Contains("leather leggings") && Player.hasLeatherLeggings == true)
            {
                Player.currentHealth += 25;
                Player.maxhealth += 25;
                Player.LegsEquipped = "Leather Leggings +1";
                return "You equip Leather Leggings";
            }
            if (lowerInput.Contains("iron leggings") && Player.hasIronLeggings == true)
            {
                Player.currentHealth += 50;
                Player.maxhealth += 50;
                Player.LegsEquipped = "Iron Leggings +2";
                return "You equip Iron Leggings";
            }
            if (lowerInput.Contains("topaz leggings") && Player.hasTopazLeggings == true)
            {
                Player.currentHealth += 75;
                Player.maxhealth += 75;
                Player.LegsEquipped = "Topaz Leggings +3";
                return "You equip Topaz Leggings";
            }
            if (lowerInput.Contains("savior leggings") && Player.hasSaviorLeggings == true)
            {
                Player.currentHealth += 100;
                Player.maxhealth += 100;
                Player.LegsEquipped = "Savior Leggings +4";
                return "You equip Savior Leggings +4";
            }
            if (lowerInput.Contains("leather chestplate") && Player.hasLeatherChestplate == true)
            {
                Player.currentHealth += 25;
                Player.maxhealth += 25;
                Player.ChestEquipped = "Leather Chestplate +1";
                return "You equip Leather Chestplate";
            }
            if (lowerInput.Contains("iron chestplate") && Player.hasIronChestplate == true)
            {
                Player.currentHealth += 50;
                Player.maxhealth += 50;
                Player.ChestEquipped = "Iron Chestplate +2";
                return "You equip Iron Chestplate";
            }
            if (lowerInput.Contains("topaz chestplate") && Player.hasTopazChestplate == true)
            {
                Player.currentHealth += 75;
                Player.maxhealth += 75;
                Player.ChestEquipped = "Topaz Chestplate +3";
                return "You equip Topaz Chestplate";
            }
            if (lowerInput.Contains("savior chestplate") && Player.hasSaviorChestplate == true)
            {
                Player.currentHealth += 100;
                Player.maxhealth += 100;
                Player.ChestEquipped = "Savior Chestplate +4";
                return "You equip Savior Chestplate";
            }
            if (lowerInput.Contains("leather helmet") && Player.hasLeatherHelmet == true)
            {
                Player.HeadEquipped = "Leather Helmet +1";
                Player.currentHealth += 25;
                Player.maxhealth += 25;
                return "You equip Leather Helmet +1";
            }
            if (lowerInput.Contains("iron helmet") && Player.hasIronHelmet == true)
            {
                Player.HeadEquipped = "Iron Helmet +2";
                Player.currentHealth += 50;
                Player.maxhealth += 50;
                return "You equip Iron Helmet +2";
            }
            if (lowerInput.Contains("topaz helmet") && Player.hasTopazHelmet == true)
            {
                Player.currentHealth += 75;
                Player.maxhealth += 75;
                Player.HeadEquipped = "Topaz Helmet +3";
                return "You equip Topaz Helmet +3";
            }
            if (lowerInput.Contains("savior helmet") && Player.hasSaviorHelmet == true)
            {
                Player.currentHealth += 100;
                Player.maxhealth += 100;
                Player.HeadEquipped = "Savior Helmet +4";
                return "You equip savior Helmet";
            }
            if (lowerInput.Contains("savior set"))
            {
                Player.HeadEquipped = "Savior Helmet +4";
                Player.ChestEquipped = "Savior Chestplate +4";
                Player.LegsEquipped = "Savior Leggings +4";
                Player.WeaponEquipped = "Savior Sword +4";
                Player.currentHealth += 400;
                Player.maxhealth += 400;
                return "You equip the savior set";
            }

            string itemName = input.Length > 6 ? input.Remove(0, 6) : input;
            return "You don't have a " + itemName + " to equip";
        }

        private string HandleUnequipCommand(string input, string lowerInput)
        {
            if (lowerInput.Contains("iron sword") && Player.WeaponEquipped == "Iron Sword +1")
            {
                Player.WeaponEquipped = "nothing";
                return "You unequip Iron sword";
            }
            if (lowerInput.Contains("fire sword") && Player.WeaponEquipped == "Fire Sword +2")
            {
                Player.WeaponEquipped = "nothing";
                return "You unequip Fire Sword";
            }
            if (lowerInput.Contains("topaz sword") && Player.WeaponEquipped == "Topaz Sword +3")
            {
                Player.WeaponEquipped = "nothing";
                return "You unequip Topaz Sword";
            }
            if (lowerInput.Contains("savior sword") && Player.WeaponEquipped == "Savior Sword +4")
            {
                Player.WeaponEquipped = "nothing";
                return "You unequip Savior Sword";
            }
            if (lowerInput.Contains("leather leggings") && Player.LegsEquipped == "Leather Leggings +1")
            {
                Player.currentHealth -= 25;
                Player.maxhealth -= 25;
                Player.LegsEquipped = "nothing";
                return "You unequip Leather Leggings";
            }
            if (lowerInput.Contains("iron leggings") && Player.LegsEquipped == "Iron Leggings +2")
            {
                Player.currentHealth -= 50;
                Player.maxhealth -= 50;
                Player.LegsEquipped = "nothing";
                return "You unequip Iron Leggings";
            }
            if (lowerInput.Contains("topaz leggings") && Player.LegsEquipped == "Topaz Leggings +3")
            {
                Player.currentHealth -= 75;
                Player.maxhealth -= 75;
                Player.LegsEquipped = "nothing";
                return "You unequip Topaz Leggings";
            }
            if (lowerInput.Contains("savior leggings") && Player.LegsEquipped == "Savior Leggings +4")
            {
                Player.currentHealth -= 100;
                Player.maxhealth -= 100;
                Player.LegsEquipped = "nothing";
                return "You unequip Savior Leggings";
            }
            if (lowerInput.Contains("leather chestplate") && Player.ChestEquipped == "Leather Chestplate +1")
            {
                Player.currentHealth -= 25;
                Player.maxhealth -= 25;
                Player.ChestEquipped = "nothing";
                return "You unequip Leather Chestplate";
            }
            if (lowerInput.Contains("iron chestplate") && Player.ChestEquipped == "Iron Chestplate +2")
            {
                Player.currentHealth -= 50;
                Player.maxhealth -= 50;
                Player.ChestEquipped = "nothing";
                return "You unequip Iron Chestplate";
            }
            if (lowerInput.Contains("topaz chestplate") && Player.ChestEquipped == "Topaz Chestplate +3")
            {
                Player.currentHealth -= 75;
                Player.maxhealth -= 75;
                Player.ChestEquipped = "nothing";
                return "You unequip Topaz Chestplate";
            }
            if (lowerInput.Contains("savior chestplate") && Player.ChestEquipped == "Savior Chestplate +4")
            {
                Player.currentHealth -= 100;
                Player.maxhealth -= 100;
                Player.ChestEquipped = "nothing";
                return "You unequip Savior Chestplate";
            }
            if (lowerInput.Contains("leather helmet") && Player.HeadEquipped == "Leather Helmet +1")
            {
                Player.currentHealth -= 25;
                Player.maxhealth -= 25;
                Player.HeadEquipped = "nothing";
                return "You unequip Leather Helmet";
            }
            if (lowerInput.Contains("iron helmet") && Player.HeadEquipped == "Iron Helmet +2")
            {
                Player.currentHealth -= 50;
                Player.maxhealth -= 50;
                Player.HeadEquipped = "nothing";
                return "You unequip Iron Helmet";
            }
            if (lowerInput.Contains("topaz helmet") && Player.HeadEquipped == "Topaz Helmet +3")
            {
                Player.currentHealth -= 75;
                Player.maxhealth -= 75;
                Player.HeadEquipped = "nothing";
                return "You unequip Topaz Helmet";
            }
            if (lowerInput.Contains("savior helmet") && Player.HeadEquipped == "Savior Helmet +4")
            {
                Player.currentHealth -= 100;
                Player.maxhealth -= 100;
                Player.HeadEquipped = "nothing";
                return "You unequip Savior Helmet";
            }
            if (lowerInput.Contains("savior set"))
            {
                Player.HeadEquipped = "nothing";
                Player.ChestEquipped = "nothing";
                Player.LegsEquipped = "nothing";
                Player.WeaponEquipped = "nothing";
                Player.currentHealth -= 400;
                Player.maxhealth -= 400;
                return "You unequip the savior set";
            }

            string itemName = input.Length > 8 ? input.Remove(0, 8) : input;
            return "You don't have a " + itemName + " to unequip";
        }

        public string checkInput(string input) // where the magic happens
        {
            string trimmedInput = input.Trim();
            string lowerInput = trimmedInput.ToLowerInvariant();

            if (MatchesAny(lowerInput, "look around", "see around", "search", "inspect surroundings"))
            {
                return game.roomsubtext[game.currentRoomIndex];
            }

            if (TryGetNamedValue(trimmedInput, "gender", out string genderValue))
            {
                Player.storeGender(genderValue);
                return $"Your gender is {Player.Gender}\n";
            }

            if (TryGetNamedValue(trimmedInput, "name", out string nameValue))
            {
                Player.storeName(nameValue);
                return BuildNameResponse();
            }

            if (TryGetMovementResponse(lowerInput, out string movementResponse))
            {
                return movementResponse;
            }

            if (MatchesAny(lowerInput, "fight", "kill", "murder", "mordor") || lowerInput.Contains("fight"))
            {
                if (string.IsNullOrWhiteSpace(enemy.currentEnemy))
                {
                    return "There is nothing here itching for a fight.";
                }
                if (enemy.isdefeated == true)
                {
                    return "Whatever was here has already been dealt with.";
                }

                Player.isFighting = true;
                return "\n" + enemy.desc + "\n" + "You begin fighting\n" + enemy.MonsterInfo();
            }

            if (MatchesAny(lowerInput, "check self", "inspect self", "stats", "whoami"))
            {
                return Player.playerInfo();
            }

            if (MatchesAny(lowerInput, "hit", "slash", "bap", "fuckemup"))
            {
                return Player.isFighting ? Combat() : $"There is nothing to {input}";
            }

            if (trimmedInput.Contains("Who is the cutest cat on the planet?"))
            {
                return "Fun fact, there is a tie between the two cutest cat's on the planet! One is named Jojo, the other is named Toulouse.";
            }

            if (trimmedInput.Contains("Emmy"))
            {
                return "What are you doing saying the game-creators girlfriend's name in this console?";
            }

            if (string.IsNullOrWhiteSpace(trimmedInput))
            {
                return "You have to write something!";
            }

            if (lowerInput == "die")
            {
                return "I bet you thought I'd say you can't die huh?";
            }

            if (lowerInput == "run")
            {
                Player.isFighting = false;
                return "You got away sucessfully";
            }

            if (lowerInput == "map")
            {
                return game.getMap();
            }

            if (lowerInput.Contains("equip"))
            {
                return HandleEquipCommand(trimmedInput, lowerInput);
            }

            if (lowerInput.Contains("take off"))
            {
                return HandleUnequipCommand(trimmedInput, lowerInput);
            }

            if (lowerInput == "explore")
            {
                return ExploreCurrentRoom();
            }

            if (ContainsAny(lowerInput, "pick up", "search ground", "loot") && enemy.isdefeated == true)
            {
                if (string.IsNullOrWhiteSpace(enemy.placedObject))
                {
                    return "There is nothing on the ground worth taking.";
                }

                string pickedUpItem = enemy.placedObject;
                Player.giveItem(pickedUpItem);
                enemy.ClearEncounter();
                return pickedUpItem == "Pickaxe" ? "Use the mine command use it when necessary!" : $"You pick up the {pickedUpItem}";
            }

            if (ContainsAny(lowerInput, "display commands", "show commands"))
            {
                MessageBox.Show(" Look around ---> gain more information about your surroundings \n Gender (gender) ---> input your gender \n name (name) ---> Input your name \n " +
                    "Fight ---> Fight the current monster in the room \n Hit ---> Hit the current monster (must first be fighting) \n check self ---> learn more information about yourself \n" +
                    " Inventory ---> look at your inventory \n equip (item) ---> toggle current equipment \n " +
                    "Search Ground ---> Pick up items on the ground \n Explore ---> look for repeatable encounters or loot in cleared rooms \n North, South, East, West ---> Walk in direction written\n" + "Run ---> If you're in combat, get out of combat \n Save ---> writes the default save file \n Save <filename> ---> writes a named save file \n Load ---> restores your default save", "Dawnbarrow Commands");
                return "";
            }

            if (ContainsAny(lowerInput, "display hidden commands", "show hidden commands"))
            {
                MessageBox.Show("Who is the cutest cat on the planet? ---> Find out \n die ---> Find out if you can die! \n Cheat ---> Gives full savior kit (ur a terrible person)", "Dawnbarrow Hidden Commands (ur sneaky)");
                return "";
            }

            if (ContainsAny(lowerInput, "pick up", "search ground", "loot") && string.IsNullOrWhiteSpace(enemy.placedObject) && string.IsNullOrWhiteSpace(enemy.currentEnemy))
            {
                return "There is nothing on the ground worth taking.";
            }

            if (ContainsAny(lowerInput, "pick up", "search ground", "loot") && enemy.isdefeated == false)
            {
                return "It's not safe enough to pick up the item in this room!";
            }

            if (lowerInput.Contains("inventory"))
            {
                return Player.displayInventory();
            }

            if (lowerInput.Contains("give friendship bracelet") && Player.hasFriendshipBracelet == true)
            {
                enemy.enemyCHP = 0;
                enemy.isdefeated = true;
                Player.isFighting = false;
                defeatedRooms.Add(GetCurrentRoomKey());
                return "You made the Lonely Giant happy, and thus he became your friend, feel free to pick up the saviors helmet before you go!";
            }

            if (lowerInput.Contains("cheat"))
            {
                Player.cheat();
                return "You have cheated!!! You have all quest items and all four pieces of the savior set";
            }

            if (lowerInput == "save")
            {
                return SaveGame();
            }

            if (lowerInput.StartsWith("save "))
            {
                return SaveGame(trimmedInput.Substring(5).Trim());
            }

            if (lowerInput == "load")
            {
                return LoadGame();
            }

            if (ContainsAny(lowerInput, "mine", "smash rocks") && Player.hasPickaxe == false)
            {
                return "You have nothing with which to mine or smash things with";
            }
            else
            if (ContainsAny(lowerInput, "mine", "smash rocks") && (Player.hasPickaxe == true) && (room.getCurrentRoomCoordinates().x == 4) && (room.getCurrentRoomCoordinates().y == 3))
            {
                Player.giveItem("Boss Key");
                return "You smash the rocks into a million pieces, inside the rocks is a key!";
            }
            else
            if (ContainsAny(lowerInput, "mine", "smash rocks") && (Player.hasPickaxe == true))
            {
                return "There is nothing to mine or smash";
            }
            else
            if (lowerInput.Contains("throw talking cat") && (room.getCurrentRoomCoordinates().x == 5) && (room.getCurrentRoomCoordinates().y == 4))
            {
                enemy.enemyCHP -= 100;
                return "The dragon ddn't like that at all, the cat scratches the ferocious beasts eyes!";
            }

            return $"You can't {input}";
        }

        private string GetSaveFilePath(string? saveName = null)
        {
            string exeDirectory = AppContext.BaseDirectory;
            DirectoryInfo? parentDirectory = Directory.GetParent(exeDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            string fileName = "Dawnbarrow_save.json";

            if (string.IsNullOrWhiteSpace(saveName) == false)
            {
                char[] invalidChars = Path.GetInvalidFileNameChars();
                string sanitizedName = new string(saveName.Trim().Where(character => invalidChars.Contains(character) == false).ToArray());

                if (string.IsNullOrWhiteSpace(sanitizedName))
                {
                    throw new ArgumentException("That save name is not valid.");
                }

                fileName = sanitizedName.EndsWith(".json", StringComparison.OrdinalIgnoreCase) ? sanitizedName : sanitizedName + ".json";
            }

            if (parentDirectory == null)
            {
                return Path.Combine(exeDirectory, fileName);
            }

            return Path.Combine(parentDirectory.FullName, fileName);
        }

        private string GetRoomKey(int x, int y)
        {
            return $"{x},{y}";
        }

        private string GetCurrentRoomKey()
        {
            (int x, int y) coords = room.getCurrentRoomCoordinates();
            return GetRoomKey(coords.x, coords.y);
        }

        private string PrepareRoomEncounter()
        {
            (int x, int y) coords = room.getCurrentRoomCoordinates();

            if (defeatedRooms.Contains(GetRoomKey(coords.x, coords.y)))
            {
                enemy.ClearEncounter();

                if (room.IsRepeatableEncounterRoom(coords.x, coords.y))
                {
                    return "\n This area has calmed down for now, but you could try exploring if you want to stir something up.";
                }

                return "\n This room has already been cleared.";
            }

            enemy.enemySpawn(coords.x, coords.y);
            return "";
        }

        private string ExploreCurrentRoom()
        {
            (int x, int y) coords = room.getCurrentRoomCoordinates();
            string roomKey = GetRoomKey(coords.x, coords.y);

            if (Player.isFighting)
            {
                return "You're a little busy fighting right now.";
            }

            if (room.IsRepeatableEncounterRoom(coords.x, coords.y) == false)
            {
                return "There's nothing new to explore here, this room's important business is already settled.";
            }

            if (defeatedRooms.Contains(roomKey) == false)
            {
                return "There is already something important going on in this room. Deal with that first.";
            }

            if (enemy.isdefeated == true && string.IsNullOrWhiteSpace(enemy.placedObject) == false)
            {
                return $"You already found a {enemy.placedObject} here. Search Ground if you want it.";
            }

            if (string.IsNullOrWhiteSpace(enemy.currentEnemy) == false && enemy.isdefeated == false)
            {
                return $"{enemy.currentEnemy} is already lurking here. Type fight if you want to engage.";
            }

            if (room.randomEncounter())
            {
                enemy.SpawnExploreEncounter(room.Biome(coords.x, coords.y));
                return "You explore the area and stir something up...\n" + enemy.desc + "\n" + enemy.MonsterInfo() + "\nType fight if you want to engage.";
            }

            string foundItem = exploreItems[exploreRandom.Next(exploreItems.Count)];
            enemy.ClearEncounter();
            enemy.placedObject = foundItem;
            enemy.isdefeated = true;
            return $"You explore the area and find a {foundItem}! Search Ground to pick it up.";
        }

        private string SaveGame(string? saveName = null)
        {
            SaveData saveData = new SaveData
            {
                RoomX = room.getCurrentRoomCoordinates().x,
                RoomY = room.getCurrentRoomCoordinates().y,
                CurrentRoomIndex = game.currentRoomIndex,
                ConsoleText = ConsoleOut.Text,
                DefeatedRooms = new List<string>(defeatedRooms),
                Player = new PlayerSaveData
                {
                    PlayerName = Player.playerName,
                    Gender = Player.Gender,
                    Level = Player.lvl,
                    MaxHealth = Player.maxhealth,
                    CurrentHealth = Player.currentHealth,
                    Armor = Player.armor,
                    Damage = Player.dmg,
                    HeadEquipped = Player.HeadEquipped,
                    ChestEquipped = Player.ChestEquipped,
                    LegsEquipped = Player.LegsEquipped,
                    WeaponEquipped = Player.WeaponEquipped,
                    IsFighting = Player.isFighting,
                    XpToNextLevel = Player.xptonextlevel,
                    CurrentXp = Player.currentxp,
                    HasLadder = Player.hasLadder,
                    HasPickaxe = Player.hasPickaxe,
                    HasBossKey = Player.hasBossKey,
                    HasTalkingCat = Player.hasTalkingCat,
                    HasFriendshipBracelet = Player.hasFriendshipBracelet,
                    HasIronSword = Player.hasIronSword,
                    HasFireSword = Player.hasFireSword,
                    HasTopazSword = Player.hasTopazSword,
                    HasSaviorSword = Player.hasSaviorSword,
                    HasLeatherHelmet = Player.hasLeatherHelmet,
                    HasIronHelmet = Player.hasIronHelmet,
                    HasTopazHelmet = Player.hasTopazHelmet,
                    HasSaviorHelmet = Player.hasSaviorHelmet,
                    HasLeatherChestplate = Player.hasLeatherChestplate,
                    HasIronChestplate = Player.hasIronChestplate,
                    HasTopazChestplate = Player.hasTopazChestplate,
                    HasSaviorChestplate = Player.hasSaviorChestplate,
                    HasLeatherLeggings = Player.hasLeatherLeggings,
                    HasIronLeggings = Player.hasIronLeggings,
                    HasTopazLeggings = Player.hasTopazLeggings,
                    HasSaviorLeggings = Player.hasSaviorLeggings
                },
                Enemy = new EnemySaveData
                {
                    EnemyHP = enemy.enemyHP,
                    EnemyCHP = enemy.enemyCHP,
                    EnemyArmor = enemy.enemyArmor,
                    EnemyDamage = enemy.enemyDamage,
                    CurrentEnemy = enemy.currentEnemy,
                    EnemyXpGiven = enemy.enemyxpgiven,
                    Description = enemy.desc,
                    PlacedObject = enemy.placedObject,
                    IsDefeated = enemy.isdefeated,
                    NeedsTalkingCat = enemy.needsTalkingCat,
                    NeedsLadder = enemy.needsLadder,
                    NeedsPickaxe = enemy.needsPickaxe,
                    NeedsBossKey = enemy.needsBossKey,
                    NeedsFriendshipBracelet = enemy.needsFriendshipBracelet
                }
            };

            string savePath;

            try
            {
                savePath = GetSaveFilePath(saveName);
            }
            catch (ArgumentException exception)
            {
                return exception.Message;
            }

            string saveJson = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(savePath, saveJson);
            return $"Game saved to {savePath}";
        }

        private string LoadGame()
        {
            string savePath = GetSaveFilePath();

            if (File.Exists(savePath) == false)
            {
                return $"No save file found at {savePath}";
            }

            string saveJson = File.ReadAllText(savePath);
            SaveData? saveData = JsonSerializer.Deserialize<SaveData>(saveJson);

            if (saveData == null)
            {
                return "The save file exists, but it could not be read.";
            }

            room.setCurrentRoom(saveData.RoomX, saveData.RoomY);
            game.setCurrentRoom(saveData.RoomX, saveData.RoomY);
            game.currentRoomIndex = saveData.CurrentRoomIndex;
            defeatedRooms.Clear();
            foreach (string defeatedRoom in saveData.DefeatedRooms)
            {
                defeatedRooms.Add(defeatedRoom);
            }

            Player.playerName = saveData.Player.PlayerName;
            Player.Gender = saveData.Player.Gender;
            Player.lvl = saveData.Player.Level;
            Player.maxhealth = saveData.Player.MaxHealth;
            Player.currentHealth = saveData.Player.CurrentHealth;
            Player.armor = saveData.Player.Armor;
            Player.dmg = saveData.Player.Damage;
            Player.HeadEquipped = saveData.Player.HeadEquipped;
            Player.ChestEquipped = saveData.Player.ChestEquipped;
            Player.LegsEquipped = saveData.Player.LegsEquipped;
            Player.WeaponEquipped = saveData.Player.WeaponEquipped;
            Player.isFighting = saveData.Player.IsFighting;
            Player.xptonextlevel = saveData.Player.XpToNextLevel;
            Player.currentxp = saveData.Player.CurrentXp;
            Player.hasLadder = saveData.Player.HasLadder;
            Player.hasPickaxe = saveData.Player.HasPickaxe;
            Player.hasBossKey = saveData.Player.HasBossKey;
            Player.hasTalkingCat = saveData.Player.HasTalkingCat;
            Player.hasFriendshipBracelet = saveData.Player.HasFriendshipBracelet;
            Player.hasIronSword = saveData.Player.HasIronSword;
            Player.hasFireSword = saveData.Player.HasFireSword;
            Player.hasTopazSword = saveData.Player.HasTopazSword;
            Player.hasSaviorSword = saveData.Player.HasSaviorSword;
            Player.hasLeatherHelmet = saveData.Player.HasLeatherHelmet;
            Player.hasIronHelmet = saveData.Player.HasIronHelmet;
            Player.hasTopazHelmet = saveData.Player.HasTopazHelmet;
            Player.hasSaviorHelmet = saveData.Player.HasSaviorHelmet;
            Player.hasLeatherChestplate = saveData.Player.HasLeatherChestplate;
            Player.hasIronChestplate = saveData.Player.HasIronChestplate;
            Player.hasTopazChestplate = saveData.Player.HasTopazChestplate;
            Player.hasSaviorChestplate = saveData.Player.HasSaviorChestplate;
            Player.hasLeatherLeggings = saveData.Player.HasLeatherLeggings;
            Player.hasIronLeggings = saveData.Player.HasIronLeggings;
            Player.hasTopazLeggings = saveData.Player.HasTopazLeggings;
            Player.hasSaviorLeggings = saveData.Player.HasSaviorLeggings;

            enemy.enemyHP = saveData.Enemy.EnemyHP;
            enemy.enemyCHP = saveData.Enemy.EnemyCHP;
            enemy.enemyArmor = saveData.Enemy.EnemyArmor;
            enemy.enemyDamage = saveData.Enemy.EnemyDamage;
            enemy.currentEnemy = saveData.Enemy.CurrentEnemy;
            enemy.enemyxpgiven = saveData.Enemy.EnemyXpGiven;
            enemy.desc = saveData.Enemy.Description;
            enemy.placedObject = saveData.Enemy.PlacedObject;
            enemy.isdefeated = saveData.Enemy.IsDefeated;
            enemy.needsTalkingCat = saveData.Enemy.NeedsTalkingCat;
            enemy.needsLadder = saveData.Enemy.NeedsLadder;
            enemy.needsPickaxe = saveData.Enemy.NeedsPickaxe;
            enemy.needsBossKey = saveData.Enemy.NeedsBossKey;
            enemy.needsFriendshipBracelet = saveData.Enemy.NeedsFriendshipBracelet;

            currentOutput = "";
            currentCharIndex = 0;
            typingQueue.Clear();
            typingTimer.Stop();
            ConsoleOut.Text = saveData.ConsoleText;
            Player.calculateArmor();
            updateBackground();
            updatelabels();
            label1.Text = room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y) + room.getCurrentRoomCoordinates().ToString();

            return $"Game loaded from {savePath}";
        }



        public string Combat()
        {
            string output = "";
            int playerDamage = Player.playerDmg(enemy.enemyArmor);

            output += $"Player {Player.playerName} hits {enemy.currentEnemy} for {playerDamage}!\n";
            enemy.enemyCHP -= playerDamage;
            output += "The enemy " + enemy.currentEnemy + " has " + enemy.enemyCHP + "/" + enemy.enemyHP + "Hit Points Remaining \n";

            if (enemy.enemyCHP <= 0)
            {
                enemy.enemyCHP = 0;
                output += "You have sucessfully defeated the enemy!\n";
                output += Player.experience(enemy.enemyxpgiven);
                enemy.isdefeated = true;
                Player.isFighting = false;
                defeatedRooms.Add(GetCurrentRoomKey());
            }

            if (enemy.enemyCHP != 0)
            {
                int monsterDamage = enemy.MonsterDmg(Player.armor);
                output += $"Enemy {enemy.currentEnemy} hits you for {monsterDamage}\n";
                Player.currentHealth -= monsterDamage;
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
            if (Player.ChestEquipped == "nothing")
            {
                Chestplate.Image = Properties.Resources.EChest;
            }
            if (Player.ChestEquipped == "Topaz Chestplate +3")
            {
                Chestplate.Image = Properties.Resources.TChest;
            }
            if (Player.ChestEquipped == "Iron Chestplate +2")
            {
                Chestplate.Image = Properties.Resources.IChest;
            }
            if (Player.ChestEquipped == "Leather Chestplate +1")
            {
                Chestplate.Image = Properties.Resources.LChest;
            }
            if (Player.ChestEquipped == "Savior Chestplate +4")
            {
                Chestplate.Image = Properties.Resources.SChest;
            }
            if (Player.HeadEquipped == "nothing")
            {
                Helmet.Image = Properties.Resources.EHelm;
            }
            if (Player.HeadEquipped == "Leather Helmet +1")
            {
                Helmet.Image = Properties.Resources.LHelm;
            }
            if (Player.HeadEquipped == "Iron Helmet +2")
            {
                Helmet.Image = Properties.Resources.IHelm;
            }
            if (Player.HeadEquipped == "Topaz Helmet +3")
            {
                Helmet.Image = Properties.Resources.THelm;
            }
            if (Player.HeadEquipped == "Savior Helmet +4")
            {
                Helmet.Image = Properties.Resources.SHelm;
            }
            if (Player.LegsEquipped == "nothing")
            {
                Leggings.Image = Properties.Resources.ELegs__1_;
            }
            if (Player.LegsEquipped == "Leather Leggings +1")
            {
                Leggings.Image = Properties.Resources.LLegs;
            }
            if (Player.LegsEquipped == "Iron Leggings +2")
            {
                Leggings.Image = Properties.Resources.ILegs;
            }
            if (Player.LegsEquipped == "Topaz Leggings +3")
            {
                Leggings.Image = Properties.Resources.TLegs;
            }
            if (Player.LegsEquipped == "Savior Leggings +4")
            {
                Leggings.Image = Properties.Resources.SLegs;
            }
        }

       
    }
}