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
        private readonly List<string> commandHistory = new List<string>();
        private int commandHistoryIndex = -1;
        private const string inputPlaceholderText = "Click To Type Here";
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
            public double MaxMana { get; set; }
            public double CurrentMana { get; set; }
            public int Armor { get; set; }
            public int Damage { get; set; }
            public int Gold { get; set; }
            public string HeadEquipped { get; set; } = "nothing";
            public string ChestEquipped { get; set; } = "nothing";
            public string LegsEquipped { get; set; } = "nothing";
            public string WeaponEquipped { get; set; } = "nothing";
            public bool IsFighting { get; set; }
            public bool IsGameOver { get; set; }
            public bool HasWon { get; set; }
            public double XpToNextLevel { get; set; }
            public double CurrentXp { get; set; }
            public bool HasLadder { get; set; }
            public bool HasPickaxe { get; set; }
            public bool HasBossKey { get; set; }
            public bool HasTalkingCat { get; set; }
            public bool HasFriendshipBracelet { get; set; }
            public int FireBombCount { get; set; }
            public int FreezingScrollCount { get; set; }
            public int FireScrollCount { get; set; }
            public int FlightScrollCount { get; set; }
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
            public int GoldDrop { get; set; }
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
            InputBox.KeyDown += InputBox_KeyDown;
            LoadGameData();
            typingTimer = new System.Windows.Forms.Timer();
            typingTimer.Interval = 1; //typing speed
            typingTimer.Tick += TypingTimerTick;
            for (int i = 0; i < titemlist.Count; i++)
            {

                int itemid = titemlist.IndexOf(titemlist[i]) + 1;

            }

            (int x, int y) startRoom = WorldData.GetStartCoordinates();
            room.setCurrentRoom(startRoom.x, startRoom.y);
            game.setCurrentRoom(startRoom.x, startRoom.y);
            SpawnRoomEncounterWithFallback(startRoom.x, startRoom.y);
            label1.Text = room.Biome(startRoom.x, startRoom.y) + room.getCurrentRoomCoordinates().ToString();
            updateBackground();
            updatelabels();

        }

        private static int GetEquipmentHealthBonus(string itemName)
        {
            if (itemName.Contains("+1"))
            {
                return 25;
            }

            if (itemName.Contains("+2"))
            {
                return 50;
            }

            if (itemName.Contains("+3"))
            {
                return 75;
            }

            if (itemName.Contains("+4"))
            {
                return 100;
            }

            return 0;
        }

        private void SetEquippedArmor(Func<string> currentItemGetter, Action<string> currentItemSetter, string nextItem)
        {
            string currentItem = currentItemGetter();

            if (currentItem == nextItem)
            {
                return;
            }

            int healthDelta = GetEquipmentHealthBonus(nextItem) - GetEquipmentHealthBonus(currentItem);
            currentItemSetter(nextItem);

            if (healthDelta == 0)
            {
                return;
            }

            Player.maxhealth = Math.Max(1, Player.maxhealth + healthDelta);
            Player.currentHealth = Math.Max(0, Math.Min(Player.maxhealth, Player.currentHealth + healthDelta));
        }

        private void RememberCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            if (string.Equals(input, inputPlaceholderText, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (commandHistory.Count == 0 || commandHistory[commandHistory.Count - 1] != input)
            {
                commandHistory.Add(input);
            }

            commandHistoryIndex = commandHistory.Count;
        }

        private void SetInputFromHistoryIndex()
        {
            if (commandHistoryIndex < 0)
            {
                commandHistoryIndex = 0;
            }

            if (commandHistoryIndex > commandHistory.Count)
            {
                commandHistoryIndex = commandHistory.Count;
            }

            InputBox.Text = commandHistoryIndex == commandHistory.Count ? "" : commandHistory[commandHistoryIndex];
            InputBox.SelectionStart = InputBox.Text.Length;
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
            RememberCommand(PlayerInput);
            string gameResponse = checkInput(PlayerInput);
            string normalizedDirection = room.direction(PlayerInput);
            bool isMovementCommand = normalizedDirection == "north" || normalizedDirection == "south" || normalizedDirection == "east" || normalizedDirection == "west";
            bool canMove = Player.isGameOver == false && Player.hasWon == false;

            (int x, int y) nextroomCoordinates = room.GetNextRoomIndex(normalizedDirection);


            {
                if (isMovementCommand && canMove == false)
                {
                    outputText = $"{playerAction} \n {gameResponse}";
                }
                else if (isMovementCommand && Player.isFighting == true)
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

        private bool TryParseCoordinatePair(string rawCoordinates, out int x, out int y)
        {
            x = 0;
            y = 0;

            if (string.IsNullOrWhiteSpace(rawCoordinates))
            {
                return false;
            }

            string[] parts = rawCoordinates
                .Replace("(", "")
                .Replace(")", "")
                .Replace(",", " ")
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                return false;
            }

            return int.TryParse(parts[0], out x) && int.TryParse(parts[1], out y);
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

            if((Player.playerName == "Jojo") || (Player.playerName == "Toulouse"))
            {
                return response + "You share a name with one of the cutest cats on the planet! \n";
            }

            if ((Player.playerName == "Bryant") || (Player.playerName == "Bryant Lanier") || (Player.playerName == "bryant") || (Player.playerName == "bryant lanier") || (Player.playerName == "Brandon Lanier") || (Player.playerName == "brandon lanier") || (Player.playerName == "Brandon") || (Player.playerName == "brandon"))
            {
                return response + "You share a name with one of the developers brother's! \n";
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
                SetEquippedArmor(() => Player.LegsEquipped, value => Player.LegsEquipped = value, "Leather Leggings +1");
                return "You equip Leather Leggings";
            }
            if (lowerInput.Contains("iron leggings") && Player.hasIronLeggings == true)
            {
                SetEquippedArmor(() => Player.LegsEquipped, value => Player.LegsEquipped = value, "Iron Leggings +2");
                return "You equip Iron Leggings";
            }
            if (lowerInput.Contains("topaz leggings") && Player.hasTopazLeggings == true)
            {
                SetEquippedArmor(() => Player.LegsEquipped, value => Player.LegsEquipped = value, "Topaz Leggings +3");
                return "You equip Topaz Leggings";
            }
            if (lowerInput.Contains("savior leggings") && Player.hasSaviorLeggings == true)
            {
                SetEquippedArmor(() => Player.LegsEquipped, value => Player.LegsEquipped = value, "Savior Leggings +4");
                return "You equip Savior Leggings +4";
            }
            if (lowerInput.Contains("leather chestplate") && Player.hasLeatherChestplate == true)
            {
                SetEquippedArmor(() => Player.ChestEquipped, value => Player.ChestEquipped = value, "Leather Chestplate +1");
                return "You equip Leather Chestplate";
            }
            if (lowerInput.Contains("iron chestplate") && Player.hasIronChestplate == true)
            {
                SetEquippedArmor(() => Player.ChestEquipped, value => Player.ChestEquipped = value, "Iron Chestplate +2");
                return "You equip Iron Chestplate";
            }
            if (lowerInput.Contains("topaz chestplate") && Player.hasTopazChestplate == true)
            {
                SetEquippedArmor(() => Player.ChestEquipped, value => Player.ChestEquipped = value, "Topaz Chestplate +3");
                return "You equip Topaz Chestplate";
            }
            if (lowerInput.Contains("savior chestplate") && Player.hasSaviorChestplate == true)
            {
                SetEquippedArmor(() => Player.ChestEquipped, value => Player.ChestEquipped = value, "Savior Chestplate +4");
                return "You equip Savior Chestplate";
            }
            if (lowerInput.Contains("leather helmet") && Player.hasLeatherHelmet == true)
            {
                SetEquippedArmor(() => Player.HeadEquipped, value => Player.HeadEquipped = value, "Leather Helmet +1");
                return "You equip Leather Helmet +1";
            }
            if (lowerInput.Contains("iron helmet") && Player.hasIronHelmet == true)
            {
                SetEquippedArmor(() => Player.HeadEquipped, value => Player.HeadEquipped = value, "Iron Helmet +2");
                return "You equip Iron Helmet +2";
            }
            if (lowerInput.Contains("topaz helmet") && Player.hasTopazHelmet == true)
            {
                SetEquippedArmor(() => Player.HeadEquipped, value => Player.HeadEquipped = value, "Topaz Helmet +3");
                return "You equip Topaz Helmet +3";
            }
            if (lowerInput.Contains("savior helmet") && Player.hasSaviorHelmet == true)
            {
                SetEquippedArmor(() => Player.HeadEquipped, value => Player.HeadEquipped = value, "Savior Helmet +4");
                return "You equip savior Helmet";
            }
            if (lowerInput.Contains("savior set"))
            {
                SetEquippedArmor(() => Player.HeadEquipped, value => Player.HeadEquipped = value, "Savior Helmet +4");
                SetEquippedArmor(() => Player.ChestEquipped, value => Player.ChestEquipped = value, "Savior Chestplate +4");
                SetEquippedArmor(() => Player.LegsEquipped, value => Player.LegsEquipped = value, "Savior Leggings +4");
                Player.WeaponEquipped = "Savior Sword +4";
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
                SetEquippedArmor(() => Player.LegsEquipped, value => Player.LegsEquipped = value, "nothing");
                return "You unequip Leather Leggings";
            }
            if (lowerInput.Contains("iron leggings") && Player.LegsEquipped == "Iron Leggings +2")
            {
                SetEquippedArmor(() => Player.LegsEquipped, value => Player.LegsEquipped = value, "nothing");
                return "You unequip Iron Leggings";
            }
            if (lowerInput.Contains("topaz leggings") && Player.LegsEquipped == "Topaz Leggings +3")
            {
                SetEquippedArmor(() => Player.LegsEquipped, value => Player.LegsEquipped = value, "nothing");
                return "You unequip Topaz Leggings";
            }
            if (lowerInput.Contains("savior leggings") && Player.LegsEquipped == "Savior Leggings +4")
            {
                SetEquippedArmor(() => Player.LegsEquipped, value => Player.LegsEquipped = value, "nothing");
                return "You unequip Savior Leggings";
            }
            if (lowerInput.Contains("leather chestplate") && Player.ChestEquipped == "Leather Chestplate +1")
            {
                SetEquippedArmor(() => Player.ChestEquipped, value => Player.ChestEquipped = value, "nothing");
                return "You unequip Leather Chestplate";
            }
            if (lowerInput.Contains("iron chestplate") && Player.ChestEquipped == "Iron Chestplate +2")
            {
                SetEquippedArmor(() => Player.ChestEquipped, value => Player.ChestEquipped = value, "nothing");
                return "You unequip Iron Chestplate";
            }
            if (lowerInput.Contains("topaz chestplate") && Player.ChestEquipped == "Topaz Chestplate +3")
            {
                SetEquippedArmor(() => Player.ChestEquipped, value => Player.ChestEquipped = value, "nothing");
                return "You unequip Topaz Chestplate";
            }
            if (lowerInput.Contains("savior chestplate") && Player.ChestEquipped == "Savior Chestplate +4")
            {
                SetEquippedArmor(() => Player.ChestEquipped, value => Player.ChestEquipped = value, "nothing");
                return "You unequip Savior Chestplate";
            }
            if (lowerInput.Contains("leather helmet") && Player.HeadEquipped == "Leather Helmet +1")
            {
                SetEquippedArmor(() => Player.HeadEquipped, value => Player.HeadEquipped = value, "nothing");
                return "You unequip Leather Helmet";
            }
            if (lowerInput.Contains("iron helmet") && Player.HeadEquipped == "Iron Helmet +2")
            {
                SetEquippedArmor(() => Player.HeadEquipped, value => Player.HeadEquipped = value, "nothing");
                return "You unequip Iron Helmet";
            }
            if (lowerInput.Contains("topaz helmet") && Player.HeadEquipped == "Topaz Helmet +3")
            {
                SetEquippedArmor(() => Player.HeadEquipped, value => Player.HeadEquipped = value, "nothing");
                return "You unequip Topaz Helmet";
            }
            if (lowerInput.Contains("savior helmet") && Player.HeadEquipped == "Savior Helmet +4")
            {
                SetEquippedArmor(() => Player.HeadEquipped, value => Player.HeadEquipped = value, "nothing");
                return "You unequip Savior Helmet";
            }
            if (lowerInput.Contains("savior set"))
            {
                SetEquippedArmor(() => Player.HeadEquipped, value => Player.HeadEquipped = value, "nothing");
                SetEquippedArmor(() => Player.ChestEquipped, value => Player.ChestEquipped = value, "nothing");
                SetEquippedArmor(() => Player.LegsEquipped, value => Player.LegsEquipped = value, "nothing");
                Player.WeaponEquipped = "nothing";
                return "You unequip the savior set";
            }

            string itemName = input.Length > 8 ? input.Remove(0, 8) : input;
            return "You don't have a " + itemName + " to unequip";
        }

        private string ResolveEnemyCounterattack()
        {
            if (enemy.enemyCHP <= 0)
            {
                return "";
            }

            string statusOutput = ProcessEnemyStatusEffectsAtTurnStart();
            if (enemy.enemyCHP <= 0)
            {
                statusOutput += HandleEnemyDefeat(enemy.currentEnemy);
                return statusOutput;
            }

            statusOutput += ProcessPlayerStatusEffectsAtTurnStart();
            if (Player.currentHealth <= 0)
            {
                return statusOutput;
            }

            if (ConsumeEnemyFrozenTurn(out string frozenOutput))
            {
                return statusOutput + frozenOutput;
            }

            if (TryResolveEnemyUniqueSkill(out string uniqueSkillOutput))
            {
                string uniqueOutput = statusOutput + uniqueSkillOutput;
                uniqueOutput += "The player has" + Player.currentHealth + "/" + Player.maxhealth + "Hit Points Remaining \n";

                if (Player.currentHealth <= 0)
                {
                    Player.currentHealth = 0;
                    Player.isFighting = false;
                    Player.isGameOver = true;
                    uniqueOutput += "You have 0 health remaining. Game Over. Type restart to begin again, or load to restore a save.";
                }

                return uniqueOutput;
            }

            int monsterDamage = enemy.MonsterDmg(Player.armor);
            Player.currentHealth -= monsterDamage;

            string output = statusOutput + $"Enemy {enemy.currentEnemy} hits you for {monsterDamage}\n";
            output += "The player has" + Player.currentHealth + "/" + Player.maxhealth + "Hit Points Remaining \n";

            if (Player.currentHealth <= 0)
            {
                Player.currentHealth = 0;
                Player.isFighting = false;
                Player.isGameOver = true;
                output += "You have 0 health remaining. Game Over. Type restart to begin again, or load to restore a save.";
            }

            return output;
        }

        private string UseConsumable(string itemName)
        {
            if (itemName == "Fire Bomb")
            {
                if (Player.fireBombCount <= 0)
                {
                    return "You don't have any Fire Bombs left.";
                }

                if (Player.isFighting == false)
                {
                    return "Save the Fire Bomb for a fight unless you're trying to burn your own boots off.";
                }

                Player.fireBombCount--;
                int damage = Math.Max(exploreRandom.Next(16, 29) - enemy.enemyArmor, 0);
                string output = ResolveCombatTechnique("Fire Bomb", damage, 0, "The bomb bursts in a spray of flame and sparks.", () =>
                {
                    ApplyEnemyBurn(3, 6);
                });

                if (Player.fireBombCount == 0)
                {
                    output += "\nThat was your last Fire Bomb.";
                }

                return output;
            }

            if (itemName == "Scroll of Freezing")
            {
                if (Player.freezingScrollCount <= 0)
                {
                    return "You don't have any Scrolls of Freezing left.";
                }

                if (Player.isFighting == false)
                {
                    return "Use the Scroll of Freezing in combat.";
                }

                Player.freezingScrollCount--;
                ApplyEnemyFreeze(3);
                return "You unfurl the Scroll of Freezing. Ice locks your foe in place for 3 turns.";
            }

            if (itemName == "Scroll of Fire")
            {
                if (Player.fireScrollCount <= 0)
                {
                    return "You don't have any Scrolls of Fire left.";
                }

                if (Player.isFighting == false)
                {
                    return "Use the Scroll of Fire in combat.";
                }

                Player.fireScrollCount--;
                int burstDamage = Math.Max(exploreRandom.Next(10, 17) - enemy.enemyArmor, 0);
                return ResolveCombatTechnique("Scroll of Fire", burstDamage, 0, "Flame sigils burst across the enemy.", () =>
                {
                    ApplyEnemyBurn(4, 7);
                });
            }

            if (itemName == "Scroll of Flight")
            {
                if (Player.flightScrollCount <= 0)
                {
                    return "You don't have any Scrolls of Flight left.";
                }

                return "To use Scroll of Flight, type: use scroll of flight x y";
            }

            return $"You can't use {itemName} right now.";
        }

        private string HandleEnemyDefeat(string enemyName)
        {
            string output = "You have sucessfully defeated the enemy!\n";
            output += Player.experience(enemy.enemyxpgiven);
            Player.gold += enemy.goldDrop;
            output += $"You loot {enemy.goldDrop} gold.\n";
            ClearEnemyStatusEffects();
            enemy.isdefeated = true;
            Player.isFighting = false;
            defeatedRooms.Add(GetCurrentRoomKey());

            if (enemyName == "Coward")
            {
                Player.playerName = "Coward";
                output += "The shame sticks. Your name is now Coward.\n";
            }

            if (enemyName == "DRAGON")
            {
                Player.hasWon = true;
                Player.isGameOver = true;
                output += "The dragon collapses and Dawnbarrow is finally safe. You won.\nType restart to begin again, or save your victory.\n";
            }

            return output;
        }

        private string ResolveCombatTechnique(string actionName, int damage, int manaCost, string extraText = "", Action? onHit = null)
        {
            if (Player.isFighting == false)
            {
                return $"There is nothing here to use {actionName} on.";
            }

            if (manaCost > 0 && Player.currentMana < manaCost)
            {
                return $"You don't have enough mana to use {actionName}.";
            }

            if (manaCost > 0)
            {
                Player.currentMana -= manaCost;
            }

            string enemyName = enemy.currentEnemy;
            enemy.enemyCHP -= damage;

            string output = $"You use {actionName} and hit {enemyName} for {damage}!\n";
            if (string.IsNullOrWhiteSpace(extraText) == false)
            {
                output += extraText + "\n";
            }

            onHit?.Invoke();

            if (enemy.enemyCHP <= 0)
            {
                enemy.enemyCHP = 0;
                output += "The enemy " + enemyName + " has 0/" + enemy.enemyHP + "Hit Points Remaining \n";
                output += HandleEnemyDefeat(enemyName);
                return output;
            }

            output += "The enemy " + enemyName + " has " + enemy.enemyCHP + "/" + enemy.enemyHP + "Hit Points Remaining \n";
            output += ResolveEnemyCounterattack();
            return output;
        }

        private string CastSpell(string lowerInput)
        {
            if (lowerInput == "fireball")
            {
                int damage = Math.Max(exploreRandom.Next(12, 23) - enemy.enemyArmor, 0);
                return ResolveCombatTechnique("Fireball", damage, 6, "The fireball explodes against your enemy in a hot burst.", () =>
                {
                    ApplyEnemyBurn(2, 5);
                });
            }

            if (lowerInput == "ice ball")
            {
                int damage = Math.Max(exploreRandom.Next(8, 17) - enemy.enemyArmor, 0);
                return ResolveCombatTechnique("Ice Ball", damage, 5, "Frost clings to your foe and dulls its edge.", () =>
                {
                    if (enemy.enemyDamage > 1)
                    {
                        enemy.enemyDamage--;
                    }
                });
            }

            if (lowerInput == "heal")
            {
                if (Player.isFighting == false)
                {
                    return "You can patch yourself up later, but there is no battle pressure right now.";
                }

                if (Player.currentMana < 7)
                {
                    return "You don't have enough mana to cast Heal.";
                }

                Player.currentMana -= 7;
                double healAmount = exploreRandom.Next(12, 23);
                Player.currentHealth = Math.Min(Player.maxhealth, Player.currentHealth + healAmount);
                ApplyPlayerRegen(2, 4);

                string output = $"You cast Heal and recover {healAmount} health.\n";
                output += "Regen surrounds you for 2 turns.\n";
                output += "The player has" + Player.currentHealth + "/" + Player.maxhealth + "Hit Points Remaining \n";
                output += ResolveEnemyCounterattack();
                return output;
            }

            return "That spell fizzles before it even starts.";
        }

        private string UseSkill(string lowerInput)
        {
            if (lowerInput == "bash")
            {
                int damage = Math.Max(exploreRandom.Next(6, 13) - enemy.enemyArmor, 0);
                return ResolveCombatTechnique("Bash", damage, 0, "The impact rattles your enemy and dents its defenses.", () =>
                {
                    ApplyEnemyArmorBreak(3, 1);
                });
            }

            if (lowerInput == "slice")
            {
                int damage = Math.Max(Player.playerDmg(enemy.enemyArmor) + 4, 0);
                return ResolveCombatTechnique("Slice", damage, 0, "You follow through with a cleaner, deeper cut.", () =>
                {
                    ApplyEnemyBleed(2, 4);
                });
            }

            if (lowerInput == "ultra instinct")
            {
                if (Player.isFighting == false)
                {
                    return "There is nothing here to go Ultra Instinct against.";
                }

                if (Player.currentMana < 10)
                {
                    return "You don't have enough mana to tap into Ultra Instinct.";
                }

                Player.currentMana -= 10;
                int damage = Math.Max(exploreRandom.Next(14, 26) - enemy.enemyArmor, 0);
                string enemyName = enemy.currentEnemy;
                enemy.enemyCHP -= damage;

                string output = $"You slip into Ultra Instinct and strike {enemyName} for {damage}!\n";
                if (enemy.enemyCHP <= 0)
                {
                    enemy.enemyCHP = 0;
                    output += "The enemy " + enemyName + " has 0/" + enemy.enemyHP + "Hit Points Remaining \n";
                    output += HandleEnemyDefeat(enemyName);
                    return output;
                }

                output += "The enemy " + enemyName + " has " + enemy.enemyCHP + "/" + enemy.enemyHP + "Hit Points Remaining \n";
                output += "You evade the counterattack entirely.\n";
                return output;
            }

            if (lowerInput == "rupture")
            {
                int damage = Math.Max(Player.playerDmg(enemy.enemyArmor) + 6, 0);
                return ResolveCombatTechnique("Rupture", damage, 5, "You carve deep and leave a heavy wound.", () =>
                {
                    ApplyEnemyBleed(4, 6);
                });
            }

            if (lowerInput == "battle trance")
            {
                if (Player.isFighting == false)
                {
                    return "There is no battle to enter a trance for.";
                }

                if (Player.currentMana < 6)
                {
                    return "You don't have enough mana for Battle Trance.";
                }

                Player.currentMana -= 6;
                ApplyPlayerRegen(3, 6);
                return "You enter Battle Trance. Regen will restore your health for 3 turns.";
            }

            return "That skill is all confidence and no execution.";
        }

        private bool IsShopRoom()
        {
            (int x, int y) coords = room.getCurrentRoomCoordinates();
            return coords.x == 3 && coords.y == 1;
        }

        private string GetShopText()
        {
            List<string> lines = new List<string>
            {
                "A traveling merchant has set up in this calm stretch of jungle."
            };

            foreach (ShopEntry entry in shopEntries)
            {
                lines.Add($"Type {entry.Command} for {entry.Price} gold. {entry.Description}");
            }

            return string.Join("\n", lines);
        }

        private string HandleBuyCommand(string lowerInput)
        {
            if (IsShopRoom() == false)
            {
                return "There is nobody here selling anything.";
            }

            if (Player.isFighting)
            {
                return "Now is not the time to browse wares.";
            }

            ShopEntry? selectedEntry = shopEntries.FirstOrDefault(entry => entry.Command == lowerInput);
            if (selectedEntry == null)
            {
                return "The merchant doesn't stock that.";
            }

            if (lowerInput == "buy fire bomb")
            {
                int cost = selectedEntry.Price;
                if (Player.gold < cost)
                {
                    return "You don't have enough gold for a Fire Bomb.";
                }

                Player.gold -= cost;
                Player.fireBombCount++;
                updatelabels();
                return "You buy a Fire Bomb and tuck it away for later.";
            }

            if (lowerInput == "buy heal")
            {
                int cost = selectedEntry.Price;
                if (Player.gold < cost)
                {
                    return "You don't have enough gold for healing.";
                }

                if (Player.currentHealth >= Player.maxhealth)
                {
                    return "You're already at full health.";
                }

                Player.gold -= cost;
                Player.currentHealth = Math.Min(Player.maxhealth, Player.currentHealth + 20);
                updatelabels();
                return "The merchant patches you up and you feel steadier already.";
            }

            if (lowerInput == "buy mana")
            {
                int cost = selectedEntry.Price;
                if (Player.gold < cost)
                {
                    return "You don't have enough gold for mana.";
                }

                if (Player.currentMana >= Player.maxMana)
                {
                    return "Your mana is already full.";
                }

                Player.gold -= cost;
                Player.currentMana = Math.Min(Player.maxMana, Player.currentMana + 10);
                updatelabels();
                return "You drink a sharp herbal tonic and your mana returns.";
            }

            if (lowerInput == "buy scroll of freezing")
            {
                int cost = selectedEntry.Price;
                if (Player.gold < cost)
                {
                    return "You don't have enough gold for a Scroll of Freezing.";
                }

                Player.gold -= cost;
                Player.freezingScrollCount++;
                updatelabels();
                return "You buy a Scroll of Freezing.";
            }

            if (lowerInput == "buy scroll of fire")
            {
                int cost = selectedEntry.Price;
                if (Player.gold < cost)
                {
                    return "You don't have enough gold for a Scroll of Fire.";
                }

                Player.gold -= cost;
                Player.fireScrollCount++;
                updatelabels();
                return "You buy a Scroll of Fire.";
            }

            if (lowerInput == "buy scroll of flight")
            {
                int cost = selectedEntry.Price;
                if (Player.gold < cost)
                {
                    return "You don't have enough gold for a Scroll of Flight.";
                }

                Player.gold -= cost;
                Player.flightScrollCount++;
                updatelabels();
                return "You buy a Scroll of Flight.";
            }

            return "The merchant doesn't stock that.";
        }

        private string HandleUseCommand(string trimmedInput, string lowerInput)
        {
            if (lowerInput == "use fire bomb" || lowerInput == "throw fire bomb")
            {
                return UseConsumable("Fire Bomb");
            }

            if (lowerInput == "use scroll of freezing")
            {
                return UseConsumable("Scroll of Freezing");
            }

            if (lowerInput == "use scroll of fire")
            {
                return UseConsumable("Scroll of Fire");
            }

            if (lowerInput.StartsWith("use scroll of flight"))
            {
                if (Player.flightScrollCount <= 0)
                {
                    return "You don't have any Scrolls of Flight left.";
                }

                string coordinatesText = trimmedInput.Substring("use scroll of flight".Length).Trim();
                if (TryParseCoordinatePair(coordinatesText, out int targetX, out int targetY) == false)
                {
                    return "Flight format: use scroll of flight x y";
                }

                if (WorldData.RoomExists(targetX, targetY) == false)
                {
                    (int minX, int maxX, int minY, int maxY) = WorldData.GetBounds();
                    return $"Those coordinates do not exist in this world. Valid bounds are X {minX}-{maxX}, Y {minY}-{maxY} and must point to a defined room.";
                }

                if (Player.isFighting)
                {
                    return "You can't safely read the flight scroll mid-fight.";
                }

                Player.flightScrollCount--;
                room.setCurrentRoom(targetX, targetY);
                game.setCurrentRoom(targetX, targetY);
                string roomDescription = game.Output();
                string encounterText = PrepareRoomEncounter();
                label1.Text = room.Biome(targetX, targetY) + room.getCurrentRoomCoordinates().ToString();
                updateBackground();
                updatelabels();
                return $"Arcane wind tears open the distance and drops you at ({targetX},{targetY}).\n{roomDescription}{encounterText}";
            }

            return "You can't use that right now.";
        }

        public string checkInput(string input) // where the magic happens
        {
            string trimmedInput = input.Trim();
            string lowerInput = trimmedInput.ToLowerInvariant();

            if (TryHandleUsageLookup(trimmedInput, lowerInput, out string usageResponse))
            {
                return usageResponse;
            }

            if (Player.isGameOver || Player.hasWon)
            {
                string gameOverResponse = HandleGameOverState(lowerInput);
                if (string.IsNullOrEmpty(gameOverResponse) == false)
                {
                    return gameOverResponse;
                }
            }

            if (MatchesAny(lowerInput, "look around", "see around", "search", "inspect surroundings"))
            {
                return game.GetCurrentRoomSubtext();
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

            if (MatchesAny(lowerInput, "fireball", "ice ball", "heal"))
            {
                return CastSpell(lowerInput);
            }

            if (MatchesAny(lowerInput, "bash", "slice", "ultra instinct", "rupture", "battle trance"))
            {
                return UseSkill(lowerInput);
            }

            if (lowerInput == "shop")
            {
                return IsShopRoom() ? GetShopText() : "There is no shop here.";
            }

            if (lowerInput.StartsWith("buy "))
            {
                return HandleBuyCommand(lowerInput);
            }

            if (lowerInput.StartsWith("use ") || lowerInput.StartsWith("throw "))
            {
                return HandleUseCommand(trimmedInput, lowerInput);
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

            if (lowerInput == "suicide")
            {
                ResetGameState(true);
                return "You end your own adventure. A new one begins immediately.";
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
                    "Fight ---> Fight the current monster in the room \n Hit ---> Hit the current monster (must first be fighting) \n Fireball / Ice Ball / Heal ---> cast combat spells \n Bash / Slice / Ultra Instinct / Rupture / Battle Trance ---> use combat skills \n check self ---> learn more information about yourself \n" +
                    " Inventory ---> look at your inventory \n equip (item) ---> toggle current equipment \n " +
                    "Search Ground ---> Pick up items on the ground \n Explore ---> look for repeatable encounters or loot in cleared rooms \n Shop ---> view merchant stock in the safe jungle room \n Buy <item> ---> spend gold at the shop \n Use Fire Bomb / Use Scroll of Freezing / Use Scroll of Fire ---> consumables \n Use Scroll of Flight x y ---> fast travel \n ? <command> ---> usage help for a specific command \n North, South, East, West ---> Walk in direction written\n" + "Run ---> If you're in combat, get out of combat \n Suicide ---> reset immediately \n Restart ---> begins a new run \n Save ---> writes the default save file \n Save <filename> ---> writes a named save file \n Load ---> restores your default save \n Load <filename> ---> restores a named save", "Dawnbarrow Commands");
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

            if ((lowerInput == "befriend creature" || lowerInput == "friend creature") && Player.hasFriendshipBracelet == true && enemy.currentEnemy == "Lonely Giant")
            {
                enemy.enemyCHP = 0;
                enemy.isdefeated = true;
                Player.isFighting = false;
                defeatedRooms.Add(GetCurrentRoomKey());
                return "You offer friendship instead of violence. The Lonely Giant brightens immediately and lets you take the savior's helmet.";
            }

            if (lowerInput.Contains("cheat"))
            {
                Player.cheat();
                return "Cheat enabled: max-tier gear, huge consumable stocks, massive gold, and boosted stats are now active.";
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

            if (lowerInput.StartsWith("load "))
            {
                return LoadGame(trimmedInput.Substring(5).Trim());
            }

            if (lowerInput == "restart")
            {
                ResetGameState(true);
                return "A new adventure begins.";
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

        private void RenderMiniMap()
        {
            (int x, int y) currentRoom = room.getCurrentRoomCoordinates();
            List<string> lines = new List<string>
            {
                "MINIMAP",
                "P = You",
                "c = Cleared",
                "o = Unknown",
                ""
            };

            (int minX, int maxX, int minY, int maxY) = WorldData.GetBounds();

            for (int row = maxY; row >= minY; row--)
            {
                string line = "";
                for (int column = minX; column <= maxX; column++)
                {
                    if (WorldData.RoomExists(column, row) == false)
                    {
                        line += "   ";
                        continue;
                    }

                    string symbol = "o";
                    if (currentRoom.x == column && currentRoom.y == row)
                    {
                        symbol = "P";
                    }
                    else if (defeatedRooms.Contains(GetRoomKey(column, row)))
                    {
                        symbol = "c";
                    }

                    line += "[" + symbol + "]";
                }

                lines.Add(line);
            }

            lines.Add("");
            lines.Add("Biome:");
            lines.Add(room.Biome(currentRoom.x, currentRoom.y));
            MiniMap.Text = string.Join("\n", lines);
        }

        private string PrepareRoomEncounter()
        {
            (int x, int y) coords = room.getCurrentRoomCoordinates();

            if (defeatedRooms.Contains(GetRoomKey(coords.x, coords.y)))
            {
                enemy.ClearEncounter();
                ClearEnemyStatusEffects();

                if (room.IsRepeatableEncounterRoom(coords.x, coords.y))
                {
                    return "\n This area has calmed down for now, but you could try exploring if you want to stir something up.";
                }

                return "\n This room has already been cleared.";
            }

            SpawnRoomEncounterWithFallback(coords.x, coords.y);
            ClearEnemyStatusEffects();
            return "";
        }

        private void ResetGameState(bool clearConsole)
        {
            game = new Game();
            room = new Room();
            Player = new Player();
            enemy = new Enemy();
            defeatedRooms.Clear();
            ClearEnemyStatusEffects();
            ClearPlayerStatusEffects();

            (int x, int y) startRoom = WorldData.GetStartCoordinates();
            room.setCurrentRoom(startRoom.x, startRoom.y);
            game.setCurrentRoom(startRoom.x, startRoom.y);
            SpawnRoomEncounterWithFallback(startRoom.x, startRoom.y);

            currentOutput = "";
            currentCharIndex = 0;
            typingQueue.Clear();
            typingTimer.Stop();

            if (clearConsole)
            {
                ConsoleOut.Clear();
            }

            label1.Text = room.Biome(startRoom.x, startRoom.y) + room.getCurrentRoomCoordinates().ToString();
            updateBackground();
            updatelabels();
        }

        private string HandleGameOverState(string lowerInput)
        {
            if (lowerInput == "restart")
            {
                ResetGameState(true);
                return "A new adventure begins.";
            }

            if (lowerInput == "save" || lowerInput.StartsWith("save ") || lowerInput == "load" || lowerInput.StartsWith("load "))
            {
                return "";
            }

            if (Player.hasWon)
            {
                return "The adventure is over. Type restart to play again, or load to revisit a save.";
            }

            return "Game Over. Type restart to begin again, or load to restore a save.";
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

            if (IsShopRoom())
            {
                return "The merchant already has this spot claimed. Type shop if you want to browse instead of wandering around.";
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
                if (TrySpawnExploreEncounterFromData(room.Biome(coords.x, coords.y)) == false)
                {
                    enemy.ClearEncounter();
                    enemy.isdefeated = false;
                    enemy.currentEnemy = "Wandering Monster";
                    enemy.enemyHP = exploreRandom.Next(6, 15);
                    enemy.enemyCHP = enemy.enemyHP;
                    enemy.enemyArmor = exploreRandom.Next(0, 3);
                    enemy.enemyDamage = exploreRandom.Next(1, 5);
                    enemy.enemyxpgiven = exploreRandom.Next(5, 17);
                    enemy.desc = "You explore the area and provoke an unknown wandering enemy.";
                    enemy.goldDrop = Math.Max(1, exploreRandom.Next(2, 12));
                }
                ClearEnemyStatusEffects();
                return "You explore the area and stir something up...\n" + enemy.desc + "\n" + enemy.MonsterInfo() + "\nType fight if you want to engage.";
            }

            string foundItem = exploreItems[exploreRandom.Next(exploreItems.Count)];
            enemy.ClearEncounter();
            ClearEnemyStatusEffects();
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
                    MaxMana = Player.maxMana,
                    CurrentMana = Player.currentMana,
                    Armor = Player.armor,
                    Damage = Player.dmg,
                    Gold = Player.gold,
                    HeadEquipped = Player.HeadEquipped,
                    ChestEquipped = Player.ChestEquipped,
                    LegsEquipped = Player.LegsEquipped,
                    WeaponEquipped = Player.WeaponEquipped,
                    IsFighting = Player.isFighting,
                    IsGameOver = Player.isGameOver,
                    HasWon = Player.hasWon,
                    XpToNextLevel = Player.xptonextlevel,
                    CurrentXp = Player.currentxp,
                    HasLadder = Player.hasLadder,
                    HasPickaxe = Player.hasPickaxe,
                    HasBossKey = Player.hasBossKey,
                    HasTalkingCat = Player.hasTalkingCat,
                    HasFriendshipBracelet = Player.hasFriendshipBracelet,
                    FireBombCount = Player.fireBombCount,
                    FreezingScrollCount = Player.freezingScrollCount,
                    FireScrollCount = Player.fireScrollCount,
                    FlightScrollCount = Player.flightScrollCount,
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
                    GoldDrop = enemy.goldDrop,
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

        private string LoadGame(string? saveName = null)
        {
            string savePath;

            try
            {
                savePath = GetSaveFilePath(saveName);
            }
            catch (ArgumentException exception)
            {
                return exception.Message;
            }

            if (File.Exists(savePath) == false)
            {
                return $"No save file found at {savePath}";
            }

            SaveData? saveData;

            try
            {
                string saveJson = File.ReadAllText(savePath);
                saveData = JsonSerializer.Deserialize<SaveData>(saveJson);
            }
            catch (JsonException)
            {
                return "The save file is corrupted or invalid JSON.";
            }
            catch (IOException exception)
            {
                return $"Could not load save: {exception.Message}";
            }

            if (saveData == null)
            {
                return "The save file exists, but it could not be read.";
            }

            (int x, int y) loadedRoom = WorldData.RoomExists(saveData.RoomX, saveData.RoomY)
                ? (saveData.RoomX, saveData.RoomY)
                : WorldData.GetStartCoordinates();

            room.setCurrentRoom(loadedRoom.x, loadedRoom.y);
            game.setCurrentRoom(loadedRoom.x, loadedRoom.y);
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
            Player.maxMana = saveData.Player.MaxMana;
            Player.currentMana = saveData.Player.CurrentMana;
            Player.armor = saveData.Player.Armor;
            Player.dmg = saveData.Player.Damage;
            Player.gold = saveData.Player.Gold;
            Player.HeadEquipped = saveData.Player.HeadEquipped;
            Player.ChestEquipped = saveData.Player.ChestEquipped;
            Player.LegsEquipped = saveData.Player.LegsEquipped;
            Player.WeaponEquipped = saveData.Player.WeaponEquipped;
            Player.isFighting = saveData.Player.IsFighting;
            Player.isGameOver = saveData.Player.IsGameOver;
            Player.hasWon = saveData.Player.HasWon;
            Player.xptonextlevel = saveData.Player.XpToNextLevel;
            Player.currentxp = saveData.Player.CurrentXp;
            Player.hasLadder = saveData.Player.HasLadder;
            Player.hasPickaxe = saveData.Player.HasPickaxe;
            Player.hasBossKey = saveData.Player.HasBossKey;
            Player.hasTalkingCat = saveData.Player.HasTalkingCat;
            Player.hasFriendshipBracelet = saveData.Player.HasFriendshipBracelet;
            Player.fireBombCount = saveData.Player.FireBombCount;
            Player.freezingScrollCount = saveData.Player.FreezingScrollCount;
            Player.fireScrollCount = saveData.Player.FireScrollCount;
            Player.flightScrollCount = saveData.Player.FlightScrollCount;
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
            enemy.goldDrop = saveData.Enemy.GoldDrop;
            enemy.desc = saveData.Enemy.Description;
            enemy.placedObject = saveData.Enemy.PlacedObject;
            enemy.isdefeated = saveData.Enemy.IsDefeated;
            enemy.needsTalkingCat = saveData.Enemy.NeedsTalkingCat;
            enemy.needsLadder = saveData.Enemy.NeedsLadder;
            enemy.needsPickaxe = saveData.Enemy.NeedsPickaxe;
            enemy.needsBossKey = saveData.Enemy.NeedsBossKey;
            enemy.needsFriendshipBracelet = saveData.Enemy.NeedsFriendshipBracelet;
            ClearEnemyStatusEffects();
            ClearPlayerStatusEffects();

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
            string enemyName = enemy.currentEnemy;
            int playerDamage = Player.playerDmg(enemy.enemyArmor);

            output += $"Player {Player.playerName} hits {enemyName} for {playerDamage}!\n";
            enemy.enemyCHP -= playerDamage;
            output += "The enemy " + enemyName + " has " + enemy.enemyCHP + "/" + enemy.enemyHP + "Hit Points Remaining \n";

            if (enemy.enemyCHP <= 0)
            {
                enemy.enemyCHP = 0;
                output += HandleEnemyDefeat(enemyName);
                return output;
            }

            output += ResolveEnemyCounterattack();
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

        private void InputBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (commandHistory.Count == 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                commandHistoryIndex = Math.Max(0, commandHistoryIndex - 1);
                SetInputFromHistoryIndex();
                return;
            }

            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                commandHistoryIndex = Math.Min(commandHistory.Count, commandHistoryIndex + 1);
                SetInputFromHistoryIndex();
            }
        }

        private void InputBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (InputBox.Text == inputPlaceholderText)
            {
                InputBox.Text = "";
            }
        }

        public void updatelabels()
        { //every label in the game except currcoordinates lol
            PlayerHP.Text = $"CurrHP: {Player.currentHealth} / TotHP: {Player.maxhealth}\nMana: {Player.currentMana} / {Player.maxMana}";
            XP.Text = $"CurrXP: {Player.currentxp} / Xp2nex: {Player.xptonextlevel}\nGold: {Player.gold}";
            //ArAt.Text = $"Armor: {Player.armor} /  Attack: {Player.dmg}";
            NGL.Text = $"{Player.playerName} / {Player.Gender} / Lvl: {Player.lvl}";
            Equip.Text = $"Currently Equipped:\n {Player.HeadEquipped},\n {Player.ChestEquipped},\n {Player.LegsEquipped},\n {Player.WeaponEquipped}";
            RenderMiniMap();
            RefreshInventoryPanel();
            RefreshShopPanel();
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
                Background.Image = global::Dawnbarrow.Properties.Resources.Forest1;
            }
            else
            if (room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y) == "Jungle")
            {
                Background.Image = global::Dawnbarrow.Properties.Resources._51;
            }
            else
            if (room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y) == "Grassland")
            {
                Background.Image = global::Dawnbarrow.Properties.Resources.grassland;
            }
            else
            if (room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y) == "River")
            {
                Background.Image = global::Dawnbarrow.Properties.Resources.River;
            }
            else
            if (room.Biome(room.getCurrentRoomCoordinates().x, room.getCurrentRoomCoordinates().y) == "Mountain Pass")
            {
                Background.Image = global::Dawnbarrow.Properties.Resources.Mountain_Pass___Ending;
            }

            if (Player.hasFriendshipBracelet == true)
            {
                FriendshipBracelet.Image = global::Dawnbarrow.Properties.Resources.FriendshipBraceletOn;
            }
            if (Player.hasPickaxe == true)
            {
                Pickaxe.Image = global::Dawnbarrow.Properties.Resources.PickaxeOn;
            }
            if (Player.hasTalkingCat == true)
            {
                TalkingCat.Image = global::Dawnbarrow.Properties.Resources.TalkingCatOn;
            }
            if (Player.hasLadder == true)
            {
                Ladder.Image = global::Dawnbarrow.Properties.Resources.LadderOn;
            }
            if (Player.hasBossKey == true)
            {
                BossKey.Image = global::Dawnbarrow.Properties.Resources.BosskeyON;
            }
            if (Player.ChestEquipped == "nothing")
            {
                Chestplate.Image = global::Dawnbarrow.Properties.Resources.EChest;
            }
            if (Player.ChestEquipped == "Topaz Chestplate +3")
            {
                Chestplate.Image = global::Dawnbarrow.Properties.Resources.TChest;
            }
            if (Player.ChestEquipped == "Iron Chestplate +2")
            {
                Chestplate.Image = global::Dawnbarrow.Properties.Resources.IChest;
            }
            if (Player.ChestEquipped == "Leather Chestplate +1")
            {
                Chestplate.Image = global::Dawnbarrow.Properties.Resources.LChest;
            }
            if (Player.ChestEquipped == "Savior Chestplate +4")
            {
                Chestplate.Image = global::Dawnbarrow.Properties.Resources.SChest;
            }
            if (Player.HeadEquipped == "nothing")
            {
                Helmet.Image = global::Dawnbarrow.Properties.Resources.EHelm;
            }
            if (Player.HeadEquipped == "Leather Helmet +1")
            {
                Helmet.Image = global::Dawnbarrow.Properties.Resources.LHelm;
            }
            if (Player.HeadEquipped == "Iron Helmet +2")
            {
                Helmet.Image = global::Dawnbarrow.Properties.Resources.IHelm;
            }
            if (Player.HeadEquipped == "Topaz Helmet +3")
            {
                Helmet.Image = global::Dawnbarrow.Properties.Resources.THelm;
            }
            if (Player.HeadEquipped == "Savior Helmet +4")
            {
                Helmet.Image = global::Dawnbarrow.Properties.Resources.SHelm;
            }
            if (Player.LegsEquipped == "nothing")
            {
                Leggings.Image = global::Dawnbarrow.Properties.Resources.ELegs__1_;
            }
            if (Player.LegsEquipped == "Leather Leggings +1")
            {
                Leggings.Image = global::Dawnbarrow.Properties.Resources.LLegs;
            }
            if (Player.LegsEquipped == "Iron Leggings +2")
            {
                Leggings.Image = global::Dawnbarrow.Properties.Resources.ILegs;
            }
            if (Player.LegsEquipped == "Topaz Leggings +3")
            {
                Leggings.Image = global::Dawnbarrow.Properties.Resources.TLegs;
            }
            if (Player.LegsEquipped == "Savior Leggings +4")
            {
                Leggings.Image = global::Dawnbarrow.Properties.Resources.SLegs;
            }
        }

       
    }
}